using CodenameNightwing.BusinessLogic;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using log4net;
using CodenameNightwing.Crypto;
using CodenameNightwing.FileManager;
using CodenameNightwing.IVR.Models;

namespace CodenameNightwing.IVR
{
    class WCentrixController
    {
        public static readonly ILog logger = LogManager.GetLogger(typeof(WCentrixController));
        private HttpClient client = new HttpClient();
        private string WCentrixBaseURL = "https://aa.wcx.cloud/";
        //private string WCentrixBaseURL = "http://localhost:7870/"; //"https://dev-3.wcxpro.com/";
        public static string userId = "AR" + VBrequestReader.getPropiedad("interact.user").ToUpper().Replace("AR", "").PadLeft(6, '0');

        public WCentrixController()
        {
            client.BaseAddress = new Uri(WCentrixBaseURL);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public async Task<string> derivarIVRPagos(Transaccion tran, bool token, TipoIdiomaIVR languageId)
        {
            string idInteraccion = null;
            HttpResponseMessage response = null;

            try
            {
                // Crear MapaContexto
                MapaContextoSecureTrx map = new MapaContextoSecureTrx("", tran, token, (int)languageId);

                string mapJson = map.toJson();
                string mapHttp = map.toUriHttp();

                response = await client.GetAsync("/ArController.svc/transfer?userId=" + userId + "&" + map.toUriHttp());

                logger.Info("response" + response.StatusCode); 
                if (response.IsSuccessStatusCode)
                {
                    idInteraccion = await response.Content.ReadAsAsync<string>();
                }

            }
            catch (Exception e)
            {
                logger.Error("WCentrixController.derivarIVRPagos(): Error en comunicacion con Servicio derivarIVRPagos", e);
                Console.WriteLine(e.StackTrace);
                return null;
            }

            finally { }

            return idInteraccion;
        }

        public async Task<IVRresponse[]> obtenerIVRResponses(string password)
        {
            HttpResponseMessage response = null;
            IVRresponse[] statusResponse = null;
            try
            {
                response = await client.GetAsync("/ArController.svc/get-status?userId=" + userId);

                if (response.IsSuccessStatusCode)
                {
                    statusResponse = await response.Content.ReadAsAsync<IVRresponse[]>();

                    foreach (IVRresponse ivrResponse in statusResponse)
                    {
                        if (ivrResponse.numeroTarjeta != null)
                        {
                            ivrResponse.numeroTarjeta = AESCrypto.Decrypt(ivrResponse.numeroTarjeta, password);
                        }

                        if (ivrResponse.cvc != null)
                        {
                            ivrResponse.cvc = AESCrypto.Decrypt(ivrResponse.cvc, password);
                        }

                    }

                }

            }
            catch (Exception e)
            {
                logger.Error("MitrolController.obtenerIVRResponses(): Error en comunicacion con Servicio obtenerIVRResponses", e);
                return null;
            }

            finally
            {

            }

            return statusResponse;
        }

        public async Task<string> generatePassword()
        {
            HttpResponseMessage response = null;
            // Generate random password
            string password = "pepesand";

            response = await client.GetAsync("/ArController.svc/set-password?userId=" + userId + "&password=" + password);
            return password;
        }
         public async Task<bool> endTransaction()
        {
            HttpResponseMessage response = null;
            bool result = false;
            try
            {
                response = await client.GetAsync("/ArController.svc/end-transaction?userId=" + userId);
                result = response.IsSuccessStatusCode;
                return result;
            }
            catch (Exception e)
            {
                logger.Error("MitrolController.cleanRepository(): Error en comunicacion con Servicio cleanRepository", e);
                Console.WriteLine(e.StackTrace);
                return result;
            }
        }

    }


}
