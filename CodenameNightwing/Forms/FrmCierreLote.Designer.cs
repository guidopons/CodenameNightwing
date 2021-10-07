namespace CodenameNightwing.Forms
{
    partial class FrmCierreLote
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
            this.lblMensaje = new System.Windows.Forms.Label();
            this.pnlMensaje = new System.Windows.Forms.Panel();
            this.btnCerrarLote = new System.Windows.Forms.Button();
            this.pnlMensaje.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblMensaje
            // 
            this.lblMensaje.AutoSize = true;
            this.lblMensaje.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMensaje.Location = new System.Drawing.Point(24, 13);
            this.lblMensaje.Name = "lblMensaje";
            this.lblMensaje.Size = new System.Drawing.Size(508, 72);
            this.lblMensaje.TabIndex = 0;
            this.lblMensaje.Text = "Presione el boton para realizar el Cierre de Lote\r\n\r\nSiga las indicaciones del PO" +
    "S aceptando con la tecla verde\r\n";
            this.lblMensaje.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlMensaje
            // 
            this.pnlMensaje.Controls.Add(this.lblMensaje);
            this.pnlMensaje.Location = new System.Drawing.Point(41, 12);
            this.pnlMensaje.Name = "pnlMensaje";
            this.pnlMensaje.Size = new System.Drawing.Size(552, 100);
            this.pnlMensaje.TabIndex = 1;
            // 
            // btnCerrarLote
            // 
            this.btnCerrarLote.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrarLote.Location = new System.Drawing.Point(251, 118);
            this.btnCerrarLote.Name = "btnCerrarLote";
            this.btnCerrarLote.Size = new System.Drawing.Size(131, 32);
            this.btnCerrarLote.TabIndex = 2;
            this.btnCerrarLote.Text = "&Cerrar Lote";
            this.btnCerrarLote.UseVisualStyleBackColor = true;
            this.btnCerrarLote.Click += new System.EventHandler(this.btnCerrarLote_Click);
            // 
            // FrmCierreLote
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Aqua;
            this.ClientSize = new System.Drawing.Size(632, 162);
            this.ControlBox = false;
            this.Controls.Add(this.btnCerrarLote);
            this.Controls.Add(this.pnlMensaje);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.KeyPreview = true;
            this.Name = "FrmCierreLote";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cierre de Lote - POS Integrado";
            this.pnlMensaje.ResumeLayout(false);
            this.pnlMensaje.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblMensaje;
        private System.Windows.Forms.Panel pnlMensaje;
        private System.Windows.Forms.Button btnCerrarLote;
    }
}