
using System.Collections;
using System.Collections.Generic;
using CodenameNightwing.BusinessLogic;


namespace CodenameNightwing.Valtech.PromoService.Response
{

    public class PromoServiceResponse
    {

        public string errorResponseMsg { get; set; }

        public AlternativeFormOfPayments alternativeFormOfPayments { get; set; }
        public List<object> debitCards { get; set; }

        public Dictionary<string, List<cuota>> fixedInstallments { get; set; }
        public Dictionary<string, List<cuota>> interestFreeInstallments { get; set; }
        public PaymentData paymentData { get; set; }
        public List<string> blockedBins { get; set; }
    }

    public class AlternativeFormOfPayments { }

    public class Bin
    {
            public List<int> values { get; set; }
    }

    public class cuota
    {
            public string paymentMethod { get; set; }
            public string paymentMethodType { get; set; }

            public string name { get; set; }
            public string issuer { get; set; }
            public List<Bin> bins { get; set; }
            public int installmentCount { get; set; }
            public decimal interestRate { get; set; }
            public string installmentSchemaId { get; set; }
            public decimal totalInterest { get; set; }
            public decimal totalAmount { get; set; }
            public decimal installmentAmount { get; set; }
            public Dictionary<string, Dictionary<string,string>> gatewayMetadata { get; set; }

            public List<GatewayMetadata> getGatewayMetaDataLs()
            {
                List<GatewayMetadata> lsMeta = new List<GatewayMetadata>();

                if (gatewayMetadata != null)
                    foreach (KeyValuePair<string, Dictionary<string, string>> entry in gatewayMetadata)
                    {
                    // do something with entry.Value or entry.Key
                        GatewayMetadata meta = new GatewayMetadata();
                        meta.gatewayName = entry.Key;
                        meta.gatewayProps = entry.Value;
                        lsMeta.Add(meta);
                    }

                return lsMeta;
        }
    }

    public class PaymentData
    {
            public string currency { get; set; }
            public decimal amount { get; set; }
    }
 
}
