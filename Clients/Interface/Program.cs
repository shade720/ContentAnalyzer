using ContentAnalyzer.Frontend.Desktop.Forms;

namespace ContentAnalyzer.Frontend.Desktop;

internal static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new MainWindow());
    }
}