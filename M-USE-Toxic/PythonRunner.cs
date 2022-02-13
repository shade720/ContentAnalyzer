using System.Diagnostics;

namespace M_USE_Toxic;

internal class PythonRunner
{
    private readonly string _interpreter;

    private StreamWriter _writer;

    public delegate void OnErrorReceived(string errorMessage);
    public event OnErrorReceived OnErrorReceivedEvent;

    public delegate void OnOutputReceived(string outputMessage);
    public event OnOutputReceived OnOutputReceivedEvent;

    public delegate void OnExit();
    public event OnExit OnExitEvent;

    public delegate void OnInitialized();
    public event OnInitialized OnInitializedEvent;


    public PythonRunner(string interpreter)
    {
        if (interpreter == null)
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
            if (script == null)
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
                void OnReady(object sender, DataReceivedEventArgs e)
                {
                    OnInitializedEvent.Invoke();
                    process.OutputDataReceived -= OnReady;
                    process.OutputDataReceived += OnOutputDataReceivedHandler;
                }
                process.ErrorDataReceived += OnErrorDataReceivedHandler;
                process.OutputDataReceived += OnReady;
                
                process.Start();

                _writer = process.StandardInput;
                _writer.AutoFlush = true;

                process.BeginErrorReadLine();
                process.BeginOutputReadLine();
                
                while (!token.IsCancellationRequested) Thread.Sleep(5000);
            }
            catch (Exception exception)
            {
                throw new Exception("An error occured during script execution. See inner exception for details.", exception);
            }
            finally
            {
                process.ErrorDataReceived -= OnErrorDataReceivedHandler;
                OnExitEvent.Invoke();
            }
        }, token);
    }

    private void OnOutputDataReceivedHandler(object sender, DataReceivedEventArgs e)
    {
        OnOutputReceivedEvent?.Invoke(e.Data ?? string.Empty);
    }

    private void OnErrorDataReceivedHandler(object sender, DataReceivedEventArgs e)
    {
        OnErrorReceivedEvent?.Invoke(e.Data ?? string.Empty);
    }

    public void WriteToScript(string text)
    {
        _writer.WriteLine(text);
    }
}