using System.Diagnostics;
using System.Text;

namespace DataAnalysisService.AnalyzeModelController;

internal class PythonRunner
{
    private readonly string _interpreter;

    private StreamWriter? _writer;
    private StreamReader? _reader;

    private Process? _pythonProcess;

    public delegate void OnErrorReceived(string errorMessage);
    public event OnErrorReceived? OnErrorReceivedEvent;

    public delegate void OnExit();
    public event OnExit? OnExitEvent;

    private bool _waiter = true;
    
    public PythonRunner(string interpreter)
    {
        if (interpreter is null)
        {
            throw new ArgumentNullException(nameof(interpreter));
        }
        if (!File.Exists(interpreter))
        {
            throw new FileNotFoundException(interpreter);
        }
        _interpreter = interpreter;
    }

    #region PublicInterface

    public bool Run(string script, string resourcePath, bool waitOnEnd)
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        if (script is null)
        {
            throw new ArgumentNullException(nameof(script));
        }
        if (!File.Exists(script))
        {
            throw new FileNotFoundException(script);
        }
        var startInfo = new ProcessStartInfo(_interpreter)
        {
            UseShellExecute = false,
            CreateNoWindow = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            RedirectStandardInput = true,
            StandardInputEncoding = Encoding.GetEncoding(1251),
            Arguments = $"\"{script}\""
        };

        _pythonProcess = new Process
        {
            StartInfo = startInfo,
            EnableRaisingEvents = true
        };
        try
        {
            _waiter = true;
            _pythonProcess.ErrorDataReceived += OnErrorDataReceivedHandler;
            _pythonProcess.Start();

            _reader = _pythonProcess.StandardOutput;
            _writer = _pythonProcess.StandardInput;
            _writer.AutoFlush = true;
            _pythonProcess.BeginErrorReadLine();

            _writer.WriteLine(resourcePath);

            while (_waiter && waitOnEnd) Thread.Sleep(5000);
        }
        catch (Exception exception)
        {
            throw new Exception($"An error occured during script execution. See inner exception for details. {exception.Message} {exception.StackTrace}");
        }
        finally
        {
            ProcessTermination();
        }
        return true;
    }

    public string ReadFromScript()
    {
        if (_reader is null) throw new Exception($"Reader was null {nameof(ReadFromScript)}");
        var line = _reader.ReadLine();
        if (line is null) throw new Exception($"Got null from script {nameof(ReadFromScript)}");
        return line;
    }

    public void WriteToScript(string text)
    {
        if (_writer is null) throw new Exception($"Writer was null {nameof(WriteToScript)}");
        if (string.IsNullOrEmpty(text)) throw new Exception($"Trying write null or empty to script {nameof(WriteToScript)}");
        _writer.WriteLine(text);
    } 
    public void Abort() => ProcessTermination();

    #endregion

    private void ProcessTermination()
    {
        _waiter = false;
        _reader?.Close();
        _writer?.Close();
        _pythonProcess?.CancelErrorRead();
        _pythonProcess.ErrorDataReceived -= OnErrorDataReceivedHandler;
        _pythonProcess?.Dispose();
        OnExitEvent?.Invoke();
    }
    private void OnErrorDataReceivedHandler(object sender, DataReceivedEventArgs e) => OnErrorReceivedEvent?.Invoke(e.Data ?? string.Empty);
}