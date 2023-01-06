using System.Text;
using BERTTokenizers;
using DataAnalysisService.BusinessLogicLayer.NeuralModels.Base;
using DataAnalysisService.BusinessLogicLayer.NeuralModels.BERT.Base;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using Newtonsoft.Json;
using Serilog;

namespace DataAnalysisService.BusinessLogicLayer.NeuralModels.BERT;

public class BertModel : NeuralModel
{
    private readonly string _tokenizerVocabPath;
    private readonly string _onnxModelPath;
    private readonly string _labelEncodingJsonPath;

    private BertCasedCustomVocabulary _tokenizer;
    private InferenceSession _model;
    private Dictionary<string, string> _labelEncoding;

    public BertModel(string tokenizerVocabPath, string onnxModelPath, string labelEncodingJsonPath)
    {
        if (string.IsNullOrEmpty(tokenizerVocabPath) || !File.Exists(tokenizerVocabPath)) throw new ArgumentException("Tokenizer vocabulary file does not exists!");
        if (string.IsNullOrEmpty(onnxModelPath) || !File.Exists(onnxModelPath)) throw new ArgumentException("ONNX model file does not exists!");
        if (string.IsNullOrEmpty(labelEncodingJsonPath) || !File.Exists(labelEncodingJsonPath)) throw new ArgumentException("Label encoder file does not exists!");
        _tokenizerVocabPath = tokenizerVocabPath;
        _onnxModelPath = onnxModelPath;
        _labelEncodingJsonPath = labelEncodingJsonPath;
        Title = "Sensetive-topics-BERT";
    }

    public override void Initialize()
    {
        if (IsInitialized)
            throw new Exception("Model already initialized!");
        _tokenizer = new BertCasedCustomVocabulary(_tokenizerVocabPath);
        _model = new InferenceSession(_onnxModelPath);
        var json = File.ReadAllText(_labelEncodingJsonPath);
        var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
        _labelEncoding = dict ?? throw new ArgumentException("Incorrect tokenizer vocabulary file!");
        IsInitialized = true;
        Log.Logger.Information("Model {title} started", Title);
    }

    private static string RemoveSpecialCharacters(string str)
    {
        var sb = new StringBuilder();
        foreach (var c in str.Where(c => c >= ' ' && c <= '~' || c >= 'А' && c <= 'я'))
        {
            sb.Append(c);
        }
        sb = sb.Replace("\n", "");
        sb = sb.Replace("\t", "");
        sb = sb.Replace("\b", "");
        return sb.ToString();
    }

    public override PredictResult Predict(string sentence)
    {
        if (!IsInitialized)
            throw new Exception("Can't predict the sentence, model is not initialized!");
        sentence = RemoveSpecialCharacters(sentence);

        var tokens = _tokenizer.Tokenize(sentence);
        var encoded = _tokenizer.Encode(tokens.Count, sentence);

        var bertInput = new BertInput
        {
            InputIds = encoded.Select(t => t.InputIds).ToArray(),
            AttentionMask = encoded.Select(t => t.AttentionMask).ToArray()
        };

        var inputIds = ConvertToTensor(bertInput.InputIds, bertInput.InputIds.Length);
        var attentionMask = ConvertToTensor(bertInput.AttentionMask, bertInput.InputIds.Length);

        var input = new List<NamedOnnxValue> {
            NamedOnnxValue.CreateFromTensor("input_ids", inputIds),
            NamedOnnxValue.CreateFromTensor("input_mask", attentionMask)
        };

        var output = _model.Run(input);

        var y = output.FirstOrDefault().AsEnumerable<float>().ToList();

        var maxValue = y.Max();
        var label = _labelEncoding[y.IndexOf(maxValue).ToString()];

        var probability = GetProbability(maxValue);

        Log.Logger.Information("{Model} predict {Text} to: {Category} - {Probability}", Title, sentence, label, probability);
        return new PredictResult
        {
            Category = label,
            Probability = probability
        };
    }

    private static double GetProbability(double logit)
    {
        var odd = Math.Exp(logit);
        return odd / (1 + odd);
    }

    private static Tensor<long> ConvertToTensor(IReadOnlyList<long> inputArray, int inputDimension)
    {
        var input = new DenseTensor<long>(new[] { 1, inputDimension });
        for (var i = 0; i < inputArray.Count; i++)
            input[0, i] = inputArray[i];
        return input;
    }

    public override void Dispose()
    {
        if (!IsInitialized)
            throw new Exception("Model already disposed!");
        _model.Dispose();
        IsInitialized = false;
        Log.Logger.Information("Model {title} stopped", Title);
    }
}