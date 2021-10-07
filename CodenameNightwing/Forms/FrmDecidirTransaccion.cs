using System;
using System.Windows.Forms;

namespace CodenameNightwing.Forms
{
    public partial class FrmDecidirTransaccion : Form
    {
        private bool _opcionElegida;
        public bool opcionElegida
        {
            get { return _opcionElegida; }
            set { _opcionElegida = value; }
        }

        public FrmDecidirTransaccion()
        {
            InitializeComponent();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (rdbNo.Checked || rdbSi.Checked)
            {
                if (rdbSi.Checked)
                    opcionElegida = true;
                else
                    opcionElegida = false;
                Close();
            }
            else
            {
                MessageBox.Show("Debe seleccionar una opción", "Seleccione una opción", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
