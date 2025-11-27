using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.Net;
using System.Threading;
using System.Diagnostics;

namespace euro_ims_printing
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());

            //------

            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            
           
                IPHostEntry hostInfo = Dns.GetHostEntry("localhost");
                string Id = hostInfo.HostName.ToString();
                String Mac = getMotherBoardID();

                MD5 stringMd5 = new MD5();
                string cadena = "Et1m4rc4s." + Id + "-" + Mac + "-IMS";

                string key = stringMd5.GetMd5Hash(cadena);
                               

                String clave = Properties.Settings.Default.clave; ;


                if (key == clave)
                {

                ////// La aplicación ya se está ejecutando, cierra esta instancia
                bool nuevaInstancia;
                using (Mutex mutex = new Mutex(true, Process.GetCurrentProcess().ProcessName, out nuevaInstancia))
                {
                    if (!nuevaInstancia)
                    {
                        
                        return;
                    }

                    Application.Run(new Form1());

                }

            }
                else
                {

                    Application.Run(new activacion());

                }

        
            //------
        }


        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = (Exception)e.ExceptionObject;
            MessageBox.Show("Error: " + ex.InnerException.ToString());
        }

        public static String getMotherBoardID()
        {

            string serial = "";

            IPGlobalProperties computerProperties = IPGlobalProperties.GetIPGlobalProperties();
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            Console.WriteLine("Interface information for {0}.{1}     ",
                    computerProperties.HostName, computerProperties.DomainName);
            foreach (NetworkInterface adapter in nics)
            {
                IPInterfaceProperties properties = adapter.GetIPProperties();

                if (adapter.NetworkInterfaceType.ToString().Equals("Ethernet"))
                {
                    serial = adapter.GetPhysicalAddress().ToString();
                    break;
                }

            }

            return serial;

        }





    }
}
