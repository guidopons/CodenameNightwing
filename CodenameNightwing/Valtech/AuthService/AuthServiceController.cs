using CodenameNightwing.BusinessLogic;
using CodenameNightwing.Config;
using CodenameNightwing.Valtech.AuditService;
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

namespace CodenameNightwing.Valtech.AuthService
{
    class AuthServiceController : BaseServiceController
    {

        public static readonly ILog logger = LogManager.GetLogger(typeof(AuthServiceController));

        public AuthServiceController():base() {
        }

        public string generateToken ( )
        {
            AuthResponse response = AsyncHelper.RunSync(() => this.generateTokenService());
            return response.access_token;
        }

        private async Task<AuthResponse> generateTokenService()
        {
            AuthResponse response = new AuthResponse();
            HttpResponseMessage responseHttp = null;

            try
            {
                string uri = Configuration.getInstance().getValtechURI("auth");
                AuthRequest authRequest = new AuthRequest();
                responseHttp = await client.PostAsJsonAsync<AuthRequest>(new Uri(uri), authRequest);

                if (responseHttp.IsSuccessStatusCode)
                {
                    response = await responseHttp.Content.ReadAsAsync<AuthResponse>();
                }


            }
            catch (Exception e)
            {
                logger.Error("Error AuthServiceController.generateToken", e);
            }
            finally
            {

            }
            return response;
        }
    }
}
