namespace Interface.Forms
{
    partial class MainWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.UpperPanel = new System.Windows.Forms.Panel();
            this.MinimizeWindowButton = new System.Windows.Forms.Button();
            this.CloseButton = new System.Windows.Forms.Button();
            this.BottomPanel = new System.Windows.Forms.Panel();
            this.ErrorsCountLabel = new System.Windows.Forms.Label();
            this.AnalysisServiceStateLabel = new System.Windows.Forms.Label();
            this.UptimeLabel = new System.Windows.Forms.Label();
            this.StatePanel = new System.Windows.Forms.Panel();
            this.StateLabel = new System.Windows.Forms.Label();
            this.CollectorServiceStateLabel = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.LeftPanel = new System.Windows.Forms.Panel();
            this.TitleLabel = new System.Windows.Forms.Label();
            this.StartStopButtonContainer = new System.Windows.Forms.Panel();
            this.StartServiceButton = new System.Windows.Forms.Button();
            this.StopServiceButton = new System.Windows.Forms.Button();
            this.ShowAllCommentsButton = new System.Windows.Forms.Button();
            this.ShowSelectedComments = new System.Windows.Forms.Button();
            this.ConfigureServiceButton = new System.Windows.Forms.Button();
            this.ViewLogsButton = new System.Windows.Forms.Button();
            this.ServiceInfoPanel = new System.Windows.Forms.Panel();
            this.SelectedCommentsFoundLabel = new System.Windows.Forms.Label();
            this.CommentsFoundLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.CentralPanel = new System.Windows.Forms.Panel();
            this.UpperPanel.SuspendLayout();
            this.BottomPanel.SuspendLayout();
            this.StatePanel.SuspendLayout();
            this.LeftPanel.SuspendLayout();
            this.StartStopButtonContainer.SuspendLayout();
            this.ServiceInfoPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // UpperPanel
            // 
            this.UpperPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UpperPanel.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.UpperPanel.Controls.Add(this.MinimizeWindowButton);
            this.UpperPanel.Controls.Add(this.CloseButton);
            this.UpperPanel.Location = new System.Drawing.Point(0, 0);
            this.UpperPanel.Name = "UpperPanel";
            this.UpperPanel.Size = new System.Drawing.Size(1382, 44);
            this.UpperPanel.TabIndex = 1;
            this.UpperPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.UpperPanel_MouseDown);
            this.UpperPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.UpperPanel_MouseMove);
            // 
            // MinimizeWindowButton
            // 
            this.MinimizeWindowButton.BackColor = System.Drawing.SystemColors.Window;
            this.MinimizeWindowButton.FlatAppearance.BorderSize = 0;
            this.MinimizeWindowButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MinimizeWindowButton.Image = ((System.Drawing.Image)(resources.GetObject("MinimizeWindowButton.Image")));
            this.MinimizeWindowButton.Location = new System.Drawing.Point(1294, 6);
            this.MinimizeWindowButton.Name = "MinimizeWindowButton";
            this.MinimizeWindowButton.Size = new System.Drawing.Size(33, 33);
            this.MinimizeWindowButton.TabIndex = 1;
            this.MinimizeWindowButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.MinimizeWindowButton.UseVisualStyleBackColor = false;
            this.MinimizeWindowButton.Click += new System.EventHandler(this.MinimizeWindowButton_Click);
            // 
            // CloseButton
            // 
            this.CloseButton.BackColor = System.Drawing.SystemColors.Window;
            this.CloseButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CloseButton.FlatAppearance.BorderSize = 0;
            this.CloseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CloseButton.Image = ((System.Drawing.Image)(resources.GetObject("CloseButton.Image")));
            this.CloseButton.Location = new System.Drawing.Point(1335, 4);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(35, 35);
            this.CloseButton.TabIndex = 0;
            this.CloseButton.UseVisualStyleBackColor = false;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // BottomPanel
            // 
            this.BottomPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BottomPanel.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.BottomPanel.Controls.Add(this.ErrorsCountLabel);
            this.BottomPanel.Controls.Add(this.AnalysisServiceStateLabel);
            this.BottomPanel.Controls.Add(this.UptimeLabel);
            this.BottomPanel.Controls.Add(this.StatePanel);
            this.BottomPanel.Controls.Add(this.CollectorServiceStateLabel);
            this.BottomPanel.Controls.Add(this.label6);
            this.BottomPanel.Controls.Add(this.label5);
            this.BottomPanel.Controls.Add(this.label7);
            this.BottomPanel.Controls.Add(this.label3);
            this.BottomPanel.Location = new System.Drawing.Point(0, 908);
            this.BottomPanel.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.BottomPanel.Name = "BottomPanel";
            this.BottomPanel.Size = new System.Drawing.Size(1382, 74);
            this.BottomPanel.TabIndex = 2;
            // 
            // ErrorsCountLabel
            // 
            this.ErrorsCountLabel.AutoSize = true;
            this.ErrorsCountLabel.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ErrorsCountLabel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ErrorsCountLabel.Location = new System.Drawing.Point(556, 43);
            this.ErrorsCountLabel.Name = "ErrorsCountLabel";
            this.ErrorsCountLabel.Size = new System.Drawing.Size(17, 20);
            this.ErrorsCountLabel.TabIndex = 10;
            this.ErrorsCountLabel.Text = "0";
            // 
            // AnalysisServiceStateLabel
            // 
            this.AnalysisServiceStateLabel.AutoSize = true;
            this.AnalysisServiceStateLabel.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.AnalysisServiceStateLabel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.AnalysisServiceStateLabel.Location = new System.Drawing.Point(420, 43);
            this.AnalysisServiceStateLabel.Name = "AnalysisServiceStateLabel";
            this.AnalysisServiceStateLabel.Size = new System.Drawing.Size(30, 20);
            this.AnalysisServiceStateLabel.TabIndex = 12;
            this.AnalysisServiceStateLabel.Text = "Up";
            // 
            // UptimeLabel
            // 
            this.UptimeLabel.AutoSize = true;
            this.UptimeLabel.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.UptimeLabel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.UptimeLabel.Location = new System.Drawing.Point(556, 14);
            this.UptimeLabel.Name = "UptimeLabel";
            this.UptimeLabel.Size = new System.Drawing.Size(17, 20);
            this.UptimeLabel.TabIndex = 9;
            this.UptimeLabel.Text = "0";
            // 
            // StatePanel
            // 
            this.StatePanel.BackColor = System.Drawing.Color.Red;
            this.StatePanel.Controls.Add(this.StateLabel);
            this.StatePanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.StatePanel.Location = new System.Drawing.Point(0, 0);
            this.StatePanel.Name = "StatePanel";
            this.StatePanel.Size = new System.Drawing.Size(275, 74);
            this.StatePanel.TabIndex = 4;
            // 
            // StateLabel
            // 
            this.StateLabel.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.StateLabel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.StateLabel.Location = new System.Drawing.Point(71, 27);
            this.StateLabel.Name = "StateLabel";
            this.StateLabel.Size = new System.Drawing.Size(127, 23);
            this.StateLabel.TabIndex = 0;
            this.StateLabel.Text = "Not working";
            this.StateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CollectorServiceStateLabel
            // 
            this.CollectorServiceStateLabel.AutoSize = true;
            this.CollectorServiceStateLabel.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.CollectorServiceStateLabel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.CollectorServiceStateLabel.Location = new System.Drawing.Point(420, 14);
            this.CollectorServiceStateLabel.Name = "CollectorServiceStateLabel";
            this.CollectorServiceStateLabel.Size = new System.Drawing.Size(30, 20);
            this.CollectorServiceStateLabel.TabIndex = 11;
            this.CollectorServiceStateLabel.Text = "Up";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label6.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label6.Location = new System.Drawing.Point(278, 14);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(143, 20);
            this.label6.TabIndex = 5;
            this.label6.Text = "Collector service: ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label5.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label5.Location = new System.Drawing.Point(501, 43);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 20);
            this.label5.TabIndex = 4;
            this.label5.Text = "Errors: ";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label7.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label7.Location = new System.Drawing.Point(290, 43);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(131, 20);
            this.label7.TabIndex = 6;
            this.label7.Text = "Analysis service: ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label3.Location = new System.Drawing.Point(488, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Uptime: ";
            // 
            // LeftPanel
            // 
            this.LeftPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.LeftPanel.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.LeftPanel.Controls.Add(this.TitleLabel);
            this.LeftPanel.Controls.Add(this.StartStopButtonContainer);
            this.LeftPanel.Controls.Add(this.ShowAllCommentsButton);
            this.LeftPanel.Controls.Add(this.ShowSelectedComments);
            this.LeftPanel.Controls.Add(this.ConfigureServiceButton);
            this.LeftPanel.Controls.Add(this.ViewLogsButton);
            this.LeftPanel.Controls.Add(this.ServiceInfoPanel);
            this.LeftPanel.Location = new System.Drawing.Point(0, 44);
            this.LeftPanel.Name = "LeftPanel";
            this.LeftPanel.Size = new System.Drawing.Size(275, 938);
            this.LeftPanel.TabIndex = 3;
            // 
            // TitleLabel
            // 
            this.TitleLabel.BackColor = System.Drawing.SystemColors.Menu;
            this.TitleLabel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.TitleLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TitleLabel.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.TitleLabel.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.TitleLabel.Location = new System.Drawing.Point(0, 1);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Size = new System.Drawing.Size(275, 70);
            this.TitleLabel.TabIndex = 1;
            this.TitleLabel.Text = "Content Analyzer";
            this.TitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // StartStopButtonContainer
            // 
            this.StartStopButtonContainer.Controls.Add(this.StartServiceButton);
            this.StartStopButtonContainer.Controls.Add(this.StopServiceButton);
            this.StartStopButtonContainer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.StartStopButtonContainer.Location = new System.Drawing.Point(0, 71);
            this.StartStopButtonContainer.Margin = new System.Windows.Forms.Padding(0);
            this.StartStopButtonContainer.Name = "StartStopButtonContainer";
            this.StartStopButtonContainer.Size = new System.Drawing.Size(275, 90);
            this.StartStopButtonContainer.TabIndex = 0;
            // 
            // StartServiceButton
            // 
            this.StartServiceButton.BackColor = System.Drawing.SystemColors.Menu;
            this.StartServiceButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StartServiceButton.FlatAppearance.BorderSize = 0;
            this.StartServiceButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.StartServiceButton.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.StartServiceButton.ForeColor = System.Drawing.SystemColors.Highlight;
            this.StartServiceButton.Location = new System.Drawing.Point(0, 0);
            this.StartServiceButton.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.StartServiceButton.Name = "StartServiceButton";
            this.StartServiceButton.Size = new System.Drawing.Size(275, 90);
            this.StartServiceButton.TabIndex = 0;
            this.StartServiceButton.Text = "Start Service";
            this.StartServiceButton.UseVisualStyleBackColor = false;
            this.StartServiceButton.Click += new System.EventHandler(this.StartDataCollectionServiceButton_Click);
            // 
            // StopServiceButton
            // 
            this.StopServiceButton.BackColor = System.Drawing.SystemColors.Menu;
            this.StopServiceButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StopServiceButton.FlatAppearance.BorderSize = 0;
            this.StopServiceButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.StopServiceButton.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.StopServiceButton.ForeColor = System.Drawing.SystemColors.Highlight;
            this.StopServiceButton.Location = new System.Drawing.Point(0, 0);
            this.StopServiceButton.Name = "StopServiceButton";
            this.StopServiceButton.Size = new System.Drawing.Size(275, 90);
            this.StopServiceButton.TabIndex = 1;
            this.StopServiceButton.Text = "Stop Service";
            this.StopServiceButton.UseVisualStyleBackColor = false;
            this.StopServiceButton.Click += new System.EventHandler(this.StopDataCollectionServiceButton_Click);
            // 
            // ShowAllCommentsButton
            // 
            this.ShowAllCommentsButton.BackColor = System.Drawing.SystemColors.Menu;
            this.ShowAllCommentsButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ShowAllCommentsButton.FlatAppearance.BorderSize = 0;
            this.ShowAllCommentsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ShowAllCommentsButton.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ShowAllCommentsButton.ForeColor = System.Drawing.SystemColors.Highlight;
            this.ShowAllCommentsButton.Location = new System.Drawing.Point(0, 161);
            this.ShowAllCommentsButton.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.ShowAllCommentsButton.Name = "ShowAllCommentsButton";
            this.ShowAllCommentsButton.Size = new System.Drawing.Size(275, 90);
            this.ShowAllCommentsButton.TabIndex = 2;
            this.ShowAllCommentsButton.Text = "All comments";
            this.ShowAllCommentsButton.UseVisualStyleBackColor = false;
            this.ShowAllCommentsButton.Click += new System.EventHandler(this.ShowAllCommentsButton_Click);
            // 
            // ShowSelectedComments
            // 
            this.ShowSelectedComments.BackColor = System.Drawing.SystemColors.Menu;
            this.ShowSelectedComments.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ShowSelectedComments.FlatAppearance.BorderSize = 0;
            this.ShowSelectedComments.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ShowSelectedComments.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ShowSelectedComments.ForeColor = System.Drawing.SystemColors.Highlight;
            this.ShowSelectedComments.Location = new System.Drawing.Point(0, 251);
            this.ShowSelectedComments.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.ShowSelectedComments.Name = "ShowSelectedComments";
            this.ShowSelectedComments.Size = new System.Drawing.Size(275, 90);
            this.ShowSelectedComments.TabIndex = 11;
            this.ShowSelectedComments.Text = "Selected comments";
            this.ShowSelectedComments.UseVisualStyleBackColor = false;
            this.ShowSelectedComments.Click += new System.EventHandler(this.ShowSelectedComments_Click);
            // 
            // ConfigureServiceButton
            // 
            this.ConfigureServiceButton.BackColor = System.Drawing.SystemColors.Menu;
            this.ConfigureServiceButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ConfigureServiceButton.FlatAppearance.BorderSize = 0;
            this.ConfigureServiceButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ConfigureServiceButton.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ConfigureServiceButton.ForeColor = System.Drawing.SystemColors.Highlight;
            this.ConfigureServiceButton.Location = new System.Drawing.Point(0, 341);
            this.ConfigureServiceButton.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.ConfigureServiceButton.Name = "ConfigureServiceButton";
            this.ConfigureServiceButton.Size = new System.Drawing.Size(275, 90);
            this.ConfigureServiceButton.TabIndex = 0;
            this.ConfigureServiceButton.Text = "Configure";
            this.ConfigureServiceButton.UseVisualStyleBackColor = false;
            this.ConfigureServiceButton.Click += new System.EventHandler(this.ConfigureServiceButton_Click);
            // 
            // ViewLogsButton
            // 
            this.ViewLogsButton.BackColor = System.Drawing.SystemColors.Menu;
            this.ViewLogsButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ViewLogsButton.FlatAppearance.BorderSize = 0;
            this.ViewLogsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ViewLogsButton.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ViewLogsButton.ForeColor = System.Drawing.SystemColors.Highlight;
            this.ViewLogsButton.Location = new System.Drawing.Point(0, 431);
            this.ViewLogsButton.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.ViewLogsButton.Name = "ViewLogsButton";
            this.ViewLogsButton.Size = new System.Drawing.Size(275, 90);
            this.ViewLogsButton.TabIndex = 10;
            this.ViewLogsButton.Text = "Logs";
            this.ViewLogsButton.UseVisualStyleBackColor = false;
            this.ViewLogsButton.Click += new System.EventHandler(this.ViewLogsButton_Click);
            // 
            // ServiceInfoPanel
            // 
            this.ServiceInfoPanel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ServiceInfoPanel.Controls.Add(this.SelectedCommentsFoundLabel);
            this.ServiceInfoPanel.Controls.Add(this.CommentsFoundLabel);
            this.ServiceInfoPanel.Controls.Add(this.label4);
            this.ServiceInfoPanel.Controls.Add(this.label2);
            this.ServiceInfoPanel.Controls.Add(this.label1);
            this.ServiceInfoPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ServiceInfoPanel.Location = new System.Drawing.Point(0, 521);
            this.ServiceInfoPanel.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.ServiceInfoPanel.Name = "ServiceInfoPanel";
            this.ServiceInfoPanel.Size = new System.Drawing.Size(275, 417);
            this.ServiceInfoPanel.TabIndex = 9;
            // 
            // SelectedCommentsFoundLabel
            // 
            this.SelectedCommentsFoundLabel.AutoSize = true;
            this.SelectedCommentsFoundLabel.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.SelectedCommentsFoundLabel.Location = new System.Drawing.Point(191, 90);
            this.SelectedCommentsFoundLabel.Name = "SelectedCommentsFoundLabel";
            this.SelectedCommentsFoundLabel.Size = new System.Drawing.Size(17, 20);
            this.SelectedCommentsFoundLabel.TabIndex = 8;
            this.SelectedCommentsFoundLabel.Text = "0";
            // 
            // CommentsFoundLabel
            // 
            this.CommentsFoundLabel.AutoSize = true;
            this.CommentsFoundLabel.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.CommentsFoundLabel.Location = new System.Drawing.Point(191, 59);
            this.CommentsFoundLabel.Name = "CommentsFoundLabel";
            this.CommentsFoundLabel.Size = new System.Drawing.Size(17, 20);
            this.CommentsFoundLabel.TabIndex = 7;
            this.CommentsFoundLabel.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label4.Location = new System.Drawing.Point(79, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(106, 21);
            this.label4.TabIndex = 3;
            this.label4.Text = "Service Info";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(30, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(162, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Selected comments: ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(50, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(142, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Comments found: ";
            // 
            // CentralPanel
            // 
            this.CentralPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CentralPanel.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.CentralPanel.Location = new System.Drawing.Point(275, 44);
            this.CentralPanel.Margin = new System.Windows.Forms.Padding(0);
            this.CentralPanel.Name = "CentralPanel";
            this.CentralPanel.Size = new System.Drawing.Size(1107, 864);
            this.CentralPanel.TabIndex = 6;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1382, 982);
            this.ControlBox = false;
            this.Controls.Add(this.CentralPanel);
            this.Controls.Add(this.BottomPanel);
            this.Controls.Add(this.LeftPanel);
            this.Controls.Add(this.UpperPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.UpperPanel.ResumeLayout(false);
            this.BottomPanel.ResumeLayout(false);
            this.BottomPanel.PerformLayout();
            this.StatePanel.ResumeLayout(false);
            this.LeftPanel.ResumeLayout(false);
            this.StartStopButtonContainer.ResumeLayout(false);
            this.ServiceInfoPanel.ResumeLayout(false);
            this.ServiceInfoPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private Panel UpperPanel;
        private Panel BottomPanel;
        private Panel LeftPanel;
        private Panel StatePanel;
        private Panel CentralPanel;
        private Button ShowAllCommentsButton;
        private Label TitleLabel;
        private Button StartServiceButton;
        private Panel ServiceInfoPanel;
        private Button ConfigureServiceButton;
        private Button StopServiceButton;
        private Button ViewLogsButton;
        private Button ShowSelectedComments;
        private Label StateLabel;
        private Panel StartStopButtonContainer;
        private Label label7;
        private Label label6;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        public Label AnalysisServiceStateLabel;
        public Label CollectorServiceStateLabel;
        public Label ErrorsCountLabel;
        public Label UptimeLabel;
        public Label SelectedCommentsFoundLabel;
        public Label CommentsFoundLabel;
        private Button CloseButton;
        private Button MinimizeWindowButton;
    }
}