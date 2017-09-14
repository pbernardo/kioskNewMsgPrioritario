namespace Kiosk
{
    partial class frmgif
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmgif));
            this.printergif = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.printergif)).BeginInit();
            this.SuspendLayout();
            // 
            // printergif
            // 
            this.printergif.Image = ((System.Drawing.Image)(resources.GetObject("printergif.Image")));
            this.printergif.Location = new System.Drawing.Point(461, 513);
            this.printergif.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.printergif.Name = "printergif";
            this.printergif.Size = new System.Drawing.Size(252, 436);
            this.printergif.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.printergif.TabIndex = 0;
            this.printergif.TabStop = false;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 28F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(-10, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1190, 146);
            this.label1.TabIndex = 1;
            this.label1.Text = "A sua senha";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 28F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(1, 166);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1179, 167);
            this.label3.TabIndex = 3;
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmgif
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1183, 969);
            this.ControlBox = false;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.printergif);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmgif";
            this.Padding = new System.Windows.Forms.Padding(22, 75, 22, 25);
            ((System.ComponentModel.ISupportInitialize)(this.printergif)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox printergif;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
    }
}