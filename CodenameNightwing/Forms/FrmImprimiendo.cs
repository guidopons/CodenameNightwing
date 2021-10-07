using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CodenameNightwing.Forms
{
    public partial class FrmImprimiendo : Form
    {
        public FrmImprimiendo()
        {
            InitializeComponent();
            Icon = Properties.Resources.logo_con_borde_grueso;
        }

        private void FrmImprimiendo_Load(object sender, EventArgs e)
        {
            lblMensaje.Text = "Imprimiendo...";
            mostrarCartel();
            
        }

        private void mostrarCartel()
        {
            pnlMensaje.Visible = true;
            pnlMensaje.Refresh();
        }
    }
}
