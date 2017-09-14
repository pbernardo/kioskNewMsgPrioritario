using MetroFramework.Forms;
using System.Windows.Forms;



namespace Kiosk
{
    public partial class frmgif : MetroForm 
    {

        int _width;
        int _heigth;



        public frmgif()
        {
            InitializeComponent();

        }

        public void ShowUp()
        {

            _width = _Sizeparent.Width;
            _heigth = _Sizeparent.Height;

            _width = _Sizeparent.Width - ((_width * 20) / 100);
            _heigth = _Sizeparent.Height - ((_heigth * 20) / 100);


            this.Width = _width;
            this.Height = _heigth;


            label1.Width = this.Width;
            label3.Width = this.Width;

            int x = (((this.Width) / 2) - (printergif.Width / 2));

            printergif.Location = new System.Drawing.Point(x, printergif.Location.Y);



            this.Show();
            //label2.Text = contador; 
        }


        
        public System.Drawing.Size _Sizeparent
        {
            get;
            set;
        }  
       


        public string departamento
        {
            set { label3.Text = value;  }
        }


        public string contador
        {
            get;set;
        }

    }
}
