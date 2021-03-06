//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó mediante una herramienta.
//     Los cambios del archivo se perderán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------
using CodenameNightwing.Config;
using System.Xml;
using System;

namespace CodenameNightwing.WebServices.WSEspecificos
{
    public class WebServiceBines : WebServiceBase
    {
        private string bin
        {
            get;
            set;
        }

        private decimal importe
        {
            get; set;
        }

        protected override void writeQuery()
        {
            XmlDocument parser = new XmlDocument();
            parser.LoadXml(Configuration.getInstance().schemaWsBines);
            XmlNode raiz = parser.DocumentElement;
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(parser.NameTable);
            nsmgr.AddNamespace("soapenv", "http://schemas.xmlsoap.org/soap/envelope/");
            nsmgr.AddNamespace("ws", "http://ws.objects.binesws.interact.aerolineas.com.ar/");
            raiz.SelectSingleNode("soapenv:Body/ws:getInteresesHoyConImporte/arg0/bin", nsmgr).InnerText = bin;
            raiz.SelectSingleNode("soapenv:Body/ws:getInteresesHoyConImporte/arg1", nsmgr).InnerText = importe.ToString().Replace(",", ".");
            request = raiz.OuterXml;
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

        public WebServiceBines(string bin, decimal imp)
        {
            this.bin = bin;
            importe = imp;
            setup();
        }
    }
}
