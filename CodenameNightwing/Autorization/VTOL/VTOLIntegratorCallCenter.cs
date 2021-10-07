using CodenameNightwing.BusinessLogic;
using CodenameNightwing.Config;
using CodenameNightwing.FileManager;
using CodenameNightwing.Printer;
using CodenameNightwing.Varios;
using CodenameNightwing.WebServices;
using CodenameNightwing.Autorization.VTOL.Config;
using CodenameNightwing.WebServices.WSEspecificos.Transacciones;
using System;
using System.Text;
using System.Windows.Forms;
using VtolClientLib;
using CodenameNightwing.Autorization.VTOL.Config.Elementos;
using CodenameNightwing.Forms;
using CodenameNightwing.Exceptions;
using log4net;

namespace CodenameNightwing.Autorization.VTOL
{
    class VTOLIntegratorCallCenter : Autorizator
    {
        private static VirtualVtolClient svc;
        private static VtolNode vn;
        private static string lastTrxId = "";
        public static readonly new ILog logger = LogManager.GetLogger(typeof(VTOLIntegratorCallCenter));

        public VtolNode getVTOLNode() {
            return vn;
        }
        private VTOLIntegratorCallCenter() { }

        private static VTOLIntegratorCallCenter instance;

        public static VTOLIntegratorCallCenter Instance
        {
            get
            {
                try
                {
                    if (instance == null)
                    {
                        if (!VTOLFullLibrary.checkServer())
                        {
                            VTOLFullLibrary.startServer();
                            logger.Info("Servicio de VTOL iniciado por el programa");
                        }
                            
                        instance = new VTOLIntegratorCallCenter();
                        svc = crearVirtualVtolClient();
                        svc.Init();
                        vn = svc.getNode();

                        logger.Info("Se obtuvo el nodo de vtol con la sucursal " + Configuration.getInstance().sucursal + " y caja virtual"  );
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error al obtener instancia de VTOLIntegrator", "Error con VTOL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    logger.Error("Error al obtener instancia de VTOLIntegrator", e);
                    instance = null;
                }
                return instance;
            }
        }

        public override Tarjeta solicitarNumeroTarjeta(string tipoTrans , Transaccion trans)
        {

            trans.tarjeta = null;

            try
            {
                FrmIngresarBin fBin = new FrmIngresarBin();
                fBin.ShowDialog();
                if (fBin.bin == null)
                {
                    return null;
                }
                trans.tarjeta = new Tarjeta();
                trans.tarjeta.numero = fBin.bin + "XXXXXXXXXX";

            }
            catch (Exception e)
            {
                MessageBox.Show("Error al obtener el BIN", "Error General", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Error("Error al leer tarjeta", e);
                cancelarTransaccion();
            }

            return trans.tarjeta;
        }

        public override void cancelarTransaccion()
        {
            try
            {
                // Cancela lectura de Tarjeta
                vn.CreateTransaction();
                vn.AddField(11, "closeSession");
                vn.AddField(1008, "CANCEL");
                vn.SendTransaction();
                logger.Info("Se envio closeSession con CANCEL a VTOL");
            }
            catch ( Exception e)
            {
                logger.Error("Error al cancelar la transaccion: " + e.ToString());
            }
        }

        public override bool verificarConexion()
        {
            bool isVtolConnected = crearSesion();
            if ( isVtolConnected)
            {
                cancelarTransaccion();
            }
            return isVtolConnected;
        }

        public override bool reimprimirUltimoCupon()
        {
            throw new NotImplementedException();
        }

        public override bool reimprimirCierreLote()
        {
            throw new NotImplementedException();
        }

        public override void cancelarLecturaTarjeta()
        {
            throw new NotImplementedException();
        }

        public override Transaccion realizarTransaccion(Transaccion aComputar)
        {

            Transaccion tran = null;

            try
            {

                if ( aComputar.tarjeta != null && aComputar.tarjeta.tipoTarjeta.Equals(TipoTarjeta.CREDITO))
                {
                    if (aComputar.tarjeta.descripcion != null && POIutils.isDebitCard( aComputar.tarjeta.descripcion.ToUpperInvariant()))
                    {
                        string msg = "Selecciono Credito y esta deslizando una tarjeta de debito: " + aComputar.tarjeta.descripcion;
                        MessageBox.Show( msg , "Error al realizar transaccion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        logger.Error(msg);
                        cancelarTransaccion();
                        return null;
                    }
                }
                else
                {
                    if (aComputar.tarjeta != null && aComputar.tarjeta.tipoTarjeta.Equals(TipoTarjeta.DEBITO))
                    {
                        if (aComputar.tarjeta.descripcion != null && !POIutils.isDebitCard(aComputar.tarjeta.descripcion.ToUpperInvariant()))
                        {
                            string msg = "Selecciono Debito y esta deslizando una tarjeta de credito: " + aComputar.tarjeta.descripcion;
                            MessageBox.Show(msg, "Error al realizar transaccion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            logger.Error(msg);
                            cancelarTransaccion();
                            return null;
                        }
                        else
                        {
                            // Selecciono Debito y la tarjeta es de Debito. Validamos la correcta seleccion
                            if (aComputar.tarjeta.debitoSeleccionada != null && aComputar.tarjeta.descripcion != null && !aComputar.tarjeta.debitoSeleccionada.ToUpperInvariant().Equals( aComputar.tarjeta.descripcion.ToUpperInvariant()))
                            {
                                string msg = "Selecciono " + aComputar.tarjeta.debitoSeleccionada + " y esta deslizando:" + aComputar.tarjeta.descripcion;
                                MessageBox.Show(msg, "Error al realizar transaccion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                logger.Error(msg);
                                cancelarTransaccion();
                                return null;
                            }

                        }
                    }
                }

            }
            catch (Exception)
            {
                // Si no pudo validar no hacer nada
            }

            try
            {
                
                if (!POIutils.isRegionalConfArgentina())
                {
                    try
                    {
                        cancelarTransaccion();
                    }
                    catch (Exception e2)
                    {
                        logger.Error("Error al cerrar la sesion: " + vn, e2);
                    }

                    return null;
                }

                switch (aComputar.tipoTrans)
                {
                    case TipoTransaccion.COMPRA:
                        tran =  realizarPago(aComputar);
                        break;
                    case TipoTransaccion.ANULACION:
                        tran = realizarVoideo(aComputar);
                        break;
                    case TipoTransaccion.DEVOLUCION:
                        tran = realizarDevolucion(aComputar);
                        break;
                    case TipoTransaccion.CIERRE:
                        tran = realizarCierreLote(aComputar);
                        break;
                    default: tran = null;break;
                }
            }
            catch (Exception e)
            {

                MessageBox.Show("Error general no especificado al realizar transaccion con VTOL", "Error al realizar transaccion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Error("Error general no especificado al realizar transaccion con VTOL: ", e);

                try
                {
                    cancelarTransaccion();
                }
                catch (Exception e2)
                {
                    logger.Error("Error al cerrar la sesion: " + vn, e2);
                }

                return null;
            }
            if (tran != null)
            {
                tran.estadoDescripcion = TipoEstadoTransaccion.PASANDO_INFO_INTERACT;
            }
            return tran;
        }

        private static VirtualVtolClient crearVirtualVtolClient()
        {
            VirtualVtolClient svc = new VirtualVtolClient();
            int port = 3000;
            svc.SetParameter(VtolParam.StoreId, Configuration.getInstance().sucursal);
            svc.SetParameter(VtolParam.HostIp, "10.250.190.2");
            svc.SetParameter(VtolParam.HostPort, port);
            svc.SetParameter(VtolParam.TimeOutConnectionHost, (Configuration.getInstance().timeOutConnectionHost == null )?"60000":Configuration.getInstance().timeOutConnectionHost);
            svc.SetParameter(VtolParam.ResponseTimeout, (Configuration.getInstance().timeOutLibraryResponse == null )?"40000": Configuration.getInstance().timeOutLibraryResponse);
            // Inicio: esto no deberia ir nunca activado
            svc.SetParameter(VtolParam.UseVtolClientFinder, false);
            // Fin: esto no deberia ir nunca activado
            //svc.SetParameter(VtolParam.VtolClientFinder, new DefaultVtolClientFinder());
            svc.SetParameter(VtolParam.UseEncryptedData, false);
            svc.SetParameter(VtolParam.Encrypter, "DESede");
            svc.SetParameter(VtolParam.EncrypterKey, "SynthesisSynthesisSynthe");

            logger.Info("Creado Cliente de VTOL, puerto: " + port  + " con params: timeOutConnectionHost: " + Configuration.getInstance().timeOutConnectionHost + "timeOutLibraryResponse: " + Configuration.getInstance().timeOutLibraryResponse);

            return svc;
        }


        private bool crearSesion()
        {
            return crearSesion(false);
        }

        private bool crearSesion(bool fromPendientes)
        {
            try
            {
                vn.CreateTransaction();
                vn.AddField(11, "createSession");
                vn.SendTransaction();
                //String responseCodeString = vtolNode.GetField(FieldId.ResponseCodeFieldId);
                //chequeo respuesta de libreria full: 1027 campo libResponseCode

                logger.Info("Respuesta al create session campo 1027: " + vn.GetField(1027));

                switch (vn.GetField(1027))
                {
                    
                    case "000":
                        logger.Info("Sesion creada con VTOL");
                        return true;
                    case "101":
                    case "702":
                    case "808":

                        if ( !fromPendientes) { 
                            //inicio - chequear campo 24 "lastTrxId" a ver que sucedio con la transaccion anterior
                            chequearTransaccionesPendientes();
                            //fin - chequear campo 24 "lastTrxId" a ver que sucedio con la transaccion anterior
                            return crearSesion(true);
                        }
                        else
                        {
                            string msg = "Se trato de crear sesion y el servidor VTOL respondio trx pendientes. TRXID: " + VTOLLastTrxId.recoverVTOLLastTrxId().ToString();
                            logger.Error( msg );
                            MessageBox.Show( msg + " . Llame al CIP y pase los siguientes datos: Sucursal:" + Configuration.getInstance().sucursal + " Caja: " + Configuration.getInstance().caja + " Indicando si se pudo resolver bien la última transacción. Cuando lo resuelvan su caja podrá volver a operar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            cancelarTransaccion();

                            return false;
                        }
                        
                    default:
                        throw new VTOLException("No se pudo crear sesión con el Servidor. Revise con Sistemas");
                }
            }
            catch (Exception e)
            {
                logger.Error("Error al crear sesion de VTOL", e);
                cancelarTransaccion();

                return false;
            }
        }


        private void chequearTransaccionesPendientes()
        {
            try
            {
                lastTrxId = VTOLLastTrxId.recoverVTOLLastTrxId().ToString();
                bool printstatus = VTOLLastTrxId.recoverVTOLLastTrxIdPrintStatus();
                if (printstatus)
                {
                    commitTrx(lastTrxId);
                    logger.Info("Commit de la trxId: " + lastTrxId + " printStatus: true");
                    
                }
                else
                {
                    vn.CreateTransaction();
                    vn.AddField(11, "closeSession");
                    vn.AddField(1008, "CANCEL");
                    vn.SendTransaction();
                    logger.Info("Se envió 11:closeSession y 1008:CANCEL por print Status: false. TrxId: " + lastTrxId);
                }
            }
            catch (Exception e)
            {

                logger.Error("Error al cancelar transaciones pendientes con VTOL", e);
                MessageBox.Show(e.Message);

                cancelarTransaccion();
            }
        }

        private void cancelForzado()
        {
            try { 
                vn.CreateTransaction();
                vn.AddField(11, "closeSession");
                vn.AddField(1008, "FORCED_CLOSE");
                vn.AddField(250, "00005");
                vn.SendTransaction();
            }
            catch ( Exception e)
            {
                logger.Error("Error al cancelar la transaccion: ", e);
            }
        }

        private void commitTrx(string trxId)
        {
            vn.CreateTransaction();
            vn.AddField(FieldId.TransactionTypeFieldId, "closeSession");
            vn.AddField(1008, "CLOSE"); // accion a realizar en el cierre de sesion: close confirma las autorizaciones realizadas
            vn.AddField(1009, "{" + trxId + "}");
            vn.SendTransactionWithoutResponse();

            logger.Info("Commit de las trx : " + trxId);

        }


        private void setearCamposPlanPagos( PlanPagos aUsar , Transaccion resultado )
        {

            if (aUsar != null)
            {
                // CÓDIGO DE COMERCIO
                if (resultado.comercio.codigoComercio == null || resultado.comercio.codigoComercio.Trim().Equals(""))
                    resultado.comercio.codigoComercio = aUsar.numeroComercio;

                // NRO DE TERMINAL
                DefinicionLote defLote = VTOLConfiguration.getInstance().getDefinicionLote(aUsar);
                if (defLote != null)
                {
                    if (resultado.nroTerminal == null || resultado.nroTerminal.Trim().Equals(""))
                        resultado.nroTerminal = defLote.numeroSerieTerminal;
                }

                // DESCRIPCION DE LA TARJETA
                Provider prov = VTOLConfiguration.getInstance().getProvider( aUsar.idTarjeta );
                if (prov != null)
                {
                    if (resultado.tarjeta.descripcion == null || resultado.tarjeta.descripcion.Trim().Equals(""))
                        resultado.tarjeta.descripcion = prov.nombre;
                }

                // TIPO DE HOST. ID LOTE
                if (resultado.tipoHost.Equals(TipoHost.DESCONOCIDO))
                    resultado.tipoHost = EnumUtils.getTipoHostFromIdLote(aUsar.idLote + "");
            }

        }

        public override string getGatewayName()
        {
            return "VTOL";
        }

        protected override Transaccion realizarPago(Transaccion paraPagar)
        {
            try
            {

                bool continuar = false;

                paraPagar.estadoDescripcion = TipoEstadoTransaccion.CREADA;
                //**********************************************************************************
                // 1. OBTENER DATOS DE LA TARJETA, POR IVR O POR FORMA DIRECTA
                //**********************************************************************************
                paraPagar.tarjeta = obtenerDatosTarjetaIvr( paraPagar);

                if (paraPagar.tarjeta == null)
                {
                    return null;
                }

                paraPagar.estadoDescripcion = TipoEstadoTransaccion.PREPARANDO_DATOS;

                //**********************************************************************************
                // 2. PREPARAR DATOS PARA EL PAGO
                //**********************************************************************************
                // ESTADO: Transaccion Creada
                vn.CreateTransaction();
                paraPagar.estadoDescripcion = TipoEstadoTransaccion.CREADA;

                logger.Info("Trx SALE creada. " + paraPagar.toLog());
                vn.AddField(FieldId.TransactionTypeFieldId, "Sale");
                vn.AddField(FieldId.ServerFieldId, "VTOL");
                vn.AddField(FieldId.MessageTypeFieldId, "DATA");
                vn.AddField(FieldId.DateTimeFieldId, DateTime.Now.ToString("yyyyMMddHHmmss"));
                vn.AddField(FieldId.InputModeFieldId, "Manual");

                vn.AddField(FieldId.CardNumberFieldId, paraPagar.tarjeta.numero );
                vn.AddField(FieldId.ExpiringDateFieldId, paraPagar.tarjeta.getVencimientoPrimeroAnios());
                vn.AddField(FieldId.CVCFieldId, paraPagar.tarjeta.cvc);

                vn.AddField(FieldId.CurrencyTypeFieldId, "$");
                vn.AddField(FieldId.PaymentsFieldId, paraPagar.cantCuotas.ToString());
                //carga de planes de pago segun configuracion de vtol server
                PlanPagos aUsar = VTOLConfiguration.getInstance().getPlanesDePagosSegunTarjeta(paraPagar.tarjeta.idTarjeta, paraPagar.cantCuotas);
                paraPagar.tarjeta.codPlan = (aUsar != null ? aUsar.plan.ToString() : "0");
                vn.AddField(FieldId.PlanFieldId, paraPagar.tarjeta.codPlan);
                vn.AddField(FieldId.AmountFieldId, paraPagar.importeTotal.ToString("#####0.00").Replace(",", ""));

                //fin carga de planes de pago segun configuracion de vtol server
                //carga de codigo de autorizacion telefonica si es offline
                if (paraPagar.modo == TipoModoTransaccion.OFFLINE)
                {
                    vn.AddField(22, paraPagar.numAutorizacion);
                    string tarjetaPara02 = aUsar != null ? aUsar.idTarjeta : "";
                    if ((string.Compare(tarjetaPara02, "AM", true) == 0) && (string.Compare(paraPagar.estado, "02", true) == 0))
                        vn.AddField(18, "1");//el campo 18 es solo para respuesta 02 y tarjeta amex
                }

                WebServiceInsertTransaction wsinsertar = new WebServiceInsertTransaction(paraPagar);
                paraPagar.trxReferenceId = WebResponseParser.parseXMLInsert(wsinsertar.getResponse());

                if (paraPagar.trxReferenceId < 0)
                {
                    throw new VTOLException("No se pudo insertar en la base de datos. Llamar a Sistemas ARSA");
                }
                else
                {
                    vn.AddField(201, paraPagar.trxReferenceId.ToString().ToBase36String());
                    logger.Info("Trx SALE ID : " + paraPagar.trxReferenceId.ToString().ToBase36String());
                }
                
                if ( paraPagar.tarjeta.providers != null && paraPagar.tarjeta.providers.Length > 1)
                {
                    vn.AddField(1102, paraPagar.tarjeta.providers[0].ToString());
                }

                //**********************************************************************************
                // 3. PROCESAR PAGO
                //**********************************************************************************
                paraPagar.estadoDescripcion = TipoEstadoTransaccion.PROCESANDO;
                vn.SendTransaction();

                //**********************************************************************************
                // 4. OBTENER LOS DATOS DEL PAGO
                //**********************************************************************************
                Transaccion resultado = null;
                string responseCodeString = vn.GetField(FieldId.ResponseCodeFieldId);

                if ( responseCodeString == null)
                {
                    string description = vn.GetField(1028);
                    MessageBox.Show( "Error con Pinpad,  Descripción: " + description , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cancelarTransaccion();
                    return null;
                }

                if (string.Compare(responseCodeString, "ISO8583", true) == 0)
                {

                    string isoResponseCodeString = vn.GetField(FieldId.ISOResponseCodeFieldId);

                    resultado = (Transaccion)paraPagar.Clone();
                    resultado.trxId = vn.GetField(24);//en realidad es lastTrxId de VTOL Server
                    lastTrxId = resultado.trxId;
                    resultado.pdv.sucursal = Configuration.getInstance().sucursal;
                    resultado.pdv.caja = Configuration.getInstance().caja;
                    resultado.moneda = "$";
                    resultado.numAutorizacion = vn.GetField(22);//codigo de autorizacion
                    resultado.numLote = Convert.ToInt32(vn.GetField(31));
                    resultado.numTicket = vn.GetField(32);
                    resultado.comercio.codigoComercio = vn.GetField(30);
                    resultado.codSoftAMEX = vn.GetField(82);
                    resultado.appNameAMEX = vn.GetField(1111);
                    resultado.tipoHost = EnumUtils.getTipoHostFromString(vn.GetField(34));
                    // INICIO DE CAMPOS AGREGADOS POR NECESIDAD DE ESPECIFICACIONES EN CUPON
                    resultado.nroTerminal = vn.GetField(29);
                    resultado.tipoIngreso = EnumUtils.getTipoIngresoTarjeta(vn.GetField(10));
                    resultado.trxIdVtolUnico = vn.GetField(166);

                    resultado.numCuenta = vn.GetField(75);
                    resultado.AID = vn.GetField(1110);
                    // SETEO DE TARJETA
                    resultado.tarjeta.descripcion = vn.GetField(33);
                    if (resultado.tarjeta.descripcion == null)
                    {
                        POIutils.updateTarjetaFromBin(paraPagar.tarjeta.primeros6(), resultado.tarjeta);
                    }

                    resultado.tarjeta.owner.nombre = vn.GetField(1112);
                    if (vn.GetField(57) != null)
                        resultado.tarjeta.tipoCuentaDebito = EnumUtils.getTipoCuentaFromString(vn.GetField(57));
                    // FIN DE CAMPOS AGREGADOS POR NECESIDAD DE ESPECIFICACIONES EN CUPON
                    if (vn.GetField(23) != null)
                    {
                        resultado.modo = vn.GetField(23).Equals("onLine", StringComparison.InvariantCultureIgnoreCase) ? TipoModoTransaccion.ONLINE : (vn.GetField(23).Equals("offLine", StringComparison.InvariantCultureIgnoreCase) ? TipoModoTransaccion.OFFLINE : TipoModoTransaccion.OFFHOST);
                    }
                    else
                        resultado.modo = TipoModoTransaccion.ONLINE;

                    string sFecha = vn.GetField(25);
                    DateTime auxFecha = DateTime.ParseExact(sFecha, "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture);
                    resultado.fecha = auxFecha;
                    resultado.respuestaHost = vn.GetField(27) + " - " + vn.GetField(28);

                    setearCamposPlanPagos(aUsar, resultado);

                    if (HostCodesVTOL.checkError(isoResponseCodeString))
                    {
                        paraPagar.estado = isoResponseCodeString;
                        paraPagar.tipoIngreso = resultado.tipoIngreso;
                        resultado.estado = isoResponseCodeString;

                        continuar = true;

                        WebServiceUpdateTransaction wsUpdate = new WebServiceUpdateTransaction(resultado);
                        bool isUpdateDone = WebResponseParser.parseXMLDatosConfirmado(wsUpdate.getResponse());

                        if (!isUpdateDone)
                        {
                            string msg = "Error insertando registro de transaccion con VTOL con numero unico: " + resultado.trxId.ToString();
                            throw new VTOLException(msg);
                        }

                        logger.Info("Trx SALE ID : " + paraPagar.trxReferenceId.ToString().ToBase36String() + " grabada con exito");

                        if ( checkFraud ( resultado ))
                        {
                            AnulationFile.grabarTransaccion(resultado);
                            commitTrx(lastTrxId);
                        }else
                        {
                            WebServiceReversarTransaccion wsReversar = new WebServiceReversarTransaccion(resultado.trxReferenceId);
                            wsReversar.getResponse();
                            cancelarTransaccion();
                        }
                        
                        return resultado;
                    }

                }
                if (!continuar)
                {
                    cancelarTransaccion();
                }
                return resultado;
            }
            catch ( VTOLException e )
            {

                string errorString = vn.GetField(1028);
                if (errorString == null)
                {
                    errorString = "indefinido";
                }

                string msg = "Error por exception en Pago, Mensaje: " + errorString + ". Extendido: " + e.getMsg();
                logger.Error( msg );
                MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                if (lastTrxId != null && ExtensionMethods.IsNumeric( lastTrxId)) { 
                    VTOLLastTrxId.writeFileVTOLLastTrxId(Convert.ToInt32(lastTrxId), false);
                }


                cancelarTransaccion();

                return null;
            }
            

        }

        protected override Transaccion manejarAutorizacionTelefonica(Transaccion aAutorizarTelefonicamente)
        {
            FrmAutorizacionTelefonica fAut;
            while (aAutorizarTelefonicamente.estado == HostCodesVTOL.HOST_01_PEDIR_AUTORIZACION_TELEFONICA || aAutorizarTelefonicamente.estado == HostCodesVTOL.HOST_02_PEDIR_AUTORIZACION ||
                aAutorizarTelefonicamente.estado == HostCodesVTOL.HOST_76_LLAMAR_AL_EMISOR || aAutorizarTelefonicamente.estado == HostCodesVTOL.HOST_91_EMISOR_FUERA_LINEA)
            {

                if ( aAutorizarTelefonicamente.numAutorizacion == null || aAutorizarTelefonicamente.estado == HostCodesVTOL.HOST_91_EMISOR_FUERA_LINEA)
                {
                    string msg = "El emisor esta fuera de línea. Puede aprobar vía telefonica con VTOL. Comuniquese con el CIP para verificar conectividad";
                    logger.Error(msg);
                    MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                fAut = new FrmAutorizacionTelefonica(aAutorizarTelefonicamente);
                fAut.ShowDialog();
                if (fAut.tran == null)
                {
                    return null;
                }
                aAutorizarTelefonicamente = fAut.tran;
            }
            return aAutorizarTelefonicamente;
        }


        protected override Transaccion realizarDevolucion(Transaccion aDevolver)
        {

            try {

                Transaccion resultado = null;
                bool continuar = false;
                int idAAnular = aDevolver.trxReferenceId != 0 ? aDevolver.trxReferenceId : aDevolver.trxReferenceIdOriginal;
                if (crearSesion())
                {
                    aDevolver.tarjeta = obtenerDatosTarjetaIvr( aDevolver);
                    if ( aDevolver.tarjeta != null )
                    {
                        vn.CreateTransaction();
                        // ESTADO: Transaccion Creada
                        aDevolver.estadoDescripcion = TipoEstadoTransaccion.CREADA;
                        logger.Info("Trx DEV creada. " + aDevolver.toLog());

                        vn.AddField(11, "Refund");
                        vn.AddField(12, aDevolver.importeTotal.ToString("#####0.00").Replace(",", ""));
                        vn.AddField(13, "$");
                        vn.AddField(14, aDevolver.cantCuotas.ToString());
                        //carga de planes de pago segun configuracion de vtol server
                        PlanPagos aUsar = VTOLConfiguration.getInstance().getPlanesDePagosSegunTarjeta(aDevolver.tarjeta.idTarjeta, aDevolver.cantCuotas);
                        aDevolver.tarjeta.codPlan = (aUsar != null ? aUsar.plan.ToString() : "0");
                        vn.AddField(15, aDevolver.tarjeta.codPlan);

                        //fin carga de planes de pago segun configuracion de vtol server
                        //carga de codigo de autorizacion telefonica si es offline

                        if (aDevolver.modo == TipoModoTransaccion.OFFLINE)
                        {
                            vn.AddField(22, aDevolver.numAutorizacion);
                            string tarjetaPara02 = aUsar != null ? aUsar.idTarjeta : "";
                            if ((string.Compare(tarjetaPara02, "AM", true) == 0) && (string.Compare(aDevolver.estado, "02", true) == 0))
                                vn.AddField(18, "1");//el campo 18 es solo para respuesta 02 y tarjeta amex
                        }

                        //fin carga de planes de pago segun configuracion de vtol server
                        //campos especificos para refund
                        vn.AddField(16, aDevolver.fechaOriginal.ToString("yyyyMMdd"));
                        vn.AddField(17, aDevolver.ticketOriginal);
                        //fin campos especificos para refund

                        logger.Info("Transaccion Creada: " +  aDevolver.toLog());

                        WebServiceInsertTransaction wsinsertar = new WebServiceInsertTransaction(aDevolver);
                        aDevolver.trxReferenceId = WebResponseParser.parseXMLInsert(wsinsertar.getResponse());

                        if (aDevolver.trxReferenceId < 0)
                        {
                            throw new VTOLException("No se pudo insertar en la base de datos. Llamar a Sistemas ARSA");
                        }
                        else
                        {
                            vn.AddField(201, aDevolver.trxReferenceId.ToString().ToBase36String());
                        }

                        // ESTADO: Transaccion Creada
                        aDevolver.estadoDescripcion = TipoEstadoTransaccion.REVISE_PINPAD;
                        vn.SendTransaction();
                        aDevolver.estadoDescripcion = TipoEstadoTransaccion.PROCESANDO;

                        string responseCodeString = vn.GetField(FieldId.ResponseCodeFieldId);
                        string codigo = vn.GetField(1027);

                        if (responseCodeString == null || !codigo.Equals("000"))
                        {
                            string description = vn.GetField(1028);
                            MessageBox.Show("Error con Pinpad, código: " + codigo + " Descripción: " + description, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            cancelarTransaccion();
                            return null;
                        }


                        if (string.Compare(responseCodeString, "ISO8583", true) == 0)
                        {
                            string isoResponseCodeString = vn.GetField(FieldId.ISOResponseCodeFieldId);

                            resultado = (Transaccion)aDevolver.Clone(); 
                            resultado.trxId = vn.GetField(24);//en realidad es lastTrxId de VTOL Server
                            lastTrxId = resultado.trxId;
                            resultado.pdv.sucursal = Configuration.getInstance().sucursal;
                            resultado.pdv.caja = Configuration.getInstance().caja;
                            resultado.moneda = "$";
                            resultado.numAutorizacion = vn.GetField(22);//codigo de autorizacion
                            resultado.numLote = Convert.ToInt32(vn.GetField(31));
                            resultado.numTicket = vn.GetField(32);
                            resultado.tarjeta.descripcion =vn.GetField(33);
                            if (resultado.tarjeta.descripcion == null)
                            {
                                POIutils.updateTarjetaFromBin(aDevolver.tarjeta.primeros6(), resultado.tarjeta);
                            }
                            resultado.comercio.codigoComercio = vn.GetField(30);
                            resultado.codSoftAMEX = vn.GetField(82);
                            resultado.appNameAMEX = vn.GetField(1111);
                            resultado.tipoHost = EnumUtils.getTipoHostFromString(vn.GetField(34));
                            // INICIO DE CAMPOS AGREGADOS POR NECESIDAD DE ESPECIFICACIONES EN CUPON
                            resultado.nroTerminal = vn.GetField(29);
                            resultado.tipoIngreso = EnumUtils.getTipoIngresoTarjeta(vn.GetField(10));
                            resultado.tarjeta.owner.nombre = vn.GetField(1112);
                            resultado.AID = vn.GetField(1110);
                            resultado.numCuenta = vn.GetField(75);
                            resultado.trxIdVtolUnico = vn.GetField(166);

                            if (vn.GetField(57) != null)
                                resultado.tarjeta.tipoCuentaDebito = EnumUtils.getTipoCuentaFromString(vn.GetField(57));
                            // FIN DE CAMPOS AGREGADOS POR NECESIDAD DE ESPECIFICACIONES EN CUPON
                            if (vn.GetField(23) != null)
                                resultado.modo = vn.GetField(23).Equals("onLine", StringComparison.InvariantCultureIgnoreCase) ? TipoModoTransaccion.ONLINE : (vn.GetField(23).Equals("offLine", StringComparison.InvariantCultureIgnoreCase) ? TipoModoTransaccion.OFFLINE : TipoModoTransaccion.OFFHOST);
                            else
                                resultado.modo = TipoModoTransaccion.ONLINE;

                            string sFecha = vn.GetField(25);
                            DateTime auxFecha = DateTime.ParseExact(sFecha, "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture);
                            resultado.fecha = auxFecha;
                            resultado.respuestaHost = vn.GetField(27) + " - " + vn.GetField(28);

                            setearCamposPlanPagos(aUsar, resultado);

                            if (HostCodesVTOL.checkError(isoResponseCodeString))
                            {
                                aDevolver.estado = isoResponseCodeString;
                                resultado.estado = isoResponseCodeString;
                                aDevolver.tipoIngreso = resultado.tipoIngreso;

                                if (!HostCodesVTOL.checkAutorizacionTelefonica(isoResponseCodeString))
                                {
                                    //context = vn.GetField()
                                    continuar = true;

                                    WebServiceUpdateTransaction wsUpdate = new WebServiceUpdateTransaction(resultado);
                                    bool isUpdateDone = WebResponseParser.parseXMLDatosConfirmado(wsUpdate.getResponse());

                                    if (!isUpdateDone)
                                    {
                                        string msg = "Error insertando registro de transaccion con VTOL con numero unico: " + resultado.trxId.ToString();
                                        logger.Error(msg);
                                        throw new VTOLException(msg);
                                    }

                                    if (imprimir(resultado))
                                    {
                                        VTOLLastTrxId.writeFileVTOLLastTrxId(Convert.ToInt32(lastTrxId), true);
                                        commitTrx(lastTrxId);
                                        logger.Info("Transaccion Confirmada: " + resultado.toLog());
                                        // ESTADO: Transaccion Confirmada
                                        aDevolver.estadoDescripcion = TipoEstadoTransaccion.CONFIRMADA;
                                    }
                                    else
                                    {

                                        WebServiceReversarTransaccion wsReversar = new WebServiceReversarTransaccion(resultado.trxReferenceId);
                                        wsReversar.getResponse();

                                        resultado = null;
                                        VTOLLastTrxId.writeFileVTOLLastTrxId(Convert.ToInt32(lastTrxId), false);
                                        string msg = "Error imprimiendo transaccion con numero unico: " + resultado.trxId.ToString();
                                        logger.Error(msg);

                                        // ESTADO: Transaccion Reversada
                                        aDevolver.estadoDescripcion = TipoEstadoTransaccion.REVERSADA;

                                        throw new VTOLException("Error al imprimir cupon de devolución. IMPORTANTE: Devolución cancelada.");
                                    }
                                }
                                else
                                {
                                    cancelarTransaccion();
                                    if (imprimir(resultado))
                                    {
                                        resultado = manejarAutorizacionTelefonica(aDevolver);
                                        if (resultado != null)
                                            continuar = true;
                                    }
                                    else
                                    {
                                        WebServiceReversarTransaccion wsReversar = new WebServiceReversarTransaccion(resultado.trxReferenceId);
                                        wsReversar.getResponse();

                                        string msg = "Error imprimiendo transaccion con numero unico: " + resultado.trxId.ToString();
                                        logger.Error(msg);
                                        throw new VTOLException(msg);
                                    }
                                }
                            }
                            else //ELSE de CHECK ERROR
                            {
                                if (!isoResponseCodeString.Equals("99"))
                                {
                                    imprimir(resultado);
                                }
                                resultado = null;

                                // ESTADO: Transaccion Reversada
                                aDevolver.estadoDescripcion = TipoEstadoTransaccion.FALLIDA;
                            }
                        }
                        else
                        {
                            throw new VTOLException("Error en el primer tramo de VTOL. Comuniquese con Sistemas.");
                        }
                    }
                    else
                    {
                        // No pudo leer la tarjeta
                        cancelarTransaccion();
                        return null;
                    }
                }
                if (!continuar)
                {
                    cancelarTransaccion();
                }
                return resultado;
            }
            catch (VTOLException e)
            {

                string errorString = vn.GetField(1028);
                if (errorString == null || errorString == "Ok")
                {
                    errorString = "indefinido";
                }

                cancelarTransaccion();

                string msg = "Error por exception en Devolución, Mensaje: " + errorString + ". Extendido: " + e.getMsg();
                logger.Error(msg + "detalle: " + e);

                MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                if (lastTrxId != null && ExtensionMethods.IsNumeric(lastTrxId))
                {
                    VTOLLastTrxId.writeFileVTOLLastTrxId(Convert.ToInt32(lastTrxId), false);
                }

                return null;
            }
        }

        protected override Transaccion realizarVoideo(Transaccion aVoidear)
        {

            try
            {

                //**********************************************************************************
                // 1. OBTENER DATOS DE LA TARJETA, POR IVR O POR FORMA DIRECTA
                //**********************************************************************************
                aVoidear.tarjeta = obtenerDatosTarjetaIvr( aVoidear);

                if (aVoidear.tarjeta == null)
                {
                    return null;
                }

                aVoidear.estadoDescripcion = TipoEstadoTransaccion.PREPARANDO_DATOS;


                string voidType = "VoidSale";
                if (aVoidear.transaccionOriginal != null && aVoidear.transaccionOriginal.tipoTrans.Equals(TipoTransaccion.DEVOLUCION))
                {
                    voidType = "VoidRefund";
                }

                Transaccion resultado = null;
                bool continuar = false;
                int idAAnular = aVoidear.trxReferenceId != 0 ? aVoidear.trxReferenceId : aVoidear.trxReferenceIdOriginal;
                if (crearSesion())
                {
                    if ( aVoidear.tarjeta != null )
                    {
                        //**********************************************************************************
                        // 2. PREPARAR DATOS PARA EL PAGO
                        //**********************************************************************************
                        // ESTADO: Transaccion Creada
                        vn.CreateTransaction();

                        logger.Info("Trx VOID " + voidType + " creada. " + aVoidear.toLog());

                        aVoidear.estadoDescripcion = TipoEstadoTransaccion.CREADA;

                        vn.AddField(FieldId.ServerFieldId, "VTOL");
                        vn.AddField(FieldId.MessageTypeFieldId, "DATA");
                        vn.AddField(FieldId.DateTimeFieldId, DateTime.Now.ToString("yyyyMMddHHmmss"));
                        vn.AddField(FieldId.InputModeFieldId, "Manual");

                        vn.AddField(FieldId.TransactionTypeFieldId, voidType);
                        vn.AddField(FieldId.AmountFieldId, aVoidear.importeTotal.ToString("#####0.00").Replace(",", ""));
                        vn.AddField(FieldId.CurrencyTypeFieldId, "$");
                        vn.AddField(FieldId.PaymentsFieldId, aVoidear.cantCuotas.ToString());
                        //carga de planes de pago segun configuracion de vtol server
                        PlanPagos aUsar = VTOLConfiguration.getInstance().getPlanesDePagosSegunTarjeta(aVoidear.tarjeta.idTarjeta, aVoidear.cantCuotas);
                        aVoidear.tarjeta.codPlan = (aUsar != null ? aUsar.plan.ToString() : "0");
                        vn.AddField(FieldId.PlanFieldId, aVoidear.tarjeta.codPlan);
                        //fin carga de planes de pago segun configuracion de vtol server
                        vn.AddField(17, aVoidear.numTicket);
                        //fin carga de planes de pago segun configuracion de vtol server
                        //carga de codigo de autorizacion telefonica si es offline
                        if (aVoidear.modo == TipoModoTransaccion.OFFLINE)
                        {
                            vn.AddField(22, aVoidear.numAutorizacion);
                            string tarjetaPara02 = aUsar != null ? aUsar.idTarjeta : "";
                            if ((string.Compare(tarjetaPara02, "AM", true) == 0) && (string.Compare(aVoidear.estado, "02", true) == 0))
                                vn.AddField(18, "1");//el campo 18 es solo para respuesta 02 y tarjeta amex
                        }

                        logger.Info("Transaccion creada: " + aVoidear.toLog());

                        WebServiceInsertTransaction wsinsertar = new WebServiceInsertTransaction(aVoidear);
                        aVoidear.trxReferenceId = WebResponseParser.parseXMLInsert(wsinsertar.getResponse());

                        if (aVoidear.trxReferenceId < 0)
                        {
                            throw new VTOLException("No se pudo insertar en la base de datos. Llamar a Sistemas ARSA");
                        }
                        else
                        {
                            vn.AddField(201, aVoidear.trxReferenceId.ToString().ToBase36String());
                            logger.Info("Transaccion ID: " + aVoidear.trxReferenceId.ToString().ToBase36String());
                        }

                        aVoidear.estadoDescripcion = TipoEstadoTransaccion.PROCESANDO;
                        vn.SendTransaction();
                        aVoidear.estadoDescripcion = TipoEstadoTransaccion.OBTENIENDO_DATOS;

                        string responseCodeString = vn.GetField(FieldId.ResponseCodeFieldId);

                        if (responseCodeString == null )
                        {
                            string description = vn.GetField(1028);
                            MessageBox.Show("Error con Pinpad, código: " + "" + " Descripción: " + description, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            cancelarTransaccion();
                            return null;
                        }

                        if (string.Compare(responseCodeString, "ISO8583", true) == 0)
                        {
                            string isoResponseCodeString = vn.GetField(FieldId.ISOResponseCodeFieldId);

                            resultado = (Transaccion) aVoidear.Clone();
                            resultado.trxId = vn.GetField(24);//en realidad es lastTrxId de VTOL Server
                            lastTrxId = resultado.trxId;
                            resultado.pdv.sucursal = Configuration.getInstance().sucursal;
                            resultado.pdv.caja = Configuration.getInstance().caja;
                            resultado.moneda = "$";
                            resultado.numAutorizacion = vn.GetField(22);//codigo de autorizacion
                            resultado.numLote = Convert.ToInt32(vn.GetField(31));
                            resultado.numTicket = vn.GetField(32);
                            resultado.tarjeta.descripcion = vn.GetField(33);
                            if (resultado.tarjeta.descripcion == null)
                            {
                                POIutils.updateTarjetaFromBin(aVoidear.tarjeta.primeros6(), resultado.tarjeta);
                            }
                            resultado.comercio.codigoComercio = vn.GetField(30);
                            resultado.codSoftAMEX = vn.GetField(82);
                            resultado.appNameAMEX = vn.GetField(1111);
                            resultado.tipoHost = EnumUtils.getTipoHostFromString(vn.GetField(34));
                            // INICIO DE CAMPOS AGREGADOS POR NECESIDAD DE ESPECIFICACIONES EN CUPON
                            resultado.nroTerminal = vn.GetField(29);
                            resultado.tipoIngreso = EnumUtils.getTipoIngresoTarjeta(vn.GetField(10));
                            resultado.tarjeta.owner.nombre = vn.GetField(1112);
                            resultado.AID = vn.GetField(1110);
                            resultado.numCuenta = vn.GetField(75);
                            resultado.trxIdVtolUnico = vn.GetField(166);

                            if (vn.GetField(57) != null)
                                resultado.tarjeta.tipoCuentaDebito = EnumUtils.getTipoCuentaFromString(vn.GetField(57));
                            // FIN DE CAMPOS AGREGADOS POR NECESIDAD DE ESPECIFICACIONES EN CUPON
                            if (vn.GetField(23) != null)
                                resultado.modo = vn.GetField(23).Equals("onLine", StringComparison.InvariantCultureIgnoreCase) ? TipoModoTransaccion.ONLINE : (vn.GetField(23).Equals("offLine", StringComparison.InvariantCultureIgnoreCase) ? TipoModoTransaccion.OFFLINE : TipoModoTransaccion.OFFHOST);
                            else
                                resultado.modo = TipoModoTransaccion.ONLINE;

                            string sFecha = vn.GetField(25);
                            DateTime auxFecha = DateTime.ParseExact(sFecha, "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture);
                            resultado.fecha = auxFecha;
                            resultado.respuestaHost = vn.GetField(27) + " - " + vn.GetField(28);

                            setearCamposPlanPagos(aUsar, resultado);

                            if (HostCodesVTOL.checkError(isoResponseCodeString))
                            {
                                aVoidear.estado = isoResponseCodeString;
                                aVoidear.tipoIngreso = resultado.tipoIngreso;
                                resultado.estado = isoResponseCodeString;

                                continuar = true;

                                WebServiceUpdateTransaction wsUpdate = new WebServiceUpdateTransaction(resultado);
                                bool isUpdateDone = WebResponseParser.parseXMLDatosConfirmado(wsUpdate.getResponse());

                                if ( !isUpdateDone )
                                {
                                    string msg = "Error update registro de transaccion con VTOL con numero unico: " + resultado.trxReferenceId;
                                    throw new VTOLException(msg);
                                }

                                VTOLLastTrxId.writeFileVTOLLastTrxId(Convert.ToInt32(lastTrxId), true);

                                commitTrx(lastTrxId);

                                aVoidear.estadoDescripcion = TipoEstadoTransaccion.CONFIRMADA;

                                logger.Info("Transaccion Confirmada: " + resultado.trxReferenceId.ToString().ToBase36String() + " " + resultado.toLog());

                                WebServiceAnularTransaccion wsanular = new WebServiceAnularTransaccion(idAAnular);
                                if (!WebResponseParser.parseXMLAnularTransaccion(wsanular.getResponse()))
                                {
                                    string msg = "Error anulando registro de transaccion con id de transaccion: " + aVoidear.trxId.ToString();
                                    logger.Error(msg);
                                }

                                

                            }
                            else //ELSE de CHECK ERROR
                            {
                                resultado = null;
                                aVoidear.estadoDescripcion = TipoEstadoTransaccion.FALLIDA;
                            }
                        }
                        else
                        {
                            throw new VTOLException("Error en primer tramo de autorizacion con VTOL");
                        }
                    }
                    else
                    {
                        // No pudo leer la tarjeta
                        cancelarTransaccion();
                        return null;
                    }
                }
                if (!continuar)
                {
                    cancelarTransaccion();
                }
                return resultado;
            }
            catch (VTOLException e)
            {

                string errorString = vn.GetField(1028);
                if (errorString == null || errorString == "Ok")
                {
                    errorString = "indefinido";
                }

                string msg = "Error por exception en Anulación, Mensaje: " + errorString + ". Extendido: " + e.getMsg();
                logger.Error(msg + "detalle: " + e);

                MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                if (lastTrxId != null && ExtensionMethods.IsNumeric(lastTrxId))
                {
                    VTOLLastTrxId.writeFileVTOLLastTrxId(Convert.ToInt32(lastTrxId), false);
                }

                cancelarTransaccion();

                return null;
            }
        }


        protected override Transaccion realizarCierreLote(Transaccion aCerrar)
        {
            throw new NotImplementedException();
        }

        public override string getNombreTransaccion(Transaccion trans)
        {

            switch (trans.tipoTrans) {
                case TipoTransaccion.COMPRA:  { return "Sale"; }
                case TipoTransaccion.ANULACION: { return "VoidSale"; }
                case TipoTransaccion.DEVOLUCION: { return "Refund"; }
            }
            return null;
        }

        public bool getConfiguration(string VTOLFileConfig)
        {
            bool resultado = false;
            try
            {
                if (crearSesion())
                {
                    vn.CreateTransaction();
                    vn.AddField(11, "getConfiguration");
                    vn.SendTransaction();
                    string respuesta = vn.GetField(1027);
                    if (Convert.ToInt32(respuesta) == 0)
                    {
                        string confVersion = vn.GetField(137);
                        string confData = vn.GetField(138);
                        byte[] data = Convert.FromBase64String(confData);
                        string decodedString = Encoding.UTF8.GetString(data);
                        resultado = VTOLConfigurationFile.writeConfiguration(VTOLFileConfig, decodedString);
                        logger.Info("Se pudo obtener configuracion VTOL correctamente. Version: " + confVersion + " Fecha: " + confData);
                        resultado = true;
                    }else
                    {
                        throw new VTOLException("No se pudo obtener el archivo. Respuesto VTOL: " + respuesta);
                    }
                    vn.CreateTransaction();
                    vn.AddField(FieldId.TransactionTypeFieldId, "closeSession");
                    vn.AddField(1008, "CLOSE");
                    vn.SendTransactionWithoutResponse();
                }
            }
            catch (VTOLException e)
            {
                string msg = e.getMsg();
                MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Error("Error al obtener archivo de configuracion de VTOL", e);
                cancelarTransaccion();
            }
            catch (Exception e)
            {
                logger.Error("Error al obtener archivo de configuracion de VTOL", e);
                cancelarTransaccion();
            }
            
            return resultado;
        }
    }
}
