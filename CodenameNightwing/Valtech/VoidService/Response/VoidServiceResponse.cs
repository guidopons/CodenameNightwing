using CodenameNightwing.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodenameNightwing.Valtech.VoidService.Response
{

    /*
{
  "pnr": "string",
  "vcr": [
    {
      "vcr": "string",
      "success": true
    }
  ],
  "success": true
}   

    */
    public class VoidServiceResponse
    {
        public List<Vcr> vcr { get; set; }

        public bool success { get; set; }
        public string errorResponseMsg { get; set; }
        public VoidServiceResponse()
        {
            this.vcr = new List<Vcr>();
        }
    }

    public class Vcr
    {
        public string vcr { get; set; }
        public bool success { get; set; }

    }


}
