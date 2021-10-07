namespace CodenameNightwing.Forms
{
    partial class FrmEnlace
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
            this.grpInfoPagos = new System.Windows.Forms.GroupBox();
            this.lblIntereses = new System.Windows.Forms.Label();
            this.lblImporteTotal = new System.Windows.Forms.Label();
            this.lblImporteCuotas = new System.Windows.Forms.Label();
            this.lblCuotas = new System.Windows.Forms.Label();
            this.lblTarjeta = new System.Windows.Forms.Label();
            this.grpAcciones = new System.Windows.Forms.GroupBox();
            this.btnVolver = new System.Windows.Forms.Button();
            this.btnCopyEnlace = new System.Windows.Forms.Button();
            this.btnVerificar = new System.Windows.Forms.Button();
            this.btnCopyText = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtEnlace = new System.Windows.Forms.TextBox();
            this.grpInfoPagos.SuspendLayout();
            this.grpAcciones.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpInfoPagos
            // 
            this.grpInfoPagos.Controls.Add(this.lblIntereses);
            this.grpInfoPagos.Controls.Add(this.lblImporteTotal);
            this.grpInfoPagos.Controls.Add(this.lblImporteCuotas);
            this.grpInfoPagos.Controls.Add(this.lblCuotas);
            this.grpInfoPagos.Controls.Add(this.lblTarjeta);
            this.grpInfoPagos.Location = new System.Drawing.Point(49, 79);
            this.grpInfoPagos.Name = "grpInfoPagos";
            this.grpInfoPagos.Size = new System.Drawing.Size(213, 166);
            this.grpInfoPagos.TabIndex = 0;
            this.grpInfoPagos.TabStop = false;
            this.grpInfoPagos.Text = "Forma de pago seleccionada:";
            // 
            // lblIntereses
            // 
            this.lblIntereses.AutoSize = true;
            this.lblIntereses.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblIntereses.Location = new System.Drawing.Point(23, 104);
            this.lblIntereses.Name = "lblIntereses";
            this.lblIntereses.Size = new System.Drawing.Size(53, 13);
            this.lblIntereses.TabIndex = 16;
            this.lblIntereses.Text = "Intereses:";
            // 
            // lblImporteTotal
            // 
            this.lblImporteTotal.AutoSize = true;
            this.lblImporteTotal.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblImporteTotal.Location = new System.Drawing.Point(23, 79);
            this.lblImporteTotal.Name = "lblImporteTotal";
            this.lblImporteTotal.Size = new System.Drawing.Size(75, 13);
            this.lblImporteTotal.TabIndex = 15;
            this.lblImporteTotal.Text = "Importe Total: ";
            // 
            // lblImporteCuotas
            // 
            this.lblImporteCuotas.AutoSize = true;
            this.lblImporteCuotas.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblImporteCuotas.Location = new System.Drawing.Point(23, 132);
            this.lblImporteCuotas.Name = "lblImporteCuotas";
            this.lblImporteCuotas.Size = new System.Drawing.Size(99, 13);
            this.lblImporteCuotas.TabIndex = 14;
            this.lblImporteCuotas.Text = "Importe por Cuotas:";
            // 
            // lblCuotas
            // 
            this.lblCuotas.AutoSize = true;
            this.lblCuotas.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblCuotas.Location = new System.Drawing.Point(23, 57);
            this.lblCuotas.Name = "lblCuotas";
            this.lblCuotas.Size = new System.Drawing.Size(46, 13);
            this.lblCuotas.TabIndex = 13;
            this.lblCuotas.Text = "Cuotas: ";
            // 
            // lblTarjeta
            // 
            this.lblTarjeta.AutoSize = true;
            this.lblTarjeta.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblTarjeta.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblTarjeta.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTarjeta.Location = new System.Drawing.Point(23, 33);
            this.lblTarjeta.Name = "lblTarjeta";
            this.lblTarjeta.Size = new System.Drawing.Size(43, 13);
            this.lblTarjeta.TabIndex = 9;
            this.lblTarjeta.Text = "Tarjeta:";
            // 
            // grpAcciones
            // 
            this.grpAcciones.Controls.Add(this.btnVolver);
            this.grpAcciones.Controls.Add(this.btnCopyEnlace);
            this.grpAcciones.Controls.Add(this.btnVerificar);
            this.grpAcciones.Controls.Add(this.btnCopyText);
            this.grpAcciones.Location = new System.Drawing.Point(47, 260);
            this.grpAcciones.Name = "grpAcciones";
            this.grpAcciones.Size = new System.Drawing.Size(215, 86);
            this.grpAcciones.TabIndex = 1;
            this.grpAcciones.TabStop = false;
            this.grpAcciones.Text = "Acciones";
            // 
            // btnVolver
            // 
            this.btnVolver.Location = new System.Drawing.Point(136, 48);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(71, 23);
            this.btnVolver.TabIndex = 4;
            this.btnVolver.Text = "&Cancelar";
            this.btnVolver.UseVisualStyleBackColor = true;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // btnCopyEnlace
            // 
            this.btnCopyEnlace.Location = new System.Drawing.Point(138, 19);
            this.btnCopyEnlace.Name = "btnCopyEnlace";
            this.btnCopyEnlace.Size = new System.Drawing.Size(71, 23);
            this.btnCopyEnlace.TabIndex = 2;
            this.btnCopyEnlace.Text = "Copiar&Enlace";
            this.btnCopyEnlace.UseVisualStyleBackColor = true;
            this.btnCopyEnlace.Click += new System.EventHandler(this.btnCopyEnlace_Click);
            // 
            // btnVerificar
            // 
            this.btnVerificar.Location = new System.Drawing.Point(18, 48);
            this.btnVerificar.Name = "btnVerificar";
            this.btnVerificar.Size = new System.Drawing.Size(71, 23);
            this.btnVerificar.TabIndex = 3;
            this.btnVerificar.Text = "&Verificar";
            this.btnVerificar.UseVisualStyleBackColor = true;
            this.btnVerificar.Click += new System.EventHandler(this.btnVerificar_Click);
            // 
            // btnCopyText
            // 
            this.btnCopyText.Location = new System.Drawing.Point(18, 19);
            this.btnCopyText.Name = "btnCopyText";
            this.btnCopyText.Size = new System.Drawing.Size(73, 23);
            this.btnCopyText.TabIndex = 1;
            this.btnCopyText.Text = "Copiar&Texto";
            this.btnCopyText.UseVisualStyleBackColor = true;
            this.btnCopyText.Click += new System.EventHandler(this.btnCopyText_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtEnlace);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(292, 52);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Enlace de pago:";
            // 
            // txtEnlace
            // 
            this.txtEnlace.Location = new System.Drawing.Point(7, 19);
            this.txtEnlace.Name = "txtEnlace";
            this.txtEnlace.ReadOnly = true;
            this.txtEnlace.Size = new System.Drawing.Size(278, 20);
            this.txtEnlace.TabIndex = 0;
            this.txtEnlace.Text = "https://www.aerolineas.com.ar/landingsespeciales/landings/730_procesando-pago";
            this.txtEnlace.TextChanged += new System.EventHandler(this.txtEnlace_TextChanged);
            // 
            // FrmEnlace
            // 
            this.AcceptButton = this.btnCopyText;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(309, 358);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grpAcciones);
            this.Controls.Add(this.grpInfoPagos);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "FrmEnlace";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Generar Enlace de Pagos";
            this.Load += new System.EventHandler(this.FrmEnlace_Load);
            this.grpInfoPagos.ResumeLayout(false);
            this.grpInfoPagos.PerformLayout();
            this.grpAcciones.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpInfoPagos;
        private System.Windows.Forms.Label lblTarjeta;
        private System.Windows.Forms.GroupBox grpAcciones;
        private System.Windows.Forms.Button btnCopyText;
        private System.Windows.Forms.Label lblCuotas;
        private System.Windows.Forms.Label lblImporteCuotas;
        private System.Windows.Forms.Button btnVerificar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnCopyEnlace;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.Label lblIntereses;
        private System.Windows.Forms.Label lblImporteTotal;
        private System.Windows.Forms.TextBox txtEnlace;
    }
}