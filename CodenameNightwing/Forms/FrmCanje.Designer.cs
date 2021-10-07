namespace CodenameNightwing.Forms
{
    partial class FrmCanje
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
            this.grpImporte = new System.Windows.Forms.GroupBox();
            this.lblImporte = new System.Windows.Forms.Label();
            this.grpTipoTarjeta = new System.Windows.Forms.GroupBox();
            this.rdbCredExt = new System.Windows.Forms.RadioButton();
            this.rdbCredArg = new System.Windows.Forms.RadioButton();
            this.rdbDebito = new System.Windows.Forms.RadioButton();
            this.grpTarjetasDebito = new System.Windows.Forms.GroupBox();
            this.grpAccionesCredito = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btnPlanes = new System.Windows.Forms.Button();
            this.btnVolverCred = new System.Windows.Forms.Button();
            this.grpAccionesDebito = new System.Windows.Forms.GroupBox();
            this.btnOtras = new System.Windows.Forms.Button();
            this.btnVolverDebito = new System.Windows.Forms.Button();
            this.btnAutorizar = new System.Windows.Forms.Button();
            this.pnlMensaje = new System.Windows.Forms.Panel();
            this.lblMensaje = new System.Windows.Forms.Label();
            this.lblPriceInformation = new System.Windows.Forms.Label();
            this.grpImporte.SuspendLayout();
            this.grpTipoTarjeta.SuspendLayout();
            this.grpAccionesCredito.SuspendLayout();
            this.grpAccionesDebito.SuspendLayout();
            this.pnlMensaje.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpImporte
            // 
            this.grpImporte.Controls.Add(this.pnlMensaje);
            this.grpImporte.Controls.Add(this.lblPriceInformation);
            this.grpImporte.Controls.Add(this.lblImporte);
            this.grpImporte.Controls.Add(this.grpTipoTarjeta);
            this.grpImporte.Location = new System.Drawing.Point(4, 5);
            this.grpImporte.Name = "grpImporte";
            this.grpImporte.Size = new System.Drawing.Size(361, 87);
            this.grpImporte.TabIndex = 0;
            this.grpImporte.TabStop = false;
            this.grpImporte.Text = "Importe canje";
            // 
            // lblImporte
            // 
            this.lblImporte.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblImporte.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblImporte.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblImporte.Location = new System.Drawing.Point(8, 25);
            this.lblImporte.Name = "lblImporte";
            this.lblImporte.Size = new System.Drawing.Size(177, 20);
            this.lblImporte.TabIndex = 11;
            this.lblImporte.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // grpTipoTarjeta
            // 
            this.grpTipoTarjeta.Controls.Add(this.rdbCredExt);
            this.grpTipoTarjeta.Controls.Add(this.rdbCredArg);
            this.grpTipoTarjeta.Controls.Add(this.rdbDebito);
            this.grpTipoTarjeta.Location = new System.Drawing.Point(238, 0);
            this.grpTipoTarjeta.Name = "grpTipoTarjeta";
            this.grpTipoTarjeta.Size = new System.Drawing.Size(123, 87);
            this.grpTipoTarjeta.TabIndex = 1;
            this.grpTipoTarjeta.TabStop = false;
            this.grpTipoTarjeta.Text = "Tipo de tarjeta";
            // 
            // rdbCredExt
            // 
            this.rdbCredExt.AutoSize = true;
            this.rdbCredExt.Location = new System.Drawing.Point(6, 65);
            this.rdbCredExt.Name = "rdbCredExt";
            this.rdbCredExt.Size = new System.Drawing.Size(96, 17);
            this.rdbCredExt.TabIndex = 2;
            this.rdbCredExt.Text = "Crédito &Extranj.";
            this.rdbCredExt.UseVisualStyleBackColor = true;
            this.rdbCredExt.CheckedChanged += new System.EventHandler(this.rdbCredExt_CheckedChanged);
            // 
            // rdbCredArg
            // 
            this.rdbCredArg.AutoSize = true;
            this.rdbCredArg.Location = new System.Drawing.Point(6, 42);
            this.rdbCredArg.Name = "rdbCredArg";
            this.rdbCredArg.Size = new System.Drawing.Size(95, 17);
            this.rdbCredArg.TabIndex = 1;
            this.rdbCredArg.Text = "&Crédito Argent.";
            this.rdbCredArg.UseVisualStyleBackColor = true;
            this.rdbCredArg.CheckedChanged += new System.EventHandler(this.rdbCredArg_CheckedChanged);
            // 
            // rdbDebito
            // 
            this.rdbDebito.AutoSize = true;
            this.rdbDebito.Location = new System.Drawing.Point(6, 19);
            this.rdbDebito.Name = "rdbDebito";
            this.rdbDebito.Size = new System.Drawing.Size(56, 17);
            this.rdbDebito.TabIndex = 0;
            this.rdbDebito.Text = "&Débito";
            this.rdbDebito.UseVisualStyleBackColor = true;
            this.rdbDebito.CheckedChanged += new System.EventHandler(this.rdbDebito_CheckedChanged);
            // 
            // grpTarjetasDebito
            // 
            this.grpTarjetasDebito.Location = new System.Drawing.Point(4, 98);
            this.grpTarjetasDebito.Name = "grpTarjetasDebito";
            this.grpTarjetasDebito.Size = new System.Drawing.Size(347, 51);
            this.grpTarjetasDebito.TabIndex = 2;
            this.grpTarjetasDebito.TabStop = false;
            this.grpTarjetasDebito.Text = "Tarjeta";
            this.grpTarjetasDebito.Enter += new System.EventHandler(this.grpTarjetasDebito_Enter);
            // 
            // grpAccionesCredito
            // 
            this.grpAccionesCredito.Controls.Add(this.button1);
            this.grpAccionesCredito.Controls.Add(this.btnPlanes);
            this.grpAccionesCredito.Controls.Add(this.btnVolverCred);
            this.grpAccionesCredito.Location = new System.Drawing.Point(365, 5);
            this.grpAccionesCredito.Name = "grpAccionesCredito";
            this.grpAccionesCredito.Size = new System.Drawing.Size(276, 87);
            this.grpAccionesCredito.TabIndex = 3;
            this.grpAccionesCredito.TabStop = false;
            this.grpAccionesCredito.Text = "Acciones";
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(143, 25);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(95, 42);
            this.button1.TabIndex = 8;
            this.button1.Text = "Otras Operaciones";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnPlanes
            // 
            this.btnPlanes.Location = new System.Drawing.Point(6, 27);
            this.btnPlanes.Name = "btnPlanes";
            this.btnPlanes.Size = new System.Drawing.Size(66, 40);
            this.btnPlanes.TabIndex = 3;
            this.btnPlanes.Text = "&Planes";
            this.btnPlanes.UseVisualStyleBackColor = true;
            this.btnPlanes.Click += new System.EventHandler(this.btnPlanes_Click);
            // 
            // btnVolverCred
            // 
            this.btnVolverCred.Location = new System.Drawing.Point(78, 26);
            this.btnVolverCred.Name = "btnVolverCred";
            this.btnVolverCred.Size = new System.Drawing.Size(59, 40);
            this.btnVolverCred.TabIndex = 2;
            this.btnVolverCred.Text = "&Volver";
            this.btnVolverCred.UseVisualStyleBackColor = true;
            this.btnVolverCred.Click += new System.EventHandler(this.btnVolverCred_Click);
            // 
            // grpAccionesDebito
            // 
            this.grpAccionesDebito.Controls.Add(this.btnOtras);
            this.grpAccionesDebito.Controls.Add(this.btnVolverDebito);
            this.grpAccionesDebito.Controls.Add(this.btnAutorizar);
            this.grpAccionesDebito.Location = new System.Drawing.Point(4, 155);
            this.grpAccionesDebito.Name = "grpAccionesDebito";
            this.grpAccionesDebito.Size = new System.Drawing.Size(347, 80);
            this.grpAccionesDebito.TabIndex = 4;
            this.grpAccionesDebito.TabStop = false;
            this.grpAccionesDebito.Text = "Acciones";
            // 
            // btnOtras
            // 
            this.btnOtras.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnOtras.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOtras.Location = new System.Drawing.Point(143, 22);
            this.btnOtras.Name = "btnOtras";
            this.btnOtras.Size = new System.Drawing.Size(95, 40);
            this.btnOtras.TabIndex = 8;
            this.btnOtras.Text = "Otras Operaciones";
            this.btnOtras.UseVisualStyleBackColor = true;
            this.btnOtras.Click += new System.EventHandler(this.btnOtras_Click);
            // 
            // btnVolverDebito
            // 
            this.btnVolverDebito.Location = new System.Drawing.Point(79, 22);
            this.btnVolverDebito.Name = "btnVolverDebito";
            this.btnVolverDebito.Size = new System.Drawing.Size(58, 40);
            this.btnVolverDebito.TabIndex = 1;
            this.btnVolverDebito.Text = "&Volver";
            this.btnVolverDebito.UseVisualStyleBackColor = true;
            this.btnVolverDebito.Click += new System.EventHandler(this.btnVolverDebito_Click);
            // 
            // btnAutorizar
            // 
            this.btnAutorizar.Location = new System.Drawing.Point(8, 22);
            this.btnAutorizar.Name = "btnAutorizar";
            this.btnAutorizar.Size = new System.Drawing.Size(65, 40);
            this.btnAutorizar.TabIndex = 0;
            this.btnAutorizar.Text = "&Autorizar";
            this.btnAutorizar.UseVisualStyleBackColor = true;
            this.btnAutorizar.Click += new System.EventHandler(this.btnAutorizar_Click);
            // 
            // pnlMensaje
            // 
            this.pnlMensaje.BackColor = System.Drawing.Color.Aqua;
            this.pnlMensaje.Controls.Add(this.lblMensaje);
            this.pnlMensaje.Location = new System.Drawing.Point(0, 25);
            this.pnlMensaje.Name = "pnlMensaje";
            this.pnlMensaje.Size = new System.Drawing.Size(348, 40);
            this.pnlMensaje.TabIndex = 1;
            this.pnlMensaje.Visible = false;
            // 
            // lblMensaje
            // 
            this.lblMensaje.AutoSize = true;
            this.lblMensaje.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMensaje.Location = new System.Drawing.Point(1, 9);
            this.lblMensaje.Name = "lblMensaje";
            this.lblMensaje.Size = new System.Drawing.Size(223, 24);
            this.lblMensaje.TabIndex = 0;
            this.lblMensaje.Text = "Obteniendo Información..";
            this.lblMensaje.Click += new System.EventHandler(this.lblMensaje_Click);
            // 
            // lblPriceInformation
            // 
            this.lblPriceInformation.AutoSize = true;
            this.lblPriceInformation.Location = new System.Drawing.Point(11, 59);
            this.lblPriceInformation.Name = "lblPriceInformation";
            this.lblPriceInformation.Size = new System.Drawing.Size(0, 13);
            this.lblPriceInformation.TabIndex = 12;
            // 
            // FrmCanje
            // 
            this.AcceptButton = this.btnAutorizar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(644, 240);
            this.ControlBox = false;
            this.Controls.Add(this.grpAccionesDebito);
            this.Controls.Add(this.grpAccionesCredito);
            this.Controls.Add(this.grpTarjetasDebito);
            this.Controls.Add(this.grpImporte);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.KeyPreview = true;
            this.Name = "FrmCanje";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Canjes - POS Integrado";
            this.Load += new System.EventHandler(this.FrmCanje_Load);
            this.grpImporte.ResumeLayout(false);
            this.grpImporte.PerformLayout();
            this.grpTipoTarjeta.ResumeLayout(false);
            this.grpTipoTarjeta.PerformLayout();
            this.grpAccionesCredito.ResumeLayout(false);
            this.grpAccionesDebito.ResumeLayout(false);
            this.pnlMensaje.ResumeLayout(false);
            this.pnlMensaje.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpImporte;
        private System.Windows.Forms.GroupBox grpTipoTarjeta;
        private System.Windows.Forms.RadioButton rdbCredExt;
        private System.Windows.Forms.RadioButton rdbCredArg;
        private System.Windows.Forms.RadioButton rdbDebito;
        private System.Windows.Forms.GroupBox grpTarjetasDebito;
        private System.Windows.Forms.GroupBox grpAccionesCredito;
        private System.Windows.Forms.Button btnPlanes;
        private System.Windows.Forms.Button btnVolverCred;
        private System.Windows.Forms.GroupBox grpAccionesDebito;
        private System.Windows.Forms.Button btnVolverDebito;
        private System.Windows.Forms.Button btnAutorizar;
        private System.Windows.Forms.Panel pnlMensaje;
        private System.Windows.Forms.Label lblMensaje;
        private System.Windows.Forms.Label lblImporte;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnOtras;
        private System.Windows.Forms.Label lblPriceInformation;
    }
}