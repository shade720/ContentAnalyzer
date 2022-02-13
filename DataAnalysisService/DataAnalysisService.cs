using Interfaces;
namespace DataAnalysisService
{
    public class DataAnalysisService
    {
        private static Analyzers _currentAnalyzer;
        
        public void SetCurrentAnalyzer(Analyzers analyzerName) => _currentAnalyzer = analyzerName;


        private readonly Dictionary<Analyzers, IDataAnalyzer> _dataAnalyzers = new();

        public void AddDataAnalyzer(Analyzers name, Func<IDataAnalyzer> dataAnalyzerConfiguration)
        {
            _dataAnalyzers.Add(name, dataAnalyzerConfiguration.Invoke());
        }

        public void Start()
        {
            _dataAnalyzers[_currentAnalyzer].Initialize();
        }

        public void Analyze(string text)
        {
            _dataAnalyzers[_currentAnalyzer].Analyze(text);
        }

        public void Stop()
        {
            _dataAnalyzers[_currentAnalyzer].Dispose();
        }
    }

}
