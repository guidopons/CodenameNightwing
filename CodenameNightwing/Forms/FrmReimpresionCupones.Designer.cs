namespace CodenameNightwing.Forms
{
    partial class FrmReimpresionCupones
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.grpFiltros = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtVtolId = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblSucursal = new System.Windows.Forms.Label();
            this.txtCaja = new System.Windows.Forms.TextBox();
            this.txtSucursal = new System.Windows.Forms.TextBox();
            this.pnlMensaje = new System.Windows.Forms.Panel();
            this.lblMensaje = new System.Windows.Forms.Label();
            this.grpAcciones = new System.Windows.Forms.GroupBox();
            this.btnVolver = new System.Windows.Forms.Button();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.txtMarca = new System.Windows.Forms.TextBox();
            this.lblDescripcionTarjeta = new System.Windows.Forms.Label();
            this.txtUltimos4Numeros = new System.Windows.Forms.MaskedTextBox();
            this.lblUltimos4Numeros = new System.Windows.Forms.Label();
            this.calFecha = new System.Windows.Forms.MonthCalendar();
            this.lblFecha = new System.Windows.Forms.Label();
            this.grpGrilla = new System.Windows.Forms.GroupBox();
            this.pnlImprimiendo = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.dgwTransacciones = new System.Windows.Forms.DataGridView();
            this.tipoOperacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.numerosTarjeta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.marcaTarjeta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.importe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sucursal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.caja = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nroCargo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.codAuth = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.modo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idVtol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Anulado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clReversado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grpFiltros.SuspendLayout();
            this.pnlMensaje.SuspendLayout();
            this.grpAcciones.SuspendLayout();
            this.grpGrilla.SuspendLayout();
            this.pnlImprimiendo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgwTransacciones)).BeginInit();
            this.SuspendLayout();
            // 
            // grpFiltros
            // 
            this.grpFiltros.Controls.Add(this.label3);
            this.grpFiltros.Controls.Add(this.txtVtolId);
            this.grpFiltros.Controls.Add(this.label1);
            this.grpFiltros.Controls.Add(this.label2);
            this.grpFiltros.Controls.Add(this.lblSucursal);
            this.grpFiltros.Controls.Add(this.txtCaja);
            this.grpFiltros.Controls.Add(this.txtSucursal);
            this.grpFiltros.Controls.Add(this.pnlMensaje);
            this.grpFiltros.Controls.Add(this.grpAcciones);
            this.grpFiltros.Controls.Add(this.txtMarca);
            this.grpFiltros.Controls.Add(this.lblDescripcionTarjeta);
            this.grpFiltros.Controls.Add(this.txtUltimos4Numeros);
            this.grpFiltros.Controls.Add(this.lblUltimos4Numeros);
            this.grpFiltros.Controls.Add(this.calFecha);
            this.grpFiltros.Controls.Add(this.lblFecha);
            this.grpFiltros.Location = new System.Drawing.Point(12, 12);
            this.grpFiltros.Name = "grpFiltros";
            this.grpFiltros.Size = new System.Drawing.Size(929, 218);
            this.grpFiltros.TabIndex = 0;
            this.grpFiltros.TabStop = false;
            this.grpFiltros.Text = "Filtros de busqueda";
            this.grpFiltros.Enter += new System.EventHandler(this.grpFiltros_Enter);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(441, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "VTOL ID:";
            // 
            // txtVtolId
            // 
            this.txtVtolId.Location = new System.Drawing.Point(444, 91);
            this.txtVtolId.Name = "txtVtolId";
            this.txtVtolId.Size = new System.Drawing.Size(48, 20);
            this.txtVtolId.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(414, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(15, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Ó";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(355, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Caja:";
            // 
            // lblSucursal
            // 
            this.lblSucursal.AutoSize = true;
            this.lblSucursal.Location = new System.Drawing.Point(298, 31);
            this.lblSucursal.Name = "lblSucursal";
            this.lblSucursal.Size = new System.Drawing.Size(51, 13);
            this.lblSucursal.TabIndex = 9;
            this.lblSucursal.Text = "Sucursal:";
            // 
            // txtCaja
            // 
            this.txtCaja.Location = new System.Drawing.Point(355, 47);
            this.txtCaja.Name = "txtCaja";
            this.txtCaja.Size = new System.Drawing.Size(48, 20);
            this.txtCaja.TabIndex = 8;
            // 
            // txtSucursal
            // 
            this.txtSucursal.Location = new System.Drawing.Point(301, 47);
            this.txtSucursal.Name = "txtSucursal";
            this.txtSucursal.Size = new System.Drawing.Size(48, 20);
            this.txtSucursal.TabIndex = 7;
            // 
            // pnlMensaje
            // 
            this.pnlMensaje.BackColor = System.Drawing.Color.Aqua;
            this.pnlMensaje.Controls.Add(this.lblMensaje);
            this.pnlMensaje.Location = new System.Drawing.Point(0, 219);
            this.pnlMensaje.Name = "pnlMensaje";
            this.pnlMensaje.Size = new System.Drawing.Size(839, 75);
            this.pnlMensaje.TabIndex = 5;
            this.pnlMensaje.Visible = false;
            // 
            // lblMensaje
            // 
            this.lblMensaje.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMensaje.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMensaje.Location = new System.Drawing.Point(91, 20);
            this.lblMensaje.Name = "lblMensaje";
            this.lblMensaje.Size = new System.Drawing.Size(646, 40);
            this.lblMensaje.TabIndex = 3;
            this.lblMensaje.Text = "Imprimiendo Cupones";
            this.lblMensaje.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // grpAcciones
            // 
            this.grpAcciones.Controls.Add(this.btnVolver);
            this.grpAcciones.Controls.Add(this.btnImprimir);
            this.grpAcciones.Controls.Add(this.btnBuscar);
            this.grpAcciones.Location = new System.Drawing.Point(260, 118);
            this.grpAcciones.Name = "grpAcciones";
            this.grpAcciones.Size = new System.Drawing.Size(289, 91);
            this.grpAcciones.TabIndex = 6;
            this.grpAcciones.TabStop = false;
            this.grpAcciones.Text = "Acciones";
            // 
            // btnVolver
            // 
            this.btnVolver.Location = new System.Drawing.Point(90, 48);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(119, 23);
            this.btnVolver.TabIndex = 2;
            this.btnVolver.Text = "&Volver";
            this.btnVolver.UseVisualStyleBackColor = true;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // btnImprimir
            // 
            this.btnImprimir.Location = new System.Drawing.Point(157, 19);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(119, 23);
            this.btnImprimir.TabIndex = 1;
            this.btnImprimir.Text = "&Imprimir seleccionado";
            this.btnImprimir.UseVisualStyleBackColor = true;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(32, 19);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(119, 23);
            this.btnBuscar.TabIndex = 0;
            this.btnBuscar.Text = "&Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // txtMarca
            // 
            this.txtMarca.Location = new System.Drawing.Point(260, 91);
            this.txtMarca.Name = "txtMarca";
            this.txtMarca.Size = new System.Drawing.Size(151, 20);
            this.txtMarca.TabIndex = 5;
            // 
            // lblDescripcionTarjeta
            // 
            this.lblDescripcionTarjeta.AutoSize = true;
            this.lblDescripcionTarjeta.Location = new System.Drawing.Point(260, 74);
            this.lblDescripcionTarjeta.Name = "lblDescripcionTarjeta";
            this.lblDescripcionTarjeta.Size = new System.Drawing.Size(84, 13);
            this.lblDescripcionTarjeta.TabIndex = 4;
            this.lblDescripcionTarjeta.Text = "Marca de tarjeta";
            // 
            // txtUltimos4Numeros
            // 
            this.txtUltimos4Numeros.Location = new System.Drawing.Point(260, 47);
            this.txtUltimos4Numeros.Mask = "0000";
            this.txtUltimos4Numeros.Name = "txtUltimos4Numeros";
            this.txtUltimos4Numeros.Size = new System.Drawing.Size(35, 20);
            this.txtUltimos4Numeros.TabIndex = 3;
            this.txtUltimos4Numeros.Validated += new System.EventHandler(this.txtUltimos4Numeros_Validated);
            // 
            // lblUltimos4Numeros
            // 
            this.lblUltimos4Numeros.AutoSize = true;
            this.lblUltimos4Numeros.Location = new System.Drawing.Point(210, 31);
            this.lblUltimos4Numeros.Name = "lblUltimos4Numeros";
            this.lblUltimos4Numeros.Size = new System.Drawing.Size(85, 13);
            this.lblUltimos4Numeros.TabIndex = 2;
            this.lblUltimos4Numeros.Text = "Ultimos 4 tarjeta:";
            this.lblUltimos4Numeros.Click += new System.EventHandler(this.lblUltimos4Numeros_Click);
            // 
            // calFecha
            // 
            this.calFecha.Location = new System.Drawing.Point(12, 47);
            this.calFecha.MaxSelectionCount = 1;
            this.calFecha.Name = "calFecha";
            this.calFecha.TabIndex = 1;
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.Location = new System.Drawing.Point(9, 25);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(124, 13);
            this.lblFecha.TabIndex = 0;
            this.lblFecha.Text = "Fecha de la transaccion:";
            // 
            // grpGrilla
            // 
            this.grpGrilla.Controls.Add(this.pnlImprimiendo);
            this.grpGrilla.Controls.Add(this.dgwTransacciones);
            this.grpGrilla.Location = new System.Drawing.Point(12, 236);
            this.grpGrilla.Name = "grpGrilla";
            this.grpGrilla.Size = new System.Drawing.Size(929, 412);
            this.grpGrilla.TabIndex = 1;
            this.grpGrilla.TabStop = false;
            this.grpGrilla.Text = "Resultados de la busqueda";
            // 
            // pnlImprimiendo
            // 
            this.pnlImprimiendo.BackColor = System.Drawing.Color.Aqua;
            this.pnlImprimiendo.Controls.Add(this.label4);
            this.pnlImprimiendo.Location = new System.Drawing.Point(6, 47);
            this.pnlImprimiendo.Name = "pnlImprimiendo";
            this.pnlImprimiendo.Size = new System.Drawing.Size(923, 75);
            this.pnlImprimiendo.TabIndex = 5;
            this.pnlImprimiendo.Visible = false;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(88, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(730, 40);
            this.label4.TabIndex = 3;
            this.label4.Text = "Imprimiendo...";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgwTransacciones
            // 
            this.dgwTransacciones.AllowUserToAddRows = false;
            this.dgwTransacciones.AllowUserToDeleteRows = false;
            this.dgwTransacciones.AllowUserToOrderColumns = true;
            this.dgwTransacciones.ColumnHeadersHeight = 30;
            this.dgwTransacciones.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.tipoOperacion,
            this.numerosTarjeta,
            this.marcaTarjeta,
            this.importe,
            this.fecha,
            this.sucursal,
            this.caja,
            this.nroCargo,
            this.codAuth,
            this.modo,
            this.idVtol,
            this.Anulado,
            this.clReversado});
            this.dgwTransacciones.Location = new System.Drawing.Point(0, 19);
            this.dgwTransacciones.Name = "dgwTransacciones";
            this.dgwTransacciones.ReadOnly = true;
            this.dgwTransacciones.Size = new System.Drawing.Size(995, 386);
            this.dgwTransacciones.TabIndex = 0;
            this.dgwTransacciones.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgwTransacciones_CellClick);
            this.dgwTransacciones.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgwTransacciones_CellContentClick);
            // 
            // tipoOperacion
            // 
            this.tipoOperacion.HeaderText = "Tipo Operacion";
            this.tipoOperacion.Name = "tipoOperacion";
            this.tipoOperacion.ReadOnly = true;
            this.tipoOperacion.Width = 90;
            // 
            // numerosTarjeta
            // 
            this.numerosTarjeta.HeaderText = "Numeros Tarjeta";
            this.numerosTarjeta.Name = "numerosTarjeta";
            this.numerosTarjeta.ReadOnly = true;
            // 
            // marcaTarjeta
            // 
            this.marcaTarjeta.HeaderText = "Marca Tarjeta";
            this.marcaTarjeta.Name = "marcaTarjeta";
            this.marcaTarjeta.ReadOnly = true;
            this.marcaTarjeta.Width = 80;
            // 
            // importe
            // 
            this.importe.HeaderText = "Importe";
            this.importe.Name = "importe";
            this.importe.ReadOnly = true;
            this.importe.Width = 75;
            // 
            // fecha
            // 
            this.fecha.HeaderText = "Fecha";
            this.fecha.Name = "fecha";
            this.fecha.ReadOnly = true;
            this.fecha.Width = 75;
            // 
            // sucursal
            // 
            this.sucursal.HeaderText = "Sucursal";
            this.sucursal.Name = "sucursal";
            this.sucursal.ReadOnly = true;
            this.sucursal.Width = 55;
            // 
            // caja
            // 
            this.caja.HeaderText = "Caja";
            this.caja.Name = "caja";
            this.caja.ReadOnly = true;
            this.caja.Width = 45;
            // 
            // nroCargo
            // 
            this.nroCargo.HeaderText = "Nro. Cargo";
            this.nroCargo.Name = "nroCargo";
            this.nroCargo.ReadOnly = true;
            this.nroCargo.Width = 65;
            // 
            // codAuth
            // 
            this.codAuth.HeaderText = "Cod. Auth";
            this.codAuth.Name = "codAuth";
            this.codAuth.ReadOnly = true;
            this.codAuth.Width = 65;
            // 
            // modo
            // 
            this.modo.HeaderText = "Modo";
            this.modo.MinimumWidth = 2;
            this.modo.Name = "modo";
            this.modo.ReadOnly = true;
            this.modo.Width = 40;
            // 
            // idVtol
            // 
            this.idVtol.HeaderText = "ID Vtol";
            this.idVtol.Name = "idVtol";
            this.idVtol.ReadOnly = true;
            this.idVtol.Width = 60;
            // 
            // Anulado
            // 
            this.Anulado.HeaderText = "Anulado";
            this.Anulado.Name = "Anulado";
            this.Anulado.ReadOnly = true;
            this.Anulado.Width = 60;
            // 
            // clReversado
            // 
            this.clReversado.HeaderText = "Reversado";
            this.clReversado.Name = "clReversado";
            this.clReversado.ReadOnly = true;
            this.clReversado.Width = 60;
            // 
            // FrmReimpresionCupones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(967, 652);
            this.ControlBox = false;
            this.Controls.Add(this.grpGrilla);
            this.Controls.Add(this.grpFiltros);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.KeyPreview = true;
            this.Name = "FrmReimpresionCupones";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reimpresion de cupones";
            this.Load += new System.EventHandler(this.FrmReimpresionCupones_Load);
            this.grpFiltros.ResumeLayout(false);
            this.grpFiltros.PerformLayout();
            this.pnlMensaje.ResumeLayout(false);
            this.grpAcciones.ResumeLayout(false);
            this.grpGrilla.ResumeLayout(false);
            this.pnlImprimiendo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgwTransacciones)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpFiltros;
        private System.Windows.Forms.GroupBox grpAcciones;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.TextBox txtMarca;
        private System.Windows.Forms.Label lblDescripcionTarjeta;
        private System.Windows.Forms.MaskedTextBox txtUltimos4Numeros;
        private System.Windows.Forms.Label lblUltimos4Numeros;
        private System.Windows.Forms.MonthCalendar calFecha;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.GroupBox grpGrilla;
        private System.Windows.Forms.DataGridView dgwTransacciones;
        private System.Windows.Forms.Panel pnlMensaje;
        private System.Windows.Forms.Label lblMensaje;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblSucursal;
        private System.Windows.Forms.TextBox txtCaja;
        private System.Windows.Forms.TextBox txtSucursal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtVtolId;
        private System.Windows.Forms.Panel pnlImprimiendo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridViewTextBoxColumn tipoOperacion;
        private System.Windows.Forms.DataGridViewTextBoxColumn numerosTarjeta;
        private System.Windows.Forms.DataGridViewTextBoxColumn marcaTarjeta;
        private System.Windows.Forms.DataGridViewTextBoxColumn importe;
        private System.Windows.Forms.DataGridViewTextBoxColumn fecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn sucursal;
        private System.Windows.Forms.DataGridViewTextBoxColumn caja;
        private System.Windows.Forms.DataGridViewTextBoxColumn nroCargo;
        private System.Windows.Forms.DataGridViewTextBoxColumn codAuth;
        private System.Windows.Forms.DataGridViewTextBoxColumn modo;
        private System.Windows.Forms.DataGridViewTextBoxColumn idVtol;
        private System.Windows.Forms.DataGridViewTextBoxColumn Anulado;
        private System.Windows.Forms.DataGridViewTextBoxColumn clReversado;
    }
}