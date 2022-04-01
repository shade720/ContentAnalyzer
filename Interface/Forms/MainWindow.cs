using System.Diagnostics;
using Timer = System.Windows.Forms.Timer;

namespace Interface.Forms;

public partial class MainWindow : Form
{
    private readonly AllCommentsForm _allCommentsForm;
    private readonly SelectedCommentsForm _selectedCommentsForm;
    private readonly ConfigureServiceForm _configureServiceForm = new();
    private readonly LogsForm _logsForm;
    private readonly Timer _timer = new();
    private readonly Stopwatch _stopwatch = new();

    public MainWindow()
    {
        _logsForm = new LogsForm(this);
        _allCommentsForm = new AllCommentsForm(this);
        _selectedCommentsForm = new SelectedCommentsForm(this);
        InitializeComponent();
        _allCommentsForm.TopLevel = false;
        _selectedCommentsForm.TopLevel = false;
        _configureServiceForm.TopLevel = false;
        _logsForm.TopLevel = false;
        CentralPanel.Controls.Add(_allCommentsForm);
        CentralPanel.Controls.Add(_selectedCommentsForm);
        CentralPanel.Controls.Add(_configureServiceForm);
        CentralPanel.Controls.Add(_logsForm);
        _timer.Interval = 1000;
        _timer.Tick += TimerOnTick;

        DataCollectionService.Startup.ConfigureService();
        DataAnalysisService.Startup.ConfigureService();
    }

    private void TimerOnTick(object? sender, EventArgs e)
    {
        UptimeLabel.Text = _stopwatch.Elapsed.ToString(@"dd\.hh\:mm\:ss");
    }

    private void StartDataCollectionServiceButton_Click(object sender, EventArgs e)
    {
        DataCollectionService.DataCollectionService.Start();
        DataAnalysisService.DataAnalysisService.StartService();
        DataAnalysisService.DataAnalysisService.StartAll();

        StartServiceButton.Hide();
        StopServiceButton.Show();
        StatePanel.BackColor = Color.Chartreuse;
        StateLabel.Text = @"Working";
        StateLabel.BackColor = Color.Chartreuse;
        CollectorServiceStateLabel.Text = "Up";
        AnalysisServiceStateLabel.Text = "Up";
        _timer.Start();
        _stopwatch.Start();
        _allCommentsForm.DisplayActualData();
        _selectedCommentsForm.DisplayActualData();
    }

    private void StopDataCollectionServiceButton_Click(object sender, EventArgs e)
    {
        _allCommentsForm.StopDisplayData();
        _selectedCommentsForm.StopDisplayData();
        DataCollectionService.DataCollectionService.Stop();
        DataAnalysisService.DataAnalysisService.StopAll();
        DataAnalysisService.DataAnalysisService.StopService();

        StartServiceButton.Show();
        StopServiceButton.Hide();
        StatePanel.BackColor = Color.Red;
        StateLabel.Text = @"Not working";
        StateLabel.BackColor = Color.Red;
        CollectorServiceStateLabel.Text = "Down";
        AnalysisServiceStateLabel.Text = "Down";
        _timer.Stop();
        _stopwatch.Stop();
        _stopwatch.Reset();
    }

    private void MainWindow_Load(object sender, EventArgs e)
    {
        _allCommentsForm.Hide();
        _selectedCommentsForm.Hide();
        _logsForm.Show();
    }

    private void ShowAllCommentsButton_Click(object sender, EventArgs e)
    {
        _allCommentsForm.Show();
        _selectedCommentsForm.Hide();
        _logsForm.Hide();
    }

    private void ShowSelectedComments_Click(object sender, EventArgs e)
    {
        _allCommentsForm.Hide();
        _selectedCommentsForm.Show();
        _logsForm.Hide();
    }

    private void ConfigureServiceButton_Click(object sender, EventArgs e)
    {

    }

    private void ViewLogsButton_Click(object sender, EventArgs e)
    {
        _allCommentsForm.Hide();
        _selectedCommentsForm.Hide();
        _logsForm.Show();
    }
}