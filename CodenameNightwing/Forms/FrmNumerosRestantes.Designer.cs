namespace CodenameNightwing.Forms
{
    partial class FrmNumerosRestantes
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
            this.txtVencimiento = new System.Windows.Forms.MaskedTextBox();
            this.lblVencimiento = new System.Windows.Forms.Label();
            this.txtUltimosNumeros = new System.Windows.Forms.MaskedTextBox();
            this.txtPrimerosNumeros = new System.Windows.Forms.MaskedTextBox();
            this.txtNumerosRestantes = new System.Windows.Forms.MaskedTextBox();
            this.lblNumeroTarjeta = new System.Windows.Forms.Label();
            this.grpAcciones = new System.Windows.Forms.GroupBox();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.grpNumeros.SuspendLayout();
            this.grpAcciones.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpNumeros
            // 
            this.grpNumeros.Controls.Add(this.txtVencimiento);
            this.grpNumeros.Controls.Add(this.lblVencimiento);
            this.grpNumeros.Controls.Add(this.txtUltimosNumeros);
            this.grpNumeros.Controls.Add(this.txtPrimerosNumeros);
            this.grpNumeros.Controls.Add(this.txtNumerosRestantes);
            this.grpNumeros.Controls.Add(this.lblNumeroTarjeta);
            this.grpNumeros.Location = new System.Drawing.Point(12, 12);
            this.grpNumeros.Name = "grpNumeros";
            this.grpNumeros.Size = new System.Drawing.Size(283, 81);
            this.grpNumeros.TabIndex = 0;
            this.grpNumeros.TabStop = false;
            this.grpNumeros.Text = "Ingrese el numero de tarjeta";
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
            this.lblVencimiento.Location = new System.Drawing.Point(11, 53);
            this.lblVencimiento.Name = "lblVencimiento";
            this.lblVencimiento.Size = new System.Drawing.Size(74, 13);
            this.lblVencimiento.TabIndex = 13;
            this.lblVencimiento.Text = "Vencimiento...";
            // 
            // txtUltimosNumeros
            // 
            this.txtUltimosNumeros.BackColor = System.Drawing.Color.PaleTurquoise;
            this.txtUltimosNumeros.Enabled = false;
            this.txtUltimosNumeros.Location = new System.Drawing.Point(215, 19);
            this.txtUltimosNumeros.Name = "txtUltimosNumeros";
            this.txtUltimosNumeros.Size = new System.Drawing.Size(44, 20);
            this.txtUltimosNumeros.TabIndex = 12;
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
            this.txtNumerosRestantes.Size = new System.Drawing.Size(58, 20);
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
            this.grpAcciones.Controls.Add(this.btnAceptar);
            this.grpAcciones.Location = new System.Drawing.Point(13, 111);
            this.grpAcciones.Name = "grpAcciones";
            this.grpAcciones.Size = new System.Drawing.Size(282, 45);
            this.grpAcciones.TabIndex = 1;
            this.grpAcciones.TabStop = false;
            this.grpAcciones.Text = "Acciones";
            this.grpAcciones.Enter += new System.EventHandler(this.grpAcciones_Enter);
            // 
            // btnAceptar
            // 
            this.btnAceptar.Location = new System.Drawing.Point(109, 16);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(75, 23);
            this.btnAceptar.TabIndex = 3;
            this.btnAceptar.Text = "&Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // FrmNumerosRestantes
            // 
            this.AcceptButton = this.btnAceptar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(313, 177);
            this.ControlBox = false;
            this.Controls.Add(this.grpAcciones);
            this.Controls.Add(this.grpNumeros);
            this.Name = "FrmNumerosRestantes";
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
        private System.Windows.Forms.MaskedTextBox txtUltimosNumeros;
        private System.Windows.Forms.MaskedTextBox txtPrimerosNumeros;
        private System.Windows.Forms.MaskedTextBox txtNumerosRestantes;
        private System.Windows.Forms.Label lblNumeroTarjeta;
        private System.Windows.Forms.GroupBox grpAcciones;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.MaskedTextBox txtVencimiento;
        private System.Windows.Forms.Label lblVencimiento;
    }
}