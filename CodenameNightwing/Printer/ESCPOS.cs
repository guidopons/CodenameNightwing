namespace CodenameNightwing.Printer
{
    public class ESCPOS
    {
        //public static readonly string dejarEspacioAlFinal = (char)27 + "d" + "1";
        public static readonly string underlineStart = (char)27 + "-" + "2";
        public static readonly string underlineEnd = (char)27 + "-" + "0";
        public static readonly string enfasisStart = (char)27 + "E" + "1";
        public static readonly string enfasisEnd = (char)27 + "E" + "0";
        public static readonly string doubleStrikeStart = (char)27 + "G" + "1";
        public static readonly string doubleStrikeEnd = (char)27 + "G" + "0";
        public static readonly string characterFontA = (char)27 + "G" + "0";
        public static readonly string characterFontB = (char)27 + "G" + "1";
        public static readonly string characterFontC = (char)27 + "G" + "2";
        public static readonly string fontBold = (char)27 + "G" + "3";
        public static readonly string justificacionIzquierda = (char)27 + "a" + "0";
        public static readonly string justificacionCentro = (char)27 + "a" + "1";
        public static readonly string justificacionDerecha = (char)27 + "a" + "2";

        public static readonly string eCut = (char)27 + "i";//+ "\r\n";
                                                          
        public static readonly string bigCharStart = (char)27 + "!" + (char)56;
        public static readonly string bigCharEnd = (char)27 + "!" + (char)0;

        public static readonly string eClear = (char)27 + "@";

        public static readonly string offlineRelieve = (char)27 + "RE";

    }
}
