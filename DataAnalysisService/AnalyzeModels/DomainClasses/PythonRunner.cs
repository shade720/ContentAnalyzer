using System.Diagnostics;
using System.Text;
using Serilog;

namespace DataAnalysisService.AnalyzeModels.DomainClasses;

public class PythonRunner
{
    private readonly string _interpreter;

    private StreamWriter? _writer;
    private StreamReader? _reader;

    private Process? _pythonProcess;

    public delegate void OnErrorReceived(string errorMessage);
    public delegate void OnExited();
    public delegate void OnStarted();

    public event OnErrorReceived? OnErrorReceivedEvent;
    public event OnExited? OnExitedEvent;
    public event OnStarted? OnStartedEvent;

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

    public void Run(string script, string resourcePath)
    {
        if (resourcePath is null) throw new ArgumentNullException(nameof(resourcePath));
        if (!File.Exists(resourcePath)) throw new FileNotFoundException(resourcePath);
        try
        {
            _pythonProcess = CreatePythonProcess(script);
            _pythonProcess.ErrorDataReceived += OnErrorDataReceivedHandler;
            _pythonProcess.Start();
            _reader = _pythonProcess.StandardOutput;
            _writer = _pythonProcess.StandardInput;
            _writer.AutoFlush = true;
            _pythonProcess.BeginErrorReadLine();
            _writer.WriteLine(resourcePath);
            OnStartedEvent?.Invoke();
            _pythonProcess.WaitForExit(-1);
        }
        catch (Exception exception)
        {
            Log.Logger.Fatal("An error occured during script execution. See inner exception for details. {exception.Message} {exception.StackTrace}", exception.Message, exception.StackTrace);
            throw new Exception($"An error occured during script execution. See inner exception for details. {exception.Message} {exception.StackTrace}");
        }
        finally
        {
            _pythonProcess?.CancelErrorRead();
            _reader?.Close();
            _writer?.Close();
            _pythonProcess!.ErrorDataReceived -= OnErrorDataReceivedHandler;
            _pythonProcess.Kill();
            OnExitedEvent?.Invoke();
            Log.Logger.Information("Script stopped");
        }
    }

    public async Task RunAsync(string script, string resourcePath)
    {
        try
        {
            await Task.Run(() => Run(script, resourcePath));
        }
        catch (Exception exception)
        {
            Log.Logger.Fatal("An error occured during script execution. See inner exception for details. {exception.Message} {exception.StackTrace}", exception.Message, exception.StackTrace);
            throw new Exception($"An error occured during script execution. See inner exception for details. {exception.Message} {exception.StackTrace}");
        }
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

    public void Abort() => _pythonProcess?.Kill();


    #endregion

    private Process CreatePythonProcess(string script)
    {
        if (script is null) throw new ArgumentNullException(nameof(script));
        if (!File.Exists(script)) throw new FileNotFoundException(script);
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
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
        return new Process
        {
            StartInfo = startInfo,
            EnableRaisingEvents = true,
        };
    }

    private void OnErrorDataReceivedHandler(object sender, DataReceivedEventArgs e)
    {
        OnErrorReceivedEvent?.Invoke(e.Data ?? string.Empty);
    }
}