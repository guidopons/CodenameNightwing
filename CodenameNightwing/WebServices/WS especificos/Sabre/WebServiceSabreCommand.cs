using CodenameNightwing.Config;
using CodenameNightwing.Sabre;
using System.Xml;

namespace CodenameNightwing.WebServices.WSEspecificos.Transacciones
{
    class WebServiceSabreCommand : WebServiceBase
    {
        private string _token;
        public string token
        {
            get { return _token; }
            set { _token = value; }
        }

        private string _hostCmd;
        public string hostCmd
        {
            get { return _hostCmd; }
            set { _hostCmd = value; }
        }


        protected override void writeQuery()
        {
            XmlDocument parser = new XmlDocument();

            string schemaWsSabreCommand = Configuration.getInstance().schemaWsSabreCommand;
            parser.Load(schemaWsSabreCommand);


            XmlNode raiz = parser.DocumentElement;
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(parser.NameTable);

            nsmgr.AddNamespace("soapenv", "http://schemas.xmlsoap.org/soap/envelope/");
            nsmgr.AddNamespace("sec", "http://schemas.xmlsoap.org/ws/2002/12/secext");
            nsmgr.AddNamespace("mes", "http://www.ebxml.org/namespaces/messageHeader");
            nsmgr.AddNamespace("ns", "http://webservices.sabre.com/sabreXML/2003/07");


             // PASAR TOKEN
             raiz.SelectSingleNode("/soapenv:Envelope/soapenv:Header/sec:Security/sec:BinarySecurityToken", nsmgr).InnerText = this.token;
            // PASAR CCNUMBER
            //5-TBM* VI4507990000000010¥12/20
            raiz.SelectSingleNode("/soapenv:Envelope/soapenv:Body/ns:SabreCommandLLSRQ/ns:Request/ns:HostCommand", nsmgr).InnerText = this.hostCmd;

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


        public WebServiceSabreCommand( string token, string cmd)
        {
            this.token = token;
            this.hostCmd = cmd;

            setup();
        }
    }
}
