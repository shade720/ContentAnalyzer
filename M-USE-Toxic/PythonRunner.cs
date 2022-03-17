using System.Diagnostics;

namespace M_USE_Toxic;

internal class PythonRunner
{
    private readonly string _interpreter;

    private StreamWriter _writer;
    private StreamReader _reader;

    public delegate void OnErrorReceived(string errorMessage);
    public event OnErrorReceived OnErrorReceivedEvent;

    public delegate void OnExit();
    public event OnExit OnExitEvent;

    public delegate void OnInitialized();
    public event OnInitialized OnInitializationEndedEvent;

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

    public async Task Run(string script, CancellationToken token)
    {
        await Task.Run(() =>
        {
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
                Arguments = $"\"{script}\""
            };

            using var process = new Process
            {
                StartInfo = startInfo,
                EnableRaisingEvents = true
            };
            try
            {
                process.ErrorDataReceived += OnErrorDataReceivedHandler;
                
                process.Start();

                _reader = process.StandardOutput;
                _writer = process.StandardInput;
                _writer.AutoFlush = true;

                process.BeginErrorReadLine();

                _reader.ReadLine();
                OnInitializationEndedEvent.Invoke();

                while (!token.IsCancellationRequested) Thread.Sleep(5000);
            }
            catch (Exception exception)
            {
                throw new Exception("An error occured during script execution. See inner exception for details.", exception);
            }
            finally
            {
                process.ErrorDataReceived -= OnErrorDataReceivedHandler;
                OnExitEvent?.Invoke();
            }
        }, token);
    }

    private void OnErrorDataReceivedHandler(object sender, DataReceivedEventArgs e) => OnErrorReceivedEvent?.Invoke(e.Data ?? string.Empty);
    public string GetPredict() => _reader.ReadLine();
    public void RequestPredict(string text) => _writer.WriteLine(text);

}