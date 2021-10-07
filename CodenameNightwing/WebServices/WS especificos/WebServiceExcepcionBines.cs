using CodenameNightwing.Config;
using System.Xml;

namespace CodenameNightwing.WebServices.WSEspecificos
{
    class WebServiceExcepcionBines : WebServiceBase
    {
        private string bin
        {
            get;
            set;
        }

        public WebServiceExcepcionBines(string bin)
        {
            this.bin = bin;
            setup();
        }

        protected override void setDireccion()
        {
            switch (Configuration.getInstance().environment)
            {
                case "QA":
                    direccion = Configuration.getInstance().dirServQa + Configuration.getInstance().serverBines.Replace("|environment|", Configuration.getInstance().environment);
                    break;
                case "PROD":
                    direccion = Configuration.getInstance().dirServProd + Configuration.getInstance().serverBines.Replace("|environment|", Configuration.getInstance().environment);
                    break;
                case "DEV":
                    direccion = Configuration.getInstance().dirServDev + Configuration.getInstance().serverBines.Replace("|environment|", Configuration.getInstance().environment);
                    break;
            }
        }

        protected override void writeQuery()
        {
            XmlDocument parser = new XmlDocument();
            parser.LoadXml(Configuration.getInstance().schemaWsExcepcionBines);
            XmlNode raiz = parser.DocumentElement;
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(parser.NameTable);
            nsmgr.AddNamespace("soapenv", "http://schemas.xmlsoap.org/soap/envelope/");
            nsmgr.AddNamespace("ws", "http://ws.objects.binesws.interact.aerolineas.com.ar/");
            raiz.SelectSingleNode("soapenv:Body/ws:getExceptionBin/arg0/bin", nsmgr).InnerText = bin;
            request = raiz.OuterXml;
        }
    }
}
