using System;
using System.Linq;
using System.Windows.Forms;
using CodenameNightwing.Varios;
using CodenameNightwing.FileManager;
using CodenameNightwing.WebServices;
using CodenameNightwing.WebServices.WSEspecificos;
using CodenameNightwing.Autorization;
using CodenameNightwing.BusinessLogic;
using CodenameNightwing.Config;
using CodenameNightwing.WebServices.WSEspecificos.Transacciones;
using log4net;

namespace CodenameNightwing.Forms
{
    public partial class FrmAnulacion : Form
    {

        public static readonly ILog logger = LogManager.GetLogger(typeof(FrmAnulacion));

        private bool _isAnulationComplete = false;
        public bool isAnulationComplete
        {
            get { return _isAnulationComplete; }
            set { _isAnulationComplete = value; }
        }

        private Transaccion _tran = null;
        public Transaccion tran
        {
            get { return _tran; }
            set { _tran = value; }
        }

        public FrmAnulacion()
        {
            InitializeComponent();
            Icon = Properties.Resources.logo_con_borde_grueso;
        }

        public FrmAnulacion( Transaccion trans):this()
        {
            this.tran = trans;
        }

        private void FrmAnulacion_Shown(object sender, EventArgs e)
        {
            this.Refresh();
            pnlMensaje.Visible = true;
            try
            {
                bool continuar = false;


                if ( POIutils.isVtolNpsAutorizator() )
                {
                    WebServiceGetTransaction ws = new WebServiceGetTransaction(tran.trxReferenceIdOriginal);
                    Transaccion aux = WebResponseParser.parseXMLGetTransaction(ws.getResponse());
                    if (aux == null)
                    {
                        aux = AnulationFile.buscarTransaccion(tran.trxReferenceIdOriginal);
                        if (aux == null)
                        {
                            MessageBox.Show("No se pudo encontrar la transaccion a anular", "Error al anular", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            PaymentFormWriter.grabarVoid(null);
                        }
                        else
                            continuar = true;
                    }
                    else
                    {
                        continuar = true;
                        tran.transaccionOriginal = aux;
                    }
                        
                    if (continuar)
                    {
                        tran.trxReferenceIdOriginal = aux.trxReferenceId;
                        tran.numTicket = aux.numTicket;
                        tran.importeTotal = aux.importeTotal;
                        tran.cantCuotas = aux.cantCuotas;
                        tran.fecha = aux.fecha;
                        tran.trxId = aux.trxId;
                        tran.currency = aux.currency;
                        continuar = true;
                    }
                }
                else
                {
                    TarjetaCajero tCaj;
                    if (EntityLoader.loadTarjetas().Count(x => x.codTarjetaSabre == tran.tarjeta.codSabre) > 1)
                    {
                        WebServiceComercio wsCom = new WebServiceComercio(tran.tarjeta.codSabre, "", tran.tarjeta.primeros6());
                        Comercio auxCom = WebResponseParser.parseXMLComercio(wsCom.getResponse());
                        tCaj = EntityLoader.loadTarjetas().First(x => x.codTarjetaSabre == tran.tarjeta.codSabre && x.codComercio == auxCom.codigoComercio && x.tipo == tran.tarjeta.tipoTarjeta);
                    }
                    else
                        tCaj = EntityLoader.loadTarjetas().First(x => x.codTarjetaSabre == tran.tarjeta.codSabre);
                    tran.tarjeta = tCaj.ToTarjeta();
                    tran.comercio.codigoComercio = tCaj.codComercio;
                    continuar = true;
                }
                if (continuar)
                {
                    Autorizator auth = AutorizatorFactory.getAutorizator( tran );
                    Transaccion resultado = auth.realizarTransaccion(tran);
                    if ( resultado != null  && resultado.isAprobada())
                    {
                        isAnulationComplete = true;
                    }
                    PaymentFormWriter.grabarVoid(resultado);
                    this.Activate();
                }
            }
            catch (Exception exIO)
            {
                MessageBox.Show("No se encontro la tarjeta de referencia en el archivo de tarjetas", "Error en codigo de tarjeta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Error("No se encontro la tarjeta de referencia en el archivo de tarjetas", exIO);
                this.Activate();
                Application.Exit();
            }
            Close();
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

        private void mostrarCartel()
        {
            pnlMensaje.Visible = true;
            pnlMensaje.Refresh();
        }

        private void FrmAnulacion_Load(object sender, EventArgs e)
        {
            if (this.tran == null)
                tran = VBrequestReader.leerVoid(); 

            txtNumero.Text = tran.tarjeta.numero;
            txtTicket.Text = tran.ticketOriginal;

            tran.eventHandler += this.handleEventTransaction;

        }

        private void lblNumero_Click(object sender, EventArgs e)
        {

        }
    }
}
