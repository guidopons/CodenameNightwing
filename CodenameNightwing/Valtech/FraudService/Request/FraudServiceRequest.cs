using CodenameNightwing.BusinessLogic;
using CodenameNightwing.Config;
using CodenameNightwing.FileManager;
using CodenameNightwing.Varios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodenameNightwing.Valtech.FraudService.Request
{
    class FraudServiceRequest
    {
        public Pointofsale pointOfSale { get; set; }

        public Channel channel { get; set; }

        public Generalfields generalFields { get; set; }
        public Specificfields specificFields { get; set; }
        public string operationMode { get; set; }
        public string operationType { get; set; }


        public static string getOperationType( Transaccion tran)
        {
            /*Enum → TICKET, EMD, EXCHANGE, CARGO*/
            switch (tran.tipoOperacion)
            {
                case TipoOperacion.COMPRA_PASAJE:
                    return "TICKET";

                case TipoOperacion.CANJE:
                    return "EXCHANGE";

                case TipoOperacion.COMPRA_SOLO_EMD:
                    return "EMD";
                default:
                    return "TICKET";
            }


        }
        public FraudServiceRequest(Transaccion tran)
        {

            this.operationMode = "SYNC";
            this.operationType = getOperationType( tran );

            this.pointOfSale = new Pointofsale(tran);
            this.channel = new Channel(tran);
            this.generalFields = new Generalfields();
            this.generalFields.pnr = tran.primaryPnr;

            Payment payment = new Payment( tran );

            Billingaddress billing = new Billingaddress( tran );
            payment.billingAddress = billing;

            Paymentdata paymentData = new Paymentdata(tran);
            payment.paymentData = paymentData;

            Gatewaydata gatewayData = new Gatewaydata( tran );
            payment.gatewayData = gatewayData;

            this.generalFields.payments = new Payment[1];
            this.generalFields.payments[0] = payment;

        }


        public class Generalfields
        {
            public Payment[] payments { get; set; }
            public string pnr { get; set; }
            public Bookingdata bookingData { get; set; }
        }

        public class Bookingdata
        {
            public Passenger[] passengers { get; set; }
        }

        public class Passenger
        {
            public string id { get; set; }
            public string firstName { get; set; }
            public string lastName { get; set; }
            public string type { get; set; }
            public string email { get; set; }
            public string phone { get; set; }
        }

        public class Payment
        {
            public Paymentholder paymentHolder { get; set; }
            public Billingaddress billingAddress { get; set; }
            public Paymentdata paymentData { get; set; }
            public Gatewaydata gatewayData { get; set; }

            public Payment ( Transaccion tran )
            {

                paymentHolder = new Paymentholder();
                paymentHolder.country = (tran.pais == null) ? "AR" : tran.pais.codigoPais;
                if (tran.tarjeta.owner.documento == null || tran.tarjeta.owner.documento.Trim().Length == 0)
                    paymentHolder.documentNumber = tran.tarjeta.owner.cuitCuil;
                else
                    paymentHolder.documentNumber = tran.tarjeta.owner.documento;

                paymentHolder.documentType = (tran.tarjeta.owner.genero == null || tran.tarjeta.owner.genero.Equals("E")) ? "PAS" : "DNI";
                paymentHolder.holderName = tran.tarjeta.owner.nombre;
                if (!(tran.tarjeta.owner.genero != null && tran.tarjeta.owner.genero.Equals("E")))
                    paymentHolder.gender = tran.tarjeta.owner.genero;
                else
                    paymentHolder.gender = "M";
                paymentHolder.companyTaxID = tran.tarjeta.owner.cuitCuil;

            }
        }

        public class Paymentholder
        {
            public string firstName { get; set; }
            public string lastName { get; set; }
            public string holderName { get; set; }
            public string documentType { get; set; }
            public string documentNumber { get; set; }
            public string country { get; set; }
            public string gender { get; set; }
            public string companyTaxID { get; set; }
        }

        public class Billingaddress
        {
            public string street { get; set; }
            public string city { get; set; }
            public string province { get; set; }
            public string zipCode { get; set; }
            public string country { get; set; }
            public string phone { get; set; }
            public string email { get; set; }

            public Billingaddress( Transaccion tran)
            {
                street = tran.tarjeta.owner.direccion;
                city = tran.tarjeta.owner.ciudad;
                province = tran.tarjeta.owner.provincia;
                zipCode = tran.tarjeta.owner.codPostal;
                country =  (tran.pais == null) ? "AR" : tran.pais.codigoPais;
            }
        }

        public class Paymentdata
        {
            public string totalAmount { get; set; }
            public string totalInterest { get; set; }
            public int installmentCount { get; set; }
            public string installmentAmount { get; set; }
            public string currency { get; set; }
            public string maskedCardNumber { get; set; }
            public string cardCode { get; set; }
            public string expirationDate { get; set; }
            public string authorizationCode { get; set; }


            public Paymentdata ( Transaccion tran)
            {
                this.totalAmount = POIutils.parseDecimalARGPunto(tran.importeTotal);
                this.totalInterest = POIutils.parseDecimalARGPunto(tran.interes);
                this.installmentCount = tran.cantCuotas;
                this.installmentAmount = POIutils.parseDecimalARGPunto(tran.importeTotal / tran.cantCuotas);
                this.currency = tran.currency;
                this.maskedCardNumber = tran.tarjeta.primeros6() + "XXXXXX" + tran.tarjeta.ultimos4();
                this.cardCode = tran.tarjeta.codSabre;
                this.expirationDate = tran.tarjeta.vencimiento;
                this.authorizationCode = tran.numAutorizacion;

            }

        }

        public class Gatewaydata
        {
            public string gatewayId { get; set; }
            public string gatewayReference { get; set; }

            public Gatewaydata( Transaccion tran)
            {
                gatewayId = tran.gateway;
                gatewayReference = (tran.gateway == "V") ? tran.trxIdVtolUnico : tran.trxId;
            }
        }

        public class Specificfields
        {
        }


    }
}
