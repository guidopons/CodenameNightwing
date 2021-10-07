using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodenameNightwing.BusinessLogic
{
    /*

         "gatewayMetadata": {
           "NPS": {
             "psp_MerchandId": "aeroarg_cc",
             "psp_Product": "66"
           },
           "VTOL": {
             "psp_MerchandId": "aeroarg_cc",
             "psp_Product": "66"
           },
           .....
         },

          */
    public class GatewayMetadata
    {

        public string gatewayName { get; set;  }
        public Dictionary<string, string> gatewayProps { get; set; }

        public GatewayMetadata()
        {
            gatewayProps = new Dictionary<string,string>();
        }
    }
}
