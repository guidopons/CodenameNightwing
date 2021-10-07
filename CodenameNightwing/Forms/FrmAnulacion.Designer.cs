namespace CodenameNightwing.Forms
{
    partial class FrmAnulacion
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
            this.lblNumero = new System.Windows.Forms.Label();
            this.lblTicketAnular = new System.Windows.Forms.Label();
            this.pnlMensaje = new System.Windows.Forms.Panel();
            this.lblMensaje = new System.Windows.Forms.Label();
            this.txtNumero = new System.Windows.Forms.Label();
            this.txtTicket = new System.Windows.Forms.Label();
            this.pnlMensaje.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblNumero
            // 
            this.lblNumero.AutoSize = true;
            this.lblNumero.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumero.Location = new System.Drawing.Point(8, 12);
            this.lblNumero.Name = "lblNumero";
            this.lblNumero.Size = new System.Drawing.Size(242, 24);
            this.lblNumero.TabIndex = 0;
            this.lblNumero.Text = "Tarjeta...................................";
            this.lblNumero.Click += new System.EventHandler(this.lblNumero_Click);
            // 
            // lblTicketAnular
            // 
            this.lblTicketAnular.AutoSize = true;
            this.lblTicketAnular.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTicketAnular.Location = new System.Drawing.Point(8, 60);
            this.lblTicketAnular.Name = "lblTicketAnular";
            this.lblTicketAnular.Size = new System.Drawing.Size(242, 24);
            this.lblTicketAnular.TabIndex = 1;
            this.lblTicketAnular.Text = "Ticket a anular......................";
            // 
            // pnlMensaje
            // 
            this.pnlMensaje.BackColor = System.Drawing.Color.Aqua;
            this.pnlMensaje.Controls.Add(this.lblMensaje);
            this.pnlMensaje.Location = new System.Drawing.Point(16, 96);
            this.pnlMensaje.Name = "pnlMensaje";
            this.pnlMensaje.Size = new System.Drawing.Size(490, 48);
            this.pnlMensaje.TabIndex = 4;
            // 
            // lblMensaje
            // 
            this.lblMensaje.AutoSize = true;
            this.lblMensaje.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMensaje.Location = new System.Drawing.Point(14, 12);
            this.lblMensaje.Name = "lblMensaje";
            this.lblMensaje.Size = new System.Drawing.Size(223, 24);
            this.lblMensaje.TabIndex = 0;
            this.lblMensaje.Text = "Obteniendo Información..";
            // 
            // txtNumero
            // 
            this.txtNumero.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtNumero.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtNumero.Location = new System.Drawing.Point(300, 13);
            this.txtNumero.Name = "txtNumero";
            this.txtNumero.Size = new System.Drawing.Size(202, 23);
            this.txtNumero.TabIndex = 5;
            // 
            // txtTicket
            // 
            this.txtTicket.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtTicket.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtTicket.Location = new System.Drawing.Point(300, 60);
            this.txtTicket.Name = "txtTicket";
            this.txtTicket.Size = new System.Drawing.Size(202, 23);
            this.txtTicket.TabIndex = 6;
            // 
            // FrmAnulacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(518, 155);
            this.ControlBox = false;
            this.Controls.Add(this.txtTicket);
            this.Controls.Add(this.txtNumero);
            this.Controls.Add(this.pnlMensaje);
            this.Controls.Add(this.lblTicketAnular);
            this.Controls.Add(this.lblNumero);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "FrmAnulacion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Anulacion de autorización Débito/Crédito - POS Integrado";
            this.Load += new System.EventHandler(this.FrmAnulacion_Load);
            this.Shown += new System.EventHandler(this.FrmAnulacion_Shown);
            this.pnlMensaje.ResumeLayout(false);
            this.pnlMensaje.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblNumero;
        private System.Windows.Forms.Label lblTicketAnular;
        private System.Windows.Forms.Panel pnlMensaje;
        private System.Windows.Forms.Label lblMensaje;
        private System.Windows.Forms.Label txtNumero;
        private System.Windows.Forms.Label txtTicket;
    }
}