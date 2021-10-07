using CodenameNightwing.Autorization;
using CodenameNightwing.BusinessLogic;
using CodenameNightwing.Config;
using CodenameNightwing.FileManager;
using CodenameNightwing.Valtech.Exceptions;
using CodenameNightwing.Varios;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace CodenameNightwing.Forms
{
    public partial class FrmCanje : Form
    {

        public static readonly ILog logger = LogManager.GetLogger(typeof(FrmCanje));


        private Transaccion _aCambiar;
        public Transaccion aCambiar
        {
            get { return _aCambiar; }
            set { _aCambiar = value; }
        }

        private TarjetaCajero tarjSeleccionada;

        public FrmCanje()
        {
            InitializeComponent();

            Icon = Properties.Resources.logo_con_borde_grueso;
            cargarTarjetas();
            grpAccionesCredito.Visible = false;
            grpTarjetasDebito.Visible = false;
            aCambiar = VBrequestReader.leerExchange();
            lblPriceInformation.Text = aCambiar.getPriceInformation();
        }

        private void cargarTarjetas()
        {
            List<TarjetaCajero> auxTarjetas = EntityLoader.loadTarjetas().Where(x => x.tipo == TipoTarjeta.DEBITO).ToList();
            foreach (TarjetaCajero item in auxTarjetas)
            {
                RadioButton rAux = new RadioButton();
                rAux.Tag = item;
                rAux.Text = item.descripcionTarjeta;
                rAux.Dock = DockStyle.Left;
                rAux.AutoSize = true;
                rAux.CheckedChanged += rAux_CheckedChanged;
                grpTarjetasDebito.Controls.Add(rAux);
            }
        }

        private void rAux_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton aux = sender as RadioButton;
            tarjSeleccionada = (TarjetaCajero)aux.Tag;
        }

        private void rdbDebito_CheckedChanged(object sender, EventArgs e)
        {
            this.Height = grpAccionesDebito.Bottom + 45;
            this.Width = grpTipoTarjeta.Right + 25;
            grpTarjetasDebito.Visible = true;
            grpAccionesCredito.Visible = false;
            logger.Info("Se selecciono Tarjeta de Debito Argentina");
        }

        private void rdbCredArg_CheckedChanged(object sender, EventArgs e)
        {
            this.Height = grpImporte.Bottom + 45;
            this.Width = grpAccionesCredito.Right + 25;
            grpAccionesCredito.Visible = true;
            grpTarjetasDebito.Visible = false;
            logger.Info("Se selecciono Tarjeta de Credito Argentina");
        }

        private void rdbCredExt_CheckedChanged(object sender, EventArgs e)
        {
            this.Height = grpImporte.Bottom + 45;
            this.Width = grpAccionesCredito.Right + 25;
            grpAccionesCredito.Visible = true;
            grpTarjetasDebito.Visible = false;
            logger.Info("Se selecciono Tarjeta de Credito Extranjeras");
        }

        private void btnVolverCred_Click(object sender, EventArgs e)
        {
            PaymentFormWriter.grabarCambio(null);
            this.Close();
        }

        private void btnVolverDebito_Click(object sender, EventArgs e)
        {
            PaymentFormWriter.grabarCambio(null);
            this.Close();
        }

        private void btnAutorizar_Click(object sender, EventArgs e)
        {
            autorizar();
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

        private void autorizar()
        {
            if (tarjSeleccionada != null)
            {

                Transaccion resultado = null;

                mostrarCartel();
                Transaccion auxTran = TransaccionBuilder.construirPago(tarjSeleccionada, 1, aCambiar.importeTotal, TipoModoTransaccion.ONLINE);
                Autorizator autorizador = AutorizatorFactory.getAutorizator( auxTran );
                
                auxTran.cantCuotas = 1;
                auxTran.modo = TipoModoTransaccion.ONLINE;

                auxTran.tarjeta = autorizador.solicitarNumeroTarjeta("Sale", auxTran);

                if ( auxTran.tarjeta != null) auxTran.tarjeta.tipoTarjeta = TipoTarjeta.DEBITO;

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

                FrmOtrosDatos formOtrosDatos = new FrmOtrosDatos(auxTran, esExtranjero);
                if (auxTran.tarjeta != null)
                {
                    formOtrosDatos.importeAutorizar = aCambiar.importeTotal;
                    formOtrosDatos.ShowDialog();
                    if (formOtrosDatos.tran != null)
                    {
                        formOtrosDatos.tran.tarjeta.debitoSeleccionada = tarjSeleccionada.descripcionTarjeta;
                        formOtrosDatos.tran.tarjeta.codSabre = tarjSeleccionada.codTarjetaSabre;
                        formOtrosDatos.tran.tarjeta.descripcion = tarjSeleccionada.descripcionTarjeta;
                        resultado = autorizador.realizarTransaccion(formOtrosDatos.tran);
                    }
                }

                if (resultado == null)
                {
                    pnlMensaje.Visible = false;
                    return;
                }

                resultado.tarjeta.tipoTarjeta = TipoTarjeta.DEBITO;
                resultado.tarjeta.codTarjeta = tarjSeleccionada.codNumTarjeta;
                resultado.tarjeta.codSabre = tarjSeleccionada.codTarjetaSabre;
                //resultado.tarjeta.descripcion = tarjSeleccionada.descripcionTarjeta.ToUpperInvariant();
                resultado.importeTotal = aCambiar.importeTotal;
                
                if (formOtrosDatos.continuar)
                {
                    PaymentFormWriter.grabarCambio(resultado);
                    this.Close();
                }
                pnlMensaje.Visible = false;
            }
            else
            {
                MessageBox.Show("Debe seleccionar el tipo de tarjeta de debito", "Falta seleccionar tipo de tarjeta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Activate();
            }
        }

        private void btnPlanes_Click(object sender, EventArgs e)
        {
            planes();
        }

        private void planes()
        {
            try
            {
                bool extranjero = (rdbCredArg.Checked ? false : true);
                bool solo1cuota = false;

                // Si es Contact Center y selecciona extranjero, tenemos que preguntar
                // si la tarjera es del pais del esta parado. Si es así, buscar las promo
                // sino una sola cuota
                if (extranjero && (Configuration.getInstance().tipoAuth.Equals(TipoAutorizador.VTOL_CALLCENTER) || Configuration.getInstance().tipoAuth.Equals(TipoAutorizador.NPS)))
                {
                    DialogResult opcion;

                    using (new CenterWinDialog(this))
                    {
                        string pseudoCityCode = (aCambiar == null) ? VBrequestReader.getPropiedad("pcc") : aCambiar.pcc;
                        string country = POIutils.getCountryFromPCC(pseudoCityCode);
                        opcion = MessageBox.Show("La tarjeta es emitida en " + country + "? Sino no traera cuotas", "Selección de tarjeta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    }

                    if (opcion == DialogResult.Yes)
                        solo1cuota = false;
                    else
                        solo1cuota = true;
                }



                Autorizator autorizador = AutorizatorFactory.getAutorizator(aCambiar);
                if (Configuration.getInstance().tipoAuth != TipoAutorizador.HASAR)
                {
                    mostrarCartel();

                    Tarjeta auxtar = autorizador.solicitarNumeroTarjeta("Sale", aCambiar);
                    if (auxtar == null)
                    {
                        pnlMensaje.Visible = false;
                        return;
                    }

                    aCambiar.estadoDescripcion = TipoEstadoTransaccion.OBTENIENDO_PROMOCIONES;

                    auxtar.tipoTarjeta = TipoTarjeta.CREDITO;
                    aCambiar.tarjeta = auxtar;

                    //FrmOtrosDatos formOtrosDatos = new FrmOtrosDatos(tran, formImportes.aAutorizar, extranjero, solo1cuota, operacion);
                    Operacion operacion = new Operacion(TipoOperacion.CANJE, aCambiar.importeTotal);
                    FrmOtrosDatos formOtrosDatos = new FrmOtrosDatos(aCambiar, aCambiar.importeTotal, extranjero, solo1cuota, operacion);
                    formOtrosDatos.ShowDialog();
                    if (formOtrosDatos.continuar)
                    {
                        lblMensaje.Text = "Pasando info a Interact..";
                        lblMensaje.Refresh();
                        mostrarCartel();
                        PaymentFormWriter.grabarCambio(formOtrosDatos.tran);
                        this.Close();
                    }
                    pnlMensaje.Visible = false;
                }
                else
                {
                    HasarOtherDataComunicator.sendExtranjero(rdbCredArg.Checked ? Procedencia.NACIONAL : Procedencia.EXTRANJERO, false);
                    Transaccion aComprar = TransaccionBuilder.construirPago(aCambiar.importeTotal);
                    Transaccion tran = autorizador.realizarTransaccion(aComprar);
                    if (tran != null)
                    {
                        PaymentFormWriter.grabarCambio(tran);
                        this.Close();
                    }
                }

            }
 
            catch (ValtchException e)
            {
                pnlMensaje.Visible = false;
                MessageBox.Show(e.getMsg(), "Error cargando promociones", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.logger.Error("Error cargando promociones: ", e);
            }
            catch (Exception e)
            {
                pnlMensaje.Visible = false;
            }

        }

        private void mostrarCartel()
        {
            pnlMensaje.Visible = true;
            pnlMensaje.Refresh();
        }

        private void FrmCanje_Load(object sender, EventArgs e)
        {
            this.Height = grpImporte.Bottom + 45;
            this.Width = grpTipoTarjeta.Right + 25;
            lblImporte.Text = aCambiar.importeSinIntereses().ToString("#####0.00").Replace(",", ".");
            cargarShortcutsTarjetasDebito();

            aCambiar.eventHandler += this.handleEventTransaction;

        }

        private void cargarShortcutsTarjetasDebito()
        {
            List<char> letrasDisponibles = buscarLetrasDisponibles();
            foreach (Control item in grpTarjetasDebito.Controls)
            {
                bool seAsignoLetra = false;
                int i = 0;
                if (item.GetType() == typeof(RadioButton))
                    while (!seAsignoLetra)
                    {
                        if (letrasDisponibles.Contains(char.ToLower(((RadioButton)item).Text[i])))
                        {
                            letrasDisponibles.Remove(char.ToLower(((RadioButton)item).Text[i]));
                            letrasDisponibles.Remove(char.ToUpper(((RadioButton)item).Text[i]));
                            ((RadioButton)item).Text = ((RadioButton)item).Text.Insert(i, "&");
                            seAsignoLetra = true;

                        }
                        i++;
                    }
            }
        }

        private List<char> buscarLetrasDisponibles()
        {
            List<char> letrasYaUsadas = new List<char>();
            List<char> letrasYNumeros = "abcdefghijklmnopqrstuvwxyz0123456789".ToCharArray().ToList<char>();
            buscarRecursivo(this.Controls);
            foreach (var item in auxL)
            {
                System.Reflection.PropertyInfo pi = item.GetType().GetProperty("Text");
                if (pi != null)
                    if (((string)pi.GetValue(item,null)).Contains("&"))
                        letrasYaUsadas.Add(((string)pi.GetValue(item, null)).Substring(((string)pi.GetValue(item, null)).IndexOf('&'))[1]);

            }
            foreach (char item in letrasYaUsadas)
            {
                letrasYNumeros.Remove(char.ToUpper(item));
                letrasYNumeros.Remove(char.ToLower(item));
            }
            return letrasYNumeros;
        }

        List<Control> auxL = new List<Control>();
        private void buscarRecursivo(System.Windows.Forms.Control.ControlCollection controles)
        {
            foreach (Control item in controles)
                if (item.HasChildren)
                    buscarRecursivo(item.Controls);
                else
                    auxL.Add(item);
        }

        private void btnOtras_Click(object sender, EventArgs e)
        {
            FrmOtrasOperaciones formOtros = new FrmOtrasOperaciones();
            formOtros.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmOtrasOperaciones formOtros = new FrmOtrasOperaciones();
            formOtros.ShowDialog();
        }

        private void grpTarjetasDebito_Enter(object sender, EventArgs e)
        {

        }

        private void lblMensaje_Click(object sender, EventArgs e)
        {

        }
    }
}
