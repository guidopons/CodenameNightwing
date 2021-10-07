namespace CodenameNightwing.Forms
{
    partial class FrmIVR
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmIVR));
            this.grpNumeros = new System.Windows.Forms.GroupBox();
            this.completeImageVencimiento = new System.Windows.Forms.PictureBox();
            this.completeImageCVC = new System.Windows.Forms.PictureBox();
            this.imageWaitCVC = new System.Windows.Forms.PictureBox();
            this.imageWaitVencimiento = new System.Windows.Forms.PictureBox();
            this.lblCodSeguridad = new System.Windows.Forms.Label();
            this.lblVencimiento = new System.Windows.Forms.Label();
            this.lblNumeroTarjeta = new System.Windows.Forms.Label();
            this.completeImgCCNumber = new System.Windows.Forms.PictureBox();
            this.imageWaitCCNumber = new System.Windows.Forms.PictureBox();
            this.grpAcciones = new System.Windows.Forms.GroupBox();
            this.btnEnlace = new System.Windows.Forms.Button();
            this.btnCortar = new System.Windows.Forms.Button();
            this.btnManual = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnDerivar = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbIdiomaNPS = new System.Windows.Forms.ComboBox();
            this.cmbIdioma = new System.Windows.Forms.ComboBox();
            this.grpEstados = new System.Windows.Forms.GroupBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblStopWatch = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.grpNumeros.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.completeImageVencimiento)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.completeImageCVC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageWaitCVC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageWaitVencimiento)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.completeImgCCNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageWaitCCNumber)).BeginInit();
            this.grpAcciones.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.grpEstados.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpNumeros
            // 
            this.grpNumeros.Controls.Add(this.completeImageVencimiento);
            this.grpNumeros.Controls.Add(this.completeImageCVC);
            this.grpNumeros.Controls.Add(this.imageWaitCVC);
            this.grpNumeros.Controls.Add(this.imageWaitVencimiento);
            this.grpNumeros.Controls.Add(this.lblCodSeguridad);
            this.grpNumeros.Controls.Add(this.lblVencimiento);
            this.grpNumeros.Controls.Add(this.lblNumeroTarjeta);
            this.grpNumeros.Controls.Add(this.completeImgCCNumber);
            this.grpNumeros.Controls.Add(this.imageWaitCCNumber);
            this.grpNumeros.Location = new System.Drawing.Point(34, 172);
            this.grpNumeros.Name = "grpNumeros";
            this.grpNumeros.Size = new System.Drawing.Size(213, 168);
            this.grpNumeros.TabIndex = 0;
            this.grpNumeros.TabStop = false;
            this.grpNumeros.Text = "Ejecución de IVR Pagos";
            this.grpNumeros.Visible = false;
            // 
            // completeImageVencimiento
            // 
            this.completeImageVencimiento.Image = ((System.Drawing.Image)(resources.GetObject("completeImageVencimiento.Image")));
            this.completeImageVencimiento.Location = new System.Drawing.Point(135, 71);
            this.completeImageVencimiento.Name = "completeImageVencimiento";
            this.completeImageVencimiento.Size = new System.Drawing.Size(37, 37);
            this.completeImageVencimiento.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.completeImageVencimiento.TabIndex = 20;
            this.completeImageVencimiento.TabStop = false;
            this.completeImageVencimiento.Visible = false;
            // 
            // completeImageCVC
            // 
            this.completeImageCVC.Image = ((System.Drawing.Image)(resources.GetObject("completeImageCVC.Image")));
            this.completeImageCVC.Location = new System.Drawing.Point(135, 120);
            this.completeImageCVC.Name = "completeImageCVC";
            this.completeImageCVC.Size = new System.Drawing.Size(37, 37);
            this.completeImageCVC.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.completeImageCVC.TabIndex = 19;
            this.completeImageCVC.TabStop = false;
            this.completeImageCVC.Visible = false;
            // 
            // imageWaitCVC
            // 
            this.imageWaitCVC.Image = ((System.Drawing.Image)(resources.GetObject("imageWaitCVC.Image")));
            this.imageWaitCVC.InitialImage = ((System.Drawing.Image)(resources.GetObject("imageWaitCVC.InitialImage")));
            this.imageWaitCVC.Location = new System.Drawing.Point(126, 120);
            this.imageWaitCVC.Name = "imageWaitCVC";
            this.imageWaitCVC.Size = new System.Drawing.Size(31, 37);
            this.imageWaitCVC.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imageWaitCVC.TabIndex = 17;
            this.imageWaitCVC.TabStop = false;
            // 
            // imageWaitVencimiento
            // 
            this.imageWaitVencimiento.Image = ((System.Drawing.Image)(resources.GetObject("imageWaitVencimiento.Image")));
            this.imageWaitVencimiento.InitialImage = ((System.Drawing.Image)(resources.GetObject("imageWaitVencimiento.InitialImage")));
            this.imageWaitVencimiento.Location = new System.Drawing.Point(126, 69);
            this.imageWaitVencimiento.Name = "imageWaitVencimiento";
            this.imageWaitVencimiento.Size = new System.Drawing.Size(31, 37);
            this.imageWaitVencimiento.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imageWaitVencimiento.TabIndex = 16;
            this.imageWaitVencimiento.TabStop = false;
            // 
            // lblCodSeguridad
            // 
            this.lblCodSeguridad.AutoSize = true;
            this.lblCodSeguridad.Location = new System.Drawing.Point(31, 132);
            this.lblCodSeguridad.Name = "lblCodSeguridad";
            this.lblCodSeguridad.Size = new System.Drawing.Size(89, 13);
            this.lblCodSeguridad.TabIndex = 14;
            this.lblCodSeguridad.Text = "Cod. Seguridad...";
            // 
            // lblVencimiento
            // 
            this.lblVencimiento.AutoSize = true;
            this.lblVencimiento.Location = new System.Drawing.Point(46, 82);
            this.lblVencimiento.Name = "lblVencimiento";
            this.lblVencimiento.Size = new System.Drawing.Size(74, 13);
            this.lblVencimiento.TabIndex = 13;
            this.lblVencimiento.Text = "Vencimiento...";
            // 
            // lblNumeroTarjeta
            // 
            this.lblNumeroTarjeta.AutoSize = true;
            this.lblNumeroTarjeta.Location = new System.Drawing.Point(46, 33);
            this.lblNumeroTarjeta.Name = "lblNumeroTarjeta";
            this.lblNumeroTarjeta.Size = new System.Drawing.Size(74, 13);
            this.lblNumeroTarjeta.TabIndex = 9;
            this.lblNumeroTarjeta.Text = "Número..........";
            // 
            // completeImgCCNumber
            // 
            this.completeImgCCNumber.Image = ((System.Drawing.Image)(resources.GetObject("completeImgCCNumber.Image")));
            this.completeImgCCNumber.Location = new System.Drawing.Point(135, 19);
            this.completeImgCCNumber.Name = "completeImgCCNumber";
            this.completeImgCCNumber.Size = new System.Drawing.Size(37, 37);
            this.completeImgCCNumber.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.completeImgCCNumber.TabIndex = 18;
            this.completeImgCCNumber.TabStop = false;
            this.completeImgCCNumber.Visible = false;
            // 
            // imageWaitCCNumber
            // 
            this.imageWaitCCNumber.Image = ((System.Drawing.Image)(resources.GetObject("imageWaitCCNumber.Image")));
            this.imageWaitCCNumber.InitialImage = ((System.Drawing.Image)(resources.GetObject("imageWaitCCNumber.InitialImage")));
            this.imageWaitCCNumber.Location = new System.Drawing.Point(126, 19);
            this.imageWaitCCNumber.Name = "imageWaitCCNumber";
            this.imageWaitCCNumber.Size = new System.Drawing.Size(31, 37);
            this.imageWaitCCNumber.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imageWaitCCNumber.TabIndex = 15;
            this.imageWaitCCNumber.TabStop = false;
            // 
            // grpAcciones
            // 
            this.grpAcciones.Controls.Add(this.btnEnlace);
            this.grpAcciones.Controls.Add(this.btnCortar);
            this.grpAcciones.Controls.Add(this.btnManual);
            this.grpAcciones.Controls.Add(this.btnCancelar);
            this.grpAcciones.Controls.Add(this.btnDerivar);
            this.grpAcciones.Location = new System.Drawing.Point(7, 346);
            this.grpAcciones.Name = "grpAcciones";
            this.grpAcciones.Size = new System.Drawing.Size(264, 80);
            this.grpAcciones.TabIndex = 1;
            this.grpAcciones.TabStop = false;
            this.grpAcciones.Text = "Acciones";
            // 
            // btnEnlace
            // 
            this.btnEnlace.Location = new System.Drawing.Point(91, 42);
            this.btnEnlace.Name = "btnEnlace";
            this.btnEnlace.Size = new System.Drawing.Size(71, 23);
            this.btnEnlace.TabIndex = 5;
            this.btnEnlace.Text = "&Enlace";
            this.btnEnlace.UseVisualStyleBackColor = true;
            this.btnEnlace.Click += new System.EventHandler(this.btnEnlace_Click);
            // 
            // btnCortar
            // 
            this.btnCortar.Enabled = false;
            this.btnCortar.Location = new System.Drawing.Point(179, 15);
            this.btnCortar.Name = "btnCortar";
            this.btnCortar.Size = new System.Drawing.Size(71, 23);
            this.btnCortar.TabIndex = 4;
            this.btnCortar.Text = "C&ortar";
            this.btnCortar.UseVisualStyleBackColor = true;
            this.btnCortar.Click += new System.EventHandler(this.btnCortar_Click);
            // 
            // btnManual
            // 
            this.btnManual.Location = new System.Drawing.Point(6, 42);
            this.btnManual.Name = "btnManual";
            this.btnManual.Size = new System.Drawing.Size(71, 23);
            this.btnManual.TabIndex = 2;
            this.btnManual.Text = "&Manual";
            this.btnManual.UseVisualStyleBackColor = true;
            this.btnManual.Click += new System.EventHandler(this.btnManual_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(91, 15);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(71, 23);
            this.btnCancelar.TabIndex = 3;
            this.btnCancelar.Text = "&Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnDerivar
            // 
            this.btnDerivar.Location = new System.Drawing.Point(5, 16);
            this.btnDerivar.Name = "btnDerivar";
            this.btnDerivar.Size = new System.Drawing.Size(71, 23);
            this.btnDerivar.TabIndex = 1;
            this.btnDerivar.Text = "&Derivar";
            this.btnDerivar.UseVisualStyleBackColor = true;
            this.btnDerivar.Click += new System.EventHandler(this.btnDerivar_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cmbIdiomaNPS);
            this.groupBox1.Controls.Add(this.cmbIdioma);
            this.groupBox1.Location = new System.Drawing.Point(32, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(213, 96);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Idioma";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 62);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "NPS...";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "IVR...";
            // 
            // cmbIdiomaNPS
            // 
            this.cmbIdiomaNPS.FormattingEnabled = true;
            this.cmbIdiomaNPS.Location = new System.Drawing.Point(46, 59);
            this.cmbIdiomaNPS.Name = "cmbIdiomaNPS";
            this.cmbIdiomaNPS.Size = new System.Drawing.Size(153, 21);
            this.cmbIdiomaNPS.TabIndex = 1;
            this.cmbIdiomaNPS.SelectedIndexChanged += new System.EventHandler(this.cmbIdiomaNPS_SelectedIndexChanged);
            // 
            // cmbIdioma
            // 
            this.cmbIdioma.FormattingEnabled = true;
            this.cmbIdioma.Items.AddRange(new object[] {
            "ESPAÑOL",
            "INGLES",
            "PORTUGUES"});
            this.cmbIdioma.Location = new System.Drawing.Point(47, 20);
            this.cmbIdioma.Name = "cmbIdioma";
            this.cmbIdioma.Size = new System.Drawing.Size(152, 21);
            this.cmbIdioma.TabIndex = 0;
            this.cmbIdioma.SelectedIndexChanged += new System.EventHandler(this.cmbIdioma_SelectedIndexChanged);
            // 
            // grpEstados
            // 
            this.grpEstados.Controls.Add(this.lblStatus);
            this.grpEstados.Location = new System.Drawing.Point(34, 114);
            this.grpEstados.Name = "grpEstados";
            this.grpEstados.Size = new System.Drawing.Size(213, 52);
            this.grpEstados.TabIndex = 7;
            this.grpEstados.TabStop = false;
            this.grpEstados.Text = "Estado";
            this.grpEstados.Visible = false;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(65, 26);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 13);
            this.lblStatus.TabIndex = 0;
            // 
            // lblStopWatch
            // 
            this.lblStopWatch.AutoSize = true;
            this.lblStopWatch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStopWatch.Location = new System.Drawing.Point(183, 429);
            this.lblStopWatch.Name = "lblStopWatch";
            this.lblStopWatch.Size = new System.Drawing.Size(28, 13);
            this.lblStopWatch.TabIndex = 8;
            this.lblStopWatch.Text = "240";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(141, 429);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Tiempo:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(209, 429);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "segundos";
            // 
            // FrmIVR
            // 
            this.AcceptButton = this.btnDerivar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(278, 451);
            this.ControlBox = false;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblStopWatch);
            this.Controls.Add(this.grpEstados);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grpAcciones);
            this.Controls.Add(this.grpNumeros);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "FrmIVR";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Comunicandose con el IVR de Pagos";
            this.Load += new System.EventHandler(this.FrmIVR_Load);
            this.grpNumeros.ResumeLayout(false);
            this.grpNumeros.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.completeImageVencimiento)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.completeImageCVC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageWaitCVC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageWaitVencimiento)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.completeImgCCNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageWaitCCNumber)).EndInit();
            this.grpAcciones.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grpEstados.ResumeLayout(false);
            this.grpEstados.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpNumeros;
        private System.Windows.Forms.Label lblNumeroTarjeta;
        private System.Windows.Forms.GroupBox grpAcciones;
        private System.Windows.Forms.Button btnDerivar;
        private System.Windows.Forms.Label lblVencimiento;
        private System.Windows.Forms.Label lblCodSeguridad;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.PictureBox imageWaitCCNumber;
        private System.Windows.Forms.PictureBox imageWaitCVC;
        private System.Windows.Forms.PictureBox imageWaitVencimiento;
        private System.Windows.Forms.PictureBox completeImageVencimiento;
        private System.Windows.Forms.PictureBox completeImageCVC;
        private System.Windows.Forms.PictureBox completeImgCCNumber;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbIdioma;
        private System.Windows.Forms.GroupBox grpEstados;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnManual;
        private System.Windows.Forms.Button btnCortar;
        private System.Windows.Forms.Label lblStopWatch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnEnlace;
        private System.Windows.Forms.ComboBox cmbIdiomaNPS;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
    }
}