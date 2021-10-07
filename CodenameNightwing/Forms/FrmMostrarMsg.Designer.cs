namespace CodenameNightwing.Forms
{
    partial class FrmMostrarMsg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMostrarMsg));
            this.pnlMensaje = new System.Windows.Forms.Panel();
            this.imageWaitCCNumber = new System.Windows.Forms.PictureBox();
            this.lblMensaje = new System.Windows.Forms.Label();
            this.pnlMensaje.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageWaitCCNumber)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMensaje
            // 
            this.pnlMensaje.BackColor = System.Drawing.Color.Aqua;
            this.pnlMensaje.Controls.Add(this.imageWaitCCNumber);
            this.pnlMensaje.Controls.Add(this.lblMensaje);
            this.pnlMensaje.Location = new System.Drawing.Point(1, 0);
            this.pnlMensaje.Name = "pnlMensaje";
            this.pnlMensaje.Size = new System.Drawing.Size(429, 74);
            this.pnlMensaje.TabIndex = 6;
            this.pnlMensaje.UseWaitCursor = true;
            this.pnlMensaje.Visible = false;
            // 
            // imageWaitCCNumber
            // 
            this.imageWaitCCNumber.Image = ((System.Drawing.Image)(resources.GetObject("imageWaitCCNumber.Image")));
            this.imageWaitCCNumber.InitialImage = ((System.Drawing.Image)(resources.GetObject("imageWaitCCNumber.InitialImage")));
            this.imageWaitCCNumber.Location = new System.Drawing.Point(48, 20);
            this.imageWaitCCNumber.Name = "imageWaitCCNumber";
            this.imageWaitCCNumber.Size = new System.Drawing.Size(31, 37);
            this.imageWaitCCNumber.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imageWaitCCNumber.TabIndex = 16;
            this.imageWaitCCNumber.TabStop = false;
            this.imageWaitCCNumber.UseWaitCursor = true;
            // 
            // lblMensaje
            // 
            this.lblMensaje.AutoSize = true;
            this.lblMensaje.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMensaje.Location = new System.Drawing.Point(109, 28);
            this.lblMensaje.Name = "lblMensaje";
            this.lblMensaje.Size = new System.Drawing.Size(143, 20);
            this.lblMensaje.TabIndex = 0;
            this.lblMensaje.Text = "Mostrar Mensaje....";
            this.lblMensaje.UseWaitCursor = true;
            this.lblMensaje.Click += new System.EventHandler(this.lblMensaje_Click);
            // 
            // FrmMostrarMsg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(422, 71);
            this.ControlBox = false;
            this.Controls.Add(this.pnlMensaje);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmMostrarMsg";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mensaje a Mostrar";
            this.UseWaitCursor = true;
            this.Load += new System.EventHandler(this.FrmMostrarMsg_Load);
            this.pnlMensaje.ResumeLayout(false);
            this.pnlMensaje.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageWaitCCNumber)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel pnlMensaje;
        private System.Windows.Forms.Label lblMensaje;
        private System.Windows.Forms.PictureBox imageWaitCCNumber;
    }
}