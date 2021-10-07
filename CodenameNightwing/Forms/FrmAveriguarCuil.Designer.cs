namespace CodenameNightwing.Forms
{
    partial class FrmAveriguarCuil
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
            this.grpDatosPagador = new System.Windows.Forms.GroupBox();
            this.IsExtranjero = new System.Windows.Forms.CheckBox();
            this.btnCompletarCuil = new System.Windows.Forms.Button();
            this.cmbPais = new System.Windows.Forms.ComboBox();
            this.cmbTipoPersona = new System.Windows.Forms.ComboBox();
            this.lblPais = new System.Windows.Forms.Label();
            this.txtDocumento = new System.Windows.Forms.MaskedTextBox();
            this.lblDocumento = new System.Windows.Forms.Label();
            this.lblTipoPersona = new System.Windows.Forms.Label();
            this.grpIdentificacionTributaria = new System.Windows.Forms.GroupBox();
            this.btnVerificarCuit = new System.Windows.Forms.Button();
            this.txtCuit = new System.Windows.Forms.MaskedTextBox();
            this.lblCuit = new System.Windows.Forms.Label();
            this.btnVolverOne = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.grpDatosPagador.SuspendLayout();
            this.grpIdentificacionTributaria.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpDatosPagador
            // 
            this.grpDatosPagador.Controls.Add(this.IsExtranjero);
            this.grpDatosPagador.Controls.Add(this.btnCompletarCuil);
            this.grpDatosPagador.Controls.Add(this.cmbPais);
            this.grpDatosPagador.Controls.Add(this.cmbTipoPersona);
            this.grpDatosPagador.Controls.Add(this.lblPais);
            this.grpDatosPagador.Controls.Add(this.txtDocumento);
            this.grpDatosPagador.Controls.Add(this.lblDocumento);
            this.grpDatosPagador.Controls.Add(this.lblTipoPersona);
            this.grpDatosPagador.Location = new System.Drawing.Point(9, 17);
            this.grpDatosPagador.Name = "grpDatosPagador";
            this.grpDatosPagador.Size = new System.Drawing.Size(267, 170);
            this.grpDatosPagador.TabIndex = 13;
            this.grpDatosPagador.TabStop = false;
            this.grpDatosPagador.Text = "Identificación del pagador";
            // 
            // IsExtranjero
            // 
            this.IsExtranjero.AutoSize = true;
            this.IsExtranjero.Location = new System.Drawing.Point(9, 20);
            this.IsExtranjero.Name = "IsExtranjero";
            this.IsExtranjero.Size = new System.Drawing.Size(94, 17);
            this.IsExtranjero.TabIndex = 12;
            this.IsExtranjero.Text = "Es Extranjero?";
            this.IsExtranjero.UseVisualStyleBackColor = true;
            this.IsExtranjero.CheckedChanged += new System.EventHandler(this.IsExtranjero_CheckedChanged);
            // 
            // btnCompletarCuil
            // 
            this.btnCompletarCuil.Location = new System.Drawing.Point(9, 134);
            this.btnCompletarCuil.Name = "btnCompletarCuil";
            this.btnCompletarCuil.Size = new System.Drawing.Size(246, 23);
            this.btnCompletarCuil.TabIndex = 11;
            this.btnCompletarCuil.Text = "Completar números del CUIL";
            this.btnCompletarCuil.UseVisualStyleBackColor = true;
            this.btnCompletarCuil.Click += new System.EventHandler(this.btnCompletarCuil_Click);
            // 
            // cmbPais
            // 
            this.cmbPais.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cmbPais.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.cmbPais.FormattingEnabled = true;
            this.cmbPais.Location = new System.Drawing.Point(87, 100);
            this.cmbPais.Name = "cmbPais";
            this.cmbPais.Size = new System.Drawing.Size(168, 21);
            this.cmbPais.TabIndex = 10;
            this.cmbPais.SelectedIndexChanged += new System.EventHandler(this.cmbPais_SelectedIndexChanged);
            // 
            // cmbTipoPersona
            // 
            this.cmbTipoPersona.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cmbTipoPersona.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.cmbTipoPersona.FormattingEnabled = true;
            this.cmbTipoPersona.Location = new System.Drawing.Point(87, 47);
            this.cmbTipoPersona.Name = "cmbTipoPersona";
            this.cmbTipoPersona.Size = new System.Drawing.Size(168, 21);
            this.cmbTipoPersona.TabIndex = 2;
            this.cmbTipoPersona.SelectedIndexChanged += new System.EventHandler(this.cmbTipoPersona_SelectedIndexChanged);
            // 
            // lblPais
            // 
            this.lblPais.AutoSize = true;
            this.lblPais.Location = new System.Drawing.Point(6, 103);
            this.lblPais.Name = "lblPais";
            this.lblPais.Size = new System.Drawing.Size(75, 13);
            this.lblPais.TabIndex = 9;
            this.lblPais.Text = "Pais................";
            // 
            // txtDocumento
            // 
            this.txtDocumento.Location = new System.Drawing.Point(87, 74);
            this.txtDocumento.Mask = "00.000.000";
            this.txtDocumento.Name = "txtDocumento";
            this.txtDocumento.Size = new System.Drawing.Size(168, 20);
            this.txtDocumento.TabIndex = 6;
            this.txtDocumento.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            // 
            // lblDocumento
            // 
            this.lblDocumento.AutoSize = true;
            this.lblDocumento.Location = new System.Drawing.Point(6, 77);
            this.lblDocumento.Name = "lblDocumento";
            this.lblDocumento.Size = new System.Drawing.Size(74, 13);
            this.lblDocumento.TabIndex = 2;
            this.lblDocumento.Text = "Documento....";
            // 
            // lblTipoPersona
            // 
            this.lblTipoPersona.AutoSize = true;
            this.lblTipoPersona.Location = new System.Drawing.Point(6, 51);
            this.lblTipoPersona.Name = "lblTipoPersona";
            this.lblTipoPersona.Size = new System.Drawing.Size(73, 13);
            this.lblTipoPersona.TabIndex = 0;
            this.lblTipoPersona.Text = "Tipo...............";
            // 
            // grpIdentificacionTributaria
            // 
            this.grpIdentificacionTributaria.Controls.Add(this.btnVerificarCuit);
            this.grpIdentificacionTributaria.Controls.Add(this.txtCuit);
            this.grpIdentificacionTributaria.Controls.Add(this.lblCuit);
            this.grpIdentificacionTributaria.Location = new System.Drawing.Point(9, 193);
            this.grpIdentificacionTributaria.Name = "grpIdentificacionTributaria";
            this.grpIdentificacionTributaria.Size = new System.Drawing.Size(267, 74);
            this.grpIdentificacionTributaria.TabIndex = 14;
            this.grpIdentificacionTributaria.TabStop = false;
            this.grpIdentificacionTributaria.Text = "Identificación Tributaria";
            // 
            // btnVerificarCuit
            // 
            this.btnVerificarCuit.Location = new System.Drawing.Point(9, 42);
            this.btnVerificarCuit.Name = "btnVerificarCuit";
            this.btnVerificarCuit.Size = new System.Drawing.Size(246, 23);
            this.btnVerificarCuit.TabIndex = 11;
            this.btnVerificarCuit.Text = "Verificar CUIT/CUIL";
            this.btnVerificarCuit.UseVisualStyleBackColor = true;
            this.btnVerificarCuit.Click += new System.EventHandler(this.btnVerificarCuit_Click);
            // 
            // txtCuit
            // 
            this.txtCuit.Location = new System.Drawing.Point(87, 16);
            this.txtCuit.Mask = "00-00000000-0";
            this.txtCuit.Name = "txtCuit";
            this.txtCuit.Size = new System.Drawing.Size(168, 20);
            this.txtCuit.TabIndex = 6;
            this.txtCuit.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            this.txtCuit.MaskInputRejected += new System.Windows.Forms.MaskInputRejectedEventHandler(this.txtCuit_MaskInputRejected);
            // 
            // lblCuit
            // 
            this.lblCuit.AutoSize = true;
            this.lblCuit.Location = new System.Drawing.Point(6, 19);
            this.lblCuit.Name = "lblCuit";
            this.lblCuit.Size = new System.Drawing.Size(73, 13);
            this.lblCuit.TabIndex = 2;
            this.lblCuit.Text = "CUIT/CUIL....";
            // 
            // btnVolverOne
            // 
            this.btnVolverOne.Location = new System.Drawing.Point(151, 273);
            this.btnVolverOne.Name = "btnVolverOne";
            this.btnVolverOne.Size = new System.Drawing.Size(119, 44);
            this.btnVolverOne.TabIndex = 16;
            this.btnVolverOne.Text = "&Cancelar";
            this.btnVolverOne.UseVisualStyleBackColor = true;
            this.btnVolverOne.Click += new System.EventHandler(this.btnVolverOne_Click);
            // 
            // btnAceptar
            // 
            this.btnAceptar.Location = new System.Drawing.Point(12, 273);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(125, 44);
            this.btnAceptar.TabIndex = 15;
            this.btnAceptar.Text = "&Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // FrmAveriguarCuil
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 327);
            this.ControlBox = false;
            this.Controls.Add(this.btnVolverOne);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.grpDatosPagador);
            this.Controls.Add(this.grpIdentificacionTributaria);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "FrmAveriguarCuil";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmAveriguarCuil";
            this.Load += new System.EventHandler(this.FrmAveriguarCuil_Load);
            this.grpDatosPagador.ResumeLayout(false);
            this.grpDatosPagador.PerformLayout();
            this.grpIdentificacionTributaria.ResumeLayout(false);
            this.grpIdentificacionTributaria.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpDatosPagador;
        private System.Windows.Forms.Button btnCompletarCuil;
        private System.Windows.Forms.ComboBox cmbPais;
        private System.Windows.Forms.ComboBox cmbTipoPersona;
        private System.Windows.Forms.Label lblPais;
        private System.Windows.Forms.MaskedTextBox txtDocumento;
        private System.Windows.Forms.Label lblDocumento;
        private System.Windows.Forms.Label lblTipoPersona;
        private System.Windows.Forms.GroupBox grpIdentificacionTributaria;
        private System.Windows.Forms.Button btnVerificarCuit;
        private System.Windows.Forms.MaskedTextBox txtCuit;
        private System.Windows.Forms.Label lblCuit;
        private System.Windows.Forms.Button btnVolverOne;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.CheckBox IsExtranjero;
    }
}