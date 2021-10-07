using CodenameNightwing.BusinessLogic;
using CodenameNightwing.FileManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodenameNightwing.Autorization.NPS.Config
{
    class NPSConfigurator
    {

        public static string getCurrencyCode ( string currency)
        {
            switch ( currency )
            {
                case "ARS": return "032";
                case "BRL": return "986";
                case "CLP": return "152";
                case "USD": return "840";
                case "EUR": return "978";
            }
            // si no se manda el currency no se va a procesar por NPS
            return "-1";
        }

        public static string getCountryCode (string country )
        {
            // en la tabla de pcc ponemos en country code directo de NPS
            return country;
        }

        public static string getProductCode( Tarjeta  tarjeta )
        {
            TarjetaCajero auxTC = EntityLoader.loadTarjetas().First(x => x.codTarjetaSabre == tarjeta.codSabre );

            // Si es Nativa, y empieza con 4, procesarla como VISA, sino como Master que está en la base
            if ( tarjeta.primeros6().StartsWith("4") && tarjeta.codSabre == "NT")
            {
                return "14";
            }

            return auxTC.codNumTarjeta;
        }
    }
}
