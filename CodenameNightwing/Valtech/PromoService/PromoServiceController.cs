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
using CodenameNightwing.Valtech.Exceptions;

namespace CodenameNightwing.Valtech.PromoService
{
    class PromoServiceController : BaseServiceController
    {
        public static readonly ILog logger = LogManager.GetLogger(typeof(PromoServiceController));

        public PromoServiceResponse checkPromo(Transaccion tran , bool withFilter)
        {
             PromoServiceResponse response = AsyncHelper.RunSync(() => this.checkPromoService(tran ,true));
            return response;
        }

        private async Task<PromoServiceResponse> checkPromoService( Transaccion tran , bool withFilter)
        {
            PromoServiceResponse promoResponse = new PromoServiceResponse();
            HttpResponseMessage responseHttp = null;
            String content = null;
            try
            {
                AuthServiceController authController = new AuthServiceController();
                string token = authController.generateToken();

                if (token == null) {
                    throw new ValtchException("ERROR PromoService.checkPromoService: No se pudo generar token Auth");
                }

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                PromoServiceRequest promoServiceRequest = new PromoServiceRequest(tran);

                string uri = Configuration.getInstance().getValtechURI("promos");

                if ( withFilter)
                {
                    uri = uri + "?paymentMethodCode=" + tran.tarjeta.codSabre + "&bin=" + tran.tarjeta.primeros6();
                }

                string jsonRequest = new JavaScriptSerializer().Serialize(promoServiceRequest);

                logger.Info("WSPromo uri: " + uri);
                logger.Info("WSPromo request: " + jsonRequest);

                responseHttp = await client.PostAsJsonAsync<PromoServiceRequest>(new Uri(uri), promoServiceRequest);
                content = await responseHttp.Content.ReadAsStringAsync();

                logger.Info("WSPromo response: " + content);

                if (responseHttp.IsSuccessStatusCode)
                {
                    //promoResponse =  JsonConvert.DeserializeObject<PromoServiceResponse>(content);
                    promoResponse = await responseHttp.Content.ReadAsAsync<PromoServiceResponse>();
                }
                else
                {
                    promoResponse.errorResponseMsg = content;
                }

            }
            catch (ValtchException e)
            {
                logger.Error("PromoServiceController.checkPromoService", e);
                promoResponse.errorResponseMsg = e.getMsg();
                throw e;
            }

            catch (Exception e)
            {
                logger.Error("PromoServiceController.checkPromoService", e);
                promoResponse.errorResponseMsg = "ERROR PromoServiceController.checkPromoService - Response: " + content;
                AuditServiceController.postAudit(new AuditData(AuditVendorId.VALTECH, AuditOperationType.PROMO, tran,"", e));
                throw new ValtchException("ERROR PromoService.checkPromoService " + e.Message);
            }
            finally
            {
            }
            return promoResponse;
        }
    }
}
