namespace CodenameNightwing.Forms
{
    partial class FrmOtrasOperaciones
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
            this.grpOpciones = new System.Windows.Forms.GroupBox();
            this.rdnEnviarLog = new System.Windows.Forms.RadioButton();
            this.rdnVerificarLogWk = new System.Windows.Forms.RadioButton();
            this.rdnVerificarSeguridad = new System.Windows.Forms.RadioButton();
            this.rdnVerInfo = new System.Windows.Forms.RadioButton();
            this.pnlMensaje = new System.Windows.Forms.Panel();
            this.lblMensaje = new System.Windows.Forms.Label();
            this.rdbResetLibrary = new System.Windows.Forms.RadioButton();
            this.rdbResetPinpad = new System.Windows.Forms.RadioButton();
            this.rdbPrinterTest = new System.Windows.Forms.RadioButton();
            this.rdbReimprimirCupon = new System.Windows.Forms.RadioButton();
            this.rdbVerificarPinpad = new System.Windows.Forms.RadioButton();
            this.rdbCompraPrueba = new System.Windows.Forms.RadioButton();
            this.rdbReimprimirCierre = new System.Windows.Forms.RadioButton();
            this.rdbVerificarConexion = new System.Windows.Forms.RadioButton();
            this.grpAcciones = new System.Windows.Forms.GroupBox();
            this.btnVolver = new System.Windows.Forms.Button();
            this.btnEjecutar = new System.Windows.Forms.Button();
            this.rdnEnviarLogPoi = new System.Windows.Forms.RadioButton();
            this.grpOpciones.SuspendLayout();
            this.pnlMensaje.SuspendLayout();
            this.grpAcciones.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpOpciones
            // 
            this.grpOpciones.Controls.Add(this.rdnEnviarLogPoi);
            this.grpOpciones.Controls.Add(this.rdnEnviarLog);
            this.grpOpciones.Controls.Add(this.rdnVerificarLogWk);
            this.grpOpciones.Controls.Add(this.rdnVerificarSeguridad);
            this.grpOpciones.Controls.Add(this.rdnVerInfo);
            this.grpOpciones.Controls.Add(this.pnlMensaje);
            this.grpOpciones.Controls.Add(this.rdbResetLibrary);
            this.grpOpciones.Controls.Add(this.rdbResetPinpad);
            this.grpOpciones.Controls.Add(this.rdbPrinterTest);
            this.grpOpciones.Controls.Add(this.rdbReimprimirCupon);
            this.grpOpciones.Controls.Add(this.rdbVerificarPinpad);
            this.grpOpciones.Controls.Add(this.rdbCompraPrueba);
            this.grpOpciones.Controls.Add(this.rdbReimprimirCierre);
            this.grpOpciones.Controls.Add(this.rdbVerificarConexion);
            this.grpOpciones.Location = new System.Drawing.Point(13, 13);
            this.grpOpciones.Margin = new System.Windows.Forms.Padding(4);
            this.grpOpciones.Name = "grpOpciones";
            this.grpOpciones.Padding = new System.Windows.Forms.Padding(4);
            this.grpOpciones.Size = new System.Drawing.Size(271, 379);
            this.grpOpciones.TabIndex = 0;
            this.grpOpciones.TabStop = false;
            this.grpOpciones.Text = "Otras Operaciones";
            // 
            // rdnEnviarLog
            // 
            this.rdnEnviarLog.AutoSize = true;
            this.rdnEnviarLog.Location = new System.Drawing.Point(15, 308);
            this.rdnEnviarLog.Name = "rdnEnviarLog";
            this.rdnEnviarLog.Size = new System.Drawing.Size(202, 20);
            this.rdnEnviarLog.TabIndex = 12;
            this.rdnEnviarLog.TabStop = true;
            this.rdnEnviarLog.Text = "Enviar VTOL LOG a Sistemas";
            this.rdnEnviarLog.UseVisualStyleBackColor = true;
            this.rdnEnviarLog.Visible = false;
            this.rdnEnviarLog.CheckedChanged += new System.EventHandler(this.rdnEnviarLog_CheckedChanged);
            // 
            // rdnVerificarLogWk
            // 
            this.rdnVerificarLogWk.AutoSize = true;
            this.rdnVerificarLogWk.Location = new System.Drawing.Point(15, 282);
            this.rdnVerificarLogWk.Name = "rdnVerificarLogWk";
            this.rdnVerificarLogWk.Size = new System.Drawing.Size(199, 20);
            this.rdnVerificarLogWk.TabIndex = 11;
            this.rdnVerificarLogWk.TabStop = true;
            this.rdnVerificarLogWk.Text = "Verificar Log de Working Key";
            this.rdnVerificarLogWk.UseVisualStyleBackColor = true;
            this.rdnVerificarLogWk.Visible = false;
            this.rdnVerificarLogWk.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged_2);
            // 
            // rdnVerificarSeguridad
            // 
            this.rdnVerificarSeguridad.AutoSize = true;
            this.rdnVerificarSeguridad.Location = new System.Drawing.Point(15, 256);
            this.rdnVerificarSeguridad.Name = "rdnVerificarSeguridad";
            this.rdnVerificarSeguridad.Size = new System.Drawing.Size(187, 20);
            this.rdnVerificarSeguridad.TabIndex = 10;
            this.rdnVerificarSeguridad.TabStop = true;
            this.rdnVerificarSeguridad.Text = "Verificar Seguridad Pinpad";
            this.rdnVerificarSeguridad.UseVisualStyleBackColor = true;
            this.rdnVerificarSeguridad.Visible = false;
            this.rdnVerificarSeguridad.CheckedChanged += new System.EventHandler(this.rdnVerificarSeguridad_CheckedChanged);
            // 
            // rdnVerInfo
            // 
            this.rdnVerInfo.AutoSize = true;
            this.rdnVerInfo.Location = new System.Drawing.Point(15, 230);
            this.rdnVerInfo.Name = "rdnVerInfo";
            this.rdnVerInfo.Size = new System.Drawing.Size(231, 20);
            this.rdnVerInfo.TabIndex = 9;
            this.rdnVerInfo.TabStop = true;
            this.rdnVerInfo.Text = "Ver Nro. Serie Pinpad e Impresora";
            this.rdnVerInfo.UseVisualStyleBackColor = true;
            this.rdnVerInfo.Visible = false;
            this.rdnVerInfo.CheckedChanged += new System.EventHandler(this.rdnVerInfo_CheckedChanged);
            // 
            // pnlMensaje
            // 
            this.pnlMensaje.BackColor = System.Drawing.Color.Aqua;
            this.pnlMensaje.Controls.Add(this.lblMensaje);
            this.pnlMensaje.Location = new System.Drawing.Point(0, 86);
            this.pnlMensaje.Name = "pnlMensaje";
            this.pnlMensaje.Size = new System.Drawing.Size(270, 72);
            this.pnlMensaje.TabIndex = 2;
            this.pnlMensaje.Visible = false;
            this.pnlMensaje.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlMensaje_Paint);
            // 
            // lblMensaje
            // 
            this.lblMensaje.AutoSize = true;
            this.lblMensaje.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMensaje.Location = new System.Drawing.Point(11, 10);
            this.lblMensaje.Name = "lblMensaje";
            this.lblMensaje.Size = new System.Drawing.Size(51, 20);
            this.lblMensaje.TabIndex = 0;
            this.lblMensaje.Text = "label1";
            // 
            // rdbResetLibrary
            // 
            this.rdbResetLibrary.AutoSize = true;
            this.rdbResetLibrary.Location = new System.Drawing.Point(16, 204);
            this.rdbResetLibrary.Name = "rdbResetLibrary";
            this.rdbResetLibrary.Size = new System.Drawing.Size(165, 20);
            this.rdbResetLibrary.TabIndex = 8;
            this.rdbResetLibrary.TabStop = true;
            this.rdbResetLibrary.Text = "Reiniciar VTOL Libreria";
            this.rdbResetLibrary.UseVisualStyleBackColor = true;
            this.rdbResetLibrary.Visible = false;
            // 
            // rdbResetPinpad
            // 
            this.rdbResetPinpad.AutoSize = true;
            this.rdbResetPinpad.Location = new System.Drawing.Point(16, 178);
            this.rdbResetPinpad.Name = "rdbResetPinpad";
            this.rdbResetPinpad.Size = new System.Drawing.Size(125, 20);
            this.rdbResetPinpad.TabIndex = 7;
            this.rdbResetPinpad.TabStop = true;
            this.rdbResetPinpad.Text = "Reiniciar Pinpad";
            this.rdbResetPinpad.UseVisualStyleBackColor = true;
            this.rdbResetPinpad.Visible = false;
            this.rdbResetPinpad.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged_1);
            // 
            // rdbPrinterTest
            // 
            this.rdbPrinterTest.AutoSize = true;
            this.rdbPrinterTest.Location = new System.Drawing.Point(16, 152);
            this.rdbPrinterTest.Name = "rdbPrinterTest";
            this.rdbPrinterTest.Size = new System.Drawing.Size(185, 20);
            this.rdbPrinterTest.TabIndex = 6;
            this.rdbPrinterTest.TabStop = true;
            this.rdbPrinterTest.Text = "Imprimir Página de Prueba";
            this.rdbPrinterTest.UseVisualStyleBackColor = true;
            this.rdbPrinterTest.Visible = false;
            this.rdbPrinterTest.CheckedChanged += new System.EventHandler(this.rdbPrinterTest_CheckedChanged);
            // 
            // rdbReimprimirCupon
            // 
            this.rdbReimprimirCupon.AutoSize = true;
            this.rdbReimprimirCupon.Location = new System.Drawing.Point(17, 74);
            this.rdbReimprimirCupon.Name = "rdbReimprimirCupon";
            this.rdbReimprimirCupon.Size = new System.Drawing.Size(169, 20);
            this.rdbReimprimirCupon.TabIndex = 1;
            this.rdbReimprimirCupon.TabStop = true;
            this.rdbReimprimirCupon.Text = "Reimprimir último cupón";
            this.rdbReimprimirCupon.UseVisualStyleBackColor = true;
            // 
            // rdbVerificarPinpad
            // 
            this.rdbVerificarPinpad.AutoSize = true;
            this.rdbVerificarPinpad.Location = new System.Drawing.Point(17, 126);
            this.rdbVerificarPinpad.Name = "rdbVerificarPinpad";
            this.rdbVerificarPinpad.Size = new System.Drawing.Size(121, 20);
            this.rdbVerificarPinpad.TabIndex = 5;
            this.rdbVerificarPinpad.TabStop = true;
            this.rdbVerificarPinpad.Text = "Verificar Pinpad";
            this.rdbVerificarPinpad.UseVisualStyleBackColor = true;
            this.rdbVerificarPinpad.Visible = false;
            this.rdbVerificarPinpad.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // rdbCompraPrueba
            // 
            this.rdbCompraPrueba.AutoSize = true;
            this.rdbCompraPrueba.Location = new System.Drawing.Point(17, 100);
            this.rdbCompraPrueba.Name = "rdbCompraPrueba";
            this.rdbCompraPrueba.Size = new System.Drawing.Size(139, 20);
            this.rdbCompraPrueba.TabIndex = 4;
            this.rdbCompraPrueba.TabStop = true;
            this.rdbCompraPrueba.Text = "Compra de prueba";
            this.rdbCompraPrueba.UseVisualStyleBackColor = true;
            this.rdbCompraPrueba.CheckedChanged += new System.EventHandler(this.rdbCompraPrueba_CheckedChanged);
            // 
            // rdbReimprimirCierre
            // 
            this.rdbReimprimirCierre.AutoSize = true;
            this.rdbReimprimirCierre.Location = new System.Drawing.Point(17, 48);
            this.rdbReimprimirCierre.Name = "rdbReimprimirCierre";
            this.rdbReimprimirCierre.Size = new System.Drawing.Size(172, 20);
            this.rdbReimprimirCierre.TabIndex = 2;
            this.rdbReimprimirCierre.TabStop = true;
            this.rdbReimprimirCierre.Text = "Reimprimir cierre de lote";
            this.rdbReimprimirCierre.UseVisualStyleBackColor = true;
            // 
            // rdbVerificarConexion
            // 
            this.rdbVerificarConexion.AutoSize = true;
            this.rdbVerificarConexion.Location = new System.Drawing.Point(17, 22);
            this.rdbVerificarConexion.Name = "rdbVerificarConexion";
            this.rdbVerificarConexion.Size = new System.Drawing.Size(146, 20);
            this.rdbVerificarConexion.TabIndex = 0;
            this.rdbVerificarConexion.TabStop = true;
            this.rdbVerificarConexion.Text = "Verificar la conexión";
            this.rdbVerificarConexion.UseVisualStyleBackColor = true;
            // 
            // grpAcciones
            // 
            this.grpAcciones.Controls.Add(this.btnVolver);
            this.grpAcciones.Controls.Add(this.btnEjecutar);
            this.grpAcciones.Location = new System.Drawing.Point(12, 399);
            this.grpAcciones.Name = "grpAcciones";
            this.grpAcciones.Size = new System.Drawing.Size(271, 62);
            this.grpAcciones.TabIndex = 1;
            this.grpAcciones.TabStop = false;
            this.grpAcciones.Text = "Acciones";
            this.grpAcciones.Enter += new System.EventHandler(this.grpAcciones_Enter);
            // 
            // btnVolver
            // 
            this.btnVolver.Location = new System.Drawing.Point(145, 21);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(108, 35);
            this.btnVolver.TabIndex = 1;
            this.btnVolver.Text = "&Volver";
            this.btnVolver.UseVisualStyleBackColor = true;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // btnEjecutar
            // 
            this.btnEjecutar.Location = new System.Drawing.Point(18, 21);
            this.btnEjecutar.Name = "btnEjecutar";
            this.btnEjecutar.Size = new System.Drawing.Size(108, 35);
            this.btnEjecutar.TabIndex = 0;
            this.btnEjecutar.Text = "&Ejecutar";
            this.btnEjecutar.UseVisualStyleBackColor = true;
            this.btnEjecutar.Click += new System.EventHandler(this.btnEjecutar_Click);
            // 
            // rdnEnviarLogPoi
            // 
            this.rdnEnviarLogPoi.AutoSize = true;
            this.rdnEnviarLogPoi.Location = new System.Drawing.Point(16, 334);
            this.rdnEnviarLogPoi.Name = "rdnEnviarLogPoi";
            this.rdnEnviarLogPoi.Size = new System.Drawing.Size(189, 20);
            this.rdnEnviarLogPoi.TabIndex = 13;
            this.rdnEnviarLogPoi.TabStop = true;
            this.rdnEnviarLogPoi.Text = "Enviar POI LOG a Sistemas";
            this.rdnEnviarLogPoi.UseVisualStyleBackColor = true;
            // 
            // FrmOtrasOperaciones
            // 
            this.AcceptButton = this.btnEjecutar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(298, 473);
            this.ControlBox = false;
            this.Controls.Add(this.grpAcciones);
            this.Controls.Add(this.grpOpciones);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmOtrasOperaciones";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Otras operaciones";
            this.Load += new System.EventHandler(this.FrmOtrasOperaciones_Load);
            this.grpOpciones.ResumeLayout(false);
            this.grpOpciones.PerformLayout();
            this.pnlMensaje.ResumeLayout(false);
            this.pnlMensaje.PerformLayout();
            this.grpAcciones.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpOpciones;
        private System.Windows.Forms.GroupBox grpAcciones;
        private System.Windows.Forms.RadioButton rdbVerificarConexion;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.Button btnEjecutar;
        private System.Windows.Forms.RadioButton rdbCompraPrueba;
        private System.Windows.Forms.RadioButton rdbReimprimirCierre;
        private System.Windows.Forms.RadioButton rdbReimprimirCupon;
        private System.Windows.Forms.Panel pnlMensaje;
        private System.Windows.Forms.Label lblMensaje;
        private System.Windows.Forms.RadioButton rdbVerificarPinpad;
        private System.Windows.Forms.RadioButton rdbPrinterTest;
        private System.Windows.Forms.RadioButton rdbResetPinpad;
        private System.Windows.Forms.RadioButton rdbResetLibrary;
        private System.Windows.Forms.RadioButton rdnVerInfo;
        private System.Windows.Forms.RadioButton rdnVerificarSeguridad;
        private System.Windows.Forms.RadioButton rdnVerificarLogWk;
        private System.Windows.Forms.RadioButton rdnEnviarLog;
        private System.Windows.Forms.RadioButton rdnEnviarLogPoi;
    }
}