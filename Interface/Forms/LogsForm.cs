using Common;

namespace Interface.Forms;

public partial class LogsForm : Form
{
    private readonly MainWindow _parent;
    private readonly Client _client;
    private LastUploading _lastUploading;

    public LogsForm(MainWindow parent, Client client)
    {
        _parent = parent;
        _client = client;
        InitializeComponent();
    }

    private void GetAnalysisLogs_Click(object sender, EventArgs e)
    {
        LogWindowTextbox.Text = _client.GetDataAnalysisServiceLogs(DateTime.Parse(LogDateComboBox.Text));
        _lastUploading = LastUploading.DataAnalysisServiceLogUploading;
    }

    private void GetCollectionLogs_Click(object sender, EventArgs e)
    {
        LogWindowTextbox.Text = _client.GetDataCollectionServiceLogs(DateTime.Parse(LogDateComboBox.Text));
        _lastUploading = LastUploading.DataCollectionServiceLogUploading;
    }

    private void RefreshButton_Click(object sender, EventArgs e)
    {
        LogWindowTextbox.Text = _lastUploading == LastUploading.DataCollectionServiceLogUploading ? 
            _client.GetDataCollectionServiceLogs(DateTime.Parse(LogDateComboBox.Text)) :
            _client.GetDataAnalysisServiceLogs(DateTime.Parse(LogDateComboBox.Text));
    }
    private enum LastUploading
    {
        DataCollectionServiceLogUploading,
        DataAnalysisServiceLogUploading
    }
}

