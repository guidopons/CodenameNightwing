using CodenameNightwing.BusinessLogic;
using CodenameNightwing.Varios;
using Newtonsoft.Json;
using System;
using System.Web;

namespace CodenameNightwing.IVR
{
    class MapaContextoSecureTrx
    {

        public MapaContextoSecureTrx( string interactionIdOriginal,  Transaccion tran , bool token , int languageId)
        {

            this.interactionId = interactionIdOriginal;
            this.numCuotas = PlanesAHORA.getCuotasDividir(tran.cantCuotas);
            string descPlan = tran.getDescripcionPlan();
            this.plan = ( descPlan != null && descPlan.Contains("PLAN Z"))?true:false;
            this.bin = tran.tarjeta.primeros6();

            int cantCC = 16;
            int cantCVC = 3;

            if (tran.tarjeta.codSabre == null || tran.tarjeta.codSabre.Trim().Length != 2 || tran.tarjeta.codSabre.Equals("NC"))
            {
                POIutils.updateTarjetaFromBin(tran.tarjeta.primeros6(), tran.tarjeta);
            }

            switch (tran.tarjeta.codSabre)
            {
                case "AX":
                    cantCC = 15;
                    cantCVC = 4;
                    break;
                case "DC":
                    if (tran.tarjeta.primeros6().StartsWith("3"))
                    {
                        cantCC = 14;
                        cantCVC = 3;
                    }
                    else
                    {
                        cantCC = 16;
                        cantCVC = 3;
                    }

                    break;
                case "MO":
                case "MA":
                    cantCC = 16;
                    cantCVC = 3;
                    break;
                default:
                    cantCC = 16;
                    cantCVC = 3;
                    break;
            }

            //TODO segun tran.tarjeta.codSabre pasar estos parametros
            this.cantCarCC = cantCC;
            this.cantCarCVC = cantCVC;

            this.urlRetorno = "https://" + NetworkUtils.GetLocalIPAddress() + ":443/api/ivr";

            this.importe = tran.importeTotal;
            this.currency = tran.currency;

            this.token = token;
            this.languageID = languageId;

        }

        public string interactionId { get; set; }

        public int numCuotas { get; set; }

        public bool plan { get; set; }

        public string bin { get; set; }

        public int cantCarCC { get; set; }

        public int cantCarCVC { get; set; }

        public string urlRetorno { get; set; }

        public decimal importe { get; set; }
 
        public string currency { get; set; }
 
        public int languageID { get; set; }

        public bool token { get; set; }

        public string toJson()
        {
            string json = JsonConvert.SerializeObject(this, Formatting.None);
            return json;
        }

        public string toUriHttp()
        {
            string strImporte =  this.importe.ToString("#####0.00").Replace("," , "");
            string strUrlRetorno = HttpUtility.UrlEncode( this.urlRetorno );
            return  String.Format("idInteraccion={10}&parametros=numCuotas:{0},plan:{1},bin:{2},cantCarCC:{3},cantCarCVC:{4},urlRetorno:{5},importe:{6},currency:{7},languageID:{8},token:{9}", this.numCuotas,this.plan,this.bin, this.cantCarCC, this.cantCarCVC, strUrlRetorno, strImporte, this.currency, this.languageID, this.token, this.interactionId);
        }
    }
}
