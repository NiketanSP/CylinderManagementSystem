namespace CylinderManagementSystem
{
    partial class Report_Tax_Particular
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
            this.Crpt_Tax_Particular = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // Crpt_Tax_Particular
            // 
            this.Crpt_Tax_Particular.ActiveViewIndex = -1;
            this.Crpt_Tax_Particular.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Crpt_Tax_Particular.Cursor = System.Windows.Forms.Cursors.Default;
            this.Crpt_Tax_Particular.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Crpt_Tax_Particular.Location = new System.Drawing.Point(0, 0);
            this.Crpt_Tax_Particular.Name = "Crpt_Tax_Particular";
            this.Crpt_Tax_Particular.Size = new System.Drawing.Size(1095, 662);
            this.Crpt_Tax_Particular.TabIndex = 0;
            // 
            // Report_Tax_Particular
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1095, 662);
            this.Controls.Add(this.Crpt_Tax_Particular);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Report_Tax_Particular";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TAX INVOICE";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Report_Tax_Particular_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer Crpt_Tax_Particular;
    }
}