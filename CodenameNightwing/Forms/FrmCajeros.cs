using CodenameNightwing.Autorization;
using CodenameNightwing.Autorization.POS;
using CodenameNightwing.BusinessLogic;
using CodenameNightwing.FileManager;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CodenameNightwing.Forms
{
    public partial class FrmCajeros : Form
    {
        public static readonly ILog logger = LogManager.GetLogger(typeof(FrmCajeros));

        private decimal _importeAAutorizar;
        public decimal importeAAutorizar
        {
            get { return _importeAAutorizar; }
            set { _importeAAutorizar = value; }
        }

        private TarjetaCajero _tarjSeleccionada;
        public TarjetaCajero tarjSeleccionada
        {
            get { return _tarjSeleccionada; }
            set { _tarjSeleccionada = value; }
        }

        private TarjetaCajero _tarjAnterior;
        public TarjetaCajero tarjAnterior
        {
            get { return _tarjAnterior; }
            set { _tarjAnterior = value; }
        }

        public FrmCajeros()
        {
            InitializeComponent();
            Icon = Properties.Resources.logo_con_borde_grueso;

        }

        private void FrmCajeros_Load(object sender, EventArgs e)
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
            List<TarjetaCajero> lTAux = EntityLoader.loadTarjetas();
            foreach (var item in lTAux)
            {
                RadioButton rAux = new RadioButton();
                rAux.Tag = item;
                rAux.Text = item.descripcionTarjeta;
                rAux.Dock = DockStyle.Top;
                rAux.CheckedChanged += rAux_CheckedChanged;
                if (item.tipo == TipoTarjeta.CREDITO)
                    grpCredito.Controls.Add(rAux);
                else
                    grpDebito.Controls.Add(rAux);
            }

            Transaccion transPriceInf = TransaccionBuilder.construirPago(1);
            lblPriceInformation.Text = transPriceInf.getPriceInformation();
        }

        private void rAux_CheckedChanged(object sender, EventArgs e)
        {
            var aux = (RadioButton)sender;
            if (aux.Checked)
            {
                tarjSeleccionada = (TarjetaCajero)aux.Tag;
                txtTipo.Text = (tarjSeleccionada.tipo == TipoTarjeta.CREDITO ? "Crédito" : "Débito");
                txtTarjeta.Text = tarjSeleccionada.descripcionTarjeta;
                if (tarjAnterior != null)
                {
                    if (tarjAnterior.tipo != tarjSeleccionada.tipo)
                        if (tarjAnterior.tipo == TipoTarjeta.DEBITO)
                            grpDebito.Controls.OfType<RadioButton>().ToList().ForEach(x => x.Checked = false);
                        else
                            grpCredito.Controls.OfType<RadioButton>().ToList().ForEach(x => x.Checked = false);
                }
                else
                    tarjAnterior = tarjSeleccionada;
            }
            else
                tarjAnterior = (TarjetaCajero)aux.Tag;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            PaymentFormWriter.grabarCambio(null);
            Close();
        }

        private void btnAutorizar_Click(object sender, EventArgs e)
        {
            autorizar();
        }

        private void autorizar()
        {

            try
            {

            if (tarjSeleccionada != null)
            {
                DialogResult opcion = DialogResult.No;
                if (tarjSeleccionada.tipo == TipoTarjeta.CREDITO)
                {
                    opcion = MessageBox.Show("La tarjeta es extranjera?", "Selección de tarjeta extranjera o nacional", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    this.Activate();
                }
                bool esExtranjero = opcion == DialogResult.No ? false : true;
                mostrarCartel();
                Transaccion auxTran = TransaccionBuilder.construirPago(tarjSeleccionada, 1, importeAAutorizar, TipoModoTransaccion.ONLINE);
                Autorizator autorizador = AutorizatorFactory.getAutorizator(auxTran);
                

                auxTran.eventHandler += handleEventTransaction;

                Transaccion resultado = autorizador.realizarTransaccion(auxTran);
                if (resultado == null)
                {
                    pnlMensaje.Visible = false;
                    return;
                }
                if (string.IsNullOrEmpty(resultado.numAutorizacion.Trim()))
                {
                    while (resultado.estado == HostCodesPOS.HOST_01_PEDIR_AUTORIZACION || resultado.estado == HostCodesPOS.HOST_02_PEDIR_AUTORIZACION || resultado.estado == HostCodesPOS.HOST_76_LLAMAR_AL_EMISOR || resultado.estado == HostCodesPOS.HOST_91_EMISOR_FUERA_LINEA || resultado.estado == HostCodesPOS.HOST_96_ERROR_EN_SISTEMA)
                    {
                        FrmAutorizacionTelefonica fAut = new FrmAutorizacionTelefonica(resultado);
                        fAut.ShowDialog();
                        if (fAut.tran == null)
                        {
                            pnlMensaje.Visible = false;
                            return;
                        }
                        resultado = fAut.tran;
                    }
                }
                resultado.tarjeta.tipoTarjeta = tarjSeleccionada.tipo;
                resultado.tarjeta.codTarjeta = tarjSeleccionada.codNumTarjeta;
                FrmOtrosDatos formOtrosDatos = new FrmOtrosDatos(resultado, esExtranjero);
                formOtrosDatos.ShowDialog();
                if (formOtrosDatos.continuar)
                {
                    PaymentFormWriter.grabarCambio(formOtrosDatos.tran);
                    this.Close();
                }
                pnlMensaje.Visible = false;
            }
            else
            {
                MessageBox.Show("Debe seleccionar una tarjeta", "Falta seleccionar tarjeta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Activate();
            }

            }
            catch (Exception e)
            {
                logger.Error("No se pudo ejecutar correctamente el formulario cajeros", e);
            }
        }

        private void mostrarCartel()
        {
            pnlMensaje.Visible = true;
            pnlMensaje.Refresh();
        }

        private void grpDebito_Enter(object sender, EventArgs e)
        {

        }


        public void handleEventTransaction(object sender, EventArgs args)
        {
            Transaccion tran = (Transaccion)sender;
            string estadoDesc = tran.getEstadoDescription();
            if (estadoDesc != null)
            {
                txtMensaje.Text = estadoDesc;
                txtMensaje.Refresh();
                mostrarCartel();
            }
        }


    }
}
