namespace ContentAnalyzer.Frontend.Desktop.Forms
{
    partial class ProcessedCommentsForm
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
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle7 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle8 = new DataGridViewCellStyle();
            SelectedCommentsDataGridView = new DataGridView();
            DateColumn = new DataGridViewTextBoxColumn();
            TextColumn = new DataGridViewTextBoxColumn();
            CategoryColumn = new DataGridViewTextBoxColumn();
            SimilarityColumn = new DataGridViewTextBoxColumn();
            LinkColumn = new DataGridViewLinkColumn();
            groupBox1 = new GroupBox();
            OpenExcelReportButton = new Button();
            groupBox4 = new GroupBox();
            SelectedDateRadioButton = new RadioButton();
            LastMonthRadioButton = new RadioButton();
            LastWeekRadioButton = new RadioButton();
            Last3DaysRadioButton = new RadioButton();
            TodayRadioButton = new RadioButton();
            label7 = new Label();
            CategoryFilterTextBox = new TextBox();
            label12 = new Label();
            TextFilterTextBox = new TextBox();
            label5 = new Label();
            AuthorFilterTextBox = new TextBox();
            label2 = new Label();
            PostFilterTextBox = new TextBox();
            label4 = new Label();
            label1 = new Label();
            CommunityFilterTextBox = new TextBox();
            RefreshButton = new Button();
            ToDate = new DateTimePicker();
            label14 = new Label();
            FromDate = new DateTimePicker();
            DisplayedRowsLabel = new Label();
            label3 = new Label();
            SaveReportDialog = new SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)SelectedCommentsDataGridView).BeginInit();
            groupBox1.SuspendLayout();
            groupBox4.SuspendLayout();
            SuspendLayout();
            // 
            // SelectedCommentsDataGridView
            // 
            SelectedCommentsDataGridView.AllowUserToAddRows = false;
            SelectedCommentsDataGridView.AllowUserToDeleteRows = false;
            SelectedCommentsDataGridView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            SelectedCommentsDataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            SelectedCommentsDataGridView.BackgroundColor = SystemColors.ControlLightLight;
            SelectedCommentsDataGridView.BorderStyle = BorderStyle.None;
            SelectedCommentsDataGridView.CellBorderStyle = DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.TopCenter;
            dataGridViewCellStyle5.BackColor = Color.White;
            dataGridViewCellStyle5.Font = new Font("Century Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point);
            dataGridViewCellStyle5.ForeColor = SystemColors.MenuHighlight;
            dataGridViewCellStyle5.SelectionBackColor = SystemColors.MenuHighlight;
            dataGridViewCellStyle5.SelectionForeColor = Color.White;
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.True;
            SelectedCommentsDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            SelectedCommentsDataGridView.ColumnHeadersHeight = 29;
            SelectedCommentsDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            SelectedCommentsDataGridView.Columns.AddRange(new DataGridViewColumn[] { DateColumn, TextColumn, CategoryColumn, SimilarityColumn, LinkColumn });
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = Color.White;
            dataGridViewCellStyle6.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle6.ForeColor = SystemColors.MenuHighlight;
            dataGridViewCellStyle6.SelectionBackColor = SystemColors.MenuHighlight;
            dataGridViewCellStyle6.SelectionForeColor = Color.White;
            dataGridViewCellStyle6.WrapMode = DataGridViewTriState.True;
            SelectedCommentsDataGridView.DefaultCellStyle = dataGridViewCellStyle6;
            SelectedCommentsDataGridView.EnableHeadersVisualStyles = false;
            SelectedCommentsDataGridView.GridColor = SystemColors.ControlLightLight;
            SelectedCommentsDataGridView.Location = new Point(5, 171);
            SelectedCommentsDataGridView.Margin = new Padding(3, 2, 3, 2);
            SelectedCommentsDataGridView.Name = "SelectedCommentsDataGridView";
            SelectedCommentsDataGridView.ReadOnly = true;
            dataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = Color.White;
            dataGridViewCellStyle7.Font = new Font("Century Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point);
            dataGridViewCellStyle7.ForeColor = Color.White;
            dataGridViewCellStyle7.SelectionBackColor = SystemColors.MenuHighlight;
            dataGridViewCellStyle7.SelectionForeColor = Color.White;
            dataGridViewCellStyle7.WrapMode = DataGridViewTriState.True;
            SelectedCommentsDataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            SelectedCommentsDataGridView.RowHeadersVisible = false;
            SelectedCommentsDataGridView.RowHeadersWidth = 51;
            dataGridViewCellStyle8.BackColor = SystemColors.ControlLightLight;
            dataGridViewCellStyle8.Font = new Font("Century Gothic", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle8.ForeColor = SystemColors.InfoText;
            dataGridViewCellStyle8.SelectionBackColor = SystemColors.MenuHighlight;
            dataGridViewCellStyle8.SelectionForeColor = Color.White;
            dataGridViewCellStyle8.WrapMode = DataGridViewTriState.True;
            SelectedCommentsDataGridView.RowsDefaultCellStyle = dataGridViewCellStyle8;
            SelectedCommentsDataGridView.RowTemplate.Height = 29;
            SelectedCommentsDataGridView.Size = new Size(1201, 575);
            SelectedCommentsDataGridView.TabIndex = 0;
            // 
            // DateColumn
            // 
            DateColumn.HeaderText = "Date";
            DateColumn.MinimumWidth = 6;
            DateColumn.Name = "DateColumn";
            DateColumn.ReadOnly = true;
            DateColumn.Width = 125;
            // 
            // TextColumn
            // 
            TextColumn.HeaderText = "Text";
            TextColumn.MinimumWidth = 6;
            TextColumn.Name = "TextColumn";
            TextColumn.ReadOnly = true;
            TextColumn.Width = 620;
            // 
            // CategoryColumn
            // 
            CategoryColumn.HeaderText = "Category";
            CategoryColumn.MinimumWidth = 6;
            CategoryColumn.Name = "CategoryColumn";
            CategoryColumn.ReadOnly = true;
            CategoryColumn.Width = 150;
            // 
            // SimilarityColumn
            // 
            SimilarityColumn.HeaderText = "Similarity";
            SimilarityColumn.MinimumWidth = 6;
            SimilarityColumn.Name = "SimilarityColumn";
            SimilarityColumn.ReadOnly = true;
            SimilarityColumn.Width = 180;
            // 
            // LinkColumn
            // 
            LinkColumn.HeaderText = "Link";
            LinkColumn.Name = "LinkColumn";
            LinkColumn.ReadOnly = true;
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBox1.Controls.Add(OpenExcelReportButton);
            groupBox1.Controls.Add(groupBox4);
            groupBox1.Controls.Add(DisplayedRowsLabel);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(SelectedCommentsDataGridView);
            groupBox1.Font = new Font("Century Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point);
            groupBox1.ForeColor = SystemColors.MenuHighlight;
            groupBox1.Location = new Point(24, 9);
            groupBox1.Margin = new Padding(3, 2, 3, 2);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(3, 2, 3, 2);
            groupBox1.Size = new Size(1210, 794);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "Processed comments";
            // 
            // OpenExcelReportButton
            // 
            OpenExcelReportButton.FlatStyle = FlatStyle.Flat;
            OpenExcelReportButton.Location = new Point(1041, 751);
            OpenExcelReportButton.Name = "OpenExcelReportButton";
            OpenExcelReportButton.Size = new Size(169, 38);
            OpenExcelReportButton.TabIndex = 17;
            OpenExcelReportButton.Text = "Open Excel Report";
            OpenExcelReportButton.UseVisualStyleBackColor = true;
            OpenExcelReportButton.Click += OpenExcelReportButton_Click;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(SelectedDateRadioButton);
            groupBox4.Controls.Add(LastMonthRadioButton);
            groupBox4.Controls.Add(LastWeekRadioButton);
            groupBox4.Controls.Add(Last3DaysRadioButton);
            groupBox4.Controls.Add(TodayRadioButton);
            groupBox4.Controls.Add(label7);
            groupBox4.Controls.Add(CategoryFilterTextBox);
            groupBox4.Controls.Add(label12);
            groupBox4.Controls.Add(TextFilterTextBox);
            groupBox4.Controls.Add(label5);
            groupBox4.Controls.Add(AuthorFilterTextBox);
            groupBox4.Controls.Add(label2);
            groupBox4.Controls.Add(PostFilterTextBox);
            groupBox4.Controls.Add(label4);
            groupBox4.Controls.Add(label1);
            groupBox4.Controls.Add(CommunityFilterTextBox);
            groupBox4.Controls.Add(RefreshButton);
            groupBox4.Controls.Add(ToDate);
            groupBox4.Controls.Add(label14);
            groupBox4.Controls.Add(FromDate);
            groupBox4.ForeColor = SystemColors.MenuHighlight;
            groupBox4.Location = new Point(6, 24);
            groupBox4.Margin = new Padding(3, 2, 3, 2);
            groupBox4.Name = "groupBox4";
            groupBox4.Padding = new Padding(3, 2, 3, 2);
            groupBox4.Size = new Size(1204, 143);
            groupBox4.TabIndex = 16;
            groupBox4.TabStop = false;
            groupBox4.Text = "Filter";
            // 
            // SelectedDateRadioButton
            // 
            SelectedDateRadioButton.AutoSize = true;
            SelectedDateRadioButton.Font = new Font("Century Gothic", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            SelectedDateRadioButton.Location = new Point(823, 24);
            SelectedDateRadioButton.Name = "SelectedDateRadioButton";
            SelectedDateRadioButton.Size = new Size(134, 22);
            SelectedDateRadioButton.TabIndex = 32;
            SelectedDateRadioButton.Text = "Selected date:";
            SelectedDateRadioButton.UseVisualStyleBackColor = true;
            // 
            // LastMonthRadioButton
            // 
            LastMonthRadioButton.AutoSize = true;
            LastMonthRadioButton.Font = new Font("Century Gothic", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            LastMonthRadioButton.Location = new Point(695, 108);
            LastMonthRadioButton.Name = "LastMonthRadioButton";
            LastMonthRadioButton.Size = new Size(103, 22);
            LastMonthRadioButton.TabIndex = 31;
            LastMonthRadioButton.Text = "Last month";
            LastMonthRadioButton.UseVisualStyleBackColor = true;
            // 
            // LastWeekRadioButton
            // 
            LastWeekRadioButton.AutoSize = true;
            LastWeekRadioButton.Font = new Font("Century Gothic", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            LastWeekRadioButton.Location = new Point(694, 80);
            LastWeekRadioButton.Name = "LastWeekRadioButton";
            LastWeekRadioButton.Size = new Size(98, 22);
            LastWeekRadioButton.TabIndex = 30;
            LastWeekRadioButton.Text = "Last week";
            LastWeekRadioButton.UseVisualStyleBackColor = true;
            // 
            // Last3DaysRadioButton
            // 
            Last3DaysRadioButton.AutoSize = true;
            Last3DaysRadioButton.Font = new Font("Century Gothic", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            Last3DaysRadioButton.Location = new Point(694, 52);
            Last3DaysRadioButton.Name = "Last3DaysRadioButton";
            Last3DaysRadioButton.Size = new Size(104, 22);
            Last3DaysRadioButton.TabIndex = 29;
            Last3DaysRadioButton.Text = "Last 3 days";
            Last3DaysRadioButton.UseVisualStyleBackColor = true;
            // 
            // TodayRadioButton
            // 
            TodayRadioButton.AutoSize = true;
            TodayRadioButton.Checked = true;
            TodayRadioButton.Font = new Font("Century Gothic", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            TodayRadioButton.Location = new Point(694, 24);
            TodayRadioButton.Name = "TodayRadioButton";
            TodayRadioButton.Size = new Size(71, 22);
            TodayRadioButton.TabIndex = 28;
            TodayRadioButton.TabStop = true;
            TodayRadioButton.Text = "Today";
            TodayRadioButton.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Century Gothic", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            label7.Location = new Point(303, 42);
            label7.Name = "label7";
            label7.Size = new Size(82, 18);
            label7.TabIndex = 27;
            label7.Text = "Category:";
            // 
            // CategoryFilterTextBox
            // 
            CategoryFilterTextBox.Font = new Font("Century Gothic", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            CategoryFilterTextBox.Location = new Point(406, 38);
            CategoryFilterTextBox.Name = "CategoryFilterTextBox";
            CategoryFilterTextBox.Size = new Size(136, 26);
            CategoryFilterTextBox.TabIndex = 26;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Century Gothic", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            label12.Location = new Point(43, 98);
            label12.Name = "label12";
            label12.Size = new Size(40, 18);
            label12.TabIndex = 25;
            label12.Text = "Text:";
            // 
            // TextFilterTextBox
            // 
            TextFilterTextBox.Font = new Font("Century Gothic", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            TextFilterTextBox.Location = new Point(146, 94);
            TextFilterTextBox.Name = "TextFilterTextBox";
            TextFilterTextBox.Size = new Size(396, 26);
            TextFilterTextBox.TabIndex = 24;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Century Gothic", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            label5.Location = new Point(303, 70);
            label5.Name = "label5";
            label5.Size = new Size(60, 18);
            label5.TabIndex = 23;
            label5.Text = "Author:";
            // 
            // AuthorFilterTextBox
            // 
            AuthorFilterTextBox.Font = new Font("Century Gothic", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            AuthorFilterTextBox.Location = new Point(406, 66);
            AuthorFilterTextBox.Name = "AuthorFilterTextBox";
            AuthorFilterTextBox.Size = new Size(136, 26);
            AuthorFilterTextBox.TabIndex = 22;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Century Gothic", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(43, 70);
            label2.Name = "label2";
            label2.Size = new Size(40, 18);
            label2.TabIndex = 21;
            label2.Text = "Post:";
            // 
            // PostFilterTextBox
            // 
            PostFilterTextBox.Font = new Font("Century Gothic", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            PostFilterTextBox.Location = new Point(146, 66);
            PostFilterTextBox.Name = "PostFilterTextBox";
            PostFilterTextBox.Size = new Size(136, 26);
            PostFilterTextBox.TabIndex = 20;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Century Gothic", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            label4.Location = new Point(843, 86);
            label4.Name = "label4";
            label4.Size = new Size(28, 18);
            label4.TabIndex = 19;
            label4.Text = "To:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Century Gothic", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(43, 42);
            label1.Name = "label1";
            label1.Size = new Size(97, 18);
            label1.TabIndex = 17;
            label1.Text = "Community:";
            // 
            // CommunityFilterTextBox
            // 
            CommunityFilterTextBox.Font = new Font("Century Gothic", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            CommunityFilterTextBox.Location = new Point(146, 38);
            CommunityFilterTextBox.Name = "CommunityFilterTextBox";
            CommunityFilterTextBox.Size = new Size(136, 26);
            CommunityFilterTextBox.TabIndex = 16;
            // 
            // RefreshButton
            // 
            RefreshButton.FlatStyle = FlatStyle.Flat;
            RefreshButton.Location = new Point(1053, 51);
            RefreshButton.Name = "RefreshButton";
            RefreshButton.Size = new Size(121, 54);
            RefreshButton.TabIndex = 14;
            RefreshButton.Text = "Refresh";
            RefreshButton.UseVisualStyleBackColor = true;
            RefreshButton.Click += RefreshButton_Click;
            // 
            // ToDate
            // 
            ToDate.Enabled = false;
            ToDate.Font = new Font("Century Gothic", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            ToDate.Location = new Point(877, 80);
            ToDate.Margin = new Padding(3, 2, 3, 2);
            ToDate.Name = "ToDate";
            ToDate.Size = new Size(146, 26);
            ToDate.TabIndex = 8;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new Font("Century Gothic", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            label14.Location = new Point(823, 57);
            label14.Name = "label14";
            label14.Size = new Size(48, 18);
            label14.TabIndex = 7;
            label14.Text = "From:";
            // 
            // FromDate
            // 
            FromDate.Enabled = false;
            FromDate.Font = new Font("Century Gothic", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            FromDate.Location = new Point(877, 51);
            FromDate.Margin = new Padding(3, 2, 3, 2);
            FromDate.Name = "FromDate";
            FromDate.Size = new Size(146, 26);
            FromDate.TabIndex = 6;
            // 
            // DisplayedRowsLabel
            // 
            DisplayedRowsLabel.AutoSize = true;
            DisplayedRowsLabel.Font = new Font("Century Gothic", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            DisplayedRowsLabel.ForeColor = SystemColors.ButtonShadow;
            DisplayedRowsLabel.Location = new Point(1182, 22);
            DisplayedRowsLabel.Name = "DisplayedRowsLabel";
            DisplayedRowsLabel.Size = new Size(17, 19);
            DisplayedRowsLabel.TabIndex = 15;
            DisplayedRowsLabel.Text = "0";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Century Gothic", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            label3.ForeColor = SystemColors.ButtonShadow;
            label3.Location = new Point(1061, 22);
            label3.Name = "label3";
            label3.Size = new Size(119, 19);
            label3.TabIndex = 14;
            label3.Text = "Displayed rows: ";
            // 
            // ProcessedCommentsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLightLight;
            ClientSize = new Size(1260, 812);
            Controls.Add(groupBox1);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 2, 3, 2);
            Name = "ProcessedCommentsForm";
            Text = "SuspiciousCommentsInDetails";
            Load += SelectedCommentsForm_Load;
            ((System.ComponentModel.ISupportInitialize)SelectedCommentsDataGridView).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView SelectedCommentsDataGridView;
        private GroupBox groupBox1;
        private Label DisplayedRowsLabel;
        private Label label3;
        private GroupBox groupBox4;
        private DateTimePicker ToDate;
        private Label label14;
        private DateTimePicker FromDate;
        private Button RefreshButton;
        private DataGridViewTextBoxColumn DateColumn;
        private DataGridViewTextBoxColumn TextColumn;
        private DataGridViewTextBoxColumn CategoryColumn;
        private DataGridViewTextBoxColumn SimilarityColumn;
        private DataGridViewLinkColumn LinkColumn;
        private Label label1;
        private TextBox CommunityFilterTextBox;
        private Label label4;
        private Button OpenExcelReportButton;
        private SaveFileDialog SaveReportDialog;
        private Label label12;
        private TextBox TextFilterTextBox;
        private Label label5;
        private TextBox AuthorFilterTextBox;
        private Label label2;
        private TextBox PostFilterTextBox;
        private Label label7;
        private TextBox CategoryFilterTextBox;
        private RadioButton LastMonthRadioButton;
        private RadioButton LastWeekRadioButton;
        private RadioButton Last3DaysRadioButton;
        private RadioButton TodayRadioButton;
        private RadioButton SelectedDateRadioButton;
    }
}