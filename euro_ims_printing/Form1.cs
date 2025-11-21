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

            sincronizar_items();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            //llena combobox con impresoras instaladas
            
            cmbImpresora.Items.Clear();
            PrintDocument prtdoc = new PrintDocument();
            string defaultPrinterName = prtdoc.PrinterSettings.PrinterName;

            foreach (string printerName in PrinterSettings.InstalledPrinters)
            {
                cmbImpresora.Items.Add(printerName);
                
                if (printerName == defaultPrinterName)
                {
                    cmbImpresora.SelectedItem = printerName;
                }
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
                    //guardar

                    this.WindowState = FormWindowState.Minimized;
                    this.Visible = false;
                    notifyIcon1.Visible = true;
                    notifyIcon1.ShowBalloonTip(1000);

                }
                else {
                    //guardar

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

        private void comboBox1_TextChanged(object sender, EventArgs e)
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
            dtgv_items.DataSource = con.select();
            con.desconectar();
        }

    }
}
