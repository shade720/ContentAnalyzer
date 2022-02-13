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
        if (string.IsNullOrEmpty(_currentAnalyzer)) throw new Exception("Current analyzer is not set");
        _dataAnalyzers[_currentAnalyzer].Initialize();
    }

    public void Analyze(IDataFrame dataFrame)
    {
        if (string.IsNullOrEmpty(_currentAnalyzer)) throw new Exception("Current analyzer is not set");
        _dataAnalyzers[_currentAnalyzer].Analyze(dataFrame);
    }

    public void Stop()
    {
        if (string.IsNullOrEmpty(_currentAnalyzer)) throw new Exception("Current analyzer is not set");
        _dataAnalyzers[_currentAnalyzer].Dispose();
    }
}