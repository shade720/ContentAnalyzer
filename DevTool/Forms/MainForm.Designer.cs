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
            this.Tabs = new System.Windows.Forms.TabControl();
            this.MainTab = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.StartAll = new System.Windows.Forms.Button();
            this.ClearEvaluatedDatabase = new System.Windows.Forms.Button();
            this.ClearCommentsDatabase = new System.Windows.Forms.Button();
            this.StartAnalysisService = new System.Windows.Forms.Button();
            this.StartCollectionService = new System.Windows.Forms.Button();
            this.StopAll = new System.Windows.Forms.Button();
            this.StopAnalysisService = new System.Windows.Forms.Button();
            this.StopCollectionService = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.AnalysisLogDate = new System.Windows.Forms.DateTimePicker();
            this.ApplyAnalysisServiceEndpoint = new System.Windows.Forms.Button();
            this.RefreshAnalysisServiceInfo = new System.Windows.Forms.Button();
            this.AnalysisEvaluated = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
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
            this.ViewAnalysisServiceLogs = new System.Windows.Forms.Button();
            this.label21 = new System.Windows.Forms.Label();
            this.AnalysisServiceEndpoint = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.CollectionLogDate = new System.Windows.Forms.DateTimePicker();
            this.ApplyNewCollectionServiceEndpoint = new System.Windows.Forms.Button();
            this.RefreshCollectionServiceInfo = new System.Windows.Forms.Button();
            this.CollectionCollected = new System.Windows.Forms.Label();
            this.CollectionWarnings = new System.Windows.Forms.Label();
            this.CollectionErrors = new System.Windows.Forms.Label();
            this.CollectionUptime = new System.Windows.Forms.Label();
            this.CollectionState = new System.Windows.Forms.Label();
            this.CollectionConnection = new System.Windows.Forms.Label();
            this.ViewCollectionServiceLogs = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.CollectionServiceEndpoint = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.SaveConfiguration = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.EvaluateThreshold = new System.Windows.Forms.TextBox();
            this.label33 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.PostQueueSize = new System.Windows.Forms.TextBox();
            this.LoadConfiguration = new System.Windows.Forms.Button();
            this.label31 = new System.Windows.Forms.Label();
            this.ScanCommentsDelay = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.ScanPostDelay = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.NewAnalysisHost = new System.Windows.Forms.TextBox();
            this.NewCollectionHost = new System.Windows.Forms.TextBox();
            this.ConnectionString = new System.Windows.Forms.TextBox();
            this.label32 = new System.Windows.Forms.Label();
            this.DeleteAnalysisServiceHost = new System.Windows.Forms.Button();
            this.DeleteCollectionServiceHost = new System.Windows.Forms.Button();
            this.AddCollectionServiceHost = new System.Windows.Forms.Button();
            this.AddAnalysisServiceHost = new System.Windows.Forms.Button();
            this.label28 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.AnalysisServiceHosts = new System.Windows.Forms.ListBox();
            this.CollectionServiceHosts = new System.Windows.Forms.ListBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.Tabs.SuspendLayout();
            this.MainTab.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // Tabs
            // 
            this.Tabs.Controls.Add(this.MainTab);
            this.Tabs.Controls.Add(this.tabPage2);
            this.Tabs.Controls.Add(this.tabPage3);
            this.Tabs.Controls.Add(this.tabPage4);
            this.Tabs.Controls.Add(this.tabPage5);
            this.Tabs.Location = new System.Drawing.Point(0, 0);
            this.Tabs.Name = "Tabs";
            this.Tabs.SelectedIndex = 0;
            this.Tabs.Size = new System.Drawing.Size(682, 653);
            this.Tabs.TabIndex = 0;
            // 
            // MainTab
            // 
            this.MainTab.Controls.Add(this.groupBox3);
            this.MainTab.Controls.Add(this.groupBox2);
            this.MainTab.Controls.Add(this.groupBox1);
            this.MainTab.Location = new System.Drawing.Point(4, 29);
            this.MainTab.Name = "MainTab";
            this.MainTab.Padding = new System.Windows.Forms.Padding(3);
            this.MainTab.Size = new System.Drawing.Size(674, 620);
            this.MainTab.TabIndex = 0;
            this.MainTab.Text = "Main";
            this.MainTab.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.StartAll);
            this.groupBox3.Controls.Add(this.ClearEvaluatedDatabase);
            this.groupBox3.Controls.Add(this.ClearCommentsDatabase);
            this.groupBox3.Controls.Add(this.StartAnalysisService);
            this.groupBox3.Controls.Add(this.StartCollectionService);
            this.groupBox3.Controls.Add(this.StopAll);
            this.groupBox3.Controls.Add(this.StopAnalysisService);
            this.groupBox3.Controls.Add(this.StopCollectionService);
            this.groupBox3.Location = new System.Drawing.Point(6, 472);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(660, 140);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Control";
            // 
            // StartAll
            // 
            this.StartAll.Location = new System.Drawing.Point(474, 26);
            this.StartAll.Name = "StartAll";
            this.StartAll.Size = new System.Drawing.Size(175, 50);
            this.StartAll.TabIndex = 6;
            this.StartAll.Text = "Start all";
            this.StartAll.UseVisualStyleBackColor = true;
            this.StartAll.Click += new System.EventHandler(this.StartAll_Click);
            // 
            // ClearEvaluatedDatabase
            // 
            this.ClearEvaluatedDatabase.Location = new System.Drawing.Point(187, 82);
            this.ClearEvaluatedDatabase.Name = "ClearEvaluatedDatabase";
            this.ClearEvaluatedDatabase.Size = new System.Drawing.Size(175, 50);
            this.ClearEvaluatedDatabase.TabIndex = 3;
            this.ClearEvaluatedDatabase.Text = "Clear evaluated database";
            this.ClearEvaluatedDatabase.UseVisualStyleBackColor = true;
            this.ClearEvaluatedDatabase.Click += new System.EventHandler(this.ClearEvaluatedDatabase_Click);
            // 
            // ClearCommentsDatabase
            // 
            this.ClearCommentsDatabase.Location = new System.Drawing.Point(6, 82);
            this.ClearCommentsDatabase.Name = "ClearCommentsDatabase";
            this.ClearCommentsDatabase.Size = new System.Drawing.Size(175, 50);
            this.ClearCommentsDatabase.TabIndex = 2;
            this.ClearCommentsDatabase.Text = "Clear comments database";
            this.ClearCommentsDatabase.UseVisualStyleBackColor = true;
            this.ClearCommentsDatabase.Click += new System.EventHandler(this.ClearCommentsDatabase_Click);
            // 
            // StartAnalysisService
            // 
            this.StartAnalysisService.Location = new System.Drawing.Point(187, 26);
            this.StartAnalysisService.Name = "StartAnalysisService";
            this.StartAnalysisService.Size = new System.Drawing.Size(175, 50);
            this.StartAnalysisService.TabIndex = 1;
            this.StartAnalysisService.Text = "Start analysis service";
            this.StartAnalysisService.UseVisualStyleBackColor = true;
            this.StartAnalysisService.Click += new System.EventHandler(this.StartAnalysisService_Click);
            // 
            // StartCollectionService
            // 
            this.StartCollectionService.Location = new System.Drawing.Point(6, 26);
            this.StartCollectionService.Name = "StartCollectionService";
            this.StartCollectionService.Size = new System.Drawing.Size(175, 50);
            this.StartCollectionService.TabIndex = 0;
            this.StartCollectionService.Text = "Start collection service";
            this.StartCollectionService.UseVisualStyleBackColor = true;
            this.StartCollectionService.Click += new System.EventHandler(this.StartCollectionService_Click);
            // 
            // StopAll
            // 
            this.StopAll.Location = new System.Drawing.Point(474, 26);
            this.StopAll.Name = "StopAll";
            this.StopAll.Size = new System.Drawing.Size(175, 50);
            this.StopAll.TabIndex = 7;
            this.StopAll.Text = "Stop all";
            this.StopAll.UseVisualStyleBackColor = true;
            this.StopAll.Click += new System.EventHandler(this.StopAll_Click);
            // 
            // StopAnalysisService
            // 
            this.StopAnalysisService.Location = new System.Drawing.Point(187, 26);
            this.StopAnalysisService.Name = "StopAnalysisService";
            this.StopAnalysisService.Size = new System.Drawing.Size(175, 50);
            this.StopAnalysisService.TabIndex = 5;
            this.StopAnalysisService.Text = "Stop analysis service";
            this.StopAnalysisService.UseVisualStyleBackColor = true;
            this.StopAnalysisService.Click += new System.EventHandler(this.StopAnalysisService_Click);
            // 
            // StopCollectionService
            // 
            this.StopCollectionService.Location = new System.Drawing.Point(6, 26);
            this.StopCollectionService.Name = "StopCollectionService";
            this.StopCollectionService.Size = new System.Drawing.Size(175, 50);
            this.StopCollectionService.TabIndex = 4;
            this.StopCollectionService.Text = "Stop collection service";
            this.StopCollectionService.UseVisualStyleBackColor = true;
            this.StopCollectionService.Click += new System.EventHandler(this.StopCollectionService_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.AnalysisLogDate);
            this.groupBox2.Controls.Add(this.ApplyAnalysisServiceEndpoint);
            this.groupBox2.Controls.Add(this.RefreshAnalysisServiceInfo);
            this.groupBox2.Controls.Add(this.AnalysisEvaluated);
            this.groupBox2.Controls.Add(this.label20);
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
            this.groupBox2.Controls.Add(this.ViewAnalysisServiceLogs);
            this.groupBox2.Controls.Add(this.label21);
            this.groupBox2.Controls.Add(this.AnalysisServiceEndpoint);
            this.groupBox2.Location = new System.Drawing.Point(353, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(315, 460);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Collection service info";
            // 
            // AnalysisLogDate
            // 
            this.AnalysisLogDate.Location = new System.Drawing.Point(88, 59);
            this.AnalysisLogDate.Name = "AnalysisLogDate";
            this.AnalysisLogDate.Size = new System.Drawing.Size(156, 27);
            this.AnalysisLogDate.TabIndex = 18;
            // 
            // ApplyAnalysisServiceEndpoint
            // 
            this.ApplyAnalysisServiceEndpoint.Location = new System.Drawing.Point(249, 25);
            this.ApplyAnalysisServiceEndpoint.Name = "ApplyAnalysisServiceEndpoint";
            this.ApplyAnalysisServiceEndpoint.Size = new System.Drawing.Size(61, 28);
            this.ApplyAnalysisServiceEndpoint.TabIndex = 30;
            this.ApplyAnalysisServiceEndpoint.Text = "Apply";
            this.ApplyAnalysisServiceEndpoint.UseVisualStyleBackColor = true;
            this.ApplyAnalysisServiceEndpoint.Click += new System.EventHandler(this.ApplyAnalysisServiceEndpoint_Click);
            // 
            // RefreshAnalysisServiceInfo
            // 
            this.RefreshAnalysisServiceInfo.Location = new System.Drawing.Point(221, 431);
            this.RefreshAnalysisServiceInfo.Name = "RefreshAnalysisServiceInfo";
            this.RefreshAnalysisServiceInfo.Size = new System.Drawing.Size(94, 29);
            this.RefreshAnalysisServiceInfo.TabIndex = 16;
            this.RefreshAnalysisServiceInfo.Text = "Refresh";
            this.RefreshAnalysisServiceInfo.UseVisualStyleBackColor = true;
            this.RefreshAnalysisServiceInfo.Click += new System.EventHandler(this.RefreshAnalysisServiceInfo_Click);
            // 
            // AnalysisEvaluated
            // 
            this.AnalysisEvaluated.AutoSize = true;
            this.AnalysisEvaluated.Location = new System.Drawing.Point(165, 326);
            this.AnalysisEvaluated.Margin = new System.Windows.Forms.Padding(5);
            this.AnalysisEvaluated.Name = "AnalysisEvaluated";
            this.AnalysisEvaluated.Size = new System.Drawing.Size(45, 20);
            this.AnalysisEvaluated.TabIndex = 29;
            this.AnalysisEvaluated.Text = "None";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(8, 28);
            this.label20.Margin = new System.Windows.Forms.Padding(5);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(72, 20);
            this.label20.TabIndex = 22;
            this.label20.Text = "Endpoint:";
            // 
            // AnalysisWarnings
            // 
            this.AnalysisWarnings.AutoSize = true;
            this.AnalysisWarnings.Location = new System.Drawing.Point(165, 296);
            this.AnalysisWarnings.Margin = new System.Windows.Forms.Padding(5);
            this.AnalysisWarnings.Name = "AnalysisWarnings";
            this.AnalysisWarnings.Size = new System.Drawing.Size(45, 20);
            this.AnalysisWarnings.TabIndex = 28;
            this.AnalysisWarnings.Text = "None";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(68, 176);
            this.label26.Margin = new System.Windows.Forms.Padding(5);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(87, 20);
            this.label26.TabIndex = 15;
            this.label26.Text = "Connection:";
            // 
            // AnalysisErrors
            // 
            this.AnalysisErrors.AutoSize = true;
            this.AnalysisErrors.Location = new System.Drawing.Point(165, 266);
            this.AnalysisErrors.Margin = new System.Windows.Forms.Padding(5);
            this.AnalysisErrors.Name = "AnalysisErrors";
            this.AnalysisErrors.Size = new System.Drawing.Size(45, 20);
            this.AnalysisErrors.TabIndex = 27;
            this.AnalysisErrors.Text = "None";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(68, 206);
            this.label25.Margin = new System.Windows.Forms.Padding(5);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(46, 20);
            this.label25.TabIndex = 16;
            this.label25.Text = "State:";
            // 
            // AnalysisUptime
            // 
            this.AnalysisUptime.AutoSize = true;
            this.AnalysisUptime.Location = new System.Drawing.Point(165, 236);
            this.AnalysisUptime.Margin = new System.Windows.Forms.Padding(5);
            this.AnalysisUptime.Name = "AnalysisUptime";
            this.AnalysisUptime.Size = new System.Drawing.Size(45, 20);
            this.AnalysisUptime.TabIndex = 26;
            this.AnalysisUptime.Text = "None";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(68, 236);
            this.label24.Margin = new System.Windows.Forms.Padding(5);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(61, 20);
            this.label24.TabIndex = 17;
            this.label24.Text = "Uptime:";
            // 
            // AnalysisState
            // 
            this.AnalysisState.AutoSize = true;
            this.AnalysisState.Location = new System.Drawing.Point(165, 206);
            this.AnalysisState.Margin = new System.Windows.Forms.Padding(5);
            this.AnalysisState.Name = "AnalysisState";
            this.AnalysisState.Size = new System.Drawing.Size(45, 20);
            this.AnalysisState.TabIndex = 25;
            this.AnalysisState.Text = "None";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(68, 266);
            this.label23.Margin = new System.Windows.Forms.Padding(5);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(50, 20);
            this.label23.TabIndex = 18;
            this.label23.Text = "Errors:";
            // 
            // AnalysisConnection
            // 
            this.AnalysisConnection.AutoSize = true;
            this.AnalysisConnection.Location = new System.Drawing.Point(165, 176);
            this.AnalysisConnection.Margin = new System.Windows.Forms.Padding(5);
            this.AnalysisConnection.Name = "AnalysisConnection";
            this.AnalysisConnection.Size = new System.Drawing.Size(45, 20);
            this.AnalysisConnection.TabIndex = 24;
            this.AnalysisConnection.Text = "None";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(68, 296);
            this.label22.Margin = new System.Windows.Forms.Padding(5);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(73, 20);
            this.label22.TabIndex = 19;
            this.label22.Text = "Warnings:";
            // 
            // ViewAnalysisServiceLogs
            // 
            this.ViewAnalysisServiceLogs.Location = new System.Drawing.Point(249, 59);
            this.ViewAnalysisServiceLogs.Name = "ViewAnalysisServiceLogs";
            this.ViewAnalysisServiceLogs.Size = new System.Drawing.Size(61, 27);
            this.ViewAnalysisServiceLogs.TabIndex = 23;
            this.ViewAnalysisServiceLogs.Text = "Logs";
            this.ViewAnalysisServiceLogs.UseVisualStyleBackColor = true;
            this.ViewAnalysisServiceLogs.Click += new System.EventHandler(this.ViewAnalysisServiceLogs_Click);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(68, 326);
            this.label21.Margin = new System.Windows.Forms.Padding(5);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(77, 20);
            this.label21.TabIndex = 20;
            this.label21.Text = "Evaluated:";
            // 
            // AnalysisServiceEndpoint
            // 
            this.AnalysisServiceEndpoint.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AnalysisServiceEndpoint.FormattingEnabled = true;
            this.AnalysisServiceEndpoint.Location = new System.Drawing.Point(88, 25);
            this.AnalysisServiceEndpoint.Name = "AnalysisServiceEndpoint";
            this.AnalysisServiceEndpoint.Size = new System.Drawing.Size(156, 28);
            this.AnalysisServiceEndpoint.TabIndex = 21;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.CollectionLogDate);
            this.groupBox1.Controls.Add(this.ApplyNewCollectionServiceEndpoint);
            this.groupBox1.Controls.Add(this.RefreshCollectionServiceInfo);
            this.groupBox1.Controls.Add(this.CollectionCollected);
            this.groupBox1.Controls.Add(this.CollectionWarnings);
            this.groupBox1.Controls.Add(this.CollectionErrors);
            this.groupBox1.Controls.Add(this.CollectionUptime);
            this.groupBox1.Controls.Add(this.CollectionState);
            this.groupBox1.Controls.Add(this.CollectionConnection);
            this.groupBox1.Controls.Add(this.ViewCollectionServiceLogs);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.CollectionServiceEndpoint);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(315, 460);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Collection service info";
            // 
            // CollectionLogDate
            // 
            this.CollectionLogDate.Location = new System.Drawing.Point(88, 59);
            this.CollectionLogDate.Name = "CollectionLogDate";
            this.CollectionLogDate.Size = new System.Drawing.Size(154, 27);
            this.CollectionLogDate.TabIndex = 17;
            // 
            // ApplyNewCollectionServiceEndpoint
            // 
            this.ApplyNewCollectionServiceEndpoint.Location = new System.Drawing.Point(248, 25);
            this.ApplyNewCollectionServiceEndpoint.Name = "ApplyNewCollectionServiceEndpoint";
            this.ApplyNewCollectionServiceEndpoint.Size = new System.Drawing.Size(61, 28);
            this.ApplyNewCollectionServiceEndpoint.TabIndex = 16;
            this.ApplyNewCollectionServiceEndpoint.Text = "Apply";
            this.ApplyNewCollectionServiceEndpoint.UseVisualStyleBackColor = true;
            this.ApplyNewCollectionServiceEndpoint.Click += new System.EventHandler(this.ApplyNewCollectionServiceEndpoint_Click);
            // 
            // RefreshCollectionServiceInfo
            // 
            this.RefreshCollectionServiceInfo.Location = new System.Drawing.Point(221, 431);
            this.RefreshCollectionServiceInfo.Name = "RefreshCollectionServiceInfo";
            this.RefreshCollectionServiceInfo.Size = new System.Drawing.Size(94, 29);
            this.RefreshCollectionServiceInfo.TabIndex = 15;
            this.RefreshCollectionServiceInfo.Text = "Refresh";
            this.RefreshCollectionServiceInfo.UseVisualStyleBackColor = true;
            this.RefreshCollectionServiceInfo.Click += new System.EventHandler(this.RefreshCollectionServiceInfo_Click);
            // 
            // CollectionCollected
            // 
            this.CollectionCollected.AutoSize = true;
            this.CollectionCollected.Location = new System.Drawing.Point(158, 326);
            this.CollectionCollected.Margin = new System.Windows.Forms.Padding(5);
            this.CollectionCollected.Name = "CollectionCollected";
            this.CollectionCollected.Size = new System.Drawing.Size(45, 20);
            this.CollectionCollected.TabIndex = 14;
            this.CollectionCollected.Text = "None";
            // 
            // CollectionWarnings
            // 
            this.CollectionWarnings.AutoSize = true;
            this.CollectionWarnings.Location = new System.Drawing.Point(158, 296);
            this.CollectionWarnings.Margin = new System.Windows.Forms.Padding(5);
            this.CollectionWarnings.Name = "CollectionWarnings";
            this.CollectionWarnings.Size = new System.Drawing.Size(45, 20);
            this.CollectionWarnings.TabIndex = 13;
            this.CollectionWarnings.Text = "None";
            // 
            // CollectionErrors
            // 
            this.CollectionErrors.AutoSize = true;
            this.CollectionErrors.Location = new System.Drawing.Point(158, 266);
            this.CollectionErrors.Margin = new System.Windows.Forms.Padding(5);
            this.CollectionErrors.Name = "CollectionErrors";
            this.CollectionErrors.Size = new System.Drawing.Size(45, 20);
            this.CollectionErrors.TabIndex = 12;
            this.CollectionErrors.Text = "None";
            // 
            // CollectionUptime
            // 
            this.CollectionUptime.AutoSize = true;
            this.CollectionUptime.Location = new System.Drawing.Point(158, 236);
            this.CollectionUptime.Margin = new System.Windows.Forms.Padding(5);
            this.CollectionUptime.Name = "CollectionUptime";
            this.CollectionUptime.Size = new System.Drawing.Size(45, 20);
            this.CollectionUptime.TabIndex = 11;
            this.CollectionUptime.Text = "None";
            // 
            // CollectionState
            // 
            this.CollectionState.AutoSize = true;
            this.CollectionState.Location = new System.Drawing.Point(158, 206);
            this.CollectionState.Margin = new System.Windows.Forms.Padding(5);
            this.CollectionState.Name = "CollectionState";
            this.CollectionState.Size = new System.Drawing.Size(45, 20);
            this.CollectionState.TabIndex = 10;
            this.CollectionState.Text = "None";
            // 
            // CollectionConnection
            // 
            this.CollectionConnection.AutoSize = true;
            this.CollectionConnection.Location = new System.Drawing.Point(158, 176);
            this.CollectionConnection.Margin = new System.Windows.Forms.Padding(5);
            this.CollectionConnection.Name = "CollectionConnection";
            this.CollectionConnection.Size = new System.Drawing.Size(45, 20);
            this.CollectionConnection.TabIndex = 9;
            this.CollectionConnection.Text = "None";
            // 
            // ViewCollectionServiceLogs
            // 
            this.ViewCollectionServiceLogs.Location = new System.Drawing.Point(248, 59);
            this.ViewCollectionServiceLogs.Name = "ViewCollectionServiceLogs";
            this.ViewCollectionServiceLogs.Size = new System.Drawing.Size(61, 29);
            this.ViewCollectionServiceLogs.TabIndex = 8;
            this.ViewCollectionServiceLogs.Text = "Logs";
            this.ViewCollectionServiceLogs.UseVisualStyleBackColor = true;
            this.ViewCollectionServiceLogs.Click += new System.EventHandler(this.ViewCollectionServiceLogs_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 28);
            this.label7.Margin = new System.Windows.Forms.Padding(5);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 20);
            this.label7.TabIndex = 7;
            this.label7.Text = "Endpoint:";
            // 
            // CollectionServiceEndpoint
            // 
            this.CollectionServiceEndpoint.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CollectionServiceEndpoint.FormattingEnabled = true;
            this.CollectionServiceEndpoint.Location = new System.Drawing.Point(88, 25);
            this.CollectionServiceEndpoint.Name = "CollectionServiceEndpoint";
            this.CollectionServiceEndpoint.Size = new System.Drawing.Size(154, 28);
            this.CollectionServiceEndpoint.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(61, 326);
            this.label6.Margin = new System.Windows.Forms.Padding(5);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 20);
            this.label6.TabIndex = 5;
            this.label6.Text = "Collected:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(61, 296);
            this.label5.Margin = new System.Windows.Forms.Padding(5);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 20);
            this.label5.TabIndex = 4;
            this.label5.Text = "Warnings:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(61, 266);
            this.label4.Margin = new System.Windows.Forms.Padding(5);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "Errors:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(61, 236);
            this.label3.Margin = new System.Windows.Forms.Padding(5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Uptime:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(61, 206);
            this.label2.Margin = new System.Windows.Forms.Padding(5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "State:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(61, 176);
            this.label1.Margin = new System.Windows.Forms.Padding(5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Connection:";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox7);
            this.tabPage2.Controls.Add(this.groupBox6);
            this.tabPage2.Controls.Add(this.groupBox5);
            this.tabPage2.Controls.Add(this.groupBox4);
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(674, 620);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Configure";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.SaveConfiguration);
            this.groupBox7.Controls.Add(this.LoadConfiguration);
            this.groupBox7.Location = new System.Drawing.Point(400, 6);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(268, 272);
            this.groupBox7.TabIndex = 3;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Configuration manager";
            // 
            // SaveConfiguration
            // 
            this.SaveConfiguration.Location = new System.Drawing.Point(151, 26);
            this.SaveConfiguration.Name = "SaveConfiguration";
            this.SaveConfiguration.Size = new System.Drawing.Size(106, 49);
            this.SaveConfiguration.TabIndex = 1;
            this.SaveConfiguration.Text = "Save configuration";
            this.SaveConfiguration.UseVisualStyleBackColor = true;
            this.SaveConfiguration.Click += new System.EventHandler(this.SaveConfiguration_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.EvaluateThreshold);
            this.groupBox6.Controls.Add(this.label33);
            this.groupBox6.Location = new System.Drawing.Point(355, 284);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(310, 333);
            this.groupBox6.TabIndex = 2;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Analysis service configuration";
            // 
            // EvaluateThreshold
            // 
            this.EvaluateThreshold.Location = new System.Drawing.Point(177, 30);
            this.EvaluateThreshold.Margin = new System.Windows.Forms.Padding(5);
            this.EvaluateThreshold.Name = "EvaluateThreshold";
            this.EvaluateThreshold.Size = new System.Drawing.Size(125, 27);
            this.EvaluateThreshold.TabIndex = 7;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(8, 33);
            this.label33.Margin = new System.Windows.Forms.Padding(5);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(142, 20);
            this.label33.TabIndex = 6;
            this.label33.Text = "Evaluate threshhold:";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.PostQueueSize);
            this.groupBox5.Controls.Add(this.label31);
            this.groupBox5.Controls.Add(this.ScanCommentsDelay);
            this.groupBox5.Controls.Add(this.label30);
            this.groupBox5.Controls.Add(this.label29);
            this.groupBox5.Controls.Add(this.ScanPostDelay);
            this.groupBox5.Location = new System.Drawing.Point(3, 284);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(310, 333);
            this.groupBox5.TabIndex = 1;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Collection service configuration";
            // 
            // PostQueueSize
            // 
            this.PostQueueSize.Location = new System.Drawing.Point(174, 104);
            this.PostQueueSize.Margin = new System.Windows.Forms.Padding(5);
            this.PostQueueSize.Name = "PostQueueSize";
            this.PostQueueSize.Size = new System.Drawing.Size(125, 27);
            this.PostQueueSize.TabIndex = 5;
            // 
            // LoadConfiguration
            // 
            this.LoadConfiguration.Location = new System.Drawing.Point(6, 26);
            this.LoadConfiguration.Name = "LoadConfiguration";
            this.LoadConfiguration.Size = new System.Drawing.Size(106, 49);
            this.LoadConfiguration.TabIndex = 0;
            this.LoadConfiguration.Text = "Load configuration";
            this.LoadConfiguration.UseVisualStyleBackColor = true;
            this.LoadConfiguration.Click += new System.EventHandler(this.LoadConfiguration_Click);
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(8, 107);
            this.label31.Margin = new System.Windows.Forms.Padding(5);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(113, 20);
            this.label31.TabIndex = 4;
            this.label31.Text = "Post queue size:";
            // 
            // ScanCommentsDelay
            // 
            this.ScanCommentsDelay.Location = new System.Drawing.Point(174, 67);
            this.ScanCommentsDelay.Margin = new System.Windows.Forms.Padding(5);
            this.ScanCommentsDelay.Name = "ScanCommentsDelay";
            this.ScanCommentsDelay.Size = new System.Drawing.Size(125, 27);
            this.ScanCommentsDelay.TabIndex = 3;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(8, 70);
            this.label30.Margin = new System.Windows.Forms.Padding(5);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(156, 20);
            this.label30.TabIndex = 2;
            this.label30.Text = "Scan comments delay:";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(8, 33);
            this.label29.Margin = new System.Windows.Forms.Padding(5);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(116, 20);
            this.label29.TabIndex = 1;
            this.label29.Text = "Scan post delay:";
            // 
            // ScanPostDelay
            // 
            this.ScanPostDelay.Location = new System.Drawing.Point(174, 30);
            this.ScanPostDelay.Margin = new System.Windows.Forms.Padding(5);
            this.ScanPostDelay.Name = "ScanPostDelay";
            this.ScanPostDelay.Size = new System.Drawing.Size(125, 27);
            this.ScanPostDelay.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.NewAnalysisHost);
            this.groupBox4.Controls.Add(this.NewCollectionHost);
            this.groupBox4.Controls.Add(this.ConnectionString);
            this.groupBox4.Controls.Add(this.label32);
            this.groupBox4.Controls.Add(this.DeleteAnalysisServiceHost);
            this.groupBox4.Controls.Add(this.DeleteCollectionServiceHost);
            this.groupBox4.Controls.Add(this.AddCollectionServiceHost);
            this.groupBox4.Controls.Add(this.AddAnalysisServiceHost);
            this.groupBox4.Controls.Add(this.label28);
            this.groupBox4.Controls.Add(this.label27);
            this.groupBox4.Controls.Add(this.AnalysisServiceHosts);
            this.groupBox4.Controls.Add(this.CollectionServiceHosts);
            this.groupBox4.Location = new System.Drawing.Point(3, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(391, 275);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Services hosts";
            // 
            // NewAnalysisHost
            // 
            this.NewAnalysisHost.Location = new System.Drawing.Point(194, 162);
            this.NewAnalysisHost.Name = "NewAnalysisHost";
            this.NewAnalysisHost.Size = new System.Drawing.Size(182, 27);
            this.NewAnalysisHost.TabIndex = 9;
            // 
            // NewCollectionHost
            // 
            this.NewCollectionHost.Location = new System.Drawing.Point(6, 162);
            this.NewCollectionHost.Name = "NewCollectionHost";
            this.NewCollectionHost.Size = new System.Drawing.Size(182, 27);
            this.NewCollectionHost.TabIndex = 8;
            // 
            // ConnectionString
            // 
            this.ConnectionString.Location = new System.Drawing.Point(146, 234);
            this.ConnectionString.Margin = new System.Windows.Forms.Padding(5);
            this.ConnectionString.Name = "ConnectionString";
            this.ConnectionString.Size = new System.Drawing.Size(230, 27);
            this.ConnectionString.TabIndex = 6;
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(8, 237);
            this.label32.Margin = new System.Windows.Forms.Padding(5);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(128, 20);
            this.label32.TabIndex = 6;
            this.label32.Text = "Connection string:";
            // 
            // DeleteAnalysisServiceHost
            // 
            this.DeleteAnalysisServiceHost.Location = new System.Drawing.Point(286, 187);
            this.DeleteAnalysisServiceHost.Name = "DeleteAnalysisServiceHost";
            this.DeleteAnalysisServiceHost.Size = new System.Drawing.Size(90, 29);
            this.DeleteAnalysisServiceHost.TabIndex = 7;
            this.DeleteAnalysisServiceHost.Text = "Delete";
            this.DeleteAnalysisServiceHost.UseVisualStyleBackColor = true;
            this.DeleteAnalysisServiceHost.Click += new System.EventHandler(this.DeleteAnalysisServiceHost_Click);
            // 
            // DeleteCollectionServiceHost
            // 
            this.DeleteCollectionServiceHost.Location = new System.Drawing.Point(97, 187);
            this.DeleteCollectionServiceHost.Name = "DeleteCollectionServiceHost";
            this.DeleteCollectionServiceHost.Size = new System.Drawing.Size(91, 29);
            this.DeleteCollectionServiceHost.TabIndex = 5;
            this.DeleteCollectionServiceHost.Text = "Delete";
            this.DeleteCollectionServiceHost.UseVisualStyleBackColor = true;
            this.DeleteCollectionServiceHost.Click += new System.EventHandler(this.DeleteCollectionServiceHost_Click);
            // 
            // AddCollectionServiceHost
            // 
            this.AddCollectionServiceHost.Location = new System.Drawing.Point(6, 187);
            this.AddCollectionServiceHost.Name = "AddCollectionServiceHost";
            this.AddCollectionServiceHost.Size = new System.Drawing.Size(93, 29);
            this.AddCollectionServiceHost.TabIndex = 4;
            this.AddCollectionServiceHost.Text = "Add";
            this.AddCollectionServiceHost.UseVisualStyleBackColor = true;
            this.AddCollectionServiceHost.Click += new System.EventHandler(this.AddCollectionServiceHost_Click);
            // 
            // AddAnalysisServiceHost
            // 
            this.AddAnalysisServiceHost.Location = new System.Drawing.Point(194, 187);
            this.AddAnalysisServiceHost.Name = "AddAnalysisServiceHost";
            this.AddAnalysisServiceHost.Size = new System.Drawing.Size(93, 29);
            this.AddAnalysisServiceHost.TabIndex = 6;
            this.AddAnalysisServiceHost.Text = "Add";
            this.AddAnalysisServiceHost.UseVisualStyleBackColor = true;
            this.AddAnalysisServiceHost.Click += new System.EventHandler(this.AddAnalysisServiceHost_Click);
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(194, 27);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(156, 20);
            this.label28.TabIndex = 3;
            this.label28.Text = "Analysis service hosts: ";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(6, 27);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(170, 20);
            this.label27.TabIndex = 2;
            this.label27.Text = "Collection service hosts: ";
            // 
            // AnalysisServiceHosts
            // 
            this.AnalysisServiceHosts.FormattingEnabled = true;
            this.AnalysisServiceHosts.ItemHeight = 20;
            this.AnalysisServiceHosts.Location = new System.Drawing.Point(194, 58);
            this.AnalysisServiceHosts.Name = "AnalysisServiceHosts";
            this.AnalysisServiceHosts.Size = new System.Drawing.Size(182, 104);
            this.AnalysisServiceHosts.TabIndex = 1;
            // 
            // CollectionServiceHosts
            // 
            this.CollectionServiceHosts.FormattingEnabled = true;
            this.CollectionServiceHosts.ItemHeight = 20;
            this.CollectionServiceHosts.Location = new System.Drawing.Point(6, 58);
            this.CollectionServiceHosts.Name = "CollectionServiceHosts";
            this.CollectionServiceHosts.Size = new System.Drawing.Size(182, 104);
            this.CollectionServiceHosts.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 29);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(674, 620);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Reports";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Location = new System.Drawing.Point(4, 29);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(674, 620);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Models";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tabPage5
            // 
            this.tabPage5.Location = new System.Drawing.Point(4, 29);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(674, 620);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Vk collector";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(682, 653);
            this.Controls.Add(this.Tabs);
            this.Name = "MainForm";
            this.Tabs.ResumeLayout(false);
            this.MainTab.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private TabControl Tabs;
        private TabPage MainTab;
        private TabPage tabPage2;
        private GroupBox groupBox2;
        private GroupBox groupBox1;
        private TabPage tabPage3;
        private TabPage tabPage4;
        private TabPage tabPage5;
        private GroupBox groupBox3;
        private Label label1;
        private Button ViewCollectionServiceLogs;
        private Label label7;
        private Label label6;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label20;
        private Label label26;
        private Label label25;
        private Label label24;
        private Label label23;
        private Label label22;
        private Button ViewAnalysisServiceLogs;
        private Label label21;
        private Button StartAll;
        private Button StopAnalysisService;
        private Button StopCollectionService;
        private Button ClearEvaluatedDatabase;
        private Button ClearCommentsDatabase;
        private Button StartAnalysisService;
        private Button StartCollectionService;
        private Button RefreshAnalysisServiceInfo;
        private Button RefreshCollectionServiceInfo;
        private Button StopAll;
        private Button ApplyAnalysisServiceEndpoint;
        private Button ApplyNewCollectionServiceEndpoint;
        private GroupBox groupBox6;
        private GroupBox groupBox5;
        private GroupBox groupBox4;
        private Button DeleteAnalysisServiceHost;
        private Button DeleteCollectionServiceHost;
        private Button AddCollectionServiceHost;
        private Button AddAnalysisServiceHost;
        private Label label28;
        private Label label27;
        private TextBox ScanPostDelay;
        private TextBox EvaluateThreshold;
        private Label label33;
        private TextBox PostQueueSize;
        private Label label31;
        private TextBox ScanCommentsDelay;
        private Label label30;
        private Label label29;
        private Label label32;
        private GroupBox groupBox7;
        private Button SaveConfiguration;
        private Button LoadConfiguration;
        public Label CollectionCollected;
        public Label CollectionWarnings;
        public Label CollectionErrors;
        public Label CollectionUptime;
        public Label CollectionState;
        public Label CollectionConnection;
        public ComboBox CollectionServiceEndpoint;
        public Label AnalysisEvaluated;
        public Label AnalysisWarnings;
        public Label AnalysisErrors;
        public Label AnalysisUptime;
        public Label AnalysisState;
        public Label AnalysisConnection;
        public ComboBox AnalysisServiceEndpoint;
        public ListBox AnalysisServiceHosts;
        public ListBox CollectionServiceHosts;
        public TextBox ConnectionString;
        private TextBox NewAnalysisHost;
        private TextBox NewCollectionHost;
        private DateTimePicker AnalysisLogDate;
        private DateTimePicker CollectionLogDate;
    }
}