namespace DevTool.Forms
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            SaveFileDialog = new SaveFileDialog();
            VkCollectorTab = new TabPage();
            groupBox8 = new GroupBox();
            label17 = new Label();
            LocalConfiguration = new Button();
            NewCommunity = new TextBox();
            SaveVkSettings = new Button();
            ServiceAccessKey = new TextBox();
            LoadVkSettings = new Button();
            label11 = new Label();
            DeleteCommunity = new Button();
            SecureKey = new TextBox();
            label10 = new Label();
            AddCommunity = new Button();
            ApplicationId = new TextBox();
            label9 = new Label();
            label12 = new Label();
            Communities = new ListBox();
            ReportsTab = new TabPage();
            OpenReport = new Button();
            groupBox4 = new GroupBox();
            CommunityId = new TextBox();
            label16 = new Label();
            PostId = new TextBox();
            label15 = new Label();
            AuthorId = new TextBox();
            ToDate = new DateTimePicker();
            label14 = new Label();
            FromDate = new DateTimePicker();
            SelectedDateCheckBox = new CheckBox();
            LastMonthCheckBox = new CheckBox();
            LastWeekCheckBox = new CheckBox();
            Last3DaysCheckBox = new CheckBox();
            TodayCheckBox = new CheckBox();
            label13 = new Label();
            ConfigureTab = new TabPage();
            LoadConfiguration = new Button();
            SaveConfiguration = new Button();
            SetLocalConfig = new Button();
            groupBox6 = new GroupBox();
            ObserveDelay = new TextBox();
            label8 = new Label();
            groupBox5 = new GroupBox();
            PostQueueSize = new TextBox();
            label31 = new Label();
            ScanCommentsDelay = new TextBox();
            label30 = new Label();
            label29 = new Label();
            ScanPostDelay = new TextBox();
            MainTab = new TabPage();
            ServicesTabs = new TabControl();
            tabPage1 = new TabPage();
            groupBox7 = new GroupBox();
            groupBox13 = new GroupBox();
            CollectionServiceEndpoint = new ComboBox();
            ApplyNewCollectionServiceEndpoint = new Button();
            groupBox9 = new GroupBox();
            CollectionServiceHosts = new ListBox();
            AddCollectionServiceHost = new Button();
            DeleteCollectionServiceHost = new Button();
            NewCollectionHost = new TextBox();
            groupBox3 = new GroupBox();
            ClearCommentsDatabase = new Button();
            CollectionLogDate = new DateTimePicker();
            StartCollectionService = new Button();
            ViewCollectionServiceLogs = new Button();
            StopCollectionService = new Button();
            groupBox1 = new GroupBox();
            CollectionServiceProgressBar = new ProgressBar();
            RefreshCollectionServiceInfo = new Button();
            CollectionCollected = new Label();
            CollectionWarnings = new Label();
            CollectionErrors = new Label();
            CollectionUptime = new Label();
            CollectionState = new Label();
            CollectionConnection = new Label();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            CollectionProgressBarRed = new Panel();
            tabPage2 = new TabPage();
            groupBox10 = new GroupBox();
            groupBox14 = new GroupBox();
            AnalysisServiceEndpoint = new ComboBox();
            ApplyAnalysisServiceEndpoint = new Button();
            groupBox12 = new GroupBox();
            AnalysisServiceHosts = new ListBox();
            DeleteAnalysisServiceHost = new Button();
            AddAnalysisServiceHost = new Button();
            NewAnalysisHost = new TextBox();
            groupBox11 = new GroupBox();
            ViewAnalysisServiceLogs = new Button();
            ClearEvaluatedDatabase = new Button();
            StopAnalysisService = new Button();
            AnalysisLogDate = new DateTimePicker();
            StartAnalysisService = new Button();
            groupBox2 = new GroupBox();
            AnalysisServiceProgressBar = new ProgressBar();
            RefreshAnalysisServiceInfo = new Button();
            AnalysisEvaluated = new Label();
            AnalysisWarnings = new Label();
            label26 = new Label();
            AnalysisErrors = new Label();
            label25 = new Label();
            AnalysisUptime = new Label();
            label24 = new Label();
            AnalysisState = new Label();
            label23 = new Label();
            AnalysisConnection = new Label();
            label22 = new Label();
            label21 = new Label();
            AnalysisProgressBarRed = new Panel();
            Tabs = new TabControl();
            VkCollectorTab.SuspendLayout();
            groupBox8.SuspendLayout();
            ReportsTab.SuspendLayout();
            groupBox4.SuspendLayout();
            ConfigureTab.SuspendLayout();
            groupBox6.SuspendLayout();
            groupBox5.SuspendLayout();
            MainTab.SuspendLayout();
            ServicesTabs.SuspendLayout();
            tabPage1.SuspendLayout();
            groupBox7.SuspendLayout();
            groupBox13.SuspendLayout();
            groupBox9.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox1.SuspendLayout();
            tabPage2.SuspendLayout();
            groupBox10.SuspendLayout();
            groupBox14.SuspendLayout();
            groupBox12.SuspendLayout();
            groupBox11.SuspendLayout();
            groupBox2.SuspendLayout();
            Tabs.SuspendLayout();
            SuspendLayout();
            // 
            // SaveFileDialog
            // 
            SaveFileDialog.DefaultExt = "xlsx";
            SaveFileDialog.FileName = "EvaluatedCommentsReport.xlsx";
            // 
            // VkCollectorTab
            // 
            VkCollectorTab.Controls.Add(groupBox8);
            VkCollectorTab.Location = new Point(4, 44);
            VkCollectorTab.Margin = new Padding(3, 2, 3, 2);
            VkCollectorTab.Name = "VkCollectorTab";
            VkCollectorTab.Padding = new Padding(3, 2, 3, 2);
            VkCollectorTab.Size = new Size(590, 448);
            VkCollectorTab.TabIndex = 4;
            VkCollectorTab.Text = "Vk collector";
            VkCollectorTab.UseVisualStyleBackColor = true;
            // 
            // groupBox8
            // 
            groupBox8.Controls.Add(label17);
            groupBox8.Controls.Add(LocalConfiguration);
            groupBox8.Controls.Add(NewCommunity);
            groupBox8.Controls.Add(SaveVkSettings);
            groupBox8.Controls.Add(ServiceAccessKey);
            groupBox8.Controls.Add(LoadVkSettings);
            groupBox8.Controls.Add(label11);
            groupBox8.Controls.Add(DeleteCommunity);
            groupBox8.Controls.Add(SecureKey);
            groupBox8.Controls.Add(label10);
            groupBox8.Controls.Add(AddCommunity);
            groupBox8.Controls.Add(ApplicationId);
            groupBox8.Controls.Add(label9);
            groupBox8.Controls.Add(label12);
            groupBox8.Controls.Add(Communities);
            groupBox8.Dock = DockStyle.Fill;
            groupBox8.Location = new Point(3, 2);
            groupBox8.Margin = new Padding(3, 2, 3, 2);
            groupBox8.Name = "groupBox8";
            groupBox8.Padding = new Padding(3, 2, 3, 2);
            groupBox8.Size = new Size(584, 444);
            groupBox8.TabIndex = 0;
            groupBox8.TabStop = false;
            groupBox8.Text = "Settings";
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new Point(13, 26);
            label17.Name = "label17";
            label17.Size = new Size(370, 60);
            label17.TabIndex = 20;
            label17.Text = resources.GetString("label17.Text");
            // 
            // LocalConfiguration
            // 
            LocalConfiguration.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            LocalConfiguration.FlatStyle = FlatStyle.Flat;
            LocalConfiguration.Location = new Point(5, 402);
            LocalConfiguration.Margin = new Padding(3, 2, 3, 2);
            LocalConfiguration.Name = "LocalConfiguration";
            LocalConfiguration.Size = new Size(144, 38);
            LocalConfiguration.TabIndex = 19;
            LocalConfiguration.Text = "Default configuration";
            LocalConfiguration.UseVisualStyleBackColor = true;
            LocalConfiguration.Click += LocalConfiguration_Click;
            // 
            // NewCommunity
            // 
            NewCommunity.Location = new Point(144, 249);
            NewCommunity.Margin = new Padding(3, 2, 3, 2);
            NewCommunity.Name = "NewCommunity";
            NewCommunity.Size = new Size(161, 23);
            NewCommunity.TabIndex = 16;
            // 
            // SaveVkSettings
            // 
            SaveVkSettings.Anchor = AnchorStyles.Bottom;
            SaveVkSettings.FlatStyle = FlatStyle.Flat;
            SaveVkSettings.Location = new Point(223, 402);
            SaveVkSettings.Margin = new Padding(3, 2, 3, 2);
            SaveVkSettings.Name = "SaveVkSettings";
            SaveVkSettings.Size = new Size(144, 38);
            SaveVkSettings.TabIndex = 18;
            SaveVkSettings.Text = "Save configuration";
            SaveVkSettings.UseVisualStyleBackColor = true;
            SaveVkSettings.Click += SaveVkSettings_Click;
            // 
            // ServiceAccessKey
            // 
            ServiceAccessKey.Location = new Point(144, 146);
            ServiceAccessKey.Margin = new Padding(4);
            ServiceAccessKey.Name = "ServiceAccessKey";
            ServiceAccessKey.Size = new Size(420, 23);
            ServiceAccessKey.TabIndex = 11;
            // 
            // LoadVkSettings
            // 
            LoadVkSettings.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            LoadVkSettings.FlatStyle = FlatStyle.Flat;
            LoadVkSettings.Location = new Point(437, 402);
            LoadVkSettings.Margin = new Padding(3, 2, 3, 2);
            LoadVkSettings.Name = "LoadVkSettings";
            LoadVkSettings.Size = new Size(144, 38);
            LoadVkSettings.TabIndex = 17;
            LoadVkSettings.Text = "Upload configuration";
            LoadVkSettings.UseVisualStyleBackColor = true;
            LoadVkSettings.Click += LoadVkSettings_Click;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(16, 148);
            label11.Margin = new Padding(4);
            label11.Name = "label11";
            label11.Size = new Size(105, 15);
            label11.TabIndex = 10;
            label11.Text = "Service access key:";
            // 
            // DeleteCommunity
            // 
            DeleteCommunity.FlatStyle = FlatStyle.Flat;
            DeleteCommunity.Location = new Point(224, 268);
            DeleteCommunity.Margin = new Padding(3, 2, 3, 2);
            DeleteCommunity.Name = "DeleteCommunity";
            DeleteCommunity.Size = new Size(80, 22);
            DeleteCommunity.TabIndex = 15;
            DeleteCommunity.Text = "Delete";
            DeleteCommunity.UseVisualStyleBackColor = true;
            DeleteCommunity.Click += DeleteCommunity_Click;
            // 
            // SecureKey
            // 
            SecureKey.Location = new Point(144, 123);
            SecureKey.Margin = new Padding(4);
            SecureKey.Name = "SecureKey";
            SecureKey.Size = new Size(420, 23);
            SecureKey.TabIndex = 9;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(55, 126);
            label10.Margin = new Padding(4);
            label10.Name = "label10";
            label10.Size = new Size(66, 15);
            label10.TabIndex = 8;
            label10.Text = "Secure key:";
            // 
            // AddCommunity
            // 
            AddCommunity.FlatStyle = FlatStyle.Flat;
            AddCommunity.Location = new Point(144, 268);
            AddCommunity.Margin = new Padding(3, 2, 3, 2);
            AddCommunity.Name = "AddCommunity";
            AddCommunity.Size = new Size(80, 22);
            AddCommunity.TabIndex = 14;
            AddCommunity.Text = "Add";
            AddCommunity.UseVisualStyleBackColor = true;
            AddCommunity.Click += AddCommunity_Click;
            // 
            // ApplicationId
            // 
            ApplicationId.Location = new Point(144, 100);
            ApplicationId.Margin = new Padding(4);
            ApplicationId.Name = "ApplicationId";
            ApplicationId.Size = new Size(420, 23);
            ApplicationId.TabIndex = 7;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(36, 103);
            label9.Margin = new Padding(4);
            label9.Name = "label9";
            label9.Size = new Size(85, 15);
            label9.TabIndex = 6;
            label9.Text = "Application ID:";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(26, 172);
            label12.Name = "label12";
            label12.Size = new Size(95, 15);
            label12.TabIndex = 13;
            label12.Text = "Communities id:";
            // 
            // Communities
            // 
            Communities.FormattingEnabled = true;
            Communities.ItemHeight = 15;
            Communities.Location = new Point(144, 172);
            Communities.Margin = new Padding(3, 2, 3, 2);
            Communities.Name = "Communities";
            Communities.Size = new Size(161, 79);
            Communities.TabIndex = 12;
            // 
            // ReportsTab
            // 
            ReportsTab.Controls.Add(OpenReport);
            ReportsTab.Controls.Add(groupBox4);
            ReportsTab.Location = new Point(4, 44);
            ReportsTab.Margin = new Padding(3, 2, 3, 2);
            ReportsTab.Name = "ReportsTab";
            ReportsTab.Padding = new Padding(3, 2, 3, 2);
            ReportsTab.Size = new Size(590, 448);
            ReportsTab.TabIndex = 2;
            ReportsTab.Text = "Reports";
            ReportsTab.UseVisualStyleBackColor = true;
            // 
            // OpenReport
            // 
            OpenReport.Anchor = AnchorStyles.Bottom;
            OpenReport.FlatStyle = FlatStyle.Flat;
            OpenReport.Location = new Point(212, 404);
            OpenReport.Margin = new Padding(3, 2, 3, 2);
            OpenReport.Name = "OpenReport";
            OpenReport.Size = new Size(153, 38);
            OpenReport.TabIndex = 1;
            OpenReport.Text = "Open report";
            OpenReport.UseVisualStyleBackColor = true;
            OpenReport.Click += OpenReport_Click;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(CommunityId);
            groupBox4.Controls.Add(label16);
            groupBox4.Controls.Add(PostId);
            groupBox4.Controls.Add(label15);
            groupBox4.Controls.Add(AuthorId);
            groupBox4.Controls.Add(ToDate);
            groupBox4.Controls.Add(label14);
            groupBox4.Controls.Add(FromDate);
            groupBox4.Controls.Add(SelectedDateCheckBox);
            groupBox4.Controls.Add(LastMonthCheckBox);
            groupBox4.Controls.Add(LastWeekCheckBox);
            groupBox4.Controls.Add(Last3DaysCheckBox);
            groupBox4.Controls.Add(TodayCheckBox);
            groupBox4.Controls.Add(label13);
            groupBox4.Location = new Point(5, 4);
            groupBox4.Margin = new Padding(3, 2, 3, 2);
            groupBox4.Name = "groupBox4";
            groupBox4.Padding = new Padding(3, 2, 3, 2);
            groupBox4.Size = new Size(360, 172);
            groupBox4.TabIndex = 0;
            groupBox4.TabStop = false;
            groupBox4.Text = "Filter";
            // 
            // CommunityId
            // 
            CommunityId.Location = new Point(229, 68);
            CommunityId.Margin = new Padding(3, 2, 3, 2);
            CommunityId.Name = "CommunityId";
            CommunityId.Size = new Size(110, 23);
            CommunityId.TabIndex = 13;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new Point(132, 71);
            label16.Name = "label16";
            label16.Size = new Size(87, 15);
            label16.TabIndex = 12;
            label16.Text = "Community id:";
            // 
            // PostId
            // 
            PostId.Location = new Point(229, 45);
            PostId.Margin = new Padding(3, 2, 3, 2);
            PostId.Name = "PostId";
            PostId.Size = new Size(110, 23);
            PostId.TabIndex = 11;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(173, 47);
            label15.Name = "label15";
            label15.Size = new Size(46, 15);
            label15.TabIndex = 10;
            label15.Text = "Post id:";
            // 
            // AuthorId
            // 
            AuthorId.Location = new Point(229, 22);
            AuthorId.Margin = new Padding(3, 2, 3, 2);
            AuthorId.Name = "AuthorId";
            AuthorId.Size = new Size(110, 23);
            AuthorId.TabIndex = 9;
            // 
            // ToDate
            // 
            ToDate.Enabled = false;
            ToDate.Location = new Point(193, 136);
            ToDate.Margin = new Padding(3, 2, 3, 2);
            ToDate.Name = "ToDate";
            ToDate.Size = new Size(146, 23);
            ToDate.TabIndex = 8;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(175, 140);
            label14.Name = "label14";
            label14.Size = new Size(12, 15);
            label14.TabIndex = 7;
            label14.Text = "-";
            // 
            // FromDate
            // 
            FromDate.Enabled = false;
            FromDate.Location = new Point(24, 136);
            FromDate.Margin = new Padding(3, 2, 3, 2);
            FromDate.Name = "FromDate";
            FromDate.Size = new Size(146, 23);
            FromDate.TabIndex = 6;
            // 
            // SelectedDateCheckBox
            // 
            SelectedDateCheckBox.AutoSize = true;
            SelectedDateCheckBox.Location = new Point(24, 113);
            SelectedDateCheckBox.Margin = new Padding(3, 2, 3, 2);
            SelectedDateCheckBox.Name = "SelectedDateCheckBox";
            SelectedDateCheckBox.Size = new Size(99, 19);
            SelectedDateCheckBox.TabIndex = 5;
            SelectedDateCheckBox.Text = "Selected date:";
            SelectedDateCheckBox.UseVisualStyleBackColor = true;
            SelectedDateCheckBox.CheckedChanged += SelectedDateCheckBox_CheckedChanged;
            // 
            // LastMonthCheckBox
            // 
            LastMonthCheckBox.AutoSize = true;
            LastMonthCheckBox.Location = new Point(24, 91);
            LastMonthCheckBox.Margin = new Padding(3, 2, 3, 2);
            LastMonthCheckBox.Name = "LastMonthCheckBox";
            LastMonthCheckBox.Size = new Size(86, 19);
            LastMonthCheckBox.TabIndex = 4;
            LastMonthCheckBox.Text = "Last month";
            LastMonthCheckBox.UseVisualStyleBackColor = true;
            LastMonthCheckBox.CheckedChanged += LastMonthCheckBox_CheckedChanged;
            // 
            // LastWeekCheckBox
            // 
            LastWeekCheckBox.AutoSize = true;
            LastWeekCheckBox.Location = new Point(24, 68);
            LastWeekCheckBox.Margin = new Padding(3, 2, 3, 2);
            LastWeekCheckBox.Name = "LastWeekCheckBox";
            LastWeekCheckBox.Size = new Size(77, 19);
            LastWeekCheckBox.TabIndex = 3;
            LastWeekCheckBox.Text = "Last week";
            LastWeekCheckBox.UseVisualStyleBackColor = true;
            LastWeekCheckBox.CheckedChanged += LastWeekCheckBox_CheckedChanged;
            // 
            // Last3DaysCheckBox
            // 
            Last3DaysCheckBox.AutoSize = true;
            Last3DaysCheckBox.Location = new Point(24, 46);
            Last3DaysCheckBox.Margin = new Padding(3, 2, 3, 2);
            Last3DaysCheckBox.Name = "Last3DaysCheckBox";
            Last3DaysCheckBox.Size = new Size(83, 19);
            Last3DaysCheckBox.TabIndex = 2;
            Last3DaysCheckBox.Text = "Last 3 days";
            Last3DaysCheckBox.UseVisualStyleBackColor = true;
            Last3DaysCheckBox.CheckedChanged += Last3DaysCheckBox_CheckedChanged;
            // 
            // TodayCheckBox
            // 
            TodayCheckBox.AutoSize = true;
            TodayCheckBox.Checked = true;
            TodayCheckBox.CheckState = CheckState.Checked;
            TodayCheckBox.Location = new Point(24, 23);
            TodayCheckBox.Margin = new Padding(3, 2, 3, 2);
            TodayCheckBox.Name = "TodayCheckBox";
            TodayCheckBox.Size = new Size(57, 19);
            TodayCheckBox.TabIndex = 1;
            TodayCheckBox.Text = "Today";
            TodayCheckBox.UseVisualStyleBackColor = true;
            TodayCheckBox.CheckedChanged += TodayCheckBox_CheckedChanged;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(159, 25);
            label13.Name = "label13";
            label13.Size = new Size(60, 15);
            label13.TabIndex = 0;
            label13.Text = "Author id:";
            // 
            // ConfigureTab
            // 
            ConfigureTab.Controls.Add(LoadConfiguration);
            ConfigureTab.Controls.Add(SaveConfiguration);
            ConfigureTab.Controls.Add(SetLocalConfig);
            ConfigureTab.Controls.Add(groupBox6);
            ConfigureTab.Controls.Add(groupBox5);
            ConfigureTab.Location = new Point(4, 44);
            ConfigureTab.Margin = new Padding(3, 2, 3, 2);
            ConfigureTab.Name = "ConfigureTab";
            ConfigureTab.Padding = new Padding(3, 2, 3, 2);
            ConfigureTab.Size = new Size(590, 448);
            ConfigureTab.TabIndex = 1;
            ConfigureTab.Text = "Configure";
            ConfigureTab.UseVisualStyleBackColor = true;
            // 
            // LoadConfiguration
            // 
            LoadConfiguration.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            LoadConfiguration.FlatStyle = FlatStyle.Flat;
            LoadConfiguration.Location = new Point(439, 418);
            LoadConfiguration.Margin = new Padding(3, 2, 3, 2);
            LoadConfiguration.Name = "LoadConfiguration";
            LoadConfiguration.Size = new Size(144, 38);
            LoadConfiguration.TabIndex = 0;
            LoadConfiguration.Text = "Upload configuration";
            LoadConfiguration.UseVisualStyleBackColor = true;
            LoadConfiguration.Click += LoadConfiguration_Click;
            // 
            // SaveConfiguration
            // 
            SaveConfiguration.Anchor = AnchorStyles.Bottom;
            SaveConfiguration.FlatStyle = FlatStyle.Flat;
            SaveConfiguration.Location = new Point(226, 418);
            SaveConfiguration.Margin = new Padding(3, 2, 3, 2);
            SaveConfiguration.Name = "SaveConfiguration";
            SaveConfiguration.Size = new Size(144, 38);
            SaveConfiguration.TabIndex = 1;
            SaveConfiguration.Text = "Save configuration";
            SaveConfiguration.UseVisualStyleBackColor = true;
            SaveConfiguration.Click += SaveConfiguration_Click;
            // 
            // SetLocalConfig
            // 
            SetLocalConfig.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            SetLocalConfig.FlatStyle = FlatStyle.Flat;
            SetLocalConfig.Location = new Point(7, 418);
            SetLocalConfig.Margin = new Padding(3, 2, 3, 2);
            SetLocalConfig.Name = "SetLocalConfig";
            SetLocalConfig.Size = new Size(144, 38);
            SetLocalConfig.TabIndex = 20;
            SetLocalConfig.Text = "Default configuration";
            SetLocalConfig.UseVisualStyleBackColor = true;
            SetLocalConfig.Click += SetLocalConfig_Click;
            // 
            // groupBox6
            // 
            groupBox6.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            groupBox6.Controls.Add(ObserveDelay);
            groupBox6.Controls.Add(label8);
            groupBox6.Location = new Point(313, 4);
            groupBox6.Margin = new Padding(3, 2, 3, 2);
            groupBox6.Name = "groupBox6";
            groupBox6.Padding = new Padding(3, 2, 3, 2);
            groupBox6.Size = new Size(271, 404);
            groupBox6.TabIndex = 2;
            groupBox6.TabStop = false;
            groupBox6.Text = "Analysis service configuration";
            // 
            // ObserveDelay
            // 
            ObserveDelay.Location = new Point(157, 205);
            ObserveDelay.Margin = new Padding(4);
            ObserveDelay.Name = "ObserveDelay";
            ObserveDelay.Size = new Size(110, 23);
            ObserveDelay.TabIndex = 9;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(9, 207);
            label8.Margin = new Padding(4);
            label8.Name = "label8";
            label8.Size = new Size(84, 15);
            label8.TabIndex = 8;
            label8.Text = "Observe delay:";
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(PostQueueSize);
            groupBox5.Controls.Add(label31);
            groupBox5.Controls.Add(ScanCommentsDelay);
            groupBox5.Controls.Add(label30);
            groupBox5.Controls.Add(label29);
            groupBox5.Controls.Add(ScanPostDelay);
            groupBox5.Location = new Point(5, 4);
            groupBox5.Margin = new Padding(3, 2, 3, 2);
            groupBox5.Name = "groupBox5";
            groupBox5.Padding = new Padding(3, 2, 3, 2);
            groupBox5.Size = new Size(278, 404);
            groupBox5.TabIndex = 1;
            groupBox5.TabStop = false;
            groupBox5.Text = "Collection service configuration";
            // 
            // PostQueueSize
            // 
            PostQueueSize.Location = new Point(152, 260);
            PostQueueSize.Margin = new Padding(4);
            PostQueueSize.Name = "PostQueueSize";
            PostQueueSize.Size = new Size(110, 23);
            PostQueueSize.TabIndex = 5;
            // 
            // label31
            // 
            label31.AutoSize = true;
            label31.Location = new Point(7, 262);
            label31.Margin = new Padding(4);
            label31.Name = "label31";
            label31.Size = new Size(91, 15);
            label31.TabIndex = 4;
            label31.Text = "Post queue size:";
            // 
            // ScanCommentsDelay
            // 
            ScanCommentsDelay.Location = new Point(152, 232);
            ScanCommentsDelay.Margin = new Padding(4);
            ScanCommentsDelay.Name = "ScanCommentsDelay";
            ScanCommentsDelay.Size = new Size(110, 23);
            ScanCommentsDelay.TabIndex = 3;
            // 
            // label30
            // 
            label30.AutoSize = true;
            label30.Location = new Point(7, 235);
            label30.Margin = new Padding(4);
            label30.Name = "label30";
            label30.Size = new Size(126, 15);
            label30.TabIndex = 2;
            label30.Text = "Scan comments delay:";
            // 
            // label29
            // 
            label29.AutoSize = true;
            label29.Location = new Point(7, 207);
            label29.Margin = new Padding(4);
            label29.Name = "label29";
            label29.Size = new Size(92, 15);
            label29.TabIndex = 1;
            label29.Text = "Scan post delay:";
            // 
            // ScanPostDelay
            // 
            ScanPostDelay.Location = new Point(152, 205);
            ScanPostDelay.Margin = new Padding(4);
            ScanPostDelay.Name = "ScanPostDelay";
            ScanPostDelay.Size = new Size(110, 23);
            ScanPostDelay.TabIndex = 0;
            // 
            // MainTab
            // 
            MainTab.Controls.Add(ServicesTabs);
            MainTab.Location = new Point(4, 44);
            MainTab.Margin = new Padding(3, 2, 3, 2);
            MainTab.Name = "MainTab";
            MainTab.Padding = new Padding(3, 2, 3, 2);
            MainTab.Size = new Size(590, 448);
            MainTab.TabIndex = 0;
            MainTab.Text = "Main";
            MainTab.UseVisualStyleBackColor = true;
            // 
            // ServicesTabs
            // 
            ServicesTabs.Controls.Add(tabPage1);
            ServicesTabs.Controls.Add(tabPage2);
            ServicesTabs.Location = new Point(2, 2);
            ServicesTabs.Name = "ServicesTabs";
            ServicesTabs.SelectedIndex = 0;
            ServicesTabs.Size = new Size(582, 443);
            ServicesTabs.TabIndex = 24;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(groupBox7);
            tabPage1.Controls.Add(groupBox3);
            tabPage1.Controls.Add(groupBox1);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(574, 415);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Collection Service";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox7
            // 
            groupBox7.Controls.Add(groupBox13);
            groupBox7.Controls.Add(groupBox9);
            groupBox7.Location = new Point(291, 5);
            groupBox7.Margin = new Padding(3, 2, 3, 2);
            groupBox7.Name = "groupBox7";
            groupBox7.Padding = new Padding(3, 2, 3, 2);
            groupBox7.Size = new Size(277, 227);
            groupBox7.TabIndex = 19;
            groupBox7.TabStop = false;
            groupBox7.Text = "Host configuration";
            // 
            // groupBox13
            // 
            groupBox13.Controls.Add(CollectionServiceEndpoint);
            groupBox13.Controls.Add(ApplyNewCollectionServiceEndpoint);
            groupBox13.Location = new Point(6, 147);
            groupBox13.Name = "groupBox13";
            groupBox13.Size = new Size(265, 75);
            groupBox13.TabIndex = 18;
            groupBox13.TabStop = false;
            groupBox13.Text = "Current host";
            // 
            // CollectionServiceEndpoint
            // 
            CollectionServiceEndpoint.DropDownStyle = ComboBoxStyle.DropDownList;
            CollectionServiceEndpoint.FormattingEnabled = true;
            CollectionServiceEndpoint.Location = new Point(57, 21);
            CollectionServiceEndpoint.Margin = new Padding(3, 2, 3, 2);
            CollectionServiceEndpoint.Name = "CollectionServiceEndpoint";
            CollectionServiceEndpoint.Size = new Size(153, 23);
            CollectionServiceEndpoint.TabIndex = 17;
            // 
            // ApplyNewCollectionServiceEndpoint
            // 
            ApplyNewCollectionServiceEndpoint.FlatStyle = FlatStyle.Flat;
            ApplyNewCollectionServiceEndpoint.Location = new Point(57, 46);
            ApplyNewCollectionServiceEndpoint.Margin = new Padding(3, 2, 3, 2);
            ApplyNewCollectionServiceEndpoint.Name = "ApplyNewCollectionServiceEndpoint";
            ApplyNewCollectionServiceEndpoint.Size = new Size(153, 25);
            ApplyNewCollectionServiceEndpoint.TabIndex = 19;
            ApplyNewCollectionServiceEndpoint.Text = "Сonnect";
            ApplyNewCollectionServiceEndpoint.UseVisualStyleBackColor = true;
            ApplyNewCollectionServiceEndpoint.Click += ApplyNewCollectionServiceEndpoint_Click_1;
            // 
            // groupBox9
            // 
            groupBox9.Controls.Add(CollectionServiceHosts);
            groupBox9.Controls.Add(AddCollectionServiceHost);
            groupBox9.Controls.Add(DeleteCollectionServiceHost);
            groupBox9.Controls.Add(NewCollectionHost);
            groupBox9.Location = new Point(6, 16);
            groupBox9.Name = "groupBox9";
            groupBox9.Size = new Size(265, 127);
            groupBox9.TabIndex = 20;
            groupBox9.TabStop = false;
            groupBox9.Text = "Saved hosts";
            // 
            // CollectionServiceHosts
            // 
            CollectionServiceHosts.FormattingEnabled = true;
            CollectionServiceHosts.ItemHeight = 15;
            CollectionServiceHosts.Location = new Point(57, 20);
            CollectionServiceHosts.Margin = new Padding(3, 2, 3, 2);
            CollectionServiceHosts.Name = "CollectionServiceHosts";
            CollectionServiceHosts.Size = new Size(153, 49);
            CollectionServiceHosts.TabIndex = 0;
            // 
            // AddCollectionServiceHost
            // 
            AddCollectionServiceHost.FlatStyle = FlatStyle.Flat;
            AddCollectionServiceHost.Location = new Point(57, 96);
            AddCollectionServiceHost.Margin = new Padding(3, 2, 3, 2);
            AddCollectionServiceHost.Name = "AddCollectionServiceHost";
            AddCollectionServiceHost.Size = new Size(74, 25);
            AddCollectionServiceHost.TabIndex = 4;
            AddCollectionServiceHost.Text = "Add";
            AddCollectionServiceHost.UseVisualStyleBackColor = true;
            AddCollectionServiceHost.Click += AddCollectionServiceHost_Click;
            // 
            // DeleteCollectionServiceHost
            // 
            DeleteCollectionServiceHost.FlatStyle = FlatStyle.Flat;
            DeleteCollectionServiceHost.Location = new Point(136, 96);
            DeleteCollectionServiceHost.Margin = new Padding(3, 2, 3, 2);
            DeleteCollectionServiceHost.Name = "DeleteCollectionServiceHost";
            DeleteCollectionServiceHost.Size = new Size(74, 25);
            DeleteCollectionServiceHost.TabIndex = 5;
            DeleteCollectionServiceHost.Text = "Delete";
            DeleteCollectionServiceHost.UseVisualStyleBackColor = true;
            DeleteCollectionServiceHost.Click += DeleteCollectionServiceHost_Click;
            // 
            // NewCollectionHost
            // 
            NewCollectionHost.Location = new Point(57, 71);
            NewCollectionHost.Margin = new Padding(3, 2, 3, 2);
            NewCollectionHost.Name = "NewCollectionHost";
            NewCollectionHost.PlaceholderText = "New host...";
            NewCollectionHost.Size = new Size(153, 23);
            NewCollectionHost.TabIndex = 8;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(ClearCommentsDatabase);
            groupBox3.Controls.Add(CollectionLogDate);
            groupBox3.Controls.Add(StartCollectionService);
            groupBox3.Controls.Add(ViewCollectionServiceLogs);
            groupBox3.Controls.Add(StopCollectionService);
            groupBox3.Location = new Point(6, 237);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(562, 172);
            groupBox3.TabIndex = 18;
            groupBox3.TabStop = false;
            groupBox3.Text = "Control";
            // 
            // ClearCommentsDatabase
            // 
            ClearCommentsDatabase.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            ClearCommentsDatabase.FlatStyle = FlatStyle.Flat;
            ClearCommentsDatabase.Location = new Point(403, 77);
            ClearCommentsDatabase.Margin = new Padding(3, 2, 3, 2);
            ClearCommentsDatabase.Name = "ClearCommentsDatabase";
            ClearCommentsDatabase.Size = new Size(153, 38);
            ClearCommentsDatabase.TabIndex = 2;
            ClearCommentsDatabase.Text = "Clear database";
            ClearCommentsDatabase.UseVisualStyleBackColor = true;
            ClearCommentsDatabase.Click += ClearCommentsDatabase_Click;
            // 
            // CollectionLogDate
            // 
            CollectionLogDate.Location = new Point(4, 50);
            CollectionLogDate.Margin = new Padding(3, 2, 3, 2);
            CollectionLogDate.Name = "CollectionLogDate";
            CollectionLogDate.Size = new Size(153, 23);
            CollectionLogDate.TabIndex = 17;
            // 
            // StartCollectionService
            // 
            StartCollectionService.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            StartCollectionService.FlatStyle = FlatStyle.Flat;
            StartCollectionService.Location = new Point(205, 77);
            StartCollectionService.Margin = new Padding(3, 2, 3, 2);
            StartCollectionService.Name = "StartCollectionService";
            StartCollectionService.Size = new Size(153, 38);
            StartCollectionService.TabIndex = 0;
            StartCollectionService.Text = "Start collection service";
            StartCollectionService.UseVisualStyleBackColor = true;
            StartCollectionService.Click += StartCollectionService_Click;
            // 
            // ViewCollectionServiceLogs
            // 
            ViewCollectionServiceLogs.FlatStyle = FlatStyle.Flat;
            ViewCollectionServiceLogs.Location = new Point(4, 77);
            ViewCollectionServiceLogs.Margin = new Padding(3, 2, 3, 2);
            ViewCollectionServiceLogs.Name = "ViewCollectionServiceLogs";
            ViewCollectionServiceLogs.Size = new Size(153, 38);
            ViewCollectionServiceLogs.TabIndex = 8;
            ViewCollectionServiceLogs.Text = "View collection log";
            ViewCollectionServiceLogs.UseVisualStyleBackColor = true;
            ViewCollectionServiceLogs.Click += ViewCollectionServiceLogs_Click;
            // 
            // StopCollectionService
            // 
            StopCollectionService.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            StopCollectionService.FlatStyle = FlatStyle.Flat;
            StopCollectionService.Location = new Point(205, 77);
            StopCollectionService.Margin = new Padding(3, 2, 3, 2);
            StopCollectionService.Name = "StopCollectionService";
            StopCollectionService.Size = new Size(153, 38);
            StopCollectionService.TabIndex = 4;
            StopCollectionService.Text = "Stop collection service";
            StopCollectionService.UseVisualStyleBackColor = true;
            StopCollectionService.Click += StopCollectionService_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(CollectionServiceProgressBar);
            groupBox1.Controls.Add(RefreshCollectionServiceInfo);
            groupBox1.Controls.Add(CollectionCollected);
            groupBox1.Controls.Add(CollectionWarnings);
            groupBox1.Controls.Add(CollectionErrors);
            groupBox1.Controls.Add(CollectionUptime);
            groupBox1.Controls.Add(CollectionState);
            groupBox1.Controls.Add(CollectionConnection);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(CollectionProgressBarRed);
            groupBox1.Location = new Point(6, 5);
            groupBox1.Margin = new Padding(3, 2, 3, 2);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(3, 2, 3, 2);
            groupBox1.Size = new Size(279, 227);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Collection service state";
            // 
            // CollectionServiceProgressBar
            // 
            CollectionServiceProgressBar.Location = new Point(5, 24);
            CollectionServiceProgressBar.Margin = new Padding(3, 2, 3, 2);
            CollectionServiceProgressBar.Name = "CollectionServiceProgressBar";
            CollectionServiceProgressBar.Size = new Size(194, 13);
            CollectionServiceProgressBar.TabIndex = 16;
            // 
            // RefreshCollectionServiceInfo
            // 
            RefreshCollectionServiceInfo.FlatStyle = FlatStyle.Flat;
            RefreshCollectionServiceInfo.Location = new Point(205, 20);
            RefreshCollectionServiceInfo.Margin = new Padding(3, 2, 3, 2);
            RefreshCollectionServiceInfo.Name = "RefreshCollectionServiceInfo";
            RefreshCollectionServiceInfo.Size = new Size(66, 27);
            RefreshCollectionServiceInfo.TabIndex = 15;
            RefreshCollectionServiceInfo.Text = "Refresh";
            RefreshCollectionServiceInfo.UseVisualStyleBackColor = true;
            RefreshCollectionServiceInfo.Click += RefreshCollectionServiceInfo_Click;
            // 
            // CollectionCollected
            // 
            CollectionCollected.AutoSize = true;
            CollectionCollected.Location = new Point(144, 169);
            CollectionCollected.Margin = new Padding(4);
            CollectionCollected.Name = "CollectionCollected";
            CollectionCollected.Size = new Size(36, 15);
            CollectionCollected.TabIndex = 14;
            CollectionCollected.Text = "None";
            // 
            // CollectionWarnings
            // 
            CollectionWarnings.AutoSize = true;
            CollectionWarnings.Location = new Point(144, 146);
            CollectionWarnings.Margin = new Padding(4);
            CollectionWarnings.Name = "CollectionWarnings";
            CollectionWarnings.Size = new Size(36, 15);
            CollectionWarnings.TabIndex = 13;
            CollectionWarnings.Text = "None";
            // 
            // CollectionErrors
            // 
            CollectionErrors.AutoSize = true;
            CollectionErrors.Location = new Point(144, 124);
            CollectionErrors.Margin = new Padding(4);
            CollectionErrors.Name = "CollectionErrors";
            CollectionErrors.Size = new Size(36, 15);
            CollectionErrors.TabIndex = 12;
            CollectionErrors.Text = "None";
            // 
            // CollectionUptime
            // 
            CollectionUptime.AutoSize = true;
            CollectionUptime.Location = new Point(144, 101);
            CollectionUptime.Margin = new Padding(4);
            CollectionUptime.Name = "CollectionUptime";
            CollectionUptime.Size = new Size(36, 15);
            CollectionUptime.TabIndex = 11;
            CollectionUptime.Text = "None";
            // 
            // CollectionState
            // 
            CollectionState.AutoSize = true;
            CollectionState.Location = new Point(144, 79);
            CollectionState.Margin = new Padding(4);
            CollectionState.Name = "CollectionState";
            CollectionState.Size = new Size(36, 15);
            CollectionState.TabIndex = 10;
            CollectionState.Text = "None";
            // 
            // CollectionConnection
            // 
            CollectionConnection.AutoSize = true;
            CollectionConnection.Location = new Point(144, 56);
            CollectionConnection.Margin = new Padding(4);
            CollectionConnection.Name = "CollectionConnection";
            CollectionConnection.Size = new Size(36, 15);
            CollectionConnection.TabIndex = 9;
            CollectionConnection.Text = "None";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(59, 169);
            label6.Margin = new Padding(4);
            label6.Name = "label6";
            label6.Size = new Size(60, 15);
            label6.TabIndex = 5;
            label6.Text = "Collected:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(59, 146);
            label5.Margin = new Padding(4);
            label5.Name = "label5";
            label5.Size = new Size(60, 15);
            label5.TabIndex = 4;
            label5.Text = "Warnings:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(59, 124);
            label4.Margin = new Padding(4);
            label4.Name = "label4";
            label4.Size = new Size(40, 15);
            label4.TabIndex = 3;
            label4.Text = "Errors:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(59, 101);
            label3.Margin = new Padding(4);
            label3.Name = "label3";
            label3.Size = new Size(49, 15);
            label3.TabIndex = 2;
            label3.Text = "Uptime:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(59, 79);
            label2.Margin = new Padding(4);
            label2.Name = "label2";
            label2.Size = new Size(36, 15);
            label2.TabIndex = 1;
            label2.Text = "State:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(59, 56);
            label1.Margin = new Padding(4);
            label1.Name = "label1";
            label1.Size = new Size(72, 15);
            label1.TabIndex = 0;
            label1.Text = "Connection:";
            // 
            // CollectionProgressBarRed
            // 
            CollectionProgressBarRed.BackColor = Color.Red;
            CollectionProgressBarRed.Location = new Point(5, 24);
            CollectionProgressBarRed.Margin = new Padding(3, 2, 3, 2);
            CollectionProgressBarRed.Name = "CollectionProgressBarRed";
            CollectionProgressBarRed.Size = new Size(194, 13);
            CollectionProgressBarRed.TabIndex = 17;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(groupBox10);
            tabPage2.Controls.Add(groupBox11);
            tabPage2.Controls.Add(groupBox2);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(574, 415);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Analysis Service";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox10
            // 
            groupBox10.Controls.Add(groupBox14);
            groupBox10.Controls.Add(groupBox12);
            groupBox10.Location = new Point(291, 5);
            groupBox10.Margin = new Padding(3, 2, 3, 2);
            groupBox10.Name = "groupBox10";
            groupBox10.Padding = new Padding(3, 2, 3, 2);
            groupBox10.Size = new Size(280, 227);
            groupBox10.TabIndex = 25;
            groupBox10.TabStop = false;
            groupBox10.Text = "Host configuration";
            // 
            // groupBox14
            // 
            groupBox14.Controls.Add(AnalysisServiceEndpoint);
            groupBox14.Controls.Add(ApplyAnalysisServiceEndpoint);
            groupBox14.Location = new Point(6, 147);
            groupBox14.Name = "groupBox14";
            groupBox14.Size = new Size(274, 75);
            groupBox14.TabIndex = 35;
            groupBox14.TabStop = false;
            groupBox14.Text = "Current host";
            // 
            // AnalysisServiceEndpoint
            // 
            AnalysisServiceEndpoint.DropDownStyle = ComboBoxStyle.DropDownList;
            AnalysisServiceEndpoint.FormattingEnabled = true;
            AnalysisServiceEndpoint.Location = new Point(57, 21);
            AnalysisServiceEndpoint.Margin = new Padding(3, 2, 3, 2);
            AnalysisServiceEndpoint.Name = "AnalysisServiceEndpoint";
            AnalysisServiceEndpoint.Size = new Size(153, 23);
            AnalysisServiceEndpoint.TabIndex = 31;
            // 
            // ApplyAnalysisServiceEndpoint
            // 
            ApplyAnalysisServiceEndpoint.FlatStyle = FlatStyle.Flat;
            ApplyAnalysisServiceEndpoint.Location = new Point(57, 46);
            ApplyAnalysisServiceEndpoint.Margin = new Padding(3, 2, 3, 2);
            ApplyAnalysisServiceEndpoint.Name = "ApplyAnalysisServiceEndpoint";
            ApplyAnalysisServiceEndpoint.Size = new Size(153, 25);
            ApplyAnalysisServiceEndpoint.TabIndex = 33;
            ApplyAnalysisServiceEndpoint.Text = "Сonnect";
            ApplyAnalysisServiceEndpoint.UseVisualStyleBackColor = true;
            ApplyAnalysisServiceEndpoint.Click += ApplyAnalysisServiceEndpoint_Click_1;
            // 
            // groupBox12
            // 
            groupBox12.Controls.Add(AnalysisServiceHosts);
            groupBox12.Controls.Add(DeleteAnalysisServiceHost);
            groupBox12.Controls.Add(AddAnalysisServiceHost);
            groupBox12.Controls.Add(NewAnalysisHost);
            groupBox12.Location = new Point(6, 16);
            groupBox12.Name = "groupBox12";
            groupBox12.Size = new Size(271, 127);
            groupBox12.TabIndex = 34;
            groupBox12.TabStop = false;
            groupBox12.Text = "Saved hosts";
            // 
            // AnalysisServiceHosts
            // 
            AnalysisServiceHosts.FormattingEnabled = true;
            AnalysisServiceHosts.ItemHeight = 15;
            AnalysisServiceHosts.Location = new Point(57, 20);
            AnalysisServiceHosts.Margin = new Padding(3, 2, 3, 2);
            AnalysisServiceHosts.Name = "AnalysisServiceHosts";
            AnalysisServiceHosts.Size = new Size(153, 49);
            AnalysisServiceHosts.TabIndex = 1;
            // 
            // DeleteAnalysisServiceHost
            // 
            DeleteAnalysisServiceHost.FlatStyle = FlatStyle.Flat;
            DeleteAnalysisServiceHost.Location = new Point(136, 96);
            DeleteAnalysisServiceHost.Margin = new Padding(3, 2, 3, 2);
            DeleteAnalysisServiceHost.Name = "DeleteAnalysisServiceHost";
            DeleteAnalysisServiceHost.Size = new Size(74, 25);
            DeleteAnalysisServiceHost.TabIndex = 7;
            DeleteAnalysisServiceHost.Text = "Delete";
            DeleteAnalysisServiceHost.UseVisualStyleBackColor = true;
            DeleteAnalysisServiceHost.Click += DeleteAnalysisServiceHost_Click;
            // 
            // AddAnalysisServiceHost
            // 
            AddAnalysisServiceHost.FlatStyle = FlatStyle.Flat;
            AddAnalysisServiceHost.Location = new Point(57, 96);
            AddAnalysisServiceHost.Margin = new Padding(3, 2, 3, 2);
            AddAnalysisServiceHost.Name = "AddAnalysisServiceHost";
            AddAnalysisServiceHost.Size = new Size(74, 25);
            AddAnalysisServiceHost.TabIndex = 6;
            AddAnalysisServiceHost.Text = "Add";
            AddAnalysisServiceHost.UseVisualStyleBackColor = true;
            AddAnalysisServiceHost.Click += AddAnalysisServiceHost_Click;
            // 
            // NewAnalysisHost
            // 
            NewAnalysisHost.Location = new Point(57, 71);
            NewAnalysisHost.Margin = new Padding(3, 2, 3, 2);
            NewAnalysisHost.Name = "NewAnalysisHost";
            NewAnalysisHost.PlaceholderText = "New host...";
            NewAnalysisHost.Size = new Size(153, 23);
            NewAnalysisHost.TabIndex = 9;
            // 
            // groupBox11
            // 
            groupBox11.Controls.Add(ViewAnalysisServiceLogs);
            groupBox11.Controls.Add(ClearEvaluatedDatabase);
            groupBox11.Controls.Add(StopAnalysisService);
            groupBox11.Controls.Add(AnalysisLogDate);
            groupBox11.Controls.Add(StartAnalysisService);
            groupBox11.Location = new Point(6, 237);
            groupBox11.Name = "groupBox11";
            groupBox11.Size = new Size(565, 172);
            groupBox11.TabIndex = 24;
            groupBox11.TabStop = false;
            groupBox11.Text = "Control";
            // 
            // ViewAnalysisServiceLogs
            // 
            ViewAnalysisServiceLogs.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ViewAnalysisServiceLogs.FlatStyle = FlatStyle.Flat;
            ViewAnalysisServiceLogs.Location = new Point(5, 77);
            ViewAnalysisServiceLogs.Margin = new Padding(3, 2, 3, 2);
            ViewAnalysisServiceLogs.Name = "ViewAnalysisServiceLogs";
            ViewAnalysisServiceLogs.Size = new Size(153, 38);
            ViewAnalysisServiceLogs.TabIndex = 23;
            ViewAnalysisServiceLogs.Text = "View analysis log";
            ViewAnalysisServiceLogs.UseVisualStyleBackColor = true;
            ViewAnalysisServiceLogs.Click += ViewAnalysisServiceLogs_Click;
            // 
            // ClearEvaluatedDatabase
            // 
            ClearEvaluatedDatabase.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            ClearEvaluatedDatabase.FlatStyle = FlatStyle.Flat;
            ClearEvaluatedDatabase.Location = new Point(407, 77);
            ClearEvaluatedDatabase.Margin = new Padding(3, 2, 3, 2);
            ClearEvaluatedDatabase.Name = "ClearEvaluatedDatabase";
            ClearEvaluatedDatabase.Size = new Size(153, 38);
            ClearEvaluatedDatabase.TabIndex = 3;
            ClearEvaluatedDatabase.Text = "Clear results";
            ClearEvaluatedDatabase.UseVisualStyleBackColor = true;
            ClearEvaluatedDatabase.Click += ClearEvaluatedDatabase_Click;
            // 
            // StopAnalysisService
            // 
            StopAnalysisService.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            StopAnalysisService.FlatStyle = FlatStyle.Flat;
            StopAnalysisService.Location = new Point(205, 77);
            StopAnalysisService.Margin = new Padding(3, 2, 3, 2);
            StopAnalysisService.Name = "StopAnalysisService";
            StopAnalysisService.Size = new Size(153, 38);
            StopAnalysisService.TabIndex = 5;
            StopAnalysisService.Text = "Stop analysis service";
            StopAnalysisService.UseVisualStyleBackColor = true;
            StopAnalysisService.Click += StopAnalysisService_Click;
            // 
            // AnalysisLogDate
            // 
            AnalysisLogDate.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            AnalysisLogDate.Location = new Point(5, 50);
            AnalysisLogDate.Margin = new Padding(3, 2, 3, 2);
            AnalysisLogDate.Name = "AnalysisLogDate";
            AnalysisLogDate.Size = new Size(153, 23);
            AnalysisLogDate.TabIndex = 18;
            // 
            // StartAnalysisService
            // 
            StartAnalysisService.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            StartAnalysisService.FlatStyle = FlatStyle.Flat;
            StartAnalysisService.Location = new Point(205, 77);
            StartAnalysisService.Margin = new Padding(3, 2, 3, 2);
            StartAnalysisService.Name = "StartAnalysisService";
            StartAnalysisService.Size = new Size(153, 38);
            StartAnalysisService.TabIndex = 1;
            StartAnalysisService.Text = "Start analysis service";
            StartAnalysisService.UseVisualStyleBackColor = true;
            StartAnalysisService.Click += StartAnalysisService_Click;
            // 
            // groupBox2
            // 
            groupBox2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            groupBox2.Controls.Add(AnalysisServiceProgressBar);
            groupBox2.Controls.Add(RefreshAnalysisServiceInfo);
            groupBox2.Controls.Add(AnalysisEvaluated);
            groupBox2.Controls.Add(AnalysisWarnings);
            groupBox2.Controls.Add(label26);
            groupBox2.Controls.Add(AnalysisErrors);
            groupBox2.Controls.Add(label25);
            groupBox2.Controls.Add(AnalysisUptime);
            groupBox2.Controls.Add(label24);
            groupBox2.Controls.Add(AnalysisState);
            groupBox2.Controls.Add(label23);
            groupBox2.Controls.Add(AnalysisConnection);
            groupBox2.Controls.Add(label22);
            groupBox2.Controls.Add(label21);
            groupBox2.Controls.Add(AnalysisProgressBarRed);
            groupBox2.Location = new Point(6, 5);
            groupBox2.Margin = new Padding(3, 2, 3, 2);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(3, 2, 3, 2);
            groupBox2.Size = new Size(279, 227);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "Analysis service info";
            // 
            // AnalysisServiceProgressBar
            // 
            AnalysisServiceProgressBar.Location = new Point(5, 24);
            AnalysisServiceProgressBar.Margin = new Padding(3, 2, 3, 2);
            AnalysisServiceProgressBar.Name = "AnalysisServiceProgressBar";
            AnalysisServiceProgressBar.Size = new Size(194, 13);
            AnalysisServiceProgressBar.TabIndex = 30;
            // 
            // RefreshAnalysisServiceInfo
            // 
            RefreshAnalysisServiceInfo.FlatStyle = FlatStyle.Flat;
            RefreshAnalysisServiceInfo.Location = new Point(205, 20);
            RefreshAnalysisServiceInfo.Margin = new Padding(3, 2, 3, 2);
            RefreshAnalysisServiceInfo.Name = "RefreshAnalysisServiceInfo";
            RefreshAnalysisServiceInfo.Size = new Size(66, 26);
            RefreshAnalysisServiceInfo.TabIndex = 16;
            RefreshAnalysisServiceInfo.Text = "Refresh";
            RefreshAnalysisServiceInfo.UseVisualStyleBackColor = true;
            RefreshAnalysisServiceInfo.Click += RefreshAnalysisServiceInfo_Click;
            // 
            // AnalysisEvaluated
            // 
            AnalysisEvaluated.AutoSize = true;
            AnalysisEvaluated.Location = new Point(144, 169);
            AnalysisEvaluated.Margin = new Padding(4);
            AnalysisEvaluated.Name = "AnalysisEvaluated";
            AnalysisEvaluated.Size = new Size(36, 15);
            AnalysisEvaluated.TabIndex = 29;
            AnalysisEvaluated.Text = "None";
            // 
            // AnalysisWarnings
            // 
            AnalysisWarnings.AutoSize = true;
            AnalysisWarnings.Location = new Point(144, 146);
            AnalysisWarnings.Margin = new Padding(4);
            AnalysisWarnings.Name = "AnalysisWarnings";
            AnalysisWarnings.Size = new Size(36, 15);
            AnalysisWarnings.TabIndex = 28;
            AnalysisWarnings.Text = "None";
            // 
            // label26
            // 
            label26.AutoSize = true;
            label26.Location = new Point(59, 56);
            label26.Margin = new Padding(4);
            label26.Name = "label26";
            label26.Size = new Size(72, 15);
            label26.TabIndex = 15;
            label26.Text = "Connection:";
            // 
            // AnalysisErrors
            // 
            AnalysisErrors.AutoSize = true;
            AnalysisErrors.Location = new Point(144, 124);
            AnalysisErrors.Margin = new Padding(4);
            AnalysisErrors.Name = "AnalysisErrors";
            AnalysisErrors.Size = new Size(36, 15);
            AnalysisErrors.TabIndex = 27;
            AnalysisErrors.Text = "None";
            // 
            // label25
            // 
            label25.AutoSize = true;
            label25.Location = new Point(59, 79);
            label25.Margin = new Padding(4);
            label25.Name = "label25";
            label25.Size = new Size(36, 15);
            label25.TabIndex = 16;
            label25.Text = "State:";
            // 
            // AnalysisUptime
            // 
            AnalysisUptime.AutoSize = true;
            AnalysisUptime.Location = new Point(144, 101);
            AnalysisUptime.Margin = new Padding(4);
            AnalysisUptime.Name = "AnalysisUptime";
            AnalysisUptime.Size = new Size(36, 15);
            AnalysisUptime.TabIndex = 26;
            AnalysisUptime.Text = "None";
            // 
            // label24
            // 
            label24.AutoSize = true;
            label24.Location = new Point(59, 101);
            label24.Margin = new Padding(4);
            label24.Name = "label24";
            label24.Size = new Size(49, 15);
            label24.TabIndex = 17;
            label24.Text = "Uptime:";
            // 
            // AnalysisState
            // 
            AnalysisState.AutoSize = true;
            AnalysisState.Location = new Point(144, 79);
            AnalysisState.Margin = new Padding(4);
            AnalysisState.Name = "AnalysisState";
            AnalysisState.Size = new Size(36, 15);
            AnalysisState.TabIndex = 25;
            AnalysisState.Text = "None";
            // 
            // label23
            // 
            label23.AutoSize = true;
            label23.Location = new Point(59, 124);
            label23.Margin = new Padding(4);
            label23.Name = "label23";
            label23.Size = new Size(40, 15);
            label23.TabIndex = 18;
            label23.Text = "Errors:";
            // 
            // AnalysisConnection
            // 
            AnalysisConnection.AutoSize = true;
            AnalysisConnection.Location = new Point(144, 56);
            AnalysisConnection.Margin = new Padding(4);
            AnalysisConnection.Name = "AnalysisConnection";
            AnalysisConnection.Size = new Size(36, 15);
            AnalysisConnection.TabIndex = 24;
            AnalysisConnection.Text = "None";
            // 
            // label22
            // 
            label22.AutoSize = true;
            label22.Location = new Point(59, 146);
            label22.Margin = new Padding(4);
            label22.Name = "label22";
            label22.Size = new Size(60, 15);
            label22.TabIndex = 19;
            label22.Text = "Warnings:";
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.Location = new Point(59, 169);
            label21.Margin = new Padding(4);
            label21.Name = "label21";
            label21.Size = new Size(61, 15);
            label21.TabIndex = 20;
            label21.Text = "Evaluated:";
            // 
            // AnalysisProgressBarRed
            // 
            AnalysisProgressBarRed.BackColor = Color.Red;
            AnalysisProgressBarRed.Location = new Point(5, 24);
            AnalysisProgressBarRed.Margin = new Padding(3, 2, 3, 2);
            AnalysisProgressBarRed.Name = "AnalysisProgressBarRed";
            AnalysisProgressBarRed.Size = new Size(194, 13);
            AnalysisProgressBarRed.TabIndex = 31;
            // 
            // Tabs
            // 
            Tabs.Controls.Add(MainTab);
            Tabs.Controls.Add(ConfigureTab);
            Tabs.Controls.Add(VkCollectorTab);
            Tabs.Controls.Add(ReportsTab);
            Tabs.Dock = DockStyle.Fill;
            Tabs.ItemSize = new Size(100, 40);
            Tabs.Location = new Point(0, 0);
            Tabs.Margin = new Padding(3, 2, 3, 2);
            Tabs.Name = "Tabs";
            Tabs.SelectedIndex = 0;
            Tabs.Size = new Size(598, 496);
            Tabs.SizeMode = TabSizeMode.Fixed;
            Tabs.TabIndex = 0;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(598, 496);
            Controls.Add(Tabs);
            Margin = new Padding(3, 2, 3, 2);
            MinimumSize = new Size(614, 535);
            Name = "MainForm";
            FormClosing += MainForm_FormClosing;
            VkCollectorTab.ResumeLayout(false);
            groupBox8.ResumeLayout(false);
            groupBox8.PerformLayout();
            ReportsTab.ResumeLayout(false);
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            ConfigureTab.ResumeLayout(false);
            groupBox6.ResumeLayout(false);
            groupBox6.PerformLayout();
            groupBox5.ResumeLayout(false);
            groupBox5.PerformLayout();
            MainTab.ResumeLayout(false);
            ServicesTabs.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            groupBox7.ResumeLayout(false);
            groupBox13.ResumeLayout(false);
            groupBox9.ResumeLayout(false);
            groupBox9.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            tabPage2.ResumeLayout(false);
            groupBox10.ResumeLayout(false);
            groupBox14.ResumeLayout(false);
            groupBox12.ResumeLayout(false);
            groupBox12.PerformLayout();
            groupBox11.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            Tabs.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private SaveFileDialog SaveFileDialog;
        private TabPage VkCollectorTab;
        private Button LocalConfiguration;
        private Button SaveVkSettings;
        private Button LoadVkSettings;
        private GroupBox groupBox8;
        private TextBox NewCommunity;
        private Button DeleteCommunity;
        private Button AddCommunity;
        private Label label12;
        private ListBox Communities;
        public TextBox ServiceAccessKey;
        private Label label11;
        public TextBox SecureKey;
        private Label label10;
        public TextBox ApplicationId;
        private Label label9;
        private TabPage ReportsTab;
        private Button OpenReport;
        private GroupBox groupBox4;
        private TextBox CommunityId;
        private Label label16;
        private TextBox PostId;
        private Label label15;
        private TextBox AuthorId;
        private DateTimePicker ToDate;
        private Label label14;
        private DateTimePicker FromDate;
        private CheckBox SelectedDateCheckBox;
        private CheckBox LastMonthCheckBox;
        private CheckBox LastWeekCheckBox;
        private CheckBox Last3DaysCheckBox;
        private CheckBox TodayCheckBox;
        private Label label13;
        private TabPage ConfigureTab;
        private Button LoadConfiguration;
        private Button SaveConfiguration;
        private Button SetLocalConfig;
        private GroupBox groupBox6;
        private TextBox ObserveDelay;
        private Label label8;
        private GroupBox groupBox5;
        private TextBox PostQueueSize;
        private Label label31;
        private TextBox ScanCommentsDelay;
        private Label label30;
        private Label label29;
        private TextBox ScanPostDelay;
        private TabPage MainTab;
        private Button ClearEvaluatedDatabase;
        private Button ClearCommentsDatabase;
        private Button StartAnalysisService;
        private Button StartCollectionService;
        private Button StopAnalysisService;
        private Button StopCollectionService;
        private GroupBox groupBox2;
        private DateTimePicker AnalysisLogDate;
        private Button RefreshAnalysisServiceInfo;
        public Label AnalysisEvaluated;
        public Label AnalysisWarnings;
        private Label label26;
        public Label AnalysisErrors;
        private Label label25;
        public Label AnalysisUptime;
        private Label label24;
        public Label AnalysisState;
        private Label label23;
        public Label AnalysisConnection;
        private Label label22;
        private Button ViewAnalysisServiceLogs;
        private Label label21;
        private GroupBox groupBox1;
        private DateTimePicker CollectionLogDate;
        private Button RefreshCollectionServiceInfo;
        public Label CollectionCollected;
        public Label CollectionWarnings;
        public Label CollectionErrors;
        public Label CollectionUptime;
        public Label CollectionState;
        public Label CollectionConnection;
        private Button ViewCollectionServiceLogs;
        private Label label6;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private TabControl Tabs;
        private Label label17;
        private ProgressBar CollectionServiceProgressBar;
        private ProgressBar AnalysisServiceProgressBar;
        private Panel AnalysisProgressBarRed;
        private Panel CollectionProgressBarRed;
        private TabControl ServicesTabs;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private GroupBox groupBox3;
        private GroupBox groupBox7;
        private Button ApplyNewCollectionServiceEndpoint;
        public ComboBox CollectionServiceEndpoint;
        public ListBox CollectionServiceHosts;
        private TextBox NewCollectionHost;
        private Button DeleteCollectionServiceHost;
        private Button AddCollectionServiceHost;
        private GroupBox groupBox9;
        private GroupBox groupBox11;
        private GroupBox groupBox10;
        private Button ApplyAnalysisServiceEndpoint;
        public ComboBox AnalysisServiceEndpoint;
        public ListBox AnalysisServiceHosts;
        private TextBox NewAnalysisHost;
        private Button AddAnalysisServiceHost;
        private Button DeleteAnalysisServiceHost;
        private GroupBox groupBox12;
        private GroupBox groupBox13;
        private GroupBox groupBox14;
    }
}