using ContentAnalyzer.Frontend.Desktop.BusinessLogicLayer;
using ContentAnalyzer.Frontend.Desktop.Models;

namespace ContentAnalyzer.Frontend.Desktop.Forms;

public partial class MainWindow : Form
{
    private readonly ProcessedCommentsForm _selectedCommentsForm;
    private readonly ConfigureServiceForm _configureServiceForm;
    private readonly LogForm _logForm;

    private readonly BackendClientFactory _backendClientFactory;

    public MainWindow()
    {
        InitializeComponent();
        _backendClientFactory = new BackendClientFactory();

        _configureServiceForm = new ConfigureServiceForm(_backendClientFactory);
        _logForm = new LogForm(this, _backendClientFactory);
        _selectedCommentsForm = new ProcessedCommentsForm(_backendClientFactory);

        _selectedCommentsForm.TopLevel = false;
        _configureServiceForm.TopLevel = false;
        _logForm.TopLevel = false;

        CentralPanel.Controls.Add(_selectedCommentsForm);
        CentralPanel.Controls.Add(_configureServiceForm);
        CentralPanel.Controls.Add(_logForm);
    }

    private void MainWindow_Load(object sender, EventArgs e)
    {
        _selectedCommentsForm.Show();
        _logForm.Hide();
        _configureServiceForm.Hide();
    }

    private async void StartDataCollectionServiceButton_Click(object sender, EventArgs e)
    {
        using var backendClient = _backendClientFactory.GetClient();
        if (backendClient is null)
        {
            MessageBox.Show(@"There is no connection to the services", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
        try
        {
            await backendClient.StartDataCollectionServiceAsync();
            await backendClient.StartDataAnalysisServiceAsync();
        }
        catch (Exception exception)
        {
            MessageBox.Show($"There is no connection to the services\r\n\n{exception.Message}", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        ServicesUpChangeControls();
    }

    public void SetBackendState(BackendState state)
    {
        ErrorsCountLabel.Text = state.ErrorsCount.ToString();
        WarningsCountLabel.Text = state.WarningsCount.ToString();
        ProcessedCommentsLabel.Text = state.ProcessedComments.ToString();
        CollectorServiceStateLabel.Text = state.CollectionServiceState.ToString();
        AnalysisServiceStateLabel.Text = state.AnalysisServiceState.ToString();
        if (state.CollectionServiceState == ServiceState.Up ||
            state.AnalysisServiceState == ServiceState.Up)
            ServicesUpChangeControls();
        else
            ServicesDownChangeControls();
    }

    private void ServicesUpChangeControls()
    {
        StartServiceButton.Hide();
        StopServiceButton.Show();
        StatePanel.BackColor = Color.Chartreuse;
        StateLabel.Text = @"В работе";
        StateLabel.BackColor = Color.Chartreuse;
        CollectorServiceStateLabel.Text = @"Работает";
        AnalysisServiceStateLabel.Text = @"Работает";
    }

    private void ServicesDownChangeControls()
    {
        StartServiceButton.Show();
        StopServiceButton.Hide();
        StatePanel.BackColor = Color.Red;
        StateLabel.Text = @"Не в работе";
        StateLabel.BackColor = Color.Red;
        CollectorServiceStateLabel.Text = @"Не работает";
        AnalysisServiceStateLabel.Text = @"Не работает";
    }

    private async void StopDataCollectionServiceButton_Click(object sender, EventArgs e)
    {
        using var backendClient = _backendClientFactory.GetClient();
        if (backendClient is null)
        {
            MessageBox.Show(@"There is no connection to the services", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
        try
        {
            await backendClient.StopDataCollectionServiceAsync();
            await backendClient.StopDataAnalysisServiceAsync();
        }
        catch (Exception exception)
        {
            MessageBox.Show($"There is no connection to the services\r\n{exception.Message}", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        ServicesDownChangeControls();
    }

    private void ShowSelectedComments_Click(object sender, EventArgs e)
    {
        _selectedCommentsForm.Show();
        _logForm.Hide();
        _configureServiceForm.Hide();
    }

    private void ConfigureServiceButton_Click(object sender, EventArgs e)
    {
        _selectedCommentsForm.Hide();
        _logForm.Hide();
        _configureServiceForm.Show();
    }

    private void ViewLogsButton_Click(object sender, EventArgs e)
    {
        _selectedCommentsForm.Hide();
        _logForm.Show();
        _configureServiceForm.Hide();
    }

    #region ControlPanel

    private void CloseButton_Click(object sender, EventArgs e)
    {
        Application.Exit();
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

    #endregion
}