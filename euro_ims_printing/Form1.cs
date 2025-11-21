using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Printing;

namespace euro_ims_printing
{
    public partial class Form1 : Form
    {
        int bandera = 0;
        public Form1()
        {
            InitializeComponent();

            //posicion de la app, arriba del reloj
            this.StartPosition = FormStartPosition.Manual;
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;
            int formWidth = this.Width;
            int formHeight = this.Height;            
            this.Location = new Point(screenWidth - formWidth, screenHeight - formHeight);
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            //leer archivo config
            leer_config();
            //mostrar los item que no han sido impresos y pertenecen a esa "maquina"
            sincronizar_items();
            //llena combobox con impresoras instaladas

            cmbImpresora.Items.Clear();
            //PrintDocument prtdoc = new PrintDocument();
            //string defaultPrinterName = prtdoc.PrinterSettings.PrinterName;

            foreach (string printerName in PrinterSettings.InstalledPrinters)
            {
                cmbImpresora.Items.Add(printerName);
                
                //if (printerName == defaultPrinterName)
                //{
                //    cmbImpresora.SelectedItem = printerName;
                //}
            }

        }



        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbFormato.Text) || string.IsNullOrEmpty(tbMaquina.Text) || string.IsNullOrEmpty(cmbImpresora.Text))
            {
                MessageBox.Show("Campos obligatorios!", "Sabueso IMS - Print");
            }
            else {
                DialogResult dgr = MessageBox.Show("Deseas minimizar?", "Sabueso IMS - Print",MessageBoxButtons.YesNo);
                if (dgr == DialogResult.Yes)
                {
                    guardar_config();

                    this.WindowState = FormWindowState.Minimized;
                    this.Visible = false;
                    notifyIcon1.Visible = true;
                    notifyIcon1.ShowBalloonTip(1000);

                }
                else {
                    guardar_config();

                }
                bandera = 0;
            }



        }

        private void notifyIcon1_Click(object sender, EventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;
        }

        private void tbMaquina_TextChanged(object sender, EventArgs e)
        {
            bandera++;
        }

        private void cmbImpresora_TextChanged(object sender, EventArgs e)
        {
            bandera++;
        }

        private void cmbFormato_TextChanged(object sender, EventArgs e)
        {
            bandera++;
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (bandera > 0) {
                MessageBox.Show("Debe guardar los cambios antes de continuar.");
            }
        }


        public void sincronizar_items() {
            conexionSQL con = new conexionSQL();
            con.conectar();
            dtgv_items.DataSource = con.select(tbMaquina.Text);
            con.desconectar();
        }

        public void guardar_config() {

            Properties.Settings.Default.maquina = tbMaquina.Text;
            Properties.Settings.Default.impresora = cmbImpresora.Text;
            Properties.Settings.Default.auto = chkAutoImp.Checked;
            Properties.Settings.Default.formato = cmbFormato.Text;
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Reload();
        }

        public void leer_config() {
            tbMaquina.Text = Properties.Settings.Default.maquina;
            cmbImpresora.Text = Properties.Settings.Default.impresora;
            chkAutoImp.Checked = Properties.Settings.Default.auto;
            cmbFormato.Text = Properties.Settings.Default.formato;
        }

        
    }
}
