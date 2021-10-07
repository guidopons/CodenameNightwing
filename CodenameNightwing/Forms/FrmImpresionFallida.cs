using CodenameNightwing.Autorization;
using CodenameNightwing.Autorization.VTOL;
using CodenameNightwing.BusinessLogic;
using CodenameNightwing.Varios;
using System;
using System.Windows.Forms;

namespace CodenameNightwing.Forms
{
    public partial class FrmImpresionFallida : Form
    {
        private Transaccion _tran;
        private TipoTransImpFallida _estadoTransFallida = TipoTransImpFallida.NO_COMPLETADA;

        public Transaccion tran
        {
            get { return _tran; }
            set { _tran = value; }
        }

        public TipoTransImpFallida estadoTransFallida
        {
            get { return _estadoTransFallida; }
            set { _estadoTransFallida = value; }
        }

        private FrmImpresionFallida()
        {
            InitializeComponent();
        }

        public FrmImpresionFallida(Transaccion aImprimir )
        {

            InitializeComponent();
            Icon = Properties.Resources.logo_con_borde_grueso;
            tran = aImprimir;

        }

        private void FrmAutorizacionTelefonica_Load(object sender, EventArgs e)
        {
            tran.eventHandler += this.handleEventTransaction;
        }

        private void mostrarCartel()
        {
            pnlMensajePOS.Visible = true;
            pnlMensajePOS.Refresh();
        }

        private void btnReimprimirCupones_Click(object sender, EventArgs e)
        {
            estadoTransFallida = TipoTransImpFallida.REIMPRESION;
            Close();
        }

        private void btnAnularTrans_Click(object sender, EventArgs e)
        {
            FrmAnulacion frmAnulacion = new FrmAnulacion(this.tran);
            frmAnulacion.Show();
            if ( frmAnulacion.isAnulationComplete)
            {
                estadoTransFallida = TipoTransImpFallida.ANULACION_OK;
                this.Close();
            }
            else
            {
                estadoTransFallida = TipoTransImpFallida.NO_COMPLETADA;
            }
        }

        public void handleEventTransaction(object sender, EventArgs args)
        {
            Transaccion tran = (Transaccion)sender;
            string estadoDesc = tran.getEstadoDescription();
            if (estadoDesc != null)
            {
                lblPOS.Text = estadoDesc;
                lblPOS.Refresh();
                mostrarCartel();
            }
        }

        private void btnCambiarImpresora_Click(object sender, EventArgs e)
        {
            FrmChangePrinter frmChangePrinter = new FrmChangePrinter(false);
            frmChangePrinter.Show();
        }

        private void btnReversarTrans_Click(object sender, EventArgs e)
        {

            bool confirmaReverso = false;

            DialogResult opcion;

            using (new CenterWinDialog(this))
            {
                opcion = MessageBox.Show("Confirma el Reverso de la transacción?", "El reverso se dará en el cierre de lote", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            }

            if (opcion == DialogResult.Yes)
                confirmaReverso = true;
            else
                confirmaReverso = false;

            if ( confirmaReverso)
            {
                estadoTransFallida = TipoTransImpFallida.TRANS_REVERSADA;
                Close();
            }
            
        }
    }
}
