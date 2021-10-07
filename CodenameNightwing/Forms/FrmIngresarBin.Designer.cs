namespace CodenameNightwing.Forms
{
    partial class FrmIngresarBin
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
            this.grpNumeroAutorizacion = new System.Windows.Forms.GroupBox();
            this.txtBIN = new System.Windows.Forms.MaskedTextBox();
            this.lblCodigoDeAutorizacion = new System.Windows.Forms.Label();
            this.grpAcciones = new System.Windows.Forms.GroupBox();
            this.btnVolver = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.cmbTarjetas = new System.Windows.Forms.ComboBox();
            this.grpNumeroAutorizacion.SuspendLayout();
            this.grpAcciones.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpNumeroAutorizacion
            // 
            this.grpNumeroAutorizacion.Controls.Add(this.cmbTarjetas);
            this.grpNumeroAutorizacion.Controls.Add(this.txtBIN);
            this.grpNumeroAutorizacion.Controls.Add(this.lblCodigoDeAutorizacion);
            this.grpNumeroAutorizacion.Location = new System.Drawing.Point(12, 12);
            this.grpNumeroAutorizacion.Name = "grpNumeroAutorizacion";
            this.grpNumeroAutorizacion.Size = new System.Drawing.Size(195, 98);
            this.grpNumeroAutorizacion.TabIndex = 0;
            this.grpNumeroAutorizacion.TabStop = false;
            this.grpNumeroAutorizacion.Text = "Ingrese BIN de la tarjeta";
            // 
            // txtBIN
            // 
            this.txtBIN.Location = new System.Drawing.Point(130, 54);
            this.txtBIN.Mask = "0#####";
            this.txtBIN.Name = "txtBIN";
            this.txtBIN.Size = new System.Drawing.Size(54, 20);
            this.txtBIN.TabIndex = 1;
            this.txtBIN.MaskInputRejected += new System.Windows.Forms.MaskInputRejectedEventHandler(this.txtBin_MaskInputRejected);
            // 
            // lblCodigoDeAutorizacion
            // 
            this.lblCodigoDeAutorizacion.AutoSize = true;
            this.lblCodigoDeAutorizacion.Location = new System.Drawing.Point(6, 58);
            this.lblCodigoDeAutorizacion.Name = "lblCodigoDeAutorizacion";
            this.lblCodigoDeAutorizacion.Size = new System.Drawing.Size(28, 13);
            this.lblCodigoDeAutorizacion.TabIndex = 0;
            this.lblCodigoDeAutorizacion.Text = "BIN:";
            // 
            // grpAcciones
            // 
            this.grpAcciones.Controls.Add(this.btnVolver);
            this.grpAcciones.Controls.Add(this.btnAceptar);
            this.grpAcciones.Location = new System.Drawing.Point(12, 116);
            this.grpAcciones.Name = "grpAcciones";
            this.grpAcciones.Size = new System.Drawing.Size(195, 63);
            this.grpAcciones.TabIndex = 1;
            this.grpAcciones.TabStop = false;
            this.grpAcciones.Text = "Acciones";
            // 
            // btnVolver
            // 
            this.btnVolver.Location = new System.Drawing.Point(99, 19);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(84, 38);
            this.btnVolver.TabIndex = 1;
            this.btnVolver.Text = "&Volver";
            this.btnVolver.UseVisualStyleBackColor = true;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // btnAceptar
            // 
            this.btnAceptar.Location = new System.Drawing.Point(9, 19);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(84, 38);
            this.btnAceptar.TabIndex = 0;
            this.btnAceptar.Text = "&Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // cmbTarjetas
            // 
            this.cmbTarjetas.FormattingEnabled = true;
            this.cmbTarjetas.Location = new System.Drawing.Point(6, 19);
            this.cmbTarjetas.Name = "cmbTarjetas";
            this.cmbTarjetas.Size = new System.Drawing.Size(176, 21);
            this.cmbTarjetas.TabIndex = 2;
            // 
            // FrmIngresarBin
            // 
            this.AcceptButton = this.btnAceptar;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(216, 191);
            this.ControlBox = false;
            this.Controls.Add(this.grpAcciones);
            this.Controls.Add(this.grpNumeroAutorizacion);
            this.KeyPreview = true;
            this.Name = "FrmIngresarBin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BIN de la tarjeta (Seis primeros)";
            this.Load += new System.EventHandler(this.FrmIngresarBin_Load);
            this.grpNumeroAutorizacion.ResumeLayout(false);
            this.grpNumeroAutorizacion.PerformLayout();
            this.grpAcciones.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpNumeroAutorizacion;
        private System.Windows.Forms.Label lblCodigoDeAutorizacion;
        private System.Windows.Forms.GroupBox grpAcciones;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.MaskedTextBox txtBIN;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.ComboBox cmbTarjetas;
    }
}