using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Net;

namespace euro_ims_printing
{
    public partial class activacion : Form
    {

        [DllImport("user32.dll")]
        public static extern long ShowWindow(IntPtr hwnd, uint nCmdShow);

        //Función para pasar a primer plano una ventana y activarla
        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hwnd);
        

        public activacion()
        {
            InitializeComponent();
            IPHostEntry hostInfo = Dns.GetHostEntry("localhost");
            tbID.Text = hostInfo.HostName.ToString();
            tbMAC.Text = getMotherBoardID();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            MD5 stringMd5 = new MD5();

            string cadena = "Et1m4rc4s." + tbID.Text.ToString() + "-" + tbMAC.Text.ToString() + "-" + tbApp.Text.ToString();
            string clave = tbActv.Text.ToString();
            string key = stringMd5.GetMd5Hash(cadena);
            if (key == tbActv.Text)
            {
                Properties.Settings.Default.clave = tbActv.Text;
                Properties.Settings.Default.Save();
                Properties.Settings.Default.Reload();

                Form1 frm = new Form1();
                frm.Show();
                this.Visible = false;
            }
            else
            {
                MessageBox.Show("Error en el Codigo de Activación, Por Favor Verifique e ingrese de nuevo");
                tbActv.Text = "";
            }



        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public String getMotherBoardID()
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

        private void activacion_Load(object sender, EventArgs e)
        {
        }
    }
}
