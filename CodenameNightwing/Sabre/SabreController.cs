using CodenameNightwing.BusinessLogic;
using CodenameNightwing.Config;
using CodenameNightwing.Valtech.AuditService;
using CodenameNightwing.Valtech.AuthService;
using CodenameNightwing.WebServices;
using CodenameNightwing.WebServices.WSEspecificos.Transacciones;
using log4net;
using System;
using System.Net;


namespace CodenameNightwing.Sabre
{
    class SabreController
    {

        public static readonly ILog logger = LogManager.GetLogger(typeof(SabreController));

        public bool addTBMtoPNR(string pnr, string ccNumber, string ccVto, string ccCode)
        {
            logger.Info("Inicio SabreController addTBMtoPNR con PNR:" + pnr);
            string token = null;
            bool result = true;

            try
            {
                token = createSabreSession(pnr);

                getSabreReservation(token, pnr);

                sendSabreCommand(token, pnr, ccCode, ccNumber, ccVto);

                sendSabreEndTransaction(token, pnr);

            }
            catch (Exception e)
            {

                logger.Error("Error SabreController addTBMtoPNR", e);
                result = false;

            }
            finally
            {

                if (token != null)
                    closeSession(token, pnr);

            }

            logger.Info("Fin SabreController addTBMtoPNR con PNR:" + pnr);

            return result;

        }

        private String createSabreSession( string pnr )
        {
            String SINE = Configuration.getInstance().sineSabreWs.Split(';')[0];
            String PASSWD = Configuration.getInstance().sineSabreWs.Split(';')[1];
            //1. WS SABRE
            WebServiceSabreCreateSession ws = new WebServiceSabreCreateSession(SINE, PASSWD);
            string wsResponse = null;
            String token = null;

            try {

                logger.Info("Call WebServiceSabreCreateSession con SINE:" + SINE);
                
                wsResponse = ws.getResponse();

                if (!ws.getHttpStatusCode().Equals(HttpStatusCode.OK))
                    throw new Exception("Status Code from Sabre Session Create not OK :" + ws.getHttpStatusCode());

                token = WebResponseParser.parseXMLSessionCreateRS(wsResponse);

            }
            catch (Exception e) {
                logger.Error("Error createSabreSession", e);

                wsResponse = (wsResponse == null) ? "Message:\n" + e.Message + "\nException toString:\n" +  e.ToString() + "\nStack Trace:\n" + e.StackTrace : wsResponse;

                Transaccion tran = new TransaccionBuilder(TipoTransaccion.NADA, TipoAutorizador.NADA);
                tran.primaryPnr = pnr;

                string requestFormat = ws.getRequest();
                requestFormat = requestFormat.Replace( PASSWD , "XXXXXXXX");

                AuditServiceController.postAudit(new AuditData(AuditVendorId.SABRE, AuditOperationType.SABRE_CREATE_SESSION, tran, 0, requestFormat, ws.getHttpStatusCode(),  wsResponse));

                throw e;
            }
            return token;
        }

        private void getSabreReservation(string token, string pnr) {
            //2. WS SABRE
            logger.Info("Call WebServiceSabreGetReservation con PNR:" + pnr);

            WebServiceSabreGetReservation ws = new WebServiceSabreGetReservation(token, pnr);
            string wsResponse = null;
            try
            {
                ws = new WebServiceSabreGetReservation(token, pnr);

                wsResponse = ws.getResponse();

                if (!ws.getHttpStatusCode().Equals(HttpStatusCode.OK))
                    throw new Exception("Status Code from Sabre Session Create not OK :" + ws.getHttpStatusCode());

                WebResponseParser.parseXMLGetReservationRS(wsResponse, pnr);

            }
            catch (Exception e)
            {
                logger.Error("Error getSabreReservation", e);

                wsResponse = (wsResponse == null) ? "Message:\n" + e.Message + "\nException toString:\n" +  e.ToString() + "\nStack Trace:\n" + e.StackTrace : wsResponse;

                Transaccion tran = new TransaccionBuilder(TipoTransaccion.NADA, TipoAutorizador.NADA);
                tran.primaryPnr = pnr;

                AuditServiceController.postAudit(new AuditData(AuditVendorId.SABRE, AuditOperationType.SABRE_GET_RESERVATION, tran, 0, ws.getRequest(), ws.getHttpStatusCode(), wsResponse));

                throw e;
            }

        }

        private void sendSabreCommand(string token, string pnr, string ccCode, string ccNumber, string ccVto)  {
            String cmdSabre = "5-TBM*" + ccCode + ccNumber + "¥" + ccVto;
            String replaceTBMAudit = "5-TBM*" + ccCode + ccNumber.Substring(0, 6) + "XXXXXX" + ccNumber.Substring(ccNumber.Length - 4) + "¥" + "XX/XX";

            //3. WS SABRE
            WebServiceSabreCommand ws = new WebServiceSabreCommand(token, cmdSabre);
            logger.Debug("SabreController.sendSabreCommand" + replaceTBMAudit );
            string wsResponse = null;

            try
            {
                wsResponse = ws.getResponse();

                if (!ws.getHttpStatusCode().Equals(HttpStatusCode.OK))
                    throw new Exception("Status Code from Sabre Session Create not OK :" + ws.getHttpStatusCode());


                WebResponseParser.parseXMLSabreCommnandRS(wsResponse);
            }
            catch (Exception e)
            {
                logger.Error("Error sendSabreCommand" + replaceTBMAudit, e);

                wsResponse = (wsResponse == null) ? "Message:\n" + e.Message + "\nException toString:\n" +  e.ToString() + "\nStack Trace:\n" + e.StackTrace : wsResponse;

                Transaccion tran = new TransaccionBuilder(TipoTransaccion.NADA, TipoAutorizador.NADA);
                tran.primaryPnr = pnr;

                string requestFormat = ws.getRequest();
                
                requestFormat = requestFormat.Replace(cmdSabre, replaceTBMAudit);
                AuditServiceController.postAudit(new AuditData(AuditVendorId.SABRE, AuditOperationType.SABRE_COMMAND, tran, 0, requestFormat, ws.getHttpStatusCode(), wsResponse));

                throw e;
            }

        }

        private void sendSabreEndTransaction(string token, string pnr) {
            logger.Info("Call WebServiceSabreEndTransaction");
            //4. WS SABRE
            WebServiceSabreEndTransaction ws = new WebServiceSabreEndTransaction(token);
            string wsResponse = null;
            try
            {
                wsResponse = ws.getResponse();

                if (!ws.getHttpStatusCode().Equals(HttpStatusCode.OK))
                    throw new Exception("Status Code from Sabre Session Create not OK :" + ws.getHttpStatusCode());

                WebResponseParser.parseXMLSabreEndTransactionRS(wsResponse);

            }
            catch (Exception e)
            {
                logger.Error("Error WebServiceSabreCommand", e);

                wsResponse = (wsResponse == null) ? "Message:\n" + e.Message + "\nException toString:\n" +  e.ToString() + "\nStack Trace:\n" + e.StackTrace : wsResponse;

                Transaccion tran = new TransaccionBuilder(TipoTransaccion.NADA, TipoAutorizador.NADA);
                tran.primaryPnr = pnr;

                AuditServiceController.postAudit(new AuditData(AuditVendorId.SABRE, AuditOperationType.SABRE_END_TRANSACTION, tran, 0, ws.getRequest(), ws.getHttpStatusCode(), wsResponse));

                throw e;
            }

        }

        private void sendSabreAddRemark(string token, string pnr, string ccCode, string ccNumber, string ccVto) {
            //logger.Debug("Call WebServiceSabreAddRemark ccCode:" + ccCode + " ccNumber:" + ccNumber + " ccVto:" + ccVto);
            //5. WS SABRE
            WebServiceSabreAddRemark ws = new WebServiceSabreAddRemark(token, ccNumber, ccCode, ccVto);
            string wsResponse = null;
            try
            {
                wsResponse = ws.getResponse();

                if (!ws.getHttpStatusCode().Equals(HttpStatusCode.OK))
                    throw new Exception("Status Code from Sabre Session Create not OK :" + ws.getHttpStatusCode());


                WebResponseParser.parseXMLSabreAddRemarkRS(wsResponse);

            }
            catch (Exception e)
            {
                logger.Error("Error WebServiceSabreCommand", e);

                wsResponse = (wsResponse == null) ? "Message:\n" + e.Message + "\nException toString:\n" +  e.ToString() + "\nStack Trace:\n" + e.StackTrace : wsResponse;

                Transaccion tran = new TransaccionBuilder(TipoTransaccion.NADA, TipoAutorizador.NADA);
                tran.primaryPnr = pnr;

                AuditServiceController.postAudit(new AuditData(AuditVendorId.SABRE, AuditOperationType.SABRE_ADD_TBM, tran, 0, ws.getRequest(), ws.getHttpStatusCode(), wsResponse));

                throw e;
            }

        }

        private void closeSession(string token, string pnr)  {

            //6 WS SABRE
            WebServiceSabreCloseSession ws = new WebServiceSabreCloseSession(token);
            string wsResponse = null;

            try
            {
                wsResponse = ws.getResponse();

                if (!ws.getHttpStatusCode().Equals(HttpStatusCode.OK))
                    throw new Exception("Status Code from Sabre Session Create not OK :" + ws.getHttpStatusCode());

                WebResponseParser.parseXMLSabreCloseSessionRS(wsResponse);


            }
            catch (Exception e)
            {
                logger.Error("Error en SabreController closeSession. Exception: " + e);

                wsResponse = (wsResponse == null) ? "Message:\n" + e.Message + "\nException toString:\n" +  e.ToString() + "\nStack Trace:\n" + e.StackTrace : wsResponse;

                Transaccion tran = new TransaccionBuilder(TipoTransaccion.NADA, TipoAutorizador.NADA);
                tran.primaryPnr = pnr;

                AuditServiceController.postAudit(new AuditData(AuditVendorId.SABRE, AuditOperationType.SABRE_CLOSE_SESSION, tran, 0, ws.getRequest(), ws.getHttpStatusCode(), wsResponse));

                throw e;
            }

        }


    }
}
