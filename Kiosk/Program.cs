using System;
using System.Windows.Forms;
using System.Linq;

namespace Kiosk
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {


            if (System.Diagnostics.Process.GetProcessesByName(System.IO.Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetEntryAssembly().Location)).Count() > 1)
            {

               // System.Windows.Forms.MessageBox.Show("Atenção a Aplicação já está a ser Executada Feche todas as Sessões .");

                System.Diagnostics.Process.GetCurrentProcess().Kill();
                return;
            }

            //  if (System.Diagnostics.Process.GetProcessesByName(System.IO.Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetEntryAssembly().Location)).Count() >= 1)
            //    System.Diagnostics.Process.GetCurrentProcess().Kill();


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmkiosk());
           //Application.Run(new Frmconfig());
        }
    }
}
