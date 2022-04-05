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
        Logger.OnLoggingEvent += AppendTextBox;
    }

    private void AppendTextBox(string value, Logger.LogLevel logLevel)
    {
        if (InvokeRequired)
        {
            BeginInvoke(new Action<string, Logger.LogLevel>(AppendTextBox), value, logLevel);
            return;
        }
        Trim();
        if (logLevel is Logger.LogLevel.Error or Logger.LogLevel.Fatal)
        {
            _parent.ErrorsCountLabel.Text = (int.Parse(_parent.ErrorsCountLabel.Text) + 1).ToString();
            LogWindowTextbox.SelectionStart = LogWindowTextbox.TextLength;
            LogWindowTextbox.SelectionLength = 0;
            LogWindowTextbox.SelectionColor = Color.Red;
        }

        LogWindowTextbox.AppendText(value);
        LogWindowTextbox.SelectionStart = LogWindowTextbox.Text.Length;
        LogWindowTextbox.ScrollToCaret();
        LogWindowTextbox.SelectionColor = LogWindowTextbox.ForeColor;
    }

    private void Trim()
    {
        if (LogWindowTextbox.Lines.Length > 1000) LogWindowTextbox.Text = "";
    }
}