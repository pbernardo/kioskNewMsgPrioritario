using System;
using System.Collections.Generic;
using System.Data;

namespace Kiosk
{
    public class Config
    {


        public Config()
        {
            
        }


        public void Save()
        {
            
            Properties.Settings.Default.Save();
        }


        public string host
        {
            get { return Properties.Settings.Default.host; }
            set { Properties.Settings.Default.host = value; }
        }


        public int idlocal
        {
            get { return Properties.Settings.Default.idlocal; }
            set { Properties.Settings.Default.idlocal = value; }

        }

        public int idkiosk
        {
            get { return Properties.Settings.Default.idkiosk; }
            set { Properties.Settings.Default.idkiosk= value; }

        }

        public string kioskname
        {
            get { return Properties.Settings.Default.kioskname; }
            set { Properties.Settings.Default.kioskname = value; }
        }
        
        public string Localname{
            get { return Properties.Settings.Default.localname; }
            set { Properties.Settings.Default.localname = value; }

        }

        public string PathImagem
        {
            get { return Properties.Settings.Default.pathkiosk; }
            set { Properties.Settings.Default.pathkiosk = value; }
        }


        public int ItemData_ID(Itemdata itemdata)
        {
            return itemdata.Id;
        }

        public string ItemData_Text(Itemdata itemdata)
        {
            return itemdata.Text;
        }



        public List<Itemdata> kioskNames(Boolean conection)
        {
            List<Itemdata> _itens=new List<Itemdata>();
            Itemdata _iten;
            string sql;
            DataSet record;

            if (conection)
            {
                sql = "select idkiosk,nome  from tSenhaKiosk ";
                record = AdoConectPhp.RowsSet(sql);
                foreach (DataRow dr in record.Tables[0].Rows)
                {
                    _iten = new Itemdata();
                    _iten.Id = Int32.Parse(dr["idkiosk"].ToString());
                    _iten.Text = dr["nome"].ToString();
                    _itens.Add(_iten);
                }

            }
            else
            {
                _iten = new Itemdata();
                _iten.Id = idkiosk;
                _iten.Text = kioskname;
                _itens.Add(_iten);
            }


            return _itens;
        }


       


        public List<Itemdata> LocalNames(Boolean conection)
        {
            List<Itemdata> _itens = new List<Itemdata>();

            Itemdata _iten;
            string sql;
            DataSet record;

            if (conection)
            {

                sql = "select idlocal,nome  from tSenhaLocal ";
                record = AdoConectPhp.RowsSet(sql);
                foreach (DataRow dr in record.Tables[0].Rows)
                {
                    _iten = new Itemdata();
                    _iten.Id = Int32.Parse(dr["idlocal"].ToString());
                    _iten.Text = dr["nome"].ToString();

                    _itens.Add(_iten);
                }

            }
            else
            {
                _iten = new Itemdata();
                _iten.Id = idlocal;
                _iten.Text = Localname;
                _itens.Add(_iten);
            }

            return _itens;
        }



      public class Itemdata
        {
            public int Id { get; set; }
            public string Text { get; set; }
        }



    }
}
