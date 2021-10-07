using CodenameNightwing.Autorization;
using CodenameNightwing.Autorization.VTOL;
using CodenameNightwing.BusinessLogic;
using CodenameNightwing.Varios;
using System;
using System.Windows.Forms;

namespace CodenameNightwing.Forms
{
    public partial class FrmImpresionFallidaComun : Form
    {

        private TipoTransImpFallida _estadoTransFallida = TipoTransImpFallida.NO_COMPLETADA;



        public TipoTransImpFallida estadoTransFallida
        {
            get { return _estadoTransFallida; }
            set { _estadoTransFallida = value; }
        }

        public FrmImpresionFallidaComun()
        {
            InitializeComponent();
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
            frmChangePrinter.Show( this );
        }

        private void btnReversarTrans_Click(object sender, EventArgs e)
        {
            estadoTransFallida = TipoTransImpFallida.TRANS_REVERSADA;
            Close();
        }

        private void lblMensaje_Click(object sender, EventArgs e)
        {

        }

        private void lblMensajeFinal_Click(object sender, EventArgs e)
        {

        }
    }
}
