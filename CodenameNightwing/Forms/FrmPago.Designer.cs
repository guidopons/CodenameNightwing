namespace CodenameNightwing.Forms
{
    partial class FrmPago
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
            this.grpAutorizar = new System.Windows.Forms.GroupBox();
            this.txtTotal = new System.Windows.Forms.Label();
            this.lblTotalAPagar = new System.Windows.Forms.Label();
            this.treeAutorizar = new System.Windows.Forms.TreeView();
            this.grpAutorizado = new System.Windows.Forms.GroupBox();
            this.txtFaltaAutorizar = new System.Windows.Forms.Label();
            this.lblFaltaAutorizar = new System.Windows.Forms.Label();
            this.treeAutorizado = new System.Windows.Forms.TreeView();
            this.grpAcciones = new System.Windows.Forms.GroupBox();
            this.btnOtras = new System.Windows.Forms.Button();
            this.btnVolver = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnOtros = new System.Windows.Forms.Button();
            this.btnDebito = new System.Windows.Forms.Button();
            this.btnCreditoExt = new System.Windows.Forms.Button();
            this.btnCreditoArg = new System.Windows.Forms.Button();
            this.lblMensaje = new System.Windows.Forms.Label();
            this.pnlMensaje = new System.Windows.Forms.Panel();
            this.lblPriceInformation = new System.Windows.Forms.Label();
            this.grpAutorizar.SuspendLayout();
            this.grpAutorizado.SuspendLayout();
            this.grpAcciones.SuspendLayout();
            this.pnlMensaje.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpAutorizar
            // 
            this.grpAutorizar.Controls.Add(this.txtTotal);
            this.grpAutorizar.Controls.Add(this.lblTotalAPagar);
            this.grpAutorizar.Controls.Add(this.treeAutorizar);
            this.grpAutorizar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpAutorizar.Location = new System.Drawing.Point(12, 12);
            this.grpAutorizar.Name = "grpAutorizar";
            this.grpAutorizar.Size = new System.Drawing.Size(323, 310);
            this.grpAutorizar.TabIndex = 0;
            this.grpAutorizar.TabStop = false;
            this.grpAutorizar.Text = "Importes a autorizar";
            // 
            // txtTotal
            // 
            this.txtTotal.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotal.Location = new System.Drawing.Point(229, 280);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.Size = new System.Drawing.Size(88, 21);
            this.txtTotal.TabIndex = 2;
            this.txtTotal.Text = "label1";
            this.txtTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotalAPagar
            // 
            this.lblTotalAPagar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalAPagar.Location = new System.Drawing.Point(6, 280);
            this.lblTotalAPagar.Name = "lblTotalAPagar";
            this.lblTotalAPagar.Size = new System.Drawing.Size(109, 21);
            this.lblTotalAPagar.TabIndex = 1;
            this.lblTotalAPagar.Text = "Total a pagar:";
            this.lblTotalAPagar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // treeAutorizar
            // 
            this.treeAutorizar.Location = new System.Drawing.Point(6, 19);
            this.treeAutorizar.Name = "treeAutorizar";
            this.treeAutorizar.Size = new System.Drawing.Size(311, 258);
            this.treeAutorizar.TabIndex = 0;
            // 
            // grpAutorizado
            // 
            this.grpAutorizado.Controls.Add(this.txtFaltaAutorizar);
            this.grpAutorizado.Controls.Add(this.lblFaltaAutorizar);
            this.grpAutorizado.Controls.Add(this.treeAutorizado);
            this.grpAutorizado.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpAutorizado.Location = new System.Drawing.Point(341, 12);
            this.grpAutorizado.Name = "grpAutorizado";
            this.grpAutorizado.Size = new System.Drawing.Size(323, 310);
            this.grpAutorizado.TabIndex = 1;
            this.grpAutorizado.TabStop = false;
            this.grpAutorizado.Text = "Cobros autorizados";
            // 
            // txtFaltaAutorizar
            // 
            this.txtFaltaAutorizar.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtFaltaAutorizar.Location = new System.Drawing.Point(232, 280);
            this.txtFaltaAutorizar.Name = "txtFaltaAutorizar";
            this.txtFaltaAutorizar.Size = new System.Drawing.Size(85, 21);
            this.txtFaltaAutorizar.TabIndex = 3;
            this.txtFaltaAutorizar.Text = "label1";
            this.txtFaltaAutorizar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblFaltaAutorizar
            // 
            this.lblFaltaAutorizar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFaltaAutorizar.Location = new System.Drawing.Point(6, 280);
            this.lblFaltaAutorizar.Name = "lblFaltaAutorizar";
            this.lblFaltaAutorizar.Size = new System.Drawing.Size(137, 21);
            this.lblFaltaAutorizar.TabIndex = 2;
            this.lblFaltaAutorizar.Text = "Falta autorizar:";
            this.lblFaltaAutorizar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // treeAutorizado
            // 
            this.treeAutorizado.CheckBoxes = true;
            this.treeAutorizado.Location = new System.Drawing.Point(6, 19);
            this.treeAutorizado.Name = "treeAutorizado";
            this.treeAutorizado.Size = new System.Drawing.Size(311, 258);
            this.treeAutorizado.TabIndex = 1;
            this.treeAutorizado.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeAutorizado_AfterCheck);
            this.treeAutorizado.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeAutorizado_AfterSelect);
            // 
            // grpAcciones
            // 
            this.grpAcciones.Controls.Add(this.btnOtras);
            this.grpAcciones.Controls.Add(this.btnVolver);
            this.grpAcciones.Controls.Add(this.btnEliminar);
            this.grpAcciones.Controls.Add(this.btnOtros);
            this.grpAcciones.Controls.Add(this.btnDebito);
            this.grpAcciones.Controls.Add(this.btnCreditoExt);
            this.grpAcciones.Controls.Add(this.btnCreditoArg);
            this.grpAcciones.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpAcciones.Location = new System.Drawing.Point(670, 12);
            this.grpAcciones.Name = "grpAcciones";
            this.grpAcciones.Size = new System.Drawing.Size(107, 310);
            this.grpAcciones.TabIndex = 2;
            this.grpAcciones.TabStop = false;
            this.grpAcciones.Text = "Acciones";
            this.grpAcciones.Enter += new System.EventHandler(this.grpAcciones_Enter);
            // 
            // btnOtras
            // 
            this.btnOtras.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnOtras.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOtras.Location = new System.Drawing.Point(6, 226);
            this.btnOtras.Name = "btnOtras";
            this.btnOtras.Size = new System.Drawing.Size(95, 36);
            this.btnOtras.TabIndex = 6;
            this.btnOtras.Text = "O&tras Operaciones";
            this.btnOtras.UseVisualStyleBackColor = true;
            this.btnOtras.Click += new System.EventHandler(this.btnOtras_Click);
            // 
            // btnVolver
            // 
            this.btnVolver.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnVolver.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVolver.Location = new System.Drawing.Point(6, 268);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(95, 36);
            this.btnVolver.TabIndex = 5;
            this.btnVolver.Text = "&Volver a Interact";
            this.btnVolver.UseVisualStyleBackColor = true;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Enabled = false;
            this.btnEliminar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEliminar.Location = new System.Drawing.Point(6, 187);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(95, 36);
            this.btnEliminar.TabIndex = 4;
            this.btnEliminar.Text = "Eliminar A&utorizacion";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnOtros
            // 
            this.btnOtros.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOtros.Location = new System.Drawing.Point(6, 145);
            this.btnOtros.Name = "btnOtros";
            this.btnOtros.Size = new System.Drawing.Size(95, 36);
            this.btnOtros.TabIndex = 3;
            this.btnOtros.Text = "Agregar &Otros";
            this.btnOtros.UseVisualStyleBackColor = true;
            this.btnOtros.Click += new System.EventHandler(this.btnOtros_Click);
            // 
            // btnDebito
            // 
            this.btnDebito.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDebito.Location = new System.Drawing.Point(6, 103);
            this.btnDebito.Name = "btnDebito";
            this.btnDebito.Size = new System.Drawing.Size(95, 36);
            this.btnDebito.TabIndex = 2;
            this.btnDebito.Text = "Tarj. &Débito";
            this.btnDebito.UseVisualStyleBackColor = true;
            this.btnDebito.Click += new System.EventHandler(this.btnDebito_Click);
            // 
            // btnCreditoExt
            // 
            this.btnCreditoExt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreditoExt.Location = new System.Drawing.Point(6, 61);
            this.btnCreditoExt.Name = "btnCreditoExt";
            this.btnCreditoExt.Size = new System.Drawing.Size(95, 36);
            this.btnCreditoExt.TabIndex = 1;
            this.btnCreditoExt.Text = "Tarj. Crédito &Extranjeras";
            this.btnCreditoExt.UseVisualStyleBackColor = true;
            this.btnCreditoExt.Click += new System.EventHandler(this.btnCreditoExt_Click);
            // 
            // btnCreditoArg
            // 
            this.btnCreditoArg.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreditoArg.Location = new System.Drawing.Point(6, 19);
            this.btnCreditoArg.Name = "btnCreditoArg";
            this.btnCreditoArg.Size = new System.Drawing.Size(95, 36);
            this.btnCreditoArg.TabIndex = 0;
            this.btnCreditoArg.Text = "Tarj. Crédito &Argentinas";
            this.btnCreditoArg.UseVisualStyleBackColor = true;
            this.btnCreditoArg.Click += new System.EventHandler(this.btnCreditoArg_Click);
            // 
            // lblMensaje
            // 
            this.lblMensaje.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMensaje.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMensaje.Location = new System.Drawing.Point(0, 0);
            this.lblMensaje.Name = "lblMensaje";
            this.lblMensaje.Size = new System.Drawing.Size(652, 71);
            this.lblMensaje.TabIndex = 3;
            this.lblMensaje.Text = "Obteniendo Información...";
            this.lblMensaje.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblMensaje.Click += new System.EventHandler(this.lblMensaje_Click);
            // 
            // pnlMensaje
            // 
            this.pnlMensaje.BackColor = System.Drawing.Color.Aqua;
            this.pnlMensaje.Controls.Add(this.lblMensaje);
            this.pnlMensaje.Location = new System.Drawing.Point(14, 98);
            this.pnlMensaje.Name = "pnlMensaje";
            this.pnlMensaje.Size = new System.Drawing.Size(652, 71);
            this.pnlMensaje.TabIndex = 3;
            this.pnlMensaje.Visible = false;
            // 
            // lblPriceInformation
            // 
            this.lblPriceInformation.AutoSize = true;
            this.lblPriceInformation.Location = new System.Drawing.Point(227, 327);
            this.lblPriceInformation.Name = "lblPriceInformation";
            this.lblPriceInformation.Size = new System.Drawing.Size(109, 13);
            this.lblPriceInformation.TabIndex = 4;
            this.lblPriceInformation.Text = "label price information";
            // 
            // FrmPago
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnVolver;
            this.ClientSize = new System.Drawing.Size(791, 349);
            this.ControlBox = false;
            this.Controls.Add(this.lblPriceInformation);
            this.Controls.Add(this.pnlMensaje);
            this.Controls.Add(this.grpAcciones);
            this.Controls.Add(this.grpAutorizado);
            this.Controls.Add(this.grpAutorizar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmPago";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Autorizador Interact - POS Integrado";
            this.Load += new System.EventHandler(this.FrmPago_Load);
            this.grpAutorizar.ResumeLayout(false);
            this.grpAutorizado.ResumeLayout(false);
            this.grpAcciones.ResumeLayout(false);
            this.pnlMensaje.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpAutorizar;
        private System.Windows.Forms.Label txtTotal;
        private System.Windows.Forms.Label lblTotalAPagar;
        private System.Windows.Forms.TreeView treeAutorizar;
        private System.Windows.Forms.GroupBox grpAutorizado;
        private System.Windows.Forms.Label lblFaltaAutorizar;
        private System.Windows.Forms.TreeView treeAutorizado;
        private System.Windows.Forms.GroupBox grpAcciones;
        private System.Windows.Forms.Label txtFaltaAutorizar;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnOtros;
        private System.Windows.Forms.Button btnDebito;
        private System.Windows.Forms.Button btnCreditoExt;
        private System.Windows.Forms.Button btnCreditoArg;
        private System.Windows.Forms.Label lblMensaje;
        private System.Windows.Forms.Panel pnlMensaje;
        private System.Windows.Forms.Button btnOtras;
        private System.Windows.Forms.Label lblPriceInformation;
    }
}