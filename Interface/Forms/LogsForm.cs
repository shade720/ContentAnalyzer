using System.Globalization;
using Common;

namespace Interface.Forms;

public partial class LogsForm : Form
{
    private readonly MainWindow _parent;

    public LogsForm(MainWindow parent)
    {
        _parent = parent;
        InitializeComponent();
        Logger.OnLoggingEvent += LoggerOnLoggingEvent;
    }

    private void AppendTextBox(string value)
    {
        if (InvokeRequired)
        {
            Invoke(new Action<string>(AppendTextBox), value);
            return;
        }
        LogWindowTextbox.Text += value;
    }
    private void LoggerOnLoggingEvent(string log)
    {
        AppendTextBox($"[{DateTime.Now.ToString(CultureInfo.InvariantCulture)}] {log}\n");
    }

    private void LogWindowTextbox_TextChanged(object sender, EventArgs e)
    {
        LogWindowTextbox.SelectionStart = LogWindowTextbox.Text.Length;
        LogWindowTextbox.ScrollToCaret();
    }
}