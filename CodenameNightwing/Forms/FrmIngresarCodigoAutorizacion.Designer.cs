namespace CodenameNightwing.Forms
{
    partial class FrmIngresarCodigoAutorizacion
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
            this.txtNumeroAutorizacion = new System.Windows.Forms.MaskedTextBox();
            this.lblCodigoDeAutorizacion = new System.Windows.Forms.Label();
            this.grpAcciones = new System.Windows.Forms.GroupBox();
            this.btnVolver = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.grpNumeroAutorizacion.SuspendLayout();
            this.grpAcciones.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpNumeroAutorizacion
            // 
            this.grpNumeroAutorizacion.Controls.Add(this.txtNumeroAutorizacion);
            this.grpNumeroAutorizacion.Controls.Add(this.lblCodigoDeAutorizacion);
            this.grpNumeroAutorizacion.Location = new System.Drawing.Point(12, 12);
            this.grpNumeroAutorizacion.Name = "grpNumeroAutorizacion";
            this.grpNumeroAutorizacion.Size = new System.Drawing.Size(195, 64);
            this.grpNumeroAutorizacion.TabIndex = 0;
            this.grpNumeroAutorizacion.TabStop = false;
            this.grpNumeroAutorizacion.Text = "Ingrese codigo de autorizacion";
            // 
            // txtNumeroAutorizacion
            // 
            this.txtNumeroAutorizacion.Location = new System.Drawing.Point(130, 25);
            this.txtNumeroAutorizacion.Mask = "0#####";
            this.txtNumeroAutorizacion.Name = "txtNumeroAutorizacion";
            this.txtNumeroAutorizacion.Size = new System.Drawing.Size(54, 20);
            this.txtNumeroAutorizacion.TabIndex = 1;
            this.txtNumeroAutorizacion.MaskInputRejected += new System.Windows.Forms.MaskInputRejectedEventHandler(this.txtNumeroAutorizacion_MaskInputRejected);
            // 
            // lblCodigoDeAutorizacion
            // 
            this.lblCodigoDeAutorizacion.AutoSize = true;
            this.lblCodigoDeAutorizacion.Location = new System.Drawing.Point(6, 28);
            this.lblCodigoDeAutorizacion.Name = "lblCodigoDeAutorizacion";
            this.lblCodigoDeAutorizacion.Size = new System.Drawing.Size(118, 13);
            this.lblCodigoDeAutorizacion.TabIndex = 0;
            this.lblCodigoDeAutorizacion.Text = "Codigo de autorizacion:";
            // 
            // grpAcciones
            // 
            this.grpAcciones.Controls.Add(this.btnVolver);
            this.grpAcciones.Controls.Add(this.btnAceptar);
            this.grpAcciones.Location = new System.Drawing.Point(12, 82);
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
            // FrmIngresarCodigoAutorizacion
            // 
            this.AcceptButton = this.btnAceptar;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(216, 156);
            this.ControlBox = false;
            this.Controls.Add(this.grpAcciones);
            this.Controls.Add(this.grpNumeroAutorizacion);
            this.KeyPreview = true;
            this.Name = "FrmIngresarCodigoAutorizacion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Codigo de autorizacion telefonica";
            this.Load += new System.EventHandler(this.FrmIngresarCodigoAutorizacion_Load);
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
        private System.Windows.Forms.MaskedTextBox txtNumeroAutorizacion;
        private System.Windows.Forms.Button btnVolver;
    }
}