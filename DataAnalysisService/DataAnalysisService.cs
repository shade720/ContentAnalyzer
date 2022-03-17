using Interfaces;
namespace DataAnalysisService;

public class DataAnalysisService
{
    private static string _currentAnalyzer;
        
    public static void SetCurrentAnalyzer(string analyzerName) => _currentAnalyzer = analyzerName;

    private readonly Dictionary<string, IDataAnalyzer> _dataAnalyzers = new();

    public void AddDataAnalyzer(string name, Func<IDataAnalyzer> dataAnalyzerConfiguration)
    {
        _dataAnalyzers.Add(name, dataAnalyzerConfiguration.Invoke());
    }

    public void Start()
    {
        foreach (var analyzer in _dataAnalyzers)
        {
            analyzer.Value.Initialize();
        }
        Console.WriteLine("Service started");
    }

    public void Analyze(IDataFrame dataFrame)
    {
        if (string.IsNullOrEmpty(_currentAnalyzer)) throw new Exception("Current analyzer is not set");
        _dataAnalyzers[_currentAnalyzer].Analyze(dataFrame);
    }

    public void Stop()
    {
        foreach (var analyzer in _dataAnalyzers)
        {
            analyzer.Value.Dispose();
        }
        Console.WriteLine("Service stopped");
    }
}