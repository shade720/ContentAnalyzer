using DevTool.Models;
using Configuration = DevTool.Models.Configuration;
using ConfigurationManager = DevTool.Models.ConfigurationManager;

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
            SetVkConfiguration(ConfigurationManager.GetVkConfiguration());

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
            PollAndRefreshAnalysis();
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

        private void ApplyNewCollectionServiceEndpoint_Click_1(object sender, EventArgs e)
        {
            _serviceManager.SetCollectionServiceHost(CollectionServiceEndpoint.Text);
            PollAndRefreshCollection();
        }


        private void ApplyAnalysisServiceEndpoint_Click_1(object sender, EventArgs e)
        {
            _serviceManager.SetAnalysisServiceHost(AnalysisServiceEndpoint.Text);
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
            StartAll.Visible = StartAnalysisService.Visible;
            StopAll.Visible = !StartAnalysisService.Visible;
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
            StartAll.Visible = StartCollectionService.Visible;
            StopAll.Visible = !StartCollectionService.Visible;
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
            StartAll.Visible = StartAnalysisService.Visible;
            StopAll.Visible = !StartAnalysisService.Visible;
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
            StartAll.Visible = StartCollectionService.Visible;
            StopAll.Visible = !StartCollectionService.Visible;
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
            StartAnalysisService.Visible = false;
            StartCollectionService.Visible = false;
            StopAnalysisService.Visible = true;
            StopCollectionService.Visible = true;
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
            StartAnalysisService.Visible = true;
            StartCollectionService.Visible = true;
            StopAnalysisService.Visible = false;
            StopCollectionService.Visible = false;
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
            CollectionServiceHosts.Items.AddRange(configuration.CollectionServiceEndpoints.ToArray());
            AnalysisServiceHosts.Items.AddRange(configuration.AnalysisServiceEndpoints.ToArray());
            CollectionServiceEndpoint.Items.AddRange(configuration.CollectionServiceEndpoints.ToArray());
            AnalysisServiceEndpoint.Items.AddRange(configuration.AnalysisServiceEndpoints.ToArray());
            CollectionServiceEndpoint.Text = configuration.CurrentCollectionServiceEndpoint;
            AnalysisServiceEndpoint.Text = configuration.CurrentAnalysisServiceEndpoint;
            ScanCommentsDelay.Text = configuration.ScanCommentsDelay.ToString();
            ScanPostDelay.Text = configuration.ScanPostDelay.ToString();
            PostQueueSize.Text = configuration.PostQueueSize.ToString();
            EvaluateThreshold.Text = configuration.EvaluateThreshold.ToString();
            ObserveDelay.Text = configuration.ObserveDelay.ToString();
        }

        private void SaveConfiguration_Click(object sender, EventArgs e)
        {
            var currentConfig = new Configuration
            {
                CollectionServiceEndpoints = new List<string>(CollectionServiceHosts.Items.OfType<string>()),
                AnalysisServiceEndpoints = new List<string>(AnalysisServiceHosts.Items.OfType<string>()),
                CurrentCollectionServiceEndpoint = CollectionServiceEndpoint.Text,
                CurrentAnalysisServiceEndpoint = AnalysisServiceEndpoint.Text,
                ScanCommentsDelay = int.Parse(ScanCommentsDelay.Text),
                ScanPostDelay = int.Parse(ScanPostDelay.Text),
                PostQueueSize = int.Parse(PostQueueSize.Text),
                ObserveDelay = int.Parse(ObserveDelay.Text),
                EvaluateThreshold = int.Parse(EvaluateThreshold.Text)
            };
            ConfigurationManager.SaveConfiguration(currentConfig);
        }
        private void AddCollectionServiceHost_Click(object sender, EventArgs e)
        {
            CollectionServiceHosts.Items.Add(NewCollectionHost.Text);
            NewCommunity.Text = string.Empty;
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
            _serviceManager.LoadConfiguration(File.ReadAllText("configuration.json"));
        }

        private void SetLocalConfig_Click(object sender, EventArgs e)
        {
            SetConfiguration(ConfigurationManager.GetConfiguration());
        }

        #endregion

        #region VkCollector

        private void AddCommunity_Click(object sender, EventArgs e)
        {
            Communities.Items.Add(NewCommunity.Text);
            NewCommunity.Text = string.Empty;
        }

        private void DeleteCommunity_Click(object sender, EventArgs e)
        {
            if (Communities.SelectedIndex == -1) return;
            Communities.Items.RemoveAt(Communities.SelectedIndex);
        }

        private void SaveVkSettings_Click(object sender, EventArgs e)
        {
            var vkConfiguration = new VkConfiguration
            {
                ApplicationId = int.Parse(ApplicationId.Text),
                SecureKey = SecureKey.Text,
                ServiceAccessKey = ServiceAccessKey.Text,
                Communities = new List<int>(Communities.Items.OfType<string>().Select(int.Parse))
            };
            ConfigurationManager.SaveVkConfiguration(vkConfiguration);
        }

        private void LoadVkSettings_Click(object sender, EventArgs e)
        {
            _serviceManager.LoadConfiguration(File.ReadAllText("vkSettings.json"));
        }

        private void LocalConfiguration_Click(object sender, EventArgs e)
        {
            var configuration = ConfigurationManager.GetVkConfiguration();
            if (configuration is null) return;
            SetVkConfiguration(configuration);
        }

        private void SetVkConfiguration(VkConfiguration configuration)
        {
            ServiceAccessKey.Text = configuration.ServiceAccessKey;
            SecureKey.Text = configuration.SecureKey;
            ApplicationId.Text = configuration.ApplicationId.ToString();
            Communities.Items.Clear();
            foreach (var community in configuration.Communities)
            {
                Communities.Items.Add(community.ToString());
            }
        }

        #endregion

        #region Report

        private void OpenReport_Click(object sender, EventArgs e)
        {
            if (SaveFileDialog.ShowDialog() != DialogResult.OK) return;

            var fromDate = DateTime.UnixEpoch;
            var toDate = DateTime.UnixEpoch;
            if (TodayCheckBox.Checked)
                fromDate = DateTime.Today;
            if (Last3DaysCheckBox.Checked)
                fromDate = DateTime.Today.AddDays(-3);
            if (LastWeekCheckBox.Checked)
                fromDate = DateTime.Today.AddDays(-7);
            if (LastMonthCheckBox.Checked)
                fromDate = DateTime.Today.AddMonths(-1);
            if (SelectedDateCheckBox.Checked)
            {
                fromDate = DateTime.Parse(FromDate.Text);
                toDate = DateTime.Parse(ToDate.Text);
                if (fromDate.Ticks >= toDate.Ticks)
                    MessageBox.Show(@"'To' date must be more than 'From' date");
            }

            var filter = new Common.CommentsQueryFilter
            {
                AuthorId = !string.IsNullOrEmpty(AuthorId.Text) ? long.Parse(AuthorId.Text) : 0,
                PostId = !string.IsNullOrEmpty(PostId.Text) ? long.Parse(PostId.Text) : 0,
                GroupId = !string.IsNullOrEmpty(CommunityId.Text) ? long.Parse(CommunityId.Text) : 0,
                FromDate = fromDate,
                ToDate = toDate
            };
            try
            {
                Reporter.OpenReport(SaveFileDialog.FileName, _serviceManager.GetEvaluatedComments(filter));
                MessageBox.Show(@"The report was created successfully");
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Error: \r\n{exception.Message}\r\n{exception.StackTrace}");
            }
        }

        #region CheckBoxHandlers

        private void TodayCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            NonFiringState(() =>
            {
                TodayCheckBox.Checked = true;
                Last3DaysCheckBox.Checked = false;
                LastWeekCheckBox.Checked = false;
                LastMonthCheckBox.Checked = false;
                SelectedDateCheckBox.Checked = false;
                FromDate.Enabled = false;
                ToDate.Enabled = false;
            });
        }

        private void Last3DaysCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            NonFiringState(() =>
            {
                TodayCheckBox.Checked = false;
                Last3DaysCheckBox.Checked = true;
                LastWeekCheckBox.Checked = false;
                LastMonthCheckBox.Checked = false;
                SelectedDateCheckBox.Checked = false;
                FromDate.Enabled = false;
                ToDate.Enabled = false;
            });
        }

        private void LastWeekCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            NonFiringState(() =>
            {
                TodayCheckBox.Checked = false;
                Last3DaysCheckBox.Checked = false;
                LastWeekCheckBox.Checked = true;
                LastMonthCheckBox.Checked = false;
                SelectedDateCheckBox.Checked = false;
                FromDate.Enabled = false;
                ToDate.Enabled = false;
            });
        }

        private void LastMonthCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            NonFiringState(() =>
            {
                TodayCheckBox.Checked = false;
                Last3DaysCheckBox.Checked = false;
                LastWeekCheckBox.Checked = false;
                LastMonthCheckBox.Checked = true;
                SelectedDateCheckBox.Checked = false;
                FromDate.Enabled = false;
                ToDate.Enabled = false;
            });
        }

        private void SelectedDateCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            NonFiringState(() =>
            {
                TodayCheckBox.Checked = false;
                Last3DaysCheckBox.Checked = false;
                LastWeekCheckBox.Checked = false;
                LastMonthCheckBox.Checked = false;
                SelectedDateCheckBox.Checked = true;
                FromDate.Enabled = true;
                ToDate.Enabled = true;
            });
        }

        #endregion

        private void NonFiringState(Action action)
        {
            TodayCheckBox.CheckedChanged -= TodayCheckBox_CheckedChanged!;
            Last3DaysCheckBox.CheckedChanged -= Last3DaysCheckBox_CheckedChanged!;
            LastWeekCheckBox.CheckedChanged -= LastWeekCheckBox_CheckedChanged!;
            LastMonthCheckBox.CheckedChanged -= LastMonthCheckBox_CheckedChanged!;
            SelectedDateCheckBox.CheckedChanged -= SelectedDateCheckBox_CheckedChanged!;
            action();
            TodayCheckBox.CheckedChanged += TodayCheckBox_CheckedChanged!;
            Last3DaysCheckBox.CheckedChanged += Last3DaysCheckBox_CheckedChanged!;
            LastWeekCheckBox.CheckedChanged += LastWeekCheckBox_CheckedChanged!;
            LastMonthCheckBox.CheckedChanged += LastMonthCheckBox_CheckedChanged!;
            SelectedDateCheckBox.CheckedChanged += SelectedDateCheckBox_CheckedChanged!;
        }

        #endregion
    }
}