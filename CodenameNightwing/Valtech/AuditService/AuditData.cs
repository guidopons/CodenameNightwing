using CodenameNightwing.BusinessLogic;
using CodenameNightwing.Varios;
using System;
using System.Net;
using System.Net.Http;

namespace CodenameNightwing.Valtech.AuditService
{
    class AuditData
    {

        public AuditData( AuditVendorId vendorId, AuditOperationType operationType , Transaccion tran , HttpResponseMessage httpResponseMessage, int responseTime, string requestContent)
        {
            this.tran = tran;
            this.operationType = operationType.ToString();

            if (httpResponseMessage != null)
            {
                this.statusCode = (int)httpResponseMessage.StatusCode;
                this.success = httpResponseMessage.IsSuccessStatusCode.ToString().ToLower();
                string responseContent = AsyncHelper.RunSync(() => httpResponseMessage.Content.ReadAsStringAsync());
                this.responsePayload = responseContent;
            }
            else
            {
                this.statusCode = 400;
                this.success = "false";
                this.responsePayload = "NONE";
            }

            this.requestPayload = requestContent;

            this.vendorId = vendorId.ToString();

            this.responseTime = responseTime;


        }

        public AuditData(AuditVendorId vendorId, AuditOperationType operationType, Transaccion tran, string requestPayload, Exception e):this( vendorId , operationType , tran , null, 0, requestPayload )
        {
            this.responsePayload = e.StackTrace;
        }


        public AuditData(AuditVendorId vendorId, AuditOperationType operationType, Transaccion tran, int responseTime, string requestContent,HttpStatusCode statusCode, string response ) :this(vendorId , operationType , tran , null , responseTime, requestContent)
        {
            this.statusCode = (int)statusCode;
            if ((int)statusCode == 200) { 
                this.success =  "true" ;
            }
            else
            {
                this.success = "false";
            }
            this.responsePayload = response;
        }

        public string id { get; set; }
        public string operationType { get; set; }
        public int responseTime { get; set; }
        public string shoppingId { get; set; }
        public string vendorId { get; set; }
        public string requestPayload { get; set; }
        public string responsePayload { get; set; }
        public string success { get; set; }
        public int statusCode { get; set; }

        public Transaccion tran { get; set; }


    }
}
