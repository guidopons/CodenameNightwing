using CodenameNightwing.Config;
using CodenameNightwing.Sabre;
using System;
using System.Windows.Forms;
using System.Xml;

namespace CodenameNightwing.WebServices.WSEspecificos.Transacciones
{
    class WebServiceSabreCreateSession : WebServiceBase
    {
        private SabreSession _sabreSession;
        public SabreSession sabreSession
        {
            get { return _sabreSession; }
            set { _sabreSession = value; }
        }

        protected override void writeQuery()
        {
            XmlDocument parser = new XmlDocument();

            string schemaWsSabreCreateSession = Configuration.getInstance().schemaWsSabreCreateSession;
            parser.Load(schemaWsSabreCreateSession);

            XmlNode raiz = parser.DocumentElement;
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(parser.NameTable);

            nsmgr.AddNamespace("soapenv", "http://schemas.xmlsoap.org/soap/envelope/");
            nsmgr.AddNamespace("mes", "http://www.ebxml.org/namespaces/messageHeader");
            nsmgr.AddNamespace("ns", "http://www.opentravel.org/OTA/2002/11");
            nsmgr.AddNamespace("sec", "http://schemas.xmlsoap.org/ws/2002/12/secext");

            try
            {
                // PASAR SINE
                raiz.SelectSingleNode("/soapenv:Envelope/soapenv:Header/sec:Security/sec:UsernameToken/sec:Username", nsmgr).InnerText = sabreSession.sine;

                // PASAR PASSWORD
                raiz.SelectSingleNode("/soapenv:Envelope/soapenv:Header/sec:Security/sec:UsernameToken/sec:Password", nsmgr).InnerText = sabreSession.password;

                request = raiz.OuterXml;
            }
            catch (Exception e) {
                Program.logger.Error("Error en el parseo de respuesta de WS SessionCreateRS", e);

            }


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

        public WebServiceSabreCreateSession( string username, string password)
        {
            SabreSession sabreSession = new SabreSession( username , password );
            this.sabreSession = sabreSession;
            setup();
        }
    }
}
