using CodenameNightwing.BusinessLogic;
using CodenameNightwing.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodenameNightwing.Valtech.AuditService.Request
{
    class AuditServiceRequest
    {

        public AuditServiceRequest( AuditData auditData )
        {

            Transaccion tran = auditData.tran;
            // PUNTO DE VENTA
            // compania
            this.company = "AEROLINEAS";
            string canal = (Configuration.getInstance().tipoAuthConf.Equals(TipoAutorizador.VTOL_CALLCENTER)) ? "CALL" : "PRES";
            string canalNombre = (Configuration.getInstance().tipoAuthConf.Equals(TipoAutorizador.VTOL_CALLCENTER)) ? "CALL CENTER" : "PRESENCIAL";
            this.channelId = canal;
            this.channelName = canalNombre;
            if (tran != null)
            {
                this.pseudoCityCode = tran.pcc;
                this.source = tran.tipoIngreso.ToString();
                this.reservationCode = tran.primaryPnr;
                this.lastName = tran.tarjeta.owner.nombre;

                string office = (Configuration.getInstance().tipoAuthConf.Equals(TipoAutorizador.VTOL_CALLCENTER)) ? canal + "_" + tran.country : Configuration.getInstance().sucursal;
                this.office = office;

            }
            this.cashier = Configuration.getInstance().caja;
            this.sine = Configuration.getInstance().interactUser;

            // OPERATION DATA
            this.timeStamp =DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff+0000");
            this.operationType = auditData.operationType;
            this.responseTime = auditData.responseTime;
            this.shoppingId = auditData.shoppingId;
            this.vendorId = auditData.vendorId;
            this.requestPayload = auditData.requestPayload;
            this.responsePayload = auditData.responsePayload;
            this.success = auditData.success;
            this.statusCode = auditData.statusCode;

            this.channel = new Channel(tran);

    }
        public string id { get; set; }
        public string timeStamp { get; set; }
        public string operationType { get; set; }
        public int responseTime { get; set; }
        public string shoppingId { get; set; }
        public string vendorId { get; set; }
        public string reservationCode { get; set; }
        public string lastName { get; set; }
        public string requestPayload { get; set; }
        public string responsePayload { get; set; }
        public Sessiondata sessionData { get; set; }
        public string success { get; set; }
        public int statusCode { get; set; }
        public string channelId { get; set; }
        public string channelName { get; set; }
        public string sabreStorefront { get; set; }
        public string pseudoCityCode { get; set; }
        public string stationId { get; set; }
        public string office { get; set; }
        public string cashier { get; set; }
        public string printer { get; set; }
        public string sine { get; set; }
        public string company { get; set; }
        public string source { get; set; }

        public Channel channel { get; set; }
    }

    public class Sessiondata
    {
    }

}
