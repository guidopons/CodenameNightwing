namespace CodenameNightwing.Forms
{
    partial class FrmCajeros
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
            this.grpCredito = new System.Windows.Forms.GroupBox();
            this.grpDebito = new System.Windows.Forms.GroupBox();
            this.grpImportes = new System.Windows.Forms.GroupBox();
            this.txtImporte = new System.Windows.Forms.Label();
            this.txtTarjeta = new System.Windows.Forms.Label();
            this.txtTipo = new System.Windows.Forms.Label();
            this.lblTarjeta = new System.Windows.Forms.Label();
            this.lblTipo = new System.Windows.Forms.Label();
            this.lblImporteAAutorizar = new System.Windows.Forms.Label();
            this.grpAcciones = new System.Windows.Forms.GroupBox();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnAutorizar = new System.Windows.Forms.Button();
            this.pnlMensaje = new System.Windows.Forms.Panel();
            this.txtMensaje = new System.Windows.Forms.Label();
            this.lblPriceInformation = new System.Windows.Forms.Label();
            this.grpImportes.SuspendLayout();
            this.grpAcciones.SuspendLayout();
            this.pnlMensaje.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpCredito
            // 
            this.grpCredito.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpCredito.Location = new System.Drawing.Point(12, 12);
            this.grpCredito.Name = "grpCredito";
            this.grpCredito.Size = new System.Drawing.Size(322, 367);
            this.grpCredito.TabIndex = 0;
            this.grpCredito.TabStop = false;
            this.grpCredito.Text = "Tarjetas de crédito";
            // 
            // grpDebito
            // 
            this.grpDebito.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpDebito.Location = new System.Drawing.Point(340, 12);
            this.grpDebito.Name = "grpDebito";
            this.grpDebito.Size = new System.Drawing.Size(322, 367);
            this.grpDebito.TabIndex = 1;
            this.grpDebito.TabStop = false;
            this.grpDebito.Text = "Tarjetas de débito";
            this.grpDebito.Enter += new System.EventHandler(this.grpDebito_Enter);
            // 
            // grpImportes
            // 
            this.grpImportes.Controls.Add(this.lblPriceInformation);
            this.grpImportes.Controls.Add(this.txtImporte);
            this.grpImportes.Controls.Add(this.txtTarjeta);
            this.grpImportes.Controls.Add(this.txtTipo);
            this.grpImportes.Controls.Add(this.lblTarjeta);
            this.grpImportes.Controls.Add(this.lblTipo);
            this.grpImportes.Controls.Add(this.lblImporteAAutorizar);
            this.grpImportes.Location = new System.Drawing.Point(12, 387);
            this.grpImportes.Name = "grpImportes";
            this.grpImportes.Size = new System.Drawing.Size(321, 88);
            this.grpImportes.TabIndex = 2;
            this.grpImportes.TabStop = false;
            this.grpImportes.Text = "Importes";
            // 
            // txtImporte
            // 
            this.txtImporte.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtImporte.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtImporte.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtImporte.Location = new System.Drawing.Point(77, 22);
            this.txtImporte.Name = "txtImporte";
            this.txtImporte.Size = new System.Drawing.Size(84, 23);
            this.txtImporte.TabIndex = 6;
            this.txtImporte.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtTarjeta
            // 
            this.txtTarjeta.AutoSize = true;
            this.txtTarjeta.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTarjeta.Location = new System.Drawing.Point(220, 36);
            this.txtTarjeta.Name = "txtTarjeta";
            this.txtTarjeta.Size = new System.Drawing.Size(0, 15);
            this.txtTarjeta.TabIndex = 5;
            // 
            // txtTipo
            // 
            this.txtTipo.AutoSize = true;
            this.txtTipo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTipo.Location = new System.Drawing.Point(220, 16);
            this.txtTipo.Name = "txtTipo";
            this.txtTipo.Size = new System.Drawing.Size(0, 15);
            this.txtTipo.TabIndex = 4;
            // 
            // lblTarjeta
            // 
            this.lblTarjeta.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTarjeta.Location = new System.Drawing.Point(167, 36);
            this.lblTarjeta.Name = "lblTarjeta";
            this.lblTarjeta.Size = new System.Drawing.Size(47, 17);
            this.lblTarjeta.TabIndex = 3;
            this.lblTarjeta.Text = "Tarjeta";
            // 
            // lblTipo
            // 
            this.lblTipo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTipo.Location = new System.Drawing.Point(167, 16);
            this.lblTipo.Name = "lblTipo";
            this.lblTipo.Size = new System.Drawing.Size(47, 17);
            this.lblTipo.TabIndex = 2;
            this.lblTipo.Text = "Tipo";
            // 
            // lblImporteAAutorizar
            // 
            this.lblImporteAAutorizar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblImporteAAutorizar.Location = new System.Drawing.Point(5, 20);
            this.lblImporteAAutorizar.Name = "lblImporteAAutorizar";
            this.lblImporteAAutorizar.Size = new System.Drawing.Size(66, 33);
            this.lblImporteAAutorizar.TabIndex = 0;
            this.lblImporteAAutorizar.Text = "Importe a autorizar";
            // 
            // grpAcciones
            // 
            this.grpAcciones.Controls.Add(this.btnCancelar);
            this.grpAcciones.Controls.Add(this.btnAutorizar);
            this.grpAcciones.Location = new System.Drawing.Point(341, 387);
            this.grpAcciones.Name = "grpAcciones";
            this.grpAcciones.Size = new System.Drawing.Size(321, 88);
            this.grpAcciones.TabIndex = 3;
            this.grpAcciones.TabStop = false;
            this.grpAcciones.Text = "Acciones";
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(175, 27);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(107, 42);
            this.btnCancelar.TabIndex = 1;
            this.btnCancelar.Text = "&Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnAutorizar
            // 
            this.btnAutorizar.Location = new System.Drawing.Point(32, 27);
            this.btnAutorizar.Name = "btnAutorizar";
            this.btnAutorizar.Size = new System.Drawing.Size(107, 42);
            this.btnAutorizar.TabIndex = 0;
            this.btnAutorizar.Text = "&Autorizar";
            this.btnAutorizar.UseVisualStyleBackColor = true;
            this.btnAutorizar.Click += new System.EventHandler(this.btnAutorizar_Click);
            // 
            // pnlMensaje
            // 
            this.pnlMensaje.BackColor = System.Drawing.Color.Aqua;
            this.pnlMensaje.Controls.Add(this.txtMensaje);
            this.pnlMensaje.Location = new System.Drawing.Point(12, 158);
            this.pnlMensaje.Name = "pnlMensaje";
            this.pnlMensaje.Size = new System.Drawing.Size(650, 60);
            this.pnlMensaje.TabIndex = 4;
            this.pnlMensaje.Visible = false;
            // 
            // txtMensaje
            // 
            this.txtMensaje.AutoSize = true;
            this.txtMensaje.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMensaje.Location = new System.Drawing.Point(194, 21);
            this.txtMensaje.Name = "txtMensaje";
            this.txtMensaje.Size = new System.Drawing.Size(192, 20);
            this.txtMensaje.TabIndex = 0;
            this.txtMensaje.Text = "Obteniendo Información...";
            // 
            // lblPriceInformation
            // 
            this.lblPriceInformation.AutoSize = true;
            this.lblPriceInformation.Location = new System.Drawing.Point(15, 63);
            this.lblPriceInformation.Name = "lblPriceInformation";
            this.lblPriceInformation.Size = new System.Drawing.Size(0, 13);
            this.lblPriceInformation.TabIndex = 7;
            // 
            // FrmCajeros
            // 
            this.AcceptButton = this.btnAutorizar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(673, 487);
            this.ControlBox = false;
            this.Controls.Add(this.pnlMensaje);
            this.Controls.Add(this.grpAcciones);
            this.Controls.Add(this.grpImportes);
            this.Controls.Add(this.grpDebito);
            this.Controls.Add(this.grpCredito);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.KeyPreview = true;
            this.Name = "FrmCajeros";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cobros - Cajas";
            this.Load += new System.EventHandler(this.FrmCajeros_Load);
            this.grpImportes.ResumeLayout(false);
            this.grpImportes.PerformLayout();
            this.grpAcciones.ResumeLayout(false);
            this.pnlMensaje.ResumeLayout(false);
            this.pnlMensaje.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpCredito;
        private System.Windows.Forms.GroupBox grpDebito;
        private System.Windows.Forms.GroupBox grpImportes;
        private System.Windows.Forms.GroupBox grpAcciones;
        private System.Windows.Forms.Label txtTarjeta;
        private System.Windows.Forms.Label txtTipo;
        private System.Windows.Forms.Label lblTarjeta;
        private System.Windows.Forms.Label lblTipo;
        private System.Windows.Forms.Label lblImporteAAutorizar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnAutorizar;
        private System.Windows.Forms.Panel pnlMensaje;
        private System.Windows.Forms.Label txtMensaje;
        private System.Windows.Forms.Label txtImporte;
        private System.Windows.Forms.Label lblPriceInformation;
    }
}