namespace Kiosk
{
    partial class Frmcrviewer
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
            this.crt = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // crt
            // 
            this.crt.ActiveViewIndex = -1;
            this.crt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crt.Cursor = System.Windows.Forms.Cursors.Default;
            this.crt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crt.Location = new System.Drawing.Point(0, 0);
            this.crt.Name = "crt";
            this.crt.Size = new System.Drawing.Size(1197, 685);
            this.crt.TabIndex = 0;
            this.crt.Load += new System.EventHandler(this.crt_Load);
            // 
            // Frmcrviewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1197, 685);
            this.Controls.Add(this.crt);
            this.Name = "Frmcrviewer";
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer crt;
    }
}