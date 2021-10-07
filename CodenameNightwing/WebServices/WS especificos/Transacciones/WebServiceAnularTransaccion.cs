using CodenameNightwing.Config;
using System.Xml;

namespace CodenameNightwing.WebServices.WSEspecificos.Transacciones
{
    class WebServiceAnularTransaccion : WebServiceBase
    {
        private int _trxReferenceIdAAnular;
        public int trxReferenceIdAAnular
        {
            get { return _trxReferenceIdAAnular; }
            set { _trxReferenceIdAAnular = value; }
        }

        protected override void writeQuery()
        {
            XmlDocument parser = new XmlDocument();
            parser.LoadXml(Configuration.getInstance().schemaWsSetAnulado);
            XmlNode raiz = parser.DocumentElement;
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(parser.NameTable);
            nsmgr.AddNamespace("soapenv", "http://schemas.xmlsoap.org/soap/envelope/");
            nsmgr.AddNamespace("ws", "http://ws.objects.binesws.interact.aerolineas.com.ar/");
            raiz.SelectSingleNode("/soapenv:Envelope/soapenv:Body/ws:setAnulado/arg0", nsmgr).InnerText = trxReferenceIdAAnular.ToString();
            request = raiz.OuterXml;
        }

        protected override void setDireccion()
        {
            switch (Configuration.getInstance().environment)
            {
                case "QA":
                    direccion = Configuration.getInstance().dirServQa + Configuration.getInstance().serverTransacciones.Replace("|environment|", Configuration.getInstance().environment);
                    break;
                case "PROD":
                    direccion = Configuration.getInstance().dirServProd + Configuration.getInstance().serverTransacciones.Replace("|environment|", Configuration.getInstance().environment);
                    break;
                case "DEV":
                    direccion = Configuration.getInstance().dirServDev + Configuration.getInstance().serverTransacciones.Replace("|environment|", Configuration.getInstance().environment);
                    break;
            }
        }

        public WebServiceAnularTransaccion(int trxReferenceIdAAnular)
        {
            this.trxReferenceIdAAnular = trxReferenceIdAAnular;
            setup();
        }
    }
}
