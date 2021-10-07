using CodenameNightwing.BusinessLogic;
using CodenameNightwing.Config;
using CodenameNightwing.Valtech.AuditService;
using CodenameNightwing.Valtech.AuthService;
using CodenameNightwing.Varios;
using log4net;
using System;
using System.Diagnostics;
using System.Net;
using System.Windows.Forms;

namespace CodenameNightwing.IVR
{
    class IVRManager
    {
        public IVRManager()
        {
        }

        public static readonly ILog logger = LogManager.GetLogger(typeof(IVRManager));

        public bool isIVRInProcess { get; set; }

        TipoWCentrixStatus statusIVR { get; set; }


        //TODO Cancel TASK
        public void cortarIVR()
        {
            isIVRInProcess = false;
            statusIVR = TipoWCentrixStatus.CORTE_MANUAL;

        }

        /*
        public Tarjeta obtenerTarjetaIVR(IProgress<IVRResponses> progress, Transaccion tran, TipoIdiomaIVR idioma)
        {
            Tarjeta tar = new BusinessLogic.Tarjeta();

            try
            {
                logger.Info("Inicio obtenerTarjetaIVR");

                MitrolController mitrolController = new MitrolController();
                IVRResponses ivrResponses = null;

                //Limpia ivrRepository
                AsyncHelper.RunSync(() => mitrolController.cleanRepository());

                // 1. GENERATE RANDOM  password
                string password = AsyncHelper.RunSync(() => mitrolController.generatePassword());

                // 2. GET ID DE LLAMADA ACTUAL
                string idInteraccionLlamadaActual = AsyncHelper.RunSync(() => mitrolController.getIdInteraccion(null, "Talking"));
                logger.Info("idInteraccionLlamadaActual: " + idInteraccionLlamadaActual);


                // Si no se pudo obtener la llamada actual, no funciona la conexión con IPPAD MITROL
                if (idInteraccionLlamadaActual == null)
                {
                    logger.Info("MitrolException: No se pudo obtener el ID de llamada actual");
                    throw new MitrolException("No se pudo obtener el ID de llamada actual");
                }

                // 3. GET ID DE LLAMADA IVR
                string idInteraccionLlamadaIVR = AsyncHelper.RunSync(() => mitrolController.derivarIVRPagos(idInteraccionLlamadaActual, tran, false, idioma));
                logger.Info("idInteraccionLlamadaIVR: " + idInteraccionLlamadaIVR);

                // Si no se pudo obtener la llamada al IVR, no funciona la conexión con IPPAD MITROL
                if (idInteraccionLlamadaIVR == null)
                {
                    logger.Info("MitrolException: No se pudo obtener el ID de llamada del IVR");
                    throw new MitrolException("No se pudo obtener el ID de llamada del IVR");
                }

                // Tiempo de espera para la derivacion (1 seg)
                int tiempoDerivacion = int.Parse(Configuration.getInstance().tiempoDerivacion) * 1000;
                System.Threading.Thread.Sleep(tiempoDerivacion);

                // TIME OUT Finalización LOOP status EN_PROCESO (4 min x default)
                int tiempoOut = int.Parse(Configuration.getInstance().timeOutIVRPagos) * 1000 * 60;

                Stopwatch sw = new Stopwatch();
                sw.Start();

                //Inicializa variables
                isIVRInProcess = true;
                statusIVR = TipoMitrolStatus.DESCONOCIDO;

                //Espera hasta que status != "EN_PROCESO" o por timeout
                while (isIVRInProcess)

                {
                    // 3. CON QUIEN ESTA HABLANDO EL PASAJERO ?
                    // TIENE QUE ESTAR HABLANDO CON EL IVR DE PAGOS
                    //string statusCall = Configuration.getInstance().callStatus;
                    //logger.Debug("Llamada a : mitrolController.getIdInteraccion() con status : " + statusCall);
                    //idInteraccionLlamadaActual = AsyncHelper.RunSync(() => mitrolController.getIdInteraccion(idInteraccionLlamadaIVR , statusCall));
                    //idInteraccionLlamadaActual = AsyncHelper.RunSync(() => mitrolController.getIdInteraccion(idInteraccionLlamadaIVR, "Talking"));
                    //logger.Debug("Respuesta: idInteraccionLlamadaActual: " + idInteraccionLlamadaActual);

                    //if (Configuration.getInstance().checkActualCall && idInteraccionLlamadaActual ==null || !idInteraccionLlamadaActual.Equals(idInteraccionLlamadaIVR))

                    if (false)
                    {
                        isIVRInProcess = false;
                        statusIVR = TipoMitrolStatus.FALLA_GRAL;
                        logger.Info("idInteraccionLlamadaActual: " + idInteraccionLlamadaActual);
                        logger.Info("idInteraccionLlamadaIVR: " + idInteraccionLlamadaIVR);
                        logger.Info("idInteraccionLlamadaActual != idInteraccionLlamadaIVR: " + ((idInteraccionLlamadaActual != null) ? (!idInteraccionLlamadaActual.Equals(idInteraccionLlamadaIVR)).ToString() : " "));
                        logger.Info("status: " + statusIVR.ToString());
                    }
                    else
                    {
                        // Tiempo de espera para llamar al localhost
                        int tiempoLocalHost = int.Parse(Configuration.getInstance().tiempoLocalHost) * 1000;
                        System.Threading.Thread.Sleep(tiempoLocalHost); // 1 segundo

                        //Obtiene Respuestas IVR:
                        ivrResponses = new IVRResponses(AsyncHelper.RunSync(() => mitrolController.obtenerIVRResponses(password)));

                        ivrResponses.timeElapsed = sw.ElapsedMilliseconds;

                        logger.Debug("ivrResponses" + ivrResponses);

                        // Actualiza controles formulario FrmIVR 
                        progress.Report(ivrResponses);


                        if (ivrResponses.getStatus() != null && !statusIVR.Equals(TipoMitrolStatus.CORTE_MANUAL) && !statusIVR.Equals(TipoMitrolStatus.TIMEOUT))
                        {
                            //Obtiene Status IVR
                            statusIVR = EnumUtils.getMitrolStatus(ivrResponses.getStatus());

                        }

                        if (!statusIVR.Equals(TipoMitrolStatus.EN_PROCESO))
                        {
                            isIVRInProcess = false;
                        }

                    }

                    if (sw.ElapsedMilliseconds > tiempoOut)
                    {
                        statusIVR = TipoMitrolStatus.TIMEOUT;
                    }
                }


                sw.Stop();

                if (statusIVR.Equals(TipoMitrolStatus.FINALIZADO_OK))
                {
                    tar.numero = ivrResponses.getNumeroTarjeta();
                    tar.vencimiento = ivrResponses.getFechaExp();
                    tar.cvc = ivrResponses.getCVC();

                    TimeSpan timeTaken = sw.Elapsed;

                    //Audita IVR Finalizado OK 
                    AuditServiceController.postAudit(new AuditData(AuditVendorId.MITROL, AuditOperationType.IVR_GET_CREDITCARD, tran, timeTaken.Milliseconds, TipoMitrolStatus.FINALIZADO_OK.ToString(), HttpStatusCode.OK, "idInteraccionLlamadaIVR: " + idInteraccionLlamadaIVR));

                    //Limpia ivrRepository
                    AsyncHelper.RunSync(() => mitrolController.cleanRepository());
                }
                else
                {
                    if (statusIVR.Equals(TipoMitrolStatus.VOLVER_A_AGENTE))
                    {

                        logger.Info("MitrolException: Error IVR Status = VOLVER_A_AGENTE");
                        throw new MitrolException("Interrupción en el proceso de IVR de Pagos: " + statusIVR, TipoMensaje.WARNING);
                    }
                    else
                    {
                        if (statusIVR.Equals(TipoMitrolStatus.CORTE_MANUAL))
                        {
                            logger.Info("CORTE MANUAL");
                        }
                        //Limpia ivrRepository
                        AsyncHelper.RunSync(() => mitrolController.cleanRepository());
                        //Error IVR Status != FINALIZADO_OK
                        logger.Info("MitrolException: Error IVR Status != FINALIZADO_OK");
                        throw new MitrolException("Error en el proceso de IVR de Pagos: " + statusIVR);
                    }
                }
                logger.Info("Fin obtenerTarjetaIVR");


            }

            catch (MitrolException eMitrol)
            {
                string msg = eMitrol.getMsg();
                TipoMensaje tipoMsg = eMitrol.getTipoMsg();
                if (tipoMsg == TipoMensaje.ERROR)
                {
                    logger.Error(msg);

                    //Audita Error IVR
                    AuditServiceController.postAudit(new AuditData(AuditVendorId.MITROL, AuditOperationType.IVR_GET_CREDITCARD, tran, "Check status IVR", eMitrol));

                    MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    logger.Info(msg);
                    MessageBox.Show(msg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                return null;
            }
            catch (Exception e)
            {
                string msg = "Intente en forma manual ";

                //Audita Error IVR
                AuditServiceController.postAudit(new AuditData(AuditVendorId.MITROL, AuditOperationType.IVR_GET_CREDITCARD, tran, "Check status IVR", e));


                MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                logger.Error(e.StackTrace);
                return null;
            }
            return tar;

        }
        */

        public Tarjeta obtenerTarjetaIVR(IProgress<IVRResponses> progress, Transaccion tran, TipoIdiomaIVR idioma)
        {
            Tarjeta tar = new BusinessLogic.Tarjeta();

            try
            {
                logger.Info("Inicio obtenerTarjetaIVRWCX");

                WCentrixController wcentrixController = new WCentrixController();
                IVRResponses ivrResponses = null;

                string password = AsyncHelper.RunSync(() => wcentrixController.generatePassword());

                string idInteraccionLlamadaIVR = AsyncHelper.RunSync(() => wcentrixController.derivarIVRPagos(tran, false, idioma));
                logger.Info("idInteraccionLlamadaIVRWCX: " + idInteraccionLlamadaIVR);

                // Si no se pudo obtener la llamada al IVR, no funciona la conexión con WCENTRIX
                if (idInteraccionLlamadaIVR == null)
                {
                    logger.Info("WcxException: No se pudo obtener el ID de llamada del IVR");
                    throw new WCentrixException("No se pudo obtener el ID de llamada del IVR");
                }

                // Tiempo de espera para la derivacion (1 seg)
                int tiempoDerivacion = int.Parse(Configuration.getInstance().tiempoDerivacion) * 1000;
                System.Threading.Thread.Sleep(tiempoDerivacion);

                // TIME OUT Finalización LOOP status EN_PROCESO (4 min x default)
                int tiempoOut = int.Parse(Configuration.getInstance().timeOutIVRPagos) * 1000 * 60;

                Stopwatch sw = new Stopwatch();
                sw.Start();

                //Inicializa variables
                isIVRInProcess = true;
                statusIVR = TipoWCentrixStatus.DESCONOCIDO;

                //Espera hasta que status != "EN_PROCESO" o por timeout
                while (isIVRInProcess)

                {

                    // Tiempo de espera para llamar al localhost
                    int tiempoLocalHost = int.Parse(Configuration.getInstance().tiempoLocalHost) * 1000;
                    System.Threading.Thread.Sleep(tiempoLocalHost); // 1 segundo

                    //Obtiene Respuestas IVR:
                    ivrResponses = new IVRResponses(AsyncHelper.RunSync(() => wcentrixController.obtenerIVRResponses(password)));

                    ivrResponses.timeElapsed = sw.ElapsedMilliseconds;

                    logger.Debug("ivrResponses" + ivrResponses);

                    // Actualiza controles formulario FrmIVR 
                    progress.Report(ivrResponses);


                    if (ivrResponses.getStatus() != null && !statusIVR.Equals(TipoWCentrixStatus.CORTE_MANUAL) && !statusIVR.Equals(TipoWCentrixStatus.TIMEOUT))
                    {
                        //Obtiene Status IVR
                        statusIVR = EnumUtils.getMitrolStatus(ivrResponses.getStatus());

                    }

                    if (!statusIVR.Equals(TipoWCentrixStatus.EN_PROCESO))
                    {
                        isIVRInProcess = false;
                    }


                    if (sw.ElapsedMilliseconds > tiempoOut)
                    {
                        statusIVR = TipoWCentrixStatus.TIMEOUT;
                    }
                }


                sw.Stop();

                if (statusIVR.Equals(TipoWCentrixStatus.FINALIZADO_OK))
                {
                    tar.numero = ivrResponses.getNumeroTarjeta();
                    tar.vencimiento = ivrResponses.getFechaExp();
                    tar.cvc = ivrResponses.getCVC();

                    TimeSpan timeTaken = sw.Elapsed;

                    //Audita IVR Finalizado OK 
                    AuditServiceController.postAudit(new AuditData(AuditVendorId.MITROL, AuditOperationType.IVR_GET_CREDITCARD, tran, timeTaken.Milliseconds, TipoWCentrixStatus.FINALIZADO_OK.ToString(), HttpStatusCode.OK, "idInteraccionLlamadaIVR: " + idInteraccionLlamadaIVR));

                    //Finaliza transaccion en wcentrix
                    AsyncHelper.RunSync(() => wcentrixController.endTransaction());
                }
                else
                {
                    if (statusIVR.Equals(TipoWCentrixStatus.VOLVER_A_AGENTE))
                    {

                        logger.Info("WcxException: Error IVR Status = VOLVER_A_AGENTE");
                        throw new WCentrixException("Interrupción en el proceso de IVR de Pagos: " + statusIVR, TipoMensaje.WARNING);
                    }
                    else
                    {
                        if (statusIVR.Equals(TipoWCentrixStatus.CORTE_MANUAL))
                        {
                            logger.Info("CORTE MANUAL");
                        }
                        //Finaliza transaccion en wcentrix
                        AsyncHelper.RunSync(() => wcentrixController.endTransaction());

                        //Error IVR Status != FINALIZADO_OK
                        logger.Info("WcxException: Error IVR Status != FINALIZADO_OK");
                        throw new WCentrixException("Error en el proceso de IVR de Pagos: " + statusIVR);
                    }
                }
                logger.Info("Fin obtenerTarjetaIVR");


            }

            catch (WCentrixException eMitrol)
            {
                string msg = eMitrol.getMsg();
                TipoMensaje tipoMsg = eMitrol.getTipoMsg();
                if (tipoMsg == TipoMensaje.ERROR)
                {
                    logger.Error(msg);

                    //Audita Error IVR
                    AuditServiceController.postAudit(new AuditData(AuditVendorId.MITROL, AuditOperationType.IVR_GET_CREDITCARD, tran, "Check status IVR", eMitrol));
                    MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    logger.Info(msg);
                    MessageBox.Show(msg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                return null;
            }
            catch (Exception e)
            {
                string msg = "Intente en forma manual ";

                //Audita Error IVR
                AuditServiceController.postAudit(new AuditData(AuditVendorId.MITROL, AuditOperationType.IVR_GET_CREDITCARD, tran, "Check status IVR", e));
                MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                logger.Error(e.StackTrace);
                return null;
            }
            return tar;

        }

    }
}
