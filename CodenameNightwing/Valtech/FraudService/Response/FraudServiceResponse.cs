using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodenameNightwing.Valtech.FraudService.Response
{
    class FraudServiceResponse
    {

        public FraudServiceResponse()
        {
            fraudCheckInformation = new Fraudcheckinformation();
        }

        public string operationMode { get; set; }
        public string caseId { get; set; }
        public Fraudcheckinformation fraudCheckInformation { get; set; }

        public class Fraudcheckinformation
        {
            public string caseId { get; set; }
            public string casePriority { get; set; }
            public string result { get; set; }
        }


    }
}
