using CodenameNightwing.Config;
using CodenameNightwing.Sabre;
using System.Text;
using System.Xml;

namespace CodenameNightwing.WebServices.WSEspecificos.Transacciones
{
    class WebServiceSabreGetReservation : WebServiceBase
    {
        private string _token;
        public string token
        {
            get { return _token; }
            set { _token = value; }
        }

        private string _pnr;
        public string pnr
        {
            get { return _pnr; }
            set { _pnr = value; }
        }

        protected override void writeQuery()
        {
            XmlDocument parser = new XmlDocument();

            string schemaWsSabreGetReservation = Configuration.getInstance().schemaWsSabreGetReservation;
            parser.Load(schemaWsSabreGetReservation);

            if (parser.FirstChild.NodeType == XmlNodeType.XmlDeclaration)
            {
                XmlDeclaration dec = (XmlDeclaration)parser.FirstChild;
                dec.Encoding = "UTF-16";
            }
            else
            {
                XmlDeclaration dec = parser.CreateXmlDeclaration("1.0", null, null);
                dec.Encoding = "UTF-16";
                parser.InsertBefore(dec, parser.DocumentElement);
            }


            XmlNode raiz = parser.DocumentElement;
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(parser.NameTable);
            
            nsmgr.AddNamespace("soapenv", "http://schemas.xmlsoap.org/soap/envelope/");
            nsmgr.AddNamespace("sec", "http://schemas.xmlsoap.org/ws/2002/12/secext");
            nsmgr.AddNamespace("mes", "http://www.ebxml.org/namespaces/messageHeader");
            nsmgr.AddNamespace("v1", "http://webservices.sabre.com/pnrbuilder/v1_19");
            nsmgr.AddNamespace("v11", "http://services.sabre.com/res/or/v1_14");

            // PASAR TOKEN
            raiz.SelectSingleNode("/soapenv:Envelope/soapenv:Header/sec:Security/sec:BinarySecurityToken", nsmgr).InnerText = this.token;
            // PASAR RESERVA
            raiz.SelectSingleNode("/soapenv:Envelope/soapenv:Body/v1:GetReservationRQ/v1:Locator", nsmgr).InnerText = this.pnr;

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

        public WebServiceSabreGetReservation( string token, string pnr)
        {
            this.token = token;
            this.pnr = pnr;
            setup();
        }
    }
}
