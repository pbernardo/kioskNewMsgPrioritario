using System;
using System.Net.Sockets;
using System.IO;


namespace Kiosk
{
    class CommandCliente
    {

         TcpClient client = new TcpClient();
         public Socket s ;


         StreamWriter sw;
         StreamReader sr;

          string NameServer="";
          string Port = "";
          NetworkStream ntws;
          IAsyncResult  result;
         
          public CommandCliente(string Host, string PortComunicate)
          {
              try
              {
                  NameServer = Host;
                  Port = PortComunicate;
                  //client.SendTimeout = 10;
                  //client.Connect(NameServer, int.Parse(Port ));
                  //s = client.Client;
                 // sw = new StreamWriter(client.GetStream());
                 // sr = new StreamReader(client.GetStream()); 
                  //ntws = client.GetStream();  



                   result = client.BeginConnect(Host, int.Parse(Port), null, null);

                  var success = result.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(1));

                  if (!success)
                  {
                      throw new Exception("Failed to connect.");
                  }
                  else
                  {
                      s = client.Client;
                      sw = new StreamWriter(client.GetStream());
                      sr = new StreamReader(client.GetStream());
                  }

              }
              catch (Exception)
              {
                  client.Close();
                  
               //MessageBox.Show("Falha de Comunicação,Verifique se o servidor está ligado,ou se tem sinal de rede");
                    
              }
          }


          public Boolean Connect()
          {
              try
              {
               
                  return s.Connected ;

              }

              catch (Exception)
              {

                  return false;
              }
          }



          void LigacaoServer()
          {
              try
              {
                  
                  client = new TcpClient();
                  /*s = client.Client;
                  client.Connect(NameServer, int.Parse(Port));
                  s = client.Client;
                  sw = new StreamWriter(client.GetStream());
                  sr = new StreamReader(client.GetStream());
                  //ntws = client.GetStream();  */

                  result = client.BeginConnect(NameServer, int.Parse(Port), null, null);

                  var success = result.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(1));

                  if (!success)
                  {
                      throw new Exception("Failed to connect.");
                  }
                  else
                  {
                      s = client.Client;
                      sw = new StreamWriter(client.GetStream());
                      sr = new StreamReader(client.GetStream());
                  }


              }
              catch (Exception)
              {
                  //MessageBox.Show("Falha de Comunicação,Verifique se o servidor está ligado,ou se tem sinal de rede");
              }
          }



          public void Close()
          {

              if (s.Connected)
              {

                  client.EndConnect(result);

                  //client.Close();
                  //s.Close();
                  
              }

          }



         public string Read_line()
          {
              string valor = "";

              try
              {
                  if (s.Connected)
                  {


                      sr = new StreamReader(client.GetStream());

                     valor= sr.ReadLine(); 


                    /*  byte[] bytes = new byte[client.ReceiveBufferSize];
                      ntws.Read(bytes, 0, (int)client.ReceiveBufferSize);
                      valor = Encoding.UTF8.GetString(bytes);*/

                  }
                  else
                  {
                  //    Cursor.Current = Cursors.Default;
                  //    s.Close();
                  //    sr.Close();
                  //    sw.Close();
                  //    if (!NovaLigacao())
                  //    {
                  //        if ((MessageBox.Show("Quer Fazer nova Tentativa? ", "Atenção", MessageBoxButtons.RetryCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)) == DialogResult.Retry)
                  //            if (NovaLigacao())
                  //                valor = "";
                  //            else
                  //            {
                  //                Cursor.Current = Cursors.Default;
                  //                valor = "";
                  //                client.Close();
                  //                s.Close();
                  //                sr.Close();
                  //                sw.Close();
                  //            }
                  //        else
                  //        {
                  //            valor = "";
                  //            return valor; 
                  //        }
                  //    }
                      valor = "DESCONECT";
                  }
                  return valor;
              }
              catch (Exception )
              {
                  valor = "FAIL";
                  return valor;
              }
          }


          public string  Write_Line(string texto)
          {
              
              try
              {
                  if (s.Connected)
                  {

                      sw.WriteLine(texto);
                      sw.Flush(); 
                       //Byte[] sendBytes = Encoding.UTF8.GetBytes(texto);
                       //ntws.Write(sendBytes, 0, sendBytes.Length);
                       

                  }
                  else
                  {
                    //Cursor.Current = Cursors.Default;
                    /*s.Close();
                    sr.Close();
                    sw.Close();*/

                    s.EndConnect(result);
                    sr.Close();
                    sw.Close();

                    if (NovaLigacao())
                    //    if ((MessageBox.Show("Falha de Comunicação,Quer Fazer nova Tentativa? ", "Atenção", MessageBoxButtons.RetryCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)) == DialogResult.Retry)
                    //        if (NovaLigacao())
                    //        {
                    //            sw.WriteLine(texto);
                    //            sw.Flush();
                    //        }
                    //        else
                    //        {
                    //            Cursor.Current = Cursors.Default;
                    //            client.Close();
                    //            s.Close();
                    //            sr.Close();
                    //            sw.Close();
                    //        }
                    //    //else
                    //    //    Application.Exit();
                    //else
                    //{
                        sw.WriteLine(texto);
                        sw.Flush();
                    //}
                    //  return "DESCONECT";
                  }

                  return "OK";

              }
              catch (Exception)
              {
                  return "FAIL";   
              }

          }



        public  bool NovaLigacao()
          {
              int i = 1;
              bool faz =true;

              while (i <= 1 && faz)
              {
                  LigacaoServer();
                  if (s.Connected) faz = false;

                  i++;
              }

              if (faz)
                  return false;
              else
                  return true;

          }


    }
       
}
