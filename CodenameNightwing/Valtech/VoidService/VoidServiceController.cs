using CodenameNightwing.BusinessLogic;
using CodenameNightwing.Config;
using CodenameNightwing.Valtech.AuditService;
using CodenameNightwing.Valtech.AuthService;
using CodenameNightwing.Valtech.VoidService.Request;
using CodenameNightwing.Valtech.VoidService.Response;
using CodenameNightwing.Varios;
using log4net;
using log4net.Repository.Hierarchy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace CodenameNightwing.Valtech.VoidService
{
    class VoidServiceController : BaseServiceController
    {
        public static readonly ILog logger = LogManager.GetLogger(typeof(VoidServiceController));

        public VoidServiceResponse voidEmision(VoidServiceRequest voidServiceRequest)
        {
            VoidServiceResponse response = AsyncHelper.RunSync(() => this.voidEmisionService(voidServiceRequest));
            return response;
        }


        private async Task<VoidServiceResponse> voidEmisionService(VoidServiceRequest voidServiceRequest)
        {

            VoidServiceResponse voidResponse = new VoidServiceResponse();
            HttpResponseMessage responseHttp = null;
            String content = null;


            //Transaccion para auditar:
            Transaccion tranAuditVoid = TransaccionBuilder.construirPago(0);
            tranAuditVoid.primaryPnr = voidServiceRequest.pnr;

            try
            {
                AuthServiceController authController = new AuthServiceController();
                string token = authController.generateToken();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                string uri = Configuration.getInstance().getValtechURI("reservation") + voidServiceRequest.pnr + "/vcr/void";

                string jsonRequest = new JavaScriptSerializer().Serialize(voidServiceRequest);

                logger.Info("WSVoid Uri: " + uri);
                logger.Info("WSVoid Request: " + jsonRequest);

                System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();
                timer.Start();

                responseHttp = await client.PutAsJsonAsync<VoidServiceRequest>(new Uri(uri), voidServiceRequest);
                content = await responseHttp.Content.ReadAsStringAsync();

                logger.Info("WSVoid Response : " + content);

                if (responseHttp.IsSuccessStatusCode)
                {
                    //voidResponse =  JsonConvert.DeserializeObject<PromoServiceResponse>(content);
                    voidResponse = await responseHttp.Content.ReadAsAsync<VoidServiceResponse>();
                }
                else
                {
                    foreach (string nroTkt in voidServiceRequest.vcr) {
                        Vcr vcr = new Vcr();
                        vcr.vcr = nroTkt;
                        vcr.success = false;
                        voidResponse.vcr.Add(vcr);
                    }
                    voidResponse.success = false;
                    voidResponse.errorResponseMsg = content;
                }

                timer.Stop();
                TimeSpan timeTaken = timer.Elapsed;

                AuditServiceController.postAudit(new AuditData(AuditVendorId.VALTECH, AuditOperationType.EMD, tranAuditVoid, responseHttp, timeTaken.Milliseconds, jsonRequest));


            }
            catch (Exception e)
            {
                logger.Error("PromoServiceController.checkPromoService", e);
                voidResponse.errorResponseMsg = "WSVoid ERROR  - Response: " +  content;
                AuditServiceController.postAudit(new AuditData(AuditVendorId.VALTECH, AuditOperationType.VOID, tranAuditVoid, "", e));
            }
            finally
            {
            }
            return voidResponse;
        }

    }
}
