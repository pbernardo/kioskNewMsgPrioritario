using System;
using System.Windows.Forms;
using MetroFramework.Forms;



namespace Kiosk
{
    public partial class Frmconfig : MetroForm
    {

        Config _config;

        public Frmconfig()
        {
            
            _config = new Config();

            InitializeComponent();

               LoadControls();
           
            
        }


        void LoadControls()
        {
            try
            {
                host.Text = _config.host;

                local.DataSource = _config.LocalNames(false);
                local.ValueMember = "Id";
                local.DisplayMember = "Text";

                kiosk.DataSource = _config.kioskNames(false);
                kiosk.ValueMember = "Id";
                kiosk.DisplayMember = "Text";
                txtpath.Text = _config.PathImagem;

            }
            catch(Exception e)
            {

            }


        }









        private void save_Click(object sender, EventArgs e)
        {

            try
            {
                _config.host = host.Text;

                _config.idlocal = ((Config.Itemdata)local.SelectedItem).Id;
                _config.idkiosk = ((Config.Itemdata)kiosk.SelectedItem).Id;
                _config.kioskname = ((Config.Itemdata)kiosk.SelectedItem).Text;
                _config.Localname = ((Config.Itemdata)local.SelectedItem).Text;
                _config.PathImagem = txtpath.Text;

                _config.Save();
            }
            catch (Exception) { }
 
            this.Close();

        }

        private void btrefresh_Click(object sender, EventArgs e)
        {

            if (host.Text.Length == 0)
            {
                System.Windows.Forms.MessageBox.Show("Atenção O host tem de ser preenchido");
                return;
            }



            _config.host = host.Text;
            local.DataSource = _config.LocalNames(true);
            local.ValueMember = "Id";
            local.DisplayMember = "Text";

            kiosk.DataSource = _config.kioskNames(true);
            kiosk.ValueMember = "Id";
            kiosk.DisplayMember = "Text";

        }

        private void bttfind_Click(object sender, EventArgs e)
        {
            OpenFileDialog theDialog = new OpenFileDialog();
            theDialog.Title = "Open Image File";
            theDialog.Filter = "Image files|";
            theDialog.InitialDirectory = @"" + System.IO.Path.GetDirectoryName(Application.ExecutablePath);

            if (theDialog.ShowDialog() == DialogResult.OK)
            {
                txtpath.Text = theDialog.FileName;
            }



        }


    }
}
