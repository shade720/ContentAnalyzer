namespace ContentAnalyzer.Frontend.Desktop.Forms
{
    partial class LogForm
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            LogDataGrid = new DataGridView();
            Date = new DataGridViewTextBoxColumn();
            Level = new DataGridViewTextBoxColumn();
            Message = new DataGridViewTextBoxColumn();
            SearchTextbox = new TextBox();
            LogLevel = new ComboBox();
            MessageTextbox = new RichTextBox();
            SearchButton = new Button();
            label4 = new Label();
            RefreshButton = new Button();
            ToDate = new DateTimePicker();
            label14 = new Label();
            FromDate = new DateTimePicker();
            SourceComboBox = new ComboBox();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)LogDataGrid).BeginInit();
            SuspendLayout();
            // 
            // LogDataGrid
            // 
            LogDataGrid.AllowUserToAddRows = false;
            LogDataGrid.AllowUserToDeleteRows = false;
            LogDataGrid.BackgroundColor = SystemColors.ControlLightLight;
            LogDataGrid.BorderStyle = BorderStyle.Fixed3D;
            LogDataGrid.CellBorderStyle = DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.White;
            dataGridViewCellStyle1.Font = new Font("Century Gothic", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = SystemColors.MenuHighlight;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.MenuHighlight;
            dataGridViewCellStyle1.SelectionForeColor = Color.White;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            LogDataGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            LogDataGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            LogDataGrid.Columns.AddRange(new DataGridViewColumn[] { Date, Level, Message });
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.White;
            dataGridViewCellStyle2.Font = new Font("Century Gothic", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = SystemColors.InfoText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.MenuHighlight;
            dataGridViewCellStyle2.SelectionForeColor = Color.White;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            LogDataGrid.DefaultCellStyle = dataGridViewCellStyle2;
            LogDataGrid.EnableHeadersVisualStyles = false;
            LogDataGrid.GridColor = SystemColors.ControlLightLight;
            LogDataGrid.Location = new Point(1, 33);
            LogDataGrid.Margin = new Padding(3, 2, 3, 2);
            LogDataGrid.MultiSelect = false;
            LogDataGrid.Name = "LogDataGrid";
            LogDataGrid.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.Font = new Font("Century Gothic", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = SystemColors.MenuHighlight;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.MenuHighlight;
            dataGridViewCellStyle3.SelectionForeColor = Color.White;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            LogDataGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            LogDataGrid.RowHeadersVisible = false;
            LogDataGrid.RowHeadersWidth = 51;
            dataGridViewCellStyle4.BackColor = Color.White;
            dataGridViewCellStyle4.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle4.ForeColor = SystemColors.InfoText;
            dataGridViewCellStyle4.SelectionBackColor = SystemColors.MenuHighlight;
            dataGridViewCellStyle4.SelectionForeColor = Color.White;
            LogDataGrid.RowsDefaultCellStyle = dataGridViewCellStyle4;
            LogDataGrid.RowTemplate.Height = 29;
            LogDataGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            LogDataGrid.Size = new Size(1259, 509);
            LogDataGrid.TabIndex = 0;
            LogDataGrid.CellClick += LogDataGrid_CellClick;
            // 
            // Date
            // 
            Date.HeaderText = "Дата";
            Date.MinimumWidth = 6;
            Date.Name = "Date";
            Date.ReadOnly = true;
            Date.Width = 150;
            // 
            // Level
            // 
            Level.HeaderText = "Уровень";
            Level.MinimumWidth = 6;
            Level.Name = "Level";
            Level.ReadOnly = true;
            Level.Width = 115;
            // 
            // Message
            // 
            Message.HeaderText = "Сообщение";
            Message.MinimumWidth = 6;
            Message.Name = "Message";
            Message.ReadOnly = true;
            Message.Width = 970;
            // 
            // SearchTextbox
            // 
            SearchTextbox.Font = new Font("Century Gothic", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            SearchTextbox.Location = new Point(151, 1);
            SearchTextbox.Margin = new Padding(0);
            SearchTextbox.Multiline = true;
            SearchTextbox.Name = "SearchTextbox";
            SearchTextbox.PlaceholderText = "Ищем...";
            SearchTextbox.Size = new Size(1002, 30);
            SearchTextbox.TabIndex = 1;
            // 
            // LogLevel
            // 
            LogLevel.DropDownStyle = ComboBoxStyle.DropDownList;
            LogLevel.FlatStyle = FlatStyle.Flat;
            LogLevel.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            LogLevel.FormattingEnabled = true;
            LogLevel.Items.AddRange(new object[] { "All", "Fatal", "Error", "Warning", "Information" });
            LogLevel.Location = new Point(1, 4);
            LogLevel.Margin = new Padding(0);
            LogLevel.Name = "LogLevel";
            LogLevel.Size = new Size(149, 25);
            LogLevel.TabIndex = 2;
            LogLevel.SelectedIndexChanged += LogLevel_SelectedIndexChanged;
            // 
            // MessageTextbox
            // 
            MessageTextbox.BackColor = SystemColors.Window;
            MessageTextbox.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            MessageTextbox.ForeColor = SystemColors.InfoText;
            MessageTextbox.Location = new Point(1, 546);
            MessageTextbox.Margin = new Padding(3, 2, 3, 2);
            MessageTextbox.Name = "MessageTextbox";
            MessageTextbox.RightMargin = 600;
            MessageTextbox.Size = new Size(1255, 186);
            MessageTextbox.TabIndex = 3;
            MessageTextbox.Text = "";
            // 
            // SearchButton
            // 
            SearchButton.BackColor = Color.White;
            SearchButton.FlatStyle = FlatStyle.Flat;
            SearchButton.Font = new Font("Century Gothic", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            SearchButton.ForeColor = SystemColors.MenuHighlight;
            SearchButton.Location = new Point(1153, 1);
            SearchButton.Margin = new Padding(0);
            SearchButton.Name = "SearchButton";
            SearchButton.Size = new Size(107, 30);
            SearchButton.TabIndex = 4;
            SearchButton.Text = "Поиск";
            SearchButton.UseVisualStyleBackColor = false;
            SearchButton.Click += SearchButton_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Century Gothic", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            label4.ForeColor = SystemColors.MenuHighlight;
            label4.Location = new Point(911, 781);
            label4.Name = "label4";
            label4.Size = new Size(34, 18);
            label4.TabIndex = 24;
            label4.Text = "До:";
            // 
            // RefreshButton
            // 
            RefreshButton.BackColor = Color.White;
            RefreshButton.FlatStyle = FlatStyle.Flat;
            RefreshButton.Font = new Font("Century Gothic", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            RefreshButton.ForeColor = SystemColors.MenuHighlight;
            RefreshButton.Location = new Point(1127, 746);
            RefreshButton.Name = "RefreshButton";
            RefreshButton.Size = new Size(121, 54);
            RefreshButton.TabIndex = 23;
            RefreshButton.Text = "Обновить";
            RefreshButton.UseVisualStyleBackColor = false;
            RefreshButton.Click += RefreshButton_Click;
            // 
            // ToDate
            // 
            ToDate.Font = new Font("Century Gothic", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            ToDate.Location = new Point(951, 775);
            ToDate.Margin = new Padding(3, 2, 3, 2);
            ToDate.Name = "ToDate";
            ToDate.Size = new Size(146, 26);
            ToDate.TabIndex = 22;
            ToDate.Value = new DateTime(2023, 12, 28, 0, 0, 0, 0);
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new Font("Century Gothic", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            label14.ForeColor = SystemColors.MenuHighlight;
            label14.Location = new Point(921, 752);
            label14.Name = "label14";
            label14.Size = new Size(24, 18);
            label14.TabIndex = 21;
            label14.Text = "С:";
            // 
            // FromDate
            // 
            FromDate.Font = new Font("Century Gothic", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            FromDate.Location = new Point(951, 746);
            FromDate.Margin = new Padding(3, 2, 3, 2);
            FromDate.Name = "FromDate";
            FromDate.Size = new Size(146, 26);
            FromDate.TabIndex = 20;
            FromDate.Value = new DateTime(2023, 12, 28, 0, 0, 0, 0);
            // 
            // SourceComboBox
            // 
            SourceComboBox.BackColor = SystemColors.ButtonFace;
            SourceComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            SourceComboBox.FlatStyle = FlatStyle.Flat;
            SourceComboBox.Font = new Font("Century Gothic", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            SourceComboBox.ForeColor = SystemColors.MenuHighlight;
            SourceComboBox.FormattingEnabled = true;
            SourceComboBox.Items.AddRange(new object[] { "Все", "Сервис сбора", "Сервис анализа" });
            SourceComboBox.Location = new Point(678, 746);
            SourceComboBox.Name = "SourceComboBox";
            SourceComboBox.Size = new Size(200, 26);
            SourceComboBox.TabIndex = 25;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Century Gothic", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = SystemColors.MenuHighlight;
            label1.Location = new Point(588, 749);
            label1.Name = "label1";
            label1.Size = new Size(84, 18);
            label1.TabIndex = 26;
            label1.Text = "Источник:";
            // 
            // LogForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1260, 812);
            Controls.Add(label1);
            Controls.Add(SourceComboBox);
            Controls.Add(label4);
            Controls.Add(RefreshButton);
            Controls.Add(ToDate);
            Controls.Add(label14);
            Controls.Add(FromDate);
            Controls.Add(SearchTextbox);
            Controls.Add(SearchButton);
            Controls.Add(MessageTextbox);
            Controls.Add(LogLevel);
            Controls.Add(LogDataGrid);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 2, 3, 2);
            Name = "LogForm";
            ShowIcon = false;
            Text = "Log";
            Load += LogViewerWindow_Load;
            ((System.ComponentModel.ISupportInitialize)LogDataGrid).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView LogDataGrid;
        private TextBox SearchTextbox;
        private ComboBox LogLevel;
        private RichTextBox MessageTextbox;
        private Button SearchButton;
        private Label label4;
        private Button RefreshButton;
        private DateTimePicker ToDate;
        private Label label14;
        private DateTimePicker FromDate;
        private ComboBox SourceComboBox;
        private Label label1;
        private DataGridViewTextBoxColumn Date;
        private DataGridViewTextBoxColumn Level;
        private DataGridViewTextBoxColumn Message;
    }
}