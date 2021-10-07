namespace CodenameNightwing.Forms
{
    partial class FrmChangePrinter

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
            this.lblPrinterList = new System.Windows.Forms.Label();
            this.cmbPrinterList = new System.Windows.Forms.ComboBox();
            this.grpAcciones = new System.Windows.Forms.GroupBox();
            this.btnVolver = new System.Windows.Forms.Button();
            this.btnEjecutar = new System.Windows.Forms.Button();
            this.grpOpciones.SuspendLayout();
            this.grpAcciones.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpOpciones
            // 
            this.grpOpciones.Controls.Add(this.lblPrinterList);
            this.grpOpciones.Controls.Add(this.cmbPrinterList);
            this.grpOpciones.Location = new System.Drawing.Point(13, 13);
            this.grpOpciones.Margin = new System.Windows.Forms.Padding(4);
            this.grpOpciones.Name = "grpOpciones";
            this.grpOpciones.Padding = new System.Windows.Forms.Padding(4);
            this.grpOpciones.Size = new System.Drawing.Size(271, 110);
            this.grpOpciones.TabIndex = 0;
            this.grpOpciones.TabStop = false;
            this.grpOpciones.Text = "Cambiar Impresora";
            // 
            // lblPrinterList
            // 
            this.lblPrinterList.AutoSize = true;
            this.lblPrinterList.Location = new System.Drawing.Point(15, 35);
            this.lblPrinterList.Name = "lblPrinterList";
            this.lblPrinterList.Size = new System.Drawing.Size(242, 16);
            this.lblPrinterList.TabIndex = 10;
            this.lblPrinterList.Text = "Seleccione impresora de Cupones/Itin :";
            this.lblPrinterList.Visible = false;
            // 
            // cmbPrinterList
            // 
            this.cmbPrinterList.FormattingEnabled = true;
            this.cmbPrinterList.Location = new System.Drawing.Point(15, 59);
            this.cmbPrinterList.Name = "cmbPrinterList";
            this.cmbPrinterList.Size = new System.Drawing.Size(238, 24);
            this.cmbPrinterList.TabIndex = 9;
            this.cmbPrinterList.Visible = false;
            // 
            // grpAcciones
            // 
            this.grpAcciones.Controls.Add(this.btnVolver);
            this.grpAcciones.Controls.Add(this.btnEjecutar);
            this.grpAcciones.Location = new System.Drawing.Point(13, 140);
            this.grpAcciones.Name = "grpAcciones";
            this.grpAcciones.Size = new System.Drawing.Size(271, 62);
            this.grpAcciones.TabIndex = 1;
            this.grpAcciones.TabStop = false;
            this.grpAcciones.Text = "Acciones";
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
            // FrmChangePrinter
            // 
            this.AcceptButton = this.btnEjecutar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(298, 207);
            this.ControlBox = false;
            this.Controls.Add(this.grpAcciones);
            this.Controls.Add(this.grpOpciones);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmChangePrinter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cambiar Impresora";
            this.grpOpciones.ResumeLayout(false);
            this.grpOpciones.PerformLayout();
            this.grpAcciones.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpOpciones;
        private System.Windows.Forms.GroupBox grpAcciones;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.Button btnEjecutar;
        private System.Windows.Forms.ComboBox cmbPrinterList;
        private System.Windows.Forms.Label lblPrinterList;
    }
}