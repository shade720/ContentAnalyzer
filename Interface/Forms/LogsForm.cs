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
        Logger.OnLoggingEvent += log=> AppendTextBox($"[{DateTime.Now.ToString(CultureInfo.InvariantCulture)}] {log}\r\n");
    }

    private void AppendTextBox(string value)
    {
        if (InvokeRequired)
        {
            BeginInvoke(new Action<string>(AppendTextBox), value);
            return;
        }
        Trim();
        LogWindowTextbox.AppendText(value);
        LogWindowTextbox.SelectionStart = LogWindowTextbox.Text.Length;
        LogWindowTextbox.ScrollToCaret();
    }

    private void Trim()
    {
        if (LogWindowTextbox.Lines.Length > 500) LogWindowTextbox.Text = "";
    }
}