using CodenameNightwing.BusinessLogic;
using CodenameNightwing.Config;
using CodenameNightwing.Sabre;
using CodenameNightwing.Valtech.AuditService;
using CodenameNightwing.Valtech.AuthService;
using CodenameNightwing.Valtech.FraudService.Request;
using CodenameNightwing.Valtech.FraudService.Response;
using CodenameNightwing.Varios;
using log4net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace CodenameNightwing.Valtech.FraudService
{
    class FraudServiceController : BaseServiceController
    {

        public static readonly ILog logger = LogManager.GetLogger(typeof(FraudServiceController));


        public FraudServiceController():base()
        {
            
        }

        public FraudServiceResponse checkFraud( Transaccion tran)
        {
            tran.estadoDescripcion = TipoEstadoTransaccion.ANALIZANDO_FRAUDE;
            FraudServiceResponse response = AsyncHelper.RunSync(() => this.checkFraudService( tran ));
            return response;
        }

        private async Task<FraudServiceResponse> checkFraudService(Transaccion tran)
        {
            FraudServiceResponse response = new FraudServiceResponse();
            HttpResponseMessage responseHttp = null;

            try
            {
                FraudServiceRequest fraudServiceRequest = new FraudServiceRequest(tran);

                string uri = Configuration.getInstance().getValtechURI("fraude");

                AuthServiceController authController = new AuthServiceController();
                string token = authController.generateToken();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                string jsonRequest = new JavaScriptSerializer().Serialize(fraudServiceRequest);

                logger.Info("WSFraude Uri: " + uri);
                logger.Info("WSFraude request: " + jsonRequest);

                System.Diagnostics.Stopwatch timer = new Stopwatch();
                timer.Start();
                responseHttp = await client.PostAsJsonAsync<FraudServiceRequest>(new Uri(uri), fraudServiceRequest);

                string content = await responseHttp.Content.ReadAsStringAsync();
                logger.Info("WSFraude Response : " + content);

                if (responseHttp.IsSuccessStatusCode)
                {
                    response = await responseHttp.Content.ReadAsAsync<FraudServiceResponse>();
                }else
                {
                    response.fraudCheckInformation = new FraudServiceResponse.Fraudcheckinformation();
                    response.fraudCheckInformation.result = "CONNECTION ERROR";
                }

                timer.Stop();
                TimeSpan timeTaken = timer.Elapsed;

                AuditServiceController.postAudit(new AuditData(AuditVendorId.VALTECH, AuditOperationType.FRAUD, tran, responseHttp, timeTaken.Milliseconds, jsonRequest));

            }
            catch (Exception e)
            {
                logger.Error("Error a", e);
                response.fraudCheckInformation.result = "CONNECTION ERROR";
                AuditServiceController.postAudit(new AuditData(AuditVendorId.VALTECH, AuditOperationType.FRAUD, tran, "Check Fraud Service PDV", e ));
            }
            finally
            {

            }
            return response;
        }
    }
}
