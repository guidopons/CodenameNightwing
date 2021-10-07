using CodenameNightwing.BusinessLogic;
using CodenameNightwing.Config;
using CodenameNightwing.Valtech.AuditService;
using CodenameNightwing.Valtech.AuditService.Request;
using CodenameNightwing.Valtech.AuditService.Response;
using CodenameNightwing.Valtech.AuthService.Request;
using CodenameNightwing.Valtech.AuthService.Response;
using CodenameNightwing.Valtech.FraudService.Request;
using CodenameNightwing.Valtech.FraudService.Response;
using CodenameNightwing.Varios;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace CodenameNightwing.Valtech.AuthService
{
    class AuditServiceController : BaseServiceController
    {

        public static readonly ILog logger = LogManager.GetLogger(typeof(AuditServiceController));

        public AuditServiceController() : base() {
        }

        public static AuditServiceResponse postAudit (  AuditData auditObj  )
        {
            AuditServiceResponse response = AsyncHelper.RunSync(() => postAuditService(auditObj));
            return response;
        }

        private static async Task<AuditServiceResponse> postAuditService(AuditData auditObj)
        {
            AuditServiceResponse response = new AuditServiceResponse();
            HttpResponseMessage responseHttp = null;
            clientStatic = new HttpClient();

            try
            {
                AuditServiceRequest auditRequest = new AuditServiceRequest( auditObj );

                string uri = Configuration.getInstance().getValtechURI("audit");

                AuthServiceController authController = new AuthServiceController();
                string token = authController.generateToken();
                clientStatic.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                string jsonRequest = new JavaScriptSerializer().Serialize(auditRequest);

                logger.Info("WSAudit Uri: " + uri);
                logger.Info("WSAudit request: " + jsonRequest);

                responseHttp = await clientStatic.PostAsJsonAsync<AuditServiceRequest>(new Uri(uri), auditRequest);

                string content = await responseHttp.Content.ReadAsStringAsync();
                logger.Info("WSAudit response : " + content);

                if (responseHttp.IsSuccessStatusCode)
                {
                    response = await responseHttp.Content.ReadAsAsync<AuditServiceResponse>();
                }

            }
            catch (Exception e)
            {
                logger.Error("Error en Audit Service : ", e);
            }
            finally
            {

            }
            return response;
        }
    }
}
