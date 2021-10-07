using CodenameNightwing.Config;
using System;
using System.Net;
using System.Net.Http;

namespace CodenameNightwing.Valtech
{



    class BaseServiceController
    {


        protected HttpClient client = null;
        protected static HttpClient clientStatic = null;

        public BaseServiceController()
        {
            {

                if ( Configuration.getInstance().isProxyEnabled)
                {
                    HttpClientHandler handler = new HttpClientHandler()
                    {
                        Proxy = new WebProxy( Configuration.getInstance().proxyAddress ),
                        UseProxy = true,
                    };

                    client = new HttpClient(handler);
                    clientStatic = new HttpClient(handler);
                }
                else
                {
                    client = new HttpClient();
                    clientStatic = new HttpClient();
                }

                client.Timeout = TimeSpan.FromMinutes(Configuration.getInstance().valtechServicesTimeout);
                clientStatic.Timeout = TimeSpan.FromMinutes(Configuration.getInstance().valtechServicesTimeout);


            }
        }
    }
}
