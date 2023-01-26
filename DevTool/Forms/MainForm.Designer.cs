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
            this.SaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.VkCollectorTab = new System.Windows.Forms.TabPage();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.label17 = new System.Windows.Forms.Label();
            this.LocalConfiguration = new System.Windows.Forms.Button();
            this.NewCommunity = new System.Windows.Forms.TextBox();
            this.SaveVkSettings = new System.Windows.Forms.Button();
            this.ServiceAccessKey = new System.Windows.Forms.TextBox();
            this.LoadVkSettings = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.DeleteCommunity = new System.Windows.Forms.Button();
            this.SecureKey = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.AddCommunity = new System.Windows.Forms.Button();
            this.ApplicationId = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.Communities = new System.Windows.Forms.ListBox();
            this.ReportsTab = new System.Windows.Forms.TabPage();
            this.OpenReport = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.CommunityId = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.PostId = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.AuthorId = new System.Windows.Forms.TextBox();
            this.ToDate = new System.Windows.Forms.DateTimePicker();
            this.label14 = new System.Windows.Forms.Label();
            this.FromDate = new System.Windows.Forms.DateTimePicker();
            this.SelectedDateCheckBox = new System.Windows.Forms.CheckBox();
            this.LastMonthCheckBox = new System.Windows.Forms.CheckBox();
            this.LastWeekCheckBox = new System.Windows.Forms.CheckBox();
            this.Last3DaysCheckBox = new System.Windows.Forms.CheckBox();
            this.TodayCheckBox = new System.Windows.Forms.CheckBox();
            this.label13 = new System.Windows.Forms.Label();
            this.ConfigureTab = new System.Windows.Forms.TabPage();
            this.LoadConfiguration = new System.Windows.Forms.Button();
            this.SaveConfiguration = new System.Windows.Forms.Button();
            this.SetLocalConfig = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.ApplyAnalysisServiceEndpoint = new System.Windows.Forms.Button();
            this.label20 = new System.Windows.Forms.Label();
            this.AnalysisServiceEndpoint = new System.Windows.Forms.ComboBox();
            this.AnalysisServiceHosts = new System.Windows.Forms.ListBox();
            this.NewAnalysisHost = new System.Windows.Forms.TextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.AddAnalysisServiceHost = new System.Windows.Forms.Button();
            this.DeleteAnalysisServiceHost = new System.Windows.Forms.Button();
            this.ObserveDelay = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.ApplyNewCollectionServiceEndpoint = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.CollectionServiceEndpoint = new System.Windows.Forms.ComboBox();
            this.CollectionServiceHosts = new System.Windows.Forms.ListBox();
            this.NewCollectionHost = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.DeleteCollectionServiceHost = new System.Windows.Forms.Button();
            this.AddCollectionServiceHost = new System.Windows.Forms.Button();
            this.PostQueueSize = new System.Windows.Forms.TextBox();
            this.label31 = new System.Windows.Forms.Label();
            this.ScanCommentsDelay = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.ScanPostDelay = new System.Windows.Forms.TextBox();
            this.MainTab = new System.Windows.Forms.TabPage();
            this.AnalysisLogDate = new System.Windows.Forms.DateTimePicker();
            this.CollectionLogDate = new System.Windows.Forms.DateTimePicker();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ClearEvaluatedDatabase = new System.Windows.Forms.Button();
            this.ClearCommentsDatabase = new System.Windows.Forms.Button();
            this.StartAll = new System.Windows.Forms.Button();
            this.StartAnalysisService = new System.Windows.Forms.Button();
            this.StartCollectionService = new System.Windows.Forms.Button();
            this.StopAll = new System.Windows.Forms.Button();
            this.StopAnalysisService = new System.Windows.Forms.Button();
            this.StopCollectionService = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.AnalysisServiceProgressBar = new System.Windows.Forms.ProgressBar();
            this.RefreshAnalysisServiceInfo = new System.Windows.Forms.Button();
            this.AnalysisEvaluated = new System.Windows.Forms.Label();
            this.AnalysisWarnings = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.AnalysisErrors = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.AnalysisUptime = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.AnalysisState = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.AnalysisConnection = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.ViewAnalysisServiceLogs = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.CollectionServiceProgressBar = new System.Windows.Forms.ProgressBar();
            this.RefreshCollectionServiceInfo = new System.Windows.Forms.Button();
            this.CollectionCollected = new System.Windows.Forms.Label();
            this.CollectionWarnings = new System.Windows.Forms.Label();
            this.CollectionErrors = new System.Windows.Forms.Label();
            this.CollectionUptime = new System.Windows.Forms.Label();
            this.CollectionState = new System.Windows.Forms.Label();
            this.CollectionConnection = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ViewCollectionServiceLogs = new System.Windows.Forms.Button();
            this.Tabs = new System.Windows.Forms.TabControl();
            this.CollectionProgressBarRed = new System.Windows.Forms.Panel();
            this.AnalysisProgressBarRed = new System.Windows.Forms.Panel();
            this.VkCollectorTab.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.ReportsTab.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.ConfigureTab.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.MainTab.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.Tabs.SuspendLayout();
            this.SuspendLayout();
            // 
            // SaveFileDialog
            // 
            this.SaveFileDialog.DefaultExt = "xlsx";
            this.SaveFileDialog.FileName = "EvaluatedCommentsReport.xlsx";
            // 
            // VkCollectorTab
            // 
            this.VkCollectorTab.Controls.Add(this.groupBox8);
            this.VkCollectorTab.Location = new System.Drawing.Point(4, 44);
            this.VkCollectorTab.Name = "VkCollectorTab";
            this.VkCollectorTab.Padding = new System.Windows.Forms.Padding(3);
            this.VkCollectorTab.Size = new System.Drawing.Size(674, 605);
            this.VkCollectorTab.TabIndex = 4;
            this.VkCollectorTab.Text = "Vk collector";
            this.VkCollectorTab.UseVisualStyleBackColor = true;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.label17);
            this.groupBox8.Controls.Add(this.LocalConfiguration);
            this.groupBox8.Controls.Add(this.NewCommunity);
            this.groupBox8.Controls.Add(this.SaveVkSettings);
            this.groupBox8.Controls.Add(this.ServiceAccessKey);
            this.groupBox8.Controls.Add(this.LoadVkSettings);
            this.groupBox8.Controls.Add(this.label11);
            this.groupBox8.Controls.Add(this.DeleteCommunity);
            this.groupBox8.Controls.Add(this.SecureKey);
            this.groupBox8.Controls.Add(this.label10);
            this.groupBox8.Controls.Add(this.AddCommunity);
            this.groupBox8.Controls.Add(this.ApplicationId);
            this.groupBox8.Controls.Add(this.label9);
            this.groupBox8.Controls.Add(this.label12);
            this.groupBox8.Controls.Add(this.Communities);
            this.groupBox8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox8.Location = new System.Drawing.Point(3, 3);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(668, 599);
            this.groupBox8.TabIndex = 0;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Settings";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(15, 34);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(469, 80);
            this.label17.TabIndex = 20;
            this.label17.Text = resources.GetString("label17.Text");
            // 
            // LocalConfiguration
            // 
            this.LocalConfiguration.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LocalConfiguration.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LocalConfiguration.Location = new System.Drawing.Point(6, 543);
            this.LocalConfiguration.Name = "LocalConfiguration";
            this.LocalConfiguration.Size = new System.Drawing.Size(165, 50);
            this.LocalConfiguration.TabIndex = 19;
            this.LocalConfiguration.Text = "Default configuration";
            this.LocalConfiguration.UseVisualStyleBackColor = true;
            this.LocalConfiguration.Click += new System.EventHandler(this.LocalConfiguration_Click);
            // 
            // NewCommunity
            // 
            this.NewCommunity.Location = new System.Drawing.Point(164, 332);
            this.NewCommunity.Name = "NewCommunity";
            this.NewCommunity.Size = new System.Drawing.Size(183, 27);
            this.NewCommunity.TabIndex = 16;
            // 
            // SaveVkSettings
            // 
            this.SaveVkSettings.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.SaveVkSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SaveVkSettings.Location = new System.Drawing.Point(255, 543);
            this.SaveVkSettings.Name = "SaveVkSettings";
            this.SaveVkSettings.Size = new System.Drawing.Size(165, 50);
            this.SaveVkSettings.TabIndex = 18;
            this.SaveVkSettings.Text = "Save configuration";
            this.SaveVkSettings.UseVisualStyleBackColor = true;
            this.SaveVkSettings.Click += new System.EventHandler(this.SaveVkSettings_Click);
            // 
            // ServiceAccessKey
            // 
            this.ServiceAccessKey.Location = new System.Drawing.Point(164, 194);
            this.ServiceAccessKey.Margin = new System.Windows.Forms.Padding(5);
            this.ServiceAccessKey.Name = "ServiceAccessKey";
            this.ServiceAccessKey.Size = new System.Drawing.Size(479, 27);
            this.ServiceAccessKey.TabIndex = 11;
            // 
            // LoadVkSettings
            // 
            this.LoadVkSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.LoadVkSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LoadVkSettings.Location = new System.Drawing.Point(499, 543);
            this.LoadVkSettings.Name = "LoadVkSettings";
            this.LoadVkSettings.Size = new System.Drawing.Size(165, 50);
            this.LoadVkSettings.TabIndex = 17;
            this.LoadVkSettings.Text = "Upload configuration";
            this.LoadVkSettings.UseVisualStyleBackColor = true;
            this.LoadVkSettings.Click += new System.EventHandler(this.LoadVkSettings_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(18, 197);
            this.label11.Margin = new System.Windows.Forms.Padding(5);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(131, 20);
            this.label11.TabIndex = 10;
            this.label11.Text = "Service access key:";
            // 
            // DeleteCommunity
            // 
            this.DeleteCommunity.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DeleteCommunity.Location = new System.Drawing.Point(256, 357);
            this.DeleteCommunity.Name = "DeleteCommunity";
            this.DeleteCommunity.Size = new System.Drawing.Size(91, 29);
            this.DeleteCommunity.TabIndex = 15;
            this.DeleteCommunity.Text = "Delete";
            this.DeleteCommunity.UseVisualStyleBackColor = true;
            this.DeleteCommunity.Click += new System.EventHandler(this.DeleteCommunity_Click);
            // 
            // SecureKey
            // 
            this.SecureKey.Location = new System.Drawing.Point(164, 164);
            this.SecureKey.Margin = new System.Windows.Forms.Padding(5);
            this.SecureKey.Name = "SecureKey";
            this.SecureKey.Size = new System.Drawing.Size(479, 27);
            this.SecureKey.TabIndex = 9;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(67, 167);
            this.label10.Margin = new System.Windows.Forms.Padding(5);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(82, 20);
            this.label10.TabIndex = 8;
            this.label10.Text = "Secure key:";
            // 
            // AddCommunity
            // 
            this.AddCommunity.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddCommunity.Location = new System.Drawing.Point(164, 357);
            this.AddCommunity.Name = "AddCommunity";
            this.AddCommunity.Size = new System.Drawing.Size(92, 29);
            this.AddCommunity.TabIndex = 14;
            this.AddCommunity.Text = "Add";
            this.AddCommunity.UseVisualStyleBackColor = true;
            this.AddCommunity.Click += new System.EventHandler(this.AddCommunity_Click);
            // 
            // ApplicationId
            // 
            this.ApplicationId.Location = new System.Drawing.Point(164, 134);
            this.ApplicationId.Margin = new System.Windows.Forms.Padding(5);
            this.ApplicationId.Name = "ApplicationId";
            this.ApplicationId.Size = new System.Drawing.Size(479, 27);
            this.ApplicationId.TabIndex = 7;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(41, 137);
            this.label9.Margin = new System.Windows.Forms.Padding(5);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(108, 20);
            this.label9.TabIndex = 6;
            this.label9.Text = "Application ID:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(33, 229);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(116, 20);
            this.label12.TabIndex = 13;
            this.label12.Text = "Communities id:";
            // 
            // Communities
            // 
            this.Communities.FormattingEnabled = true;
            this.Communities.ItemHeight = 20;
            this.Communities.Location = new System.Drawing.Point(164, 229);
            this.Communities.Name = "Communities";
            this.Communities.Size = new System.Drawing.Size(183, 104);
            this.Communities.TabIndex = 12;
            // 
            // ReportsTab
            // 
            this.ReportsTab.Controls.Add(this.OpenReport);
            this.ReportsTab.Controls.Add(this.groupBox4);
            this.ReportsTab.Location = new System.Drawing.Point(4, 44);
            this.ReportsTab.Name = "ReportsTab";
            this.ReportsTab.Padding = new System.Windows.Forms.Padding(3);
            this.ReportsTab.Size = new System.Drawing.Size(674, 605);
            this.ReportsTab.TabIndex = 2;
            this.ReportsTab.Text = "Reports";
            this.ReportsTab.UseVisualStyleBackColor = true;
            // 
            // OpenReport
            // 
            this.OpenReport.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.OpenReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OpenReport.Location = new System.Drawing.Point(242, 538);
            this.OpenReport.Name = "OpenReport";
            this.OpenReport.Size = new System.Drawing.Size(175, 50);
            this.OpenReport.TabIndex = 1;
            this.OpenReport.Text = "Open report";
            this.OpenReport.UseVisualStyleBackColor = true;
            this.OpenReport.Click += new System.EventHandler(this.OpenReport_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.CommunityId);
            this.groupBox4.Controls.Add(this.label16);
            this.groupBox4.Controls.Add(this.PostId);
            this.groupBox4.Controls.Add(this.label15);
            this.groupBox4.Controls.Add(this.AuthorId);
            this.groupBox4.Controls.Add(this.ToDate);
            this.groupBox4.Controls.Add(this.label14);
            this.groupBox4.Controls.Add(this.FromDate);
            this.groupBox4.Controls.Add(this.SelectedDateCheckBox);
            this.groupBox4.Controls.Add(this.LastMonthCheckBox);
            this.groupBox4.Controls.Add(this.LastWeekCheckBox);
            this.groupBox4.Controls.Add(this.Last3DaysCheckBox);
            this.groupBox4.Controls.Add(this.TodayCheckBox);
            this.groupBox4.Controls.Add(this.label13);
            this.groupBox4.Location = new System.Drawing.Point(6, 6);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(411, 229);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Filter";
            // 
            // CommunityId
            // 
            this.CommunityId.Location = new System.Drawing.Point(262, 91);
            this.CommunityId.Name = "CommunityId";
            this.CommunityId.Size = new System.Drawing.Size(125, 27);
            this.CommunityId.TabIndex = 13;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(151, 93);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(105, 20);
            this.label16.TabIndex = 12;
            this.label16.Text = "Community id:";
            // 
            // PostId
            // 
            this.PostId.Location = new System.Drawing.Point(262, 60);
            this.PostId.Name = "PostId";
            this.PostId.Size = new System.Drawing.Size(125, 27);
            this.PostId.TabIndex = 11;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(200, 63);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(56, 20);
            this.label15.TabIndex = 10;
            this.label15.Text = "Post id:";
            // 
            // AuthorId
            // 
            this.AuthorId.Location = new System.Drawing.Point(262, 29);
            this.AuthorId.Name = "AuthorId";
            this.AuthorId.Size = new System.Drawing.Size(125, 27);
            this.AuthorId.TabIndex = 9;
            // 
            // ToDate
            // 
            this.ToDate.Enabled = false;
            this.ToDate.Location = new System.Drawing.Point(221, 181);
            this.ToDate.Name = "ToDate";
            this.ToDate.Size = new System.Drawing.Size(166, 27);
            this.ToDate.TabIndex = 8;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(200, 186);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(15, 20);
            this.label14.TabIndex = 7;
            this.label14.Text = "-";
            // 
            // FromDate
            // 
            this.FromDate.Enabled = false;
            this.FromDate.Location = new System.Drawing.Point(28, 181);
            this.FromDate.Name = "FromDate";
            this.FromDate.Size = new System.Drawing.Size(166, 27);
            this.FromDate.TabIndex = 6;
            // 
            // SelectedDateCheckBox
            // 
            this.SelectedDateCheckBox.AutoSize = true;
            this.SelectedDateCheckBox.Location = new System.Drawing.Point(28, 151);
            this.SelectedDateCheckBox.Name = "SelectedDateCheckBox";
            this.SelectedDateCheckBox.Size = new System.Drawing.Size(125, 24);
            this.SelectedDateCheckBox.TabIndex = 5;
            this.SelectedDateCheckBox.Text = "Selected date:";
            this.SelectedDateCheckBox.UseVisualStyleBackColor = true;
            this.SelectedDateCheckBox.CheckedChanged += new System.EventHandler(this.SelectedDateCheckBox_CheckedChanged);
            // 
            // LastMonthCheckBox
            // 
            this.LastMonthCheckBox.AutoSize = true;
            this.LastMonthCheckBox.Location = new System.Drawing.Point(28, 121);
            this.LastMonthCheckBox.Name = "LastMonthCheckBox";
            this.LastMonthCheckBox.Size = new System.Drawing.Size(104, 24);
            this.LastMonthCheckBox.TabIndex = 4;
            this.LastMonthCheckBox.Text = "Last month";
            this.LastMonthCheckBox.UseVisualStyleBackColor = true;
            this.LastMonthCheckBox.CheckedChanged += new System.EventHandler(this.LastMonthCheckBox_CheckedChanged);
            // 
            // LastWeekCheckBox
            // 
            this.LastWeekCheckBox.AutoSize = true;
            this.LastWeekCheckBox.Location = new System.Drawing.Point(28, 91);
            this.LastWeekCheckBox.Name = "LastWeekCheckBox";
            this.LastWeekCheckBox.Size = new System.Drawing.Size(95, 24);
            this.LastWeekCheckBox.TabIndex = 3;
            this.LastWeekCheckBox.Text = "Last week";
            this.LastWeekCheckBox.UseVisualStyleBackColor = true;
            this.LastWeekCheckBox.CheckedChanged += new System.EventHandler(this.LastWeekCheckBox_CheckedChanged);
            // 
            // Last3DaysCheckBox
            // 
            this.Last3DaysCheckBox.AutoSize = true;
            this.Last3DaysCheckBox.Location = new System.Drawing.Point(28, 61);
            this.Last3DaysCheckBox.Name = "Last3DaysCheckBox";
            this.Last3DaysCheckBox.Size = new System.Drawing.Size(103, 24);
            this.Last3DaysCheckBox.TabIndex = 2;
            this.Last3DaysCheckBox.Text = "Last 3 days";
            this.Last3DaysCheckBox.UseVisualStyleBackColor = true;
            this.Last3DaysCheckBox.CheckedChanged += new System.EventHandler(this.Last3DaysCheckBox_CheckedChanged);
            // 
            // TodayCheckBox
            // 
            this.TodayCheckBox.AutoSize = true;
            this.TodayCheckBox.Checked = true;
            this.TodayCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.TodayCheckBox.Location = new System.Drawing.Point(28, 31);
            this.TodayCheckBox.Name = "TodayCheckBox";
            this.TodayCheckBox.Size = new System.Drawing.Size(71, 24);
            this.TodayCheckBox.TabIndex = 1;
            this.TodayCheckBox.Text = "Today";
            this.TodayCheckBox.UseVisualStyleBackColor = true;
            this.TodayCheckBox.CheckedChanged += new System.EventHandler(this.TodayCheckBox_CheckedChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(182, 32);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(74, 20);
            this.label13.TabIndex = 0;
            this.label13.Text = "Author id:";
            // 
            // ConfigureTab
            // 
            this.ConfigureTab.Controls.Add(this.LoadConfiguration);
            this.ConfigureTab.Controls.Add(this.SaveConfiguration);
            this.ConfigureTab.Controls.Add(this.SetLocalConfig);
            this.ConfigureTab.Controls.Add(this.groupBox6);
            this.ConfigureTab.Controls.Add(this.groupBox5);
            this.ConfigureTab.Location = new System.Drawing.Point(4, 44);
            this.ConfigureTab.Name = "ConfigureTab";
            this.ConfigureTab.Padding = new System.Windows.Forms.Padding(3);
            this.ConfigureTab.Size = new System.Drawing.Size(674, 605);
            this.ConfigureTab.TabIndex = 1;
            this.ConfigureTab.Text = "Configure";
            this.ConfigureTab.UseVisualStyleBackColor = true;
            // 
            // LoadConfiguration
            // 
            this.LoadConfiguration.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.LoadConfiguration.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LoadConfiguration.Location = new System.Drawing.Point(501, 550);
            this.LoadConfiguration.Name = "LoadConfiguration";
            this.LoadConfiguration.Size = new System.Drawing.Size(165, 50);
            this.LoadConfiguration.TabIndex = 0;
            this.LoadConfiguration.Text = "Upload configuration";
            this.LoadConfiguration.UseVisualStyleBackColor = true;
            this.LoadConfiguration.Click += new System.EventHandler(this.LoadConfiguration_Click);
            // 
            // SaveConfiguration
            // 
            this.SaveConfiguration.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.SaveConfiguration.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SaveConfiguration.Location = new System.Drawing.Point(257, 550);
            this.SaveConfiguration.Name = "SaveConfiguration";
            this.SaveConfiguration.Size = new System.Drawing.Size(165, 50);
            this.SaveConfiguration.TabIndex = 1;
            this.SaveConfiguration.Text = "Save configuration";
            this.SaveConfiguration.UseVisualStyleBackColor = true;
            this.SaveConfiguration.Click += new System.EventHandler(this.SaveConfiguration_Click);
            // 
            // SetLocalConfig
            // 
            this.SetLocalConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SetLocalConfig.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SetLocalConfig.Location = new System.Drawing.Point(8, 550);
            this.SetLocalConfig.Name = "SetLocalConfig";
            this.SetLocalConfig.Size = new System.Drawing.Size(165, 50);
            this.SetLocalConfig.TabIndex = 20;
            this.SetLocalConfig.Text = "Default configuration";
            this.SetLocalConfig.UseVisualStyleBackColor = true;
            this.SetLocalConfig.Click += new System.EventHandler(this.SetLocalConfig_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox6.Controls.Add(this.groupBox10);
            this.groupBox6.Controls.Add(this.ObserveDelay);
            this.groupBox6.Controls.Add(this.label8);
            this.groupBox6.Location = new System.Drawing.Point(356, 6);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(310, 539);
            this.groupBox6.TabIndex = 2;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Analysis service configuration";
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.ApplyAnalysisServiceEndpoint);
            this.groupBox10.Controls.Add(this.label20);
            this.groupBox10.Controls.Add(this.AnalysisServiceEndpoint);
            this.groupBox10.Controls.Add(this.AnalysisServiceHosts);
            this.groupBox10.Controls.Add(this.NewAnalysisHost);
            this.groupBox10.Controls.Add(this.label28);
            this.groupBox10.Controls.Add(this.AddAnalysisServiceHost);
            this.groupBox10.Controls.Add(this.DeleteAnalysisServiceHost);
            this.groupBox10.Location = new System.Drawing.Point(6, 26);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(298, 222);
            this.groupBox10.TabIndex = 9;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "Host configuration";
            // 
            // ApplyAnalysisServiceEndpoint
            // 
            this.ApplyAnalysisServiceEndpoint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ApplyAnalysisServiceEndpoint.Location = new System.Drawing.Point(66, 179);
            this.ApplyAnalysisServiceEndpoint.Name = "ApplyAnalysisServiceEndpoint";
            this.ApplyAnalysisServiceEndpoint.Size = new System.Drawing.Size(182, 28);
            this.ApplyAnalysisServiceEndpoint.TabIndex = 33;
            this.ApplyAnalysisServiceEndpoint.Text = "Reconnect";
            this.ApplyAnalysisServiceEndpoint.UseVisualStyleBackColor = true;
            this.ApplyAnalysisServiceEndpoint.Click += new System.EventHandler(this.ApplyAnalysisServiceEndpoint_Click_1);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(15, 154);
            this.label20.Margin = new System.Windows.Forms.Padding(5);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(43, 20);
            this.label20.TabIndex = 32;
            this.label20.Text = "Host:";
            // 
            // AnalysisServiceEndpoint
            // 
            this.AnalysisServiceEndpoint.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AnalysisServiceEndpoint.FormattingEnabled = true;
            this.AnalysisServiceEndpoint.Location = new System.Drawing.Point(66, 151);
            this.AnalysisServiceEndpoint.Name = "AnalysisServiceEndpoint";
            this.AnalysisServiceEndpoint.Size = new System.Drawing.Size(182, 28);
            this.AnalysisServiceEndpoint.TabIndex = 31;
            // 
            // AnalysisServiceHosts
            // 
            this.AnalysisServiceHosts.FormattingEnabled = true;
            this.AnalysisServiceHosts.ItemHeight = 20;
            this.AnalysisServiceHosts.Location = new System.Drawing.Point(66, 30);
            this.AnalysisServiceHosts.Name = "AnalysisServiceHosts";
            this.AnalysisServiceHosts.Size = new System.Drawing.Size(182, 64);
            this.AnalysisServiceHosts.TabIndex = 1;
            // 
            // NewAnalysisHost
            // 
            this.NewAnalysisHost.Location = new System.Drawing.Point(66, 96);
            this.NewAnalysisHost.Name = "NewAnalysisHost";
            this.NewAnalysisHost.PlaceholderText = "New host...";
            this.NewAnalysisHost.Size = new System.Drawing.Size(182, 27);
            this.NewAnalysisHost.TabIndex = 9;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(15, 29);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(53, 20);
            this.label28.TabIndex = 3;
            this.label28.Text = "Hosts: ";
            // 
            // AddAnalysisServiceHost
            // 
            this.AddAnalysisServiceHost.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddAnalysisServiceHost.Location = new System.Drawing.Point(66, 122);
            this.AddAnalysisServiceHost.Name = "AddAnalysisServiceHost";
            this.AddAnalysisServiceHost.Size = new System.Drawing.Size(92, 29);
            this.AddAnalysisServiceHost.TabIndex = 6;
            this.AddAnalysisServiceHost.Text = "Add";
            this.AddAnalysisServiceHost.UseVisualStyleBackColor = true;
            this.AddAnalysisServiceHost.Click += new System.EventHandler(this.AddAnalysisServiceHost_Click);
            // 
            // DeleteAnalysisServiceHost
            // 
            this.DeleteAnalysisServiceHost.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DeleteAnalysisServiceHost.Location = new System.Drawing.Point(158, 122);
            this.DeleteAnalysisServiceHost.Name = "DeleteAnalysisServiceHost";
            this.DeleteAnalysisServiceHost.Size = new System.Drawing.Size(90, 29);
            this.DeleteAnalysisServiceHost.TabIndex = 7;
            this.DeleteAnalysisServiceHost.Text = "Delete";
            this.DeleteAnalysisServiceHost.UseVisualStyleBackColor = true;
            this.DeleteAnalysisServiceHost.Click += new System.EventHandler(this.DeleteAnalysisServiceHost_Click);
            // 
            // ObserveDelay
            // 
            this.ObserveDelay.Location = new System.Drawing.Point(179, 273);
            this.ObserveDelay.Margin = new System.Windows.Forms.Padding(5);
            this.ObserveDelay.Name = "ObserveDelay";
            this.ObserveDelay.Size = new System.Drawing.Size(125, 27);
            this.ObserveDelay.TabIndex = 9;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 276);
            this.label8.Margin = new System.Windows.Forms.Padding(5);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(106, 20);
            this.label8.TabIndex = 8;
            this.label8.Text = "Observe delay:";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.groupBox7);
            this.groupBox5.Controls.Add(this.PostQueueSize);
            this.groupBox5.Controls.Add(this.label31);
            this.groupBox5.Controls.Add(this.ScanCommentsDelay);
            this.groupBox5.Controls.Add(this.label30);
            this.groupBox5.Controls.Add(this.label29);
            this.groupBox5.Controls.Add(this.ScanPostDelay);
            this.groupBox5.Location = new System.Drawing.Point(6, 6);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(318, 539);
            this.groupBox5.TabIndex = 1;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Collection service configuration";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.ApplyNewCollectionServiceEndpoint);
            this.groupBox7.Controls.Add(this.label7);
            this.groupBox7.Controls.Add(this.CollectionServiceEndpoint);
            this.groupBox7.Controls.Add(this.CollectionServiceHosts);
            this.groupBox7.Controls.Add(this.NewCollectionHost);
            this.groupBox7.Controls.Add(this.label27);
            this.groupBox7.Controls.Add(this.DeleteCollectionServiceHost);
            this.groupBox7.Controls.Add(this.AddCollectionServiceHost);
            this.groupBox7.Location = new System.Drawing.Point(6, 26);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(306, 222);
            this.groupBox7.TabIndex = 6;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Host configuration";
            // 
            // ApplyNewCollectionServiceEndpoint
            // 
            this.ApplyNewCollectionServiceEndpoint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ApplyNewCollectionServiceEndpoint.Location = new System.Drawing.Point(65, 179);
            this.ApplyNewCollectionServiceEndpoint.Name = "ApplyNewCollectionServiceEndpoint";
            this.ApplyNewCollectionServiceEndpoint.Size = new System.Drawing.Size(183, 28);
            this.ApplyNewCollectionServiceEndpoint.TabIndex = 19;
            this.ApplyNewCollectionServiceEndpoint.Text = "Reconnect";
            this.ApplyNewCollectionServiceEndpoint.UseVisualStyleBackColor = true;
            this.ApplyNewCollectionServiceEndpoint.Click += new System.EventHandler(this.ApplyNewCollectionServiceEndpoint_Click_1);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(16, 154);
            this.label7.Margin = new System.Windows.Forms.Padding(5);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(43, 20);
            this.label7.TabIndex = 18;
            this.label7.Text = "Host:";
            // 
            // CollectionServiceEndpoint
            // 
            this.CollectionServiceEndpoint.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CollectionServiceEndpoint.FormattingEnabled = true;
            this.CollectionServiceEndpoint.Location = new System.Drawing.Point(65, 151);
            this.CollectionServiceEndpoint.Name = "CollectionServiceEndpoint";
            this.CollectionServiceEndpoint.Size = new System.Drawing.Size(183, 28);
            this.CollectionServiceEndpoint.TabIndex = 17;
            // 
            // CollectionServiceHosts
            // 
            this.CollectionServiceHosts.FormattingEnabled = true;
            this.CollectionServiceHosts.ItemHeight = 20;
            this.CollectionServiceHosts.Location = new System.Drawing.Point(65, 30);
            this.CollectionServiceHosts.Name = "CollectionServiceHosts";
            this.CollectionServiceHosts.Size = new System.Drawing.Size(183, 64);
            this.CollectionServiceHosts.TabIndex = 0;
            // 
            // NewCollectionHost
            // 
            this.NewCollectionHost.Location = new System.Drawing.Point(65, 96);
            this.NewCollectionHost.Name = "NewCollectionHost";
            this.NewCollectionHost.PlaceholderText = "New host...";
            this.NewCollectionHost.Size = new System.Drawing.Size(183, 27);
            this.NewCollectionHost.TabIndex = 8;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(10, 30);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(49, 20);
            this.label27.TabIndex = 2;
            this.label27.Text = "Hosts:";
            // 
            // DeleteCollectionServiceHost
            // 
            this.DeleteCollectionServiceHost.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DeleteCollectionServiceHost.Location = new System.Drawing.Point(158, 122);
            this.DeleteCollectionServiceHost.Name = "DeleteCollectionServiceHost";
            this.DeleteCollectionServiceHost.Size = new System.Drawing.Size(90, 29);
            this.DeleteCollectionServiceHost.TabIndex = 5;
            this.DeleteCollectionServiceHost.Text = "Delete";
            this.DeleteCollectionServiceHost.UseVisualStyleBackColor = true;
            this.DeleteCollectionServiceHost.Click += new System.EventHandler(this.DeleteCollectionServiceHost_Click);
            // 
            // AddCollectionServiceHost
            // 
            this.AddCollectionServiceHost.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddCollectionServiceHost.Location = new System.Drawing.Point(65, 122);
            this.AddCollectionServiceHost.Name = "AddCollectionServiceHost";
            this.AddCollectionServiceHost.Size = new System.Drawing.Size(92, 29);
            this.AddCollectionServiceHost.TabIndex = 4;
            this.AddCollectionServiceHost.Text = "Add";
            this.AddCollectionServiceHost.UseVisualStyleBackColor = true;
            this.AddCollectionServiceHost.Click += new System.EventHandler(this.AddCollectionServiceHost_Click);
            // 
            // PostQueueSize
            // 
            this.PostQueueSize.Location = new System.Drawing.Point(174, 347);
            this.PostQueueSize.Margin = new System.Windows.Forms.Padding(5);
            this.PostQueueSize.Name = "PostQueueSize";
            this.PostQueueSize.Size = new System.Drawing.Size(125, 27);
            this.PostQueueSize.TabIndex = 5;
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(8, 350);
            this.label31.Margin = new System.Windows.Forms.Padding(5);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(113, 20);
            this.label31.TabIndex = 4;
            this.label31.Text = "Post queue size:";
            // 
            // ScanCommentsDelay
            // 
            this.ScanCommentsDelay.Location = new System.Drawing.Point(174, 310);
            this.ScanCommentsDelay.Margin = new System.Windows.Forms.Padding(5);
            this.ScanCommentsDelay.Name = "ScanCommentsDelay";
            this.ScanCommentsDelay.Size = new System.Drawing.Size(125, 27);
            this.ScanCommentsDelay.TabIndex = 3;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(8, 313);
            this.label30.Margin = new System.Windows.Forms.Padding(5);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(156, 20);
            this.label30.TabIndex = 2;
            this.label30.Text = "Scan comments delay:";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(8, 276);
            this.label29.Margin = new System.Windows.Forms.Padding(5);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(116, 20);
            this.label29.TabIndex = 1;
            this.label29.Text = "Scan post delay:";
            // 
            // ScanPostDelay
            // 
            this.ScanPostDelay.Location = new System.Drawing.Point(174, 273);
            this.ScanPostDelay.Margin = new System.Windows.Forms.Padding(5);
            this.ScanPostDelay.Name = "ScanPostDelay";
            this.ScanPostDelay.Size = new System.Drawing.Size(125, 27);
            this.ScanPostDelay.TabIndex = 0;
            // 
            // MainTab
            // 
            this.MainTab.Controls.Add(this.AnalysisLogDate);
            this.MainTab.Controls.Add(this.CollectionLogDate);
            this.MainTab.Controls.Add(this.groupBox3);
            this.MainTab.Controls.Add(this.groupBox2);
            this.MainTab.Controls.Add(this.ViewAnalysisServiceLogs);
            this.MainTab.Controls.Add(this.groupBox1);
            this.MainTab.Controls.Add(this.ViewCollectionServiceLogs);
            this.MainTab.Location = new System.Drawing.Point(4, 44);
            this.MainTab.Name = "MainTab";
            this.MainTab.Padding = new System.Windows.Forms.Padding(3);
            this.MainTab.Size = new System.Drawing.Size(674, 605);
            this.MainTab.TabIndex = 0;
            this.MainTab.Text = "Main";
            this.MainTab.UseVisualStyleBackColor = true;
            // 
            // AnalysisLogDate
            // 
            this.AnalysisLogDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AnalysisLogDate.Location = new System.Drawing.Point(353, 351);
            this.AnalysisLogDate.Name = "AnalysisLogDate";
            this.AnalysisLogDate.Size = new System.Drawing.Size(154, 27);
            this.AnalysisLogDate.TabIndex = 18;
            // 
            // CollectionLogDate
            // 
            this.CollectionLogDate.Location = new System.Drawing.Point(6, 351);
            this.CollectionLogDate.Name = "CollectionLogDate";
            this.CollectionLogDate.Size = new System.Drawing.Size(154, 27);
            this.CollectionLogDate.TabIndex = 17;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.ClearEvaluatedDatabase);
            this.groupBox3.Controls.Add(this.ClearCommentsDatabase);
            this.groupBox3.Controls.Add(this.StartAll);
            this.groupBox3.Controls.Add(this.StartAnalysisService);
            this.groupBox3.Controls.Add(this.StartCollectionService);
            this.groupBox3.Controls.Add(this.StopAll);
            this.groupBox3.Controls.Add(this.StopAnalysisService);
            this.groupBox3.Controls.Add(this.StopCollectionService);
            this.groupBox3.Location = new System.Drawing.Point(8, 400);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(660, 196);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Control";
            // 
            // ClearEvaluatedDatabase
            // 
            this.ClearEvaluatedDatabase.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ClearEvaluatedDatabase.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ClearEvaluatedDatabase.Location = new System.Drawing.Point(479, 84);
            this.ClearEvaluatedDatabase.Name = "ClearEvaluatedDatabase";
            this.ClearEvaluatedDatabase.Size = new System.Drawing.Size(175, 50);
            this.ClearEvaluatedDatabase.TabIndex = 3;
            this.ClearEvaluatedDatabase.Text = "Clear evaluated comments";
            this.ClearEvaluatedDatabase.UseVisualStyleBackColor = true;
            this.ClearEvaluatedDatabase.Click += new System.EventHandler(this.ClearEvaluatedDatabase_Click);
            // 
            // ClearCommentsDatabase
            // 
            this.ClearCommentsDatabase.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ClearCommentsDatabase.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ClearCommentsDatabase.Location = new System.Drawing.Point(6, 84);
            this.ClearCommentsDatabase.Name = "ClearCommentsDatabase";
            this.ClearCommentsDatabase.Size = new System.Drawing.Size(175, 50);
            this.ClearCommentsDatabase.TabIndex = 2;
            this.ClearCommentsDatabase.Text = "Clear database";
            this.ClearCommentsDatabase.UseVisualStyleBackColor = true;
            this.ClearCommentsDatabase.Click += new System.EventHandler(this.ClearCommentsDatabase_Click);
            // 
            // StartAll
            // 
            this.StartAll.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.StartAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.StartAll.Location = new System.Drawing.Point(248, 140);
            this.StartAll.Name = "StartAll";
            this.StartAll.Size = new System.Drawing.Size(175, 50);
            this.StartAll.TabIndex = 6;
            this.StartAll.Text = "Start all";
            this.StartAll.UseVisualStyleBackColor = true;
            this.StartAll.Click += new System.EventHandler(this.StartAll_Click);
            // 
            // StartAnalysisService
            // 
            this.StartAnalysisService.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.StartAnalysisService.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.StartAnalysisService.Location = new System.Drawing.Point(479, 140);
            this.StartAnalysisService.Name = "StartAnalysisService";
            this.StartAnalysisService.Size = new System.Drawing.Size(175, 50);
            this.StartAnalysisService.TabIndex = 1;
            this.StartAnalysisService.Text = "Start analysis service";
            this.StartAnalysisService.UseVisualStyleBackColor = true;
            this.StartAnalysisService.Click += new System.EventHandler(this.StartAnalysisService_Click);
            // 
            // StartCollectionService
            // 
            this.StartCollectionService.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.StartCollectionService.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.StartCollectionService.Location = new System.Drawing.Point(6, 140);
            this.StartCollectionService.Name = "StartCollectionService";
            this.StartCollectionService.Size = new System.Drawing.Size(175, 50);
            this.StartCollectionService.TabIndex = 0;
            this.StartCollectionService.Text = "Start collection service";
            this.StartCollectionService.UseVisualStyleBackColor = true;
            this.StartCollectionService.Click += new System.EventHandler(this.StartCollectionService_Click);
            // 
            // StopAll
            // 
            this.StopAll.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.StopAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.StopAll.Location = new System.Drawing.Point(248, 140);
            this.StopAll.Name = "StopAll";
            this.StopAll.Size = new System.Drawing.Size(175, 50);
            this.StopAll.TabIndex = 7;
            this.StopAll.Text = "Stop all";
            this.StopAll.UseVisualStyleBackColor = true;
            this.StopAll.Click += new System.EventHandler(this.StopAll_Click);
            // 
            // StopAnalysisService
            // 
            this.StopAnalysisService.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.StopAnalysisService.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.StopAnalysisService.Location = new System.Drawing.Point(479, 140);
            this.StopAnalysisService.Name = "StopAnalysisService";
            this.StopAnalysisService.Size = new System.Drawing.Size(175, 50);
            this.StopAnalysisService.TabIndex = 5;
            this.StopAnalysisService.Text = "Stop analysis service";
            this.StopAnalysisService.UseVisualStyleBackColor = true;
            this.StopAnalysisService.Click += new System.EventHandler(this.StopAnalysisService_Click);
            // 
            // StopCollectionService
            // 
            this.StopCollectionService.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.StopCollectionService.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.StopCollectionService.Location = new System.Drawing.Point(6, 140);
            this.StopCollectionService.Name = "StopCollectionService";
            this.StopCollectionService.Size = new System.Drawing.Size(175, 50);
            this.StopCollectionService.TabIndex = 4;
            this.StopCollectionService.Text = "Stop collection service";
            this.StopCollectionService.UseVisualStyleBackColor = true;
            this.StopCollectionService.Click += new System.EventHandler(this.StopCollectionService_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.AnalysisServiceProgressBar);
            this.groupBox2.Controls.Add(this.RefreshAnalysisServiceInfo);
            this.groupBox2.Controls.Add(this.AnalysisEvaluated);
            this.groupBox2.Controls.Add(this.AnalysisWarnings);
            this.groupBox2.Controls.Add(this.label26);
            this.groupBox2.Controls.Add(this.AnalysisErrors);
            this.groupBox2.Controls.Add(this.label25);
            this.groupBox2.Controls.Add(this.AnalysisUptime);
            this.groupBox2.Controls.Add(this.label24);
            this.groupBox2.Controls.Add(this.AnalysisState);
            this.groupBox2.Controls.Add(this.label23);
            this.groupBox2.Controls.Add(this.AnalysisConnection);
            this.groupBox2.Controls.Add(this.label22);
            this.groupBox2.Controls.Add(this.label21);
            this.groupBox2.Controls.Add(this.AnalysisProgressBarRed);
            this.groupBox2.Location = new System.Drawing.Point(353, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(315, 345);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Analysis service info";
            // 
            // AnalysisServiceProgressBar
            // 
            this.AnalysisServiceProgressBar.Location = new System.Drawing.Point(6, 32);
            this.AnalysisServiceProgressBar.Name = "AnalysisServiceProgressBar";
            this.AnalysisServiceProgressBar.Size = new System.Drawing.Size(222, 17);
            this.AnalysisServiceProgressBar.TabIndex = 30;
            // 
            // RefreshAnalysisServiceInfo
            // 
            this.RefreshAnalysisServiceInfo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RefreshAnalysisServiceInfo.Location = new System.Drawing.Point(234, 26);
            this.RefreshAnalysisServiceInfo.Name = "RefreshAnalysisServiceInfo";
            this.RefreshAnalysisServiceInfo.Size = new System.Drawing.Size(75, 29);
            this.RefreshAnalysisServiceInfo.TabIndex = 16;
            this.RefreshAnalysisServiceInfo.Text = "Refresh";
            this.RefreshAnalysisServiceInfo.UseVisualStyleBackColor = true;
            this.RefreshAnalysisServiceInfo.Click += new System.EventHandler(this.RefreshAnalysisServiceInfo_Click);
            // 
            // AnalysisEvaluated
            // 
            this.AnalysisEvaluated.AutoSize = true;
            this.AnalysisEvaluated.Location = new System.Drawing.Point(164, 225);
            this.AnalysisEvaluated.Margin = new System.Windows.Forms.Padding(5);
            this.AnalysisEvaluated.Name = "AnalysisEvaluated";
            this.AnalysisEvaluated.Size = new System.Drawing.Size(45, 20);
            this.AnalysisEvaluated.TabIndex = 29;
            this.AnalysisEvaluated.Text = "None";
            // 
            // AnalysisWarnings
            // 
            this.AnalysisWarnings.AutoSize = true;
            this.AnalysisWarnings.Location = new System.Drawing.Point(164, 195);
            this.AnalysisWarnings.Margin = new System.Windows.Forms.Padding(5);
            this.AnalysisWarnings.Name = "AnalysisWarnings";
            this.AnalysisWarnings.Size = new System.Drawing.Size(45, 20);
            this.AnalysisWarnings.TabIndex = 28;
            this.AnalysisWarnings.Text = "None";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(67, 75);
            this.label26.Margin = new System.Windows.Forms.Padding(5);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(87, 20);
            this.label26.TabIndex = 15;
            this.label26.Text = "Connection:";
            // 
            // AnalysisErrors
            // 
            this.AnalysisErrors.AutoSize = true;
            this.AnalysisErrors.Location = new System.Drawing.Point(164, 165);
            this.AnalysisErrors.Margin = new System.Windows.Forms.Padding(5);
            this.AnalysisErrors.Name = "AnalysisErrors";
            this.AnalysisErrors.Size = new System.Drawing.Size(45, 20);
            this.AnalysisErrors.TabIndex = 27;
            this.AnalysisErrors.Text = "None";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(67, 105);
            this.label25.Margin = new System.Windows.Forms.Padding(5);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(46, 20);
            this.label25.TabIndex = 16;
            this.label25.Text = "State:";
            // 
            // AnalysisUptime
            // 
            this.AnalysisUptime.AutoSize = true;
            this.AnalysisUptime.Location = new System.Drawing.Point(164, 135);
            this.AnalysisUptime.Margin = new System.Windows.Forms.Padding(5);
            this.AnalysisUptime.Name = "AnalysisUptime";
            this.AnalysisUptime.Size = new System.Drawing.Size(45, 20);
            this.AnalysisUptime.TabIndex = 26;
            this.AnalysisUptime.Text = "None";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(67, 135);
            this.label24.Margin = new System.Windows.Forms.Padding(5);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(61, 20);
            this.label24.TabIndex = 17;
            this.label24.Text = "Uptime:";
            // 
            // AnalysisState
            // 
            this.AnalysisState.AutoSize = true;
            this.AnalysisState.Location = new System.Drawing.Point(164, 105);
            this.AnalysisState.Margin = new System.Windows.Forms.Padding(5);
            this.AnalysisState.Name = "AnalysisState";
            this.AnalysisState.Size = new System.Drawing.Size(45, 20);
            this.AnalysisState.TabIndex = 25;
            this.AnalysisState.Text = "None";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(67, 165);
            this.label23.Margin = new System.Windows.Forms.Padding(5);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(50, 20);
            this.label23.TabIndex = 18;
            this.label23.Text = "Errors:";
            // 
            // AnalysisConnection
            // 
            this.AnalysisConnection.AutoSize = true;
            this.AnalysisConnection.Location = new System.Drawing.Point(164, 75);
            this.AnalysisConnection.Margin = new System.Windows.Forms.Padding(5);
            this.AnalysisConnection.Name = "AnalysisConnection";
            this.AnalysisConnection.Size = new System.Drawing.Size(45, 20);
            this.AnalysisConnection.TabIndex = 24;
            this.AnalysisConnection.Text = "None";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(67, 195);
            this.label22.Margin = new System.Windows.Forms.Padding(5);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(73, 20);
            this.label22.TabIndex = 19;
            this.label22.Text = "Warnings:";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(67, 225);
            this.label21.Margin = new System.Windows.Forms.Padding(5);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(77, 20);
            this.label21.TabIndex = 20;
            this.label21.Text = "Evaluated:";
            // 
            // ViewAnalysisServiceLogs
            // 
            this.ViewAnalysisServiceLogs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ViewAnalysisServiceLogs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ViewAnalysisServiceLogs.Location = new System.Drawing.Point(513, 351);
            this.ViewAnalysisServiceLogs.Name = "ViewAnalysisServiceLogs";
            this.ViewAnalysisServiceLogs.Size = new System.Drawing.Size(155, 37);
            this.ViewAnalysisServiceLogs.TabIndex = 23;
            this.ViewAnalysisServiceLogs.Text = "View analysis log";
            this.ViewAnalysisServiceLogs.UseVisualStyleBackColor = true;
            this.ViewAnalysisServiceLogs.Click += new System.EventHandler(this.ViewAnalysisServiceLogs_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.CollectionServiceProgressBar);
            this.groupBox1.Controls.Add(this.RefreshCollectionServiceInfo);
            this.groupBox1.Controls.Add(this.CollectionCollected);
            this.groupBox1.Controls.Add(this.CollectionWarnings);
            this.groupBox1.Controls.Add(this.CollectionErrors);
            this.groupBox1.Controls.Add(this.CollectionUptime);
            this.groupBox1.Controls.Add(this.CollectionState);
            this.groupBox1.Controls.Add(this.CollectionConnection);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.CollectionProgressBarRed);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(315, 345);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Collection service info";
            // 
            // CollectionServiceProgressBar
            // 
            this.CollectionServiceProgressBar.Location = new System.Drawing.Point(6, 32);
            this.CollectionServiceProgressBar.Name = "CollectionServiceProgressBar";
            this.CollectionServiceProgressBar.Size = new System.Drawing.Size(222, 17);
            this.CollectionServiceProgressBar.TabIndex = 16;
            // 
            // RefreshCollectionServiceInfo
            // 
            this.RefreshCollectionServiceInfo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RefreshCollectionServiceInfo.Location = new System.Drawing.Point(234, 26);
            this.RefreshCollectionServiceInfo.Name = "RefreshCollectionServiceInfo";
            this.RefreshCollectionServiceInfo.Size = new System.Drawing.Size(75, 29);
            this.RefreshCollectionServiceInfo.TabIndex = 15;
            this.RefreshCollectionServiceInfo.Text = "Refresh";
            this.RefreshCollectionServiceInfo.UseVisualStyleBackColor = true;
            this.RefreshCollectionServiceInfo.Click += new System.EventHandler(this.RefreshCollectionServiceInfo_Click);
            // 
            // CollectionCollected
            // 
            this.CollectionCollected.AutoSize = true;
            this.CollectionCollected.Location = new System.Drawing.Point(164, 225);
            this.CollectionCollected.Margin = new System.Windows.Forms.Padding(5);
            this.CollectionCollected.Name = "CollectionCollected";
            this.CollectionCollected.Size = new System.Drawing.Size(45, 20);
            this.CollectionCollected.TabIndex = 14;
            this.CollectionCollected.Text = "None";
            // 
            // CollectionWarnings
            // 
            this.CollectionWarnings.AutoSize = true;
            this.CollectionWarnings.Location = new System.Drawing.Point(164, 195);
            this.CollectionWarnings.Margin = new System.Windows.Forms.Padding(5);
            this.CollectionWarnings.Name = "CollectionWarnings";
            this.CollectionWarnings.Size = new System.Drawing.Size(45, 20);
            this.CollectionWarnings.TabIndex = 13;
            this.CollectionWarnings.Text = "None";
            // 
            // CollectionErrors
            // 
            this.CollectionErrors.AutoSize = true;
            this.CollectionErrors.Location = new System.Drawing.Point(164, 165);
            this.CollectionErrors.Margin = new System.Windows.Forms.Padding(5);
            this.CollectionErrors.Name = "CollectionErrors";
            this.CollectionErrors.Size = new System.Drawing.Size(45, 20);
            this.CollectionErrors.TabIndex = 12;
            this.CollectionErrors.Text = "None";
            // 
            // CollectionUptime
            // 
            this.CollectionUptime.AutoSize = true;
            this.CollectionUptime.Location = new System.Drawing.Point(164, 135);
            this.CollectionUptime.Margin = new System.Windows.Forms.Padding(5);
            this.CollectionUptime.Name = "CollectionUptime";
            this.CollectionUptime.Size = new System.Drawing.Size(45, 20);
            this.CollectionUptime.TabIndex = 11;
            this.CollectionUptime.Text = "None";
            // 
            // CollectionState
            // 
            this.CollectionState.AutoSize = true;
            this.CollectionState.Location = new System.Drawing.Point(164, 105);
            this.CollectionState.Margin = new System.Windows.Forms.Padding(5);
            this.CollectionState.Name = "CollectionState";
            this.CollectionState.Size = new System.Drawing.Size(45, 20);
            this.CollectionState.TabIndex = 10;
            this.CollectionState.Text = "None";
            // 
            // CollectionConnection
            // 
            this.CollectionConnection.AutoSize = true;
            this.CollectionConnection.Location = new System.Drawing.Point(164, 75);
            this.CollectionConnection.Margin = new System.Windows.Forms.Padding(5);
            this.CollectionConnection.Name = "CollectionConnection";
            this.CollectionConnection.Size = new System.Drawing.Size(45, 20);
            this.CollectionConnection.TabIndex = 9;
            this.CollectionConnection.Text = "None";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(67, 225);
            this.label6.Margin = new System.Windows.Forms.Padding(5);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 20);
            this.label6.TabIndex = 5;
            this.label6.Text = "Collected:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(67, 195);
            this.label5.Margin = new System.Windows.Forms.Padding(5);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 20);
            this.label5.TabIndex = 4;
            this.label5.Text = "Warnings:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(67, 165);
            this.label4.Margin = new System.Windows.Forms.Padding(5);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "Errors:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(67, 135);
            this.label3.Margin = new System.Windows.Forms.Padding(5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Uptime:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(67, 105);
            this.label2.Margin = new System.Windows.Forms.Padding(5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "State:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(67, 75);
            this.label1.Margin = new System.Windows.Forms.Padding(5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Connection:";
            // 
            // ViewCollectionServiceLogs
            // 
            this.ViewCollectionServiceLogs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ViewCollectionServiceLogs.Location = new System.Drawing.Point(166, 351);
            this.ViewCollectionServiceLogs.Name = "ViewCollectionServiceLogs";
            this.ViewCollectionServiceLogs.Size = new System.Drawing.Size(155, 37);
            this.ViewCollectionServiceLogs.TabIndex = 8;
            this.ViewCollectionServiceLogs.Text = "View collection log";
            this.ViewCollectionServiceLogs.UseVisualStyleBackColor = true;
            this.ViewCollectionServiceLogs.Click += new System.EventHandler(this.ViewCollectionServiceLogs_Click);
            // 
            // Tabs
            // 
            this.Tabs.Controls.Add(this.MainTab);
            this.Tabs.Controls.Add(this.ConfigureTab);
            this.Tabs.Controls.Add(this.VkCollectorTab);
            this.Tabs.Controls.Add(this.ReportsTab);
            this.Tabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Tabs.ItemSize = new System.Drawing.Size(100, 40);
            this.Tabs.Location = new System.Drawing.Point(0, 0);
            this.Tabs.Name = "Tabs";
            this.Tabs.SelectedIndex = 0;
            this.Tabs.Size = new System.Drawing.Size(682, 653);
            this.Tabs.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.Tabs.TabIndex = 0;
            // 
            // CollectionProgressBarRed
            // 
            this.CollectionProgressBarRed.BackColor = System.Drawing.Color.Red;
            this.CollectionProgressBarRed.Location = new System.Drawing.Point(6, 32);
            this.CollectionProgressBarRed.Name = "CollectionProgressBarRed";
            this.CollectionProgressBarRed.Size = new System.Drawing.Size(222, 17);
            this.CollectionProgressBarRed.TabIndex = 17;
            // 
            // AnalysisProgressBarRed
            // 
            this.AnalysisProgressBarRed.BackColor = System.Drawing.Color.Red;
            this.AnalysisProgressBarRed.Location = new System.Drawing.Point(6, 32);
            this.AnalysisProgressBarRed.Name = "AnalysisProgressBarRed";
            this.AnalysisProgressBarRed.Size = new System.Drawing.Size(222, 17);
            this.AnalysisProgressBarRed.TabIndex = 31;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(682, 653);
            this.Controls.Add(this.Tabs);
            this.MinimumSize = new System.Drawing.Size(700, 700);
            this.Name = "MainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.VkCollectorTab.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.ReportsTab.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ConfigureTab.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.MainTab.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.Tabs.ResumeLayout(false);
            this.ResumeLayout(false);

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
        private GroupBox groupBox10;
        private Button ApplyAnalysisServiceEndpoint;
        private Label label20;
        public ComboBox AnalysisServiceEndpoint;
        public ListBox AnalysisServiceHosts;
        private TextBox NewAnalysisHost;
        private Label label28;
        private Button AddAnalysisServiceHost;
        private Button DeleteAnalysisServiceHost;
        private TextBox ObserveDelay;
        private Label label8;
        private GroupBox groupBox5;
        private GroupBox groupBox7;
        private Button ApplyNewCollectionServiceEndpoint;
        private Label label7;
        public ComboBox CollectionServiceEndpoint;
        public ListBox CollectionServiceHosts;
        private TextBox NewCollectionHost;
        private Label label27;
        private Button DeleteCollectionServiceHost;
        private Button AddCollectionServiceHost;
        private TextBox PostQueueSize;
        private Label label31;
        private TextBox ScanCommentsDelay;
        private Label label30;
        private Label label29;
        private TextBox ScanPostDelay;
        private TabPage MainTab;
        private GroupBox groupBox3;
        private Button StartAll;
        private Button ClearEvaluatedDatabase;
        private Button ClearCommentsDatabase;
        private Button StartAnalysisService;
        private Button StartCollectionService;
        private Button StopAll;
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
    }
}