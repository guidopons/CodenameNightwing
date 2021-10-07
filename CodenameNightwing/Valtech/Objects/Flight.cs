using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CodenameNightwing.Valtech.Objects
{
    class Flight
    {

        public string origin { get; set; }
        public string destination { get; set; }

        public string flightNumber { get; set; }
        public string cabinClass { get; set; }
        public string brandId { get; set; }
        public char bookingClass { get; set; }
        public string operatingAirline { get; set; }

        public string departureDate { get; set; }

        public string arrivalDate { get; set; }

        public Flight(string itinerario)
        {
            string pattern = "(\\w{2})\\s*([\\d*]+)\\s+(\\w{5})\\s+(\\w{1})\\s+(\\w{3})(\\w{3})\\s+([\\d{4}]+)\\s+([\\d{4}]+)";

            Match match = Regex.Match(itinerario, pattern);

            this.operatingAirline = match.Groups[1].Value;
            this.flightNumber = match.Groups[2].Value;
            this.bookingClass = char.Parse(match.Groups[4].Value);

            string orig = match.Groups[5].Value;
            string dest = match.Groups[6].Value;

            this.origin = (new[] { "EZE", "AEP" }.Any(c => orig.Contains(c)) ? "BUE" : orig);
            this.destination = (new[] { "EZE", "AEP" }.Any(c => dest.Contains(c)) ? "BUE" : dest);

            string flightDate = match.Groups[3].Value;
            string departureTime = match.Groups[7].Value;
            string arrivalTime = match.Groups[8].Value;
            
            DateTime depDateTime = getDateTimefromStr(flightDate, departureTime);
            this.departureDate = depDateTime.ToString("s") + "Z"; //String.Format("{0:u}", depDateTime);  

            DateTime arrDateTime = getDateTimefromStr(flightDate, arrivalTime);

            int value = DateTime.Compare(depDateTime, arrDateTime);
            if (value > 0)
                //Departure date correponde al dia siguiente
                arrDateTime = arrDateTime.AddDays(1);

            this.arrivalDate = arrDateTime.ToString("s") + "Z"; // String.Format("{0:u}", arrDateTime);

            //TODO Mapa de clases hardodeado , ver si se puede parametrizar
            this.cabinClass = getCabinClass(this.bookingClass);

            //TODO ver si capturamos la branded
            this.brandId = "";    //"EC";




        }

        private string getCabinClass(char clase)
        {
            string cabinClass = null;
            switch (clase)
            {
                case 'W':
                case 'S':
                case 'Z':
                    cabinClass = "Club Economy";
                    break;
                case 'J':
                case 'C':
                case 'D':
                case 'I':
                case 'O':
                case 'F':
                    cabinClass = "Club Condor";
                    break;
                default:
                    cabinClass = "Economy";
                    break;
            }
            return cabinClass;
        }

        private DateTime getDateTimefromStr(string strDate, string strTime)
        {
            DateTime dateTime;
            DateTime today = DateTime.Today;
            try
            {
                int depDay = int.Parse(strDate.Substring(0, 2));

                string monthName = strDate.Substring(2, 3);
                int depMonth = DateTime.ParseExact(monthName, "MMM", CultureInfo.InvariantCulture).Month;

                int depHour = int.Parse(strTime.Substring(0, 2));
                int depMin = int.Parse(strTime.Substring(2, 2));

                dateTime = new DateTime(today.Year, depMonth, depDay, depHour, depMin, 0, 0);

                int value = DateTime.Compare(dateTime, today);
                if (value < 0)
                    //Departure date correponde al año siguiente
                    dateTime = dateTime.AddYears(1);
            }
            catch (Exception e)
            {
                throw e;
            }

            return dateTime;
        }


    }
}
