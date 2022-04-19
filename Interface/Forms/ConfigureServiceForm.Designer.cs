namespace Interface.Forms
{
    partial class ConfigureServiceForm
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
            this.DataAnalysisServiceHost = new System.Windows.Forms.TextBox();
            this.DataCollectionServiceHost = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // DataAnalysisServiceHost
            // 
            this.DataAnalysisServiceHost.Location = new System.Drawing.Point(12, 45);
            this.DataAnalysisServiceHost.Name = "DataAnalysisServiceHost";
            this.DataAnalysisServiceHost.Size = new System.Drawing.Size(240, 27);
            this.DataAnalysisServiceHost.TabIndex = 0;
            this.DataAnalysisServiceHost.Text = "http://localhost:5136";
            // 
            // DataCollectionServiceHost
            // 
            this.DataCollectionServiceHost.Location = new System.Drawing.Point(12, 12);
            this.DataCollectionServiceHost.Name = "DataCollectionServiceHost";
            this.DataCollectionServiceHost.Size = new System.Drawing.Size(240, 27);
            this.DataCollectionServiceHost.TabIndex = 1;
            this.DataCollectionServiceHost.Text = "http://localhost:5000";
            // 
            // ConfigureServiceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1107, 864);
            this.Controls.Add(this.DataCollectionServiceHost);
            this.Controls.Add(this.DataAnalysisServiceHost);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ConfigureServiceForm";
            this.Load += new System.EventHandler(this.ConfigureAnalysisService_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public TextBox DataAnalysisServiceHost;
        public TextBox DataCollectionServiceHost;
    }
}