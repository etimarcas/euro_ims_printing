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
using System.IO;

namespace euro_ims_printing
{
    public partial class Form1 : Form
    {
        
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
            //llena combobox con impresoras instaladas
            listar_impresoras();
            //llena combobox con formatos PRN
            listar_prn();
            //mostrar los item que no han sido impresos y pertenecen a esa "maquina"
            sincronizar_items();


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
            
        }

        private void cmbImpresora_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void cmbFormato_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
                imprimir_items();

        }


        private async void sincronizar_items() {
            conexionSQL con = new conexionSQL();
            con.conectar();
            dtgv_items.DataSource = await con.select(tbMaquina.Text);
            con.desconectar();
        }

        private void guardar_config() {

            Properties.Settings.Default.maquina = tbMaquina.Text;
            Properties.Settings.Default.impresora = cmbImpresora.Text;
            Properties.Settings.Default.auto = chkAutoImp.Checked;
            Properties.Settings.Default.formato = cmbFormato.Text;
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Reload();
        }

        private void leer_config() {
            tbMaquina.Text = Properties.Settings.Default.maquina;
            cmbImpresora.Text = Properties.Settings.Default.impresora;
            chkAutoImp.Checked = Properties.Settings.Default.auto;
            cmbFormato.Text = Properties.Settings.Default.formato;
        }

        private void listar_impresoras() {
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

        private void listar_prn() {
            var prnFiles = new DirectoryInfo("formatos\\").GetFiles("*.prn");
            cmbFormato.DataSource = prnFiles;
        }

        private void imprimir_items()
        {

            if (chkAutoImp.Checked)
            {

                for (int i = 0; i < (dtgv_items.Rows.Count - 1); i++)
                {
                    item itm = new item();


                    itm.consecutivo = dtgv_items.Rows[i].Cells[1].Value.ToString();
                    itm.CodArticulo = dtgv_items.Rows[i].Cells[2].Value.ToString();
                    itm.descripcion = dtgv_items.Rows[i].Cells[3].Value.ToString();
                    itm.precio = dtgv_items.Rows[i].Cells[4].Value.ToString();
                    itm.barra = dtgv_items.Rows[i].Cells[5].Value.ToString();
                    itm.pum = dtgv_items.Rows[i].Cells[6].Value.ToString();
                    itm.Fecha = dtgv_items.Rows[i].Cells[7].Value.ToString();
                    itm.Impreso = dtgv_items.Rows[i].Cells[8].Value.ToString();
                    itm.num_impresiones = dtgv_items.Rows[i].Cells[9].Value.ToString();
                    itm.NombreMaquina = dtgv_items.Rows[i].Cells[10].Value.ToString();

                    RawPrinterHelper.SendStringToPrinter(cmbImpresora.Text, archivo(itm));

                    actualizar_items(itm);

                }

            }
            else {

                for (int i = 0; i < (dtgv_items.Rows.Count - 1); i++)
                {
                    if (Convert.ToBoolean(dtgv_items.Rows[i].Cells["chkPrint"].Value)) {

                        item itm = new item();

                        itm.consecutivo = dtgv_items.Rows[i].Cells[1].Value.ToString();
                        itm.CodArticulo = dtgv_items.Rows[i].Cells[2].Value.ToString();
                        itm.descripcion = dtgv_items.Rows[i].Cells[3].Value.ToString();
                        itm.precio = dtgv_items.Rows[i].Cells[4].Value.ToString();
                        itm.barra = dtgv_items.Rows[i].Cells[5].Value.ToString();
                        itm.pum = dtgv_items.Rows[i].Cells[6].Value.ToString();
                        itm.Fecha = dtgv_items.Rows[i].Cells[7].Value.ToString();
                        itm.Impreso = dtgv_items.Rows[i].Cells[8].Value.ToString();
                        itm.num_impresiones = dtgv_items.Rows[i].Cells[9].Value.ToString();
                        itm.NombreMaquina = dtgv_items.Rows[i].Cells[10].Value.ToString();

                        RawPrinterHelper.SendStringToPrinter(cmbImpresora.Text, archivo(itm));

                        actualizar_items(itm);

                    }
                        
                }
            }

    
        }

        private async void actualizar_items(item itm)
        {
            conexionSQL con = new conexionSQL();
            con.conectar();
            await con.update(itm.consecutivo);
            con.desconectar();


            sincronizar_items();
        }

        private string archivo(item itm) {
            

            string archivo = File.ReadAllText("formatos\\" + cmbFormato.Text);

            try
            {

                
                archivo = archivo.Replace("$CODIGO$", itm.barra);
                archivo = archivo.Replace("$ITEM$", itm.CodArticulo);

                if (itm.descripcion.Length > 25)
                {
                    archivo = archivo.Replace("$DESCRIPCION1$", itm.descripcion.Substring(0, 25));
                    archivo = archivo.Replace("$DESCRIPCION2$", itm.descripcion.Substring(25));
                }
                else
                {
                    archivo = archivo.Replace("$DESCRIPCION1$", itm.descripcion);
                    archivo = archivo.Replace("$DESCRIPCION2$", "");
                }

                archivo = archivo.Replace("$PRECIO$", itm.precio);
                archivo = archivo.Replace("$CANTIDAD$", itm.num_impresiones);
                archivo = archivo.Replace("$PPUM$", itm.pum);                
                archivo = archivo.Replace("$FECHA$", itm.Fecha);
            }
            catch (Exception e) { MessageBox.Show("Error en [Archivo] " + e); }

            return archivo;

        }
    }
}
