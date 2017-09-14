using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
//using System.Windows.Input;
using MetroFramework.Forms;
using System.Threading;

namespace Kiosk
{
    public partial class frmkiosk :MetroForm 
    {

        Kiosk _bttk;
        frmgif _frmgif;
        public delegate void InvokeDelegate();

       

        public frmkiosk()
        {
            InitializeComponent();

            try
            {


                if (Properties.Settings.Default.host != "")
                {

                    _bttk = new Kiosk();
                    _bttk.Load_kiosk();

                    
                    DesignHeader();
                    DesignKiosk();

                    _bttk.PrintingEvent += new PrintingEventHandler(On_printing);
                    _bttk.PrintingFinishEvent += new PrintingFinishEventHandler(Finish_printing);
                    _bttk.NextTimeDepartments += new DepartamentsGroupEventHandler(On_timeRefreshButtons);

                }
            }
            catch(Exception e) { }

        }



       
        void ReloadKiosk()
        {
           
            this.OnResizeBegin(EventArgs.Empty);
        }




        public void On_timeRefreshButtons(object sender, EventArgs e)
        {
            if (_bttk != null)
                this.BeginInvoke(new InvokeDelegate(ReloadKiosk));


        }




        public void On_printing(object sender, EventArgs e)
        {

            Kiosk.ButtonKiosk btt  = (Kiosk.ButtonKiosk)sender;
                
            _frmgif = new frmgif();

            _frmgif.departamento = "" + btt.departamento ;
            // _frmgif.contador = _bttk.ContadorActual;


            _frmgif._Sizeparent = new Size(this.Width, this.Height);
                

            _frmgif.ShowUp();




        }





        public void Finish_printing(object sender, EventArgs e)
        {
            if (_frmgif != null)
            {

               // _frmgif.contador = _bttk.ContadorActual;

                _frmgif.Dispose();
                _frmgif = null;
            }

        }



        void controlsremove()
        {
            int count=0;

            count=_bttk.buttonkiosk.Count();   

             foreach (Kiosk.ButtonKiosk btt in _bttk.buttonkiosk)
            {
                this.Controls.Remove(btt.button );
             }

             _bttk.buttonkiosk.Clear();
        }





        void DesignHeader()
        {
           
            picture.Location = new Point(0, 10);


            picture.Image = new Bitmap("" + _bttk.PathImageKiosk);
            picture.Size = new Size(this.Width, picture.Height);
            picture.Refresh();



            //label.Location = new Point(this.Width / 14, picture.Height + 10);
            label.Location = new Point(this.Width / 14, picture.Height);



        }





        void DesignKiosk()
        {
            int y=picture.Height +100;
           // int y = 10;
            int x = 5;
            int i = 1;

            int heigth=0;
            int posyaux=0;
            int heigthinicial = 0;

            try
            {

                this.BackColor = _bttk.CorFundo;



               // System.Windows.Forms.MessageBox.Show("" + SystemInformation.VirtualScreen.Width + " ----" + SystemInformation.VirtualScreen.Height);     

                this.Width = SystemInformation.VirtualScreen.Width;

                heigthinicial = SystemInformation.VirtualScreen.Height - (y) - (((SystemInformation.VirtualScreen.Height - (y))*20)/100);

                heigth = (heigthinicial / _bttk.buttonkiosk.Count);

              //  if (heigth >= 200) heigth = 250;

                if ((heigth * _bttk.buttonkiosk.Count)+y < (heigthinicial ))
                {
                    posyaux = heigthinicial - (heigth * _bttk.buttonkiosk.Count);

                    posyaux = posyaux / 2;
                    y=y+posyaux;
                }



                
                               
                
                foreach (Kiosk.ButtonKiosk btt in _bttk.buttonkiosk)
                {

                    if (i == 1)
                    {
                        label.Width = this.Width;
                        label.Font = new Font("Arial", btt.button.FontSizeDepartamento);
                        label.ForeColor = btt.button.CaptionBackcolor;              
                    }



                    btt.button.Size = new Size((this.Width - (x * 2)), heigth  );
                    if (i != 1)
                        y = y + (btt.button.Height)+10;

                    btt.button.Location = new Point(x, y);
                    this.Controls.Add(btt.button);
                    i++;
                    
                }

                
               

            }
            catch (Exception ) { } 
           
        }


      

        private void frmkiosk_KeyPress(object sender, KeyPressEventArgs e)
        {

            MetroForm frm;
             
            switch (Convert.ToInt32( e.KeyChar)){
                case 20: //tecla ctrl + t 

                    frm = new Frmconfig();
                    frm.ShowDialog();

                    break;
                 case 5:


                    if (_bttk != null)
                    {
                        _bttk.close(); 
                        _bttk = null;
                    }

                    this.Dispose();

                    break ;
            }
                
        }


 
        private void frmkiosk_ResizeBegin(object sender, EventArgs e)
        {

                controlsremove();
                _bttk.Load_kiosk();
                DesignKiosk();
            

        }




       






    }



}
