using CodenameNightwing.Autorization;
using CodenameNightwing.BusinessLogic;
using CodenameNightwing.Config;
using CodenameNightwing.FileManager;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using CodenameNightwing.Varios;

namespace CodenameNightwing.Forms
{
    public partial class FrmOnePayRed : Form
    {


        private decimal _importeAAutorizar;
        public decimal importeAAutorizar
        {
            get { return _importeAAutorizar; }
            set { _importeAAutorizar = value; }
        }

        private bool _esCambio;
        public bool esCambio
        {
            get { return _esCambio; }
            set { _esCambio = value; }
        }

        private Transaccion _aCambiar;
        public Transaccion aCambiar
        {
            get { return _aCambiar; }
            set { _aCambiar = value; }
        }

        public FrmOnePayRed(bool cambio)
        {
            InitializeComponent();
            Icon = Properties.Resources.logo_con_borde_grueso;
            esCambio = cambio;
            if (cambio)
            {
                aCambiar = VBrequestReader.leerExchange();
                if (aCambiar != null)
                    importeAAutorizar = aCambiar.importeTotal;
            }

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

        private void FrmOnePayRed_Load(object sender, EventArgs e)
        {
            if (esCambio)
            {
                this.Text = "Exchange";
                txtImporte.Text = importeAAutorizar.ToString("#####0.00").Replace(",", ".");
            }
            else
            {
                List<Pnr> lPnr = VBrequestReader.leerPnrs();

                foreach (var item in lPnr)
                {
                    if (item.onlyEmd)
                        importeAAutorizar += item.getTotalEmds();
                    else
                        importeAAutorizar += item.getTotalAmount();
                }
                txtImporte.Text = importeAAutorizar.ToString("#####0.00").Replace(",", ".");
            }

            if (aCambiar != null)
            {
                aCambiar.eventHandler += this.handleEventTransaction;
            }else
            {
                Transaccion trans = TransaccionBuilder.construirPago(importeAAutorizar);
                VBrequestReader.setCommonFields( trans );
                aCambiar = trans;
            }
            lblPriceInformation.Text = aCambiar.getPriceInformation();

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            PaymentFormWriter.grabarCambio(null);
            Close();
        }

        private void btnAutorizar_Click(object sender, EventArgs e)
        {
            mostrarCartel();
            Transaccion tran;

            if(Configuration.getInstance().tipoAuth == TipoAutorizador.HASAR)
                HasarOtherDataComunicator.sendExtranjero(Procedencia.PREGUNTAR,true);

            Transaccion aComprar = TransaccionBuilder.construirPago(importeAAutorizar);
            Autorizator autorizador = AutorizatorFactory.getAutorizator(aComprar);
            

            if (Configuration.getInstance().tipoAuth == TipoAutorizador.VTOL || Configuration.getInstance().tipoAuth == TipoAutorizador.NPS)
            {
                aComprar.tarjeta = autorizador.solicitarNumeroTarjeta("Sale" , aComprar);
                if (aComprar.tarjeta != null)
                {
                    bool esExtranjero = false;

                    DialogResult opcion;

                    using (new CenterWinDialog(this))
                    {
                        opcion = MessageBox.Show("La tarjeta es extranjera?", "Selección de tarjeta extranjera o nacional", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    }

                    if (opcion == DialogResult.Yes)
                        esExtranjero = true;
                    else
                        esExtranjero = false;

                    aComprar.tarjeta.tipoTarjeta = TipoTarjeta.CREDITO_DEBITO;
                    FrmOtrosDatos formOtrosDatos = new FrmOtrosDatos(aComprar, esExtranjero, true);
                    formOtrosDatos.ShowDialog();
                    tran = formOtrosDatos.tran;
                    if (!formOtrosDatos.cargoOK)
                    {
                        autorizador.cancelarTransaccion();
                        tran = null;
                    }
                    else
                    {

                        if (tran.tarjeta.codSabre == null || tran.tarjeta.codSabre.Trim().Length != 2)
                        {
                            POIutils.updateTarjetaFromBin(tran.tarjeta.primeros6(), tran.tarjeta);
                        }

                    }
                }
                else
                {
                    tran = null;
                }
            }
            else
            {
                
                tran = autorizador.realizarTransaccion(aComprar);
            }
                
            pnlMensaje.Visible = false;
            PaymentFormWriter.grabarCambio(tran);

            // Si está aprobada cierro el formulario
            if ( tran != null && tran.isAprobada())
                Close();

        }

        private void mostrarCartel()
        {
            pnlMensaje.Visible = true;
            pnlMensaje.Refresh();
        }

        private void lblMensaje_Click(object sender, EventArgs e)
        {

        }

        private void btnOtras_Click(object sender, EventArgs e)
        {
            FrmOtrasOperaciones formOtros = new FrmOtrasOperaciones();
            formOtros.ShowDialog();
        }
    }
}
