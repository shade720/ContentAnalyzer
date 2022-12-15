namespace DevTool.Forms
{
    partial class LogViewerWindow
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
            this.LogDataGrid = new System.Windows.Forms.DataGridView();
            this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Level = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Message = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.LogLevel = new System.Windows.Forms.ComboBox();
            this.MessageTextbox = new System.Windows.Forms.RichTextBox();
            this.SearchButton = new System.Windows.Forms.Button();
            this.RefreshButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.LogDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // LogDataGrid
            // 
            this.LogDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.LogDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Date,
            this.Level,
            this.Message});
            this.LogDataGrid.Location = new System.Drawing.Point(1, 29);
            this.LogDataGrid.MultiSelect = false;
            this.LogDataGrid.Name = "LogDataGrid";
            this.LogDataGrid.RowHeadersVisible = false;
            this.LogDataGrid.RowHeadersWidth = 51;
            this.LogDataGrid.RowTemplate.Height = 29;
            this.LogDataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.LogDataGrid.Size = new System.Drawing.Size(981, 381);
            this.LogDataGrid.TabIndex = 0;
            this.LogDataGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.LogDataGrid_CellClick);
            // 
            // Date
            // 
            this.Date.HeaderText = "Date";
            this.Date.MinimumWidth = 6;
            this.Date.Name = "Date";
            this.Date.ReadOnly = true;
            this.Date.Width = 125;
            // 
            // Level
            // 
            this.Level.HeaderText = "Level";
            this.Level.MinimumWidth = 6;
            this.Level.Name = "Level";
            this.Level.ReadOnly = true;
            this.Level.Width = 125;
            // 
            // Message
            // 
            this.Message.HeaderText = "Message";
            this.Message.MinimumWidth = 6;
            this.Message.Name = "Message";
            this.Message.ReadOnly = true;
            this.Message.Width = 720;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(152, 2);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(646, 27);
            this.textBox1.TabIndex = 1;
            // 
            // LogLevel
            // 
            this.LogLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.LogLevel.FormattingEnabled = true;
            this.LogLevel.Items.AddRange(new object[] {
            "All",
            "Error",
            "Warning",
            "Information"});
            this.LogLevel.Location = new System.Drawing.Point(1, 1);
            this.LogLevel.Name = "LogLevel";
            this.LogLevel.Size = new System.Drawing.Size(151, 28);
            this.LogLevel.TabIndex = 2;
            this.LogLevel.SelectedIndexChanged += new System.EventHandler(this.LogLevel_SelectedIndexChanged);
            // 
            // MessageTextbox
            // 
            this.MessageTextbox.Location = new System.Drawing.Point(1, 416);
            this.MessageTextbox.Name = "MessageTextbox";
            this.MessageTextbox.RightMargin = 600;
            this.MessageTextbox.Size = new System.Drawing.Size(978, 234);
            this.MessageTextbox.TabIndex = 3;
            this.MessageTextbox.Text = "";
            // 
            // SearchButton
            // 
            this.SearchButton.Location = new System.Drawing.Point(797, 0);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(94, 29);
            this.SearchButton.TabIndex = 4;
            this.SearchButton.Text = "Search";
            this.SearchButton.UseVisualStyleBackColor = true;
            // 
            // RefreshButton
            // 
            this.RefreshButton.Location = new System.Drawing.Point(888, 0);
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Size = new System.Drawing.Size(94, 29);
            this.RefreshButton.TabIndex = 5;
            this.RefreshButton.Text = "Refresh";
            this.RefreshButton.UseVisualStyleBackColor = true;
            // 
            // LogViewerWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(982, 653);
            this.Controls.Add(this.RefreshButton);
            this.Controls.Add(this.SearchButton);
            this.Controls.Add(this.MessageTextbox);
            this.Controls.Add(this.LogLevel);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.LogDataGrid);
            this.Name = "LogViewerWindow";
            this.Text = "LogViewerWindow";
            ((System.ComponentModel.ISupportInitialize)(this.LogDataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DataGridView LogDataGrid;
        private TextBox textBox1;
        private ComboBox LogLevel;
        private RichTextBox MessageTextbox;
        private DataGridViewTextBoxColumn Date;
        private DataGridViewTextBoxColumn Level;
        private DataGridViewTextBoxColumn Message;
        private Button SearchButton;
        private Button RefreshButton;
    }
}