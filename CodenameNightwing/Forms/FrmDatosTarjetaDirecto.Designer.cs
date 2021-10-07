namespace CodenameNightwing.Forms
{
    partial class FrmDatosTarjetaDirecto
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
            this.grpNumeros = new System.Windows.Forms.GroupBox();
            this.txtNumeroTarjeta = new System.Windows.Forms.MaskedTextBox();
            this.txtCodSeguridad = new System.Windows.Forms.MaskedTextBox();
            this.lblCodSeguridad = new System.Windows.Forms.Label();
            this.txtVencimiento = new System.Windows.Forms.MaskedTextBox();
            this.lblVencimiento = new System.Windows.Forms.Label();
            this.txtPrimerosNumeros = new System.Windows.Forms.MaskedTextBox();
            this.txtNumerosRestantes = new System.Windows.Forms.MaskedTextBox();
            this.lblNumeroTarjeta = new System.Windows.Forms.Label();
            this.grpAcciones = new System.Windows.Forms.GroupBox();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.grpNumeros.SuspendLayout();
            this.grpAcciones.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpNumeros
            // 
            this.grpNumeros.Controls.Add(this.txtNumeroTarjeta);
            this.grpNumeros.Controls.Add(this.txtCodSeguridad);
            this.grpNumeros.Controls.Add(this.lblCodSeguridad);
            this.grpNumeros.Controls.Add(this.txtVencimiento);
            this.grpNumeros.Controls.Add(this.lblVencimiento);
            this.grpNumeros.Controls.Add(this.txtPrimerosNumeros);
            this.grpNumeros.Controls.Add(this.txtNumerosRestantes);
            this.grpNumeros.Controls.Add(this.lblNumeroTarjeta);
            this.grpNumeros.Location = new System.Drawing.Point(12, 12);
            this.grpNumeros.Name = "grpNumeros";
            this.grpNumeros.Size = new System.Drawing.Size(283, 125);
            this.grpNumeros.TabIndex = 0;
            this.grpNumeros.TabStop = false;
            this.grpNumeros.Text = "Ingrese el numero de tarjeta";
            // 
            // txtNumeroTarjeta
            // 
            this.txtNumeroTarjeta.Enabled = false;
            this.txtNumeroTarjeta.Location = new System.Drawing.Point(92, 19);
            this.txtNumeroTarjeta.Name = "txtNumeroTarjeta";
            this.txtNumeroTarjeta.Size = new System.Drawing.Size(185, 20);
            this.txtNumeroTarjeta.TabIndex = 1;
            this.txtNumeroTarjeta.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            this.txtNumeroTarjeta.Visible = false;
            this.txtNumeroTarjeta.MaskInputRejected += new System.Windows.Forms.MaskInputRejectedEventHandler(this.txtNumeroTarjeta_MaskInputRejected);
            this.txtNumeroTarjeta.TextChanged += new System.EventHandler(this.txtNumeroTarjeta_TextChanged);
            // 
            // txtCodSeguridad
            // 
            this.txtCodSeguridad.Location = new System.Drawing.Point(91, 88);
            this.txtCodSeguridad.Name = "txtCodSeguridad";
            this.txtCodSeguridad.PasswordChar = '*';
            this.txtCodSeguridad.Size = new System.Drawing.Size(41, 20);
            this.txtCodSeguridad.TabIndex = 3;
            this.txtCodSeguridad.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            this.txtCodSeguridad.UseSystemPasswordChar = true;
            // 
            // lblCodSeguridad
            // 
            this.lblCodSeguridad.AutoSize = true;
            this.lblCodSeguridad.Location = new System.Drawing.Point(1, 91);
            this.lblCodSeguridad.Name = "lblCodSeguridad";
            this.lblCodSeguridad.Size = new System.Drawing.Size(89, 13);
            this.lblCodSeguridad.TabIndex = 14;
            this.lblCodSeguridad.Text = "Cod. Seguridad...";
            this.lblCodSeguridad.Click += new System.EventHandler(this.lblCodSeguridad_Click);
            // 
            // txtVencimiento
            // 
            this.txtVencimiento.Location = new System.Drawing.Point(92, 50);
            this.txtVencimiento.Mask = "00/00";
            this.txtVencimiento.Name = "txtVencimiento";
            this.txtVencimiento.Size = new System.Drawing.Size(40, 20);
            this.txtVencimiento.TabIndex = 2;
            this.txtVencimiento.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            this.txtVencimiento.MaskInputRejected += new System.Windows.Forms.MaskInputRejectedEventHandler(this.txtVencimiento_MaskInputRejected);
            this.txtVencimiento.TextChanged += new System.EventHandler(this.txtVencimiento_TextChanged);
            // 
            // lblVencimiento
            // 
            this.lblVencimiento.AutoSize = true;
            this.lblVencimiento.Location = new System.Drawing.Point(12, 53);
            this.lblVencimiento.Name = "lblVencimiento";
            this.lblVencimiento.Size = new System.Drawing.Size(74, 13);
            this.lblVencimiento.TabIndex = 13;
            this.lblVencimiento.Text = "Vencimiento...";
            // 
            // txtPrimerosNumeros
            // 
            this.txtPrimerosNumeros.BackColor = System.Drawing.Color.PaleTurquoise;
            this.txtPrimerosNumeros.Enabled = false;
            this.txtPrimerosNumeros.Location = new System.Drawing.Point(91, 19);
            this.txtPrimerosNumeros.Name = "txtPrimerosNumeros";
            this.txtPrimerosNumeros.Size = new System.Drawing.Size(54, 20);
            this.txtPrimerosNumeros.TabIndex = 11;
            // 
            // txtNumerosRestantes
            // 
            this.txtNumerosRestantes.Location = new System.Drawing.Point(151, 19);
            this.txtNumerosRestantes.Name = "txtNumerosRestantes";
            this.txtNumerosRestantes.Size = new System.Drawing.Size(126, 20);
            this.txtNumerosRestantes.TabIndex = 1;
            this.txtNumerosRestantes.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            this.txtNumerosRestantes.MaskInputRejected += new System.Windows.Forms.MaskInputRejectedEventHandler(this.txtNumerosRestantes_MaskInputRejected);
            this.txtNumerosRestantes.TextChanged += new System.EventHandler(this.txtNumerosRestantes_TextChanged);
            // 
            // lblNumeroTarjeta
            // 
            this.lblNumeroTarjeta.AutoSize = true;
            this.lblNumeroTarjeta.Location = new System.Drawing.Point(11, 22);
            this.lblNumeroTarjeta.Name = "lblNumeroTarjeta";
            this.lblNumeroTarjeta.Size = new System.Drawing.Size(74, 13);
            this.lblNumeroTarjeta.TabIndex = 9;
            this.lblNumeroTarjeta.Text = "Número..........";
            // 
            // grpAcciones
            // 
            this.grpAcciones.Controls.Add(this.btnCancelar);
            this.grpAcciones.Controls.Add(this.btnAceptar);
            this.grpAcciones.Location = new System.Drawing.Point(19, 143);
            this.grpAcciones.Name = "grpAcciones";
            this.grpAcciones.Size = new System.Drawing.Size(282, 45);
            this.grpAcciones.TabIndex = 1;
            this.grpAcciones.TabStop = false;
            this.grpAcciones.Text = "Acciones";
            this.grpAcciones.Enter += new System.EventHandler(this.grpAcciones_Enter);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(138, 16);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 5;
            this.btnCancelar.Text = "&Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnAceptar
            // 
            this.btnAceptar.Location = new System.Drawing.Point(57, 16);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(75, 23);
            this.btnAceptar.TabIndex = 4;
            this.btnAceptar.Text = "&Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // FrmDatosTarjetaDirecto
            // 
            this.AcceptButton = this.btnAceptar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(313, 200);
            this.ControlBox = false;
            this.Controls.Add(this.grpAcciones);
            this.Controls.Add(this.grpNumeros);
            this.Name = "FrmDatosTarjetaDirecto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ingrese numero de tarjeta";
            this.Load += new System.EventHandler(this.FrmNumerosRestantes_Load);
            this.grpNumeros.ResumeLayout(false);
            this.grpNumeros.PerformLayout();
            this.grpAcciones.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpNumeros;
        private System.Windows.Forms.MaskedTextBox txtPrimerosNumeros;
        private System.Windows.Forms.MaskedTextBox txtNumerosRestantes;
        private System.Windows.Forms.Label lblNumeroTarjeta;
        private System.Windows.Forms.GroupBox grpAcciones;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.MaskedTextBox txtVencimiento;
        private System.Windows.Forms.Label lblVencimiento;
        private System.Windows.Forms.Label lblCodSeguridad;
        private System.Windows.Forms.MaskedTextBox txtCodSeguridad;
        private System.Windows.Forms.MaskedTextBox txtNumeroTarjeta;
        private System.Windows.Forms.Button btnCancelar;
    }
}