using CodenameNightwing.BusinessLogic;
using CodenameNightwing.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodenameNightwing.Valtech
{
    class Pointofsale
    {

        public Pointofsale ( Transaccion tran)
        {
            // PUNTO DE VENTA
            // compania
            this.company = "AEROLINEAS";
            string canal = (Configuration.getInstance().tipoAuth.Equals(TipoAutorizador.VTOL_CALLCENTER)) ? "CALL" : "PRES";
            string canalNombre = (Configuration.getInstance().tipoAuth.Equals(TipoAutorizador.VTOL_CALLCENTER)) ? "CALL CENTER" : "PRESENCIAL";
            this.channelId = canal;
            this.channelName = canalNombre;
            this.pseudoCityCode = tran.pcc;

            string office = (Configuration.getInstance().tipoAuth.Equals(TipoAutorizador.VTOL_CALLCENTER)) ? canal + "_" + tran.country : Configuration.getInstance().sucursal;
            this.office = office;
            this.cashier = Configuration.getInstance().caja;
            this.sine = Configuration.getInstance().interactUser;
            this.source = tran.tipoIngreso.ToString();

        }

        public string source { get; set; }
        public string pointOfSaleId { get; set; }
        public string sabreStorefront { get; set; }
        public string pseudoCityCode { get; set; }
        public string stationId { get; set; }
        public string office { get; set; }
        public string cashier { get; set; }
        public string printer { get; set; }
        public string sine { get; set; }
        public string company { get; set; }
        public string channelId { get; set; }
        public string channelName { get; set; }

    }
}
