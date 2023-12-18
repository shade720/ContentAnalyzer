namespace ContentAnalyzer.Frontend.Desktop.Forms
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
            UpperPanel = new Panel();
            MinimizeWindowButton = new Button();
            CloseButton = new Button();
            BottomPanel = new Panel();
            ErrorsCountLabel = new Label();
            AnalysisServiceStateLabel = new Label();
            UptimeLabel = new Label();
            StatePanel = new Panel();
            StateLabel = new Label();
            CollectorServiceStateLabel = new Label();
            label6 = new Label();
            label5 = new Label();
            label7 = new Label();
            label3 = new Label();
            LeftPanel = new Panel();
            TitleLabel = new Label();
            StartStopButtonContainer = new Panel();
            StartServiceButton = new Button();
            StopServiceButton = new Button();
            ShowAllCommentsButton = new Button();
            ShowSelectedComments = new Button();
            ConfigureServiceButton = new Button();
            ViewLogsButton = new Button();
            ServiceInfoPanel = new Panel();
            SelectedCommentsFoundLabel = new Label();
            CommentsFoundLabel = new Label();
            label4 = new Label();
            label2 = new Label();
            label1 = new Label();
            CentralPanel = new Panel();
            UpperPanel.SuspendLayout();
            BottomPanel.SuspendLayout();
            StatePanel.SuspendLayout();
            LeftPanel.SuspendLayout();
            StartStopButtonContainer.SuspendLayout();
            ServiceInfoPanel.SuspendLayout();
            SuspendLayout();
            // 
            // UpperPanel
            // 
            UpperPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            UpperPanel.BackColor = Color.FromArgb(51, 153, 255);
            UpperPanel.Controls.Add(MinimizeWindowButton);
            UpperPanel.Controls.Add(CloseButton);
            UpperPanel.Location = new Point(0, 0);
            UpperPanel.Margin = new Padding(3, 2, 3, 2);
            UpperPanel.Name = "UpperPanel";
            UpperPanel.Size = new Size(1209, 33);
            UpperPanel.TabIndex = 1;
            UpperPanel.MouseDown += UpperPanel_MouseDown;
            UpperPanel.MouseMove += UpperPanel_MouseMove;
            // 
            // MinimizeWindowButton
            // 
            MinimizeWindowButton.BackColor = SystemColors.Window;
            MinimizeWindowButton.FlatAppearance.BorderSize = 0;
            MinimizeWindowButton.FlatStyle = FlatStyle.Flat;
            MinimizeWindowButton.Image = (Image)resources.GetObject("MinimizeWindowButton.Image");
            MinimizeWindowButton.Location = new Point(1132, 4);
            MinimizeWindowButton.Margin = new Padding(3, 2, 3, 2);
            MinimizeWindowButton.Name = "MinimizeWindowButton";
            MinimizeWindowButton.Size = new Size(29, 25);
            MinimizeWindowButton.TabIndex = 1;
            MinimizeWindowButton.TextImageRelation = TextImageRelation.ImageAboveText;
            MinimizeWindowButton.UseVisualStyleBackColor = false;
            MinimizeWindowButton.Click += MinimizeWindowButton_Click;
            // 
            // CloseButton
            // 
            CloseButton.BackColor = SystemColors.Window;
            CloseButton.BackgroundImageLayout = ImageLayout.Stretch;
            CloseButton.FlatAppearance.BorderSize = 0;
            CloseButton.FlatStyle = FlatStyle.Flat;
            CloseButton.Image = (Image)resources.GetObject("CloseButton.Image");
            CloseButton.Location = new Point(1168, 3);
            CloseButton.Margin = new Padding(3, 2, 3, 2);
            CloseButton.Name = "CloseButton";
            CloseButton.Size = new Size(31, 26);
            CloseButton.TabIndex = 0;
            CloseButton.UseVisualStyleBackColor = false;
            CloseButton.Click += CloseButton_Click;
            // 
            // BottomPanel
            // 
            BottomPanel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            BottomPanel.BackColor = Color.FromArgb(51, 153, 255);
            BottomPanel.Controls.Add(ErrorsCountLabel);
            BottomPanel.Controls.Add(AnalysisServiceStateLabel);
            BottomPanel.Controls.Add(UptimeLabel);
            BottomPanel.Controls.Add(StatePanel);
            BottomPanel.Controls.Add(CollectorServiceStateLabel);
            BottomPanel.Controls.Add(label6);
            BottomPanel.Controls.Add(label5);
            BottomPanel.Controls.Add(label7);
            BottomPanel.Controls.Add(label3);
            BottomPanel.Location = new Point(0, 681);
            BottomPanel.Margin = new Padding(3, 0, 3, 2);
            BottomPanel.Name = "BottomPanel";
            BottomPanel.Size = new Size(1209, 56);
            BottomPanel.TabIndex = 2;
            // 
            // ErrorsCountLabel
            // 
            ErrorsCountLabel.AutoSize = true;
            ErrorsCountLabel.Font = new Font("Century Gothic", 9F, FontStyle.Regular, GraphicsUnit.Point);
            ErrorsCountLabel.ForeColor = SystemColors.ButtonHighlight;
            ErrorsCountLabel.Location = new Point(486, 32);
            ErrorsCountLabel.Name = "ErrorsCountLabel";
            ErrorsCountLabel.Size = new Size(15, 17);
            ErrorsCountLabel.TabIndex = 10;
            ErrorsCountLabel.Text = "0";
            // 
            // AnalysisServiceStateLabel
            // 
            AnalysisServiceStateLabel.AutoSize = true;
            AnalysisServiceStateLabel.Font = new Font("Century Gothic", 9F, FontStyle.Regular, GraphicsUnit.Point);
            AnalysisServiceStateLabel.ForeColor = SystemColors.ButtonHighlight;
            AnalysisServiceStateLabel.Location = new Point(368, 32);
            AnalysisServiceStateLabel.Name = "AnalysisServiceStateLabel";
            AnalysisServiceStateLabel.Size = new Size(42, 17);
            AnalysisServiceStateLabel.TabIndex = 12;
            AnalysisServiceStateLabel.Text = "Down";
            // 
            // UptimeLabel
            // 
            UptimeLabel.AutoSize = true;
            UptimeLabel.Font = new Font("Century Gothic", 9F, FontStyle.Regular, GraphicsUnit.Point);
            UptimeLabel.ForeColor = SystemColors.ButtonHighlight;
            UptimeLabel.Location = new Point(486, 10);
            UptimeLabel.Name = "UptimeLabel";
            UptimeLabel.Size = new Size(15, 17);
            UptimeLabel.TabIndex = 9;
            UptimeLabel.Text = "0";
            // 
            // StatePanel
            // 
            StatePanel.BackColor = Color.Red;
            StatePanel.Controls.Add(StateLabel);
            StatePanel.Dock = DockStyle.Left;
            StatePanel.Location = new Point(0, 0);
            StatePanel.Margin = new Padding(3, 2, 3, 2);
            StatePanel.Name = "StatePanel";
            StatePanel.Size = new Size(241, 56);
            StatePanel.TabIndex = 4;
            // 
            // StateLabel
            // 
            StateLabel.Font = new Font("Century Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point);
            StateLabel.ForeColor = SystemColors.ButtonHighlight;
            StateLabel.Location = new Point(63, 13);
            StateLabel.Name = "StateLabel";
            StateLabel.Size = new Size(111, 28);
            StateLabel.TabIndex = 0;
            StateLabel.Text = "Not working";
            StateLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // CollectorServiceStateLabel
            // 
            CollectorServiceStateLabel.AutoSize = true;
            CollectorServiceStateLabel.Font = new Font("Century Gothic", 9F, FontStyle.Regular, GraphicsUnit.Point);
            CollectorServiceStateLabel.ForeColor = SystemColors.ButtonHighlight;
            CollectorServiceStateLabel.Location = new Point(368, 10);
            CollectorServiceStateLabel.Name = "CollectorServiceStateLabel";
            CollectorServiceStateLabel.Size = new Size(42, 17);
            CollectorServiceStateLabel.TabIndex = 11;
            CollectorServiceStateLabel.Text = "Down";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Century Gothic", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label6.ForeColor = SystemColors.ButtonHighlight;
            label6.Location = new Point(243, 10);
            label6.Name = "label6";
            label6.Size = new Size(117, 17);
            label6.TabIndex = 5;
            label6.Text = "Collector service: ";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Century Gothic", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label5.ForeColor = SystemColors.ButtonHighlight;
            label5.Location = new Point(438, 32);
            label5.Name = "label5";
            label5.Size = new Size(45, 17);
            label5.TabIndex = 4;
            label5.Text = "Errors: ";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Century Gothic", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label7.ForeColor = SystemColors.ButtonHighlight;
            label7.Location = new Point(254, 32);
            label7.Name = "label7";
            label7.Size = new Size(107, 17);
            label7.TabIndex = 6;
            label7.Text = "Analysis service: ";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Century Gothic", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label3.ForeColor = SystemColors.ButtonHighlight;
            label3.Location = new Point(427, 10);
            label3.Name = "label3";
            label3.Size = new Size(57, 17);
            label3.TabIndex = 2;
            label3.Text = "Uptime: ";
            // 
            // LeftPanel
            // 
            LeftPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            LeftPanel.BackColor = SystemColors.ActiveBorder;
            LeftPanel.Controls.Add(TitleLabel);
            LeftPanel.Controls.Add(StartStopButtonContainer);
            LeftPanel.Controls.Add(ShowAllCommentsButton);
            LeftPanel.Controls.Add(ShowSelectedComments);
            LeftPanel.Controls.Add(ConfigureServiceButton);
            LeftPanel.Controls.Add(ViewLogsButton);
            LeftPanel.Controls.Add(ServiceInfoPanel);
            LeftPanel.Location = new Point(0, 33);
            LeftPanel.Margin = new Padding(3, 2, 3, 2);
            LeftPanel.Name = "LeftPanel";
            LeftPanel.Size = new Size(241, 704);
            LeftPanel.TabIndex = 3;
            // 
            // TitleLabel
            // 
            TitleLabel.BackColor = SystemColors.Menu;
            TitleLabel.Dock = DockStyle.Bottom;
            TitleLabel.FlatStyle = FlatStyle.Flat;
            TitleLabel.Font = new Font("Century Gothic", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            TitleLabel.ForeColor = Color.FromArgb(51, 153, 255);
            TitleLabel.Location = new Point(0, -1);
            TitleLabel.Name = "TitleLabel";
            TitleLabel.Size = new Size(241, 52);
            TitleLabel.TabIndex = 1;
            TitleLabel.Text = "Content Analyzer";
            TitleLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // StartStopButtonContainer
            // 
            StartStopButtonContainer.Controls.Add(StartServiceButton);
            StartStopButtonContainer.Controls.Add(StopServiceButton);
            StartStopButtonContainer.Dock = DockStyle.Bottom;
            StartStopButtonContainer.Location = new Point(0, 51);
            StartStopButtonContainer.Margin = new Padding(0);
            StartStopButtonContainer.Name = "StartStopButtonContainer";
            StartStopButtonContainer.Size = new Size(241, 68);
            StartStopButtonContainer.TabIndex = 0;
            // 
            // StartServiceButton
            // 
            StartServiceButton.BackColor = SystemColors.Menu;
            StartServiceButton.Dock = DockStyle.Fill;
            StartServiceButton.FlatAppearance.BorderSize = 0;
            StartServiceButton.FlatStyle = FlatStyle.Flat;
            StartServiceButton.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point);
            StartServiceButton.ForeColor = Color.FromArgb(51, 153, 255);
            StartServiceButton.Location = new Point(0, 0);
            StartServiceButton.Margin = new Padding(3, 0, 3, 0);
            StartServiceButton.Name = "StartServiceButton";
            StartServiceButton.Size = new Size(241, 68);
            StartServiceButton.TabIndex = 0;
            StartServiceButton.Text = "Start Service";
            StartServiceButton.UseVisualStyleBackColor = false;
            StartServiceButton.Click += StartDataCollectionServiceButton_Click;
            // 
            // StopServiceButton
            // 
            StopServiceButton.BackColor = SystemColors.Menu;
            StopServiceButton.Dock = DockStyle.Fill;
            StopServiceButton.FlatAppearance.BorderSize = 0;
            StopServiceButton.FlatStyle = FlatStyle.Flat;
            StopServiceButton.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point);
            StopServiceButton.ForeColor = SystemColors.Highlight;
            StopServiceButton.Location = new Point(0, 0);
            StopServiceButton.Margin = new Padding(3, 2, 3, 2);
            StopServiceButton.Name = "StopServiceButton";
            StopServiceButton.Size = new Size(241, 68);
            StopServiceButton.TabIndex = 1;
            StopServiceButton.Text = "Stop Service";
            StopServiceButton.UseVisualStyleBackColor = false;
            StopServiceButton.Click += StopDataCollectionServiceButton_Click;
            // 
            // ShowAllCommentsButton
            // 
            ShowAllCommentsButton.BackColor = SystemColors.Menu;
            ShowAllCommentsButton.Dock = DockStyle.Bottom;
            ShowAllCommentsButton.FlatAppearance.BorderSize = 0;
            ShowAllCommentsButton.FlatStyle = FlatStyle.Flat;
            ShowAllCommentsButton.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point);
            ShowAllCommentsButton.ForeColor = Color.FromArgb(51, 153, 255);
            ShowAllCommentsButton.Location = new Point(0, 119);
            ShowAllCommentsButton.Margin = new Padding(3, 0, 3, 0);
            ShowAllCommentsButton.Name = "ShowAllCommentsButton";
            ShowAllCommentsButton.Size = new Size(241, 68);
            ShowAllCommentsButton.TabIndex = 2;
            ShowAllCommentsButton.Text = "All comments";
            ShowAllCommentsButton.UseVisualStyleBackColor = false;
            ShowAllCommentsButton.Click += ShowAllCommentsButton_Click;
            // 
            // ShowSelectedComments
            // 
            ShowSelectedComments.BackColor = SystemColors.Menu;
            ShowSelectedComments.Dock = DockStyle.Bottom;
            ShowSelectedComments.FlatAppearance.BorderSize = 0;
            ShowSelectedComments.FlatStyle = FlatStyle.Flat;
            ShowSelectedComments.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point);
            ShowSelectedComments.ForeColor = Color.FromArgb(51, 153, 255);
            ShowSelectedComments.Location = new Point(0, 187);
            ShowSelectedComments.Margin = new Padding(3, 0, 3, 0);
            ShowSelectedComments.Name = "ShowSelectedComments";
            ShowSelectedComments.Size = new Size(241, 68);
            ShowSelectedComments.TabIndex = 11;
            ShowSelectedComments.Text = "Selected comments";
            ShowSelectedComments.UseVisualStyleBackColor = false;
            ShowSelectedComments.Click += ShowSelectedComments_Click;
            // 
            // ConfigureServiceButton
            // 
            ConfigureServiceButton.BackColor = SystemColors.Menu;
            ConfigureServiceButton.Dock = DockStyle.Bottom;
            ConfigureServiceButton.FlatAppearance.BorderSize = 0;
            ConfigureServiceButton.FlatStyle = FlatStyle.Flat;
            ConfigureServiceButton.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point);
            ConfigureServiceButton.ForeColor = Color.FromArgb(51, 153, 255);
            ConfigureServiceButton.Location = new Point(0, 255);
            ConfigureServiceButton.Margin = new Padding(3, 0, 3, 0);
            ConfigureServiceButton.Name = "ConfigureServiceButton";
            ConfigureServiceButton.Size = new Size(241, 68);
            ConfigureServiceButton.TabIndex = 0;
            ConfigureServiceButton.Text = "Configure";
            ConfigureServiceButton.UseVisualStyleBackColor = false;
            ConfigureServiceButton.Click += ConfigureServiceButton_Click;
            // 
            // ViewLogsButton
            // 
            ViewLogsButton.BackColor = SystemColors.Menu;
            ViewLogsButton.Dock = DockStyle.Bottom;
            ViewLogsButton.FlatAppearance.BorderSize = 0;
            ViewLogsButton.FlatStyle = FlatStyle.Flat;
            ViewLogsButton.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point);
            ViewLogsButton.ForeColor = Color.FromArgb(51, 153, 255);
            ViewLogsButton.Location = new Point(0, 323);
            ViewLogsButton.Margin = new Padding(3, 0, 3, 0);
            ViewLogsButton.Name = "ViewLogsButton";
            ViewLogsButton.Size = new Size(241, 68);
            ViewLogsButton.TabIndex = 10;
            ViewLogsButton.Text = "Logs";
            ViewLogsButton.UseVisualStyleBackColor = false;
            ViewLogsButton.Click += ViewLogsButton_Click;
            // 
            // ServiceInfoPanel
            // 
            ServiceInfoPanel.BackColor = SystemColors.ControlLight;
            ServiceInfoPanel.Controls.Add(SelectedCommentsFoundLabel);
            ServiceInfoPanel.Controls.Add(CommentsFoundLabel);
            ServiceInfoPanel.Controls.Add(label4);
            ServiceInfoPanel.Controls.Add(label2);
            ServiceInfoPanel.Controls.Add(label1);
            ServiceInfoPanel.Dock = DockStyle.Bottom;
            ServiceInfoPanel.Location = new Point(0, 391);
            ServiceInfoPanel.Margin = new Padding(3, 0, 3, 0);
            ServiceInfoPanel.Name = "ServiceInfoPanel";
            ServiceInfoPanel.Size = new Size(241, 313);
            ServiceInfoPanel.TabIndex = 9;
            // 
            // SelectedCommentsFoundLabel
            // 
            SelectedCommentsFoundLabel.AutoSize = true;
            SelectedCommentsFoundLabel.Font = new Font("Century Gothic", 9F, FontStyle.Regular, GraphicsUnit.Point);
            SelectedCommentsFoundLabel.Location = new Point(167, 68);
            SelectedCommentsFoundLabel.Name = "SelectedCommentsFoundLabel";
            SelectedCommentsFoundLabel.Size = new Size(15, 17);
            SelectedCommentsFoundLabel.TabIndex = 8;
            SelectedCommentsFoundLabel.Text = "0";
            // 
            // CommentsFoundLabel
            // 
            CommentsFoundLabel.AutoSize = true;
            CommentsFoundLabel.Font = new Font("Century Gothic", 9F, FontStyle.Regular, GraphicsUnit.Point);
            CommentsFoundLabel.Location = new Point(167, 44);
            CommentsFoundLabel.Name = "CommentsFoundLabel";
            CommentsFoundLabel.Size = new Size(15, 17);
            CommentsFoundLabel.TabIndex = 7;
            CommentsFoundLabel.Text = "0";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Century Gothic", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            label4.Location = new Point(69, 7);
            label4.Name = "label4";
            label4.Size = new Size(87, 19);
            label4.TabIndex = 3;
            label4.Text = "Service Info";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Century Gothic", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(26, 68);
            label2.Name = "label2";
            label2.Size = new Size(135, 17);
            label2.TabIndex = 1;
            label2.Text = "Selected comments: ";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Century Gothic", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(44, 44);
            label1.Name = "label1";
            label1.Size = new Size(115, 17);
            label1.TabIndex = 0;
            label1.Text = "Comments found: ";
            // 
            // CentralPanel
            // 
            CentralPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            CentralPanel.BackColor = SystemColors.InactiveCaption;
            CentralPanel.Location = new Point(241, 33);
            CentralPanel.Margin = new Padding(0);
            CentralPanel.Name = "CentralPanel";
            CentralPanel.Size = new Size(969, 648);
            CentralPanel.TabIndex = 6;
            // 
            // MainWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1209, 736);
            ControlBox = false;
            Controls.Add(CentralPanel);
            Controls.Add(BottomPanel);
            Controls.Add(LeftPanel);
            Controls.Add(UpperPanel);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 2, 3, 2);
            Name = "MainWindow";
            StartPosition = FormStartPosition.CenterScreen;
            Load += MainWindow_Load;
            UpperPanel.ResumeLayout(false);
            BottomPanel.ResumeLayout(false);
            BottomPanel.PerformLayout();
            StatePanel.ResumeLayout(false);
            LeftPanel.ResumeLayout(false);
            StartStopButtonContainer.ResumeLayout(false);
            ServiceInfoPanel.ResumeLayout(false);
            ServiceInfoPanel.PerformLayout();
            ResumeLayout(false);
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