using System;
using System.Collections.Generic;
using System.Collections;
using System.Drawing;
using System.Data;
using System.Xml;
using System.Windows.Forms;
using System.Threading;

namespace Kiosk
{


    public delegate void PrintingEventHandler(object sender, EventArgs e);

    public delegate void PrintingFinishEventHandler(object sender, EventArgs e);

    public delegate void DepartamentsGroupEventHandler(object sender, EventArgs e);


    public class Kiosk
    {

        public event PrintingEventHandler PrintingEvent;
        public event PrintingFinishEventHandler PrintingFinishEvent;
        public event DepartamentsGroupEventHandler NextTimeDepartments;

        private Thread threadnextime = null;

       

        List<ButtonKiosk> _bttkiosk;
        int _idkiosk;
        int _idlocal;
        string _host;
        string _pathimage;

        
        Boolean _clickbutton=false;

        ButtonKiosk  _btt;


        protected virtual void OnNextTimeDepartments(EventArgs e)
        {
            if (NextTimeDepartments != null)
                NextTimeDepartments(this,e);


            
        }



        protected virtual void OnPrinting(EventArgs e)
        {
            if (PrintingEvent != null)

                PrintingEvent(_btt , e);
        }

        protected virtual void FinishPrinting(EventArgs e)
        {
            if (PrintingFinishEvent != null)

                PrintingFinishEvent(this, e);
        }


        public string ContadorActual
        {
            get;set;
        }



       public void close()
        {

            try
            {

                _bttkiosk.Clear();
                _bttkiosk = null;

                if (threadnextime != null)
                {
                    threadnextime.Abort();
                    threadnextime = null;
                }
            }
            catch (ThreadStateException)
            {
               
            }

        }



        //~Kiosk()
        //{

        //    _bttkiosk.Clear();
        //    _bttkiosk = null;

        //    if (threadnextime != null)
        //    {
        //        threadnextime.Abort ();
        //        threadnextime = null;
        //    }

        //}




        public Kiosk()
        {

            _bttkiosk = new List<ButtonKiosk>();
           
            try
            {
                host = Properties.Settings.Default.host;
                idkiosk = Properties.Settings.Default.idkiosk;
                idlocal = Properties.Settings.Default.idlocal;
                _pathimage = Properties.Settings.Default.pathkiosk;

            }
            catch(Exception e) { }
            _pathimage = Properties.Settings.Default.pathkiosk;
           

        }




        public int SecondNextEvent
        {
            get;
            set;
        }
                    


        public String Settings
        {
            get { return KioskSettings(); }
            
        }


        public string PathImageKiosk
        {
            get { return _pathimage; }
        }
        
		
        public Color CorFundo
        {
            get
            {
                System.Drawing.Color col = System.Drawing.ColorTranslator.FromHtml(GetCorFundo_xml());
                return col;
            }
        }


        public List<ButtonKiosk> buttonkiosk
        {
            get { return _bttkiosk; }
        }

        public int idlocal
        {
            get { return _idlocal; }
            set { _idlocal = value; }
        }

        public int idkiosk
        {
            get { return _idkiosk; }
            set { _idkiosk = value; }
        }

        public string host
        {
            get { return host; }
            set { _host = value; }
        }



        

         string GetCorFundo_xml()
            {
                if (Settings.Length == 0)
                {
                    return "";
                }
                else
                {
                    XmlDocument xml = new XmlDocument();
                    xml.LoadXml(Settings);
                    XmlNodeList xnList = xml.SelectNodes("/settings");
                    XmlNode xn;
                    xn = xnList.Item(0);
                    return xn["Color"].InnerText;
                }

            }



        String  KioskSettings()
        {

            String sql;
            DataSet record;
            DataRow dr;

            sql = "select settings from tSenhaKiosk where idkiosk=" + idkiosk;
            record = AdoConectPhp.RowsSet(sql);

            if (record.Tables[0].Rows.Count > 0)
            {
                dr = record.Tables[0].Rows[0];
                return dr["settings"].ToString();
            }
            else
                return "";


        }


        public void Load_kiosk()
        {
            DataSet record;
            int i = 0;

            try
            {

                record = scheduleHelper.getDepartmentsDataset(Properties.Settings.Default.idlocal, Properties.Settings.Default.idkiosk);

                SecondNextEvent = scheduleHelper.secsToNextEvent;

                scheduleHelper.secsToNextEvent = 0;


                // System.Windows.Forms.MessageBox.Show("" + scheduleHelper.minutesToNextEvent);

                foreach (DataRow dr in record.Tables[0].Rows)
                {
                    ButtonKiosk _dpt = new ButtonKiosk();
                    _dpt.country = "pt-PT";
                    _dpt.settings = dr["settings"].ToString();
                    _dpt.iddepartamento = Int32.Parse(dr["iddepartamento"].ToString());
                    //_dpt.departamento = dr["nome"].ToString();
                    _dpt.serie = dr["serie"].ToString();



                    myButtonObject myButton = new myButtonObject();
                    EventHandler myHandler = new EventHandler(btt_Click);
                    myButton.Click += myHandler;
                    myButton.Tag = i++;


                    myButton.CaptionDepartamento = _dpt.departamento;
                    myButton.FontSizeDepartamento = _dpt.FontTextSize;
                    myButton.Caption = "Carregue Aqui.";
                    myButton.CaptionSerie = _dpt.serie;
                    myButton.FontSizeSerie = _dpt.SerieFontSize;


                    myButton.DepartamentoBackColor = _dpt.Cor;
                    myButton.Textcolor = _dpt.TextColor;
                    myButton.CaptionBackcolor = Color.White;
                    myButton.SerieBackcolor = _dpt.Cor;

					//MESSAGE
					myButton.hasmessage = _dpt.hasmessage;
					myButton.messagetext = _dpt.messagetext;
					myButton.messagetype = _dpt.messagetype;

					_dpt.button = myButton;
                    _bttkiosk.Add(_dpt);



                    /*Button  btt = new Button();
                    btt.Tag = i++;
                    btt.Text = _dpt.departamento  + "     " + _dpt.serie ;
                    btt.BackColor =_dpt.Cor;

                    btt.Click += new EventHandler(btt_Click );
                    _dpt.button = btt;
                    _bttkiosk.Add(_dpt);*/

                    //_dpt = null;
                }

                nexttime();
            }
            catch(Exception e) { }

        }


       void fire()
        {

            //System.Windows.Forms.MessageBox.Show(""+ MinuteNextEvent); 

            Thread.Sleep(TimeSpan.FromSeconds(SecondNextEvent));
            OnNextTimeDepartments(EventArgs.Empty);

       
        }


        private void nexttime()
        {
            
            if (threadnextime != null)
            {
                threadnextime.Abort ();
                threadnextime = null;
            }


            if (SecondNextEvent > 0)
            {
                threadnextime = new Thread(fire);
                threadnextime.Start();
            }

        }



        public void SendBalcaoThread()
        {
            Thread t;
            t = new Thread(sendmsgbalcao);
            t.Start();
        }


        public void sendmsgbalcao()
        {
            String sql;
            DataSet record;
            CommandCliente sendbalcao;


            try
            {

                sql = "select ip,porta,notification from tsenhabalcao where tsenhabalcao.ip is not null and ip<>'' ";

                record = AdoConectPhp.RowsSet(sql);

                if (record.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in record.Tables[0].Rows)
                    {
                        sendbalcao = new CommandCliente(dr["ip"].ToString(), dr["porta"].ToString());

                        sendbalcao.Write_Line("Exit;" + dr["notification"].ToString());
                        //senddisplay.Close(); 

                    }
                }
            }
             catch (Exception e)
            {

            }

        }



        public void SendtoBalcao()
        {
            SendBalcaoThread();
        }


        //public void SendtoBalcao()
        //{
        //    String sql;
        //    DataSet record;
        //    CommandCliente sendbalcao;

        //    sql = "select ip,porta from tsenhabalcao where tsenhabalcao.ip is not null and ip<>'' ";

        //    record = AdoConectPhp.RowsSet(sql);

        //    if (record.Tables[0].Rows.Count > 0)
        //    {
        //        foreach (DataRow dr in record.Tables[0].Rows)
        //        {
        //            sendbalcao = new CommandCliente(dr["ip"].ToString(), dr["porta"].ToString());
        //            sendbalcao.Write_Line("Exit");
        //            //senddisplay.Close(); 


        //        }
        //    }
        //}




        private void btt_Click(object sender, EventArgs e)
        {

            DataSet record;
            string sql = "";
            DataRow dr;
            ButtonKiosk bttkiosk;
            myButtonObject btt = (myButtonObject)sender;
            bttkiosk = this.buttonkiosk[(int)btt.Tag];
            PrintingSenha prtsenha = new PrintingSenha() ;
            Boolean _zera=false;
            int _contadordept=0;



			if (btt.hasmessage)
			{
                var messageReturn = CustomMessageBox.Show(btt.messagetext, "", "SIM", "NÃO").ToString();

                //MessageBox.Show(CustomMessageBox.Show(btt.messagetext, "", "SIM", "NÃO").ToString());
                //teste
                if (messageReturn == "No") {
                    return;
                }
                //if (CustomMessageBox.Show(btt.messagetext, "", "SIM", "NÃO") == DialogResult.No) {
                //  
                //} 
			}
				

				
			ContadorActual = "";

            if (_clickbutton)
                return;
            else
                _clickbutton = true;




            // sql = "select insertsenhaqueue("  + bttkiosk.iddepartamento + "," + this.idlocal   + "," +  this.idkiosk  + ") ";
            // record = AdoConectPhp.RowsSet (sql);




            _btt = bttkiosk;
            OnPrinting(EventArgs.Empty);

            Cursor.Current = Cursors.WaitCursor;





            if ((prtsenha.StatusError != PrintingSenha.Status.NoError) && (prtsenha.StatusError != PrintingSenha.Status.Unknown))
            {
                System.Windows.Forms.MessageBox.Show("Contacte equipa de Manutenção :" + prtsenha.Errordescription + " na Impressora ");
                _clickbutton = false;
                Cursor.Current = Cursors.Default;
                FinishPrinting(EventArgs.Empty);
                return;
            }


            sql = "CALL `GetContadorDepartamento`(" + bttkiosk.iddepartamento + ")";
            record = AdoConectPhp.RowsSet(sql);

            if (record.Tables[0].Rows.Count > 0)
            {
                dr=record.Tables[0].Rows[0];
                _contadordept =int.Parse(  dr["numerador"].ToString());
                ContadorActual = (_contadordept+1).ToString();

                // System.Windows.Forms.MessageBox.Show(ContadorActual);  

                //_btt = bttkiosk; 
                //OnPrinting(EventArgs.Empty  );

                if ((_contadordept + 1) >= int.Parse(bttkiosk.ResetCounterNumber)) _zera = true; 
            }

            if (!_zera)
            {
                sql = "Call `getdataresetcounter`(" + bttkiosk.iddepartamento + ",'" + bttkiosk.ResetCounterHour + "')";

                record = AdoConectPhp.RowsSet(sql);
                if (record.Tables[0].Rows.Count > 0)
                {
                    dr = record.Tables[0].Rows[0];

                   // System.Windows.Forms.MessageBox.Show(""+ dr[0].ToString()); 

                    if ( Int32.Parse((dr[0].ToString() ) )==1 ) _zera = true;
                }
            }




            sql = "CALL `insertqueue`(" +  bttkiosk.iddepartamento + "," + this.idkiosk +  "," + (_zera?1:0) + ") ";
            record = AdoConectPhp.RowsSet(sql);

            if (record.Tables[0].Rows.Count > 0)
            {
                dr = record.Tables[0].Rows[0];
               // prtsenha = new PrintingSenha();

                prtsenha.Kiosk = dr["kiosk"].ToString();
                prtsenha.Contador = dr["contadornumerador"].ToString();




                //prtsenha.Departamento = dr["departamento"].ToString();
                prtsenha.Departamento = bttkiosk.departamento;  
                prtsenha.Serie = dr["serie"].ToString();
                prtsenha.Data = dr["Data"].ToString();

                prtsenha.Printing();
            }

            FinishPrinting(EventArgs.Empty);

            SendtoBalcao();
            Cursor.Current = Cursors.Default ;
            _clickbutton = false;
            // throw new NotImplementedException();

            
        }






        public class ButtonKiosk
        {
            myButtonObject _btt;
            int _iddepartamento = 0;
            String _departamento = "";
            string _serie = "";
            string _settings = "";
			string _messagetext = "";
			string _messagetype = "";
           

            Hashtable _departamentolg=new Hashtable();


            public string country
            {
                get;
                set;
            }



            public string ResetCounterNumber
            {
                get { return GetResetCounterNumber(); }
                
            }


            public string ResetCounterHour
            {
                get { return GetResetCounterHour(); }
                
            }



            public void AddDepartamentoLg(String country,String value)
            {
                _departamentolg.Add(country, value); 
            }


            
            public void LoadDepartamento(String _xml)
            {

                XmlNode xml_properties;
                XmlNode xml_property;

                XmlDocument xml = new XmlDocument();
                xml.LoadXml(_xml);
                XmlNodeList xnList = xml.SelectNodes("/settings");

                xml_properties = xnList.Item(0); 

                xml_property = xml_properties["properties"].ChildNodes.Item(0);
                                
                foreach (XmlNode xn in xml_property )
                {
                    
                    AddDepartamentoLg(xn.Attributes["language"].InnerText, xn.InnerText );
                }

             
            }





            public int SerieFontSize
            {
                get
                {
                     return Int32.Parse( getFontseriesize()); 
                }

            }

            public int FontTextSize
            {
                get
                {  
                    return  Int32.Parse(getFonttextsize()) ; 
                }

            }





            public Color Cor
            {
                get
                {
                    System.Drawing.Color col = System.Drawing.ColorTranslator.FromHtml(getcolorSettings_xml());
                    return col;
                   
                }
            }


            public Color TextColor{

                get
                {
                    System.Drawing.Color col = System.Drawing.ColorTranslator.FromHtml(getcolortext ());
                    return col;
                   
                }

           }



            public string settings
            {
                get { return _settings; }
                set { 
                    _settings = value;
                    LoadDepartamento(_settings);
                }
            }

            public myButtonObject button
            {
                get { return _btt; }
                set
                {
                    _btt = value;
                }
            }

            public int iddepartamento
            {
                get { return _iddepartamento; }
                set { _iddepartamento = value; }
            }

            public string departamento
            {
                get { return  _departamentolg[country].ToString();}
                set { _departamento = value; }
            }

            public string serie
            {
                get { return _serie; }
                set { _serie = value; }
            }

			//MESSAGE SETTINGS
			public string messagetext
			{
				get { return _messagetext; }
				set { _messagetext = value; }
			}
			public string messagetype
			{
				get { return _messagetype; }
				set { _messagetype = value; }
			}

			public bool hasmessage
			{
				get
				{
					return hasMessage();
				}

			}


			bool hasMessage()
			{
				if (settings.Length == 0)
				{
					return false;
				}
				else
				{
					XmlDocument xml = new XmlDocument();
					xml.LoadXml(settings);
					XmlNodeList xnList = xml.SelectNodes("/settings");
					XmlNode xn;
					xn = xnList.Item(0);
					if (xn.SelectSingleNode("//message") == null) return false;
					else if (xn.SelectSingleNode("//message").Attributes["status"].Value == "enabled") {
						_messagetext = xn.SelectSingleNode("//message").InnerText;
						_messagetype = xn.SelectSingleNode("//message").Attributes["type"].Value;
						return true;
					} 
					else return false;					
					
				}
			}
			//FIM MESSAGE SETTINGS

			string getcolorSettings_xml()
            {
                if (settings.Length == 0)
                {
                    return "";
                }
                else
                {
                    XmlDocument xml = new XmlDocument();
                    xml.LoadXml(settings);
                    XmlNodeList xnList = xml.SelectNodes("/settings");
                    XmlNode xn;
                    xn = xnList.Item(0);
                    return xn["Color"].InnerText;
                }

            }


            string GetResetCounterNumber()
            {
                if (settings.Length == 0)
                {
                    return "";
                }
                else
                {
                    XmlDocument xml = new XmlDocument();
                    xml.LoadXml(settings);
                    XmlNodeList xnList = xml.SelectNodes("/settings");
                    XmlNode xn;
                    xn = xnList.Item(0);
                    return xn["resetcounternumber"].InnerText;
                }

            }

            string GetResetCounterHour()
            {
                if (settings.Length == 0)
                {
                    return "";
                }
                else
                {
                    XmlDocument xml = new XmlDocument();
                    xml.LoadXml(settings);
                    XmlNodeList xnList = xml.SelectNodes("/settings");
                    XmlNode xn;
                    xn = xnList.Item(0);
                    return xn["ResetCounterHour"].InnerText ;
                }
            }       





            string getFontseriesize()
            {

                try
                {

                    if (settings.Length == 0)
                    {
                        return "";
                    }
                    else
                    {
                        XmlDocument xml = new XmlDocument();
                        xml.LoadXml(settings);
                        XmlNodeList xnList = xml.SelectNodes("/settings");
                        XmlNode xn;


                        xn = xnList.Item(0);
                        return xn["seriefontsize"].InnerText;

                    }

                }
                catch (Exception e)
                {
                    return "16";
                }
            }


            string getFonttextsize()
            {

                try
                {

                    if (settings.Length == 0)
                    {
                        return "";
                    }
                    else
                    {
                        XmlDocument xml = new XmlDocument();
                        xml.LoadXml(settings);
                        XmlNodeList xnList = xml.SelectNodes("/settings");
                        XmlNode xn;


                        xn = xnList.Item(0);
                        return xn["textfontsize"].InnerText;

                    }

                }
                catch (Exception e)
                {
                    return "16"  ;
                }



            }


            string getcolortext()
            {
                if (settings.Length == 0)
                {
                    return "";
                }
                else
                {
                    XmlDocument xml = new XmlDocument();
                    xml.LoadXml(settings);
                    XmlNodeList xnList = xml.SelectNodes("/settings");
                    XmlNode xn;

                    
                        xn = xnList.Item(0);
                        return xn["TextColor"].InnerText;
                                       
                }


            }






        }




        public class myButtonObject : Button
        {

            int _poligono_height;
            int _poligono_width;

            int _poligono1_height;
            int _poligono1_width;

            int _elipseheight;

            int _poligonoX;
            int _poligono1x;
            int _elipsex;
            int _elipsey=0;

            Color _departamentobackcolor;
            Color _Captionbackcolor;
            Color _seriebackcolor;
            Color _textcolor;

            string _captiondeparatamento;
            string _captionserie;
            string _caption;

            int _fontsizedepartamento;
            int _fontsizeserie;

			//Message
			bool _hasmessage;
			string _messagetext;
			string _messagetype;


			public bool hasmessage
			{
				get { return _hasmessage; }
				set { _hasmessage = value; }
			}
			public string messagetext
			{
				get { return _messagetext; }
				set { _messagetext = value; }
			}
			public string messagetype
			{
				get { return _messagetype; }
				set { _messagetype = value; }
			}
			//FIM MESSAGE

			public int SerieX
            {
                get { return _elipsex; }
                set { _elipsex =  ((base.Width - (base.Width/10))+5) + value ; }
            }



            public int SerieY
            {
                get { return _elipsey; }
                set {_elipsey=value; }
                 


            }


            public int SerieHeight
            {
                get { return _elipseheight ; }
                set { _elipseheight = value; }
            }




            public int DepartamentoX
            {
                get { return _poligonoX; }
                set { _poligonoX = value; }
            }


            public int CaptionX
            {
                get { return _poligono1x; }
                //set { _poligono1x=value+ ((base.Width/2)+1); }
                set { _poligono1x = value + ((((base.Width * 70) / 100)) + 1); }
            }


            public int DepartamentoHeight
            {
                get { return _poligono_height; }
                set { _poligono_height = value; }
            }

            public int DepartamentoWidth
            {
                get { return _poligono_width; }
                set { _poligono_width = value; }
            }


            public int CaptionHeight
            {
                get { return _poligono1_height; }
                set { _poligono1_height = value; }
            }


            public int CaptionWidth
            {
                get { return _poligono1_width; }
                set { _poligono1_width = value; }
            }


            public string CaptionDepartamento
            {
                get { return _captiondeparatamento; }
                set { _captiondeparatamento = value;}
            }

            public string Caption
            {
                get { return _caption; }
                set { _caption = value; }
            }


            public string CaptionSerie
            {
                get { return _captionserie; }
                set { _captionserie = value; }
            }


            public int FontSizeDepartamento
            {
                get { return _fontsizedepartamento; }
                set { _fontsizedepartamento = value; }
            }

            public int FontSizeSerie
            {
                get { return _fontsizeserie; }
                set { _fontsizeserie = value; }
            }



            public Color Textcolor
            {
                get { return _textcolor; }
                set { _textcolor = value; }
            }



            public Color SerieBackcolor
            {
                get { return _seriebackcolor; }
                set { _seriebackcolor = value; }
            }


            

            public Color DepartamentoBackColor
            {
                get { return _departamentobackcolor; }
                set { _departamentobackcolor = value; }
            }

            public Color CaptionBackcolor
            {
                get { return _Captionbackcolor; }
                set { _Captionbackcolor = value; }
            }


            public int FontHeight
            {
                get;
                set;
            }


            private void _CaptionDepartamento(PaintEventArgs e)
            {

               
                Font drawFont = new Font("Arial", FontSizeDepartamento);
                SolidBrush drawBrush = new SolidBrush(Textcolor );
                // Create point for upper-left corner of drawing.

                DepartamentoX = 0;

                PointF drawPoint = new PointF(DepartamentoX+30 ,(DepartamentoHeight/2)-10 );
                // Draw string to screen.
                e.Graphics.DrawString(CaptionDepartamento, drawFont, drawBrush, drawPoint);

            }


            private void _Caption(PaintEventArgs e)
            {
                Font drawFont = new Font("Arial", 12);

                SolidBrush drawBrush = new SolidBrush(Color.Black   );



                var points = drawFont.SizeInPoints;

                var pixelswidth = points * e.Graphics.DpiX / 72;
                var pixelsyheight = points * e.Graphics.DpiY / 72;

                this.FontHeight = (int)pixelsyheight;



                PointF drawPoint = new PointF(SerieX -(Caption.Length * pixelswidth), (CaptionHeight / 2) - 10);




                // Draw string to screen.
                e.Graphics.DrawString(Caption, drawFont, drawBrush, drawPoint);

            }

            private void _CaptionSerie(PaintEventArgs e)
            {



                Font drawFont = new Font("Arial", FontSizeSerie);
                SolidBrush drawBrush = new SolidBrush(Textcolor );
                // Create point for upper-left corner of drawing.

                var points = drawFont.SizeInPoints;

                var pixelswidth = points * e.Graphics.DpiX / 72;
                var pixelsyheight = points * e.Graphics.DpiY / 72;

                this.FontHeight = (int)pixelsyheight;

                PointF drawPoint = new PointF((SerieX + (SerieHeight / 2)) - (pixelswidth/2), ((SerieY) + (SerieHeight / 2)) - (this.FontHeight / 2));
                
                // Draw string to screen.
                e.Graphics.DrawString(CaptionSerie, drawFont, drawBrush, drawPoint);

               

            }






            // Draw the new button. 
            protected override void OnPaint(PaintEventArgs e)
            {

                Brush brush;

                base.OnPaint(e); 
                
                //Matrix m = new Matrix();
                //m.Scale(1, 1);
 
                Graphics graphics = e.Graphics;
                Pen myPen = new Pen(Color.Red);
                
               // graphics.Transform = m;
               
                brush = new   SolidBrush (DepartamentoBackColor );
                graphics.FillPolygon( brush  , Poligono().ToArray()); 
                //graphics.DrawPolygon(myPen, Poligono().ToArray());
                _CaptionDepartamento(e);
 
                brush = new SolidBrush(CaptionBackcolor );
                graphics.FillPolygon(brush  , Poligono1().ToArray());

                //graphics.DrawPolygon(myPen, Poligono1().ToArray());
                //_Caption(e);

                brush = new SolidBrush(SerieBackcolor );

                SerieHeight= base.Height ;

             

                SerieX = 0;

                int elipsewidth=0;
                elipsewidth = (base.Width - SerieX);
               

                if (elipsewidth < SerieHeight)
                {
                    
                    SerieX = -(SerieHeight - elipsewidth);
                }


                graphics.FillEllipse(brush, SerieX, 0, (SerieHeight), SerieHeight);

               // _Caption(e);


                _CaptionSerie(e);

                //graphics.DrawEllipse(myPen, (base.Width - 45), 2, 40, base.Height-5);  

                brush.Dispose(); 
                myPen.Dispose();

            }




          


            List<Point> Poligono1()
            {
                List<Point> lstp = new List<Point>();

               

                lstp.Add(new Point((((base.Width *80)/100) ), 0));
                lstp.Add(new Point((((base.Width * 80) / 100)), (base.Height / 4)));

                lstp.Add(new Point((((base.Width * 80) / 100)) + (base.Height / 4), (base.Height / 2)));
                lstp.Add(new Point((((base.Width * 80) / 100)), base.Height - (base.Height / 4)));
                lstp.Add(new Point((((base.Width * 80) / 100)), base.Height - 1));

                lstp.Add(new Point((base.Width-1), base.Height - 1));
                lstp.Add(new Point((base.Width - 1), 0));

                //CaptionWidth = (base.Width - 1) - (((base.Width * 70) / 100));
                CaptionWidth =  (((base.Width * 80) / 100));
                CaptionHeight= base.Height - 1;

                return lstp;
            }


            List<Point> Poligono()
            {
                List<Point> lstp = new List<Point>();
                
                lstp.Add(new Point(0, 0));
                lstp.Add(new Point((((base.Width * 80) / 100)), 0));
                lstp.Add(new Point((((base.Width * 80) / 100)), (base.Height / 4)));

                lstp.Add(new Point((((base.Width * 80) / 100)) + (base.Height / 4), (base.Height / 2)));
                lstp.Add(new Point((((base.Width * 80) / 100)), base.Height - (base.Height / 4)));
                lstp.Add(new Point((((base.Width * 80) / 100)), base.Height - 1));
                lstp.Add(new Point(0, base.Height-1));


                DepartamentoWidth = (base.Width * 80);
                DepartamentoHeight = base.Height - 1; 

                return lstp;
            }



            protected override void OnClick(EventArgs e)
            {
                base.OnClick(e);

            }

        }

    }  
}
