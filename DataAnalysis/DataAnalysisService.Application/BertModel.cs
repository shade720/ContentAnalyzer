using BERTTokenizers;
using DataAnalysisService.Domain;
using DataAnalysisService.Domain.Abstractions;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using Newtonsoft.Json;
using Serilog;
using System.Text;

namespace DataAnalysisService.Application;

public class BertModel : IArtificialIntelligenceModel
{
    public string Title { get; }

    private readonly BertCasedCustomVocabulary _tokenizer;
    private readonly InferenceSession _model;
    private readonly Dictionary<string, string> _labelEncoding;

    private const int MaxSentenceLength = 2000;

    public BertModel(
        string modelTitle,
        string tokenizerVocabPath, 
        string onnxModelPath, 
        string labelEncodingJsonPath)
    {
        if (string.IsNullOrEmpty(tokenizerVocabPath) || !File.Exists(tokenizerVocabPath))
            throw new ArgumentException("Tokenizer vocabulary file does not exists!");

        if (string.IsNullOrEmpty(onnxModelPath) || !File.Exists(onnxModelPath)) 
            throw new ArgumentException("ONNX model file does not exists!");

        if (string.IsNullOrEmpty(labelEncodingJsonPath) || !File.Exists(labelEncodingJsonPath)) 
            throw new ArgumentException("Label encoder file does not exists!");

        _tokenizer = new BertCasedCustomVocabulary(tokenizerVocabPath);
        _model = new InferenceSession(onnxModelPath);
        var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(labelEncodingJsonPath));
        _labelEncoding = dict ?? throw new ArgumentException("Incorrect tokenizer vocabulary file!");

        Title = modelTitle;

        Log.Logger.Information("Model {title} initialized", Title);
    }

    public PredictResult Predict(string sentence)
    {
        sentence = RemoveSpecialCharacters(TrimToLength(sentence, MaxSentenceLength));

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

    private static string TrimToLength(string str, int length)
    {
        return string.Join("", str.Take(length));
    }

    private static string RemoveSpecialCharacters(string str)
    {
        var sb = new StringBuilder();
        foreach (var c in str.Where(c => c is 
                     >= ' ' and <= '~' or 
                     >= 'А' and <= 'я'))
        {
            sb.Append(c);
        }
        sb = sb.Replace("\n", "");
        sb = sb.Replace("\t", "");
        sb = sb.Replace("\b", "");
        return sb.ToString();
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

    public void Dispose()
    {
        _model.Dispose();
        Log.Logger.Information("Model {title} disposed", Title);
    }
}