using CodenameNightwing.BusinessLogic;
using CodenameNightwing.Config;
using System.Xml;

namespace CodenameNightwing.WebServices.WSEspecificos.Transacciones
{
    class WebServiceInsertTransaction : WebServiceBase
    {
        private Transaccion _tran;
        public Transaccion tran
        {
            get { return _tran; }
            set { _tran = value; }
        }

        protected override void writeQuery()
        {
            XmlDocument parser = new XmlDocument();
            parser.LoadXml(Configuration.getInstance().schemaWsInsertTransaction);
            XmlNode raiz = parser.DocumentElement;
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(parser.NameTable);
            nsmgr.AddNamespace("soapenv", "http://schemas.xmlsoap.org/soap/envelope/");
            nsmgr.AddNamespace("ws", "http://ws.objects.binesws.interact.aerolineas.com.ar/");
            raiz.SelectSingleNode("/soapenv:Envelope/soapenv:Body/ws:insertTransaction/arg0", nsmgr).InnerText = tran.pdv.sucursal;
            raiz.SelectSingleNode("/soapenv:Envelope/soapenv:Body/ws:insertTransaction/arg1", nsmgr).InnerText = tran.pdv.caja;
            raiz.SelectSingleNode("/soapenv:Envelope/soapenv:Body/ws:insertTransaction/arg2", nsmgr).InnerText = tran.trxId;
            raiz.SelectSingleNode("/soapenv:Envelope/soapenv:Body/ws:insertTransaction/arg3", nsmgr).InnerText = ((int)tran.tipoAuth).ToString();
            raiz.SelectSingleNode("/soapenv:Envelope/soapenv:Body/ws:insertTransaction/arg4", nsmgr).InnerText = tran.fecha.ToString("yyyyMMdd HHmmss");
            raiz.SelectSingleNode("/soapenv:Envelope/soapenv:Body/ws:insertTransaction/arg5", nsmgr).InnerText = tran.getModoInt().ToString();
            raiz.SelectSingleNode("/soapenv:Envelope/soapenv:Body/ws:insertTransaction/arg6", nsmgr).InnerText = ((int)tran.tipoTrans).ToString();
            raiz.SelectSingleNode("/soapenv:Envelope/soapenv:Body/ws:insertTransaction/arg7", nsmgr).InnerText = ((int)tran.tipoIngreso).ToString();
            raiz.SelectSingleNode("/soapenv:Envelope/soapenv:Body/ws:insertTransaction/arg8", nsmgr).InnerText = tran.importeTotal.ToString("######0.00").Replace(",", ".");
            raiz.SelectSingleNode("/soapenv:Envelope/soapenv:Body/ws:insertTransaction/arg9", nsmgr).InnerText = tran.cantCuotas.ToString();
            raiz.SelectSingleNode("/soapenv:Envelope/soapenv:Body/ws:insertTransaction/arg10", nsmgr).InnerText = tran.numLote.ToString();
            raiz.SelectSingleNode("/soapenv:Envelope/soapenv:Body/ws:insertTransaction/arg11", nsmgr).InnerText = tran.numTicket;
            raiz.SelectSingleNode("/soapenv:Envelope/soapenv:Body/ws:insertTransaction/arg12", nsmgr).InnerText = tran.numAutorizacion;
            raiz.SelectSingleNode("/soapenv:Envelope/soapenv:Body/ws:insertTransaction/arg13", nsmgr).InnerText = tran.comercio.codigoComercio;
            raiz.SelectSingleNode("/soapenv:Envelope/soapenv:Body/ws:insertTransaction/arg14", nsmgr).InnerText = tran.tarjeta.primeros6() + "xxxxxx" + tran.tarjeta.ultimos4();
            raiz.SelectSingleNode("/soapenv:Envelope/soapenv:Body/ws:insertTransaction/arg15", nsmgr).InnerText = tran.tarjeta.descripcion;
            raiz.SelectSingleNode("/soapenv:Envelope/soapenv:Body/ws:insertTransaction/arg16", nsmgr).InnerText = tran.tarjeta.codPlan;
            raiz.SelectSingleNode("/soapenv:Envelope/soapenv:Body/ws:insertTransaction/arg17", nsmgr).InnerText = tran.nroTerminal;
            raiz.SelectSingleNode("/soapenv:Envelope/soapenv:Body/ws:insertTransaction/arg18", nsmgr).InnerText = string.IsNullOrEmpty(tran.tarjeta.owner.nombre) ? "" : tran.tarjeta.owner.nombre;
            raiz.SelectSingleNode("/soapenv:Envelope/soapenv:Body/ws:insertTransaction/arg19", nsmgr).InnerText = tran.fechaOriginal != null ? tran.fechaOriginal.ToString("yyyyMMdd HHmmss") : "";
            raiz.SelectSingleNode("/soapenv:Envelope/soapenv:Body/ws:insertTransaction/arg20", nsmgr).InnerText = tran.pdvOriginal.sucursal;
            raiz.SelectSingleNode("/soapenv:Envelope/soapenv:Body/ws:insertTransaction/arg21", nsmgr).InnerText = tran.pdvOriginal.caja;
            raiz.SelectSingleNode("/soapenv:Envelope/soapenv:Body/ws:insertTransaction/arg22", nsmgr).InnerText = tran.trxIdOriginal;
            raiz.SelectSingleNode("/soapenv:Envelope/soapenv:Body/ws:insertTransaction/arg23", nsmgr).InnerText = string.IsNullOrEmpty(tran.ticketOriginal) ? "0" : tran.ticketOriginal;
            raiz.SelectSingleNode("/soapenv:Envelope/soapenv:Body/ws:insertTransaction/arg24", nsmgr).InnerText = ((int)tran.tipoHost).ToString();
            raiz.SelectSingleNode("/soapenv:Envelope/soapenv:Body/ws:insertTransaction/arg25", nsmgr).InnerText = Configuration.getInstance().interactUser;
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

        public WebServiceInsertTransaction(Transaccion tran)
        {
            this.tran = tran;
            setup();
        }
    }
}
