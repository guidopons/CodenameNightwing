using CodenameNightwing.Autorization;
using CodenameNightwing.Autorization.VTOL;
using CodenameNightwing.BusinessLogic;
using log4net;
using System;
using System.Windows.Forms;

namespace CodenameNightwing.Forms
{
    public partial class FrmAutorizacionTelefonica : Form
    {

        public static readonly ILog logger = LogManager.GetLogger(typeof(FrmAutorizacionTelefonica));

        private Transaccion _tran;
        public Transaccion tran
        {
            get { return _tran; }
            set { _tran = value; }
        }

        private FrmAutorizacionTelefonica()
        {
            InitializeComponent();
        }

        public FrmAutorizacionTelefonica(Transaccion aComputar )
        {
            InitializeComponent();
            Icon = Properties.Resources.logo_con_borde_grueso;
            tran = aComputar;
            string txtMsgPrincipal =  "";
            switch ( aComputar.tipoTrans )
            {
                case TipoTransaccion.COMPRA:
                    txtMsgPrincipal = "LA TARJETA SOLICITA PEDIR AUTORIZACION TELEFONICA DE LA COMPRA";
                    break;
                case TipoTransaccion.DEVOLUCION:
                    txtMsgPrincipal = "LA TARJETA SOLICITA PEDIR AUTORIZACION TELEFONICA DE LA DEVOLUCION";
                    break;
                case TipoTransaccion.ANULACION:
                    txtMsgPrincipal = "LA TARJETA SOLICITA PEDIR AUTORIZACION TELEFONICA DE LA ANULACION";
                    break;
                
            }
            lblMensaje.Text = txtMsgPrincipal;
            if (tran.tipoAuth != TipoAutorizador.POS_INGENICO)
                lblMensajeFinal.Text =
                    @"* Anote el código obtenido telefónicamente ya que lo necesitará posteriormente
                    * Luego presione ""Autorizar OFFLINE"" y se repetirá la operación en modo offline.
                             Por último se le solicitará que ingrese el código obtenido.
                    *Si la operadora en lugar de un código le solicitó que repita la operación
                            presione ""Autorizar ONLINE"" y se repetirá la operación normalmente.
                    *Si no va a solicitar autorización telefónica presione ""Volver sin autorizar""";

            if (aComputar.tipoIngreso.Equals(TipoIngresoTarjeta.EMV))
            {
                btnAutorizarOffline.Enabled = false;
            }
            else
            {
                btnAutorizarOffline.Enabled = true;
            }

        }

        private void FrmAutorizacionTelefonica_Load(object sender, EventArgs e)
        {
            txtCodigoComercio.Text = tran.comercio.codigoComercio;
            txtCuotas.Text = tran.cantCuotas.ToString();
            txtImporteAAutorizar.Text = tran.importeTotal.ToString("#####0.00");

            tran.eventHandler += this.handleEventTransaction;

            if (!btnAutorizarOffline.Enabled) {
                MessageBox.Show("Las tarjetas con Chip no permiten operaciones Offline. Llame y pida liberar la banda  o repetir la operacion", "Operacion Offline con CHIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void mostrarCartel()
        {
            pnlMensajePOS.Visible = true;
            pnlMensajePOS.Refresh();
        }

        private void btnAutorizarOffline_Click(object sender, EventArgs e)
        {
            autorizar(TipoModoTransaccion.OFFLINE );
        }

        private void btnAutorizarOnline_Click(object sender, EventArgs e)
        {
            autorizar( TipoModoTransaccion.ONLINE );
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


        private void autorizar( TipoModoTransaccion modoTrans)
        {
            try
            {
                Autorizator auth = AutorizatorFactory.getAutorizator( tran );
                tran.modo = modoTrans;
                if (tran.tipoAuth != TipoAutorizador.POS_INGENICO && TipoModoTransaccion.OFFLINE.Equals(modoTrans))
                {
                    FrmIngresarCodigoAutorizacion auxFrm = new FrmIngresarCodigoAutorizacion(tran);
                    auxFrm.ShowDialog();
                    if (!auxFrm.isCorrect())
                    {
                        return;
                    }
                    tran = auxFrm.tran;
                }

                mostrarCartel();

                if (tran.tipoAuth == TipoAutorizador.VTOL)
                {
                    Autorizator auxAuth = VTOLIntegrator.Instance;
                    string nameTrans = auxAuth.getNombreTransaccion(tran);
                    if (tran.tipoTrans == TipoTransaccion.COMPRA)
                    {
                        Tarjeta tar = auxAuth.solicitarNumeroTarjeta(nameTrans, tran);
                        if (tar != null)
                        {
                            tran = auth.realizarTransaccion(tran);
                        }
                    }
                    else
                    {
                        tran = auth.realizarTransaccion(tran);
                    }
                }
                else
                {
                    tran = auth.realizarTransaccion(tran);
                }


                Close();
            }
            catch ( Exception e)
            {
                logger.Error("Error al autorizar telefonica", e);
                MessageBox.Show("Error inesperado en autorizacion telefonica", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            tran = null;
            Close();
        }

        private void txtCodigoComercio_Click(object sender, EventArgs e)
        {

        }

        private void lblMensajeInicial_Click(object sender, EventArgs e)
        {

        }

        private void lblMensajeFinal_Click(object sender, EventArgs e)
        {

        }
    }
}
