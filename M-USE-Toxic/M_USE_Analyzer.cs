using Interfaces;

namespace M_USE_Toxic;

public class M_USE_Analyzer : IDataAnalyzer
{
    private PythonRunner _runner;

    private string _interpreterPath;
    private string _scriptPath;

    private Action<string> _errorHandler;
    private Action<PredictResult> _predictResultHandler;

    private CancellationTokenSource _cancellationToken;
    private bool _isDisposeCalled;
    
    public void Configure(string scriptPath, string interpreterPath, Action<PredictResult> predictResultHandler, Action<string> errorHandler) => 
        (_interpreterPath, _scriptPath, _predictResultHandler, _errorHandler) = (interpreterPath, scriptPath, predictResultHandler, errorHandler);

    public void Initialize()
    {
        Console.WriteLine("Start initialization... ");
        var isReady = false;
        _isDisposeCalled = false;
        _cancellationToken = new CancellationTokenSource();
        _runner = new PythonRunner(_interpreterPath);

        var result = _runner.Run(_scriptPath, _cancellationToken.Token);

        _runner.OnErrorReceivedEvent += RunnerOnErrorReceivedEventHandler;
        _runner.OnExitEvent += RunnerOnExitEventHandler;

        _runner.OnInitializationEndedEvent += () => isReady = true;
        while (!isReady) Thread.Sleep(1000);

        Console.WriteLine("Script started");
    }

    public void Dispose()
    {
        _isDisposeCalled = true;
        _cancellationToken.Cancel();
        _runner.OnErrorReceivedEvent -= RunnerOnErrorReceivedEventHandler;
        _runner.OnExitEvent -= RunnerOnExitEventHandler;
        Console.WriteLine("Script disposed");
    }

    public void Analyze(IDataFrame dataFrame)
    {
        _runner.RequestPredict(dataFrame.Text);
        _predictResultHandler.Invoke(new PredictResult { DataFrame = dataFrame, Toxicity = _runner.GetPredict() });
    }

    private void RunnerOnExitEventHandler()
    {
        Console.WriteLine("Exit from script");
        Dispose();
        if (!_isDisposeCalled) Initialize();
    }

    private void RunnerOnErrorReceivedEventHandler(string errorMessage)
    {
        _errorHandler.Invoke(errorMessage);
    }
}