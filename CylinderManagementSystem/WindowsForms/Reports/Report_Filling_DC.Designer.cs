namespace CylinderManagementSystem
{
    partial class Report_Filling_DC
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
            this.Crpt_Filling = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // Crpt_Filling
            // 
            this.Crpt_Filling.ActiveViewIndex = -1;
            this.Crpt_Filling.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Crpt_Filling.Cursor = System.Windows.Forms.Cursors.Default;
            this.Crpt_Filling.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Crpt_Filling.Location = new System.Drawing.Point(0, 0);
            this.Crpt_Filling.Name = "Crpt_Filling";
            this.Crpt_Filling.Size = new System.Drawing.Size(1086, 627);
            this.Crpt_Filling.TabIndex = 0;
            // 
            // CRPT_Filling_DC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1086, 627);
            this.Controls.Add(this.Crpt_Filling);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "CRPT_Filling_DC";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FILLING DELIVERY CHALLAN";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.CRPT_Filling_DC_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer Crpt_Filling;
    }
}