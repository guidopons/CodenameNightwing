using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodenameNightwing.IVR.Models
{
    public class IVRresponse
    {

        public string numeroTarjeta { get; set; }

        public string fechaExp { get; set; }

        public string cvc { get; set; }

        public string status { get; set; }
    }
}