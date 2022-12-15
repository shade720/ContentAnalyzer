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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
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
            this.ApplyAnalysisServiceEndpoint = new System.Windows.Forms.Button();
            this.RefreshAnalysisServiceInfo = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.ViewAnalysisServiceLogs = new System.Windows.Forms.Button();
            this.label21 = new System.Windows.Forms.Label();
            this.AnalysisServiceEndpoint = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ApplyNewCollectionServiceEndpoint = new System.Windows.Forms.Button();
            this.RefreshCollectionServiceInfo = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
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
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(682, 653);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(674, 620);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Main";
            this.tabPage1.UseVisualStyleBackColor = true;
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
            this.groupBox3.Location = new System.Drawing.Point(6, 312);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(660, 300);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Control";
            // 
            // StartAll
            // 
            this.StartAll.Location = new System.Drawing.Point(479, 181);
            this.StartAll.Name = "StartAll";
            this.StartAll.Size = new System.Drawing.Size(175, 50);
            this.StartAll.TabIndex = 6;
            this.StartAll.Text = "Start all";
            this.StartAll.UseVisualStyleBackColor = true;
            this.StartAll.Click += new System.EventHandler(this.StartAll_Click);
            // 
            // ClearEvaluatedDatabase
            // 
            this.ClearEvaluatedDatabase.Location = new System.Drawing.Point(192, 237);
            this.ClearEvaluatedDatabase.Name = "ClearEvaluatedDatabase";
            this.ClearEvaluatedDatabase.Size = new System.Drawing.Size(175, 50);
            this.ClearEvaluatedDatabase.TabIndex = 3;
            this.ClearEvaluatedDatabase.Text = "Clear evaluated database";
            this.ClearEvaluatedDatabase.UseVisualStyleBackColor = true;
            this.ClearEvaluatedDatabase.Click += new System.EventHandler(this.ClearEvaluatedDatabase_Click);
            // 
            // ClearCommentsDatabase
            // 
            this.ClearCommentsDatabase.Location = new System.Drawing.Point(11, 237);
            this.ClearCommentsDatabase.Name = "ClearCommentsDatabase";
            this.ClearCommentsDatabase.Size = new System.Drawing.Size(175, 50);
            this.ClearCommentsDatabase.TabIndex = 2;
            this.ClearCommentsDatabase.Text = "Clear comments database";
            this.ClearCommentsDatabase.UseVisualStyleBackColor = true;
            this.ClearCommentsDatabase.Click += new System.EventHandler(this.ClearCommentsDatabase_Click);
            // 
            // StartAnalysisService
            // 
            this.StartAnalysisService.Location = new System.Drawing.Point(192, 181);
            this.StartAnalysisService.Name = "StartAnalysisService";
            this.StartAnalysisService.Size = new System.Drawing.Size(175, 50);
            this.StartAnalysisService.TabIndex = 1;
            this.StartAnalysisService.Text = "Start analysis service";
            this.StartAnalysisService.UseVisualStyleBackColor = true;
            this.StartAnalysisService.Click += new System.EventHandler(this.StartAnalysisService_Click);
            // 
            // StartCollectionService
            // 
            this.StartCollectionService.Location = new System.Drawing.Point(11, 181);
            this.StartCollectionService.Name = "StartCollectionService";
            this.StartCollectionService.Size = new System.Drawing.Size(175, 50);
            this.StartCollectionService.TabIndex = 0;
            this.StartCollectionService.Text = "Start collection service";
            this.StartCollectionService.UseVisualStyleBackColor = true;
            this.StartCollectionService.Click += new System.EventHandler(this.StartCollectionService_Click);
            // 
            // StopAll
            // 
            this.StopAll.Location = new System.Drawing.Point(479, 181);
            this.StopAll.Name = "StopAll";
            this.StopAll.Size = new System.Drawing.Size(175, 50);
            this.StopAll.TabIndex = 7;
            this.StopAll.Text = "Stop all";
            this.StopAll.UseVisualStyleBackColor = true;
            this.StopAll.Click += new System.EventHandler(this.StopAll_Click);
            // 
            // StopAnalysisService
            // 
            this.StopAnalysisService.Location = new System.Drawing.Point(192, 181);
            this.StopAnalysisService.Name = "StopAnalysisService";
            this.StopAnalysisService.Size = new System.Drawing.Size(175, 50);
            this.StopAnalysisService.TabIndex = 5;
            this.StopAnalysisService.Text = "Stop analysis service";
            this.StopAnalysisService.UseVisualStyleBackColor = true;
            this.StopAnalysisService.Click += new System.EventHandler(this.StopAnalysisService_Click);
            // 
            // StopCollectionService
            // 
            this.StopCollectionService.Location = new System.Drawing.Point(11, 181);
            this.StopCollectionService.Name = "StopCollectionService";
            this.StopCollectionService.Size = new System.Drawing.Size(175, 50);
            this.StopCollectionService.TabIndex = 4;
            this.StopCollectionService.Text = "Stop collection service";
            this.StopCollectionService.UseVisualStyleBackColor = true;
            this.StopCollectionService.Click += new System.EventHandler(this.StopCollectionService_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ApplyAnalysisServiceEndpoint);
            this.groupBox2.Controls.Add(this.RefreshAnalysisServiceInfo);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.label20);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.label26);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.label25);
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Controls.Add(this.label24);
            this.groupBox2.Controls.Add(this.label18);
            this.groupBox2.Controls.Add(this.label23);
            this.groupBox2.Controls.Add(this.label19);
            this.groupBox2.Controls.Add(this.label22);
            this.groupBox2.Controls.Add(this.ViewAnalysisServiceLogs);
            this.groupBox2.Controls.Add(this.label21);
            this.groupBox2.Controls.Add(this.AnalysisServiceEndpoint);
            this.groupBox2.Location = new System.Drawing.Point(353, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(315, 300);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Collection service info";
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
            this.RefreshAnalysisServiceInfo.Location = new System.Drawing.Point(0, 271);
            this.RefreshAnalysisServiceInfo.Name = "RefreshAnalysisServiceInfo";
            this.RefreshAnalysisServiceInfo.Size = new System.Drawing.Size(94, 29);
            this.RefreshAnalysisServiceInfo.TabIndex = 16;
            this.RefreshAnalysisServiceInfo.Text = "Refresh";
            this.RefreshAnalysisServiceInfo.UseVisualStyleBackColor = true;
            this.RefreshAnalysisServiceInfo.Click += new System.EventHandler(this.RefreshAnalysisServiceInfo_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(105, 226);
            this.label14.Margin = new System.Windows.Forms.Padding(5);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(45, 20);
            this.label14.TabIndex = 29;
            this.label14.Text = "None";
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
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(105, 196);
            this.label15.Margin = new System.Windows.Forms.Padding(5);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(45, 20);
            this.label15.TabIndex = 28;
            this.label15.Text = "None";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(8, 76);
            this.label26.Margin = new System.Windows.Forms.Padding(5);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(87, 20);
            this.label26.TabIndex = 15;
            this.label26.Text = "Connection:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(105, 166);
            this.label16.Margin = new System.Windows.Forms.Padding(5);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(45, 20);
            this.label16.TabIndex = 27;
            this.label16.Text = "None";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(8, 106);
            this.label25.Margin = new System.Windows.Forms.Padding(5);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(46, 20);
            this.label25.TabIndex = 16;
            this.label25.Text = "State:";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(105, 136);
            this.label17.Margin = new System.Windows.Forms.Padding(5);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(45, 20);
            this.label17.TabIndex = 26;
            this.label17.Text = "None";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(8, 136);
            this.label24.Margin = new System.Windows.Forms.Padding(5);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(61, 20);
            this.label24.TabIndex = 17;
            this.label24.Text = "Uptime:";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(105, 106);
            this.label18.Margin = new System.Windows.Forms.Padding(5);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(45, 20);
            this.label18.TabIndex = 25;
            this.label18.Text = "None";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(8, 166);
            this.label23.Margin = new System.Windows.Forms.Padding(5);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(50, 20);
            this.label23.TabIndex = 18;
            this.label23.Text = "Errors:";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(105, 76);
            this.label19.Margin = new System.Windows.Forms.Padding(5);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(45, 20);
            this.label19.TabIndex = 24;
            this.label19.Text = "None";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(8, 196);
            this.label22.Margin = new System.Windows.Forms.Padding(5);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(73, 20);
            this.label22.TabIndex = 19;
            this.label22.Text = "Warnings:";
            // 
            // ViewAnalysisServiceLogs
            // 
            this.ViewAnalysisServiceLogs.Location = new System.Drawing.Point(222, 271);
            this.ViewAnalysisServiceLogs.Name = "ViewAnalysisServiceLogs";
            this.ViewAnalysisServiceLogs.Size = new System.Drawing.Size(94, 29);
            this.ViewAnalysisServiceLogs.TabIndex = 23;
            this.ViewAnalysisServiceLogs.Text = "Logs";
            this.ViewAnalysisServiceLogs.UseVisualStyleBackColor = true;
            this.ViewAnalysisServiceLogs.Click += new System.EventHandler(this.ViewAnalysisServiceLogs_Click);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(8, 226);
            this.label21.Margin = new System.Windows.Forms.Padding(5);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(77, 20);
            this.label21.TabIndex = 20;
            this.label21.Text = "Evaluated:";
            // 
            // AnalysisServiceEndpoint
            // 
            this.AnalysisServiceEndpoint.FormattingEnabled = true;
            this.AnalysisServiceEndpoint.Location = new System.Drawing.Point(88, 25);
            this.AnalysisServiceEndpoint.Name = "AnalysisServiceEndpoint";
            this.AnalysisServiceEndpoint.Size = new System.Drawing.Size(156, 28);
            this.AnalysisServiceEndpoint.TabIndex = 21;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ApplyNewCollectionServiceEndpoint);
            this.groupBox1.Controls.Add(this.RefreshCollectionServiceInfo);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
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
            this.groupBox1.Size = new System.Drawing.Size(315, 300);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Collection service info";
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
            this.RefreshCollectionServiceInfo.Location = new System.Drawing.Point(0, 271);
            this.RefreshCollectionServiceInfo.Name = "RefreshCollectionServiceInfo";
            this.RefreshCollectionServiceInfo.Size = new System.Drawing.Size(94, 29);
            this.RefreshCollectionServiceInfo.TabIndex = 15;
            this.RefreshCollectionServiceInfo.Text = "Refresh";
            this.RefreshCollectionServiceInfo.UseVisualStyleBackColor = true;
            this.RefreshCollectionServiceInfo.Click += new System.EventHandler(this.RefreshCollectionServiceInfo_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(105, 226);
            this.label13.Margin = new System.Windows.Forms.Padding(5);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(45, 20);
            this.label13.TabIndex = 14;
            this.label13.Text = "None";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(105, 196);
            this.label12.Margin = new System.Windows.Forms.Padding(5);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(45, 20);
            this.label12.TabIndex = 13;
            this.label12.Text = "None";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(105, 166);
            this.label11.Margin = new System.Windows.Forms.Padding(5);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(45, 20);
            this.label11.TabIndex = 12;
            this.label11.Text = "None";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(105, 136);
            this.label10.Margin = new System.Windows.Forms.Padding(5);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(45, 20);
            this.label10.TabIndex = 11;
            this.label10.Text = "None";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(105, 106);
            this.label9.Margin = new System.Windows.Forms.Padding(5);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(45, 20);
            this.label9.TabIndex = 10;
            this.label9.Text = "None";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(105, 76);
            this.label8.Margin = new System.Windows.Forms.Padding(5);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(45, 20);
            this.label8.TabIndex = 9;
            this.label8.Text = "None";
            // 
            // ViewCollectionServiceLogs
            // 
            this.ViewCollectionServiceLogs.Location = new System.Drawing.Point(222, 271);
            this.ViewCollectionServiceLogs.Name = "ViewCollectionServiceLogs";
            this.ViewCollectionServiceLogs.Size = new System.Drawing.Size(94, 29);
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
            this.CollectionServiceEndpoint.FormattingEnabled = true;
            this.CollectionServiceEndpoint.Location = new System.Drawing.Point(88, 25);
            this.CollectionServiceEndpoint.Name = "CollectionServiceEndpoint";
            this.CollectionServiceEndpoint.Size = new System.Drawing.Size(154, 28);
            this.CollectionServiceEndpoint.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 226);
            this.label6.Margin = new System.Windows.Forms.Padding(5);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 20);
            this.label6.TabIndex = 5;
            this.label6.Text = "Collected:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 196);
            this.label5.Margin = new System.Windows.Forms.Padding(5);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 20);
            this.label5.TabIndex = 4;
            this.label5.Text = "Warnings:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 166);
            this.label4.Margin = new System.Windows.Forms.Padding(5);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "Errors:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 136);
            this.label3.Margin = new System.Windows.Forms.Padding(5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Uptime:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 106);
            this.label2.Margin = new System.Windows.Forms.Padding(5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "State:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 76);
            this.label1.Margin = new System.Windows.Forms.Padding(5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Connection:";
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(674, 620);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Configure";
            this.tabPage2.UseVisualStyleBackColor = true;
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
            this.Controls.Add(this.tabControl1);
            this.Name = "MainForm";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private GroupBox groupBox2;
        private GroupBox groupBox1;
        private TabPage tabPage3;
        private TabPage tabPage4;
        private TabPage tabPage5;
        private GroupBox groupBox3;
        private Label label1;
        private Label label13;
        private Label label12;
        private Label label11;
        private Label label10;
        private Label label9;
        private Label label8;
        private Button ViewCollectionServiceLogs;
        private Label label7;
        private ComboBox CollectionServiceEndpoint;
        private Label label6;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label14;
        private Label label20;
        private Label label15;
        private Label label26;
        private Label label16;
        private Label label25;
        private Label label17;
        private Label label24;
        private Label label18;
        private Label label23;
        private Label label19;
        private Label label22;
        private Button ViewAnalysisServiceLogs;
        private Label label21;
        private ComboBox AnalysisServiceEndpoint;
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
    }
}