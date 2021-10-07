using CodenameNightwing.BusinessLogic;
using CodenameNightwing.Config;
using CodenameNightwing.FileManager;
using CodenameNightwing.Varios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodenameNightwing.Valtech
{
    public class Channel
    {

        public Channel( Transaccion tran )
        {
            // compania
            this.company = "AEROLINEAS";
            string canal = (Configuration.getInstance().tipoAuthConf.Equals(TipoAutorizador.VTOL_CALLCENTER)) ? "CALL" : "PRES";
            string canalNombre = (Configuration.getInstance().tipoAuthConf.Equals(TipoAutorizador.VTOL_CALLCENTER)) ? "CALL CENTER POI" : "PRESENCIAL";

            this.channelName = canalNombre;

            this.stationId = VBrequestReader.getPropiedad("station");
            this.printer = VBrequestReader.getPropiedad("printer");
            this.cashier = Configuration.getInstance().caja;
            this.sine = Configuration.getInstance().interactUser;

            this.pseudoCityCode = ( tran == null )? VBrequestReader.getPropiedad("pcc") : tran.pcc;

            string office = (Configuration.getInstance().tipoAuthConf.Equals(TipoAutorizador.VTOL_CALLCENTER)) ? null : Configuration.getInstance().sucursal;
            this.office = office;
            this.cashier = Configuration.getInstance().caja;
            this.sine = Configuration.getInstance().interactUser;
            this.source = (tran == null)?null:tran.tipoIngreso.ToString();

            this.country = POIutils.getCountryFromPCC( this.pseudoCityCode) ;

            string code = this.company + "_" + canal + "_" + country ;
            string sucursal = Configuration.getInstance().sucursal;

            this.code = (!Configuration.getInstance().tipoAuthConf.Equals(TipoAutorizador.VTOL_CALLCENTER)) ? code + "_" + this.pseudoCityCode + "_" + sucursal:code;

        }

        public Channel():this(null)
        {

        }


        public string code { get; set; }
        public string company { get; set; }
        public string channelName { get; set; }
        public string country { get; set; }
        public string sabreStorefront { get; set; }
        public string pseudoCityCode { get; set; }
        public string stationId { get; set; }
        public string office { get; set; }
        public string cashier { get; set; }
        public string printer { get; set; }
        public string sine { get; set; }
        public string source { get; set; }

    }
}
