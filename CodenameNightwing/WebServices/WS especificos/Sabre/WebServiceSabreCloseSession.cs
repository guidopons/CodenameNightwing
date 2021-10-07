using CodenameNightwing.Config;
using CodenameNightwing.Sabre;
using System.Xml;

namespace CodenameNightwing.WebServices.WSEspecificos.Transacciones
{
    class WebServiceSabreCloseSession : WebServiceBase
    {
        private string _token;
        public string token
        {
            get { return _token; }
            set { _token = value; }
        }

        protected override void writeQuery()
        {
            XmlDocument parser = new XmlDocument();

            string schemaWsSabreCloseSession = Configuration.getInstance().schemaWsSabreCloseSession;
            parser.Load(schemaWsSabreCloseSession);


            XmlNode raiz = parser.DocumentElement;
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(parser.NameTable);

            nsmgr.AddNamespace("soapenv", "http://schemas.xmlsoap.org/soap/envelope/");
            nsmgr.AddNamespace("sec", "http://schemas.xmlsoap.org/ws/2002/12/secext");
            nsmgr.AddNamespace("mes", "http://www.ebxml.org/namespaces/messageHeader");
            nsmgr.AddNamespace("ns", "http://www.opentravel.org/OTA/2002/11");

             // PASAR TOKEN
            raiz.SelectSingleNode("/soapenv:Envelope/soapenv:Header/sec:Security/sec:BinarySecurityToken", nsmgr).InnerText = this.token;
            request = raiz.OuterXml;
            
        }

        protected override void setDireccion()
        {
            switch (Configuration.getInstance().environment)
            {
                case "QA":
                    direccion = "https://sws-crt-as.cert.havail.sabre.com";
                    break;
                case "PROD":
                    direccion = "https://webservices.havail.sabre.com";
                    break;
                case "DEV":
                    direccion = "https://sws-crt-as.cert.havail.sabre.com";
                    break;
            }
        }



        public WebServiceSabreCloseSession(string token)
        {
            this.token = token;
            setup();
        }
    }
}
