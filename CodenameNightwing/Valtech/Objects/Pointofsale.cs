using CodenameNightwing.BusinessLogic;
using CodenameNightwing.Config;
using CodenameNightwing.FileManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodenameNightwing.Valtech
{
    public class Pointofsale
    {

        public Pointofsale ( Transaccion tran)
        {
            // PUNTO DE VENTA
            // compania
            this.company = "AEROLINEAS";
            string canal = (Configuration.getInstance().tipoAuthConf.Equals(TipoAutorizador.VTOL_CALLCENTER)) ? "CALL" : "PRES";
            string canalNombre = (Configuration.getInstance().tipoAuthConf.Equals(TipoAutorizador.VTOL_CALLCENTER)) ? "CALL CENTER POI" : "PRESENCIAL";

            // channel id = CALL = CALL_ARG, CALL_BRA, etc.
            //            = PRES = PRES
            //            = WEB = WEB_ARG, WEB_BRA
            string channelId = (Configuration.getInstance().tipoAuthConf.Equals(TipoAutorizador.VTOL_CALLCENTER)) ? canal + "_" + tran.country : canal ;

            this.channelId = channelId;
            this.channelName = canalNombre;
            

            string office = (Configuration.getInstance().tipoAuthConf.Equals(TipoAutorizador.VTOL_CALLCENTER)) ? channelId : Configuration.getInstance().sucursal;
            this.office = office;
            this.cashier = Configuration.getInstance().caja;
            this.sine = Configuration.getInstance().interactUser;

            this.source = tran.tipoIngreso.ToString();
            this.pseudoCityCode = tran.pcc;
            this.stationId = tran.station;
            this.printer = tran.printer;

        }

        public Pointofsale()
        {
            // compania
            this.company = "AEROLINEAS";
            string canal = (Configuration.getInstance().tipoAuthConf.Equals(TipoAutorizador.VTOL_CALLCENTER)) ? "CALL" : "PRES";
            string canalNombre = (Configuration.getInstance().tipoAuthConf.Equals(TipoAutorizador.VTOL_CALLCENTER)) ? "CALL CENTER POI" : "PRESENCIAL";

            //Datos obtenidos de VBrequestReader commonFields
            this.country = VBrequestReader.getPropiedad("country");
            this.stationId = VBrequestReader.getPropiedad("station");
            this.printer = VBrequestReader.getPropiedad("printer");
            this.pseudoCityCode = VBrequestReader.getPropiedad("pcc");

            /* channel id = CALL = CALL_ARG, CALL_BRA, etc.
                          = PRES = PRES //AEROLINEAS_PRES_AR_USH_2510
                          = WEB = WEB_ARG, WEB_BRA
            */
            string channelId = (Configuration.getInstance().tipoAuthConf.Equals(TipoAutorizador.VTOL_CALLCENTER)) ? canal + "_" + this.country : canal;

            this.channelId = channelId;
            this.channelName = canalNombre;
            string office = (Configuration.getInstance().tipoAuthConf.Equals(TipoAutorizador.VTOL_CALLCENTER)) ? channelId : Configuration.getInstance().sucursal;
            this.office = office;
            this.cashier = Configuration.getInstance().caja;
            this.sine = Configuration.getInstance().interactUser;

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

        public string country { get; set; }

    }
}

