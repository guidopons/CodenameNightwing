using CodenameNightwing.Autorization;
using CodenameNightwing.BusinessLogic;
using CodenameNightwing.Config;
using CodenameNightwing.FileManager;
using CodenameNightwing.Valtech.Exceptions;
using CodenameNightwing.Valtech.PromoService;
using CodenameNightwing.Valtech.PromoService.Response;
using CodenameNightwing.Varios;
using CodenameNightwing.WebServices;
using CodenameNightwing.WebServices.WSEspecificos;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace CodenameNightwing.Forms
{
    public partial class FrmOtrosDatos : Form
    {
        public bool continuar { get; set; }

        public Transaccion tran { get; set; }

        public List<Promocion> promos { get; set; }

        public decimal importeAutorizar { get; set; }

        public bool esExtranjero { get; set; }

        public bool isShortBox { get; set; }

        public bool cargoOK { get; set; }


        public FrmOtrosDatos(Transaccion auxTran, bool extranjero)
        {
            InitializeComponent();

            auxTran.eventHandler += this.handleEventTransaction;

            Icon = Properties.Resources.logo_con_borde_grueso;
            esExtranjero = extranjero;
            tran = auxTran;
            if (esExtranjero)
                tran.tarjeta.owner.tipoCuitCuil = "CUIT";
            try
            {
                promos = new List<Promocion>();
                pnlCuotas.Visible = false;
                this.Width = pnlDatosGenerales.Right + 20;
                this.Height = pnlAceptarDebito.Bottom + 50;
                cargoOK = true;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error cargando tarjeta de archivo de tarjetas", "Error cargando promociones", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.logger.Error("Error cargando tarjeta de archivo de tarjetas: ", e);
                cargoOK = false;
            }
        }

        public FrmOtrosDatos(Transaccion auxTran, bool extranjero, bool isOnePay) : this(auxTran, extranjero)
        {
            if (isOnePay)
            {
                txtDescripcionTarjeta.Visible = false;
                lblTarjeta.Visible = false;
                txtBanco.Visible = false;
                lblBanco.Visible = false;

            }
        }

        public FrmOtrosDatos(Transaccion transaction, decimal importe, bool extranjero, bool solo1cuota, Operacion operacion)
        {
            try
            {
                InitializeComponent();

                Icon = Properties.Resources.logo_con_borde_grueso;

                transaction.eventHandler += this.handleEventTransaction;


                esExtranjero = extranjero;
                importeAutorizar = importe;

                //Configuracion en BD para habilitar promos Valtech
                if (Configuration.getInstance().isPromoAvail)
                    cargarPromosValtech(transaction, solo1cuota);
                else
                    cargarPromosTarcred(transaction.tarjeta, solo1cuota, operacion);

                if (promos != null && promos.Count > 0)
                {

                    TarjetaCajero auxTC;
                    if (promos[0].esBinExcepcion)
                    {
                        WebServiceExcepcionBines wsEx = new WebServiceExcepcionBines(transaction.tarjeta.primeros6());
                        auxTC = WebResponseParser.parseXMLExceptionBin(wsEx.getResponse());
                    }
                    else
                    {
                        if (EntityLoader.loadTarjetas().Count(x => x.codTarjetaSabre == promos[0].codTarjetaSabre) > 1)
                        {
                            WebServiceComercio wsCom = new WebServiceComercio(promos[0].codTarjetaSabre, "", transaction.tarjeta.primeros6());
                            //   wsCom.sendRequest();
                            Comercio auxWsCom = WebResponseParser.parseXMLComercio(wsCom.getResponse());
                            auxTC = EntityLoader.loadTarjetas().First(x => x.codTarjetaSabre == promos[0].codTarjetaSabre && x.codComercio == auxWsCom.codigoComercio && x.tipo == TipoTarjeta.CREDITO);
                        }
                        else
                            auxTC = EntityLoader.loadTarjetas().First(x => x.codTarjetaSabre == promos[0].codTarjetaSabre);
                    }
                    //TarjetaCajero auxTC = EntityLoader.loadTarjetas().First(x => x.codTarjetaSabre == promos[0].codTarjetaSabre);
                    tran = TransaccionBuilder.construirPago(auxTC.codComercio, 1, importe, transaction.tarjeta.codTarjeta, TipoModoTransaccion.ONLINE);
                    transaction.tarjeta.codTarjeta = auxTC.codNumTarjeta;
                    transaction.tarjeta.codPlan = auxTC.codPlan;
                    tran.tarjeta = transaction.tarjeta;
                    tran.tipoOperacion = operacion.tipo;
                    esconderCosas();
                    cargoOK = true;

                }
                else
                {
                    cargoOK = false;
                    MessageBox.Show("No se pudieron cargar las promociones, verifique su conexion y vuelva a intentarlo", "Error cargando promociones", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Activate();
                }
            }
            
            catch (ValtchException e)
            {
                cargoOK = false;
                throw e;
            }
            catch (Exception e)
            {
                cargoOK = false;
                MessageBox.Show("Error cargando tarjeta de archivo de tarjetas", "Error cargando promociones", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.logger.Error("Error cargando tarjeta de archivo de tarjetas: ", e);
                throw e;
            }
        }



        private void FrmOtrosDatos_Load(object sender, EventArgs e)
        {
            try
            {
                if (!cargoOK)
                    this.Close();
                txtPrimerosNumeros.Text = tran.tarjeta.primeros6();
                txtUltimosNumeros.Text = tran.tarjeta.ultimos4();
                txtNumerosRestantes.Text = tran.tarjeta.numero.Substring(6, tran.tarjeta.numero.Length - 10);
                txtVencimiento.Text = tran.tarjeta.vencimiento;

                if (promos.Count > 0)
                {
                    txtDescripcionTarjeta.Text = promos[0].descripcionTarjeta;
                    txtBanco.Text = esExtranjero ? "EXTRANJERO" : (string.IsNullOrEmpty(promos[0].banco) ? "SIN PROMOCION" : promos[0].banco);
                }
                else
                {
                    txtDescripcionTarjeta.Text = tran.tarjeta.descripcion;
                    txtBanco.Text = "SIN PROMOCION";
                }

                continuar = false;
                cargarMascaras();
                if (esExtranjero)
                {
                    cargarPaises();
                    txtDocumento.Mask = "";
                    btnCompletarCuil.Enabled = false;
                    txtCuit.Enabled = false;
                    txtCuit.Text = "30641405554";
                    tran.tarjeta.owner.tipoCuitCuil = "CUIT";
                }
                else
                {
                    cmbPais.Items.Add(new Pais("AR", "ARGENTINA"));
                    cmbPais.SelectedValue = "AR";
                    cmbPais.Text = "ARGENTINA";
                    cmbPais.Enabled = false;

                }

                tran.tarjeta.owner.esExtranjero = esExtranjero;

                cargarComboTipoPersonas();

                txtCuotas.Text = 1.ToString();
                txtIntereses.Text = 0.0m.ToString("#####0.00").Replace(",", ".");
                txtImporteAAutorizar.Text = importeAutorizar.ToString("#####0.00").Replace(",", ".");
                if (grdCuotas.Rows.Count > 0)
                    grdCuotas.Rows[0].Cells[0].Selected = false;
                if (POIutils.isVtolNpsAutorizator())
                {
                    txtNumerosRestantes.Enabled = false;
                    txtNumerosRestantes.BackColor = Color.PaleTurquoise;
                }

                lblPriceInformation.Text = tran.getPriceInformation();

                if (tran != null && tran.tarjeta != null && tran.tarjeta.owner != null && tran.tarjeta.owner.nombre != null)
                {
                    txtNombre.Text = tran.tarjeta.owner.nombre;
                }

                if (!tran.isTransToCheckFraud())
                {

                    cmbPais.Enabled = false;
                    txtProvincia.Enabled = false;
                    cmbState.Enabled = false;
                    txtCiudad.Enabled = false;
                    txtCodPostal.Enabled = false;
                    txtDireccionCalle.Enabled = false;
                    txtDireccionNro.Enabled = false;
                    txtNacimiento.Enabled = false;


                }

                if (Configuration.getInstance().tipoAuth != TipoAutorizador.NPS)
                    txtNacimiento.Enabled = false;

            }
            catch (Exception ex)
            {
                Program.logger.Error("Error cargando formulario: " + ex);
                continuar = false;
                tran = null;
                this.Activate();
                MessageBox.Show("Error al cargar formulario", "Error de carga", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        public void setBotonesDebito()
        {
            btnVolverOne.Enabled = true;
            btnAceptar.Text = "&Autorizar";
        }

 
        public void handleEventTransaction(object sender, EventArgs args)
        {
            Transaccion tran = (Transaccion)sender;
            string estadoDesc = tran.getEstadoDescription();
            if (estadoDesc != null) {
                if (isShortBox)
                { 
                    lblMensajeShort.Text = estadoDesc;
                    lblMensajeShort.Refresh();
                    mostrarCartelShort();
                }
                else
                {
                    lblMensaje.Text = estadoDesc;
                    lblMensaje.Refresh();
                    mostrarCartel();
                }
                   
            }
        }

        private void cargarMascaras()
        {
            string auxCodTarjeta;
            if (promos.Count > 0)
                auxCodTarjeta = promos[0].codTarjeta;
            else
                auxCodTarjeta = tran.tarjeta.codSabre;
            switch (auxCodTarjeta)
            {
                case "AX":
                    txtPrimerosNumeros.Mask = "0000-00";
                    txtUltimosNumeros.Mask = "0000";
                    txtNumerosRestantes.Mask = "0000-0";
                    break;
                case "DC":
                    txtPrimerosNumeros.Mask = "0000-00";
                    txtUltimosNumeros.Mask = "0000";
                    txtNumerosRestantes.Mask = "0000-";
                    break;
                case "MA":
                    txtPrimerosNumeros.Mask = "000000";
                    //txtUltimosNumeros.Mask = "0000";
                    txtUltimosNumeros.Visible = false;
                    txtNumerosRestantes.Width = txtBanco.Width - txtPrimerosNumeros.Width - 5;
                    txtNumerosRestantes.Mask = "0000000000####";
                    break;
                default:
                    txtPrimerosNumeros.Mask = "0000-00";
                    txtUltimosNumeros.Mask = "-0000";
                    txtNumerosRestantes.Mask = "00-0000";
                    break;
            }
        }


        private void cargarComboTipoPersonas()
        {
            Dictionary<string, string> test = new Dictionary<string, string>();
            test.Add("M", "Masculino");
            test.Add("F", "Femenino");
            //test.Add("E", "Empresa");
            cmbTipoPersona.DataSource = new BindingSource(test, null);
            cmbTipoPersona.AutoCompleteCustomSource.Add("Masculino");
            cmbTipoPersona.AutoCompleteCustomSource.Add("Femenino");
           // cmbTipoPersona.AutoCompleteCustomSource.Add("Empresa");
            cmbTipoPersona.DisplayMember = "Value";
            cmbTipoPersona.ValueMember = "Key";
        }


        private void cargarStates( string countryCode )
        {

            SortedDictionary<string, string> states = POIutils.getStatesList( countryCode );
            cmbState.DataSource = new BindingSource(states, null);
            cmbState.DisplayMember = "Key";
            cmbState.ValueMember = "Value";
        }

        private void cargarPaises()
        {
            foreach (var item in EntityLoader.loadPaises())
            {
                cmbPais.AutoCompleteCustomSource.Add(item.descripcionPais);
                cmbPais.Items.Add(item);
            }
        }

        private void cargarPromosTarcred(Tarjeta tar, bool solo1cuota, Operacion operacion)
        {
            if ( operacion != null)
            {
                WebServiceBinesOperacion wsPromo = new WebServiceBinesOperacion(tar.primeros6(), importeAutorizar, operacion);
                promos = WebResponseParser.parseXMLBines(wsPromo.getResponse());
            }
            else
            {
                WebServiceBines wsPromo = new WebServiceBines(tar.primeros6(), importeAutorizar);
                promos = WebResponseParser.parseXMLBines(wsPromo.getResponse());
            }
            
            if (!esExtranjero && !solo1cuota)
            {
                foreach (var item in promos)
                {
                    if (item.cuotas > 0) {
                        decimal auxImpCuota = item.porcentaje == 0.0M ? importeAutorizar / PlanesAHORA.getCuotasDividir(item.cuotas) : (importeAutorizar + (importeAutorizar * item.porcentaje / 100)) / PlanesAHORA.getCuotasDividir(item.cuotas);
                        decimal auxTotal = item.porcentaje == 0.0M ? importeAutorizar : importeAutorizar + (importeAutorizar * item.porcentaje) / 100;

                        grdCuotas.Rows.Add(new object[] { PlanesAHORA.getCuotasMostrar(item.cuotas), item.porcentaje, Convert.ToDecimal(auxImpCuota.ToString("######0.00")), Convert.ToDecimal(auxTotal.ToString("######0.00")), item.idPromo, PlanesAHORA.getCuotasMostrarNombre(item.cuotas) });
                        //tar.descripcion = item.descripcionTarjeta;
                        tar.codSabre = item.codTarjetaSabre;
                        tar.codTarjeta = item.codTarjeta;
                    }
                }
            }
            else
            {
                grdCuotas.Rows.Add(new object[] { "1","0.00", Convert.ToDecimal(importeAutorizar.ToString("######0.00")), Convert.ToDecimal(importeAutorizar.ToString("######0.00")), "1", "Cuota 1" });
            }
        }


        private void cargarPromosValtech(Transaccion transaction, bool solo1cuota)
        {
            try
            {
                PromoServiceController promoController = new PromoServiceController();

                if (transaction.tarjeta.codSabre == null || transaction.tarjeta.codSabre.Equals(""))
                {
                    POIutils.updateTarjetaFromBin(transaction.tarjeta.primeros6(), transaction.tarjeta, true);
                }

                if (transaction.tarjeta.codSabre == null)
                {
                    throw new Exception("Error Servicio Promociones: PaymentMethodCode nulo");
                }

                PromoServiceResponse promoResponse = promoController.checkPromo(transaction, true);

                if (promoResponse == null || (promoResponse.errorResponseMsg != null && promoResponse.errorResponseMsg.Contains("ERROR")))
                {
                    MessageBox.Show("Error cargando promociones del motor de reglas", "Error cargando promociones", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    throw new Exception("Error Servicio Promociones: PromoServiceResponse nulo");
                }

                Dictionary<string, List<cuota>> dictionaryFrom = promoResponse.interestFreeInstallments;
                Dictionary<string, List<cuota>> dictionaryTo = promoResponse.fixedInstallments;

                //Sobreescribe lista de fixedInstallments con lista interestFreeInstallments
                dictionaryFrom.ToList().ForEach(x => dictionaryTo[x.Key] = x.Value);

                List<Promocion> promosValtech = new List<Promocion>();

                int idPromo = 1;

                foreach (KeyValuePair<string, List<cuota>> kvp in dictionaryTo)
                {
                    List<cuota> lsCuotas = kvp.Value;

                    int i = 0;
                    foreach (cuota c in lsCuotas)
                    {

                        string issuer = kvp.Value[i].issuer;
                        int cuotas;
                        //Planes Ahora aplica solo a Argentina
                        if (transaction.country == "ARG")
                        {
                             cuotas = PlanesAHORA.getAhoraFromCode(issuer, kvp.Value[i].installmentCount);
                        }
                        else {
                             cuotas = kvp.Value[i].installmentCount;
                        }


                        decimal installmentAmount = kvp.Value[i].installmentAmount;
                        decimal porcentaje = kvp.Value[i].interestRate;
                        decimal totalAmount = kvp.Value[i].totalAmount;
                        decimal totalInterest = kvp.Value[i].totalInterest;

                        string codTcSabre = kvp.Value[i].paymentMethod;
                        string nombre = kvp.Value[i].name;

                        List<GatewayMetadata> gatewayMetadataLs = kvp.Value[i].getGatewayMetaDataLs();

                        i++;
                        //TODO Si es necesario completar estos datos deberemos buscarlos en Tarcred

                        string banco = "";
                        string codTc = "";
                        string descTc = "";

                        Promocion p = new Promocion(gatewayMetadataLs, idPromo.ToString(), nombre, cuotas, banco, codTc, codTcSabre, DateTime.Now, descTc, porcentaje, false);
                        idPromo++;

                        promosValtech.Add(p);

                    }
                }

                // promos = promos.OrderBy(x => x.cuotas).ThenBy(x => x.porcentaje).ToList();
                // IEnumerable<Promocion> noduplicates = promos.Distinct().OrderBy(x => x.porcentaje).ThenBy(x => x.cuotas);
                //promos = promosValtech.Distinct().OrderBy(x => x.porcentaje).ThenBy(x => x.cuotas).ToList();
                // Primero ordenar y luego el Distint para que se quede con el primero que encuentra
                // o sea con el porcentaje mas bajo

                if (promosValtech.Count > 0)
                {
                    promos = promosValtech.OrderBy(x => x.porcentaje).ThenBy(x => x.cuotas).Distinct().ToList();

                    // PAra Valtech, si es extranjero, dejamos que traiga las cuotas cargadas en servidor
                    //if (!esExtranjero && !solo1cuota)
                    if (solo1cuota || (Configuration.getInstance().tipoAuth.Equals(TipoAutorizador.VTOL) && esExtranjero))
                    {
                        grdCuotas.Rows.Add(new object[] { "1", "0.00", Convert.ToDecimal(importeAutorizar.ToString("######0.00")), Convert.ToDecimal(importeAutorizar.ToString("######0.00")), "1", "Cuota 1" });
                    }
                    else
                    {

                        foreach (var item in promos)
                        {
                            decimal auxImpCuota = item.porcentaje == 0.0M ? importeAutorizar / item.cuotas : (importeAutorizar + (importeAutorizar * item.porcentaje / 100)) / item.cuotas;
                            decimal auxTotal = item.porcentaje == 0.0M ? importeAutorizar : importeAutorizar + (importeAutorizar * item.porcentaje) / 100;

                            if (transaction.country == "ARG")
                            {
                                grdCuotas.Rows.Add(new object[] { PlanesAHORA.getCuotasMostrar(item.cuotas), item.porcentaje, Convert.ToDecimal(auxImpCuota.ToString("######0.00")), Convert.ToDecimal(auxTotal.ToString("######0.00")), item.idPromo, item.nombre });
                            }
                            else
                            {
                                grdCuotas.Rows.Add(new object[] { item.cuotas, item.porcentaje, Convert.ToDecimal(auxImpCuota.ToString("######0.00")), Convert.ToDecimal(auxTotal.ToString("######0.00")), item.idPromo, item.nombre });
                            }

                            
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        private void esconderCosas()
        {
            switch (tran.tarjeta.tipoTarjeta)
            {
                case TipoTarjeta.CREDITO:
                case TipoTarjeta.CREDITO_DEBITO:
                    pnlAceptarDebito.Hide();
                    this.Height = pnlDatosGenerales.Bottom + 50;
                    break;
                case TipoTarjeta.DEBITO:
                    pnlCuotas.Hide();
                    this.Width = pnlDatosGenerales.Right + 20;
                    this.Height = pnlAceptarDebito.Bottom + 50;
                    break;
                default:
                    break;
            }
        }

        private void grdCuotas_Click(object sender, EventArgs e)
        {
            grdCuotas.Rows[grdCuotas.CurrentCell.RowIndex].Selected = true;

            txtCuotas.Text = PlanesAHORA.getCuotasDividir(PlanesAHORA.getAhoraFromString(grdCuotas.Rows[grdCuotas.CurrentCell.RowIndex].Cells[0].Value.ToString())).ToString();
            tran.cantCuotas = PlanesAHORA.getAhoraFromString( grdCuotas.Rows[grdCuotas.CurrentCell.RowIndex].Cells[0].Value.ToString());
            txtImporteAAutorizar.Text = grdCuotas.Rows[grdCuotas.CurrentCell.RowIndex].Cells[3].Value.ToString().Replace(",", ".");
            tran.importeTotal = Convert.ToDecimal(grdCuotas.Rows[grdCuotas.CurrentCell.RowIndex].Cells[3].Value, CultureInfo.InvariantCulture);
            tran.interes = tran.importeTotal - importeAutorizar;
            txtIntereses.Text = (tran.importeTotal - importeAutorizar).ToString("#####0.00").Replace(",", ".");

            string idPromo = grdCuotas.Rows[grdCuotas.CurrentCell.RowIndex].Cells[4].Value.ToString();
            Promocion promoSelected = this.promos.Find(promoObj => promoObj.idPromo == idPromo);
            tran.promocion = promoSelected;

            
        }


        private void volverSinAuth()
        {
            tran = null;
            this.cargoOK = false;
            this.continuar = false;
            if (Configuration.getInstance().tipoAuth == TipoAutorizador.VTOL)
            {
                Autorizator aux = AutorizatorFactory.getAutorizator();
                aux.cancelarLecturaTarjeta();
            }
            this.Close();
        }
        private void btnVolver_Click(object sender, EventArgs e)
        {
            volverSinAuth();
        }

        private void btnAutorizar_Click(object sender, EventArgs e)
        {
            aceptar(false);
        }


        private bool validarForm()
        {
            bool bandera = true;

            // INGRESO DE CUIT
            if (!txtCuit.MaskCompleted)
            {
                bandera = false;
                txtCuit.BackColor = Color.LightCoral;
            }
            else
                txtCuit.BackColor = Color.White;

            if ( txtDireccionCalle.Enabled )
                if (txtDireccionCalle.Text == "") {
                    bandera = false;
                    txtDireccionCalle.BackColor = Color.LightCoral;
                }
                else
                    txtDireccionCalle.BackColor = Color.White;

            if (txtDireccionNro.Enabled)
                if (txtDireccionNro.Text == "")
                {
                    bandera = false;
                    txtDireccionNro.BackColor = Color.LightCoral;
                }
                else
                    txtDireccionNro.BackColor = Color.White;

            if ( txtCodPostal.Enabled )
                if (txtCodPostal.Text == "") {
                    bandera = false;
                    txtCodPostal.BackColor = Color.LightCoral;
                }
                else
                {
                    //US: numeric characters in either of these
                    //formats:
                    // 5 characters: NNNNN
                    // 9 characters: NNNNN - NNNN
                    // CA: six alphanumeric characters in this
                    //format: ANA NAN.

                    txtCodPostal.BackColor = Color.White;

                    if (cmbPais.SelectedItem != null)
                    {
                        string countryCode = ((Pais)cmbPais.SelectedItem).codigoPais;
                        if (countryCode.Equals("US") || countryCode.Equals("CA"))
                        {
                            Regex regexStateUSA = new Regex(@"(?:^\d{5}$|^\d{5} - \d{4}$)");
                            Regex regexStateCA = new Regex(@"^\w{3}\s\w{3}$");

                            Regex regex = (countryCode.Equals("US")) ? regexStateUSA : regexStateCA;

                            Match match = regex.Match(txtCodPostal.Text);
                            if (!match.Success)
                            {
                                txtCodPostal.BackColor = Color.LightCoral;
                                MessageBox.Show("Codigo Postal USA: NNNNN o NNNNN - NNNN ( N=numerico) \n de CANADA: CCC CCC (C=caracter) \n Respetar espacios", "Error Formato Codigo Postal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                bandera = false;
                            }
                        }
                    }

                    
                }

            if (txtProvincia.Enabled)
                if (txtProvincia.Text == "")
                {
                    bandera = false;
                    txtProvincia.BackColor = Color.LightCoral;
                }
                else
                    txtProvincia.BackColor = Color.White;


            if ( txtCiudad.Enabled )
                if (txtCiudad.Text == "")   {
                    bandera = false;
                    txtCiudad.BackColor = Color.LightCoral;
                }
                else
                    txtCiudad.BackColor = Color.White;


            if ( txtNacimiento.Enabled )
                if ( ! POIutils.isDateSmallerNow(txtNacimiento.Text))
                {
                    txtNacimiento.BackColor = Color.LightCoral;
                    MessageBox.Show("La fecha de nacimiento debe ser menor igual al mes y año actual o es invalido", "Error Fecha Nacimiento", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    bandera = false;
                }
                else
                    txtNacimiento.BackColor = Color.White;

            if (txtNombre.Text == "" || !txtNombre.Text.Trim().Contains(" "))
            {
                bandera = false;
                txtNombre.BackColor = Color.LightCoral;
                MessageBox.Show("Debe completar nombre y apellido como figura en la tarjeta separado por espacio. Primero apellido luego nombre", "Nombre completo como figura en Tarjeta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                txtNombre.BackColor = Color.White;


            // NUMEROS RESTANTES
            if ( !POIutils.isVtolNpsAutorizator() && !txtNumerosRestantes.MaskCompleted)
            {
                bandera = false;
                txtNumerosRestantes.BackColor = Color.LightCoral;
            }
            else
                txtNumerosRestantes.BackColor = Color.White;

            // VENCIMIENTO DE LA TARJETAS
            // 0.3.2: lo sacamos de aca y lo movemos a FrmNumerosRestantes
           /* if (!txtVencimiento.MaskCompleted)
            {
                bandera = false;
                txtVencimiento.BackColor = Color.LightCoral;
            }
            else
                txtVencimiento.BackColor = Color.White;*/


            // PAIS
            if (esExtranjero)
            {
                if (cmbPais.Enabled && cmbPais.SelectedItem == null)
                {
                    bandera = false;
                    cmbPais.Text = "";
                    cmbPais.BackColor = Color.LightCoral;
                }
                else
                    txtDocumento.BackColor = Color.White;
            }

            // DOCUMENTO
            if (cmbTipoPersona.Enabled)
            {
                if (cmbTipoPersona.SelectedIndex >= 0)
                {
                    if (((KeyValuePair<string, string>)cmbTipoPersona.SelectedItem).Key != "E")
                    {
                        if (!txtDocumento.MaskCompleted)
                        {
                            bandera = false;
                            txtDocumento.BackColor = Color.LightCoral;
                        }
                        else
                            txtDocumento.BackColor = Color.White;
                    }
                }
                else
                {
                    cmbTipoPersona.Text = "";
                    bandera = false;
                    txtDocumento.BackColor = Color.LightCoral;
                }
            }
            else
            {
                if (txtDocumento.Text.Length == 0)
                {
                    bandera = false;
                    txtDocumento.BackColor = Color.LightCoral;
                }
                else
                    txtDocumento.BackColor = Color.White;
            }
            return bandera;
        }

        private void btnCompletarCuil_Click(object sender, EventArgs e)
        {
            if (txtDocumento.MaskFull)
            {
                if (cmbTipoPersona.SelectedIndex >= 0)
                {
                    if (((KeyValuePair<string, string>)cmbTipoPersona.SelectedItem).Key == "M" || ((KeyValuePair<string, string>)cmbTipoPersona.SelectedItem).Key == "F")
                    {
                        WebServiceObtenerCuit ws = new WebServiceObtenerCuit(cmbTipoPersona.SelectedValue.ToString(), txtDocumento.Text.Replace(",", ""));
                        txtCuit.Text = WebResponseParser.parseXMLObtenerCuit(ws.getResponse());
                    }
                    else
                    {
                        MessageBox.Show("Solo se puede determinar el CUIL de personas fisicas", "Error al determinar CUIL", MessageBoxButtons.OK);
                        this.Activate();
                    }
                }
                else
                {
                    cmbTipoPersona.Text = "";
                    MessageBox.Show("Tipo de persona no valido", "Tipo de persona invalido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Activate();
                }
            }
            else
            {
                MessageBox.Show("Para poder generar el CUIL debe completar el campo numero de documento y seleccionar el tipo de persona", "Error al determinar CUIL", MessageBoxButtons.OK);
                this.Activate();
            }

            txtDireccionCalle.Focus();
        }

        private void btnVerificarCuit_Click(object sender, EventArgs e)
        {
            if (txtCuit.MaskFull)
            {
                WebServiceVerificarCuit ws = new WebServiceVerificarCuit(txtCuit.Text.Substring(0, 2), txtCuit.Text.Substring(2, 8), txtCuit.Text.Substring(10, 1));
                if (WebResponseParser.parseXMLVerificarCuit(ws.getResponse()))
                    MessageBox.Show("El CUIT/CUIL ha sido validado correctamente", "CUIT/CUIL correcto", MessageBoxButtons.OK);
                else
                    MessageBox.Show("El CUIT/CUIL es erroneo", "CUIT/CUIL incorrecto", MessageBoxButtons.OK);
                this.Activate();
            }
            else
            {
                MessageBox.Show("El campo numero de numero de cuil debe encontrarse completo", "Error al validar CUIL/CUIT", MessageBoxButtons.OK);
                this.Activate();
            }
        }

        private void cmbTipoPersona_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((KeyValuePair<string, string>)cmbTipoPersona.SelectedItem).Key == "E")
            {
                txtDocumento.Enabled = true;
                btnCompletarCuil.Enabled = false;
                txtCuit.Focus();
                tran.tarjeta.owner.genero = "M";
                if ( !esExtranjero )
                    txtDocumento.Enabled = false;
                
            }
            else
            {
                txtDocumento.Enabled = true;
                if (!esExtranjero)
                    btnCompletarCuil.Enabled = true;
                txtDocumento.Focus();
                tran.tarjeta.owner.genero = ((KeyValuePair<string, string>)cmbTipoPersona.SelectedItem).Key;
                
            }

            lblDocumento.Text = (esExtranjero) ? "Pasaporte" : "DNI";

            tran.tarjeta.owner.tipoCuitCuil = ((KeyValuePair<string, string>)cmbTipoPersona.SelectedItem).Key == "E" ? "CUIT" : "CUIL";
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            aceptar(true);
        }


        private string getState()
        {

            string state = txtProvincia.Text;

            if (cmbState.Enabled)
            {
                state = ((KeyValuePair<string, string>)cmbState.SelectedItem).Value;
            }
            return state;
        }

        private void aceptar(bool isFormShort)
        {
            isShortBox = isFormShort;

            if (Configuration.getInstance().tipoAuth == TipoAutorizador.VTOL)
            {
                lblMensaje.Text = "Revise la operacion en el Pinpad";
            }

            tran.eventHandler += this.handleEventTransaction;

            if (validarForm())
            {
                // tarjeta credito debito es para one pay que no valida tipo
                if  ( ( (tran.tarjeta.tipoTarjeta == TipoTarjeta.CREDITO  || tran.tarjeta.tipoTarjeta == TipoTarjeta.CREDITO_DEBITO ) 
                    && (tran.numAutorizacion == "" || tran.numAutorizacion == null) )  )
                {
                    Transaccion resultado = null;
                    Autorizator auth = AutorizatorFactory.getAutorizator( tran );

                    if (isFormShort)
                        mostrarCartelShort();
                    else
                        mostrarCartel();

                    if (auth != null) {
                        if (promos.Count > 0)
                        {
                            //tran.tarjeta.descripcion = promos[0].descripcionTarjeta;
                            tran.tarjeta.codSabre = promos[0].codTarjetaSabre;
                        }else
                        {
                            tran.tarjeta.codSabre = tran.tarjeta.idTarjeta;
                        }

                        //TODO Verificar que no rompa
                        POIutils.updateTarjetaFromBin(tran.tarjeta.primeros6(), tran.tarjeta, true);

                        tran.tarjeta.vencimiento = txtVencimiento.Text;
                        tran.tarjeta.owner.documento = txtDocumento.Text;
                        tran.tarjeta.owner.cuitCuil = txtCuit.Text;

                        tran.tarjeta.owner.provincia = getState();
                        tran.tarjeta.owner.ciudad = txtCiudad.Text;
                        tran.tarjeta.owner.codPostal = txtCodPostal.Text;
                        tran.tarjeta.owner.direccionCalle = txtDireccionCalle.Text;
                        tran.tarjeta.owner.direccionNro = txtDireccionNro.Text;
                        tran.tarjeta.owner.nombre = txtNombre.Text;
                        tran.tarjeta.owner.fechaNacimiento = txtNacimiento.Text;

                        tran.pais = (Pais)cmbPais.SelectedItem;
                        
                        resultado = auth.realizarTransaccion(tran);
                    }
                    else
                        MessageBox.Show("Error al obtener interface de autorizacion", "Error en autorizador", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (resultado != null)
                    {
                        continuar = true;
                        tran = (Transaccion)resultado.Clone();
                        tran.numAutorizacion = resultado.numAutorizacion;
                        tran.numTicket = resultado.numTicket;
                        tran.numLote = resultado.numLote;
                        tran.estado = resultado.estado;
                        tran.tarjeta.codTarjeta = resultado.tarjeta.codTarjeta;
                        tran.fecha = resultado.fecha;
                        pnlMensaje.Visible = false;
                        tran.fecha = resultado.fecha;
                        
                        if ( !POIutils.isVtolNpsAutorizator() )
                            tran.tarjeta.numero = tran.tarjeta.numero.Replace(tran.tarjeta.numero.Substring(tran.tarjeta.numero.IndexOf('x'), tran.tarjeta.numero.LastIndexOf('x') - tran.tarjeta.numero.IndexOf('x') + 1), txtNumerosRestantes.Text);

                        //Solo cerrar form si la tx esta aprobada
                        if (resultado.isAprobada())
                        {
                            //Cierra FrmOtrosDatos
                            this.Close();
                        }
                        else
                        {
                            ocultarCartel();
                            continuar = false;
                        }
                    }
                    else
                    {
                        continuar = false;
                        ocultarCartel();
                        //tran = null;
                        this.cargoOK = false;
                    }
                   
                }
                else
                {
                    continuar = true;
                    if (tran.tipoAuth == TipoAutorizador.POS_INGENICO)
                        tran.fecha = DateTime.Now;

                    // Comentamos porque ya viene el numero de tarjeta completo
                    /*if (tran.tarjeta.isMaestro())
                        tran.tarjeta.numero = tran.tarjeta.numero.Substring(0, 6) + txtNumerosRestantes.Text;
                    
                    
                    //else
                        //tran.tarjeta.numero = tran.tarjeta.numero.Replace(tran.tarjeta.numero.Substring(tran.tarjeta.numero.IndexOf('x'), tran.tarjeta.numero.LastIndexOf('x') - tran.tarjeta.numero.IndexOf('x') + 1), txtNumerosRestantes.Text);
                        */
                    tran.tarjeta.vencimiento = txtVencimiento.Text;
                    if (promos.Count > 0)
                    {
                       // tran.tarjeta.descripcion = promos[0].descripcionTarjeta;
                        tran.tarjeta.codSabre = promos[0].codTarjetaSabre;
                    }
                    tran.tarjeta.vencimiento = txtVencimiento.Text;
                    tran.tarjeta.owner.documento = txtDocumento.Text;
                    tran.tarjeta.owner.cuitCuil = txtCuit.Text;

                    tran.tarjeta.owner.provincia = getState();
                    tran.tarjeta.owner.ciudad = txtCiudad.Text;
                    tran.tarjeta.owner.codPostal = txtCodPostal.Text;
                    tran.tarjeta.owner.direccionCalle = txtDireccionCalle.Text;
                    tran.tarjeta.owner.direccionNro = txtDireccionNro.Text;
                    tran.tarjeta.owner.nombre = txtNombre.Text;
                    tran.pais = (Pais)cmbPais.SelectedItem;

                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Faltan completar campos", "Error de validacion", MessageBoxButtons.OK);
                this.Activate();
            }
        }

        private void cmbPais_SelectedIndexChanged(object sender, EventArgs e)
        {
            tran.pais = (Pais)cmbPais.SelectedItem;
            if (cmbPais.BackColor == Color.LightCoral)
                cmbPais.BackColor = Color.White;

            if ( tran.pais.codigoPais.Equals( "US") || tran.pais.codigoPais.Equals("CA"))
            {
                txtProvincia.Enabled = false;
                txtProvincia.Visible = false;
                cmbState.Visible = true;
                cmbState.Enabled = true;
                cargarStates(tran.pais.codigoPais );
            }else
            {
                txtProvincia.Enabled = true;
                txtProvincia.Visible = true;
                cmbState.Visible = false;
                cmbState.Enabled = false;
            }
        }

        private void mostrarCartel()
        {
            pnlMensaje.Visible = true;
            pnlMensaje.Refresh();
        }

        private void ocultarCartel()
        {
            pnlMensaje.Visible = false;
            pnlMensaje.Refresh();
        }

        private void mostrarCartelShort()
        {
            pnlMensajeShort.Visible = true;
            pnlMensajeShort.Refresh();
        }

        private void txtDocumento_Enter(object sender, EventArgs e)
        {
            this.BeginInvoke((MethodInvoker)delegate ()
            {
                if (txtDocumento.Text.Length == 0)
                    txtDocumento.Select(0, 0);
            });
        }

        private void txtNumerosRestantes_Enter(object sender, EventArgs e)
        {
            this.BeginInvoke((MethodInvoker)delegate ()
            {
                if (txtNumerosRestantes.Text.Length == 0)
                    txtNumerosRestantes.Select(0, 0);
            });
        }

        private void txtVencimiento_Enter(object sender, EventArgs e)
        {
            this.BeginInvoke((MethodInvoker)delegate ()
            {
                if (txtVencimiento.Text.Length == 0)
                    txtVencimiento.Select(0, 0);
            });
        }

        private void txtCuit_Enter(object sender, EventArgs e)
        {
            this.BeginInvoke((MethodInvoker)delegate ()
            {
                if (txtCuit.Text.Length == 0)
                    txtCuit.Select(0, 0);
            });
        }

        private void grdCuotas_KeyUp(object sender, KeyEventArgs e)
        {
            if (grdCuotas.CurrentCell != null)
            {
                int rowIndexActual = grdCuotas.CurrentCell.RowIndex;
                bool modificar = false;
                if (e.KeyCode == Keys.Tab)
                {
                    modificar = true;
                    if (rowIndexActual + 1 < grdCuotas.Rows.Count)
                        grdCuotas.CurrentCell = grdCuotas.Rows[rowIndexActual + 1].Cells[0];
                    else
                        grdCuotas.CurrentCell = grdCuotas.Rows[0].Cells[0];
                }
                else
                    if (e.KeyCode == Keys.Up)
                {
                    modificar = true;
                    if (grdCuotas.CurrentCell.RowIndex >= 0)
                        grdCuotas.CurrentCell = grdCuotas.Rows[rowIndexActual].Cells[0];
                }
                else
                        if (e.KeyCode == Keys.Down)
                {
                    modificar = true;
                    if (rowIndexActual <= (grdCuotas.Rows.Count - 1))
                        grdCuotas.CurrentCell = grdCuotas.Rows[rowIndexActual].Cells[0];
                }
                else
                            if (e.KeyCode == Keys.Left)
                    modificar = true;
                else if (e.KeyCode == Keys.Right)
                    modificar = true;
                if (modificar)
                {
                    txtCuotas.Text = grdCuotas.Rows[grdCuotas.CurrentCell.RowIndex].Cells[0].Value.ToString();
                    txtImporteAAutorizar.Text = grdCuotas.Rows[grdCuotas.CurrentCell.RowIndex].Cells[3].Value.ToString().Replace(",", ".");
                    txtIntereses.Text = (Convert.ToDecimal(grdCuotas.Rows[grdCuotas.CurrentCell.RowIndex].Cells[3].Value.ToString()) - importeAutorizar).ToString("#####0.00").Replace(",", ".");
                    tran.cantCuotas = Convert.ToInt32(grdCuotas.Rows[grdCuotas.CurrentCell.RowIndex].Cells[0].Value);
                    tran.importeTotal = Convert.ToDecimal(grdCuotas.Rows[grdCuotas.CurrentCell.RowIndex].Cells[3].Value, CultureInfo.InvariantCulture);
                    tran.interes = tran.importeTotal - importeAutorizar;
                    grdCuotas.Rows[grdCuotas.CurrentCell.RowIndex].Selected = true;
                }
            }
        }

        private void txtCuit_TextChanged(object sender, EventArgs e)
        {
            if (txtCuit.MaskCompleted)
            {
                if (txtCuit.BackColor != Color.White)
                    txtCuit.BackColor = Color.White;
                btnVerificarCuit.Focus();
            }
        }

        private void txtDocumento_TextChanged(object sender, EventArgs e)
        {
            if ( !esExtranjero && txtDocumento.MaskCompleted)
            {
                if (txtDocumento.BackColor != Color.White)
                    txtDocumento.BackColor = Color.White;
                btnCompletarCuil.Focus();
            }
        }

        private void txtNumerosRestantes_TextChanged(object sender, EventArgs e)
        {
            if (txtNumerosRestantes.MaskCompleted)
            {
                if ( !POIutils.isVtolNpsAutorizator() ) { 
                    if (txtNumerosRestantes.BackColor != Color.White)
                        txtNumerosRestantes.BackColor = Color.White;
                }
                if (txtUltimosNumeros.Visible == true)
                    txtVencimiento.Focus();
            }
        }

        private void txtVencimiento_TextChanged(object sender, EventArgs e)
        {
            if (txtVencimiento.MaskCompleted)
            {
                if (txtVencimiento.BackColor != Color.White)
                    txtVencimiento.BackColor = Color.White;
                cmbTipoPersona.Focus();
            }
        }

        private void btnVolverOne_Click(object sender, EventArgs e)
        {
            volverSinAuth();
            this.cargoOK = false;
        }


        private void chkNombrePagadorPax_CheckedChanged(object sender, EventArgs e)
        {
            if ( chkNombrePagadorPax.Checked)
            {
                string paxName = VBrequestReader.getPropiedad("pnr1.pax1.name");
                if ( paxName != null )
                    txtNombre.Text = paxName.Replace("/", " ");
            }else
            {
                txtNombre.Text = "";
            }
        }


        public void CopyToClipboardWithHeaders(DataGridView _dgv)
        {
            _dgv.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            DataObject dataObj = _dgv.GetClipboardContent();
            if (dataObj != null)
            {
                Clipboard.SetDataObject(dataObj);
                MessageBox.Show("Planes copiados al portapapeles", "Planes Copiados", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnCopiar_Click(object sender, EventArgs e)
        {
            //TODO Probar: Copia Planes del datagrid a la papelera de Windows
            grdCuotas.MultiSelect = true;
            grdCuotas.SelectAll();
            CopyToClipboardWithHeaders(grdCuotas);
            grdCuotas.ClearSelection();
            grdCuotas.MultiSelect = false;
        }
    }
}
