namespace Kiosk
{
    partial class Frmconfig
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
            this.host = new MetroFramework.Controls.MetroTextBox();
            this.local = new MetroFramework.Controls.MetroComboBox();
            this.kiosk = new MetroFramework.Controls.MetroComboBox();
            this.lbhost = new MetroFramework.Controls.MetroLabel();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.save = new MetroFramework.Controls.MetroButton();
            this.btrefresh = new MetroFramework.Controls.MetroButton();
            this.titlebalcao = new MetroFramework.Controls.MetroTile();
            this.metroTile1 = new MetroFramework.Controls.MetroTile();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.txtpath = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.bttfind = new MetroFramework.Controls.MetroButton();
            this.SuspendLayout();
            // 
            // host
            // 
            this.host.Location = new System.Drawing.Point(53, 97);
            this.host.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.host.Name = "host";
            this.host.Size = new System.Drawing.Size(418, 41);
            this.host.TabIndex = 1;
            this.host.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // local
            // 
            this.local.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.local.FormattingEnabled = true;
            this.local.ItemHeight = 29;
            this.local.Location = new System.Drawing.Point(53, 178);
            this.local.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.local.Name = "local";
            this.local.Size = new System.Drawing.Size(585, 35);
            this.local.TabIndex = 2;
            // 
            // kiosk
            // 
            this.kiosk.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kiosk.FormattingEnabled = true;
            this.kiosk.ItemHeight = 21;
            this.kiosk.Location = new System.Drawing.Point(53, 246);
            this.kiosk.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.kiosk.Name = "kiosk";
            this.kiosk.Size = new System.Drawing.Size(585, 27);
            this.kiosk.TabIndex = 3;
            // 
            // lbhost
            // 
            this.lbhost.Location = new System.Drawing.Point(53, 76);
            this.lbhost.Name = "lbhost";
            this.lbhost.Size = new System.Drawing.Size(418, 18);
            this.lbhost.TabIndex = 4;
            this.lbhost.Text = "Host";
            this.lbhost.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // metroLabel1
            // 
            this.metroLabel1.Location = new System.Drawing.Point(53, 158);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(585, 18);
            this.metroLabel1.TabIndex = 5;
            this.metroLabel1.Text = "Local";
            this.metroLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // metroLabel2
            // 
            this.metroLabel2.Location = new System.Drawing.Point(53, 226);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(585, 18);
            this.metroLabel2.TabIndex = 6;
            this.metroLabel2.Text = "Kiosk";
            this.metroLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // save
            // 
            this.save.Location = new System.Drawing.Point(54, 351);
            this.save.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(587, 40);
            this.save.TabIndex = 7;
            this.save.Text = "Gravar";
            this.save.Click += new System.EventHandler(this.save_Click);
            // 
            // btrefresh
            // 
            this.btrefresh.Location = new System.Drawing.Point(489, 97);
            this.btrefresh.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btrefresh.Name = "btrefresh";
            this.btrefresh.Size = new System.Drawing.Size(149, 41);
            this.btrefresh.TabIndex = 9;
            this.btrefresh.Text = "Refresh";
            this.btrefresh.Click += new System.EventHandler(this.btrefresh_Click);
            // 
            // titlebalcao
            // 
            this.titlebalcao.FontSize = MetroFramework.Drawing.MetroFontSize.Large;
            this.titlebalcao.FontWeight = MetroFramework.Drawing.MetroFontWeight.Bold;
            this.titlebalcao.Location = new System.Drawing.Point(52, 12);
            this.titlebalcao.Name = "titlebalcao";
            this.titlebalcao.Size = new System.Drawing.Size(587, 48);
            this.titlebalcao.TabIndex = 10;
            this.titlebalcao.Text = "Xopvision";
            this.titlebalcao.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // metroTile1
            // 
            this.metroTile1.FontSize = MetroFramework.Drawing.MetroFontSize.Large;
            this.metroTile1.FontWeight = MetroFramework.Drawing.MetroFontWeight.Bold;
            this.metroTile1.Location = new System.Drawing.Point(54, 408);
            this.metroTile1.Name = "metroTile1";
            this.metroTile1.Size = new System.Drawing.Size(585, 10);
            this.metroTile1.TabIndex = 11;
            this.metroTile1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtpath
            // 
            this.txtpath.BackColor = System.Drawing.Color.White;
            this.txtpath.Enabled = false;
            this.txtpath.Location = new System.Drawing.Point(52, 300);
            this.txtpath.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtpath.Name = "txtpath";
            this.txtpath.Size = new System.Drawing.Size(457, 37);
            this.txtpath.TabIndex = 12;
            this.txtpath.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // metroLabel3
            // 
            this.metroLabel3.Location = new System.Drawing.Point(52, 275);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(457, 23);
            this.metroLabel3.TabIndex = 13;
            this.metroLabel3.Text = "Imagem Topo Kiosk";
            this.metroLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bttfind
            // 
            this.bttfind.Location = new System.Drawing.Point(515, 300);
            this.bttfind.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.bttfind.Name = "bttfind";
            this.bttfind.Size = new System.Drawing.Size(126, 37);
            this.bttfind.TabIndex = 14;
            this.bttfind.Text = "Procurar";
            this.bttfind.Click += new System.EventHandler(this.bttfind_Click);
            // 
            // Frmconfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(704, 427);
            this.Controls.Add(this.bttfind);
            this.Controls.Add(this.metroLabel3);
            this.Controls.Add(this.txtpath);
            this.Controls.Add(this.metroTile1);
            this.Controls.Add(this.titlebalcao);
            this.Controls.Add(this.btrefresh);
            this.Controls.Add(this.save);
            this.Controls.Add(this.metroLabel2);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.lbhost);
            this.Controls.Add(this.kiosk);
            this.Controls.Add(this.local);
            this.Controls.Add(this.host);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frmconfig";
            this.Padding = new System.Windows.Forms.Padding(18, 60, 18, 16);
            this.Resizable = false;
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroTextBox host;
        private MetroFramework.Controls.MetroComboBox local;
        private MetroFramework.Controls.MetroComboBox kiosk;
        private MetroFramework.Controls.MetroLabel lbhost;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroButton save;
        private MetroFramework.Controls.MetroButton btrefresh;
        private MetroFramework.Controls.MetroTile titlebalcao;
        private MetroFramework.Controls.MetroTile metroTile1;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private MetroFramework.Controls.MetroTextBox txtpath;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private MetroFramework.Controls.MetroButton bttfind;
    }
}