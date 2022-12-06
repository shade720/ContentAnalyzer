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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SearchTextBox = new System.Windows.Forms.TextBox();
            this.SearchComboBox = new System.Windows.Forms.ComboBox();
            this.LogDateComboBox = new System.Windows.Forms.ComboBox();
            this.GetAnalysisLogs = new System.Windows.Forms.Button();
            this.GetCollectionLogs = new System.Windows.Forms.Button();
            this.RefreshButton = new System.Windows.Forms.Button();
            this.LogGrid = new System.Windows.Forms.DataGridView();
            this.Timestamp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LogLevel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Text = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LogGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.SearchTextBox);
            this.groupBox1.Controls.Add(this.SearchComboBox);
            this.groupBox1.Controls.Add(this.LogDateComboBox);
            this.groupBox1.Controls.Add(this.GetAnalysisLogs);
            this.groupBox1.Controls.Add(this.GetCollectionLogs);
            this.groupBox1.Controls.Add(this.RefreshButton);
            this.groupBox1.Controls.Add(this.LogGrid);
            this.groupBox1.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.groupBox1.Location = new System.Drawing.Point(28, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1050, 840);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Service Logs";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(527, 749);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 23);
            this.label1.TabIndex = 12;
            this.label1.Text = "Log file date";
            // 
            // SearchTextBox
            // 
            this.SearchTextBox.BackColor = System.Drawing.SystemColors.ControlLight;
            this.SearchTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SearchTextBox.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.SearchTextBox.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.SearchTextBox.Location = new System.Drawing.Point(187, 51);
            this.SearchTextBox.Multiline = true;
            this.SearchTextBox.Name = "SearchTextBox";
            this.SearchTextBox.PlaceholderText = "Search for...";
            this.SearchTextBox.Size = new System.Drawing.Size(857, 30);
            this.SearchTextBox.TabIndex = 10;
            this.SearchTextBox.TextChanged += new System.EventHandler(this.SearchTextBox_TextChanged);
            // 
            // SearchComboBox
            // 
            this.SearchComboBox.BackColor = System.Drawing.SystemColors.ControlLight;
            this.SearchComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SearchComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SearchComboBox.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.SearchComboBox.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.SearchComboBox.FormattingEnabled = true;
            this.SearchComboBox.Items.AddRange(new object[] {
            "Timestamp",
            "Level",
            "Text"});
            this.SearchComboBox.Location = new System.Drawing.Point(6, 51);
            this.SearchComboBox.Name = "SearchComboBox";
            this.SearchComboBox.Size = new System.Drawing.Size(175, 31);
            this.SearchComboBox.TabIndex = 11;
            // 
            // LogDateComboBox
            // 
            this.LogDateComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.LogDateComboBox.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LogDateComboBox.FormattingEnabled = true;
            this.LogDateComboBox.Location = new System.Drawing.Point(664, 746);
            this.LogDateComboBox.Name = "LogDateComboBox";
            this.LogDateComboBox.Size = new System.Drawing.Size(151, 31);
            this.LogDateComboBox.TabIndex = 9;
            // 
            // GetAnalysisLogs
            // 
            this.GetAnalysisLogs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.GetAnalysisLogs.Location = new System.Drawing.Point(821, 746);
            this.GetAnalysisLogs.Name = "GetAnalysisLogs";
            this.GetAnalysisLogs.Size = new System.Drawing.Size(223, 45);
            this.GetAnalysisLogs.TabIndex = 8;
            this.GetAnalysisLogs.Text = "View analysis logs";
            this.GetAnalysisLogs.UseVisualStyleBackColor = true;
            this.GetAnalysisLogs.Click += new System.EventHandler(this.GetAnalysisLogs_Click);
            // 
            // GetCollectionLogs
            // 
            this.GetCollectionLogs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.GetCollectionLogs.Location = new System.Drawing.Point(821, 791);
            this.GetCollectionLogs.Name = "GetCollectionLogs";
            this.GetCollectionLogs.Size = new System.Drawing.Size(223, 45);
            this.GetCollectionLogs.TabIndex = 7;
            this.GetCollectionLogs.Text = "View collection logs";
            this.GetCollectionLogs.UseVisualStyleBackColor = true;
            this.GetCollectionLogs.Click += new System.EventHandler(this.GetCollectionLogs_Click);
            // 
            // RefreshButton
            // 
            this.RefreshButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.RefreshButton.Location = new System.Drawing.Point(6, 746);
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Size = new System.Drawing.Size(135, 88);
            this.RefreshButton.TabIndex = 6;
            this.RefreshButton.Text = "Refresh";
            this.RefreshButton.UseVisualStyleBackColor = true;
            this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // LogGrid
            // 
            this.LogGrid.AllowUserToAddRows = false;
            this.LogGrid.AllowUserToDeleteRows = false;
            this.LogGrid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.LogGrid.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.LogGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.MenuHighlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.LogGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.LogGrid.ColumnHeadersHeight = 29;
            this.LogGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.LogGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Timestamp,
            this.LogLevel,
            this.Text});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.MenuHighlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.LogGrid.DefaultCellStyle = dataGridViewCellStyle2;
            this.LogGrid.EnableHeadersVisualStyles = false;
            this.LogGrid.GridColor = System.Drawing.SystemColors.ControlLightLight;
            this.LogGrid.Location = new System.Drawing.Point(6, 88);
            this.LogGrid.Name = "LogGrid";
            this.LogGrid.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.MenuHighlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.LogGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.LogGrid.RowHeadersVisible = false;
            this.LogGrid.RowHeadersWidth = 51;
            this.LogGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            dataGridViewCellStyle4.NullValue = null;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.MenuHighlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.LogGrid.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.LogGrid.RowTemplate.Height = 29;
            this.LogGrid.Size = new System.Drawing.Size(1040, 651);
            this.LogGrid.TabIndex = 5;
            // 
            // Timestamp
            // 
            this.Timestamp.Frozen = true;
            this.Timestamp.HeaderText = "Timestamp";
            this.Timestamp.MinimumWidth = 6;
            this.Timestamp.Name = "Timestamp";
            this.Timestamp.ReadOnly = true;
            this.Timestamp.Width = 180;
            // 
            // LogLevel
            // 
            this.LogLevel.Frozen = true;
            this.LogLevel.HeaderText = "Level";
            this.LogLevel.MinimumWidth = 6;
            this.LogLevel.Name = "LogLevel";
            this.LogLevel.ReadOnly = true;
            this.LogLevel.Width = 150;
            // 
            // Text
            // 
            this.Text.Frozen = true;
            this.Text.HeaderText = "Text";
            this.Text.MinimumWidth = 6;
            this.Text.Name = "Text";
            this.Text.ReadOnly = true;
            this.Text.Width = 730;
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
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LogGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private GroupBox groupBox1;
        private DataGridView LogGrid;
        private ComboBox LogDateComboBox;
        private Button GetAnalysisLogs;
        private Button GetCollectionLogs;
        private Button RefreshButton;
        private TextBox SearchTextBox;
        private ComboBox SearchComboBox;
        private Label label1;
        private DataGridViewTextBoxColumn Timestamp;
        private DataGridViewTextBoxColumn LogLevel;
        private DataGridViewTextBoxColumn Text;
    }
}