using CodenameNightwing.Config;
using System.Xml;

namespace CodenameNightwing.WebServices.WSEspecificos.Transacciones
{
    class WebServiceReversarTransaccion : WebServiceBase
    {
        private int _trxReferenceIdAReversar;
        public int trxReferenceIdAReversar
        {
            get { return _trxReferenceIdAReversar; }
            set { _trxReferenceIdAReversar = value; }
        }

        protected override void writeQuery()
        {
            XmlDocument parser = new XmlDocument();
            parser.LoadXml(Configuration.getInstance().schemaWsSetReversado);
            XmlNode raiz = parser.DocumentElement;
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(parser.NameTable);
            nsmgr.AddNamespace("soapenv", "http://schemas.xmlsoap.org/soap/envelope/");
            nsmgr.AddNamespace("ws", "http://ws.objects.binesws.interact.aerolineas.com.ar/");
            raiz.SelectSingleNode("/soapenv:Envelope/soapenv:Body/ws:setReversado/arg0", nsmgr).InnerText = trxReferenceIdAReversar.ToString();
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

        public WebServiceReversarTransaccion(int trxReferenceIdAAnular)
        {
            this.trxReferenceIdAReversar = trxReferenceIdAAnular;
            setup();
        }
    }
}
