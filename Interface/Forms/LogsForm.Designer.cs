namespace Interface.Forms
{
    partial class LogsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.LogWindowTextbox = new System.Windows.Forms.RichTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.LogDateComboBox = new System.Windows.Forms.ComboBox();
            this.GetAnalysisLogs = new System.Windows.Forms.Button();
            this.GetCollectionLogs = new System.Windows.Forms.Button();
            this.RefreshButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // LogWindowTextbox
            // 
            this.LogWindowTextbox.BackColor = System.Drawing.SystemColors.InfoText;
            this.LogWindowTextbox.DetectUrls = false;
            this.LogWindowTextbox.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LogWindowTextbox.ForeColor = System.Drawing.SystemColors.Highlight;
            this.LogWindowTextbox.Location = new System.Drawing.Point(6, 125);
            this.LogWindowTextbox.Name = "LogWindowTextbox";
            this.LogWindowTextbox.ReadOnly = true;
            this.LogWindowTextbox.Size = new System.Drawing.Size(1074, 709);
            this.LogWindowTextbox.TabIndex = 0;
            this.LogWindowTextbox.Text = "";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.LogDateComboBox);
            this.groupBox1.Controls.Add(this.GetAnalysisLogs);
            this.groupBox1.Controls.Add(this.GetCollectionLogs);
            this.groupBox1.Controls.Add(this.RefreshButton);
            this.groupBox1.Controls.Add(this.LogWindowTextbox);
            this.groupBox1.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1086, 840);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Service Logs";
            // 
            // LogDateComboBox
            // 
            this.LogDateComboBox.FormattingEnabled = true;
            this.LogDateComboBox.Location = new System.Drawing.Point(609, 16);
            this.LogDateComboBox.Name = "LogDateComboBox";
            this.LogDateComboBox.Size = new System.Drawing.Size(151, 31);
            this.LogDateComboBox.TabIndex = 4;
            this.LogDateComboBox.Text = "20.04.2022";
            // 
            // GetAnalysisLogs
            // 
            this.GetAnalysisLogs.Location = new System.Drawing.Point(766, 16);
            this.GetAnalysisLogs.Name = "GetAnalysisLogs";
            this.GetAnalysisLogs.Size = new System.Drawing.Size(154, 103);
            this.GetAnalysisLogs.TabIndex = 3;
            this.GetAnalysisLogs.Text = "Get data analysis service logs";
            this.GetAnalysisLogs.UseVisualStyleBackColor = true;
            this.GetAnalysisLogs.Click += new System.EventHandler(this.GetAnalysisLogs_Click);
            // 
            // GetCollectionLogs
            // 
            this.GetCollectionLogs.Location = new System.Drawing.Point(926, 16);
            this.GetCollectionLogs.Name = "GetCollectionLogs";
            this.GetCollectionLogs.Size = new System.Drawing.Size(154, 103);
            this.GetCollectionLogs.TabIndex = 2;
            this.GetCollectionLogs.Text = "Get data collection service logs";
            this.GetCollectionLogs.UseVisualStyleBackColor = true;
            this.GetCollectionLogs.Click += new System.EventHandler(this.GetCollectionLogs_Click);
            // 
            // RefreshButton
            // 
            this.RefreshButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.RefreshButton.Location = new System.Drawing.Point(20, 44);
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Size = new System.Drawing.Size(94, 46);
            this.RefreshButton.TabIndex = 1;
            this.RefreshButton.Text = "Refresh";
            this.RefreshButton.UseVisualStyleBackColor = true;
            this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // LogsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(1107, 864);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "LogsForm";
            this.Text = "DataCollectionLogs";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private RichTextBox LogWindowTextbox;
        private GroupBox groupBox1;
        private Button GetAnalysisLogs;
        private Button GetCollectionLogs;
        private Button RefreshButton;
        private ComboBox LogDateComboBox;
    }
}