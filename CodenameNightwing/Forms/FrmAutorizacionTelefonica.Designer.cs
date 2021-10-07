namespace CodenameNightwing.Forms
{
    partial class FrmAutorizacionTelefonica
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAutorizacionTelefonica));
            this.pnlMensaje = new System.Windows.Forms.Panel();
            this.lblMensaje = new System.Windows.Forms.Label();
            this.lblMensajeInicial = new System.Windows.Forms.Label();
            this.lblCodigoComercio = new System.Windows.Forms.Label();
            this.lblImporteAAutorizar = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCodigoComercio = new System.Windows.Forms.Label();
            this.txtImporteAAutorizar = new System.Windows.Forms.Label();
            this.txtCuotas = new System.Windows.Forms.Label();
            this.lblMensajeFinal = new System.Windows.Forms.Label();
            this.grpAcciones = new System.Windows.Forms.GroupBox();
            this.btnVolver = new System.Windows.Forms.Button();
            this.btnAutorizarOnline = new System.Windows.Forms.Button();
            this.btnAutorizarOffline = new System.Windows.Forms.Button();
            this.lblPOS = new System.Windows.Forms.Label();
            this.pnlMensajePOS = new System.Windows.Forms.Panel();
            this.pnlMensaje.SuspendLayout();
            this.grpAcciones.SuspendLayout();
            this.pnlMensajePOS.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMensaje
            // 
            this.pnlMensaje.BackColor = System.Drawing.Color.Yellow;
            this.pnlMensaje.Controls.Add(this.lblMensaje);
            this.pnlMensaje.Location = new System.Drawing.Point(7, 9);
            this.pnlMensaje.Name = "pnlMensaje";
            this.pnlMensaje.Size = new System.Drawing.Size(683, 31);
            this.pnlMensaje.TabIndex = 0;
            // 
            // lblMensaje
            // 
            this.lblMensaje.AutoSize = true;
            this.lblMensaje.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMensaje.Location = new System.Drawing.Point(15, 5);
            this.lblMensaje.Name = "lblMensaje";
            this.lblMensaje.Size = new System.Drawing.Size(464, 20);
            this.lblMensaje.TabIndex = 0;
            this.lblMensaje.Text = "LA TARJETA SOLICITA PEDIR AUTORIZACIÓN TELEFÓNICA";
            // 
            // lblMensajeInicial
            // 
            this.lblMensajeInicial.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblMensajeInicial.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMensajeInicial.Location = new System.Drawing.Point(6, 51);
            this.lblMensajeInicial.Name = "lblMensajeInicial";
            this.lblMensajeInicial.Size = new System.Drawing.Size(683, 79);
            this.lblMensajeInicial.TabIndex = 1;
            this.lblMensajeInicial.Text = "* Si va a solicitar la autorización telefónica realice lo siguiente:\r\n        No " +
    "cierre esta ventana, llame a la tarjeta correspondiente y solicite autorización";
            this.lblMensajeInicial.Click += new System.EventHandler(this.lblMensajeInicial_Click);
            // 
            // lblCodigoComercio
            // 
            this.lblCodigoComercio.AutoSize = true;
            this.lblCodigoComercio.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCodigoComercio.Location = new System.Drawing.Point(12, 134);
            this.lblCodigoComercio.Name = "lblCodigoComercio";
            this.lblCodigoComercio.Size = new System.Drawing.Size(175, 16);
            this.lblCodigoComercio.TabIndex = 2;
            this.lblCodigoComercio.Text = "Código de comercio...............";
            // 
            // lblImporteAAutorizar
            // 
            this.lblImporteAAutorizar.AutoSize = true;
            this.lblImporteAAutorizar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblImporteAAutorizar.Location = new System.Drawing.Point(12, 160);
            this.lblImporteAAutorizar.Name = "lblImporteAAutorizar";
            this.lblImporteAAutorizar.Size = new System.Drawing.Size(175, 16);
            this.lblImporteAAutorizar.TabIndex = 3;
            this.lblImporteAAutorizar.Text = "Importe a autorizar...................";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 187);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(176, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Cuotas..........................................";
            // 
            // txtCodigoComercio
            // 
            this.txtCodigoComercio.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtCodigoComercio.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtCodigoComercio.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodigoComercio.Location = new System.Drawing.Point(206, 130);
            this.txtCodigoComercio.Name = "txtCodigoComercio";
            this.txtCodigoComercio.Size = new System.Drawing.Size(118, 20);
            this.txtCodigoComercio.TabIndex = 5;
            this.txtCodigoComercio.Click += new System.EventHandler(this.txtCodigoComercio_Click);
            // 
            // txtImporteAAutorizar
            // 
            this.txtImporteAAutorizar.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtImporteAAutorizar.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtImporteAAutorizar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtImporteAAutorizar.Location = new System.Drawing.Point(206, 156);
            this.txtImporteAAutorizar.Name = "txtImporteAAutorizar";
            this.txtImporteAAutorizar.Size = new System.Drawing.Size(118, 20);
            this.txtImporteAAutorizar.TabIndex = 6;
            // 
            // txtCuotas
            // 
            this.txtCuotas.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtCuotas.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtCuotas.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCuotas.Location = new System.Drawing.Point(206, 183);
            this.txtCuotas.Name = "txtCuotas";
            this.txtCuotas.Size = new System.Drawing.Size(118, 20);
            this.txtCuotas.TabIndex = 7;
            // 
            // lblMensajeFinal
            // 
            this.lblMensajeFinal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblMensajeFinal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMensajeFinal.Location = new System.Drawing.Point(7, 208);
            this.lblMensajeFinal.Name = "lblMensajeFinal";
            this.lblMensajeFinal.Size = new System.Drawing.Size(683, 146);
            this.lblMensajeFinal.TabIndex = 8;
            this.lblMensajeFinal.Text = resources.GetString("lblMensajeFinal.Text");
            this.lblMensajeFinal.Click += new System.EventHandler(this.lblMensajeFinal_Click);
            // 
            // grpAcciones
            // 
            this.grpAcciones.Controls.Add(this.btnVolver);
            this.grpAcciones.Controls.Add(this.btnAutorizarOnline);
            this.grpAcciones.Controls.Add(this.btnAutorizarOffline);
            this.grpAcciones.Location = new System.Drawing.Point(7, 361);
            this.grpAcciones.Name = "grpAcciones";
            this.grpAcciones.Size = new System.Drawing.Size(682, 51);
            this.grpAcciones.TabIndex = 9;
            this.grpAcciones.TabStop = false;
            this.grpAcciones.Text = "Acciones";
            // 
            // btnVolver
            // 
            this.btnVolver.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVolver.Location = new System.Drawing.Point(472, 15);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(141, 30);
            this.btnVolver.TabIndex = 2;
            this.btnVolver.Text = "&Volver sin autorizar";
            this.btnVolver.UseVisualStyleBackColor = true;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // btnAutorizarOnline
            // 
            this.btnAutorizarOnline.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAutorizarOnline.Location = new System.Drawing.Point(255, 15);
            this.btnAutorizarOnline.Name = "btnAutorizarOnline";
            this.btnAutorizarOnline.Size = new System.Drawing.Size(141, 30);
            this.btnAutorizarOnline.TabIndex = 1;
            this.btnAutorizarOnline.Text = "Autorizar O&NLINE";
            this.btnAutorizarOnline.UseVisualStyleBackColor = true;
            this.btnAutorizarOnline.Click += new System.EventHandler(this.btnAutorizarOnline_Click);
            // 
            // btnAutorizarOffline
            // 
            this.btnAutorizarOffline.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAutorizarOffline.Location = new System.Drawing.Point(40, 15);
            this.btnAutorizarOffline.Name = "btnAutorizarOffline";
            this.btnAutorizarOffline.Size = new System.Drawing.Size(141, 30);
            this.btnAutorizarOffline.TabIndex = 0;
            this.btnAutorizarOffline.Text = "Autorizar O&FFLINE";
            this.btnAutorizarOffline.UseVisualStyleBackColor = true;
            this.btnAutorizarOffline.Click += new System.EventHandler(this.btnAutorizarOffline_Click);
            // 
            // lblPOS
            // 
            this.lblPOS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPOS.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPOS.Location = new System.Drawing.Point(96, 17);
            this.lblPOS.Name = "lblPOS";
            this.lblPOS.Size = new System.Drawing.Size(453, 40);
            this.lblPOS.TabIndex = 3;
            this.lblPOS.Text = "Obteniendo Información..";
            this.lblPOS.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlMensajePOS
            // 
            this.pnlMensajePOS.BackColor = System.Drawing.Color.Aqua;
            this.pnlMensajePOS.Controls.Add(this.lblPOS);
            this.pnlMensajePOS.Location = new System.Drawing.Point(6, 130);
            this.pnlMensajePOS.Name = "pnlMensajePOS";
            this.pnlMensajePOS.Size = new System.Drawing.Size(683, 75);
            this.pnlMensajePOS.TabIndex = 5;
            this.pnlMensajePOS.Visible = false;
            // 
            // FrmAutorizacionTelefonica
            // 
            this.AcceptButton = this.btnAutorizarOffline;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(696, 419);
            this.ControlBox = false;
            this.Controls.Add(this.pnlMensajePOS);
            this.Controls.Add(this.grpAcciones);
            this.Controls.Add(this.lblMensajeFinal);
            this.Controls.Add(this.txtCuotas);
            this.Controls.Add(this.txtImporteAAutorizar);
            this.Controls.Add(this.txtCodigoComercio);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblImporteAAutorizar);
            this.Controls.Add(this.lblCodigoComercio);
            this.Controls.Add(this.lblMensajeInicial);
            this.Controls.Add(this.pnlMensaje);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.KeyPreview = true;
            this.Name = "FrmAutorizacionTelefonica";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Autorizacion telefonica";
            this.Load += new System.EventHandler(this.FrmAutorizacionTelefonica_Load);
            this.pnlMensaje.ResumeLayout(false);
            this.pnlMensaje.PerformLayout();
            this.grpAcciones.ResumeLayout(false);
            this.pnlMensajePOS.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlMensaje;
        private System.Windows.Forms.Label lblMensaje;
        private System.Windows.Forms.Label lblMensajeInicial;
        private System.Windows.Forms.Label lblCodigoComercio;
        private System.Windows.Forms.Label lblImporteAAutorizar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label txtCodigoComercio;
        private System.Windows.Forms.Label txtImporteAAutorizar;
        private System.Windows.Forms.Label txtCuotas;
        private System.Windows.Forms.Label lblMensajeFinal;
        private System.Windows.Forms.GroupBox grpAcciones;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.Button btnAutorizarOnline;
        private System.Windows.Forms.Button btnAutorizarOffline;
        private System.Windows.Forms.Label lblPOS;
        private System.Windows.Forms.Panel pnlMensajePOS;
    }
}