using System.ComponentModel;
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
    private readonly Client _services;

    public MainWindow()
    {
        _services = new Client(_configureServiceForm.DataAnalysisServiceHost.Text, _configureServiceForm.DataCollectionServiceHost.Text);
        _logsForm = new LogsForm(this);
        _allCommentsForm = new AllCommentsForm(this, _services);
        _selectedCommentsForm = new SelectedCommentsForm(this, _services);
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

    }

    private void TimerOnTick(object? sender, EventArgs e)
    {
        UptimeLabel.Text = _stopwatch.Elapsed.ToString(@"dd\.hh\:mm\:ss");
    }

    private async void StartDataCollectionServiceButton_Click(object sender, EventArgs e)
    {
        await Task.Run(()=>
        {
            _services.StartDataCollectionService();
            _services.StartDataAnalysisService();
            _services.StartAllAnalyzeModels();
        });
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

    private async void StopDataCollectionServiceButton_Click(object sender, EventArgs e)
    {
        _allCommentsForm.StopDisplayData();
        _selectedCommentsForm.StopDisplayData();
        await Task.Run(StopService);
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
        _services.Dispose();
    }

    private void StopService()
    {
        _services.StopDataCollectionService();
        _services.StopAllModels();
        _services.StopDataAnalysisService();
    }

    private void MainWindow_Load(object sender, EventArgs e)
    {
        _allCommentsForm.Hide();
        _selectedCommentsForm.Hide();
        _logsForm.Show();
        _configureServiceForm.Hide();
    }

    private void ShowAllCommentsButton_Click(object sender, EventArgs e)
    {
        _allCommentsForm.Show();
        _selectedCommentsForm.Hide();
        _logsForm.Hide();
        _configureServiceForm.Hide();
    }

    private void ShowSelectedComments_Click(object sender, EventArgs e)
    {
        _allCommentsForm.Hide();
        _selectedCommentsForm.Show();
        _logsForm.Hide();
        _configureServiceForm.Hide();
    }

    private void ConfigureServiceButton_Click(object sender, EventArgs e)
    {
        _allCommentsForm.Hide();
        _selectedCommentsForm.Hide();
        _logsForm.Hide();
        _configureServiceForm.Show();
    }

    private void ViewLogsButton_Click(object sender, EventArgs e)
    {
        _allCommentsForm.Hide();
        _selectedCommentsForm.Hide();
        _logsForm.Show();
        _configureServiceForm.Hide();
    }

    private void CloseButton_Click(object sender, EventArgs e)
    {
        if (StateLabel.Text == "Working") StopService();
        
        Application.Exit();
    }

    protected override void OnClosing(CancelEventArgs e)
    {
        _services.StopDataCollectionService();
        _services.StopAllModels();
        _services.StopDataAnalysisService();
    }

    private void MinimizeWindowButton_Click(object sender, EventArgs e)
    {
        WindowState = FormWindowState.Minimized;
    }

    private Point _lastLocation;
    

    private void UpperPanel_MouseMove(object sender, MouseEventArgs e)
    {
        if (e.Button != MouseButtons.Left) return;
        Left += e.X - _lastLocation.X;
        Top += e.Y - _lastLocation.Y;
    }

    private void UpperPanel_MouseDown(object sender, MouseEventArgs e)
    {
        _lastLocation = e.Location;
    }
}