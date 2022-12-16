using DevTool.Models;

namespace DevTool.Forms
{
    public partial class MainForm : Form
    {
        private readonly ServiceManager _serviceManager;
        public MainForm(ServiceManager serviceManager)
        {
            InitializeComponent();
            _serviceManager = serviceManager;

            SetConfiguration(ConfigurationManager.GetConfiguration());

            _serviceManager.Subscribe(RefreshCollectionInfo, RefreshAnalysisInfo);
            _serviceManager.SetCollectionServiceHost(CollectionServiceEndpoint.Text);
            _serviceManager.SetAnalysisServiceHost(AnalysisServiceEndpoint.Text);

            PollAndRefreshCollection();
            PollAndRefreshAnalysis();
        }

        #region MainTab

        #region Info

        private void RefreshCollectionServiceInfo_Click(object sender, EventArgs e)
        {
            PollAndRefreshCollection();
        }

        private void RefreshAnalysisServiceInfo_Click(object sender, EventArgs e)
        {
            PollAndRefreshCollection();
        }

        private void PollAndRefreshCollection()
        {
            var collectionInfo = _serviceManager.PollCollectionService();
            RefreshCollectionInfo(collectionInfo);
        }
        private void PollAndRefreshAnalysis()
        {
            var analysisInfo = _serviceManager.PollAnalysisService();
            RefreshAnalysisInfo(analysisInfo);
        }

        private void RefreshCollectionInfo(ServiceInfo serviceInfo)
        {
            CollectionConnection.Text = serviceInfo.ConnectionState.ToString();
            CollectionState.Text = serviceInfo.State.ToString();
            CollectionUptime.Text = serviceInfo.Uptime.ToString();
            CollectionErrors.Text = serviceInfo.ErrorsCount.ToString();
            CollectionWarnings.Text = serviceInfo.WarningsCount.ToString();
            CollectionCollected.Text = serviceInfo.CollectedCommentsCount.ToString();
        }

        private void RefreshAnalysisInfo(ServiceInfo serviceInfo)
        {
            AnalysisConnection.Text = serviceInfo.ConnectionState.ToString();
            AnalysisState.Text = serviceInfo.State.ToString();
            AnalysisUptime.Text = serviceInfo.Uptime.ToString();
            AnalysisErrors.Text = serviceInfo.ErrorsCount.ToString();
            AnalysisWarnings.Text = serviceInfo.WarningsCount.ToString();
            AnalysisEvaluated.Text = serviceInfo.EvaluatedCommentsCount.ToString();
        }

        private void ViewCollectionServiceLogs_Click(object sender, EventArgs e)
        {
            new LogViewerWindow(_serviceManager.ViewCollectionLog(DateTime.Parse(CollectionLogDate.Text))).Show();
        }

        private void ViewAnalysisServiceLogs_Click(object sender, EventArgs e)
        {
            new LogViewerWindow(_serviceManager.ViewAnalysisLog(DateTime.Parse(AnalysisLogDate.Text))).Show();
        }

        private void ApplyNewCollectionServiceEndpoint_Click(object sender, EventArgs e)
        {
            _serviceManager.SetCollectionServiceHost(CollectionServiceEndpoint.Text);
            PollAndRefreshCollection();
        }

        private void ApplyAnalysisServiceEndpoint_Click(object sender, EventArgs e)
        {
            _serviceManager.SetCollectionServiceHost(AnalysisServiceEndpoint.Text);
            PollAndRefreshAnalysis();
        }

        #endregion

        #region Control

        private void StartCollectionService_Click(object sender, EventArgs e)
        {
            try
            {
                _serviceManager.StartDataCollectionService();
            }
            catch
            {
                MessageBox.Show(@"Connection error");
                return;
            }
            StartCollectionService.Visible = false;
            StopCollectionService.Visible = true;
        }

        private void StartAnalysisService_Click(object sender, EventArgs e)
        {
            try
            {
                _serviceManager.StartDataAnalysisService();
            }
            catch
            {
                MessageBox.Show(@"Connection error");
                return;
            }
            StartAnalysisService.Visible = false;
            StopAnalysisService.Visible = true;
        }

        private void StopCollectionService_Click(object sender, EventArgs e)
        {
            try
            {
                _serviceManager.StopDataCollectionService();
                PollAndRefreshCollection();
            }
            catch
            {
                MessageBox.Show(@"Connection error");
                return;
            }
            StartCollectionService.Visible = true;
            StopCollectionService.Visible = false;
        }

        private void StopAnalysisService_Click(object sender, EventArgs e)
        {
            try
            {
                _serviceManager.StopDataAnalysisService();
                PollAndRefreshAnalysis();
            }
            catch
            {
                MessageBox.Show(@"Connection error");
                return;
            }
            StartAnalysisService.Visible = true;
            StopAnalysisService.Visible = false;
        }

        private void ClearCommentsDatabase_Click(object sender, EventArgs e)
        {

        }

        private void ClearEvaluatedDatabase_Click(object sender, EventArgs e)
        {

        }

        private void StartAll_Click(object sender, EventArgs e)
        {
            try
            {
                _serviceManager.StartDataAnalysisService();
                _serviceManager.StartDataCollectionService();
            }
            catch
            {
                MessageBox.Show(@"Connection error");
                return;
            }
            StartAll.Visible = false;
            StopAll.Visible = true;
        }

        private void StopAll_Click(object sender, EventArgs e)
        {
            try
            {
                _serviceManager.StopDataCollectionService();
                _serviceManager.StopDataAnalysisService();
                PollAndRefreshCollection();
                PollAndRefreshAnalysis();
            }
            catch
            {
                MessageBox.Show(@"Connection error");
                return;
            }
            StartAll.Visible = true;
            StopAll.Visible = false;
        }

        #endregion

        #endregion

        #region ConfigurationTab

        private void SetConfiguration(Configuration configuration)
        {
            CollectionServiceEndpoint.Items.Clear();
            AnalysisServiceEndpoint.Items.Clear();
            CollectionServiceHosts.Items.Clear();
            AnalysisServiceHosts.Items.Clear();
            CollectionServiceHosts.Items.AddRange(configuration.CollectionServiceEndpoint.ToArray());
            AnalysisServiceHosts.Items.AddRange(configuration.AnalysisServiceEndpoints.ToArray());
            CollectionServiceEndpoint.Items.AddRange(configuration.CollectionServiceEndpoint.ToArray());
            AnalysisServiceEndpoint.Items.AddRange(configuration.AnalysisServiceEndpoints.ToArray());
            CollectionServiceEndpoint.Text = configuration.CurrentCollectionServiceEndpoint;
            AnalysisServiceEndpoint.Text = configuration.CurrentAnalysisServiceEndpoint;
            ConnectionString.Text = configuration.ConnectionString;
            ScanCommentsDelay.Text = configuration.ScanCommentsDelay.ToString();
            ScanPostDelay.Text = configuration.ScanPostDelay.ToString();
            PostQueueSize.Text = configuration.PostQueueSize.ToString();
        }

        private void SaveConfiguration_Click(object sender, EventArgs e)
        {
            var currentConfig = new Configuration
            {
                CollectionServiceEndpoint = new List<string>(CollectionServiceHosts.Items.OfType<string>()),
                AnalysisServiceEndpoints = new List<string>(AnalysisServiceHosts.Items.OfType<string>()),
                CurrentCollectionServiceEndpoint = CollectionServiceEndpoint.Text,
                CurrentAnalysisServiceEndpoint = AnalysisServiceEndpoint.Text,
                ConnectionString = ConnectionString.Text,
                ScanCommentsDelay = int.Parse(ScanCommentsDelay.Text),
                ScanPostDelay = int.Parse(ScanPostDelay.Text),
                PostQueueSize = int.Parse(PostQueueSize.Text)
            };
            ConfigurationManager.SaveConfiguration(currentConfig);
        }
        private void AddCollectionServiceHost_Click(object sender, EventArgs e)
        {
            CollectionServiceHosts.Items.Add(NewCollectionHost.Text);
        }

        private void DeleteCollectionServiceHost_Click(object sender, EventArgs e)
        {
            CollectionServiceHosts.Items.RemoveAt(CollectionServiceHosts.SelectedIndex);
        }

        private void AddAnalysisServiceHost_Click(object sender, EventArgs e)
        {
            AnalysisServiceHosts.Items.Add(NewAnalysisHost.Text);
        }

        private void DeleteAnalysisServiceHost_Click(object sender, EventArgs e)
        {
            AnalysisServiceHosts.Items.RemoveAt(AnalysisServiceHosts.SelectedIndex);
        }
        private void LoadConfiguration_Click(object sender, EventArgs e)
        {
            var file = File.ReadAllText("appsettings.json");
            _serviceManager.LoadConfiguration(file);
        }

        #endregion
    }
}