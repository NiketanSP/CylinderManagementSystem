namespace CylinderManagementSystem
{
    partial class Report_CDC
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
            this.Crpt_CDC = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // Crpt_CDC
            // 
            this.Crpt_CDC.ActiveViewIndex = -1;
            this.Crpt_CDC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Crpt_CDC.Cursor = System.Windows.Forms.Cursors.Default;
            this.Crpt_CDC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Crpt_CDC.Location = new System.Drawing.Point(0, 0);
            this.Crpt_CDC.Name = "Crpt_CDC";
            this.Crpt_CDC.Size = new System.Drawing.Size(1174, 688);
            this.Crpt_CDC.TabIndex = 0;
            // 
            // CDC_Report
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1174, 688);
            this.Controls.Add(this.Crpt_CDC);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "CDC_Report";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DELIVERY CHALLAN ";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.CDC_Report_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer Crpt_CDC;
    }
}