namespace CodenameNightwing.Forms
{
    partial class FrmVerificarPromos
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
            this.grpDatosTarjeta = new System.Windows.Forms.GroupBox();
            this.chx_importe = new System.Windows.Forms.CheckBox();
            this.pnlNumeroTarjeta = new System.Windows.Forms.Panel();
            this.txtUltimosNumeros = new System.Windows.Forms.MaskedTextBox();
            this.txtPrimerosNumeros = new System.Windows.Forms.MaskedTextBox();
            this.txtNumerosRestantes = new System.Windows.Forms.MaskedTextBox();
            this.lblNumeroTarjeta = new System.Windows.Forms.Label();
            this.pnlBin = new System.Windows.Forms.Panel();
            this.txtBin = new System.Windows.Forms.TextBox();
            this.lblBin = new System.Windows.Forms.Label();
            this.txtBanco = new System.Windows.Forms.TextBox();
            this.txtDescripcionTarjeta = new System.Windows.Forms.TextBox();
            this.lblBanco = new System.Windows.Forms.Label();
            this.lblTarjeta = new System.Windows.Forms.Label();
            this.grpAcciones = new System.Windows.Forms.GroupBox();
            this.btnVolver = new System.Windows.Forms.Button();
            this.btnPlanes = new System.Windows.Forms.Button();
            this.grpPlanesEImportes = new System.Windows.Forms.GroupBox();
            this.txtImporteAAutorizar = new System.Windows.Forms.Label();
            this.txtIntereses = new System.Windows.Forms.Label();
            this.txtCuotas = new System.Windows.Forms.Label();
            this.txtImporte = new System.Windows.Forms.TextBox();
            this.lblImporte = new System.Windows.Forms.Label();
            this.grdCuotas = new System.Windows.Forms.DataGridView();
            this.Cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Interes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cuota = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblIntereses = new System.Windows.Forms.Label();
            this.lblImporteAAutorizar = new System.Windows.Forms.Label();
            this.lblCantCuotas = new System.Windows.Forms.Label();
            this.pnlMensaje = new System.Windows.Forms.Panel();
            this.lblMensaje = new System.Windows.Forms.Label();
            this.grpDatosTarjeta.SuspendLayout();
            this.pnlNumeroTarjeta.SuspendLayout();
            this.pnlBin.SuspendLayout();
            this.grpAcciones.SuspendLayout();
            this.grpPlanesEImportes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdCuotas)).BeginInit();
            this.pnlMensaje.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpDatosTarjeta
            // 
            this.grpDatosTarjeta.Controls.Add(this.chx_importe);
            this.grpDatosTarjeta.Controls.Add(this.pnlNumeroTarjeta);
            this.grpDatosTarjeta.Controls.Add(this.pnlBin);
            this.grpDatosTarjeta.Controls.Add(this.txtBanco);
            this.grpDatosTarjeta.Controls.Add(this.txtDescripcionTarjeta);
            this.grpDatosTarjeta.Controls.Add(this.lblBanco);
            this.grpDatosTarjeta.Controls.Add(this.lblTarjeta);
            this.grpDatosTarjeta.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpDatosTarjeta.Location = new System.Drawing.Point(12, 12);
            this.grpDatosTarjeta.Name = "grpDatosTarjeta";
            this.grpDatosTarjeta.Size = new System.Drawing.Size(296, 227);
            this.grpDatosTarjeta.TabIndex = 1;
            this.grpDatosTarjeta.TabStop = false;
            this.grpDatosTarjeta.Text = "Datos de la tarjeta";
            // 
            // chx_importe
            // 
            this.chx_importe.AutoSize = true;
            this.chx_importe.Location = new System.Drawing.Point(11, 54);
            this.chx_importe.Name = "chx_importe";
            this.chx_importe.Size = new System.Drawing.Size(106, 20);
            this.chx_importe.TabIndex = 22;
            this.chx_importe.Text = "Con importe?";
            this.chx_importe.UseVisualStyleBackColor = true;
            this.chx_importe.CheckedChanged += new System.EventHandler(this.chx_importe_CheckedChanged);
            // 
            // pnlNumeroTarjeta
            // 
            this.pnlNumeroTarjeta.Controls.Add(this.txtUltimosNumeros);
            this.pnlNumeroTarjeta.Controls.Add(this.txtPrimerosNumeros);
            this.pnlNumeroTarjeta.Controls.Add(this.txtNumerosRestantes);
            this.pnlNumeroTarjeta.Controls.Add(this.lblNumeroTarjeta);
            this.pnlNumeroTarjeta.Location = new System.Drawing.Point(5, 158);
            this.pnlNumeroTarjeta.Name = "pnlNumeroTarjeta";
            this.pnlNumeroTarjeta.Size = new System.Drawing.Size(285, 63);
            this.pnlNumeroTarjeta.TabIndex = 21;
            // 
            // txtUltimosNumeros
            // 
            this.txtUltimosNumeros.BackColor = System.Drawing.Color.PaleTurquoise;
            this.txtUltimosNumeros.Enabled = false;
            this.txtUltimosNumeros.Location = new System.Drawing.Point(236, 13);
            this.txtUltimosNumeros.Name = "txtUltimosNumeros";
            this.txtUltimosNumeros.Size = new System.Drawing.Size(44, 22);
            this.txtUltimosNumeros.TabIndex = 12;
            // 
            // txtPrimerosNumeros
            // 
            this.txtPrimerosNumeros.BackColor = System.Drawing.Color.PaleTurquoise;
            this.txtPrimerosNumeros.Enabled = false;
            this.txtPrimerosNumeros.Location = new System.Drawing.Point(112, 13);
            this.txtPrimerosNumeros.Name = "txtPrimerosNumeros";
            this.txtPrimerosNumeros.Size = new System.Drawing.Size(54, 22);
            this.txtPrimerosNumeros.TabIndex = 11;
            // 
            // txtNumerosRestantes
            // 
            this.txtNumerosRestantes.Location = new System.Drawing.Point(172, 13);
            this.txtNumerosRestantes.Name = "txtNumerosRestantes";
            this.txtNumerosRestantes.Size = new System.Drawing.Size(58, 22);
            this.txtNumerosRestantes.TabIndex = 10;
            this.txtNumerosRestantes.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            // 
            // lblNumeroTarjeta
            // 
            this.lblNumeroTarjeta.AutoSize = true;
            this.lblNumeroTarjeta.Location = new System.Drawing.Point(3, 16);
            this.lblNumeroTarjeta.Name = "lblNumeroTarjeta";
            this.lblNumeroTarjeta.Size = new System.Drawing.Size(98, 16);
            this.lblNumeroTarjeta.TabIndex = 9;
            this.lblNumeroTarjeta.Text = "Número..............";
            // 
            // pnlBin
            // 
            this.pnlBin.Controls.Add(this.txtBin);
            this.pnlBin.Controls.Add(this.lblBin);
            this.pnlBin.Location = new System.Drawing.Point(5, 21);
            this.pnlBin.Name = "pnlBin";
            this.pnlBin.Size = new System.Drawing.Size(285, 34);
            this.pnlBin.TabIndex = 2;
            // 
            // txtBin
            // 
            this.txtBin.Location = new System.Drawing.Point(135, 3);
            this.txtBin.Name = "txtBin";
            this.txtBin.Size = new System.Drawing.Size(105, 22);
            this.txtBin.TabIndex = 3;
            this.txtBin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBin_KeyPress);
            // 
            // lblBin
            // 
            this.lblBin.AutoSize = true;
            this.lblBin.Location = new System.Drawing.Point(3, 6);
            this.lblBin.Name = "lblBin";
            this.lblBin.Size = new System.Drawing.Size(117, 16);
            this.lblBin.TabIndex = 13;
            this.lblBin.Text = "Ingrese BIN.............";
            // 
            // txtBanco
            // 
            this.txtBanco.Enabled = false;
            this.txtBanco.Location = new System.Drawing.Point(140, 110);
            this.txtBanco.Name = "txtBanco";
            this.txtBanco.Size = new System.Drawing.Size(145, 22);
            this.txtBanco.TabIndex = 12;
            // 
            // txtDescripcionTarjeta
            // 
            this.txtDescripcionTarjeta.Enabled = false;
            this.txtDescripcionTarjeta.Location = new System.Drawing.Point(140, 78);
            this.txtDescripcionTarjeta.Name = "txtDescripcionTarjeta";
            this.txtDescripcionTarjeta.Size = new System.Drawing.Size(145, 22);
            this.txtDescripcionTarjeta.TabIndex = 11;
            // 
            // lblBanco
            // 
            this.lblBanco.AutoSize = true;
            this.lblBanco.Location = new System.Drawing.Point(8, 113);
            this.lblBanco.Name = "lblBanco";
            this.lblBanco.Size = new System.Drawing.Size(116, 16);
            this.lblBanco.TabIndex = 1;
            this.lblBanco.Text = "Banco.......................";
            // 
            // lblTarjeta
            // 
            this.lblTarjeta.AutoSize = true;
            this.lblTarjeta.Location = new System.Drawing.Point(8, 81);
            this.lblTarjeta.Name = "lblTarjeta";
            this.lblTarjeta.Size = new System.Drawing.Size(117, 16);
            this.lblTarjeta.TabIndex = 0;
            this.lblTarjeta.Text = "Tarjeta......................";
            // 
            // grpAcciones
            // 
            this.grpAcciones.Controls.Add(this.btnVolver);
            this.grpAcciones.Controls.Add(this.btnPlanes);
            this.grpAcciones.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpAcciones.Location = new System.Drawing.Point(12, 245);
            this.grpAcciones.Name = "grpAcciones";
            this.grpAcciones.Size = new System.Drawing.Size(296, 65);
            this.grpAcciones.TabIndex = 6;
            this.grpAcciones.TabStop = false;
            this.grpAcciones.Text = "Acciones";
            // 
            // btnVolver
            // 
            this.btnVolver.Location = new System.Drawing.Point(158, 21);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(119, 38);
            this.btnVolver.TabIndex = 9;
            this.btnVolver.Text = "&Volver";
            this.btnVolver.UseVisualStyleBackColor = true;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // btnPlanes
            // 
            this.btnPlanes.Location = new System.Drawing.Point(15, 21);
            this.btnPlanes.Name = "btnPlanes";
            this.btnPlanes.Size = new System.Drawing.Size(119, 38);
            this.btnPlanes.TabIndex = 7;
            this.btnPlanes.Text = "&Planes";
            this.btnPlanes.UseVisualStyleBackColor = true;
            this.btnPlanes.Click += new System.EventHandler(this.btnPlanes_Click);
            // 
            // grpPlanesEImportes
            // 
            this.grpPlanesEImportes.Controls.Add(this.txtImporteAAutorizar);
            this.grpPlanesEImportes.Controls.Add(this.txtIntereses);
            this.grpPlanesEImportes.Controls.Add(this.txtCuotas);
            this.grpPlanesEImportes.Controls.Add(this.txtImporte);
            this.grpPlanesEImportes.Controls.Add(this.lblImporte);
            this.grpPlanesEImportes.Controls.Add(this.grdCuotas);
            this.grpPlanesEImportes.Controls.Add(this.lblIntereses);
            this.grpPlanesEImportes.Controls.Add(this.lblImporteAAutorizar);
            this.grpPlanesEImportes.Controls.Add(this.lblCantCuotas);
            this.grpPlanesEImportes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpPlanesEImportes.Location = new System.Drawing.Point(314, 13);
            this.grpPlanesEImportes.Name = "grpPlanesEImportes";
            this.grpPlanesEImportes.Size = new System.Drawing.Size(294, 297);
            this.grpPlanesEImportes.TabIndex = 4;
            this.grpPlanesEImportes.TabStop = false;
            this.grpPlanesEImportes.Text = "Planes e importes";
            // 
            // txtImporteAAutorizar
            // 
            this.txtImporteAAutorizar.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtImporteAAutorizar.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtImporteAAutorizar.Location = new System.Drawing.Point(183, 256);
            this.txtImporteAAutorizar.Name = "txtImporteAAutorizar";
            this.txtImporteAAutorizar.Size = new System.Drawing.Size(105, 22);
            this.txtImporteAAutorizar.TabIndex = 18;
            // 
            // txtIntereses
            // 
            this.txtIntereses.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtIntereses.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtIntereses.Location = new System.Drawing.Point(183, 232);
            this.txtIntereses.Name = "txtIntereses";
            this.txtIntereses.Size = new System.Drawing.Size(105, 22);
            this.txtIntereses.TabIndex = 17;
            // 
            // txtCuotas
            // 
            this.txtCuotas.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtCuotas.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtCuotas.Location = new System.Drawing.Point(183, 204);
            this.txtCuotas.Name = "txtCuotas";
            this.txtCuotas.Size = new System.Drawing.Size(105, 22);
            this.txtCuotas.TabIndex = 16;
            // 
            // txtImporte
            // 
            this.txtImporte.Location = new System.Drawing.Point(183, 25);
            this.txtImporte.Name = "txtImporte";
            this.txtImporte.Size = new System.Drawing.Size(105, 22);
            this.txtImporte.TabIndex = 5;
            this.txtImporte.TextChanged += new System.EventHandler(this.txtImporte_TextChanged);
            this.txtImporte.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtImporte_KeyPress);
            // 
            // lblImporte
            // 
            this.lblImporte.AutoSize = true;
            this.lblImporte.Location = new System.Drawing.Point(6, 28);
            this.lblImporte.Name = "lblImporte";
            this.lblImporte.Size = new System.Drawing.Size(176, 16);
            this.lblImporte.TabIndex = 14;
            this.lblImporte.Text = "Importe.........................................";
            // 
            // grdCuotas
            // 
            this.grdCuotas.AllowUserToAddRows = false;
            this.grdCuotas.AllowUserToDeleteRows = false;
            this.grdCuotas.AllowUserToResizeColumns = false;
            this.grdCuotas.AllowUserToResizeRows = false;
            this.grdCuotas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdCuotas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Cantidad,
            this.Interes,
            this.Cuota,
            this.Total});
            this.grdCuotas.Location = new System.Drawing.Point(6, 53);
            this.grdCuotas.MultiSelect = false;
            this.grdCuotas.Name = "grdCuotas";
            this.grdCuotas.ReadOnly = true;
            this.grdCuotas.RowHeadersWidth = 30;
            this.grdCuotas.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.grdCuotas.Size = new System.Drawing.Size(282, 148);
            this.grdCuotas.TabIndex = 8;
            this.grdCuotas.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdCuotas_CellContentClick);
            this.grdCuotas.Click += new System.EventHandler(this.grdCuotas_Click);
            this.grdCuotas.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dataGridView1_KeyUp);
            // 
            // Cantidad
            // 
            this.Cantidad.HeaderText = "Cant.";
            this.Cantidad.Name = "Cantidad";
            this.Cantidad.ReadOnly = true;
            this.Cantidad.Width = 50;
            // 
            // Interes
            // 
            this.Interes.HeaderText = "% int.";
            this.Interes.Name = "Interes";
            this.Interes.ReadOnly = true;
            this.Interes.Width = 50;
            // 
            // Cuota
            // 
            this.Cuota.HeaderText = "Cuota";
            this.Cuota.Name = "Cuota";
            this.Cuota.ReadOnly = true;
            this.Cuota.Width = 75;
            // 
            // Total
            // 
            this.Total.HeaderText = "Total";
            this.Total.Name = "Total";
            this.Total.ReadOnly = true;
            this.Total.Width = 75;
            // 
            // lblIntereses
            // 
            this.lblIntereses.AutoSize = true;
            this.lblIntereses.Location = new System.Drawing.Point(6, 236);
            this.lblIntereses.Name = "lblIntereses";
            this.lblIntereses.Size = new System.Drawing.Size(174, 16);
            this.lblIntereses.TabIndex = 12;
            this.lblIntereses.Text = "Intereses.....................................";
            // 
            // lblImporteAAutorizar
            // 
            this.lblImporteAAutorizar.AutoSize = true;
            this.lblImporteAAutorizar.Location = new System.Drawing.Point(6, 262);
            this.lblImporteAAutorizar.Name = "lblImporteAAutorizar";
            this.lblImporteAAutorizar.Size = new System.Drawing.Size(175, 16);
            this.lblImporteAAutorizar.TabIndex = 9;
            this.lblImporteAAutorizar.Text = "Importe a autorizar...................";
            // 
            // lblCantCuotas
            // 
            this.lblCantCuotas.AutoSize = true;
            this.lblCantCuotas.Location = new System.Drawing.Point(6, 210);
            this.lblCantCuotas.Name = "lblCantCuotas";
            this.lblCantCuotas.Size = new System.Drawing.Size(175, 16);
            this.lblCantCuotas.TabIndex = 8;
            this.lblCantCuotas.Text = "Cantidad de cuotas.................";
            // 
            // pnlMensaje
            // 
            this.pnlMensaje.BackColor = System.Drawing.Color.Aqua;
            this.pnlMensaje.Controls.Add(this.lblMensaje);
            this.pnlMensaje.Location = new System.Drawing.Point(11, 126);
            this.pnlMensaje.Name = "pnlMensaje";
            this.pnlMensaje.Size = new System.Drawing.Size(597, 60);
            this.pnlMensaje.TabIndex = 22;
            this.pnlMensaje.Visible = false;
            // 
            // lblMensaje
            // 
            this.lblMensaje.AutoSize = true;
            this.lblMensaje.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMensaje.Location = new System.Drawing.Point(101, 15);
            this.lblMensaje.Name = "lblMensaje";
            this.lblMensaje.Size = new System.Drawing.Size(400, 24);
            this.lblMensaje.TabIndex = 0;
            this.lblMensaje.Text = "Cargando promociones, aguarde un instante...";
            // 
            // FrmVerificarPromos
            // 
            this.AcceptButton = this.btnPlanes;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(622, 322);
            this.ControlBox = false;
            this.Controls.Add(this.pnlMensaje);
            this.Controls.Add(this.grpPlanesEImportes);
            this.Controls.Add(this.grpAcciones);
            this.Controls.Add(this.grpDatosTarjeta);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.KeyPreview = true;
            this.Name = "FrmVerificarPromos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Verificar promos";
            this.Load += new System.EventHandler(this.FrmVerificarPromos_Load);
            this.grpDatosTarjeta.ResumeLayout(false);
            this.grpDatosTarjeta.PerformLayout();
            this.pnlNumeroTarjeta.ResumeLayout(false);
            this.pnlNumeroTarjeta.PerformLayout();
            this.pnlBin.ResumeLayout(false);
            this.pnlBin.PerformLayout();
            this.grpAcciones.ResumeLayout(false);
            this.grpPlanesEImportes.ResumeLayout(false);
            this.grpPlanesEImportes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdCuotas)).EndInit();
            this.pnlMensaje.ResumeLayout(false);
            this.pnlMensaje.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpDatosTarjeta;
        private System.Windows.Forms.GroupBox grpAcciones;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.Button btnPlanes;
        private System.Windows.Forms.GroupBox grpPlanesEImportes;
        private System.Windows.Forms.DataGridView grdCuotas;
        private System.Windows.Forms.Label lblIntereses;
        private System.Windows.Forms.Label lblImporteAAutorizar;
        private System.Windows.Forms.Label lblCantCuotas;
        private System.Windows.Forms.TextBox txtImporte;
        private System.Windows.Forms.Label lblImporte;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn Interes;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cuota;
        private System.Windows.Forms.DataGridViewTextBoxColumn Total;
        private System.Windows.Forms.Panel pnlNumeroTarjeta;
        private System.Windows.Forms.Panel pnlBin;
        private System.Windows.Forms.TextBox txtBin;
        private System.Windows.Forms.Label lblBin;
        private System.Windows.Forms.TextBox txtBanco;
        private System.Windows.Forms.TextBox txtDescripcionTarjeta;
        private System.Windows.Forms.Label lblBanco;
        private System.Windows.Forms.Label lblTarjeta;
        private System.Windows.Forms.MaskedTextBox txtUltimosNumeros;
        private System.Windows.Forms.MaskedTextBox txtPrimerosNumeros;
        private System.Windows.Forms.MaskedTextBox txtNumerosRestantes;
        private System.Windows.Forms.Label lblNumeroTarjeta;
        private System.Windows.Forms.Label txtCuotas;
        private System.Windows.Forms.Label txtImporteAAutorizar;
        private System.Windows.Forms.Label txtIntereses;
        private System.Windows.Forms.Panel pnlMensaje;
        private System.Windows.Forms.Label lblMensaje;
        private System.Windows.Forms.CheckBox chx_importe;
    }
}