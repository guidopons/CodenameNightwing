using CodenameNightwing.Config;
using CodenameNightwing.Sabre;
using System.Xml;

namespace CodenameNightwing.WebServices.WSEspecificos.Transacciones
{
    class WebServiceSabreAddRemark : WebServiceBase
    {
        private string _token;
        public string token
        {
            get { return _token; }
            set { _token = value; }
        }

        private string _ccNumber;
        public string ccNumber
        {
            get { return _ccNumber; }
            set { _ccNumber = value; }
        }


        private string _ccCode;
        public string ccCode
        {
            get { return _ccCode; }
            set { _ccCode = value; }
        }


        private string _expireDate;
        public string expireDate
        {
            get { return _expireDate; }
            set { _expireDate = value; }
        }


        protected override void writeQuery()
        {
            XmlDocument parser = new XmlDocument();

            string schemaWsAddRemarkLLSRQ = Configuration.getInstance().schemaWsAddRemarkLLSRQ;
            parser.Load(schemaWsAddRemarkLLSRQ);


            XmlNode raiz = parser.DocumentElement;
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(parser.NameTable);

            nsmgr.AddNamespace("soapenv", "http://schemas.xmlsoap.org/soap/envelope/");
            nsmgr.AddNamespace("sec", "http://schemas.xmlsoap.org/ws/2002/12/secext");
            nsmgr.AddNamespace("mes", "http://www.ebxml.org/namespaces/messageHeader");
            nsmgr.AddNamespace("ns", "http://webservices.sabre.com/sabreXML/2011/10");

            // PASAR TOKEN
            raiz.SelectSingleNode("/soapenv:Envelope/soapenv:Header/sec:Security/sec:BinarySecurityToken", nsmgr).InnerText = this.token;

            // PASAR CC Number , Code, ExpireDate
           
            //<ns:PaymentCard Code="VI" ExpireDate="2020-12" Number="4507990000000010"/>
            raiz.SelectSingleNode("/soapenv:Envelope/soapenv:Body/ns:AddRemarkRQ/ns:RemarkInfo/ns:FOP_Remark/ns:CC_Info/ns:PaymentCard", nsmgr).Attributes["Code"].Value = this.ccCode;
            raiz.SelectSingleNode("/soapenv:Envelope/soapenv:Body/ns:AddRemarkRQ/ns:RemarkInfo/ns:FOP_Remark/ns:CC_Info/ns:PaymentCard", nsmgr).Attributes["ExpireDate"].Value = this.expireDate;
            raiz.SelectSingleNode("/soapenv:Envelope/soapenv:Body/ns:AddRemarkRQ/ns:RemarkInfo/ns:FOP_Remark/ns:CC_Info/ns:PaymentCard", nsmgr).Attributes["Number"].Value = this.ccNumber;

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


        public WebServiceSabreAddRemark( string token, string ccNumber, string code, string expireDate)
        {
            this.token = token;
            this.ccNumber = ccNumber;
            this.ccCode = code;
            this.expireDate = expireDate;

            setup();
        }
    }
}
