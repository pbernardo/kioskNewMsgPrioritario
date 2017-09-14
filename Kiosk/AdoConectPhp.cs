using System;
using System.Data;
using System.Net;
using Newtonsoft.Json;

namespace Kiosk
{
    class AdoConectPhp
    {

        private static DataSet CallProxy(string type, string query)
        {


            try
            {

                System.Net.ServicePointManager.Expect100Continue = false;
                using (WebClient client = new WebClient())
                {
                    byte[] response =
                    client.UploadValues("http://" + Properties.Settings.Default.host  + "/proxy.php", new System.Collections.Specialized.NameValueCollection()
                    {
                        { "type", type },
                        { "query", query }
                    });
                    string htmlresponse = System.Text.Encoding.UTF8.GetString(response);
                    try
                    {
                        return JsonConvert.DeserializeObject<DataSet>(htmlresponse);
                    }

                    catch (Exception)
                    {
                        return JsonConvert.DeserializeObject<DataSet>("{\"dataset\":[{\"success\":0,\"message\":\"Ocorreu um erro !\"}]}");
                    }

                }
            }
            catch (Exception) {
                return JsonConvert.DeserializeObject<DataSet>("{\"dataset\":[]}");
            }
        }


        public static DataSet Execute(string query)
        {
           return  CallProxy("POST", query);
        }

        public static DataSet RowsSet(string query)
        {
            return CallProxy("GET", query);
        }	 



    }



}
