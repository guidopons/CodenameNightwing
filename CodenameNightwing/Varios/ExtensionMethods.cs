using CodenameNightwing.Autorization;
using CodenameNightwing.BusinessLogic;
using System;
using System.Globalization;
using System.Windows.Forms;

namespace CodenameNightwing.Varios
{
    public static class ExtensionMethods
    {
        public static string ToBase36String(this string numeroAConvertir)
        {
            return Convertir(10, 36, numeroAConvertir);
        }

        public static string ToLongFromBase36(this string numeroAConvertir)
        {
            return Convertir(36, 10, numeroAConvertir);
        }

        //Convert number in string representation from base:from to base:to. 
        //Return result as a string
        public static string Convertir(int from, int to, string s)
        {
            //Return error if input is empty
            if (string.IsNullOrEmpty(s))
            {
                return ("Error: Nothing in Input String");
            }
            //only allow uppercase input characters in string
            s = s.ToUpper();
            //only do base 2 to base 36 (digit represented by characters 0-Z)"
            if (from < 2 || from > 36 || to < 2 || to > 36)
            {
                return ("Base requested outside range");
            }
            //convert string to an array of integer digits representing number in base:from
            int il = s.Length;
            int[] fs = new int[il];
            int k = 0;
            for (int i = s.Length - 1; i >= 0; i--)
            {
                if (s[i] >= '0' && s[i] <= '9') { fs[k++] = (int)(s[i] - '0'); }
                else
                {
                    if (s[i] >= 'A' && s[i] <= 'Z') { fs[k++] = 10 + (int)(s[i] - 'A'); }
                    else
                    { return ("Error: Input string must only contain any of 0 - 9 or A-Z"); } //only allow 0-9 A-Z characters
                }
            }
            //check the input for digits that exceed the allowable for base:from
            foreach (int i in fs)
            {
                if (i >= from) { return ("Error: Not a valid number for this input base"); }
            }
            //find how many digits the output needs
            int ol = il * (from / to + 1);
            int[] ts = new int[ol + 10]; //assign accumulation array
            int[] cums = new int[ol + 10]; //assign the result array
            ts[0] = 1; //initialize array with number 1 
            //evaluate the output
            for (int i = 0; i < il; i++) //for each input digit
            {
                for (int j = 0; j < ol; j++) //add the input digit times (base:to from^i) to the output cumulator
                {
                    cums[j] += ts[j] * fs[i];
                    int temp = cums[j];
                    int rem = 0;
                    int ip = j;
                    do // fix up any remainders in base:to
                    {
                        rem = temp / to;
                        cums[ip] = temp - rem * to; ip++;
                        cums[ip] += rem;
                        temp = cums[ip];
                    }
                    while (temp >= to);
                }
                //calculate the next power from^i) in base:to format
                for (int j = 0; j < ol; j++)
                {
                    ts[j] = ts[j] * from;
                }
                for (int j = 0; j < ol; j++) //check for any remainders
                {
                    int temp = ts[j];
                    int rem = 0;
                    int ip = j;
                    do  //fix up any remainders
                    {
                        rem = temp / to;
                        ts[ip] = temp - rem * to; ip++;
                        ts[ip] += rem;
                        temp = ts[ip];
                    }
                    while (temp >= to);
                }
            }
            //convert the output to string format (digits 0,to-1 converted to 0-Z characters) 
            string sout = string.Empty; //initialize output string
            bool first = false; //leading zero flag
            for (int i = ol; i >= 0; i--)
            {
                if (cums[i] != 0) { first = true; }
                if (!first) { continue; }
                if (cums[i] < 10) { sout += (char)(cums[i] + '0'); }
                else { sout += (char)(cums[i] + 'A' - 10); }
            }
            if (string.IsNullOrEmpty(sout)) { return "0"; } //input was zero, return 0 return the converted string
            return sout;
        }

        public static Transaccion ToTransaccion(this string aConvertir)
        {
            Transaccion resultado;
            string[] arr = aConvertir.Split('|');
            if (arr != null)
            {
                try
                {
                    resultado = new TransaccionBuilder(TipoTransaccion.COMPRA, (arr[5] == "VTL" ? TipoAutorizador.VTOL : TipoAutorizador.NPS));
                    //resultado.sucursal = Convert.ToInt32(arr[0]);
                    //resultado.caja = Convert.ToInt32(arr[1]);
                    resultado.fecha = DateTime.ParseExact(arr[0], "yyyyMMdd HHmmss", CultureInfo.InvariantCulture);
                    resultado.numTicket = arr[1];
                    resultado.trxReferenceId = Convert.ToInt32(arr[2].ToLongFromBase36());
                    resultado.importeTotal = Convert.ToDecimal(arr[3], CultureInfo.InvariantCulture);
                    resultado.cantCuotas = Convert.ToInt32(arr[4]);
                    resultado.trxId = arr[6];
                }
                catch (Exception e)
                {
                    Program.logger.Error("Error al leer entrada de archivo de anulaciones", e);
                    resultado = null;
                }
            }
            else
                resultado = null;
            return resultado;
        }

        public static string ToNPSDateString(this DateTime aConvertir) {

            string dateStr = aConvertir.ToString("yyyy-MM-dd HH:mm:ss");
            return dateStr;

        }

        public static string ToNPSDateStringBirth(this DateTime aConvertir)
        {

            string dateStr = aConvertir.ToString("yyyy-MM-dd");
            return dateStr;

        }

        public static string ToVTOLDateString(this DateTime aConvertir)
        {
            string dateStr = aConvertir.ToString("yyyyMMddHHmmss");
            return dateStr;
        }

        public static string ToSabreDateString(this DateTime aConvertir)
        {
            switch (aConvertir.Month)
            {
                case 1:
                    return aConvertir.Day + "JAN" + aConvertir.Year;
                case 2:
                    return aConvertir.Day + "FEB" + aConvertir.Year;
                case 3:
                    return aConvertir.Day + "MAR" + aConvertir.Year;
                case 4:
                    return aConvertir.Day + "APR" + aConvertir.Year;
                case 5:
                    return aConvertir.Day + "MAY" + aConvertir.Year;
                case 6:
                    return aConvertir.Day + "JUN" + aConvertir.Year;
                case 7:
                    return aConvertir.Day + "JUL" + aConvertir.Year;
                case 8:
                    return aConvertir.Day + "AUG" + aConvertir.Year;
                case 9:
                    return aConvertir.Day + "SEP" + aConvertir.Year;
                case 10:
                    return aConvertir.Day + "OCT" + aConvertir.Year;
                case 11:
                    return aConvertir.Day + "NOV" + aConvertir.Year;
                case 12:
                    return aConvertir.Day + "DEC" + aConvertir.Year;
                default:
                    return aConvertir.Day + "JAN" + aConvertir.Year;
            }
        }

        public static Tarjeta ToTarjeta(this TarjetaCajero aConvertir)
        {
            Tarjeta aDevolver = new Tarjeta();
            aDevolver.codSabre = aConvertir.codTarjetaSabre;
            aDevolver.codTarjeta = aConvertir.codNumTarjeta;
            aDevolver.tipoTarjeta = aConvertir.tipo;
            aDevolver.codPlan = aConvertir.codPlan;
            aDevolver.descripcion = aConvertir.descripcionTarjeta;
            return aDevolver;
        }

        /* Para ocultar nodos de treeview  */
        private const int TVIF_STATE = 0x8;
        private const int TVIS_STATEIMAGEMASK = 0xF000;
        private const int TV_FIRST = 0x1100;
        private const int TVM_SETITEM = TV_FIRST + 63;

        /// <summary>
        /// Hides the checkbox for the specified node on a TreeView control.
        /// </summary>
        public static void HideCheckBox(this TreeNode node)
        {
            NativeMethods.TVITEM tvi = new NativeMethods.TVITEM();
            tvi.hItem = node.Handle;
            tvi.mask = TVIF_STATE;
            tvi.stateMask = TVIS_STATEIMAGEMASK;
            tvi.state = 0;
            NativeMethods.SendMessage(node.TreeView.Handle, TVM_SETITEM, IntPtr.Zero, ref tvi);
        }

        public static bool IsNumeric(this string text)
        {
            double test;
            return double.TryParse(text, out test);
        }
    }
}