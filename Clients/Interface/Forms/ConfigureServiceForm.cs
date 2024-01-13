using ContentAnalyzer.Frontend.Desktop.BusinessLogicLayer;
using ContentAnalyzer.Frontend.Desktop.Models;

namespace ContentAnalyzer.Frontend.Desktop.Forms;

public partial class ConfigureServiceForm : Form
{
    private readonly BackendClientFactory _backendClientFactory;

    public ConfigureServiceForm(BackendClientFactory backendClientFactory)
    {
        _backendClientFactory = backendClientFactory;
        InitializeComponent();
    }

    private void ConfigureAnalysisService_Load(object sender, EventArgs e)
    {
        var appConfig = ConfigurationManager.GetAppConfiguration();

        if (appConfig is null)
        {
            MessageBox.Show(@"There is no app configuration, please configure app again", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        SetConfiguration(appConfig);
    }

    private void SetConfiguration(AppConfiguration appConfiguration)
    {
        CurrentBackendHost.Items.Clear();
        BackendHosts.Items.Clear();
        CurrentBackendHost.Items.AddRange(appConfiguration.BackendHosts.ToArray());
        BackendHosts.Items.AddRange(appConfiguration.BackendHosts.ToArray());
        CurrentBackendHost.Text = appConfiguration.CurrentBackendHost;

        LoginTextBox.Text = appConfiguration.BackendLogin;
        TokenTextBox.Text = appConfiguration.BackendToken;
        ScanCommentsDelay.Text = appConfiguration.ScanCommentsDelay.ToString();
        ScanPostDelay.Text = appConfiguration.ScanPostDelay.ToString();
        PostQueueSize.Text = appConfiguration.PostQueueSize.ToString();
        ObserveDelay.Text = appConfiguration.ObserveDelay.ToString();
    }

    private void AddBackendHost_Click(object sender, EventArgs e)
    {
        BackendHosts.Items.Add(NewBackendHost.Text);
        CurrentBackendHost.Items.Add(NewBackendHost.Text);
        NewBackendHost.Text = string.Empty;
    }

    private void DeleteBackendHost_Click(object sender, EventArgs e)
    {
        if (BackendHosts.SelectedIndex < 0)
            return;
        BackendHosts.Items.RemoveAt(BackendHosts.SelectedIndex);
        CurrentBackendHost.Items.Clear();
        CurrentBackendHost.Items.AddRange(BackendHosts.Items.OfType<string>().ToArray());
    }

    private void SaveCollectionServiceConfiguration_Click(object sender, EventArgs e)
    {
        var currentConfig = new CollectionServiceConfiguration
        {
            ScanCommentsDelay = int.Parse(ScanCommentsDelay.Text),
            ScanPostDelay = int.Parse(ScanPostDelay.Text),
            PostQueueSize = int.Parse(PostQueueSize.Text)
        };
        ConfigurationManager.SaveCollectionConfiguration(currentConfig);
        MessageBox.Show(@"The configuration has been saved successfully", @"Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private async void LoadCollectionServiceConfiguration_Click(object sender, EventArgs e)
    {
        var configuration = ConfigurationManager.GetCollectionServiceConfiguration();
        if (configuration is null)
        {
            MessageBox.Show(@"The collection service configuration is not found", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        using var backendClient = _backendClientFactory.GetClient();
        if (backendClient is null)
        {
            MessageBox.Show(@"There is no connection to the services", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
        try
        {
            await backendClient.LoadCollectionServiceConfiguration(configuration);
            MessageBox.Show(@"The configuration has been loaded successfully", @"Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception exception)
        {
            MessageBox.Show($"There is no connection to the services\r\n\n{exception.Message}", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void SaveAnalysisServiceConfiguration_Click(object sender, EventArgs e)
    {
        var currentConfig = new AnalysisServiceConfiguration
        {
            ObserveDelay = int.Parse(ObserveDelay.Text),
        };
        ConfigurationManager.SaveAnalysisConfiguration(currentConfig);
        MessageBox.Show(@"The configuration has been saved successfully", @"Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private async void LoadAnalysisServiceConfiguration_Click(object sender, EventArgs e)
    {
        var configuration = ConfigurationManager.GetAnalysisServiceConfiguration();
        if (configuration is null)
        {
            MessageBox.Show(@"The analysis service configuration is not found", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        using var backendClient = _backendClientFactory.GetClient();
        if (backendClient is null)
        {
            MessageBox.Show(@"There is no connection to the services", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
        try
        {
            await backendClient.LoadAnalysisServiceConfiguration(configuration);
            MessageBox.Show(@"The configuration has been loaded successfully", @"Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception exception)
        {
            MessageBox.Show($"There is no connection to the services\r\n\n{exception.Message}", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void SaveAppConfigButton_Click(object sender, EventArgs e)
    {
        var currentAppConfig = new AppConfiguration
        {
            BackendHosts = new List<string>(BackendHosts.Items.OfType<string>()),
            CurrentBackendHost = CurrentBackendHost.Text,
            BackendLogin = LoginTextBox.Text,
            BackendToken = TokenTextBox.Text,
            ScanCommentsDelay = int.Parse(string.IsNullOrEmpty(ScanCommentsDelay.Text) ? "0" : ScanCommentsDelay.Text),
            ScanPostDelay = int.Parse(string.IsNullOrEmpty(ScanPostDelay.Text) ? "0" : ScanPostDelay.Text),
            PostQueueSize = int.Parse(string.IsNullOrEmpty(PostQueueSize.Text) ? "0" : PostQueueSize.Text),
            ObserveDelay = int.Parse(string.IsNullOrEmpty(ObserveDelay.Text) ? "0" : ObserveDelay.Text),
        };
        ConfigurationManager.SaveAppConfiguration(currentAppConfig);
        MessageBox.Show(@"The configuration has been saved successfully", @"Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
}