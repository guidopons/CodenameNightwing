using CodenameNightwing.BusinessLogic;
using CodenameNightwing.Config;
using CodenameNightwing.FileManager;
using CodenameNightwing.WebServices;
using CodenameNightwing.WebServices.WSEspecificos;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace CodenameNightwing.Printer.Cupones
{
    class PrintItin: PrinterCupon
    {

        System.Collections.Generic.Dictionary<string,string> mapBines = new Dictionary<string, string>();

        private Pnr _infoPnr;
        public Pnr infoPnr
        {
            get { return _infoPnr; }
            set { _infoPnr = value; }
        }

        public PrintItin(Transaccion trans, EnumCopiaUOriginal tipo, bool isDuplicado)
            : base(false)
        {
        }

        public PrintItin(Transaccion trans, EnumCopiaUOriginal tipo)
            : base(false)
        {
        }

        public PrintItin(bool testing)
            : base(false)
        {
        }

        public PrintItin(bool testing , Pnr pnr)
            : base(false)
        {
            this.infoPnr = pnr;
            setInfoTicketInfoDelOriginal();
        }

        public override string getNombreOperacion()
        {
            return ( this.nombreOperacion != "")? this.nombreOperacion:"ITINERARIO";
        }


        protected override void setInfoTicketInfoDelOriginal()
        {

            
            encabezado.Add(LogoPrinter.GetLogo());
            if ( Configuration.getInstance().puesto != null)
            {
                encabezado.Add( ESCPOS.justificacionIzquierda + "Puesto: " + Configuration.getInstance().puesto);
            }
            if (Configuration.getInstance().nombreImpresora != null)
            {
                encabezado.Add(ESCPOS.justificacionIzquierda + "Impresora: " + Configuration.getInstance().nombreImpresora);
            }

            if ( infoPnr.agentDesc != null)
            {
                encabezado.Add(devolverConPadding((ESCPOS.justificacionIzquierda + "Agente:" + infoPnr.agentDesc) , DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToString("HH:mm")));
            }
            encabezado.Add("_____________________________________________\n" + ESCPOS.justificacionIzquierda);
            encabezado.Add( ESCPOS.bigCharStart + "Cod. Reserva: " + infoPnr.codSabre + "\n"  + ESCPOS.bigCharEnd );

            if (infoPnr.pasajeros.Count >= 1)
            {
                encabezado.Add("_____________________________________________\n" + ESCPOS.justificacionIzquierda);
                encabezado.Add("Pasajeros:\n" + ESCPOS.justificacionIzquierda);
                addSpace(encabezado, 1);

                if ( infoPnr.groupName != null && infoPnr.groupName != "" && !infoPnr.groupName.Equals("null"))
                {
                    encabezado.Add("Nombre de Grupo:");
                    encabezado.AddRange( getStringForCupon(infoPnr.groupName).Split('|'));
                    addSpace(encabezado, 1);
                }
            }
            
            foreach (Pasajero pax in infoPnr.pasajeros)
            {
                encabezado.Add(pax.nombre + ".");
            }
            addSpace(encabezado, 1);
            if (infoPnr.emails != null && !infoPnr.emails.Trim().Equals(""))
            {
                string emailInfo = Regex.Replace(infoPnr.emails, "==", "_");
                encabezado.Add("Emails:");
                string[] arrayEmail = emailInfo.Split(',');
                foreach (string emailAux in arrayEmail) {
                    encabezado.Add( emailAux);
                }

            }

            string avoidItin = VBrequestReader.getPropiedad("printer.options.avoidItinPrinter");

            if ( !(avoidItin != null && avoidItin.Equals("Y"))) { 

                if (infoPnr.itins.Count >= 1)
                {
                    encabezado.Add("_____________________________________________\n" + ESCPOS.justificacionIzquierda);
                    encabezado.Add("Itinerario:\n" + ESCPOS.justificacionIzquierda);
                    addSpace(encabezado, 1);
                }

                string lastOperated = null;
                foreach (string itinStr in infoPnr.itins)
                {
                    if ( itinStr.Length != 0)
                    {
                        string[] lsParts = itinStr.Split(';');
                        foreach (string itinPart in lsParts)
                        {
                            if (itinPart.Contains("undefined"))
                            {
                                encabezado.Add(lastOperated + ESCPOS.fontBold);
                            }
                            else
                            {
                                encabezado.Add(itinPart + ESCPOS.fontBold);
                                if (itinStr.Contains("AR1"))
                                {
                                    encabezado.Add("OPERATED BY AEROLINEAS ARGENTINAS" + ESCPOS.justificacionIzquierda + ESCPOS.fontBold);
                                }
                                else
                                {
                                    if ( lsParts.Length >= 2 && !lsParts[1].Contains("undefined"))
                                        lastOperated = lsParts[1];
                                }
                            }
                        

                        }
                        
                       addSpace(encabezado, 1);

                    }

                }
            }

            if (infoPnr.seats.Count >= 1)
            {
                encabezado.Add("_____________________________________________\n" + ESCPOS.justificacionIzquierda);
                encabezado.Add("Asientos:\n" + ESCPOS.justificacionIzquierda);
                addSpace(encabezado, 1);
            }

            int i = 0;
            
            foreach (string seatLine in infoPnr.seats)
            {
                encabezado.Add(seatLine);
                i++;
                if ( i %  2 == 0)
                {
                    addSpace(encabezado, 1);
                }
            }


            if (infoPnr.ffNumbers.Count >= 1)
            {
                encabezado.Add("_____________________________________________\n" + ESCPOS.justificacionIzquierda);
                encabezado.Add("Pasajero Frecuente:\n" + ESCPOS.justificacionIzquierda);
                addSpace(encabezado, 1);
            }

            i = 0;
            foreach (string ffLine in infoPnr.ffNumbers)
            {
                encabezado.Add(ffLine);
                i++;
                if (i % 2 == 0)
                {
                    addSpace(encabezado, 1);
                }
            }

            Dictionary<string , decimal> mapFop = new Dictionary<string, decimal>();
            Dictionary<string, decimal> mapFopRefund = new Dictionary<string, decimal>();

            if (infoPnr.tickets.Count >= 1)
            {
                encabezado.Add("_____________________________________________\n" + ESCPOS.justificacionIzquierda);
                encabezado.Add("Tickets:\n" + ESCPOS.justificacionIzquierda);
                addSpace(encabezado, 1);
            }

            bool isItinWithVoid = false;
            bool isItinWithFee = false;

            foreach (Ticket ticket in infoPnr.tickets)
            {
                string auxTktStr = ticket.descripcion.Trim();
                string tktType = auxTktStr.Substring(0, 2);


                // Saco los tickets que estan void
                if (tktType.Equals("TE") && infoPnr.isTicketVoided(ticket))
                {
                    isItinWithVoid = true;
                    continue;
                }
                else
                {
                    if (tktType.Equals("TV"))
                    {
                        isItinWithVoid = true;
                    }
                }


                // Saco los tickets que no son del agente si tiene el filtro
                string filterAgent = VBrequestReader.getPropiedad("printer.options.filterAgent");
                string agentNameFilter = VBrequestReader.getPropiedad("printer.options.agentNameFilter");
                if ( filterAgent != null && filterAgent.ToLower().Equals("y") && agentNameFilter != null && ticket.agentDesc != null)
                {
                    // Si el SINE es de un ROBOT usado por POI, imprimir
                    // Si no es de un Robot
                    if ( !ticket.agentDesc.Contains( Configuration.getInstance().robotSineToPrint))
                    {
                        // Si no es el Agente actual
                        if (!ticket.agentDesc.Contains(agentNameFilter))
                        {
                            continue;
                        }
                    }
                    
                }



                bool ticketWithTourCode = false;
              

                if ( auxTktStr.Length > 1 ) {
                    

                    // Si tiene Tour Code debemos ocultar el precio
                    if ( ticket.tourCode != null && ticket.tourCode != "" && !ticket.tourCode.Equals("null"))
                    {
                        if ( ticket.tourCode.Equals("BT") || ticket.tourCode.Equals("IT"))
                        {
                            string pattern = "(.*)\\s\\d+\\.\\d{2}(.*)";
                            string replacement = "$1$2";
                            Regex rgx = new Regex(pattern);
                            auxTktStr = rgx.Replace(auxTktStr, replacement);
                            ticketWithTourCode = true;
                        }
                    }

                    // Si es EMD de intereses ocultar el nombre
                    if ( ticket.descExpanded!= null && ticket.descExpanded.Contains("ITC"))
                    {
                        string pattern = "(\\w+\\s+)[^\\d]*(.*)";
                        string replacement = "$1$2";
                        Regex rgx = new Regex(pattern);
                        auxTktStr = rgx.Replace(auxTktStr, replacement);
                        
                    }

                    if ( ticket.status != null && !ticket.status.Equals("NOGO"))
                    {
                        auxTktStr = auxTktStr + " " + ticket.status;
                    }

                    encabezado.Add( (auxTktStr.Length > 48)?auxTktStr.Substring(0,47): auxTktStr );
                    if (ticket.descExpanded != null && ticket.descExpanded != "" && !ticket.descExpanded.Equals("undefined"))
                    {
                        
                        ticket.descExpanded = ticket.descExpanded.Replace("ENDO\\:", "ENDO:");
                        ticket.descExpanded = Regex.Replace(ticket.descExpanded, @"\\u00A5", "");
                        ticket.descExpanded = ticket.descExpanded.Replace("\\n", "");
                        
                        // Si tiene ENDO sacarlo
                        if (ticket.descExpanded.Contains("ENDO:"))
                        {
                            string fullStr = ticket.descExpanded;
                            ticket.descExpanded = ticket.descExpanded.Substring(0, ticket.descExpanded.IndexOf("ENDO"));
                            string endo = fullStr.Substring(fullStr.IndexOf("ENDO") + 5).Trim();
                            
                            Regex regex = new Regex(Configuration.getInstance().emdPrintEndoPattern);
                            Match match = regex.Match(ticket.descExpanded);

                            if (match.Success)
                            {
                                encabezado.Add("Info: " + endo);
                            }
                        }
                        
                        encabezado.Add("Desc: " + ticket.descExpanded);
                        
                        if (ticket.descExpanded.Contains("FEE"))
                        {
                            isItinWithFee = true;
                        }
                        
                    }

                    if ( ticket.from != null && ticket.from != "")
                    {
                        encabezado.Add( "From: " + ticket.from);
                    }

                    if ( ticket.agentDesc != null && ticket.agentDesc != "" && ticket.issueDate != null && ticket.issueDate != "")
                        encabezado.Add(devolverConPadding("Agent:" + ticket.agentDesc , "Issue Date:" + ticket.issueDate));
                }
                
                foreach (FOP fop in ticket.fops)
                {
                    auxTktStr = fop.descripcion.Trim();
                    if (auxTktStr.Length > 1)
                    {
                        string result = formatCreditNumber(auxTktStr);
                        if (tktType.Equals("TK") && (ticket.descripcion.Contains("0440490") || ticket.descripcion.Contains("0440480")))
                        {
                            result = Regex.Replace(result, "(\\s+)([\\d,.]+\\s+)", "$1-$2");
                        }

                        result = Regex.Replace(result, "CUO\\\\:", "C");

                        if ( !ticketWithTourCode)
                            encabezado.AddRange(getStringForCupon(result).Split('|'));
                        else
                        {
                            // SAcamos el precio si es con tour code
                            string pattern = "(.*)\\s\\d+\\.\\d{2}\\s*\\w{3}(.*)";
                            string replacement = "$1$2";
                            Regex rgx = new Regex(pattern);
                            result = rgx.Replace(result, replacement);
                            encabezado.AddRange(getStringForCupon(result).Split('|'));
                        }


                    }
                    else
                    {
                        continue;
                    }

                    if (!(tktType.Equals("TK") && (ticket.descripcion.Contains("0440490") || ticket.descripcion.Contains("0440480"))))
                    {
                        auxTktStr = Regex.Replace(auxTktStr, "CUO\\\\:", "C");
                        string[] arrayFop = auxTktStr.Split(' ');
                        decimal value = 0;
                        string key = "";

                        if (!auxTktStr.Contains("ERROR")) {


                            key = arrayFop[0] + " [" + fop.currency + "]";

                            // agregamos a la key el app code si tiene
                            if (arrayFop.Length >= 4 && arrayFop[3].Contains("AP"))
                            {
                                key = arrayFop[0] + " " + arrayFop[3] + " [" + fop.currency + "]";
                            }

                            // si tiene ID VTOL
                            Regex rgx = new Regex(@"\(\w+\)");
                            if ( rgx.IsMatch(auxTktStr) )
                            {
                                key = "VTOL ID: " + arrayFop[3];
                            }

                            if ( ! (ticket.status!= null && ticket.status.Equals("RFND") )) { 
                                if (!mapFop.TryGetValue(key, out value))
                                {
                                    mapFop[key] = decimal.Parse(arrayFop[1] , CultureInfo.InvariantCulture);
                                }
                                else
                                {
                                    mapFop[key] = decimal.Parse(arrayFop[1], CultureInfo.CreateSpecificCulture("en-US")) + value;
                                }
                            }
                        }
                    }
                    else
                    {
                        string[] arrayFop = auxTktStr.Split(' ');
                        decimal value = 0;
                        string key = arrayFop[0] + " [" + fop.currency + "]";

                        if (!mapFopRefund.TryGetValue(key, out value))
                        {
                            mapFopRefund[key] = decimal.Parse(arrayFop[1], CultureInfo.InvariantCulture);
                        }
                        else
                        {
                            mapFopRefund[key] = decimal.Parse(arrayFop[1], CultureInfo.InvariantCulture) + value;
                        }
                    }

                }
                addSpace(encabezado, 1);
            }


            // ITINERARIO CON FEE
            if ( isItinWithFee )
            {
                addSpace(pie, 1);
                encabezado.Add("_____________________________________________\n" + ESCPOS.justificacionIzquierda);
                addSpace(encabezado, 1);
                encabezado.Add("Itinerario con FEE" + ESCPOS.fontBold + ESCPOS.justificacionCentro);
                addSpace(encabezado, 1);
                encabezado.Add("_____________________________________________\n" + ESCPOS.justificacionIzquierda);
            }


            // TOTALES DE COBRADO

            if (!isItinWithVoid) {

                bool isOnlyET = true;
                foreach (string key in mapFop.Keys)
                {
                    decimal value = 0;
                    if (mapFop.TryGetValue(key, out value))
                    {
                        string result = key;
                        if (result != null && !result.StartsWith("ET"))
                            isOnlyET = false;

                    }
                }

                if ( mapFop.Keys.Count >= 1 && !isOnlyET) { 
                    encabezado.Add("_____________________________________________\n" + ESCPOS.justificacionIzquierda);
                    addSpace(encabezado, 1);
                    encabezado.Add("Totales:\n" + ESCPOS.justificacionIzquierda);
                    addSpace(encabezado, 1);
                }

                foreach ( string key in mapFop.Keys)
                {
                    decimal value = 0;
                    if (mapFop.TryGetValue(key, out value))
                    {
                        string result = formatCreditNumber(key);
                        // Obviamos el canje en los Totales
                        if ( result!= null && !result.StartsWith("ET")) {

                            Regex rgx = new Regex("(.*)\\[([^]]*)");
                            string currency = "ARS";
                            if (rgx.IsMatch(result))
                            {
                                string auxKey = result;
                                currency = rgx.Match(auxKey).Groups[2].Value;
                                result = rgx.Match(auxKey).Groups[1].Value;
                            }

                            string totalStr = result + ":" + Math.Round(value, 2) + " " + currency;
                            encabezado.AddRange(getStringForCupon(totalStr , ESCPOS.fontBold ).Split('|'));
                        }

                    }
                }

            
                // TOTALES DE REFUND
                if (mapFopRefund.Keys.Count >= 1)
                {
                    addSpace(pie, 1);
                    encabezado.Add("_____________________________________________\n" + ESCPOS.justificacionIzquierda);
                    addSpace(encabezado, 1);
                    encabezado.Add("Totales Devoluciones:\n" + ESCPOS.justificacionIzquierda);
                    addSpace(encabezado, 1);
                }

                foreach (string key in mapFopRefund.Keys)
                {
                    decimal value = 0;
                    if (mapFopRefund.TryGetValue(key, out value))
                    {
                        string result = formatCreditNumber(key);
                        value = value * -1;

                        Regex rgx = new Regex("(.*)\\[([^]]*)");
                        string currency = "ARS";
                        if (rgx.IsMatch(result))
                        {
                            string auxKey = result;
                            currency = rgx.Match(auxKey).Groups[2].Value;
                            result = rgx.Match(auxKey).Groups[1].Value;
                        }

                        string totalStr = result + " : " + Math.Round(value, 2) + " " + currency;
                        encabezado.AddRange(getStringForCupon(totalStr, ESCPOS.fontBold).Split('|'));
                    }
                }
            }

            addSpace(pie, 1);
            pie.AddRange(getStringForCupon("Horario de presentación: Cabotaje: 2hrs y Regional/Internacional: 3 hrs, antes de horario de vuelo." , ESCPOS.fontBold ).Split('|'));
            addSpace(pie, 1);
            pie.AddRange( getStringForCupon ("Recuerde que le enviamos un mail con el detalle de la compra realizada, donde se discriminan por cada ticket, los conceptos, tasas e impuestos , el cual sirve como documento válido ante la AFIP. Pero, si su servicio se encuentra gravado con IVA, deberá solicitar la constancia de crédito Fiscal en nuestra web: aerolineas.com", ESCPOS.fontBold).Split('|'));
            //Si tu servicio se encuentra gravado con IVA, deberás solicitar la Constancia de Crédito Fiscal en nuestra pagina web
            addSpace(pie, 1);
            pie.Add("Términos y condiciones:");
            pie.Add("aerolineas.com/terminos");
            addSpace(pie, 1);
            pie.Add("Por favor mantenga alejado este cupón");
            pie.Add("de altas temperaturas.");
            addSpace(pie, 2);

            if (!Configuration.getInstance().tipoPrinter.Equals(TipoPrinter.EPSON))
            {
                pie.Add(ESCPOS.eCut);
            }
        }

        protected override string getNumeroDeFactura()
        {
            return "0000 - 00000000";
        }


        private string getTarjetaSabre(string bin , string codSabreToCheck)
        {
            string codSabre = null;

            if (isDebitCard(codSabreToCheck))
            {
                return codSabreToCheck;
            }

            if (mapBines.TryGetValue(bin, out codSabre))
            {
                return codSabre;
            }


            WebServiceBines wsPromo = new WebServiceBines(bin, 10);
            List<Promocion>  promos = WebResponseParser.parseXMLBines(wsPromo.getResponse());

            if (promos.Count > 0)
            {
                mapBines[bin] = promos[0].codTarjetaSabre;
                return promos[0].codTarjetaSabre;
            }
            else
            {
                return "CC";
            }

        }
        

        public static bool isDebitCard( string codSabre)
        {
            try
            {
                List<TarjetaCajero> tarjetasTraidas = EntityWriter.fetchTarjetas();
                if (tarjetasTraidas.Count > 0)
                {
                    
                    foreach (var item in tarjetasTraidas)
                    {
                        if ( item.codTarjetaSabre.Equals( codSabre) && item.tipo.Equals( TipoTarjeta.DEBITO))
                        {
                            return true;
                        }
                    }

                }
            }
            catch ( Exception)
            {
                return false;
            }

            return false;
        }

        private string formatCreditNumber ( string strNumber)
        {

            string result = strNumber;

            // Le pongo el Codigo de Sabre
            /*Regex rgx = new Regex("^(\\w{2})((\\d{4,6})[\\d*]+\\s+)");
            if (rgx.IsMatch(result))
            {
                string codSabre = getTarjetaSabre(rgx.Match(result).Groups[3].Value , rgx.Match(result).Groups[1].Value);
                result = rgx.Replace(result, codSabre + "$2");

            }*/

            // Enmarcaro la tarjeta
            Regex rgx = new Regex("(\\w{2}\\d{6})(\\d+)(\\d{4}.*)");
            if ( rgx.IsMatch(result)) { 
                string middleNumbers = rgx.Match(result).Groups[2].Value;
                middleNumbers = (middleNumbers.Length == 6) ? "******" : "*****";
                result = rgx.Replace(result, "$1" + middleNumbers + "$3");
            }

            // Cambio por MO
            /*rgx = new Regex("(MO\\d{2})(.*)(\\d{4})");
            if (rgx.IsMatch(result))
            {
                string middleNumbers = rgx.Match(result).Groups[2].Value;
                middleNumbers = (middleNumbers.Length == 6) ? "******" : "*****";
                result = rgx.Replace(result, "$1" + middleNumbers + "$3");
            }*/


            return result;
        }

    }
}
