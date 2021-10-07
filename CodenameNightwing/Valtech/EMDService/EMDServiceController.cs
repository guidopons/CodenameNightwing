using CodenameNightwing.Config;
using CodenameNightwing.Valtech.AuthService;
using CodenameNightwing.Varios;
using CodenameNightwing.Valtech.PromoService.Request;
using CodenameNightwing.Valtech.PromoService.Response;

using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using log4net;
using CodenameNightwing.BusinessLogic;
using Newtonsoft.Json;
using CodenameNightwing.Valtech.AuditService;

namespace CodenameNightwing.Valtech.PromoService
{
    class EMDServiceController : BaseServiceController
    {
        public static readonly ILog logger = LogManager.GetLogger(typeof(EMDServiceController));

        public EMDServiceResponse addEMD(EMDServiceRequest emdRequest)
        {
            EMDServiceResponse response = AsyncHelper.RunSync(() => this.addEMDService(emdRequest));
            return response;
        }

        private async Task<EMDServiceResponse> addEMDService(EMDServiceRequest emdRequest)
        {
            EMDServiceResponse emdResponse = new EMDServiceResponse();
            HttpResponseMessage responseHttp = null;
            String content = null;
            string jsonRequest = null;

            //Transaccion para auditar:
            Transaccion tranAuditEmd = TransaccionBuilder.construirPago(emdRequest.optionalFields.emdAmount);
            tranAuditEmd.primaryPnr = emdRequest.pnr;

            try
            {
                AuthServiceController authController = new AuthServiceController();
                string token = authController.generateToken();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                 
                string uri = Configuration.getInstance().getValtechURI("reservation") + emdRequest.pnr + "/emd";

                jsonRequest = new JavaScriptSerializer().Serialize(emdRequest);

                logger.Info("WSEMD Uri: " + uri);
                logger.Info("WSEMD Request: " + jsonRequest);

                System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();
                timer.Start();

                responseHttp = await client.PutAsJsonAsync<EMDServiceRequest>(new Uri(uri), emdRequest);
                content = await responseHttp.Content.ReadAsStringAsync();

                logger.Info("WSEMD Response: " + content);

                if (responseHttp.IsSuccessStatusCode)
                {
                    //emdResponse =  JsonConvert.DeserializeObject<EMDServiceResponse>(content);
                    emdResponse = await responseHttp.Content.ReadAsAsync<EMDServiceResponse>();
                }
                else
                {
                    emdResponse.errorResponseMsg = content;
                }

                timer.Stop();
                TimeSpan timeTaken = timer.Elapsed;

                AuditServiceController.postAudit(new AuditData(AuditVendorId.VALTECH, AuditOperationType.EMD, tranAuditEmd, responseHttp, timeTaken.Milliseconds, jsonRequest));
            }
            catch (Exception e)
            {
                logger.Error("EMDServiceController.addEMDService", e);
                emdResponse.errorResponseMsg = "WSEMD ERROR  - Response: " + content;
                AuditServiceController.postAudit(new AuditData(AuditVendorId.VALTECH, AuditOperationType.EMD, tranAuditEmd, jsonRequest, e));
            }
            finally
            {
            }
            return emdResponse;
        }
    }
}
