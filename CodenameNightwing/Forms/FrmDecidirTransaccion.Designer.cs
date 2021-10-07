namespace CodenameNightwing.Forms
{
    partial class FrmDecidirTransaccion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDecidirTransaccion));
            this.grpAcciones = new System.Windows.Forms.GroupBox();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.grpExplicacion = new System.Windows.Forms.GroupBox();
            this.pnlExplicacion = new System.Windows.Forms.Panel();
            this.lblExplicacion = new System.Windows.Forms.Label();
            this.grpOpciones = new System.Windows.Forms.GroupBox();
            this.rdbNo = new System.Windows.Forms.RadioButton();
            this.rdbSi = new System.Windows.Forms.RadioButton();
            this.grpAcciones.SuspendLayout();
            this.grpExplicacion.SuspendLayout();
            this.pnlExplicacion.SuspendLayout();
            this.grpOpciones.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpAcciones
            // 
            this.grpAcciones.Controls.Add(this.btnAceptar);
            this.grpAcciones.Location = new System.Drawing.Point(12, 186);
            this.grpAcciones.Name = "grpAcciones";
            this.grpAcciones.Size = new System.Drawing.Size(504, 64);
            this.grpAcciones.TabIndex = 0;
            this.grpAcciones.TabStop = false;
            this.grpAcciones.Text = "Acciones";
            // 
            // btnAceptar
            // 
            this.btnAceptar.Location = new System.Drawing.Point(124, 19);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(253, 32);
            this.btnAceptar.TabIndex = 0;
            this.btnAceptar.Text = "&Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // grpExplicacion
            // 
            this.grpExplicacion.Controls.Add(this.pnlExplicacion);
            this.grpExplicacion.Location = new System.Drawing.Point(12, 12);
            this.grpExplicacion.Name = "grpExplicacion";
            this.grpExplicacion.Size = new System.Drawing.Size(504, 100);
            this.grpExplicacion.TabIndex = 1;
            this.grpExplicacion.TabStop = false;
            this.grpExplicacion.Text = "Explicación";
            // 
            // pnlExplicacion
            // 
            this.pnlExplicacion.BackColor = System.Drawing.Color.Aqua;
            this.pnlExplicacion.Controls.Add(this.lblExplicacion);
            this.pnlExplicacion.Location = new System.Drawing.Point(6, 19);
            this.pnlExplicacion.Name = "pnlExplicacion";
            this.pnlExplicacion.Size = new System.Drawing.Size(487, 75);
            this.pnlExplicacion.TabIndex = 0;
            // 
            // lblExplicacion
            // 
            this.lblExplicacion.AutoSize = true;
            this.lblExplicacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExplicacion.Location = new System.Drawing.Point(7, 8);
            this.lblExplicacion.Name = "lblExplicacion";
            this.lblExplicacion.Size = new System.Drawing.Size(471, 60);
            this.lblExplicacion.TabIndex = 0;
            this.lblExplicacion.Text = resources.GetString("lblExplicacion.Text");
            this.lblExplicacion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // grpOpciones
            // 
            this.grpOpciones.Controls.Add(this.rdbNo);
            this.grpOpciones.Controls.Add(this.rdbSi);
            this.grpOpciones.Location = new System.Drawing.Point(12, 118);
            this.grpOpciones.Name = "grpOpciones";
            this.grpOpciones.Size = new System.Drawing.Size(504, 62);
            this.grpOpciones.TabIndex = 2;
            this.grpOpciones.TabStop = false;
            this.grpOpciones.Text = "Opciones";
            // 
            // rdbNo
            // 
            this.rdbNo.AutoSize = true;
            this.rdbNo.Location = new System.Drawing.Point(16, 39);
            this.rdbNo.Name = "rdbNo";
            this.rdbNo.Size = new System.Drawing.Size(184, 17);
            this.rdbNo.TabIndex = 1;
            this.rdbNo.TabStop = true;
            this.rdbNo.Text = "La operación debe ser cancelada";
            this.rdbNo.UseVisualStyleBackColor = true;
            // 
            // rdbSi
            // 
            this.rdbSi.AutoSize = true;
            this.rdbSi.Location = new System.Drawing.Point(16, 19);
            this.rdbSi.Name = "rdbSi";
            this.rdbSi.Size = new System.Drawing.Size(379, 17);
            this.rdbSi.TabIndex = 0;
            this.rdbSi.TabStop = true;
            this.rdbSi.Text = "La operación se llevó a cabo con éxito y el cupón se imprimió exitosamente";
            this.rdbSi.UseVisualStyleBackColor = true;
            // 
            // FrmDecidirTransaccion
            // 
            this.AcceptButton = this.btnAceptar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(527, 262);
            this.ControlBox = false;
            this.Controls.Add(this.grpOpciones);
            this.Controls.Add(this.grpExplicacion);
            this.Controls.Add(this.grpAcciones);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.KeyPreview = true;
            this.Name = "FrmDecidirTransaccion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Seleccione el estado de la ultima transaccion";
            this.grpAcciones.ResumeLayout(false);
            this.grpExplicacion.ResumeLayout(false);
            this.pnlExplicacion.ResumeLayout(false);
            this.pnlExplicacion.PerformLayout();
            this.grpOpciones.ResumeLayout(false);
            this.grpOpciones.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpAcciones;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.GroupBox grpExplicacion;
        private System.Windows.Forms.Panel pnlExplicacion;
        private System.Windows.Forms.Label lblExplicacion;
        private System.Windows.Forms.GroupBox grpOpciones;
        private System.Windows.Forms.RadioButton rdbNo;
        private System.Windows.Forms.RadioButton rdbSi;
    }
}