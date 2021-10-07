using CodenameNightwing.Autorization;
using CodenameNightwing.BusinessLogic;
using CodenameNightwing.Config;
using CodenameNightwing.FileManager;
using System;
using System.Windows.Forms;

namespace CodenameNightwing.Forms
{
    public partial class FrmCierreLote : Form
    {
        private Transaccion _tran;
        public Transaccion tran
        {
            get { return _tran; }
            set { _tran = value; }
        }

        public FrmCierreLote()
        {
            InitializeComponent();
            Icon = Properties.Resources.logo_con_borde_grueso;
        }

        private void btnCerrarLote_Click(object sender, EventArgs e)
        {
            if (Configuration.getInstance().tipoAuth == TipoAutorizador.POS_INGENICO)
                cerrarLote();
            else
            {
                MessageBox.Show("No es necesario realizar cierre de lote con autorizador por red", "Operacion innecesaria", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
        }

        private void cerrarLote()
        {
            btnCerrarLote.Enabled = false;
            tran = AutorizatorFactory.getAutorizator().realizarTransaccion(TransaccionBuilder.construirCierreLote());
            if (tran != null)
            {
                PaymentFormWriter.grabarCierreLote(true);
                MessageBox.Show(this, "El cierre de lote se realizó correctamente.", "Cierre de lote exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                PaymentFormWriter.grabarCierreLote(false);
            Close();
        }
    }
}
