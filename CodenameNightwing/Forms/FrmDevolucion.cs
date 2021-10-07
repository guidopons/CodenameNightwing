using System;
using System.Linq;
using System.Windows.Forms;
using CodenameNightwing.Varios;
using CodenameNightwing.WebServices.WSEspecificos;
using CodenameNightwing.WebServices;
using CodenameNightwing.Autorization;
using CodenameNightwing.FileManager;
using CodenameNightwing.BusinessLogic;
using CodenameNightwing.Config;
using CodenameNightwing.WebServices.WSEspecificos.Transacciones;

namespace CodenameNightwing.Forms
{
    public partial class FrmDevolucion : Form
    {
        private Transaccion _tran;
        public Transaccion tran
        {
            get { return _tran; }
            set { _tran = value; }
        }

        public FrmDevolucion()
        {
            InitializeComponent();
            Icon = Properties.Resources.logo_con_borde_grueso;
        }

        private void FrmDevolucion_Load(object sender, EventArgs e)
        {
            tran = VBrequestReader.leerRefund();
            tran.eventHandler += this.handleEventTransaction;
            lblPriceInformation.Text = tran.getPriceInformation();
            TarjetaCajero auxTC = null;
            bool continuar = false;
            if (Configuration.getInstance().tipoAuth == TipoAutorizador.POS_INGENICO)
            {
                if (EntityLoader.loadTarjetas().Count(x => x.codTarjetaSabre == tran.tarjeta.codSabre) > 1)
                {
                    WebServiceComercio wsCom = new WebServiceComercio(tran.tarjeta.codSabre, "", tran.tarjeta.primeros6());
                    Comercio auxWsCom = WebResponseParser.parseXMLComercio(wsCom.getResponse());
                    auxTC = EntityLoader.loadTarjetas().First(x => x.codTarjetaSabre == tran.tarjeta.codSabre && x.codComercio == auxWsCom.codigoComercio);
                }
                else
                    auxTC = EntityLoader.loadTarjetas().First(x => x.codTarjetaSabre == tran.tarjeta.codSabre);
                if (auxTC != null)
                {
                    Comercio auxCom = new Comercio();
                    auxCom.codigoComercio = auxTC.codComercio;
                    tran.comercio = auxCom;
                    Tarjeta auxtar = tran.tarjeta;
                    tran.tarjeta = auxTC.ToTarjeta();
                    tran.tarjeta.numero = auxtar.numero;
                    continuar = true;
                }
            }
            else
            {
                WebServiceGetTransaction ws = new WebServiceGetTransaction(tran.trxReferenceIdOriginal);
                Transaccion aux = WebResponseParser.parseXMLGetTransaction(ws.getResponse());
                if (aux == null)
                {
                    aux = AnulationFile.buscarTransaccion(tran.trxReferenceId);
                    if (aux != null)
                        continuar = true;
                }
                else
                    continuar = true;
                if (continuar)
                {
                    tran.trxIdOriginal = aux.trxId;
                    tran.ticketOriginal = aux.numTicket;
                    tran.fechaOriginal = aux.fecha;
                    tran.cantCuotas = aux.cantCuotas;
                    tran.tarjeta.numero = aux.tarjeta.numero;
                    tran.trxId = aux.trxId;
                    tran.currency = aux.currency;
                }
            }
            if (continuar)
            {
                txtFecha.Text = tran.fechaOriginal.ToShortDateString();
                txtNumeroCupon.Text = tran.ticketOriginal;
                txtNumeroTarjeta.Text = tran.tarjeta.numero;
                txtTipoTarjeta.Text = tran.tarjeta.tipoTarjeta == TipoTarjeta.CREDITO ? "Crédito" : "Débito";
                txtImporte.Text = tran.importeTotal.ToString("#####0.00");
            }
            else
            {
                MessageBox.Show("Error cargando datos para devolucion. Revise si la operación original se encuentra y no está anulada", "Error en carga", MessageBoxButtons.OK, MessageBoxIcon.Error);
                PaymentFormWriter.grabarVoid(null);
                Close();
            }
        }

        //private Comercio cargarCodComercio(Transaccion aux)
        //{
        //    WebServiceComercio wsCom = new WebServiceComercio(aux.tarjeta.codTarjeta, "EZE", aux.tarjeta.primeros6());
        //    wsCom.sendRequest();
        //    return WebResponseParser.parseXMLComercio(wsCom.getResponse());
        //}

        private void btnAutorizar_Click(object sender, EventArgs e)
        {
            autorizar();
        }

        private void autorizar()
        {
            mostrarCartel();
            Autorizator auth = AutorizatorFactory.getAutorizator( tran );
            Transaccion resultado = null;
            if (auth != null)
                 resultado = auth.realizarTransaccion(tran);
            if (resultado != null)
            {
                POIutils.updateTarjetaFromBin(resultado.tarjeta.primeros6(), resultado.tarjeta);
                if (Configuration.getInstance().tipoAuth == TipoAutorizador.VTOL) { 
                    FrmNumerosRestantes frmIngresoNumeros = new FrmNumerosRestantes(resultado.tarjeta);
                    frmIngresoNumeros.ShowDialog();
                    resultado.tarjeta.numero = resultado.tarjeta.numero.Replace("\\D+", frmIngresoNumeros.numResTarjeta);
                }

                if ( resultado!= null && resultado.isAprobada()) { 
                    lblMensaje.Text = "Pasando información a Interact..";
                    lblMensaje.Refresh();
                    mostrarCartel();
                    PaymentFormWriter.grabarRefund(resultado);
                    Close();
                }

            }
            pnlMensaje.Visible = false;
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            PaymentFormWriter.grabarRefund(null);
            Close();
        }

        private void mostrarCartel()
        {
            pnlMensaje.Visible = true;
            pnlMensaje.Refresh();
        }


        public void handleEventTransaction(object sender, EventArgs args)
        {
            Transaccion tran = (Transaccion)sender;
            string estadoDesc = tran.getEstadoDescription();
            if (estadoDesc != null)
            {
                lblMensaje.Text = estadoDesc;
                lblMensaje.Refresh();
                mostrarCartel();
            }
        }

    }
}
