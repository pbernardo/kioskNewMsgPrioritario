using System;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;


namespace Kiosk
{
    public partial class Frmcrviewer : Form
    {
        

        public Frmcrviewer()
        {
            InitializeComponent();

            

        }

        public void Show()
        {
           // crt.PrintReport();
            crt.RefreshReport();
            ShowDialog();

              

        }

        public ReportDocument Document
        {
            set { crt.ReportSource = value;}
        }

        private void crt_Load(object sender, EventArgs e)
        {

        }





    }
}
