namespace euro_ims_printing
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.dtgv_items = new System.Windows.Forms.DataGridView();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.chkAutoImp = new System.Windows.Forms.CheckBox();
            this.lbImpresora = new System.Windows.Forms.Label();
            this.cmbImpresora = new System.Windows.Forms.ComboBox();
            this.lbMaquina = new System.Windows.Forms.Label();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.cmbFormato = new System.Windows.Forms.ComboBox();
            this.tbMaquina = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dtgv_items)).BeginInit();
            this.SuspendLayout();
            // 
            // dtgv_items
            // 
            this.dtgv_items.AllowUserToAddRows = false;
            this.dtgv_items.AllowUserToDeleteRows = false;
            this.dtgv_items.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dtgv_items.Location = new System.Drawing.Point(14, 208);
            this.dtgv_items.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dtgv_items.Name = "dtgv_items";
            this.dtgv_items.ReadOnly = true;
            this.dtgv_items.Size = new System.Drawing.Size(286, 287);
            this.dtgv_items.TabIndex = 0;
            // 
            // btnAceptar
            // 
            this.btnAceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAceptar.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAceptar.Location = new System.Drawing.Point(189, 566);
            this.btnAceptar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(112, 37);
            this.btnAceptar.TabIndex = 1;
            this.btnAceptar.Text = "Guardar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // btnImprimir
            // 
            this.btnImprimir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImprimir.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImprimir.Location = new System.Drawing.Point(188, 505);
            this.btnImprimir.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(112, 37);
            this.btnImprimir.TabIndex = 2;
            this.btnImprimir.Text = "Imprimir";
            this.btnImprimir.UseVisualStyleBackColor = true;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // chkAutoImp
            // 
            this.chkAutoImp.AutoSize = true;
            this.chkAutoImp.Checked = true;
            this.chkAutoImp.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoImp.Location = new System.Drawing.Point(156, 182);
            this.chkAutoImp.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkAutoImp.Name = "chkAutoImp";
            this.chkAutoImp.Size = new System.Drawing.Size(153, 25);
            this.chkAutoImp.TabIndex = 3;
            this.chkAutoImp.Text = "Impresion -Auto.";
            this.chkAutoImp.UseVisualStyleBackColor = true;
            // 
            // lbImpresora
            // 
            this.lbImpresora.AutoSize = true;
            this.lbImpresora.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbImpresora.Location = new System.Drawing.Point(15, 106);
            this.lbImpresora.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbImpresora.Name = "lbImpresora";
            this.lbImpresora.Size = new System.Drawing.Size(89, 21);
            this.lbImpresora.TabIndex = 4;
            this.lbImpresora.Text = "Impresora:";
            // 
            // cmbImpresora
            // 
            this.cmbImpresora.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbImpresora.FormattingEnabled = true;
            this.cmbImpresora.Location = new System.Drawing.Point(121, 98);
            this.cmbImpresora.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbImpresora.Name = "cmbImpresora";
            this.cmbImpresora.Size = new System.Drawing.Size(180, 28);
            this.cmbImpresora.TabIndex = 5;
            this.cmbImpresora.TextChanged += new System.EventHandler(this.cmbImpresora_TextChanged);
            // 
            // lbMaquina
            // 
            this.lbMaquina.AutoSize = true;
            this.lbMaquina.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMaquina.Location = new System.Drawing.Point(25, 62);
            this.lbMaquina.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbMaquina.Name = "lbMaquina";
            this.lbMaquina.Size = new System.Drawing.Size(77, 21);
            this.lbMaquina.TabIndex = 6;
            this.lbMaquina.Text = "Maquina:";
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon1.BalloonTipText = "Sabueso IMS Print se esta ejecutando en segundo plano";
            this.notifyIcon1.BalloonTipTitle = "Sabueso IMS";
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "Sabueso IMS-Print";
            this.notifyIcon1.Click += new System.EventHandler(this.notifyIcon1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(27, 151);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 21);
            this.label1.TabIndex = 7;
            this.label1.Text = "Formato:";
            // 
            // cmbFormato
            // 
            this.cmbFormato.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbFormato.FormattingEnabled = true;
            this.cmbFormato.Location = new System.Drawing.Point(121, 143);
            this.cmbFormato.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbFormato.Name = "cmbFormato";
            this.cmbFormato.Size = new System.Drawing.Size(180, 28);
            this.cmbFormato.TabIndex = 8;
            this.cmbFormato.TextChanged += new System.EventHandler(this.comboBox1_TextChanged);
            // 
            // tbMaquina
            // 
            this.tbMaquina.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbMaquina.Location = new System.Drawing.Point(121, 54);
            this.tbMaquina.Name = "tbMaquina";
            this.tbMaquina.Size = new System.Drawing.Size(179, 27);
            this.tbMaquina.TabIndex = 9;
            this.tbMaquina.TextChanged += new System.EventHandler(this.tbMaquina_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(15, 582);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(157, 21);
            this.label2.TabIndex = 14;
            this.label2.Text = "Etimarcas SAS 2025";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Gainsboro;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(200, 40);
            this.label3.TabIndex = 15;
            this.label3.Text = "Sabueso IMS - Print";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(323, 657);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbMaquina);
            this.Controls.Add(this.cmbFormato);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbMaquina);
            this.Controls.Add(this.cmbImpresora);
            this.Controls.Add(this.lbImpresora);
            this.Controls.Add(this.chkAutoImp);
            this.Controls.Add(this.btnImprimir);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.dtgv_items);
            this.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgv_items)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dtgv_items;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.CheckBox chkAutoImp;
        private System.Windows.Forms.Label lbImpresora;
        private System.Windows.Forms.ComboBox cmbImpresora;
        private System.Windows.Forms.Label lbMaquina;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbFormato;
        private System.Windows.Forms.TextBox tbMaquina;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Timer timer1;
    }
}

