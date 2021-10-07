using CodenameNightwing.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NpsSDK;
using System.Net;
using CodenameNightwing.Forms;
using System.Windows.Forms;
using log4net;
using CodenameNightwing.Varios;
using CodenameNightwing.Config;
using CodenameNightwing.Autorization.NPS.Config;
using System.Globalization;
using CodenameNightwing.WebServices.WSEspecificos.Transacciones;
using CodenameNightwing.WebServices;
using CodenameNightwing.Exceptions;
using CodenameNightwing.FileManager;
using CodenameNightwing.Valtech.FraudService;
using CodenameNightwing.Valtech.FraudService.Response;
using CodenameNightwing.Valtech.FraudService.Request;
using System.Threading;
using CodenameNightwing.Valtech.AuthService;
using CodenameNightwing.Valtech.AuditService;

namespace CodenameNightwing.Autorization.NPS
{
    class NPSIntegrator : Autorizator
    {
        public static readonly new ILog logger = LogManager.GetLogger(typeof(NPSIntegrator));
        protected IWebProxy webProxy = new WebProxy();
        protected NpsSdk npsSdk = null;
        private int incrementalTrxId = 1;
        private string merchantId = null;


        private static NPSIntegrator instance;

        public override string getGatewayName()
        {
            return "NPS";
        }
        private void showTransactionResponse ( Transaccion tran)
        {

            string respuestaNps = "Tipo de Transaccion: " + tran.tipoTrans + "\n" + "Respuesta: " + tran.respuestaHost + "\n" + "Extendida: " + tran.respuestaExtendida + "\n";
            if (tran.isAprobada())
            {
                respuestaNps = respuestaNps + "Cod. Autorizacion: " + tran.numAutorizacion;
            }
            logger.Info("Resultado Transaccion NPS" + respuestaNps);
            MessageBox.Show(respuestaNps, "Resultado Transaccion NPS", MessageBoxButtons.OK, (tran.isAprobada() || tran.isEnCurso()) ? MessageBoxIcon.Information : MessageBoxIcon.Error );

        }


        public static void setNpsSdk()
        {
            switch (Configuration.getInstance().environment)
            {
                case "QA":
                    String npsKeyQA = (Configuration.getInstance().npsMerchantId.Equals("aeroarg_web") ? "G14nPoVdZN3XoAaSysl2D4wqE9bI1KRFLT1lIWMrrQMfav5WnrXztRp8YOOZjqm2" : "GYioAJbOSgbVzzckVfWLINiGGhTcBlFGAfBtKCystBWmj9TcF6vfBKuogigVxbMS");
                    instance.npsSdk = new NpsSdk(new NpsSdk.WsdlHandlerConfiguration(LogLevel.Debug, NpsSdk.NpsEnvironment.Staging, npsKeyQA, new DebugLogger(), 60, true));
                    break;
                case "PROD":
                    String npsKeyPROD = (Configuration.getInstance().npsMerchantId.Equals("aeroarg_web") ? "zqyHQTpFxTmIVKeFYAgw2RhqyJI6gCVd2SuRvjeqBQ8gimtHuXDwztnvC5ztGVUG" : "zqyHQTpFxTmIVKeFYAgw2RhqyJI6gCVd2SuRvjeqBQ8gimtHuXDwztnvC5ztGVUG");
                    instance.npsSdk = new NpsSdk(new NpsSdk.WsdlHandlerConfiguration(LogLevel.Info, NpsSdk.NpsEnvironment.Production, npsKeyPROD, new DebugLogger(), 60, false));
                    break;
                case "DEV":
                    instance.npsSdk = new NpsSdk(new NpsSdk.WsdlHandlerConfiguration(LogLevel.Debug, NpsSdk.NpsEnvironment.Staging, "GYioAJbOSgbVzzckVfWLINiGGhTcBlFGAfBtKCystBWmj9TcF6vfBKuogigVxbMS", new DebugLogger(), 60, true));
                    break;
            }
        }

        public static NPSIntegrator Instance
        {
            get
            {
                try
                {
                    if (instance == null)
                    {
                        instance = new NPSIntegrator();
                        Thread thread = new Thread(new ThreadStart( NPSIntegrator.setNpsSdk ));
                        thread.Start();
                        logger.Info("Se creo el NPSSDK para el ambiente: " + Configuration.getInstance().environment);

                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error al crear el NPSSDK", "Error con NPS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    logger.Error("Error al obtener instancia de NPSIntegrator", e);
                    instance = null;
                }
                return instance;
            }
        }

        private NPSIntegrator()
        {
            
        }

        override public Tarjeta solicitarNumeroTarjeta(string tipoTrans, Transaccion trans)
        {

            logger.Debug("Comienzo de solicitarNumeroTarjeta");
            trans.tarjeta = null;

            try
            {
                FrmIngresarBin fBin = new FrmIngresarBin();
                trans.tarjeta = new Tarjeta();
                
                while (fBin.bin == null || trans.tarjeta.codSabre == null)
                {
                    fBin.ShowDialog();
                    if (!fBin.isCorrect())
                    {
                        return null;
                    }
                    trans.tarjeta.numero = fBin.bin + "XXXXXXXXXX";
                    if (fBin.tarjeta != null)
                        trans.tarjeta.codSabre = fBin.tarjeta.codTarjetaSabre;
                    else
                        POIutils.updateTarjetaFromBin(trans.tarjeta.primeros6(), trans.tarjeta);
                    if (trans.tarjeta.codSabre == null)
                        MessageBox.Show("Por favor intente nuevamente seleccionando tipo de tarjeta en el combo", "Error al obtener el tipo de tarjeta", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }



            catch (Exception e)
            {
                MessageBox.Show("Error al obtener el BIN", "Error General", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Error("Error al leer tarjeta", e);
                cancelarTransaccion();
            }

            logger.Debug("Fin de solicitarNumeroTarjeta");
            return trans.tarjeta;

        }

        public override void cancelarLecturaTarjeta() {
            return;
        }

        public override void cancelarTransaccion()
        {
            return;
        }

        public override bool verificarConexion()
        {
            return false;
        }

        public override bool reimprimirUltimoCupon()
        {
            return false;
        }

        public override bool reimprimirCierreLote()
        {
            return false;
        }

        private RootElement getCommonData( Transaccion tran )
        {
            RootElement data = new RootElement();

            data.Add("psp_Version", "2.2");

            string merchandValue = "";
            if ( tran.tarjeta.codSabre != null && tran.tarjeta.codSabre.Equals("NT"))
            {
                merchandValue = Configuration.getInstance().npsMerchIdNat ; 
            }
            else
            {merchandValue =  Configuration.getInstance().npsMerchantId ;
            }

            string merchant = getParamValueFromPromo("psp_MerchantId", tran.promocion, merchandValue).Trim();
            data.Add("psp_MerchantId", merchant);
            this.merchantId = merchant;

            logger.Info("MerchantId a NPS: " + merchant);

            // Esto esta bien ? cambie WEB por CALL
            string source = "";
            if (tran.currency != "USD")
            {
                source = (tran != null && tran.tipoIngreso.Equals(TipoIngresoTarjeta.IVR)) ? "IVR" : "TELORDER";
            }
            else
            {
                source = "WEB";
            }

            data.Add("psp_TxSource", source);

            DateTime dateToday = System.DateTime.Now;
            string datePos = dateToday.ToNPSDateString();
            data.Add("psp_PosDateTime", datePos );

            if (tran.primaryEmail != null)
                data.Add("psp_PurchaseDescription", tran.primaryPnr);

            if ( tran.primaryEmail != null && tran.primaryEmail.Contains("@"))
                data.Add("psp_CustomerMail", tran.primaryEmail);

            string refAdd = "";
            string opType = FraudServiceRequest.getOperationType(tran) + " from " + tran.primaryPnr;
            switch (tran.tipoTrans)
            {
                case TipoTransaccion.COMPRA:
                    refAdd = "BUY";
                    data.Add("psp_BillingDetails", getPayerData(tran));
                    data.Add("psp_AirlineDetails", getAirlineData(tran));
                    break;
                case TipoTransaccion.ANULACION:
                    refAdd = "VOID";
                    break;
                case TipoTransaccion.DEVOLUCION:
                    refAdd = "REFUND";
                    break;
                case TipoTransaccion.CIERRE:
                    refAdd = "";
                    break;
                default: tran = null; break;
            }

            refAdd = refAdd + " at " + opType;

            data.Add("psp_MerchAdditionalRef", refAdd);
           

            return data;

        }

        private ComplexElement getPayerData( Transaccion tran)
        {


            ComplexElement pspBillingDetails = new ComplexElement();

            if ( tran.tarjeta != null && tran.tarjeta.owner != null && tran.tarjeta.owner.nombre != null ) { 
                ComplexElement Person = new ComplexElement();
                Person.Add("FirstName",  ((tran.tarjeta.owner.nombre != null) ? tran.tarjeta.owner.nombre.Split(' ')[1]: null));
                if ( !(tran.tarjeta.owner.genero == null || tran.tarjeta.owner.genero.Equals("E")))
                    Person.Add("Gender", tran.tarjeta.owner.genero);

                Person.Add("IDNumber", tran.tarjeta.owner.documento );
                Person.Add("LastName", ((tran.tarjeta.owner.nombre != null) ? tran.tarjeta.owner.nombre.Split(' ')[0] : null));
                Person.Add("Nationality",  tran.country );

                DateTime pDate;
                if (DateTime.TryParseExact( tran.tarjeta.owner.fechaNacimiento , "ddMMyyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out pDate))
                {
                    Person.Add("DateOfBirth", pDate.ToNPSDateStringBirth() );
                    Person.Add("IDType",  tran.tarjeta.owner.esExtranjero?"100":"200");
                }
                

                pspBillingDetails.Add("Person", Person);

                if (tran.tarjeta.owner.codPostal != null && tran.tarjeta.owner.codPostal.Trim().Length > 0)
                {
                    ComplexElement Address = new ComplexElement();
                    Address.Add("City", tran.tarjeta.owner.ciudad);
                    Address.Add("Country", tran.country);
                    Address.Add("Street", tran.tarjeta.owner.direccionCalle);
                    Address.Add("HouseNumber", tran.tarjeta.owner.direccionNro);
                    Address.Add("ZipCode", tran.tarjeta.owner.codPostal);

                    pspBillingDetails.Add("Address", Address);
                }
                
            }

            return pspBillingDetails;

        }

        private ComplexElement getAirlineData( Transaccion tran )
        {

            ComplexElement pspAirlineDetails = new ComplexElement();
            pspAirlineDetails.Add("PNR",  tran.primaryPnr );

            if ( tran.listPnr != null && tran.listPnr.Count >= 1)
            {
                ComplexElementArray Passengers = new ComplexElementArray();
                Pnr primaryPnr = tran.listPnr.First<Pnr>();
                foreach ( Pasajero pax in  primaryPnr.pasajeros )
                {
                    ComplexElementArrayItem Passengers1 = new ComplexElementArrayItem();
                    Passengers1.Add("FirstName",  pax.nombre.Split('/')[1]);
                    Passengers1.Add("LastName", pax.nombre.Split('/')[0]);

                    Passengers.Add(Passengers1);
                }
               

                pspAirlineDetails.Add("Passengers", Passengers);

            }
            

            return pspAirlineDetails;


        }


        /*private string getTokenFromPayment( string TransactionId )
        {

            try
            {

                RootElement data = getCommonData();

                data.Add("psp_TransactionId", TransactionId );
                data.Add("psp_PaymentMethodTag", "Aerolineas Card");
                RootElement response = npsSdk.CreatePaymentMethodFromPayment(data);


                response.GetValue("psp_ResponseCod");
                response.GetValue("psp_ResponseMsg");
                response.GetValue("psp_MerchantId");

                ComplexElement pspPaymentMethod = response.GetComplexElement("psp_PaymentMethod");
                pspPaymentMethod.GetValue("PaymentMethodId");
                pspPaymentMethod.GetValue("PaymentMethodTag");
                pspPaymentMethod.GetValue("Product");

                ComplexElement cardOutputDetails = pspPaymentMethod.GetComplexElement("CardOutputDetails");
                cardOutputDetails.GetValue("ExpirationDate");
                cardOutputDetails.GetValue("ExpirationYear");
                cardOutputDetails.GetValue("ExpirationMonth");
                cardOutputDetails.GetValue("IIN");
                cardOutputDetails.GetValue("Last4");
                cardOutputDetails.GetValue("NumberLength");
                cardOutputDetails.GetValue("MaskedNumber");
                cardOutputDetails.GetValue("MaskedNumberAlternative");

                pspPaymentMethod.GetValue("FingerPrint");
                pspPaymentMethod.GetValue("CreatedAt");
                pspPaymentMethod.GetValue("UpdatedAt");

                response.GetValue("psp_CustomerId");
                response.GetValue("psp_PosDateTime");
            }
            catch (Exception ex)
            {
                //Code to handle error
            }

            return null;
        }*/

        private Transaccion abrirFormularioEnlace(Transaccion tran, string enlace , string idTrx)
        {
            FrmEnlace frmEnlace = null;
            try
            {
                frmEnlace = new FrmEnlace(tran, enlace);
                tran.tipoIngreso = TipoIngresoTarjeta.FORM_3P;
                frmEnlace.ShowDialog();
                switch (frmEnlace.seleccionEnlace)
                {

                    case TipoSeleccionEnlace.CANCELAR:

                        tran = verifyNPSTrx(tran, idTrx);
                        if (tran.isAprobada())
                        {
                            MessageBox.Show("La transacción ya se ha aprobado. Se producirá la anulación.", "Anulación de transacción", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            tran.tarjeta.cvc = "123";
                            realizarVoideo(tran);
                        }
                        tran.isRedirectPayment = false;
                        return null;

                    case TipoSeleccionEnlace.VERIFICAR:
                        tran = verifyNPSTrx( tran , idTrx);
                        if (tran.isEnCurso())
                        {
                            showTransactionResponse( tran );
                            return abrirFormularioEnlace( tran, enlace, idTrx );
                        }

                        if (tran.isAprobada())
                        {
                            // tiene que coincidir el bin
                            if (!tran.tarjeta.numero.StartsWith(tran.tarjeta.originalNumber.Substring(0, 6)))
                            {
                                MessageBox.Show("Ingreso en la pagina de NPS otro BIN: " + tran.tarjeta.numero.Substring(0,6) + " del dado al operador : " + tran.tarjeta.originalNumber.Substring(0, 6) + ". Se producirá la anulación", "Bin incorrecto", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                tran.isRedirectPayment = false;
                                tran.tarjeta.cvc = "123";
                                realizarVoideo(tran);
                                return null;
                            }
                        }


                        return tran;
                    case TipoSeleccionEnlace.COPIAR_TEXTO:
                        return abrirFormularioEnlace(tran, enlace, idTrx);
                    case TipoSeleccionEnlace.COPIAR_ENLACE:
                        return abrirFormularioEnlace(tran, enlace, idTrx);
                }

            }
            catch (Exception e)
            {
                string msg = "Error obteniendo datos de la tarjeta con NPS integrator";
                logger.Error(msg);
                logger.Error(e);
                throw new Exception(msg);
            }

            return null;
        }

        protected override Transaccion realizarPago(Transaccion paraPagar)
        {
            paraPagar.gateway = "N";

            paraPagar.estadoDescripcion = TipoEstadoTransaccion.CREADA;
            //**********************************************************************************
            // 1. OBTENER DATOS DE LA TARJETA, POR IVR O POR FORMA DIRECTA
            //**********************************************************************************
            paraPagar.tarjeta = obtenerDatosTarjetaIvr(paraPagar);

            if (!paraPagar.isRedirectPayment)
            {
                if (paraPagar.tarjeta.cvc == null || paraPagar.tarjeta.cvc == "" ||
                    paraPagar.tarjeta.vencimiento == null || paraPagar.tarjeta.vencimiento == "")
                {
                    return null;
                }

            }


            paraPagar.estadoDescripcion = TipoEstadoTransaccion.PREPARANDO_DATOS;

            //**********************************************************************************
            // 2. PREPARAR DATOS PARA EL PAGO
            //**********************************************************************************
            RootElement data = null;
            string idTxRef = null;

            try
            {
                data = getCommonData( paraPagar );

                WebServiceInsertTransaction wsinsertar = new WebServiceInsertTransaction(paraPagar);
                paraPagar.trxReferenceId = WebResponseParser.parseXMLInsert(wsinsertar.getResponse());

                if (paraPagar.trxReferenceId < 0)
                {
                    throw new NPSException("No se pudo insertar en la base de datos. Llamar a Sistemas ARSA");
                }
                else
                {
                    string txRef = paraPagar.trxReferenceId.ToString().ToBase36String();
                    // poner id unico + id incremental
                    idTxRef = txRef + "-" + incrementalTrxId;
                    data.Add("psp_MerchTxRef", idTxRef);
                    // poner id unico
                    data.Add("psp_MerchOrderId", txRef);

                    logger.Info("Trx SALE ID : " + txRef);
                    incrementalTrxId++;

                }

                logger.Info("OPERACION: SALE , IMPORTE ORIGINAL: " + paraPagar.importeTotal);
                string importeFormatoArg = paraPagar.importeTotal.ToString("#####0.00", CultureInfo.CreateSpecificCulture("es-AR"));
                logger.Info("OPERACION: SALE , IMPORTE FORMATO ARGENTINA: " + importeFormatoArg);
                string importeFormatoArgSinComa = importeFormatoArg.Replace(",", "");
                logger.Info("OPERACION: SALE , IMPORTE FORMATO ARGENTINA SIN COMA ENVIADO A NPS: " + importeFormatoArgSinComa);

                data.Add("psp_Amount", importeFormatoArgSinComa);
                
                string currencyCode = NPSConfigurator.getCurrencyCode(paraPagar.currency);
                data.Add("psp_Currency", currencyCode);
                logger.Info("Currency a NPS: " + currencyCode + " Seleccionado: " + paraPagar.currency);

                string countryCode = NPSConfigurator.getCountryCode( paraPagar.country );
                data.Add("psp_Country", countryCode );
                logger.Info("Country a NPS: " + countryCode + " Seleccionado: " + paraPagar.country);

                if ( paraPagar.tarjeta.codSabre == null && (paraPagar.tarjeta.tipoTarjeta.Equals(TipoTarjeta.CREDITO) || paraPagar.tarjeta.tipoTarjeta.Equals(TipoTarjeta.CREDITO_DEBITO)))
                {
                    POIutils.updateTarjetaFromBin(paraPagar.tarjeta.primeros6(), paraPagar.tarjeta, true);
                }
                

                string productCode = NPSConfigurator.getProductCode( paraPagar.tarjeta );
                productCode = getParamValueFromPromo("psp_Product", paraPagar.promocion, productCode);
                data.Add("psp_Product", productCode);
                logger.Info("Producto enviado a NPS: " + productCode);


                // DATOS DE TARJETA
                if ( paraPagar.isRedirectPayment)
                {
                    data.Add("psp_ReturnURL", Configuration.getInstance().confirmationPaymentPage );
                    data.Add("psp_FrmLanguage", paraPagar.idiomaFormNPS);
                    data.Add("psp_FrmTimeout", Configuration.getInstance().timeOutPaymentForm);
                    
                }
                else
                {
                    data.Add("psp_CardNumber", paraPagar.tarjeta.numero);
                    string fVencimiento = paraPagar.tarjeta.getVencimientoPrimeroAnios();
                    data.Add("psp_CardExpDate", fVencimiento);
                    data.Add("psp_CardSecurityCode", paraPagar.tarjeta.cvc);
                }
                

                string cuotas = paraPagar.cantCuotas.ToString();
                // Plan unicamente para las tarjetas Naranja
                // Si es Naranja y plan Z = cuotas 11

                if (paraPagar.cantCuotas > 1)
                {
                    if (productCode.Equals("9") && paraPagar.cantCuotas == 11)
                    {
                        cuotas = "1";
                        string pspPlan = "PlanZ";
                        pspPlan = getParamValueFromPromo("psp_plan", paraPagar.promocion, pspPlan);
                        data.Add("psp_Plan", pspPlan);
                        logger.Info("Plan enviado a NPS: " + pspPlan);
                    }
                    else
                    {
                        string pspPlan = getParamValueFromPromo("psp_plan", paraPagar.promocion, null);
                        if (pspPlan != null)
                        {
                            data.Add("psp_Plan", pspPlan);
                            logger.Info("Plan enviado a NPS: " + pspPlan);
                        }
                    }
                }
                
                cuotas = getParamValueFromPromo("psp_NumPayments", paraPagar.promocion, cuotas);
                data.Add("psp_NumPayments", cuotas);
                logger.Info("Cuotas enviado a NPS: " + cuotas);
            }
            catch (Exception e)
            {
                string msg = "Error preparando datos de la transaccion con NPS integrator";
                logger.Error(msg);
                logger.Error(e);
                throw new NPSException(msg);
            }
            //**********************************************************************************
            // 3. PROCESAR PAGO
            //**********************************************************************************

            paraPagar.estadoDescripcion = TipoEstadoTransaccion.PROCESANDO;
            RootElement npsResponse = ( paraPagar.isRedirectPayment) ? npsSdk.PayOnLine_3p(data): npsSdk.PayOnLine_2p(data);
            logger.Info("NPS RESPONSE : " + npsResponse.GetValue("psp_ResponseMsg"));
            
            //**********************************************************************************
            // 4. OBTENER LOS DATOS DEL PAGO
            //**********************************************************************************
            Transaccion resultado = null;

            try
            {
                paraPagar.estadoDescripcion = TipoEstadoTransaccion.OBTENIENDO_DATOS;

                if ( paraPagar.isRedirectPayment){
                    
                    string enlace = npsResponse.GetValue("psp_FrontPSP_URL");
                    if (enlace == null)
                    {
                
                        string msg = "Error generando el enlace de NPS: " + (resultado!=null?resultado.trxId.ToString():"");
                        logger.Error(msg);
                        throw new NPSException(msg);
                    }
                        
                    resultado = abrirFormularioEnlace( paraPagar, enlace, idTxRef);
                }
                else
                {
                    resultado = updateTransacionFromResponse(paraPagar, npsResponse, idTxRef);
                }

                if ( resultado == null)
                {
                    return null;
                }

                WebServiceUpdateTransaction wsUpdate = new WebServiceUpdateTransaction(resultado);
                bool isUpdateDone = WebResponseParser.parseXMLDatosConfirmado(wsUpdate.getResponse());

                if (!isUpdateDone)
                {
                        string msg = "Error insertando registro de transaccion con NPS con numero unico: " + (resultado!=null?resultado.trxId.ToString():"");

                    logger.Error(msg);
                    throw new NPSException(msg);
                }

                logger.Info("Trx SALE ID : " + paraPagar.trxReferenceId.ToString().ToBase36String() + " grabada con exito");


                if ( checkFraud( resultado) )
                {
                    AnulationFile.grabarTransaccion(resultado);
                    showTransactionResponse(resultado);
                    AuditServiceController.postAudit(new AuditData(AuditVendorId.NPS, AuditOperationType.PAYMENT_NOTIFICATION, resultado, 0, data.ToString(), HttpStatusCode.OK, npsResponse.ToString() ));
                }
                else
                {
                    resultado.tipoOperacion = TipoOperacion.ANULACION;
                    resultado.tipoTrans = TipoTransaccion.ANULACION;
                    realizarVoideo(resultado);
                    return null;
                }

                
            }
            catch (Exception e)
            {
                string msg = "Error obteniendo y guardando datos de la transaccion con NPS integrator";
                logger.Error(msg);
                logger.Error(e);

                if ( resultado != null && resultado.isAprobada())
                {
                    //reversar la transaccion por algún error
                    logger.Info("Se anula la transaccion + " + resultado.trxReferenceId + " por excepcion");
                    MessageBox.Show("No se pudo procesar correctamente el resultado de la transaccion. Se procede a anularla", "Resultado Transaccion NPS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    realizarVoideo( resultado );
                }

                throw new NPSException(msg);
            }
            return resultado;
        }

        private Transaccion verifyNPSTrx ( Transaccion tran ,  string psp_MerchTxRef )
        {

            RootElement data = new RootElement();

            data.Add("psp_Version", "2.2");
            data.Add("psp_MerchantId", this.merchantId);
            data.Add("psp_QueryCriteria", "M");
            data.Add("psp_QueryCriteriaId", psp_MerchTxRef);

            DateTime dateToday = System.DateTime.Now;
            string datePos = dateToday.ToNPSDateString();
            data.Add("psp_PosDateTime", datePos);

            RootElement response = npsSdk.SimpleQueryTx(data);

            return updateTransacionFromResponse(tran, response , psp_MerchTxRef);

        }


        private string getCardNumber(Transaccion tran, string merchantId, string psp_MerchTxRef)
        {

            RootElement data = new RootElement();

            data.Add("psp_Version", "2.2");
            data.Add("psp_MerchantId", merchantId);
            data.Add("psp_QueryCriteria", "M");
            data.Add("psp_QueryCriteriaId", psp_MerchTxRef);

            DateTime dateToday = System.DateTime.Now;
            string datePos = dateToday.ToNPSDateString();
            data.Add("psp_PosDateTime", datePos);

            RootElement response = npsSdk.QueryCardNumber(data);

            return response.GetValue("psp_CardNumber");

        }

        private Transaccion updateTransacionFromResponse( Transaccion tran , ComplexElement response , string psp_MerchTxRef)
        {

            Transaccion resultado = (Transaccion)tran.Clone();

            if ( tran.isRedirectPayment)
            {
                response = response.GetComplexElement("psp_Transaction");
            }
            

            resultado.trxId = response.GetValue("psp_TransactionId");
            resultado.pdv.sucursal = Configuration.getInstance().sucursal;
            resultado.pdv.caja = Configuration.getInstance().caja;
            resultado.moneda = "$";
            resultado.numAutorizacion = response.GetValue("psp_AuthorizationCode");//codigo de autorizacion
            resultado.numLote = Convert.ToInt32(response.GetValue("psp_BatchNro"));
            resultado.numTicket = response.GetValue("psp_TicketNumber");
            // INICIO DE CAMPOS AGREGADOS POR NECESIDAD DE ESPECIFICACIONES EN CUPON
            resultado.nroTerminal = response.GetValue("psp_ClExternalTerminal");
            resultado.modo = TipoModoTransaccion.ONLINE;
            string sFecha = response.GetValue("psp_PosDateTime");
            if (sFecha.Contains("-"))
            {
                sFecha = sFecha.Substring(0, sFecha.IndexOf("-"));
            }
            try
            {
                DateTime auxFecha = DateTime.ParseExact(sFecha, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                resultado.fecha = auxFecha;
            }
            catch ( Exception e)
            {
                resultado.fecha = System.DateTime.Now;
            }
            
            resultado.respuestaHost = response.GetValue("psp_ResponseCod") + " - " + response.GetValue("psp_ResponseMsg");
            resultado.respuestaExtendida = response.GetValue("psp_ResponseExtended");

            if (tran.isRedirectPayment && resultado.isAprobada())
            {
                resultado.tarjeta.originalNumber = resultado.tarjeta.numero;
                string cardNumber = getCardNumber( tran, merchantId, psp_MerchTxRef);
                resultado.tarjeta.numero = cardNumber;
                DateTime expDate = System.DateTime.Now.AddMonths(1);
                resultado.tarjeta.vencimiento = expDate.ToString("MMyy");

            }
            return resultado;
        }

        protected override Transaccion manejarAutorizacionTelefonica(Transaccion aAutorizarTelefonicamente)
        {
            return null;
        }


        protected override Transaccion realizarDevolucion(Transaccion aDevolver)
        {
            aDevolver.gateway = "N";
            aDevolver.estadoDescripcion = TipoEstadoTransaccion.CREADA;

            //TODO Revisar tipo DEVOLUCION
            aDevolver.tipoTrans = TipoTransaccion.ANULACION;
            //**********************************************************************************
            // 1. OBTENER DATOS DE LA TARJETA, POR IVR O POR FORMA DIRECTA
            //**********************************************************************************
            // IVR
            // Verificar si el IVR está online para sacar los datos de la tarjeta de él
            if (aDevolver.tarjeta.faltanDatosTarjeta())
                aDevolver.tarjeta = obtenerDatosTarjetaIvr(aDevolver);

            if (aDevolver.tipoIngreso != TipoIngresoTarjeta.FORM_3P) {

                if (aDevolver.tarjeta.cvc == null || aDevolver.tarjeta.cvc == "" ||
                aDevolver.tarjeta.vencimiento == null || aDevolver.tarjeta.vencimiento == "")
                {
                    return null;
                }
            }

            aDevolver.estadoDescripcion = TipoEstadoTransaccion.PREPARANDO_DATOS;

            //**********************************************************************************
            // 2. PREPARAR DATOS PARA EL PAGO
            //**********************************************************************************
            RootElement data = null;
            string idTrx = null;
            try
            {
                data = getCommonData(aDevolver);

                WebServiceInsertTransaction wsinsertar = new WebServiceInsertTransaction(aDevolver);
                aDevolver.trxReferenceId = WebResponseParser.parseXMLInsert(wsinsertar.getResponse());

                if (aDevolver.trxReferenceId < 0)
                {
                    throw new NPSException("No se pudo insertar en la base de datos. Llamar a Sistemas ARSA");
                }
                else
                {
                    string txRef = aDevolver.trxReferenceId.ToString().ToBase36String();
                    idTrx = txRef + "-" + incrementalTrxId;
                    // poner id unico + id incremental
                    data.Add("psp_MerchTxRef", txRef + "-" + incrementalTrxId);
                    // poner id unico
                    data.Add("psp_MerchOrderId", txRef);

                    logger.Info("Trx SALE ID : " + txRef);
                    incrementalTrxId++;

                }

                logger.Info("OPERACION: SALE , IMPORTE ORIGINAL: " + aDevolver.importeTotal);
                string importeFormatoArg = aDevolver.importeTotal.ToString("#####0.00", CultureInfo.CreateSpecificCulture("es-AR"));
                logger.Info("OPERACION: SALE , IMPORTE FORMATO ARGENTINA: " + importeFormatoArg);
                string importeFormatoArgSinComa = importeFormatoArg.Replace(",", "");
                logger.Info("OPERACION: SALE , IMPORTE FORMATO ARGENTINA SIN COMA ENVIADO A NPS: " + importeFormatoArgSinComa);

                data.Add("psp_AmountToRefund", importeFormatoArgSinComa);
                data.Add("psp_TransactionId_Orig", aDevolver.trxId);

                // procesa en las mismas cuotas que la compra original segun NPS
                //data.Add("psp_NumPayments", aDevolver.cantCuotas.ToString());

                if (Configuration.getInstance().interactUser != null && Configuration.getInstance().interactUser.Length > 0)
                {
                    data.Add("psp_UserId", Configuration.getInstance().interactUser);
                }


                // DATOS DE TARJETA
                if (aDevolver.tipoIngreso != TipoIngresoTarjeta.FORM_3P)
                {
                    data.Add("psp_CardNumber", aDevolver.tarjeta.numero);
                    string fVencimiento = aDevolver.tarjeta.getVencimientoPrimeroAnios();
                    data.Add("psp_CardExpDate", fVencimiento);
                    data.Add("psp_CardSecurityCode", aDevolver.tarjeta.cvc);
                }

            }
            catch (Exception e)
            {
                string msg = "Error preparando datos de la transaccion con NPS integrator";
                logger.Error(msg);
                logger.Error(e);
                throw new NPSException(msg);
            }
            //**********************************************************************************
            // 3. PROCESAR DEVOLUCION
            //**********************************************************************************
            aDevolver.estadoDescripcion = TipoEstadoTransaccion.PROCESANDO;

            RootElement npsResponse = npsSdk.Refund(data);

            //**********************************************************************************
            // 4. OBTENER LOS DATOS DEL PAGO
            //**********************************************************************************
            Transaccion resultado = null;

            if (npsResponse.GetValue("psp_ResponseCod") != "0") {
                string respuestaHost = npsResponse.GetValue("psp_ResponseCod") + " - " + npsResponse.GetValue("psp_ResponseMsg");
                string respuestaExtendida = npsResponse.GetValue("psp_ResponseExtended");

                string msg = "Transaccion Rechazada: ResponseCode" + respuestaHost + " " + respuestaExtendida;
                logger.Error(msg);
                throw new NPSException(msg);
            }


            try
            {
                aDevolver.estadoDescripcion = TipoEstadoTransaccion.OBTENIENDO_DATOS;

                resultado = updateTransacionFromResponse(aDevolver, npsResponse, idTrx);

                WebServiceUpdateTransaction wsUpdate = new WebServiceUpdateTransaction(resultado);
                bool isUpdateDone = WebResponseParser.parseXMLDatosConfirmado(wsUpdate.getResponse());

                if (!isUpdateDone)
                {
                    string msg = "Error insertando registro de transaccion con NPS con numero unico: " + resultado.trxId.ToString();
                    throw new NPSException(msg);
                }

                logger.Info("Trx SALE ID : " + aDevolver.trxReferenceId.ToString().ToBase36String() + " grabada con exito");

                AnulationFile.grabarTransaccion(resultado);
                showTransactionResponse(resultado);
                AuditServiceController.postAudit(new AuditData(AuditVendorId.NPS, resultado.tipoTrans.Equals(TipoTransaccion.ANULACION) ? AuditOperationType.PAYMENT_ANULATION : AuditOperationType.PAYMENT_REFUND, resultado, 0, data.ToString(), HttpStatusCode.OK, npsResponse.ToString()));

            }
            catch (Exception e)
            {
                string msg = "Error obteniendo y guardando datos de la transaccion con NPS integrator";
                logger.Error(msg);
                logger.Error(e);

                if (resultado != null && resultado.isAprobada())
                {
                    //reversar la transaccion por algún error
                    logger.Info("No se pudo procesar la transaccion" + resultado.trxReferenceId + " por excepcion");
                    MessageBox.Show("No se pudo procesar correctamente el resultado de la transaccion de la Anulación o Devolución. Verifique con Adm NPS y termine transaccion", "Resultado Transaccion NPS", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

                throw new NPSException(msg);
            }
            return resultado;
        }

        protected override Transaccion realizarVoideo(Transaccion aVoidear)
        {
            return realizarDevolucion( aVoidear );
        }

        protected override Transaccion realizarCierreLote(Transaccion aCerrar)
        {
            return null;
        }

        public override string getNombreTransaccion(Transaccion trans)
        {
            return null;
        }

    }
}
