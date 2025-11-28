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
        bool ampliado;
        public Form1()
        {
            InitializeComponent();

            ////posicion de la app, arriba del reloj
            //this.StartPosition = FormStartPosition.Manual;
            //int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            //int screenHeight = Screen.PrimaryScreen.Bounds.Height;
            //int formWidth = this.Width;
            //int formHeight = this.Height;
            //this.Size = new System.Drawing.Size(323, 657); 
            //this.Location = new Point(screenWidth - formWidth, screenHeight - formHeight);
           
        }

        private  void Form1_Load(object sender, EventArgs e)
        {
            
            //carga la config
            leer_config();
            //ajustar pantalla
            ajustar_pantalla(ampliado);
            //llena combobox con impresoras instaladas
            listar_impresoras();
            //llena combobox con formatos PRN
            listar_prn();
            //mostrar los item que no han sido impresos y pertenecen a esa "maquina"
            pgbar.Visible = true;
            sincronizar_items();
            pgbar.Visible = false;

            timer1.Interval = 5000;
            timer1.Tick += new EventHandler(this.t_sincronizar_items);
            timer1.Enabled = true;

            


        }
        
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbFormato.Text) || string.IsNullOrEmpty(tbMaquina.Text) || string.IsNullOrEmpty(cmbImpresora.Text))
            {
                MessageBox.Show("Campos obligatorios!", "Sabueso Modulo IMS");
            }
            else {
                DialogResult dgr = MessageBox.Show("Deseas minimizar?", "Sabueso Modulo IMS",MessageBoxButtons.YesNo);
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
        
        private void btnImprimir_Click(object sender, EventArgs e)
        {

            imprimir_items_manual();

        }
        
        public void t_sincronizar_items(object sender, EventArgs e) {
            sincronizar_items();
        }

        private async void sincronizar_items() {
            try { 
                conexionSQL con = new conexionSQL();
                con.conectar();
                
                dtgv_items.DataSource = await Task.Run(() => con.select(tbMaquina.Text));
                dtgv_items.Columns[1].Visible = false; //oculta la columna del consecutivo o id
                con.desconectar();
                

            if (chkAutoImp.Checked) {
                imprimir_items_auto();
            }
            }
            catch (Exception e) {
                if (timer1.Enabled) { timer1.Enabled = false; }
                MessageBox.Show("Error en [sincronizar_items]: "+e.Message);
                
                
            }
        }

        private void guardar_config() {

            Properties.Settings.Default.maquina = tbMaquina.Text;
            Properties.Settings.Default.impresora = cmbImpresora.Text;
            Properties.Settings.Default.auto = chkAutoImp.Checked;
            Properties.Settings.Default.formato = cmbFormato.Text;
            Properties.Settings.Default.ampliado = ampliado;
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Reload();
        }

        private void leer_config() {
            try { 
            tbMaquina.Text = Properties.Settings.Default.maquina;
            cmbImpresora.Text = Properties.Settings.Default.impresora;
            chkAutoImp.Checked = Properties.Settings.Default.auto;
            cmbFormato.Text = Properties.Settings.Default.formato;
            ampliado = Properties.Settings.Default.ampliado;
            }
            catch (Exception e) {
                MessageBox.Show("Error en [leer_config]: " + e.Message);
            }
        }

        private void listar_impresoras() {
            cmbImpresora.Items.Clear();
            //PrintDocument prtdoc = new PrintDocument();
            //string defaultPrinterName = prtdoc.PrinterSettings.PrinterName;
            try { 
            foreach (string printerName in PrinterSettings.InstalledPrinters)
            {
                cmbImpresora.Items.Add(printerName);

                //if (printerName == defaultPrinterName)
                //{
                //    cmbImpresora.SelectedItem = printerName;
                //}
            }
            }
            catch (Exception e) {
                MessageBox.Show("Error en [listar_impresoras]: " + e.Message);
            }
        }

        private void listar_prn() {
            try {
            var prnFiles = new DirectoryInfo("formatos\\").GetFiles("*.prn");
            cmbFormato.DataSource = prnFiles;
            }
            catch (Exception e) { MessageBox.Show("Error en [listar_prn]: " + e.Message); }
        }

        private void imprimir_items_auto() {


            //if (timer1.Enabled==true) { timer1.Enabled = false; }

            for (int i = 0; i < (dtgv_items.Rows.Count); i++)
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
            //if (timer1.Enabled == false) { timer1.Enabled = true; }
        }

        private void imprimir_items_manual()
        {
            for (int i = 0; i < (dtgv_items.Rows.Count); i++)
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

        private async void actualizar_items(item itm)
        {
            try { 
            conexionSQL con = new conexionSQL();
            con.conectar();
            await con.update(itm.consecutivo);
            con.desconectar();


            sincronizar_items();

            }
            catch (Exception e) { MessageBox.Show("Error en [actualizar_items]: " + e.Message); }
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
            catch (Exception e) { MessageBox.Show("Error en [Archivo] " + e.Message); }

            return archivo;

        }

        private void picbSize_Click(object sender, EventArgs e)
        {
            if (ampliado)
            {
                ajustar_pantalla(false);
            }
            else {
                ajustar_pantalla(true);
            }
            
        }

        private void picbClose_Click(object sender, EventArgs e)
        {
            DialogResult dgr = MessageBox.Show("Deseas cerrar la aplicacion?", "Sabueso Modulo IMS", MessageBoxButtons.YesNo);
            if (dgr == DialogResult.Yes)
            {
                this.Close();
               
            }
            
        }

        private void ajustar_pantalla(bool ampliar) {
            if (ampliar)
            {

                //posicion de la app, grande y central
                this.Hide();
                this.Location = new Point(50, 50);
                this.Size = new System.Drawing.Size(800, 657);
                dtgv_items.Size = new System.Drawing.Size(763, 287);


                tbMaquina.Location = new Point(589, 54);
                cmbImpresora.Location = new Point(589, 98);
                cmbFormato.Location = new Point(589, 143);

                lbMaquina.Location = new Point(502, 62);
                lbImpresora.Location = new Point(492, 106);
                lblFormato.Location = new Point(504, 151);

                picbSize.Location = new Point(701, 5);
                picbClose.Location = new Point(742, 5);

                btnImprimir.Location = new Point(665, 505);
                btnAceptar.Location = new Point(665, 566);

                chkAutoImp.Location = new Point(633, 182);

                picbSize.Image = euro_ims_printing.Properties.Resources.in_launcher;
                this.Show();
                ampliado = true;

                

            }
            else
            {

                //posicion de la app, pquena y arriba del reloj
                this.Hide();
                
                this.StartPosition = FormStartPosition.Manual;
                int screenWidth = Screen.PrimaryScreen.Bounds.Width;
                int screenHeight = Screen.PrimaryScreen.Bounds.Height;
                this.Size = new System.Drawing.Size(323, 657);
                int formWidth = this.Width;
                int formHeight = this.Height;

                this.Location = new Point(screenWidth - formWidth, screenHeight - formHeight);


                dtgv_items.Size = new System.Drawing.Size(286, 287);

                tbMaquina.Location = new Point(121, 54);
                cmbImpresora.Location = new Point(121, 98);
                cmbFormato.Location = new Point(121, 143);

                lbMaquina.Location = new Point(25, 62);
                lbImpresora.Location = new Point(15, 106);
                lblFormato.Location = new Point(27, 151);

                picbSize.Location = new Point(225, 5);
                picbClose.Location = new Point(266, 5);

                btnImprimir.Location = new Point(188, 505);
                btnAceptar.Location = new Point(189, 566);

                chkAutoImp.Location = new Point(156, 182);

                picbSize.Image = euro_ims_printing.Properties.Resources.ic_launcher;

                this.Show();
                ampliado = false;



            }
        }
    }
}
