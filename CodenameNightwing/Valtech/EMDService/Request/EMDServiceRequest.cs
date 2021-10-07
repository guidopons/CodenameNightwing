using CodenameNightwing.BusinessLogic;
using CodenameNightwing.Config;
using CodenameNightwing.FileManager;
using CodenameNightwing.Valtech.Objects;
using System.Collections.Generic;
using System.Globalization;


namespace CodenameNightwing.Valtech.PromoService.Request
{
    public class EMDServiceRequest
    {
        public string pnr { get; set; }

        public Channel channel { get; set; }

        public Pointofsale pointOfSale { get; set; }
        public Emd emd { get; set; }

        public List<Passenger> passengers { get; set; }

        public List<FormOfPayment> formOfPayment { get; set; }

        public List<string> segments { get; set; }


        public OptionalFields optionalFields;

        public EMDServiceRequest( ) {

            this.pointOfSale = new Pointofsale();
            this.channel = new Channel();

            string code = "D";
            string subCode = "ITC";
            string groupCode = "99";
            string comercialName = "INTERESES TARJETAS DE CREDITO";

            this.emd = new Emd(code, subCode, groupCode, comercialName);

            this.passengers = new List<Passenger>();
            this.passengers.Add(new Passenger("1.1", "1"));

            this.segments = new List<string>();
        }
    }

    public class Passenger
    {
        public Passenger(string index, string count) {
            this.passengerIndex = index;
            this.itemCount = count;
        }
        public string passengerIndex { get; set; }
        public string itemCount { get; set; }


    }
    public class Emd
    {
        public string code { get; set; }
        public string subCode { get; set; }
        public string groupCode { get; set; }
        public string commercialName { get; set; }

        public Emd(string code, string subCode, string groupCode, string comercialName) {
            this.code = code;
            this.subCode = subCode;
            this.groupCode = groupCode;
            this.commercialName = comercialName;
        }
    }


    public class OptionalFields
    {
        public string endorsement { get; set; }
        public bool isPnrOpened { get; set; }
        public string sabreSessionToken { get; set; }
        public decimal emdAmount { get; set; }

        public OptionalFields(string endorsement, bool isPnrOpened, decimal emdAmount) {
            this.endorsement = endorsement;
            this.isPnrOpened = isPnrOpened;
            this.emdAmount = emdAmount;
        }
    }

    public class FormOfPayment
    {
        public string fopId { get; set; }
        public string cardCode { get; set; }
        public string cardNumber { get; set; }
        public string expirationDate { get; set; }

        public string approvalCode { get; set; }

        public decimal amount { get; set; }

        public string currency { get; set; }

        public FormOfPayment(string fopId, string cardCode, string cardNumber, string expirationDate, string approvalCode, decimal amount, string currency ) {
            this.fopId = fopId;
            this.cardCode = cardCode;
            this.cardNumber = cardNumber;
            this.expirationDate = expirationDate;
            this.approvalCode = approvalCode;
            this.amount = amount;
            this.currency = currency;
        }

        public FormOfPayment(string fopId, decimal amount, string currency)
        {
            this.fopId = fopId;
            this.amount = amount;
            this.currency = currency;
        }
    }

}
