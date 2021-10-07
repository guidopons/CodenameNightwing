namespace CodenameNightwing.Forms
{
    partial class FrmImportes
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
            this.grpACobrar = new System.Windows.Forms.GroupBox();
            this.chkElegirPax = new System.Windows.Forms.CheckBox();
            this.grpTarjeta = new System.Windows.Forms.GroupBox();
            this.grpAcciones = new System.Windows.Forms.GroupBox();
            this.btnVolver = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.grpImporte = new System.Windows.Forms.GroupBox();
            this.txtAutorizar = new System.Windows.Forms.TextBox();
            this.grpPasajeros = new System.Windows.Forms.GroupBox();
            this.treePasajeros = new System.Windows.Forms.TreeView();
            this.lblPriceInformation = new System.Windows.Forms.Label();
            this.grpACobrar.SuspendLayout();
            this.grpAcciones.SuspendLayout();
            this.grpImporte.SuspendLayout();
            this.grpPasajeros.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpACobrar
            // 
            this.grpACobrar.Controls.Add(this.chkElegirPax);
            this.grpACobrar.Location = new System.Drawing.Point(12, 12);
            this.grpACobrar.Name = "grpACobrar";
            this.grpACobrar.Size = new System.Drawing.Size(171, 43);
            this.grpACobrar.TabIndex = 0;
            this.grpACobrar.TabStop = false;
            this.grpACobrar.Text = "A cobrar";
            // 
            // chkElegirPax
            // 
            this.chkElegirPax.AutoSize = true;
            this.chkElegirPax.Location = new System.Drawing.Point(6, 19);
            this.chkElegirPax.Name = "chkElegirPax";
            this.chkElegirPax.Size = new System.Drawing.Size(108, 17);
            this.chkElegirPax.TabIndex = 0;
            this.chkElegirPax.Text = "Elegir de lista pax";
            this.chkElegirPax.UseVisualStyleBackColor = true;
            this.chkElegirPax.CheckedChanged += new System.EventHandler(this.chkElegirPax_CheckedChanged);
            // 
            // grpTarjeta
            // 
            this.grpTarjeta.Location = new System.Drawing.Point(12, 54);
            this.grpTarjeta.Name = "grpTarjeta";
            this.grpTarjeta.Size = new System.Drawing.Size(171, 141);
            this.grpTarjeta.TabIndex = 1;
            this.grpTarjeta.TabStop = false;
            this.grpTarjeta.Text = "Tarjeta";
            this.grpTarjeta.Enter += new System.EventHandler(this.grpTarjeta_Enter);
            // 
            // grpAcciones
            // 
            this.grpAcciones.Controls.Add(this.btnVolver);
            this.grpAcciones.Controls.Add(this.btnAceptar);
            this.grpAcciones.Location = new System.Drawing.Point(12, 275);
            this.grpAcciones.Name = "grpAcciones";
            this.grpAcciones.Size = new System.Drawing.Size(171, 43);
            this.grpAcciones.TabIndex = 2;
            this.grpAcciones.TabStop = false;
            this.grpAcciones.Text = "Acciones";
            // 
            // btnVolver
            // 
            this.btnVolver.Location = new System.Drawing.Point(88, 14);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(75, 23);
            this.btnVolver.TabIndex = 1;
            this.btnVolver.Text = "&Volver";
            this.btnVolver.UseVisualStyleBackColor = true;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // btnAceptar
            // 
            this.btnAceptar.Location = new System.Drawing.Point(6, 14);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(75, 23);
            this.btnAceptar.TabIndex = 0;
            this.btnAceptar.Text = "&Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // grpImporte
            // 
            this.grpImporte.Controls.Add(this.lblPriceInformation);
            this.grpImporte.Controls.Add(this.txtAutorizar);
            this.grpImporte.Location = new System.Drawing.Point(12, 201);
            this.grpImporte.Name = "grpImporte";
            this.grpImporte.Size = new System.Drawing.Size(171, 68);
            this.grpImporte.TabIndex = 3;
            this.grpImporte.TabStop = false;
            this.grpImporte.Text = "Importe a autorizar";
            // 
            // txtAutorizar
            // 
            this.txtAutorizar.Location = new System.Drawing.Point(15, 19);
            this.txtAutorizar.Name = "txtAutorizar";
            this.txtAutorizar.Size = new System.Drawing.Size(117, 20);
            this.txtAutorizar.TabIndex = 0;
            this.txtAutorizar.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtAutorizar.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAutorizar_KeyPress);
            this.txtAutorizar.Validated += new System.EventHandler(this.txtAutorizar_Validated);
            // 
            // grpPasajeros
            // 
            this.grpPasajeros.Controls.Add(this.treePasajeros);
            this.grpPasajeros.Location = new System.Drawing.Point(189, 12);
            this.grpPasajeros.Name = "grpPasajeros";
            this.grpPasajeros.Size = new System.Drawing.Size(613, 281);
            this.grpPasajeros.TabIndex = 4;
            this.grpPasajeros.TabStop = false;
            this.grpPasajeros.Text = "Lista de pasajeros";
            this.grpPasajeros.Visible = false;
            // 
            // treePasajeros
            // 
            this.treePasajeros.CheckBoxes = true;
            this.treePasajeros.Location = new System.Drawing.Point(6, 19);
            this.treePasajeros.Name = "treePasajeros";
            this.treePasajeros.Size = new System.Drawing.Size(601, 256);
            this.treePasajeros.TabIndex = 0;
            this.treePasajeros.BeforeCheck += new System.Windows.Forms.TreeViewCancelEventHandler(this.treePasajeros_BeforeCheck);
            this.treePasajeros.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treePasajeros_AfterSelect);
            // 
            // lblPriceInformation
            // 
            this.lblPriceInformation.AutoSize = true;
            this.lblPriceInformation.Location = new System.Drawing.Point(12, 42);
            this.lblPriceInformation.Name = "lblPriceInformation";
            this.lblPriceInformation.Size = new System.Drawing.Size(0, 13);
            this.lblPriceInformation.TabIndex = 1;
            // 
            // FrmImportes
            // 
            this.AcceptButton = this.btnAceptar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(814, 319);
            this.ControlBox = false;
            this.Controls.Add(this.grpPasajeros);
            this.Controls.Add(this.grpImporte);
            this.Controls.Add(this.grpAcciones);
            this.Controls.Add(this.grpTarjeta);
            this.Controls.Add(this.grpACobrar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.KeyPreview = true;
            this.Name = "FrmImportes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Importes";
            this.Load += new System.EventHandler(this.FrmImportes_Load);
            this.grpACobrar.ResumeLayout(false);
            this.grpACobrar.PerformLayout();
            this.grpAcciones.ResumeLayout(false);
            this.grpImporte.ResumeLayout(false);
            this.grpImporte.PerformLayout();
            this.grpPasajeros.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpACobrar;
        private System.Windows.Forms.CheckBox chkElegirPax;
        private System.Windows.Forms.GroupBox grpTarjeta;
        private System.Windows.Forms.GroupBox grpAcciones;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.GroupBox grpImporte;
        private System.Windows.Forms.TextBox txtAutorizar;
        private System.Windows.Forms.GroupBox grpPasajeros;
        private System.Windows.Forms.TreeView treePasajeros;
        private System.Windows.Forms.Label lblPriceInformation;
    }
}