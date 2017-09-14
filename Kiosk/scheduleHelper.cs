using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Xml;
using System.Xml.Linq;

namespace Kiosk
{
    //criar aki uma propriedade para ser sacada.

    public static class scheduleHelper
    {

        private static int _secsToNextEvent = 0;
        public static int secsToNextEvent
        {
            get { return _secsToNextEvent; }
            set { _secsToNextEvent = value; }
        }


        public static DataSet getDepartmentsDataset(Int32 idlocal, Int32 idkiosk)
        {
            // DataSet ds;

            //string sql = " SELECT `tSenhaDepartamento`.iddepartamento,`tSenhaDepartamento`.nome,`tSenhaDepartamento`.serie,`tSenhaDepartamento`.settings FROM `tSenha_Kiosk_Departamento`,`tSenhaKiosk` ,`tSenhaDepartamento` "
            //+ " WHERE tSenhaKiosk.idlocal=" + Properties.Settings.Default.idlocal + " and `tSenha_Kiosk_Departamento`.idkiosk=tSenhaKiosk.idkiosk and  "
            // + " `tSenhaKiosk`.idkiosk=" + Properties.Settings.Default.idkiosk + " and `tSenhaDepartamento`.iddepartamento=`tSenha_Kiosk_Departamento`.iddepartamento "
            //+ " and `tSenhaDepartamento`.enabled=true order by `tSenha_Kiosk_Departamento`.ordem asc";

            string sql = "select tsenhakiosk.IDKiosk,tsenhakiosk.IDDeptGroup,tschedule.ScheduleEvents, tsenhakiosk.IDSchedule,tschedule.Type,tschedule.InitDate, tschedule.EndDate,tschedule.HasInitDate,tschedule.HasEndDate,tschedule.Ativo from tsenhakiosk LEFT OUTER JOIN tschedule on tsenhakiosk.IDSchedule=tschedule.ScheduleID where tsenhakiosk.IDKiosk=" + idkiosk.ToString() + " AND tsenhakiosk.IDLocal=" + idlocal.ToString() + " ";
            DataSet record = AdoConectPhp.RowsSet(sql);

            if (record.Tables[0].Rows.Count == 0)
            {
                //Retorna o Antigo baseado na Relacao Kiosk Departments
                sql = " SELECT `tSenhaDepartamento`.iddepartamento,`tSenhaDepartamento`.nome,`tSenhaDepartamento`.serie,`tSenhaDepartamento`.settings FROM `tSenha_Kiosk_Departamento`,`tSenhaKiosk` ,`tSenhaDepartamento` "
                + " WHERE tSenhaKiosk.idlocal=" + idlocal.ToString() + " and `tSenha_Kiosk_Departamento`.idkiosk=tSenhaKiosk.idkiosk and  "
                 + " `tSenhaKiosk`.idkiosk=" + idkiosk.ToString() + " and `tSenhaDepartamento`.iddepartamento=`tSenha_Kiosk_Departamento`.iddepartamento "
                + " and `tSenhaDepartamento`.enabled=true order by `tSenha_Kiosk_Departamento`.ordem asc";
                UpdateDisplayDepartments(idkiosk, "0");
                return AdoConectPhp.RowsSet(sql);
            }
            else
            {
                foreach (DataRow dr in record.Tables[0].Rows)
                {

                    if (Int32.Parse(dr["IDSchedule"].ToString()) == 0)
                    {
                        if (Int32.Parse(dr["IDDeptGroup"].ToString()) == 0)
                        {
                            //Retorna o Antigo baseado na Relacao Kiosk Departments
                            sql = " SELECT `tSenhaDepartamento`.iddepartamento,`tSenhaDepartamento`.nome,`tSenhaDepartamento`.serie,`tSenhaDepartamento`.settings FROM `tSenha_Kiosk_Departamento`,`tSenhaKiosk` ,`tSenhaDepartamento` "
                            + " WHERE tSenhaKiosk.idlocal=" + idlocal.ToString() + " and `tSenha_Kiosk_Departamento`.idkiosk=tSenhaKiosk.idkiosk and  "
                             + " `tSenhaKiosk`.idkiosk=" + idkiosk.ToString() + " and `tSenhaDepartamento`.iddepartamento=`tSenha_Kiosk_Departamento`.iddepartamento "
                            + " and `tSenhaDepartamento`.enabled=true order by `tSenha_Kiosk_Departamento`.ordem asc";
                            UpdateDisplayDepartments(idkiosk, "0");
                            return AdoConectPhp.RowsSet(sql);
                        }
                        else
                        {
                            //Retorna o Grupo Default que está na definido na tabela kiosk
                            UpdateDisplayDepartments(idkiosk, dr["IDDeptGroup"].ToString());
                            sql = " CALL `GetDepartmentsNew`(" + dr["IDDeptGroup"].ToString() + ")";
                            return AdoConectPhp.RowsSet(sql);
                        }
                    }
                    else
                    {
                        //Vamos esgalhar a porra do XML
                        string myGroupID = GetObjectFromXMLEvents(dr["ScheduleEvents"].ToString());
                        if (myGroupID == "0") myGroupID = dr["IDDeptGroup"].ToString();
                        if (Int32.Parse(myGroupID) == 0)
                        {
                            UpdateDisplayDepartments(idkiosk, "0");
                            //Retorna o Antigo baseado na Relacao Kiosk Departments
                            sql = " SELECT `tSenhaDepartamento`.iddepartamento,`tSenhaDepartamento`.nome,`tSenhaDepartamento`.serie,`tSenhaDepartamento`.settings FROM `tSenha_Kiosk_Departamento`,`tSenhaKiosk` ,`tSenhaDepartamento` "
                            + " WHERE tSenhaKiosk.idlocal=" + idlocal.ToString() + " and `tSenha_Kiosk_Departamento`.idkiosk=tSenhaKiosk.idkiosk and  "
                             + " `tSenhaKiosk`.idkiosk=" + idkiosk.ToString() + " and `tSenhaDepartamento`.iddepartamento=`tSenha_Kiosk_Departamento`.iddepartamento "
                            + " and `tSenhaDepartamento`.enabled=true order by `tSenha_Kiosk_Departamento`.ordem asc";

                            return AdoConectPhp.RowsSet(sql);
                        }
                        else
                        {
                            UpdateDisplayDepartments(idkiosk, myGroupID.ToString());
                            sql = " CALL `GetDepartmentsNew`(" + myGroupID.ToString() + ")";
                            return AdoConectPhp.RowsSet(sql);
                        }
                    }
                }
            }
            //ConfiguraProxThread(minutos);
            return null;
        }

        private static void UpdateDisplayDepartments(Int32 idkiosk, string kioskgroupid)
        {
            string sql = "CALL `UpdateDisplaysDepartmentState`(" + idkiosk.ToString() + "," + kioskgroupid + ")";
            AdoConectPhp.RowsSet(sql);
        }

        private static string GetObjectFromXMLEvents(string xmldata)
        {
            XDocument xdoc = XDocument.Parse(xmldata);

            foreach (XElement x in xdoc.Root.Elements("event"))
            {
                if (EventIsOnline(x)) return x.Attribute("objid").Value;
            }
            return "0";
        }

        private static bool EventIsOnline(XElement x)
        {
            DateTime initTime = GetTimeWithCurrentDate(x.Attribute("dateinit").Value);
            DateTime endTime = GetTimeWithCurrentDate(x.Attribute("dateend").Value);
            //DateTime myDateNow = DateTime.Now;
            //if (initTime > endTime) {
            //    endTime = endTime.AddDays(1);
            //    //if (DateTime.Now<initTime) myDateNow = myDateNow.AddDays(1);
            //} 



            int secsToNext = 0;
            switch (x.Attribute("type").Value)
            {
                case "daily":

                    if (DateTime.Now <= endTime && DateTime.Now >= initTime)
                    {
                        secsToNextEvent = Convert.ToInt32(Math.Round(endTime.Subtract(DateTime.Now).TotalSeconds) + 5);
                        return true;
                    }
                    else
                    {
                        //Evento n está online
                        //if (initTime > DateTime.Now) secsToNext = Convert.ToInt32(Math.Round(initTime.Subtract(DateTime.Now).TotalSeconds) + 5);
                        //else secsToNext = Convert.ToInt32(Math.Round(initTime.AddDays(1).Subtract(DateTime.Now).TotalSeconds) + 5);
                        secsToNext = FindSecsToNextEvent(initTime);
                        //Só Atualiza os segundos se o valor for inferior ao já encontrado                        
                        if (secsToNextEvent == 0 || secsToNext < secsToNextEvent) secsToNextEvent = secsToNext;
                    }
                    break;
                case "weekly":
                    int weekday = (int)DateTime.Now.DayOfWeek;
                    string[] arrWeek = x.Attribute("dayweek").Value.Split(';');
                    int pos = Array.IndexOf(arrWeek, weekday.ToString());
                    if (pos > -1)
                    {
                        //evento encontrado no dia de hoje
                        if (DateTime.Now <= endTime && DateTime.Now >= initTime)
                        {
                            //minutesToNextEvent = endTime.Subtract(DateTime.Now).Seconds;
                            secsToNextEvent = Convert.ToInt32(Math.Round(endTime.Subtract(DateTime.Now).TotalSeconds) + 5);
                            return true;
                        }
                        else
                        {
                            //evento corre hoje mas n está online
                            //Se o evento for do passado calcula a data deste evento mas para a proxima semana
                            if (DateTime.Now > initTime) initTime = initTime.AddDays(7);
                            secsToNext = FindSecsToNextEvent(initTime);
                            if (secsToNextEvent == 0 || secsToNext < secsToNextEvent) secsToNextEvent = secsToNext;
                        }

                    }
                    else
                    {
                        //Encontrar os segundos q falta p proximo 
                        for (int i = 1; i < 8; i++)
                        {
                            int nextDay = (int)DateTime.Now.AddDays(i).DayOfWeek;
                            if (Array.IndexOf(arrWeek, nextDay.ToString()) > -1)
                            {
                                DateTime myDate = DateTime.Today.AddDays(i).AddHours(initTime.Hour).AddMinutes(initTime.Minute).AddSeconds(initTime.Second);
                                //myDate.AddHours(initTime.Hour).AddMinutes(initTime.Minute).AddSeconds(initTime.Second);

                                secsToNext = FindSecsToNextEvent(myDate);

                                //if (myDate > DateTime.Now) secsToNext = Convert.ToInt32(Math.Round(myDate.Subtract(DateTime.Now).TotalSeconds) + 5);
                                //else secsToNext = Convert.ToInt32(Math.Round(myDate.AddDays(1).Subtract(DateTime.Now).TotalSeconds) + 5);


                                //int secsToNext = Convert.ToInt32(Math.Round(myDate.Subtract(DateTime.Now).TotalSeconds) + 5);
                                //Só Atualiza os segundos se o valor for inferior ao já encontrado
                                //if (secsToNext >= 0)
                                if (secsToNextEvent == 0 || secsToNext < secsToNextEvent) secsToNextEvent = secsToNext;
                                break;
                            }
                        }
                    }
                    break;
                default:
                    break;
            }
            return false;
        }

        private static int FindSecsToNextEvent(DateTime initTime)
        {
            int secs = 0;
            if (initTime > DateTime.Now) secs = Convert.ToInt32(Math.Round(initTime.Subtract(DateTime.Now).TotalSeconds) + 5);
            else secs = Convert.ToInt32(Math.Round(initTime.AddDays(1).Subtract(DateTime.Now).TotalSeconds) + 5);
            return secs;
        }
        private static DateTime GetTimeWithCurrentDate(string xmlDate)
        {
            DateTimeOffset dtoffset = DateTimeOffset.Parse(xmlDate);
            DateTime fromxml = dtoffset.DateTime;
            DateTime myDate = DateTime.Today;
            return myDate.AddHours(fromxml.Hour).AddMinutes(fromxml.Minute).AddSeconds(fromxml.Second);
        }
    }


}

