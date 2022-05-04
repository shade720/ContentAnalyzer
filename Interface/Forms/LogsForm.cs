namespace Interface.Forms;

public partial class LogsForm : Form
{
    private readonly Client _client;
    private LastUploading _lastUploading;

    public LogsForm(Client client)
    {
        _client = client;
        InitializeComponent();
        InitializeLogDateComboBox();
    }

    private void InitializeLogDateComboBox()
    {
        for (var i = 0; i < 7; i++) LogDateComboBox.Items.Add(DateTime.Now.AddDays(-i).Date);
        LogDateComboBox.SelectedIndex = 0;
    }

    private void GetAnalysisLogs_Click(object sender, EventArgs e)
    {
        LogFileParser(_client.GetDataAnalysisServiceLogs(DateTime.Parse(LogDateComboBox.Text)));
        _lastUploading = LastUploading.DataAnalysisServiceLogUploading;
    }

    private void GetCollectionLogs_Click(object sender, EventArgs e)
    {
        LogFileParser(_client.GetDataCollectionServiceLogs(DateTime.Parse(LogDateComboBox.Text)));
        _lastUploading = LastUploading.DataCollectionServiceLogUploading;
    }

    private void RefreshButton_Click(object sender, EventArgs e)
    {
        LogFileParser(_lastUploading == LastUploading.DataCollectionServiceLogUploading ? 
            _client.GetDataCollectionServiceLogs(DateTime.Parse(LogDateComboBox.Text)) :
            _client.GetDataAnalysisServiceLogs(DateTime.Parse(LogDateComboBox.Text)));
    }
    private enum LastUploading
    {
        DataCollectionServiceLogUploading,
        DataAnalysisServiceLogUploading
    }

    private void LogFileParser(string logFile)
    {
        LogGrid.Rows.Clear();
        var logRecords = logFile.Split("~");
        foreach (var log in logRecords)
        {
            if (string.IsNullOrEmpty(log)) continue;
            var date = log[..(log.IndexOf('+') - 2)];
            var level = log[log.IndexOf('[')..(log.IndexOf(']') + 1)];
            var logMessage = log[(log.IndexOf(']')+1)..];
            LogGrid.Rows.Add(date, level, logMessage);
        }
    }

    private void SearchTextBox_TextChanged(object sender, EventArgs e)
    {
        ShowAll();
        if (SearchTextBox.Text == "") return;
        HideByColumn(SearchComboBox.SelectedIndex);
    }

    private void ShowAll()
    {
        foreach (DataGridViewRow row in LogGrid.Rows)
        {
            row.Visible = true;
        }
    }

    private void HideByColumn(int columnNum)
    {
        foreach (DataGridViewRow row in LogGrid.Rows)
        {
            if (!row.Cells[columnNum].Value.ToString().ToLower().Contains(SearchTextBox.Text.ToLower()))
                row.Visible = false;
        }
    }
}

