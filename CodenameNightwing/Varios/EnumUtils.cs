using CodenameNightwing.BusinessLogic;
using CodenameNightwing.IVR;
using CodenameNightwing.Printer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodenameNightwing.Varios
{
    class EnumUtils
    {
        public static TipoHost getTipoHostFromString( string tipoHostStr) {

            TipoHost tipo = TipoHost.DESCONOCIDO;
            if ( tipoHostStr != null) { 
                tipo = ((tipoHostStr.Equals("Amex")) ? TipoHost.AMEX : (tipoHostStr.Equals("Posnet") ? TipoHost.FIRST_DATA : TipoHost.VISA));
            }
            return tipo;

        }

        public static TipoHost getTipoHostFromIdLote(string idLote)
        {

            TipoHost tipo = TipoHost.DESCONOCIDO;
            if (idLote != null)
            {
                tipo = ((idLote.Equals("0")) ? TipoHost.VISA : (idLote.Equals("1") ? TipoHost.AMEX: TipoHost.FIRST_DATA));
            }
            return tipo;

        }

        public static string getDescriptionFromTipoCuenta(TipoCuentaDebito tipoCuenta)
        {

            switch (tipoCuenta)
            {
                case TipoCuentaDebito.CAJA_AHORRO_DOLARES:
                    return "Caja Ahorro Dolares";
                case TipoCuentaDebito.CAJA_AHORRO_PESOS:
                    return "Caja Ahorro Pesos";
                case TipoCuentaDebito.CUENTA_CORRIENTE_DOLARES:
                    return "Cuenta Corriente Dolares";
                case TipoCuentaDebito.CUENTA_CORRIENTE_PESOS:
                    return "Cuenta Corriente Pesos";
                default:
                    return null;
            }

        }


        public static TipoIngresoTarjeta getTipoIngresoTarjeta(string tipoCuenta)
        {

            switch (tipoCuenta)
            {
                case "MSR":
                    return TipoIngresoTarjeta.BAND;
                case "Contactless":
                    return TipoIngresoTarjeta.CONTACTLESS;
                case "Chip":
                    return TipoIngresoTarjeta.EMV;
                case "Manual":
                    return TipoIngresoTarjeta.MANUAL;
                case "MSR Chip":
                    return TipoIngresoTarjeta.FALLBACK;
                default:
                    return TipoIngresoTarjeta.BAND;
            }

        }

        public static string getDescripcionTipoIngresoTarjeta(TipoIngresoTarjeta tipoCuenta)
        {

            switch (tipoCuenta)
            {
                case TipoIngresoTarjeta.BAND:
                    return "banda";
                case TipoIngresoTarjeta.EMV:
                    return "chip";
                case TipoIngresoTarjeta.MANUAL:
                    return "manual";
                case TipoIngresoTarjeta.FALLBACK:
                    return "fallback";
                case TipoIngresoTarjeta.CONTACTLESS:
                    return "contactless";
                default:
                    return "";
            }

        }

        public static string getDescripcionTipoIngresoTarjetaAMEX(TipoIngresoTarjeta tipoCuenta)
        {

            switch (tipoCuenta)
            {
                case TipoIngresoTarjeta.FALLBACK:
                case TipoIngresoTarjeta.BAND:
                    return "B";
                case TipoIngresoTarjeta.EMV:
                    return "C";
                case TipoIngresoTarjeta.MANUAL:
                    return "M";
                default:
                    return "";
            }

        }

        public static TipoCuentaDebito getTipoCuentaFromString(string tipoCuenta)
        {

            switch (tipoCuenta)
            {
                case "1":
                    return TipoCuentaDebito.CAJA_AHORRO_PESOS;
                case "2":
                    return TipoCuentaDebito.CUENTA_CORRIENTE_PESOS;
                case "3":
                    return TipoCuentaDebito.CAJA_AHORRO_DOLARES;
                case "4":
                    return TipoCuentaDebito.CUENTA_CORRIENTE_DOLARES;
                default:
                    return TipoCuentaDebito.CAJA_AHORRO_PESOS;
            }

        }

        public static TipoWCentrixStatus getMitrolStatus( string mitrolStatus)
        {

            try
            {
                TipoWCentrixStatus statusValue = (TipoWCentrixStatus)Enum.Parse(typeof(TipoWCentrixStatus), mitrolStatus, true);
                if (Enum.IsDefined(typeof(TipoWCentrixStatus), statusValue) | statusValue.ToString().Contains(","))
                    return statusValue;
                else
                    return TipoWCentrixStatus.DESCONOCIDO;
            }
            catch (Exception e)
            {
                IVRManager.logger.Error("No se pudo convertir el mitrolStatus enviado: " + mitrolStatus + " exception : " + e) ;
                return TipoWCentrixStatus.DESCONOCIDO;
            }
        }


        public static TipoIdiomaIVR getIdioma(string idiomaMitrol)
        {

            try
            {
                TipoIdiomaIVR statusValue = (TipoIdiomaIVR)Enum.Parse(typeof(TipoIdiomaIVR), idiomaMitrol, true);
                if (Enum.IsDefined(typeof(TipoIdiomaIVR), statusValue) | statusValue.ToString().Contains(","))
                    return statusValue;
                else
                    return TipoIdiomaIVR.ESPAÑOL;
            }
            catch (ArgumentException)
            {
                return TipoIdiomaIVR.ESPAÑOL;
            }
        }

    }
}
