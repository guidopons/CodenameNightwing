using CodenameNightwing.BusinessLogic;
using CodenameNightwing.Valtech.Objects;
using System;
using System.Collections.Generic;
using System.Globalization;


namespace CodenameNightwing.Valtech.PromoService.Request
{
    class PromoServiceRequest
    {
        public string accountCode { get; set; }
        public string product { get; set; }
        public string flightType { get; set; }
        public string campaignId { get; set; }

        public string evaluationDate { get; set; }

        public int adtCount { get; set; }
        public int infCount { get; set; }
        public int chdCount { get; set; }

        public List<Flight> segments { get; set; }

        public PaymentData paymentData { get; set; }
        public Channel channel { get; set; }


        public PromoServiceRequest(Transaccion tran)
        {

            switch (tran.tipoOperacion)
            {
                case TipoOperacion.COMPRA_PASAJE:
                    this.product = "TICKET";
                    break;

                case TipoOperacion.CANJE:
                    this.product = "EXCHANGE";
                    break;

                case TipoOperacion.COMPRA_SOLO_EMD:
                    this.product = "ANCILLARIES";
                    break;

                default:
                    this.product = "TICKET";
                    break;

            }

            // TODO Workaound hasta que nos pongamos de acuerdo con Valtech en este campo
            // y poder devolver por default una regla y además inyectarla
            //this.product = "INTERACT";

            this.campaignId = "";
            this.accountCode = "";

            //TODO evaluationDate es fecha del dia?
            DateTime today = DateTime.Today;
            this.evaluationDate = today.ToString("s") + "Z";

            this.paymentData = new PaymentData()
            {
                amount = tran.importeTotal,
                currency = tran.currency
            };

            this.channel = new Channel(tran);

            //Solo se parsea 1er PNR: tran.listPnr[0]
            if (tran.listPnr != null) {

                //TODO: Por ahora solo se implementa "ONE_WAY" para 1 segmento y "ROUND_TRIP" para mas de 1  ;
                if (tran.listPnr[0].itins.Count == 1 )
                    this.flightType = "ONE_WAY"; 
                else
                    this.flightType = "ROUND_TRIP"; 

                this.segments = new List<Flight>();

                foreach (string itin in tran.listPnr[0].itins)
                {
                    if (itin != "")
                    {
                        Flight flight = new Flight(itin);
                        this.segments.Add(flight);
                        //this.routes.Add(flight.origin + "-" + flight.destination);
                    }
                }

                foreach (Pasajero pax in tran.listPnr[0].pasajeros)
                {
                    switch (pax.tipoPax)
                    {
                        case "ADT":
                            this.adtCount++;
                            break;
                        case "CNN":
                            this.chdCount++;
                            break;
                        case "INF":
                            this.infCount++;
                            break;
                    }
                }
            }
        }
     }

    public class PaymentData
    {
        public string currency { get; set; }
        public decimal amount { get; set; }
    }



}
      
