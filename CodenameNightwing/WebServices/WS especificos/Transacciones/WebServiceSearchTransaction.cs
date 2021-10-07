using CodenameNightwing.Config;
using System;
using System.Xml;

namespace CodenameNightwing.WebServices.WS_especificos.Transacciones
{
    class WebServiceSearchTransaction : WebServiceBase
    {
        private DateTime _fechaABuscar;
        public DateTime fechaABuscar
        {
            get { return _fechaABuscar; }
            set { _fechaABuscar = value; }
        }

        private string _ultimos4Tarjeta;
        public string ultimos4Tarjeta
        {
            get { return _ultimos4Tarjeta; }
            set { _ultimos4Tarjeta = value; }
        }

        private string _nombreTarjeta;
        public string nombreTarjeta
        {
            get { return _nombreTarjeta; }
            set { _nombreTarjeta = value; }
        }

        private string _caja;
        public string caja
        {
            get { return _caja; }
            set { _caja = value; }
        }


        private string _sucursal;
        public string sucursal
        {
            get { return _sucursal; }
            set { _sucursal = value; }
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

        protected override void writeQuery()
        {
            XmlDocument parser = new XmlDocument();
            parser.LoadXml(Configuration.getInstance().schemaWsSearchTransaction);
            XmlNode raiz = parser.DocumentElement;
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(parser.NameTable);
            nsmgr.AddNamespace("soapenv", "http://schemas.xmlsoap.org/soap/envelope/");
            nsmgr.AddNamespace("ws", "http://ws.objects.binesws.interact.aerolineas.com.ar/");
            raiz.SelectSingleNode("/soapenv:Envelope/soapenv:Body/ws:searchTransaction/arg0", nsmgr).InnerText = fechaABuscar.ToString("yyyyMMdd HHmmss");
            raiz.SelectSingleNode("/soapenv:Envelope/soapenv:Body/ws:searchTransaction/arg1", nsmgr).InnerText = ultimos4Tarjeta;
            raiz.SelectSingleNode("/soapenv:Envelope/soapenv:Body/ws:searchTransaction/arg2", nsmgr).InnerText = nombreTarjeta;
            raiz.SelectSingleNode("/soapenv:Envelope/soapenv:Body/ws:searchTransaction/arg3", nsmgr).InnerText = sucursal;
            raiz.SelectSingleNode("/soapenv:Envelope/soapenv:Body/ws:searchTransaction/arg4", nsmgr).InnerText = caja;
            request = raiz.OuterXml;
        }

        public WebServiceSearchTransaction(DateTime fechaABuscar,string ultimos4Tarjeta,string nombreTarjeta, string sucursalParam , string cajaParam)
        {
            this.fechaABuscar = fechaABuscar;
            this.ultimos4Tarjeta = ultimos4Tarjeta;
            this.nombreTarjeta = nombreTarjeta;
            this.caja = cajaParam;
            this.sucursal = sucursalParam;
            setup();
        }
    }
}
