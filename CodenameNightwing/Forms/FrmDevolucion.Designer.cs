namespace CodenameNightwing.Forms
{
    partial class FrmDevolucion
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
            this.pnlMensaje = new System.Windows.Forms.Panel();
            this.lblMensaje = new System.Windows.Forms.Label();
            this.txtImporte = new System.Windows.Forms.Label();
            this.txtNumeroTarjeta = new System.Windows.Forms.Label();
            this.txtTipoTarjeta = new System.Windows.Forms.Label();
            this.txtNumeroCupon = new System.Windows.Forms.Label();
            this.txtFecha = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTipoTarjeta = new System.Windows.Forms.Label();
            this.lblNumeroCupon = new System.Windows.Forms.Label();
            this.lblFecha = new System.Windows.Forms.Label();
            this.grpAcciones = new System.Windows.Forms.GroupBox();
            this.btnVolver = new System.Windows.Forms.Button();
            this.btnAutorizar = new System.Windows.Forms.Button();
            this.lblPriceInformation = new System.Windows.Forms.Label();
            this.grpDatosTarjeta.SuspendLayout();
            this.pnlMensaje.SuspendLayout();
            this.grpAcciones.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpDatosTarjeta
            // 
            this.grpDatosTarjeta.Controls.Add(this.lblPriceInformation);
            this.grpDatosTarjeta.Controls.Add(this.txtImporte);
            this.grpDatosTarjeta.Controls.Add(this.txtNumeroTarjeta);
            this.grpDatosTarjeta.Controls.Add(this.txtTipoTarjeta);
            this.grpDatosTarjeta.Controls.Add(this.txtNumeroCupon);
            this.grpDatosTarjeta.Controls.Add(this.txtFecha);
            this.grpDatosTarjeta.Controls.Add(this.label2);
            this.grpDatosTarjeta.Controls.Add(this.label1);
            this.grpDatosTarjeta.Controls.Add(this.lblTipoTarjeta);
            this.grpDatosTarjeta.Controls.Add(this.lblNumeroCupon);
            this.grpDatosTarjeta.Controls.Add(this.lblFecha);
            this.grpDatosTarjeta.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpDatosTarjeta.Location = new System.Drawing.Point(8, 10);
            this.grpDatosTarjeta.Name = "grpDatosTarjeta";
            this.grpDatosTarjeta.Size = new System.Drawing.Size(397, 237);
            this.grpDatosTarjeta.TabIndex = 0;
            this.grpDatosTarjeta.TabStop = false;
            this.grpDatosTarjeta.Text = "Datos de la tarjeta y cupón";
            // 
            // pnlMensaje
            // 
            this.pnlMensaje.BackColor = System.Drawing.Color.Aqua;
            this.pnlMensaje.Controls.Add(this.lblMensaje);
            this.pnlMensaje.Location = new System.Drawing.Point(8, 30);
            this.pnlMensaje.Name = "pnlMensaje";
            this.pnlMensaje.Size = new System.Drawing.Size(396, 180);
            this.pnlMensaje.TabIndex = 2;
            this.pnlMensaje.Visible = false;
            // 
            // lblMensaje
            // 
            this.lblMensaje.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMensaje.Location = new System.Drawing.Point(40, 64);
            this.lblMensaje.Name = "lblMensaje";
            this.lblMensaje.Size = new System.Drawing.Size(307, 49);
            this.lblMensaje.TabIndex = 0;
            this.lblMensaje.Text = "Obteniendo Información...";
            this.lblMensaje.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtImporte
            // 
            this.txtImporte.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtImporte.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtImporte.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtImporte.Location = new System.Drawing.Point(234, 172);
            this.txtImporte.Name = "txtImporte";
            this.txtImporte.Size = new System.Drawing.Size(118, 20);
            this.txtImporte.TabIndex = 14;
            // 
            // txtNumeroTarjeta
            // 
            this.txtNumeroTarjeta.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtNumeroTarjeta.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtNumeroTarjeta.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumeroTarjeta.Location = new System.Drawing.Point(234, 135);
            this.txtNumeroTarjeta.Name = "txtNumeroTarjeta";
            this.txtNumeroTarjeta.Size = new System.Drawing.Size(152, 20);
            this.txtNumeroTarjeta.TabIndex = 13;
            // 
            // txtTipoTarjeta
            // 
            this.txtTipoTarjeta.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtTipoTarjeta.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtTipoTarjeta.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTipoTarjeta.Location = new System.Drawing.Point(234, 97);
            this.txtTipoTarjeta.Name = "txtTipoTarjeta";
            this.txtTipoTarjeta.Size = new System.Drawing.Size(152, 20);
            this.txtTipoTarjeta.TabIndex = 12;
            // 
            // txtNumeroCupon
            // 
            this.txtNumeroCupon.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtNumeroCupon.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtNumeroCupon.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumeroCupon.Location = new System.Drawing.Point(234, 63);
            this.txtNumeroCupon.Name = "txtNumeroCupon";
            this.txtNumeroCupon.Size = new System.Drawing.Size(118, 20);
            this.txtNumeroCupon.TabIndex = 11;
            // 
            // txtFecha
            // 
            this.txtFecha.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtFecha.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtFecha.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFecha.Location = new System.Drawing.Point(234, 28);
            this.txtFecha.Name = "txtFecha";
            this.txtFecha.Size = new System.Drawing.Size(118, 20);
            this.txtFecha.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 172);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(201, 18);
            this.label2.TabIndex = 4;
            this.label2.Text = "Importe devolución.................";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 137);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(202, 18);
            this.label1.TabIndex = 3;
            this.label1.Text = "Número de tarjeta...................";
            // 
            // lblTipoTarjeta
            // 
            this.lblTipoTarjeta.AutoSize = true;
            this.lblTipoTarjeta.Location = new System.Drawing.Point(6, 99);
            this.lblTipoTarjeta.Name = "lblTipoTarjeta";
            this.lblTipoTarjeta.Size = new System.Drawing.Size(201, 18);
            this.lblTipoTarjeta.TabIndex = 2;
            this.lblTipoTarjeta.Text = "Tipo tarjeta .............................";
            // 
            // lblNumeroCupon
            // 
            this.lblNumeroCupon.AutoSize = true;
            this.lblNumeroCupon.Location = new System.Drawing.Point(6, 65);
            this.lblNumeroCupon.Name = "lblNumeroCupon";
            this.lblNumeroCupon.Size = new System.Drawing.Size(201, 18);
            this.lblNumeroCupon.TabIndex = 1;
            this.lblNumeroCupon.Text = "Número original del cupón.....";
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.Location = new System.Drawing.Point(6, 30);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(200, 18);
            this.lblFecha.TabIndex = 0;
            this.lblFecha.Text = "Fecha original del cupón........";
            // 
            // grpAcciones
            // 
            this.grpAcciones.Controls.Add(this.btnVolver);
            this.grpAcciones.Controls.Add(this.btnAutorizar);
            this.grpAcciones.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpAcciones.Location = new System.Drawing.Point(8, 253);
            this.grpAcciones.Name = "grpAcciones";
            this.grpAcciones.Size = new System.Drawing.Size(397, 97);
            this.grpAcciones.TabIndex = 1;
            this.grpAcciones.TabStop = false;
            this.grpAcciones.Text = "Acciones";
            // 
            // btnVolver
            // 
            this.btnVolver.Location = new System.Drawing.Point(210, 27);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(156, 44);
            this.btnVolver.TabIndex = 1;
            this.btnVolver.Text = "&Volver sin aut.";
            this.btnVolver.UseVisualStyleBackColor = true;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // btnAutorizar
            // 
            this.btnAutorizar.Location = new System.Drawing.Point(24, 27);
            this.btnAutorizar.Name = "btnAutorizar";
            this.btnAutorizar.Size = new System.Drawing.Size(156, 44);
            this.btnAutorizar.TabIndex = 0;
            this.btnAutorizar.Text = "&Autorizar";
            this.btnAutorizar.UseVisualStyleBackColor = true;
            this.btnAutorizar.Click += new System.EventHandler(this.btnAutorizar_Click);
            // 
            // lblPriceInformation
            // 
            this.lblPriceInformation.AutoSize = true;
            this.lblPriceInformation.Location = new System.Drawing.Point(6, 203);
            this.lblPriceInformation.Name = "lblPriceInformation";
            this.lblPriceInformation.Size = new System.Drawing.Size(0, 18);
            this.lblPriceInformation.TabIndex = 15;
            // 
            // FrmDevolucion
            // 
            this.AcceptButton = this.btnAutorizar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(412, 362);
            this.ControlBox = false;
            this.Controls.Add(this.grpAcciones);
            this.Controls.Add(this.pnlMensaje);
            this.Controls.Add(this.grpDatosTarjeta);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.KeyPreview = true;
            this.Name = "FrmDevolucion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Devolución - POS Integrado";
            this.Load += new System.EventHandler(this.FrmDevolucion_Load);
            this.grpDatosTarjeta.ResumeLayout(false);
            this.grpDatosTarjeta.PerformLayout();
            this.pnlMensaje.ResumeLayout(false);
            this.grpAcciones.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpDatosTarjeta;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTipoTarjeta;
        private System.Windows.Forms.Label lblNumeroCupon;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox grpAcciones;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.Button btnAutorizar;
        private System.Windows.Forms.Panel pnlMensaje;
        private System.Windows.Forms.Label lblMensaje;
        private System.Windows.Forms.Label txtTipoTarjeta;
        private System.Windows.Forms.Label txtNumeroCupon;
        private System.Windows.Forms.Label txtFecha;
        private System.Windows.Forms.Label txtImporte;
        private System.Windows.Forms.Label txtNumeroTarjeta;
        private System.Windows.Forms.Label lblPriceInformation;
    }
}