using System;
using System.Xml;
using CrystalDecisions.CrystalReports.Engine;

using System.Management;


namespace Kiosk
{
    class PrintingSenha
    {

        string _kiosk;
        string _contador;
        string _departamento;
        string _serie;
        string _data;




      public  enum Status 
        {
            Unknown = 0,
            Other = 1,
            NoError = 2,
            LowPaper = 3,
            NoPaper = 4,
            LowToner = 5,
            NoToner = 6,
            DoorOpen = 7,
            Jammed = 8,
            Offline = 9,
            ServiceRequested = 10,
            OutputBinFull = 11,
        }







        public PrintingSenha()
        {
            Kiosk = "";
            Contador = "";
            Departamento = "";
            Serie = "";
            Data = ""; 
        }


        

        public string Kiosk
        {
            get { return _kiosk; }
            set { _kiosk = value; }
        }


        public string Contador
        {
            get { return _contador; }
            set { _contador = value; }
        }


        public string Departamento
        {
            get { return _departamento; }
            set { _departamento = value; }
        }

        public string Serie
        {
            get { return _serie; }
            set { _serie = value; }
        }

        public string Data
        {
            get { return _data; }
            set { _data = value; }
        }


        public Status StatusError
        {
            get{return CheckstatusPrinter(); }
            
        }


        public String Errordescription
        {
            get
            {
                switch (StatusError) {
                    case Status.DoorOpen :
                      return "Tampa Aberta";
                    case Status.Jammed :
                      return "Jammed";
                    case Status.LowPaper:
                      return "Pouco Papel";
                    case Status.NoError :
                      return "Sem Erros";
                    case Status.NoPaper :
                      return "Sem papel";
                    case Status.Offline :
                      return "OffLine";
                    case Status.Other:
                      return "Erros";
                    case Status.Unknown:
                      return "Erro Desconhecido";
                    default:
                        return "Erro Desconhecido";
                }
        }

        }





        static string GetDefaultPrinterName()
        {
            var query = new ObjectQuery("SELECT * FROM Win32_Printer");
            var searcher = new ManagementObjectSearcher(query);

            foreach (ManagementObject mo in searcher.Get())
            {
                if (((bool?)mo["Default"]) ?? false)
                {
                    return mo["Name"] as string;
                   
                }
            }

            return null;
        }



       Status  CheckstatusPrinter()
        {

        //  https://msdn.microsoft.com/en-us/library/windows/desktop/aa394363%28v=vs.85%29.aspx

       // http://www.dreamincode.net/forums/topic/63969-how-to-get-extended-printer-status/

            //string printerName = "YourPrinterName";
            string query = string.Format("SELECT * from Win32_Printer WHERE Name LIKE '%{0}'", GetDefaultPrinterName());
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2",query);
            ManagementObjectCollection coll = searcher.Get();

            Status _status=Status.Unknown ;


           // System.Windows.Forms.MessageBox.Show("" + GetDefaultPrinterName());     

            foreach (ManagementObject printer in coll)
            {
                

                //System.Windows.Forms.MessageBox.Show(printer["PrinterStatus"].ToString());

                //System.Windows.Forms.MessageBox.Show(printer["DetectedErrorState"].ToString());


                _status = (Status)int.Parse(printer["DetectedErrorState"].ToString());



                if ( (printer["Status"].ToString().ToUpper()) == "ERROR")
                    _status = Status.Other  ;
                else
                    _status = Status.NoError  ;

                // _status = (Status ) int.Parse(printer["DetectedErrorState"].ToString());
                //_status = (Status)int.Parse(printer["StatusInfo"].ToString());

                
                /*foreach (PropertyData property in printer.Properties)
                {
                    System.Windows.Forms.MessageBox.Show(""+ property.Name + "  " + property.Value);
                }*/
            }

           // System.Windows.Forms.MessageBox.Show("" + _status );

            return _status;
           

        }




       Status CheckstatusPrinter1()
       {

           //  https://msdn.microsoft.com/en-us/library/windows/desktop/aa394363%28v=vs.85%29.aspx

           //string printerName = "YourPrinterName";
           string query = string.Format("SELECT * from Win32_Printer WHERE Name LIKE '%{0}'", GetDefaultPrinterName());
           ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);
           ManagementObjectCollection coll = searcher.Get();

           Status _status = Status.Unknown;

           foreach (ManagementObject printer in coll)
           {


              // System.Windows.Forms.MessageBox.Show(printer["Status"].ToString());

               _status = (Status)int.Parse(printer["DetectedErrorState"].ToString());
               //_status = (Status)int.Parse(printer["StatusInfo"].ToString());


               /*foreach (PropertyData property in printer.Properties)
               {
                   System.Windows.Forms.MessageBox.Show(""+ property.Name + "  " + property.Value);
               }*/
           }

           return _status;

       }




        private void CreateXmlSenha()
        {

            XmlNode SenhaKiosk=null;
            XmlNode kiosk = null;
            XmlNode contador = null;
            XmlNode departamento = null;
            XmlNode serie = null;
            XmlNode data = null;
            XmlNode table = null;


            XmlDocument doc = new XmlDocument();
            XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            doc.AppendChild(docNode);
            SenhaKiosk= doc.CreateElement("SenhaKiosk");
            doc.AppendChild(SenhaKiosk);

            table = doc.CreateElement("table");
            SenhaKiosk.AppendChild(table);


            kiosk = doc.CreateElement("Kiosk");
            kiosk.AppendChild(doc.CreateTextNode(Kiosk.ToString()));
            table.AppendChild(kiosk);

            contador  = doc.CreateElement("contador");
            contador.AppendChild(doc.CreateTextNode(Contador.ToString()));
            table.AppendChild(contador);

            departamento = doc.CreateElement("departamento");
            departamento.AppendChild(doc.CreateTextNode(Departamento.ToString()));
            table.AppendChild(departamento);

            serie = doc.CreateElement("serie");
            serie.AppendChild(doc.CreateTextNode(Serie.ToString()));
            table.AppendChild(serie);


            data = doc.CreateElement("data");
            data.AppendChild(doc.CreateTextNode(Data.ToString()));
            table.AppendChild(data);

           
            doc.Save(@"C:\kiosksenha\senhakiosk.xml");

           

        }


        void ImprimeReport1()
        {

            ReportDocument _rptdoc;
            Frmcrviewer _frmcrviewer;
            
            _rptdoc = new ReportDocument();
            _rptdoc.Load(@"C:\KioskSenha\senhakiosk.rpt");


            _frmcrviewer = new Frmcrviewer();

            _frmcrviewer.Document = _rptdoc;

            _frmcrviewer.Show(); 

        }






        void ImprimeReport()
        {

            try
            {
                // ISCDReportClientDocument reportClientDocument;
                //DatabaseController crDatabaseController;

                ReportDocument _rptdoc;
               // Frmcrviewer _frmcrviewer;

                _rptdoc = new ReportDocument();

                _rptdoc.Load(@"C:\Kiosksenha\senhakiosk.rpt");

                //_rptdoc.Load("C:\\KioskSenhas\\Kiosk\\Kiosk\\bin\\Debug\\senhakiosk.rpt");
                // reportClientDocument = _rptdoc.ReportClientDocument;
                // crDatabaseController = reportClientDocument.DatabaseController;

                // PropertyBag QELogonProperties = new PropertyBag();
                //QELogonProperties.Add("Local XML File", @"C:\KioskSenha\senhakiosk.xml");

                //PropertyBag QEProperties = new PropertyBag();
                //QEProperties.Add("Database DLL", "crdb_xml.dll");
                //QEProperties.Add("Database DLL", "crdb_adoplus.dll");
                //QEProperties.Add("QE_DatabaseType", "ADO.NET (XML)");
                //QEProperties.Add("QE_LogonProperties", QELogonProperties);

                //Create Connection info and table
                //ConnectionInfo ConnInf = new CrystalDecisions.ReportAppServer.DataDefModel.ConnectionInfo();
                //ConnInf.Attributes = QEProperties;
                //ConnInf.Kind = CrystalDecisions.ReportAppServer.DataDefModel.CrConnectionInfoKindEnum.crConnectionInfoKindCRQE;
                //ConnInf.Password = null;
                //ConnInf.UserName = null;


                //Add Connection Information to a table and add the table to the report
                //CrystalDecisions.ReportAppServer.DataDefModel.Table TBL = new CrystalDecisions.ReportAppServer.DataDefModel.Table();
                //TBL.ConnectionInfo = ConnInf;
                //TBL.Name = "SenhaKiosk";





                //Then set the table to the report
                //_rptdoc.ReportClientDocument.DatabaseController.AddTable(TBL);


                _rptdoc.SetDatabaseLogon("Null", "Null", "SenhaKiosk", "C:\\KioskSenhas\\Kiosk\\Kiosk\\bin\\Debug\\senhakiosk.xml");

                _rptdoc.PrintToPrinter(1, false, 1, 1);

                //_frmcrviewer = new Frmcrviewer();
                //_frmcrviewer.Document = _rptdoc;
                //_frmcrviewer.Show() ;
                //_frmcrviewer.Dispose();


                _rptdoc.Close();
                _rptdoc = null;
            }
            catch (Exception)
            {
               
            }


        }


        public bool Printing()
        {

            CreateXmlSenha();

            ImprimeReport(); 



            return true;
        }





    }
}
