//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó mediante una herramienta.
//     Los cambios del archivo se perderán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------
using CodenameNightwing.Config;
using System.Collections.Generic;
using System.Text;

namespace CodenameNightwing.BusinessLogic
{
    public class Pnr
    {
        private List<Pasajero> _pasajeros;
        public List<Pasajero> pasajeros
        {
            get { return _pasajeros; }
            set { _pasajeros = value; }
        }

        private List<string> _ffNumbers;
        public List<string> ffNumbers
        {
            get { return _ffNumbers; }
            set { _ffNumbers = value; }
        }

        private List<string> _seats;
        public List<string> seats
        {
            get { return _seats; }
            set { _seats = value; }
        }

        private List<string> _itins;
        public List<string> itins
        {
            get { return _itins; }
            set { _itins = value; }
        }

        private List<Ticket> _tickets;
        public List<Ticket> tickets
        {
            get { return _tickets; }
            set { _tickets = value; }
        }

        private List<string> _vcrs;
        public List<string> vcrs
        {
            get { return _vcrs; }
            set { _vcrs = value; }
        }

        public string getAgentDesc( )
        {

            string agentDesc = null;


            foreach (Ticket tkt in tickets)
            {

                if (tkt.agentDesc != null && tkt.agentDesc != "" && !tkt.agentDesc.Contains( Configuration.getInstance().robotSineToPrint))
                {
                    agentDesc = tkt.agentDesc;
                }

            }

            return agentDesc;


        }


        public bool isTicketVoided ( Ticket ticket )
        {

            bool isTicketVoided = false;


            foreach ( Ticket tkt in tickets)
            {
                
                if ( tkt.descripcion != null && tkt.descripcion.IndexOf( "TV") == 0)
                {
                    string tktNumber = tkt.getTicketNumber();
                    string tktNumberSearched = ticket.getTicketNumber();

                    if ( tktNumber != null && tktNumberSearched != null && tktNumber.Equals(tktNumberSearched))
                    {
                        return true;
                    }

                }

            }

            return isTicketVoided;


        }

        private bool _onlyEmd;
        public bool onlyEmd
        {
            get { return _onlyEmd; }
            set { _onlyEmd = value; }
        }

        private string _emails;
        public string emails
        {
            get { return _emails; }
            set { _emails = value; }
        }

        private string _groupName;
        public string groupName
        {
            get { return _groupName; }
            set { _groupName = value; }
        }

        private string _agentDesc;
        public string agentDesc
        {
            get { return _agentDesc; }
            set { _agentDesc = value; }
        }

        private string _tipoItinerario;
        public string tipoItinerario
        {
            get { return _tipoItinerario; }
            set { _tipoItinerario = value; }
        }

        private string _codSabre;
        public string codSabre
        {
            get { return _codSabre; }
            set { _codSabre = value; }
        }

        public decimal getTotalAmount()
        {
            decimal devolver = 0.0M;
            foreach (Pasajero aux in pasajeros)
            {
                devolver += aux.getTotalPax();
            }
            return devolver;
        }

        public decimal getTotalEmds()
        {
            decimal devolver = 0.0M;
            foreach (Pasajero aux in pasajeros)
            {
                devolver += aux.getTotalEmd();
            }
            return devolver;
        }

        public Pnr()
        {
            pasajeros = new List<Pasajero>();
            itins = new List<string>();
            tickets = new List<Ticket>();
            seats = new List<string>();
            ffNumbers = new List<string>();
            tipoItinerario = "";
            codSabre = "";
        }

        /*
         * 
         *  <lsPnr>
               <!--Optional:-->
               <agentDesc>pepe</agentDesc>
               <!--Optional:-->
               <codSabre>XDFGTR</codSabre>
               <!--Optional:-->
               <emails>pepe@pepe-cp,</emails>
               <!--Zero or more repetitions:-->
               <ffNumbers>15151551</ffNumbers>
               <!--Optional:-->
               <groupName>PEPE</groupName>
               <!--Zero or more repetitions:-->
               <itins>AEPGIG</itins>
               <onlyEmd>YES</onlyEmd>
               <!--Zero or more repetitions:-->
               <pasajeros>
                  <!--Zero or more repetitions:-->
                  <emds>
                     <codEmd>2</codEmd>
                     <!--Optional:-->
                     <descripcion>pepe</descripcion>
                     <fare>200.00</fare>
                  </emds>
                  <emds>
                     <codEmd>1</codEmd>
                     <!--Optional:-->
                     <descripcion>pepe 2</descripcion>
                     <fare>100.00</fare>
                  </emds>

                  <fare>200.00</fare>
                  <!--Optional:-->
                  <nombre>PEPE</nombre>
                  <!--Optional:-->
                  <tipoPax>ADT</tipoPax>
               </pasajeros>
               <!--Zero or more repetitions:-->
               <seats>pepe</seats>
               <!--Zero or more repetitions:-->
               <tickets>
                  <!--Optional:-->
                  <descExpanded>ho</descExpanded>
                  <!--Optional:-->
                  <descripcion>pepe</descripcion>
                  <!--Zero or more repetitions:-->
                  <fops>crop</fops>
                  <!--Optional:-->
                  <from>pepe</from>
                  <!--Optional:-->
                  <tourCode>AT</tourCode>
               </tickets>
               <!--Optional:-->
               <tipoItinerario>INT</tipoItinerario>
            </lsPnr>
*/


        public string toXMLOperacion()
        {
            StringBuilder xmlOutput = new StringBuilder();

            xmlOutput.Append("<lsPnr>");
            if (pasajeros != null)
            {
                foreach (Pasajero pax in pasajeros)
                {
                    xmlOutput.Append(pax.toXMLOperacion());
                }
            }

            if (tickets != null)
            {
                foreach (Ticket ticket in tickets)
                {
                    xmlOutput.Append(ticket.toXMLOperacion());
                }
            }

            xmlOutput.Append("<agentDesc>" + agentDesc + "</agentDesc>");
            xmlOutput.Append("<codSabre>" + codSabre + "</codSabre>");
            xmlOutput.Append("<emails>" + emails + "</emails>");
            string ffStr = (ffNumbers != null) ? string.Join("|", ffNumbers.ToArray()) : "";
            xmlOutput.Append("<ffNumbers>" + ffStr + "</ffNumbers>");
            xmlOutput.Append("<groupName>" + groupName + "</groupName>");
            string itinsStr = (itins != null) ? string.Join("|", itins.ToArray()) : "";
            xmlOutput.Append("<itins>" + itinsStr + "</itins>");

            xmlOutput.Append("<onlyEmd>" + onlyEmd + "</onlyEmd>");
            string seatsStr = (seats != null) ? string.Join("|", seats.ToArray()) : "";
            xmlOutput.Append("<seats>" + seatsStr + "</seats>");
            xmlOutput.Append("<tipoItinerario>" + tipoItinerario + "</tipoItinerario>");

            xmlOutput.Append("</lsPnr>");

            return xmlOutput.ToString();

        }
    }
}
