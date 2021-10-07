namespace CodenameNightwing.Forms
{
    partial class FrmImpresionFallidaComun
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
            this.pnlMensaje = new System.Windows.Forms.Panel();
            this.lblMensaje = new System.Windows.Forms.Label();
            this.lblMensajeFinal = new System.Windows.Forms.Label();
            this.grpAcciones = new System.Windows.Forms.GroupBox();
            this.btnReversarTrans = new System.Windows.Forms.Button();
            this.btnCambiarImpresora = new System.Windows.Forms.Button();
            this.btnReimprimirCupones = new System.Windows.Forms.Button();
            this.lblPOS = new System.Windows.Forms.Label();
            this.pnlMensajePOS = new System.Windows.Forms.Panel();
            this.pnlMensaje.SuspendLayout();
            this.grpAcciones.SuspendLayout();
            this.pnlMensajePOS.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMensaje
            // 
            this.pnlMensaje.BackColor = System.Drawing.Color.Yellow;
            this.pnlMensaje.Controls.Add(this.lblMensaje);
            this.pnlMensaje.Location = new System.Drawing.Point(7, 9);
            this.pnlMensaje.Name = "pnlMensaje";
            this.pnlMensaje.Size = new System.Drawing.Size(683, 31);
            this.pnlMensaje.TabIndex = 0;
            // 
            // lblMensaje
            // 
            this.lblMensaje.AutoSize = true;
            this.lblMensaje.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMensaje.Location = new System.Drawing.Point(15, 5);
            this.lblMensaje.Name = "lblMensaje";
            this.lblMensaje.Size = new System.Drawing.Size(178, 20);
            this.lblMensaje.TabIndex = 0;
            this.lblMensaje.Text = "FALLÓ LA IMPRESIÓN";
            this.lblMensaje.Click += new System.EventHandler(this.lblMensaje_Click);
            // 
            // lblMensajeFinal
            // 
            this.lblMensajeFinal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblMensajeFinal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMensajeFinal.Location = new System.Drawing.Point(12, 43);
            this.lblMensajeFinal.Name = "lblMensajeFinal";
            this.lblMensajeFinal.Size = new System.Drawing.Size(683, 94);
            this.lblMensajeFinal.TabIndex = 8;
            this.lblMensajeFinal.Text = "La impresora falló. Puede seleccionar las opciones de abajo. Recuerde que en el m" +
    "enú POI / Printer, encuentra opciones para reimprimir documentos";
            this.lblMensajeFinal.Click += new System.EventHandler(this.lblMensajeFinal_Click);
            // 
            // grpAcciones
            // 
            this.grpAcciones.Controls.Add(this.btnReversarTrans);
            this.grpAcciones.Controls.Add(this.btnCambiarImpresora);
            this.grpAcciones.Controls.Add(this.btnReimprimirCupones);
            this.grpAcciones.Location = new System.Drawing.Point(13, 143);
            this.grpAcciones.Name = "grpAcciones";
            this.grpAcciones.Size = new System.Drawing.Size(682, 51);
            this.grpAcciones.TabIndex = 9;
            this.grpAcciones.TabStop = false;
            this.grpAcciones.Text = "Acciones";
            // 
            // btnReversarTrans
            // 
            this.btnReversarTrans.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReversarTrans.Location = new System.Drawing.Point(426, 15);
            this.btnReversarTrans.Name = "btnReversarTrans";
            this.btnReversarTrans.Size = new System.Drawing.Size(163, 30);
            this.btnReversarTrans.TabIndex = 3;
            this.btnReversarTrans.Text = "C&ancelar";
            this.btnReversarTrans.UseVisualStyleBackColor = true;
            this.btnReversarTrans.Click += new System.EventHandler(this.btnReversarTrans_Click);
            // 
            // btnCambiarImpresora
            // 
            this.btnCambiarImpresora.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCambiarImpresora.Location = new System.Drawing.Point(279, 15);
            this.btnCambiarImpresora.Name = "btnCambiarImpresora";
            this.btnCambiarImpresora.Size = new System.Drawing.Size(141, 30);
            this.btnCambiarImpresora.TabIndex = 2;
            this.btnCambiarImpresora.Text = "&Cambiar Impresora";
            this.btnCambiarImpresora.UseVisualStyleBackColor = true;
            this.btnCambiarImpresora.Click += new System.EventHandler(this.btnCambiarImpresora_Click);
            // 
            // btnReimprimirCupones
            // 
            this.btnReimprimirCupones.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReimprimirCupones.Location = new System.Drawing.Point(132, 15);
            this.btnReimprimirCupones.Name = "btnReimprimirCupones";
            this.btnReimprimirCupones.Size = new System.Drawing.Size(141, 30);
            this.btnReimprimirCupones.TabIndex = 0;
            this.btnReimprimirCupones.Text = "&Reimprimir";
            this.btnReimprimirCupones.UseVisualStyleBackColor = true;
            this.btnReimprimirCupones.Click += new System.EventHandler(this.btnReimprimirCupones_Click);
            // 
            // lblPOS
            // 
            this.lblPOS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPOS.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPOS.Location = new System.Drawing.Point(96, 17);
            this.lblPOS.Name = "lblPOS";
            this.lblPOS.Size = new System.Drawing.Size(453, 40);
            this.lblPOS.TabIndex = 3;
            this.lblPOS.Text = "Imprimiendo Cupones...";
            this.lblPOS.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlMensajePOS
            // 
            this.pnlMensajePOS.BackColor = System.Drawing.Color.Aqua;
            this.pnlMensajePOS.Controls.Add(this.lblPOS);
            this.pnlMensajePOS.Location = new System.Drawing.Point(13, 46);
            this.pnlMensajePOS.Name = "pnlMensajePOS";
            this.pnlMensajePOS.Size = new System.Drawing.Size(683, 75);
            this.pnlMensajePOS.TabIndex = 5;
            this.pnlMensajePOS.Visible = false;
            // 
            // FrmImpresionFallidaComun
            // 
            this.AcceptButton = this.btnReimprimirCupones;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(696, 203);
            this.ControlBox = false;
            this.Controls.Add(this.pnlMensajePOS);
            this.Controls.Add(this.grpAcciones);
            this.Controls.Add(this.lblMensajeFinal);
            this.Controls.Add(this.pnlMensaje);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.KeyPreview = true;
            this.Name = "FrmImpresionFallidaComun";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Impresión Fallida";
            this.pnlMensaje.ResumeLayout(false);
            this.pnlMensaje.PerformLayout();
            this.grpAcciones.ResumeLayout(false);
            this.pnlMensajePOS.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMensaje;
        private System.Windows.Forms.Label lblMensaje;
        private System.Windows.Forms.Label lblMensajeFinal;
        private System.Windows.Forms.GroupBox grpAcciones;
        private System.Windows.Forms.Button btnCambiarImpresora;
        private System.Windows.Forms.Button btnReimprimirCupones;
        private System.Windows.Forms.Label lblPOS;
        private System.Windows.Forms.Panel pnlMensajePOS;
        private System.Windows.Forms.Button btnReversarTrans;
    }
}