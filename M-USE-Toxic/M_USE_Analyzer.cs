using System.Text;
using System.Text.RegularExpressions;
using Interfaces;

namespace M_USE_Toxic
{
    public class M_USE_Analyzer : IDataAnalyzer
    {
        private PythonRunner _runner;

        private string _interpreterPath;
        private string _scriptPath;

        private Action<string> _errorHandler;
        private Action<PredictResult> _predictResultHandler;

        private CancellationTokenSource _cancellationToken;
        private bool _isDisposeCalled;

        public void Configure(string scriptPath, string interpreterPath, Action<PredictResult> predictResultHandler,
            Action<string> errorHandler) => (_interpreterPath, _scriptPath, _predictResultHandler, _errorHandler) =
            (interpreterPath, scriptPath, predictResultHandler, errorHandler);

        public void Initialize()
        {
            var isReady = false;
            _isDisposeCalled = false;
            _cancellationToken = new CancellationTokenSource();
            _runner = new PythonRunner(_interpreterPath);

            var result = _runner.Run(_scriptPath, _cancellationToken.Token);

            _runner.OnErrorReceivedEvent += RunnerOnErrorReceivedEvent;
            _runner.OnExitEvent += RunnerOnExitEvent;
            _runner.OnInitializedEvent += () => isReady = true;

            while (!isReady) Thread.Sleep(1000);
            _runner.OnOutputReceivedEvent += RunnerOnOutputReceivedEvent;
            Console.WriteLine("Script started");
        }
        public void Dispose()
        {
            _isDisposeCalled = true;
            _cancellationToken.Cancel();
            _runner.OnErrorReceivedEvent -= RunnerOnErrorReceivedEvent;
            _runner.OnOutputReceivedEvent -= RunnerOnOutputReceivedEvent;
            _runner.OnExitEvent -= RunnerOnExitEvent;
            Console.WriteLine("Script stopped");
        }

        public void Analyze(string analyzedText) => _runner.WriteToScript(Process(analyzedText));
        
        private void RunnerOnExitEvent()
        {
            if (!_isDisposeCalled) Initialize();
        }
        private void RunnerOnErrorReceivedEvent(string errorMessage) => _errorHandler.Invoke(errorMessage);

        private void RunnerOnOutputReceivedEvent(string outputMessage)
        {
            var splited = outputMessage.Split('|');
            _predictResultHandler.Invoke(new PredictResult {Text = splited[0], Toxicity = splited[1]});
        }
        private static string ProcessString(string str)
        {
            var regex = new Regex(@"[\p{Cc}\p{Cf}\p{Mn}\p{Me}\p{Zl}\p{Zp}]");
            return regex.Replace(str, "");
        }

        private static string Process(string from)
        {
            var bytes = Encoding.UTF8.GetBytes(from);
            var str = Encoding.UTF8.GetString(bytes);
            return str.Length < 4 ? "null" : str;
        }
    }
}
