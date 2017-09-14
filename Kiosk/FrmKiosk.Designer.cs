namespace Kiosk
{
    partial class frmkiosk
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmkiosk));
            this.picture = new System.Windows.Forms.PictureBox();
            this.label = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.picture)).BeginInit();
            this.SuspendLayout();
            // 
            // picture
            // 
            this.picture.BackColor = System.Drawing.Color.Transparent;
            this.picture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.picture.Image = ((System.Drawing.Image)(resources.GetObject("picture.Image")));
            this.picture.Location = new System.Drawing.Point(2, 5);
            this.picture.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.picture.Name = "picture";
            this.picture.Size = new System.Drawing.Size(1194, 196);
            this.picture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picture.TabIndex = 0;
            this.picture.TabStop = false;
            // 
            // label
            // 
            this.label.BackColor = System.Drawing.Color.Transparent;
            this.label.Location = new System.Drawing.Point(-1, 205);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(1197, 10);
            this.label.TabIndex = 1;
            this.label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmkiosk
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.BorderStyle = MetroFramework.Drawing.MetroBorderStyle.None;
            this.ClientSize = new System.Drawing.Size(1203, 961);
            this.ControlBox = false;
            this.Controls.Add(this.label);
            this.Controls.Add(this.picture);
            this.DisplayHeader = false;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Movable = false;
            this.Name = "frmkiosk";
            this.Padding = new System.Windows.Forms.Padding(20, 38, 20, 20);
            this.Resizable = false;
            this.ShadowType = MetroFramework.Forms.MetroForm.MetroFormShadowType.Flat;
            this.ShowInTaskbar = false;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResizeBegin += new System.EventHandler(this.frmkiosk_ResizeBegin);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frmkiosk_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.picture)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picture;
        private System.Windows.Forms.Label label;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}

