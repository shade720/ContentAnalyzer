namespace Interface.Forms
{
    partial class AllCommentsForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.AllCommentsDataGridView = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PostId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GroupId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AuthorId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Comment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SearchTextBox = new System.Windows.Forms.TextBox();
            this.SearchComboBox = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.DisplayedRowsLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.AllCommentsDataGridView)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // AllCommentsDataGridView
            // 
            this.AllCommentsDataGridView.AllowUserToAddRows = false;
            this.AllCommentsDataGridView.AllowUserToDeleteRows = false;
            this.AllCommentsDataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.AllCommentsDataGridView.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.AllCommentsDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.AllCommentsDataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.AllCommentsDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.AllCommentsDataGridView.ColumnHeadersHeight = 29;
            this.AllCommentsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.AllCommentsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.PostId,
            this.GroupId,
            this.AuthorId,
            this.Date,
            this.Comment});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.Teal;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.AllCommentsDataGridView.DefaultCellStyle = dataGridViewCellStyle6;
            this.AllCommentsDataGridView.EnableHeadersVisualStyles = false;
            this.AllCommentsDataGridView.GridColor = System.Drawing.SystemColors.ControlLightLight;
            this.AllCommentsDataGridView.Location = new System.Drawing.Point(6, 88);
            this.AllCommentsDataGridView.Name = "AllCommentsDataGridView";
            this.AllCommentsDataGridView.ReadOnly = true;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.AllCommentsDataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.AllCommentsDataGridView.RowHeadersVisible = false;
            this.AllCommentsDataGridView.RowHeadersWidth = 51;
            this.AllCommentsDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.InfoText;
            dataGridViewCellStyle8.NullValue = null;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.InfoText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.AllCommentsDataGridView.RowsDefaultCellStyle = dataGridViewCellStyle8;
            this.AllCommentsDataGridView.RowTemplate.Height = 29;
            this.AllCommentsDataGridView.Size = new System.Drawing.Size(1040, 747);
            this.AllCommentsDataGridView.TabIndex = 1;
            // 
            // Id
            // 
            this.Id.Frozen = true;
            this.Id.HeaderText = "Id";
            this.Id.MinimumWidth = 30;
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Id.Width = 125;
            // 
            // PostId
            // 
            this.PostId.Frozen = true;
            this.PostId.HeaderText = "Post Id";
            this.PostId.MinimumWidth = 6;
            this.PostId.Name = "PostId";
            this.PostId.ReadOnly = true;
            this.PostId.Width = 125;
            // 
            // GroupId
            // 
            this.GroupId.Frozen = true;
            this.GroupId.HeaderText = "Group Id";
            this.GroupId.MinimumWidth = 6;
            this.GroupId.Name = "GroupId";
            this.GroupId.ReadOnly = true;
            this.GroupId.Width = 125;
            // 
            // AuthorId
            // 
            this.AuthorId.Frozen = true;
            this.AuthorId.HeaderText = "Author Id";
            this.AuthorId.MinimumWidth = 6;
            this.AuthorId.Name = "AuthorId";
            this.AuthorId.ReadOnly = true;
            this.AuthorId.Width = 125;
            // 
            // Date
            // 
            this.Date.Frozen = true;
            this.Date.HeaderText = "Date";
            this.Date.MinimumWidth = 6;
            this.Date.Name = "Date";
            this.Date.ReadOnly = true;
            this.Date.Width = 125;
            // 
            // Comment
            // 
            this.Comment.Frozen = true;
            this.Comment.HeaderText = "Comment";
            this.Comment.MinimumWidth = 30;
            this.Comment.Name = "Comment";
            this.Comment.ReadOnly = true;
            this.Comment.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Comment.Width = 400;
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
            this.SearchTextBox.Size = new System.Drawing.Size(844, 30);
            this.SearchTextBox.TabIndex = 0;
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
            "Id",
            "Post id",
            "Group id",
            "Author id",
            "Date",
            "Comment text"});
            this.SearchComboBox.Location = new System.Drawing.Point(6, 51);
            this.SearchComboBox.Name = "SearchComboBox";
            this.SearchComboBox.Size = new System.Drawing.Size(175, 31);
            this.SearchComboBox.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.DisplayedRowsLabel);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.SearchTextBox);
            this.groupBox1.Controls.Add(this.AllCommentsDataGridView);
            this.groupBox1.Controls.Add(this.SearchComboBox);
            this.groupBox1.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.groupBox1.Location = new System.Drawing.Point(28, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1050, 840);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "All collected comments ";
            // 
            // DisplayedRowsLabel
            // 
            this.DisplayedRowsLabel.AutoSize = true;
            this.DisplayedRowsLabel.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.DisplayedRowsLabel.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.DisplayedRowsLabel.Location = new System.Drawing.Point(974, 22);
            this.DisplayedRowsLabel.Name = "DisplayedRowsLabel";
            this.DisplayedRowsLabel.Size = new System.Drawing.Size(19, 21);
            this.DisplayedRowsLabel.TabIndex = 17;
            this.DisplayedRowsLabel.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.label3.Location = new System.Drawing.Point(835, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(143, 21);
            this.label3.TabIndex = 16;
            this.label3.Text = "Displayed rows: ";
            // 
            // AllCommentsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(1107, 864);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AllCommentsForm";
            this.Load += new System.EventHandler(this.AllCommentsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.AllCommentsDataGridView)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private DataGridView AllCommentsDataGridView;
        private TextBox SearchTextBox;
        private ComboBox SearchComboBox;
        private GroupBox groupBox1;
        private DataGridViewTextBoxColumn Id;
        private DataGridViewTextBoxColumn PostId;
        private DataGridViewTextBoxColumn GroupId;
        private DataGridViewTextBoxColumn AuthorId;
        private DataGridViewTextBoxColumn Date;
        private DataGridViewTextBoxColumn Comment;
        private Label DisplayedRowsLabel;
        private Label label3;
    }
}