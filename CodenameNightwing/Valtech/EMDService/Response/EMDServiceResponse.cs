
using System.Collections.Generic;

namespace CodenameNightwing.Valtech.PromoService.Response
{

    public class EMDServiceResponse
    {
        public string pnr { get; set; }
        public string errorResponseMsg { get; set; }
        public List<string> tickets { get; set; }

    }
 
}
