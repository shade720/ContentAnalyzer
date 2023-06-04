using Common.EntityFramework;
using DevTool.Models;
using Configuration = DevTool.Models.Configuration;
using ConfigurationManager = DevTool.Models.ConfigurationManager;

namespace DevTool.Forms;

internal partial class MainForm : Form
{
    private ServiceClient<Comment> _collectionServiceClient;
    private ServiceClient<EvaluatedComment> _analysisServiceClient;

    public MainForm()
    {
        InitializeComponent();

        var settings = ConfigurationManager.GetConfiguration();
        if (settings != null)
            SetConfiguration(settings);
        var vkConfig = ConfigurationManager.GetVkConfiguration();
        if (vkConfig != null)
            SetVkConfiguration(vkConfig);

        _analysisServiceClient = new AnalysisServiceClient(AnalysisServiceEndpoint.Text);
        _collectionServiceClient = new CollectionServiceClient(CollectionServiceEndpoint.Text);
        _analysisServiceClient.OnServiceInfoProgress = new Progress<ServiceInfo>(RefreshAnalysisInfo);
        _collectionServiceClient.OnServiceInfoProgress = new Progress<ServiceInfo>(RefreshCollectionInfo);

        _collectionServiceClient.OnProgressBarStep = new Progress<bool>(CollectionProgressBarStep);
        _analysisServiceClient.OnProgressBarStep = new Progress<bool>(AnalysisProgressBarStep);

        AnalysisServiceProgressBar.Value = 0;
        CollectionServiceProgressBar.Value = 0;

        _analysisServiceClient.StartPolling();
        _collectionServiceClient.StartPolling();
    }

    #region MainTab

    #region Info

    private void CollectionProgressBarStep(bool isNeedToReset)
    {
        CollectionServiceProgressBar.PerformStep();
        if (isNeedToReset) CollectionServiceProgressBar.Value = 0;
    }
    private void AnalysisProgressBarStep(bool isNeedToReset)
    {
        AnalysisServiceProgressBar.PerformStep();
        if (isNeedToReset) AnalysisServiceProgressBar.Value = 0;
    }

    private void RefreshCollectionServiceInfo_Click(object sender, EventArgs e)
    {
        _collectionServiceClient.Poll();
    }

    private void RefreshAnalysisServiceInfo_Click(object sender, EventArgs e)
    {
        _analysisServiceClient.Poll();
    }

    private void RefreshCollectionInfo(ServiceInfo serviceInfo)
    {
        CollectionConnection.Text = serviceInfo.ConnectionState.ToString();
        CollectionState.Text = serviceInfo.State.ToString();
        CollectionUptime.Text = serviceInfo.Uptime.ToString();
        CollectionErrors.Text = serviceInfo.ErrorsCount.ToString();
        CollectionWarnings.Text = serviceInfo.WarningsCount.ToString();
        CollectionCollected.Text = serviceInfo.CollectedCommentsCount.ToString();

        StartCollectionService.Visible = serviceInfo.State != State.Up;
        StartAll.Visible = serviceInfo.State != State.Up;
        StopCollectionService.Visible = serviceInfo.State == State.Up;
        StopAll.Visible = serviceInfo.State == State.Up;

        if (serviceInfo.ConnectionState == ConnectionState.Connected)
        {
            CollectionProgressBarRed.Visible = false;
            CollectionServiceProgressBar.Visible = true;
        }
        else
        {
            CollectionProgressBarRed.Visible = true;
            CollectionServiceProgressBar.Visible = false;
        }
    }

    private void RefreshAnalysisInfo(ServiceInfo serviceInfo)
    {
        AnalysisConnection.Text = serviceInfo.ConnectionState.ToString();
        AnalysisState.Text = serviceInfo.State.ToString();
        AnalysisUptime.Text = serviceInfo.Uptime.ToString();
        AnalysisErrors.Text = serviceInfo.ErrorsCount.ToString();
        AnalysisWarnings.Text = serviceInfo.WarningsCount.ToString();
        AnalysisEvaluated.Text = serviceInfo.EvaluatedCommentsCount.ToString();

        StartAnalysisService.Visible = serviceInfo.State != State.Up;
        StartAll.Visible = serviceInfo.State != State.Up;
        StopAnalysisService.Visible = serviceInfo.State == State.Up;
        StopAll.Visible = serviceInfo.State == State.Up;
        if (serviceInfo.ConnectionState == ConnectionState.Connected)
        {
            AnalysisProgressBarRed.Visible = false;
            AnalysisServiceProgressBar.Visible = true;
        }
        else
        {
            AnalysisProgressBarRed.Visible = true;
            AnalysisServiceProgressBar.Visible = false;
        }
    }

    private void ViewCollectionServiceLogs_Click(object sender, EventArgs e)
    {
        new LogViewerWindow(_collectionServiceClient.GetLog(DateTime.Parse(CollectionLogDate.Text))).Show();
    }

    private void ViewAnalysisServiceLogs_Click(object sender, EventArgs e)
    {
        new LogViewerWindow(_analysisServiceClient.GetLog(DateTime.Parse(AnalysisLogDate.Text))).Show();
    }

    private void ApplyNewCollectionServiceEndpoint_Click_1(object sender, EventArgs e)
    {
        _collectionServiceClient.StopPolling();
        _collectionServiceClient.Dispose();
        _collectionServiceClient = new CollectionServiceClient(CollectionServiceEndpoint.Text);
        _collectionServiceClient.StartPolling();
    }


    private void ApplyAnalysisServiceEndpoint_Click_1(object sender, EventArgs e)
    {
        _analysisServiceClient.StopPolling();
        _analysisServiceClient.Dispose();
        _analysisServiceClient = new AnalysisServiceClient(AnalysisServiceEndpoint.Text);
        _analysisServiceClient.StartPolling();
    }

    #endregion

    #region Control

    private void StartCollectionService_Click(object sender, EventArgs e)
    {
        StartCollectionService.Enabled = false;
        StartAll.Enabled = false;
        Task.Run(() => _collectionServiceClient.StartService()).ContinueWith(result =>
        {
            if (result.IsCompleted)
            {
                StartCollectionService.Visible = false;
                StopCollectionService.Visible = true;
                StartAll.Visible = StartAnalysisService.Visible;
                StopAll.Visible = !StartAnalysisService.Visible;
            }
            else
            {
                MessageBox.Show(@"Connection error");
            }
            StartCollectionService.Enabled = true;
            StartAll.Enabled = true;
        });
    }

    private void StartAnalysisService_Click(object sender, EventArgs e)
    {
        StartAnalysisService.Enabled = false;
        StartAll.Enabled = false;
        Task.Run(() => _analysisServiceClient.StartService()).ContinueWith(result =>
        {
            if (result.IsCompleted)
            {
                StartAnalysisService.Visible = false;
                StopAnalysisService.Visible = true;
                StartAll.Visible = StartCollectionService.Visible;
                StopAll.Visible = !StartCollectionService.Visible;
            }
            else
            {
                MessageBox.Show(@"Connection error");
            }
            StartAnalysisService.Enabled = true;
            StartAll.Enabled = true;
        }, CancellationToken.None, TaskContinuationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());
    }

    private void StopCollectionService_Click(object sender, EventArgs e)
    {
        StopCollectionService.Enabled = false;
        StopAll.Enabled = false;
        Task.Run(() =>
        {
            _collectionServiceClient.StopService();
            _collectionServiceClient.Poll();
        }).ContinueWith(result =>
        {
            if (result.IsCompleted)
            {
                StartCollectionService.Visible = true;
                StopCollectionService.Visible = false;
                StartAll.Visible = StartAnalysisService.Visible;
                StopAll.Visible = !StartAnalysisService.Visible;
            }
            else
            {
                MessageBox.Show(@"Connection error");
            }
            StopCollectionService.Enabled = true;
            StopAll.Enabled = true;
        }, CancellationToken.None, TaskContinuationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());
    }

    private void StopAnalysisService_Click(object sender, EventArgs e)
    {
        StopAnalysisService.Enabled = false;
        StopAll.Enabled = false;
        Task.Run(() =>
        {
            _analysisServiceClient.StopService();
            _analysisServiceClient.Poll();
        }).ContinueWith(result =>
        {
            if (result.IsCompleted)
            {
                StartAnalysisService.Visible = true;
                StopAnalysisService.Visible = false;
                StartAll.Visible = StartCollectionService.Visible;
                StopAll.Visible = !StartCollectionService.Visible;
            }
            else
            {
                MessageBox.Show(@"Connection error");
            }
            StopAnalysisService.Enabled = true;
            StopAll.Enabled = true;
        }, CancellationToken.None, TaskContinuationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());
    }

    private void ClearCommentsDatabase_Click(object sender, EventArgs e)
    {
        if (MessageBox.Show(@"Are you sure you want to clear the comments database?", "", MessageBoxButtons.YesNo) !=
            DialogResult.Yes) return;
        ClearCommentsDatabase.Enabled = false;
        Task.Run(() => _collectionServiceClient.ClearDatabase()).ContinueWith(result =>
        {
            if (result.IsCompleted)
            {
                ClearCommentsDatabase.Enabled = true;
            }
            else
            {
                MessageBox.Show(@"Connection error");
            }
        }, CancellationToken.None, TaskContinuationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());
    }

    private void ClearEvaluatedDatabase_Click(object sender, EventArgs e)
    {
        if (MessageBox.Show(@"Are you sure you want to clear the evaluated comments database?", "", MessageBoxButtons.YesNo) != DialogResult.Yes) return;
        ClearEvaluatedDatabase.Enabled = false;
        Task.Run(() => _analysisServiceClient.ClearDatabase()).ContinueWith(result =>
        {
            if (result.IsCompleted)
            {
                ClearEvaluatedDatabase.Enabled = true;
            }
            else
            {
                MessageBox.Show(@"Connection error");
            }
        }, CancellationToken.None, TaskContinuationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());
    }

    private void StartAll_Click(object sender, EventArgs e)
    {
        StartAll.Enabled = false;
        StartAnalysisService.Enabled = false;
        StartCollectionService.Enabled = false;
        Task.Run(() =>
        {
            _collectionServiceClient.StartService();
            _analysisServiceClient.StartService();
        }).ContinueWith(result =>
        {
            if (result.IsCompleted)
            {
                StartAll.Visible = false;
                StopAll.Visible = true;
                StartAnalysisService.Visible = false;
                StartCollectionService.Visible = false;
                StopAnalysisService.Visible = true;
                StopCollectionService.Visible = true;
            }
            else
            {
                MessageBox.Show(@"Connection error");
            }
            StartAll.Enabled = true;
            StartAnalysisService.Enabled = true;
            StartCollectionService.Enabled = true;
        }, CancellationToken.None, TaskContinuationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());
    }

    private void StopAll_Click(object sender, EventArgs e)
    {
        StopAll.Enabled = false;
        StopAnalysisService.Enabled = false;
        StopCollectionService.Enabled = false;

        Task.Run(() =>
        {
            _collectionServiceClient.StopService();
            _analysisServiceClient.StopService();
            _collectionServiceClient.Poll();
            _analysisServiceClient.Poll();
        }).ContinueWith(result =>
        {
            if (result.IsCompleted)
            {
                StartAll.Visible = true;
                StopAll.Visible = false;
                StartAnalysisService.Visible = true;
                StartCollectionService.Visible = true;
                StopAnalysisService.Visible = false;
                StopCollectionService.Visible = false;
            }
            else
            {
                MessageBox.Show(@"Connection error");
            }
            StopAll.Enabled = false;
            StopAnalysisService.Enabled = false;
            StopCollectionService.Enabled = false;
        }, CancellationToken.None, TaskContinuationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());
    }

    #endregion

    #endregion

    #region Configuration

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
        };
        ConfigurationManager.SaveConfiguration(currentConfig);
        _collectionServiceClient.LoadConfiguration(File.ReadAllText("configuration.json"));
        _analysisServiceClient.LoadConfiguration(File.ReadAllText("configuration.json"));
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
        _collectionServiceClient.LoadConfiguration(File.ReadAllText("vkSettings.json"));
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

        OpenReport.Enabled = false;
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

        Task.Run(() => Reporter.OpenReport(SaveFileDialog.FileName, _analysisServiceClient.GetResults(filter)))
            .ContinueWith(result =>
            {
                MessageBox.Show(result.IsCompleted
                    ? @"The report was created successfully"
                    : $"Error: \r\n{result.Exception.Message}\r\n{result.Exception.StackTrace}");
                OpenReport.Enabled = true;
            }, CancellationToken.None, TaskContinuationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());
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

    private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        _analysisServiceClient.StopPolling();
        _collectionServiceClient.StopPolling();
    }
}