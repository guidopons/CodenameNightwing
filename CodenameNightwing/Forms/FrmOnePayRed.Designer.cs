namespace CodenameNightwing.Forms
{
    partial class FrmOnePayRed
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtImporte = new System.Windows.Forms.Label();
            this.grpImporte = new System.Windows.Forms.GroupBox();
            this.grpAcciones = new System.Windows.Forms.GroupBox();
            this.btnOtras = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnAutorizar = new System.Windows.Forms.Button();
            this.pnlMensaje = new System.Windows.Forms.Panel();
            this.lblMensaje = new System.Windows.Forms.Label();
            this.lblPriceInformation = new System.Windows.Forms.Label();
            this.grpImporte.SuspendLayout();
            this.grpAcciones.SuspendLayout();
            this.pnlMensaje.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.label1.Location = new System.Drawing.Point(29, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Importe a autorizar:";
            // 
            // txtImporte
            // 
            this.txtImporte.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtImporte.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtImporte.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtImporte.Location = new System.Drawing.Point(184, 16);
            this.txtImporte.Name = "txtImporte";
            this.txtImporte.Size = new System.Drawing.Size(107, 23);
            this.txtImporte.TabIndex = 7;
            this.txtImporte.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // grpImporte
            // 
            this.grpImporte.Controls.Add(this.lblPriceInformation);
            this.grpImporte.Controls.Add(this.label1);
            this.grpImporte.Controls.Add(this.txtImporte);
            this.grpImporte.Location = new System.Drawing.Point(12, 0);
            this.grpImporte.Name = "grpImporte";
            this.grpImporte.Size = new System.Drawing.Size(321, 68);
            this.grpImporte.TabIndex = 8;
            this.grpImporte.TabStop = false;
            // 
            // grpAcciones
            // 
            this.grpAcciones.Controls.Add(this.btnOtras);
            this.grpAcciones.Controls.Add(this.btnCancelar);
            this.grpAcciones.Controls.Add(this.btnAutorizar);
            this.grpAcciones.Location = new System.Drawing.Point(12, 137);
            this.grpAcciones.Name = "grpAcciones";
            this.grpAcciones.Size = new System.Drawing.Size(348, 65);
            this.grpAcciones.TabIndex = 9;
            this.grpAcciones.TabStop = false;
            this.grpAcciones.Text = "Acciones";
            // 
            // btnOtras
            // 
            this.btnOtras.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnOtras.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOtras.Location = new System.Drawing.Point(240, 17);
            this.btnOtras.Name = "btnOtras";
            this.btnOtras.Size = new System.Drawing.Size(95, 42);
            this.btnOtras.TabIndex = 7;
            this.btnOtras.Text = "O&tras Operaciones";
            this.btnOtras.UseVisualStyleBackColor = true;
            this.btnOtras.Click += new System.EventHandler(this.btnOtras_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(126, 17);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(107, 42);
            this.btnCancelar.TabIndex = 1;
            this.btnCancelar.Text = "&Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnAutorizar
            // 
            this.btnAutorizar.Location = new System.Drawing.Point(13, 17);
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
            this.pnlMensaje.Controls.Add(this.lblMensaje);
            this.pnlMensaje.Location = new System.Drawing.Point(7, 54);
            this.pnlMensaje.Name = "pnlMensaje";
            this.pnlMensaje.Size = new System.Drawing.Size(342, 77);
            this.pnlMensaje.TabIndex = 10;
            this.pnlMensaje.Visible = false;
            // 
            // lblMensaje
            // 
            this.lblMensaje.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMensaje.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMensaje.Location = new System.Drawing.Point(-5, 0);
            this.lblMensaje.Name = "lblMensaje";
            this.lblMensaje.Size = new System.Drawing.Size(342, 77);
            this.lblMensaje.TabIndex = 3;
            this.lblMensaje.Text = "Obteniendo Información...";
            this.lblMensaje.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblMensaje.Click += new System.EventHandler(this.lblMensaje_Click);
            // 
            // lblPriceInformation
            // 
            this.lblPriceInformation.AutoSize = true;
            this.lblPriceInformation.Location = new System.Drawing.Point(29, 41);
            this.lblPriceInformation.Name = "lblPriceInformation";
            this.lblPriceInformation.Size = new System.Drawing.Size(35, 13);
            this.lblPriceInformation.TabIndex = 8;
            this.lblPriceInformation.Text = "label2";
            // 
            // FrmOnePayRed
            // 
            this.AcceptButton = this.btnAutorizar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(372, 214);
            this.ControlBox = false;
            this.Controls.Add(this.pnlMensaje);
            this.Controls.Add(this.grpAcciones);
            this.Controls.Add(this.grpImporte);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "FrmOnePayRed";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cobros - Cajas";
            this.Load += new System.EventHandler(this.FrmOnePayRed_Load);
            this.grpImporte.ResumeLayout(false);
            this.grpImporte.PerformLayout();
            this.grpAcciones.ResumeLayout(false);
            this.pnlMensaje.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label txtImporte;
        private System.Windows.Forms.GroupBox grpImporte;
        private System.Windows.Forms.GroupBox grpAcciones;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnAutorizar;
        private System.Windows.Forms.Panel pnlMensaje;
        private System.Windows.Forms.Label lblMensaje;
        private System.Windows.Forms.Button btnOtras;
        private System.Windows.Forms.Label lblPriceInformation;
    }
}