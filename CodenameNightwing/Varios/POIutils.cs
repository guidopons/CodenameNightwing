using CodenameNightwing.BusinessLogic;
using CodenameNightwing.Config;
using CodenameNightwing.FileManager;
using CodenameNightwing.WebServices;
using CodenameNightwing.WebServices.WSEspecificos;
using log4net;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CodenameNightwing.Varios
{
    class POIutils
    {
        public static readonly ILog logger = LogManager.GetLogger(typeof(POIutils));

        public static decimal parseDecimalStr( string decimalStr )
        {
            return Convert.ToDecimal( decimalStr , CultureInfo.InvariantCulture);
        }

        public static string decimalToString (decimal decimalNumber )
        {
            return decimalNumber.ToString("#####0.00", CultureInfo.InvariantCulture );
        }

        public static bool isDebitCard(string cardDescription) {

            List<TarjetaCajero> auxTarjetas = EntityLoader.loadTarjetas().Where(x => x.tipo == TipoTarjeta.DEBITO).ToList();
            foreach (TarjetaCajero item in auxTarjetas)
            {
               if ( item.descripcionTarjeta.ToUpperInvariant().Equals( cardDescription.ToUpperInvariant()))
                {
                    return true;
                }
            }

            return false;
        }


        public static SortedDictionary<string, string> getStatesList( string countryCode)
        {
            SortedDictionary<string, string> states = new SortedDictionary<string, string>();

            switch ( countryCode)
            {

                case "CA":

                    states.Add("Alberta Alberta", "AB");
                    states.Add("British Columbia Columbie-Britannique", "BC");
                    states.Add("Manitoba Manitoba", "MB");
                    states.Add("New Brunswick Nouveau-Brunswick", "NB");
                    states.Add("Newfoundland and Labrador Terre-Neuve-et-Labrador", "NL");
                    states.Add("Northwest Territories Territoires du Nord-Ouest", "NT");
                    states.Add("Nova Scotia Nouvelle-Écosse", "NS");
                    states.Add("Nunavut Nunavut", "NU");
                    states.Add("Ontario Ontario", "ON");
                    states.Add("Prince Edward Island Île-du-Prince-Édouard", "PE");
                    states.Add("Quebec Québec", "QC");
                    states.Add("Saskatchewan Saskatchewan", "SK");
                    states.Add("Yukon Yukon", "YT");

                    break;
                case "US":

                    states.Add("Alabama", "AL");
                    states.Add("Montana", "MT");
                    states.Add("Alaska", "AK");
                    states.Add("Nebraska", "NE");
                    states.Add("American Samoa", "AS");
                    states.Add("Nevada", "NV");
                    states.Add("Arizona", "AZ");
                    states.Add("New Hampshire", "NH");
                    states.Add("Arkansas", "AR");
                    states.Add("New Jersey", "NJ");
                    states.Add("California", "CA");
                    states.Add("New Mexico", "NM");
                    states.Add("Colorado", "CO");
                    states.Add("New York", "NY");
                    states.Add("Connecticut", "CT");
                    states.Add("North Carolina", "NC");
                    states.Add("Delaware", "DE");
                    states.Add("North Dakota", "ND");
                    states.Add("District of Columbia", "DC");
                    states.Add("Northern Mariana Islands", "MP");
                    states.Add("Federated States of Micronesia", "FM");
                    states.Add("Ohio", "OH");
                    states.Add("Florida", "FL");
                    states.Add("Oklahoma", "OK");
                    states.Add("Georgia", "GA");
                    states.Add("Oregon", "OR");
                    states.Add("Guam", "GU");
                    states.Add("Palau", "PW");
                    states.Add("Hawaii", "HI");
                    states.Add("Pennsylvania", "PA");
                    states.Add("Idaho", "ID");
                    states.Add("Puerto Rico", "PR");
                    states.Add("Illinois", "IL");
                    states.Add("Rhode Island", "RI");
                    states.Add("Indiana", "IN");
                    states.Add("South Carolina", "SC");
                    states.Add("Iowa", "IA");
                    states.Add("South Dakota", "SD");
                    states.Add("Kansas", "KS");
                    states.Add("Tennessee", "TN");
                    states.Add("Kentucky", "KY");
                    states.Add("Texas", "TX");
                    states.Add("Louisiana", "LA");
                    states.Add("Utah", "UT");
                    states.Add("Maine", "ME");
                    states.Add("Vermont", "VT");
                    states.Add("Marshall Islands", "MH");
                    states.Add("Virgin Islands", "VI");
                    states.Add("Maryland MD Virginia", "VA");
                    states.Add("Massachusetts MA Washington", "WA");
                    states.Add("Michigan MI West Virginia", "WV");
                    states.Add("Minnesota MN Wisconsin", "WI");
                    states.Add("Mississippi MS Wyoming", "WY");
                    states.Add("Missouri", "MO");

                    break;
            }

            return states;
        }

        public static string getCountryFromPCC( string pseudoCityCode )
        {
            string country = "AR";

            switch (pseudoCityCode)
            {
                case "NJL": country = "BR"; break;
                case "LYJ": country = "PE"; break;
                case "KYM": country = "US"; break;
                case "QDR": country = "ES"; break;
                case "GCB": country = "CO"; break;
                case "EYN": country = "CL"; break;
                case "OFR": country = "PY"; break;
                case "CWY": country = "BO"; break;
                case "RUC": country = "UY"; break;
                case "GYC": country = "MX"; break;
            }
            return country;
        }
        public static string parseDecimalARGPunto( decimal importe )
        {

            string importeFormatoArg = importe.ToString("#####0.00", CultureInfo.CreateSpecificCulture("es-AR"));
            string importeFormatoArgSinComa = importeFormatoArg.Replace(",", ".");
            return importeFormatoArgSinComa;

        }


        public static bool isRegionalConfArgentina() {
            
            // validacion de configuracion regional

            if ( Configuration.getInstance().confRegArg!= null && Configuration.getInstance().confRegArg.Equals("YES"))
            {

                string decimalSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
                string groupSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberGroupSeparator;

                if (!(decimalSeparator.Equals(",") && groupSeparator.Equals(".")))
                {
                    logger.Error("se inicio el programa con configuracion regional invalida: NumberGroupSeparator" + groupSeparator + " y NumberDecimalSeparator " + decimalSeparator);
                    MessageBox.Show("Este programa se inicio con configuracion regional de moneda distinta de Argentina, la puede cambiar o llamar a MDA", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                else
                {
                    logger.Info("se inicio el programa con configuracion regional NumberGroupSeparator: " + groupSeparator + " y NumberDecimalSeparator " + decimalSeparator);
                    return true;
                }

            }
            
            return true;

        }

        public static bool isMonthYearSmallerNow(string input)
        {
            input = input.Substring(0, 2) + "20" + input.Substring(2);
            DateTime pDate;
            if (!DateTime.TryParseExact(input, "MMyyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out pDate))
            {
                //Invalid date
                //log , show error
                return false;
            }
            if (  pDate.Year >= DateTime.Now.Year || (pDate.Month >= DateTime.Now.Month && pDate.Year == DateTime.Now.Year))
            {
                return true;
            }
            return false;
        }

        public static bool isDateSmallerNow(string input)
        {
            input = input.Replace("/", "");
            DateTime pDate;
            if (!DateTime.TryParseExact(input, "ddMMyyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out pDate))
            {
                //Invalid date
                //log , show error
                return false;
            }
            if (pDate.Year <= DateTime.Now.Year || (pDate.Month <= DateTime.Now.Month && pDate.Year == DateTime.Now.Year))
            {
                return true;
            }
            return false;
        }

        public static bool isVtolNpsAutorizator()
        {
            if (Configuration.getInstance().tipoAuth == TipoAutorizador.VTOL || Configuration.getInstance().tipoAuth == TipoAutorizador.NPS || Configuration.getInstance().tipoAuth == TipoAutorizador.VTOL_CALLCENTER)
            {
                return true;
            }
            return false;

        }

        public static bool isVtolNpsPosAutorizator()
        {
            if (Configuration.getInstance().tipoAuth == TipoAutorizador.POS_INGENICO || Configuration.getInstance().tipoAuth == TipoAutorizador.VTOL || Configuration.getInstance().tipoAuth == TipoAutorizador.NPS || Configuration.getInstance().tipoAuth == TipoAutorizador.VTOL_CALLCENTER)
            {
                return true;
            }
            return false;

        }

        public static bool isVtolNpsCallCenterAutorizator()
        {
            if (Configuration.getInstance().tipoAuth == TipoAutorizador.VTOL_CALLCENTER)
            {
                return true;
            }
            return false;

        }

        public static void updateTarjetaFromBin(string bin, Tarjeta tar)
        {
            updateTarjetaFromBin(bin, tar, false);
        }

        public static void updateTarjetaFromBin(string bin , Tarjeta tar, bool withDesc)
        {

            string desc = null;
            string codSabre = null;

            WebServiceBines wsPromo = new WebServiceBines(bin, 10);
            List<Promocion> promos = WebResponseParser.parseXMLBines(wsPromo.getResponse());
            if (promos.Count > 0)
            {
                desc = promos[0].descripcionTarjeta;
                codSabre = promos[0].codTarjetaSabre;
            }

            if ( desc != null && desc.Contains("AMEX"))
            {
                desc = "AMERICAN EXPRESS";
            }

            if( withDesc )
                tar.descripcion = desc;

            tar.codSabre = codSabre;

        }

    }
}
