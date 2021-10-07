using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodenameNightwing.Valtech.AuditService.Response
{
    class AuditServiceResponse
    {

        public int statusCode { get; set; }
        public string errorMessage { get; set; }
        public string description { get; set; }
        public Validationdetails validationDetails { get; set; }

        public class Validationdetails
        {
        }

    }
}
