using CodenameNightwing.BusinessLogic;
using System.Collections.Generic;
using CodenameNightwing.Varios;
using CodenameNightwing.Config;
using CodenameNightwing.Forms;

namespace CodenameNightwing.Printer
{
    public abstract class PrinterCupon
    {
        public const int CANT_CARACT_POR_FILA = 40;
        private bool isDuplicado = false;
        protected List<string> encabezado = new List<string>();
        protected List<string> infoTicket = new List<string>();
        protected List<string> infoTrans = new List<string>();
        protected List<string> pie = new List<string>();
        protected Transaccion trans;
        protected bool isOriginal = true;
        protected bool withEscPos = true;
        protected string nombreOperacion = "";
        protected EnumCopiaUOriginal copiaOriginal;



        public string getStringForCupon(string original)
        {

            if (original.Length > CANT_CARACT_POR_FILA)
            {
                string toMax = original.Substring(0, CANT_CARACT_POR_FILA);
                int lastSpace = toMax.LastIndexOf(' ');
                toMax = original.Substring(0, lastSpace);
                string moreMax = original.Substring(lastSpace + 1);
                return toMax + "|" + getStringForCupon(moreMax);
            }
            else
            {
                return original;
            }

        }

        public string getStringForCupon(string original, string escPosCommand)
        {

            if (original.Length > CANT_CARACT_POR_FILA)
            {
                string toMax = original.Substring(0, CANT_CARACT_POR_FILA);
                int lastSpace = toMax.LastIndexOf(' ');
                toMax = original.Substring(0, lastSpace);
                string moreMax = original.Substring(lastSpace + 1);
                return toMax + escPosCommand + "|" + getStringForCupon(moreMax, escPosCommand);
            }
            else
            {
                return original + escPosCommand;
            }

        }

        public void setNombreOperacion(string nombre)
        {
            this.nombreOperacion = nombre;
        }

        public abstract string getNombreOperacion();


        public string escAuthDenegadaZ(string codAuth)
        {
            if (codAuth == null)
            {
                return "";
            }

            codAuth = codAuth.Trim();
            switch (codAuth)
            {
                case "Z1":
                    return "";
                case "Z3":
                    return "";
                default:
                    return codAuth;
            }
        }

        public string escPosString(string escpos)
        {
            if (withEscPos)
            {
                return escpos;
            }
            else
            {
                return "";
            }

        }
        public void setEncabezado()
        {
            encabezado.Add(LogoPrinter.GetLogo());
            encabezado.Add(devolverConPadding(escPosString(ESCPOS.justificacionCentro) + trans.fecha.ToShortDateString(), trans.fecha.ToString("HH:mm:ss")));
            encabezado.Add(escPosString(ESCPOS.justificacionCentro) + ((trans.tarjeta.descripcion != null) ? trans.tarjeta.descripcion.ToUpperInvariant() : "TARJETA"));
            encabezado.Add(escPosString(ESCPOS.justificacionCentro) + getNombreOperacion());
            encabezado.Add("\n");
            encabezado.Add(escPosString(ESCPOS.justificacionCentro) + escPosString(ESCPOS.underlineStart) + "AEROLINEAS ARGENTINAS SA\n" + escPosString(ESCPOS.underlineEnd));
            encabezado.Add(escPosString(ESCPOS.justificacionCentro) + "AV. RAFAEL OBLIGADO S/N, T4, PISO 6");
            encabezado.Add(escPosString(ESCPOS.justificacionCentro) + "CIUDAD AUTONOMA DE BUENOS AIRES, CP 1425\n");
            if (trans.tipoHost.Equals(TipoHost.AMEX))
            {
                encabezado.Add(escPosString(ESCPOS.justificacionCentro) + "TELÉFONO: 0810-222-8652\n");
            }
            encabezado.Add(escPosString(ESCPOS.justificacionCentro) + "CUIT: 30-64140555-4");
            encabezado.Add("_____________________________________________\n" + escPosString(ESCPOS.justificacionIzquierda));
        }

        protected abstract void setInfoTicketInfoDelOriginal();

        protected abstract string getNumeroDeFactura();

        private void setInfoTicketRelieveOffline()
        {
            // Si es la copia para el comercio hacemos el Relieve si es OFFLINE
            if (this.copiaOriginal.Equals(EnumCopiaUOriginal.ORIGINAL) && (trans.modo == TipoModoTransaccion.OFFLINE || trans.tipoIngreso == TipoIngresoTarjeta.MANUAL) && trans.isAprobada())
            {
                addSpace(infoTrans, 30);
            }
        }

        protected void setInfoTicketTresUltLineas()
        {

            // RESPUESTA DEL HOST
            if (!trans.isAprobada() && escAuthDenegadaZ(trans.numAutorizacion).Equals(""))
            {
                infoTrans.Add(escPosString(ESCPOS.justificacionCentro) + "TARJETA RECHAZADA-" + trans.numAutorizacion);
            }

            switch (trans.tipoHost)
            {
                case TipoHost.VISA:
                case TipoHost.FIRST_DATA:

                    infoTrans.Add(escPosString(ESCPOS.justificacionCentro) + this.trans.respuestaHost + "\n");

                    string numFactura = getNumeroDeFactura();
                    if (numFactura != null && trans.isAprobada())
                    {
                        infoTrans.Add(escPosString(ESCPOS.justificacionIzquierda) + "Nro. de Factura: " + numFactura);
                    }

                    break;
                case TipoHost.AMEX:

                    string numFacturaAmex = getNumeroDeFactura();
                    if (numFacturaAmex != null && trans.isAprobada())
                    {
                        infoTrans.Add(devolverConPadding("NUMERO FACTURA", numFacturaAmex));
                    }
                    infoTrans.Add(escPosString(ESCPOS.justificacionCentro) + this.trans.respuestaHost + "\n");

                    break;
            }

            setInfoTicketRelieveOffline();

        }

        protected void addSpace(List<string> list, int size)
        {
            for (int i = 0; i < size; i++)
            {
                list.Add("\n");
            }

        }

        protected void setInfoTicketCuatroPriLineas()
        {
            switch (trans.tipoHost)
            {
                case TipoHost.VISA:
                case TipoHost.FIRST_DATA:

                    infoTicket.Add(devolverConPadding("Nro. Com: " + trans.comercio.codigoComercio, "Term: " + ((trans.nroTerminal != null) ? trans.nroTerminal : "")));
                    infoTicket.Add(devolverConPadding("Nro. de Lote: " + trans.numLote, "Nro. de Cargo: " + ((trans.numTicket != null) ? trans.numTicket : "")));
                    if (trans.tipoHost.Equals(TipoHost.VISA))
                    {
                        infoTicket.Add(devolverConPadding("xxxxxxxxxxxx" + trans.tarjeta.ultimos4() + "(" + EnumUtils.getDescripcionTipoIngresoTarjeta(trans.tipoIngreso) + ")", " **/**"));
                    }
                    else
                    {
                        infoTicket.Add(devolverConPadding(trans.tarjeta.primeros6() + "xxxxxx" + trans.tarjeta.ultimos4() + "(" + EnumUtils.getDescripcionTipoIngresoTarjeta(trans.tipoIngreso) + ")", " **/**"));
                    }

                    string encabezadoModo = (this.trans.tipoHost.Equals(TipoHost.FIRST_DATA)) ? "Autoriz. (Modo):" : "";
                    infoTicket.Add(devolverConPadding(encabezadoModo + (trans.modo == TipoModoTransaccion.ONLINE ? "Online" : (trans.modo == TipoModoTransaccion.OFFLINE ? "Offline" : "Offhost")), "Autoriz.: " + escAuthDenegadaZ(trans.numAutorizacion)));
                    if (trans.tarjeta.isFirstDataDebit())
                    {
                        infoTicket.Add("Tipo Cuenta: " + EnumUtils.getDescriptionFromTipoCuenta(trans.tarjeta.tipoCuentaDebito));
                    }
                    if (trans.numCuenta != null)
                    {
                        infoTicket.Add("Nro Cuenta: " + trans.numCuenta);
                    }
                    break;
                case TipoHost.AMEX:

                    infoTicket.Add(devolverConPadding("NUM. COM: " + trans.comercio.codigoComercio, "TERM: " + trans.nroTerminal));
                    infoTicket.Add(devolverConPadding("LOTE: " + trans.numLote, "CARGO: " + trans.numTicket));
                    infoTicket.Add(devolverConPadding("AMEX", (trans.modo == TipoModoTransaccion.ONLINE ? "ON-LINE" : (trans.modo == TipoModoTransaccion.OFFLINE ? "OFF-LINE" : "OFF-HOST"))));
                    infoTicket.Add(devolverConPadding("xxxxxxxxxxxx" + trans.tarjeta.ultimos4() + "(" + EnumUtils.getDescripcionTipoIngresoTarjetaAMEX(trans.tipoIngreso) + ")", " Vto.: **/**"));

                    break;
            }

            if (trans.tipoIngreso == TipoIngresoTarjeta.EMV && trans.AID != null)
            {
                if (trans.tipoHost.Equals(TipoHost.AMEX) && trans.appNameAMEX != null && trans.appNameAMEX != "")
                {
                    infoTicket.Add(trans.appNameAMEX);
                    infoTicket.Add(trans.AID);
                }
                else
                {
                    if (trans.appNameAMEX != null)
                        infoTicket.Add(trans.appNameAMEX);
                    infoTicket.Add("AID: " + trans.AID);
                }

            }

            addSpace(infoTicket, 1);

        }

        protected void setInfoTrans()
        {

            string impTotal = trans.importeTotal.ToString("######0.00").Replace(",", ".");
            string descPlan = trans.getDescripcionPlan();

            switch (trans.tipoHost)
            {
                case TipoHost.VISA:
                case TipoHost.FIRST_DATA:
                    infoTrans.Add(trans.codSoftAMEX);
                    addSpace(infoTicket, 1);
                    infoTrans.Add(devolverConPadding("Imp. Total: -$- " + impTotal, "Cuotas: " + trans.cantCuotas));
                    if (!string.IsNullOrEmpty(trans.tarjeta.codPlan))
                    {
                        if (!string.IsNullOrEmpty(trans.tarjeta.codPlan.Trim()))
                        {
                            if (descPlan != null)
                            {
                                infoTrans.Add("Plan: " + descPlan);
                            }
                            else
                            {
                                infoTrans.Add("Tipo de Plan: " + trans.tarjeta.codPlan);
                            }
                        }

                    }

                    break;

                case TipoHost.AMEX:

                    infoTrans.Add(devolverConPadding((trans.codSoftAMEX == null) ? "" : trans.codSoftAMEX, "AUT.:" + escAuthDenegadaZ(trans.numAutorizacion)));
                    addSpace(infoTicket, 1);
                    infoTrans.Add(devolverConPadding("TOTAL", "$ " + impTotal));

                    if (string.IsNullOrEmpty(descPlan))
                    {
                        descPlan = "";
                    }
                    infoTrans.Add(devolverConPadding("CUOTAS: " + trans.cantCuotas, descPlan));

                    break;
            }

            addSpace(infoTrans, 2);

        }

        protected void setPie()
        {
            if (trans.tipoHost.Equals(TipoHost.AMEX) && trans.tipoIngreso.Equals(TipoIngresoTarjeta.EMV))
            {
                pie.Add(ESCPOS.justificacionCentro + "Verificado por PIN");
            }

            string planesAhora = PlanesAHORA.getCuotasMostrar(trans.cantCuotas);
            if (planesAhora != null && planesAhora.Contains("AH"))
            {
                addSpace(pie, 1);
                pie.Add(ESCPOS.justificacionCentro + "Plan " + planesAhora.Replace("AH", "AHORA "));
            }

            addSpace(pie, 3);

            if (this.copiaOriginal.Equals(EnumCopiaUOriginal.ORIGINAL))
            {
                pie.Add("Firma:      _________________________________\n");

                if (trans.tarjeta.owner.nombre != null && !trans.tarjeta.owner.nombre.Trim().Equals("") && Configuration.getInstance().printPaxName)
                {
                    pie.Add(escPosString(ESCPOS.justificacionCentro) + trans.tarjeta.owner.nombre.Trim());
                }
                else
                {
                    if (Configuration.getInstance().tipoPrinter.Equals(TipoPrinter.EPSON))
                    {
                        addSpace(pie, 5);
                    }
                    pie.Add(escPosString(ESCPOS.justificacionIzquierda) + "Aclaración: _________________________________");
                }
                pie.Add(escPosString(ESCPOS.justificacionIzquierda));
                addSpace(pie, 1);
                pie.Add("Nro. Doc.: " + trans.tarjeta.owner.documento);
                addSpace(pie, 1);
            }

            addSpace(pie, 1);

            pie.Add("Firmo en conformidad con las condiciones");
            pie.Add("de tarifas y equipaje permitido");
            pie.Add("respecto al servicio adquirido.");
            addSpace(pie, 1);
            pie.Add("Si tu servicio se encuentra gravado con");
            pie.Add("IVA, deberás solicitar la Constancia");
            pie.Add("de Crédito Fiscal en nuestra web.");
            //Si tu servicio se encuentra gravado con IVA, deberás solicitar la Constancia de Crédito Fiscal en nuestra pagina web
            addSpace(pie, 1);
            pie.Add("Por reclamos o consultas acerca de los");
            pie.Add("servicios adquiridos y/o prestados:");
            pie.Add("Aerolineas.com/servicioscliente.");
            addSpace(pie, 1);
            pie.Add("Por favor mantenga alejado este cupón ");
            pie.Add("de altas temperaturas.");
            addSpace(pie, 1);
            pie.Add(escPosString(ESCPOS.justificacionCentro) + "Visite www.aerolineas.com.ar");
            addSpace(pie, 2);
        }


        public string completeWithAsterisk(string line)
        {
            int cantAst = (CANT_CARACT_POR_FILA - line.Length) / 2;
            string devolver = "";
            if (cantAst > 0)
                devolver = devolver.Insert(0, new string('*', cantAst));
            devolver += line;
            if (cantAst > 0)
                devolver = devolver.Insert(devolver.Length, new string('*', cantAst));
            return devolver;
        }

        public string devolverConPadding(string inicio, string fin)
        {

            inicio = (inicio == null) ? "" : inicio;
            fin = (fin == null) ? "" : fin;

            int cantEspaciosAInsertar = CANT_CARACT_POR_FILA - (inicio.Length + fin.Length);
            string devolver = "";
            devolver += inicio;
            if (cantEspaciosAInsertar > 0)
                devolver = devolver.Insert(inicio.Length, new string(' ', cantEspaciosAInsertar));
            devolver += fin;
            return devolver;
        }
        private void setCopiaUOriginal(EnumCopiaUOriginal cuo)
        {
            if (cuo == EnumCopiaUOriginal.ORIGINAL)
                pie.Add(escPosString(ESCPOS.justificacionCentro) + "ORIGINAL - COMERCIO");
            else
                pie.Add(escPosString(ESCPOS.justificacionCentro) + "COPIA - CLIENTE");
            addSpace(pie, 4);
        }
        public string[] devolverCupon()
        {
            List<string> devolver = new List<string>();
            devolver.AddRange(encabezado);
            devolver.AddRange(infoTicket);
            devolver.AddRange(infoTrans);
            devolver.AddRange(pie);
            return devolver.ToArray();
        }
        public PrinterCupon(Transaccion aCrear, EnumCopiaUOriginal cuo) : this(aCrear, cuo, false, true)
        {

        }

        public PrinterCupon(Transaccion aCrear, EnumCopiaUOriginal cuo, bool isDupli, bool withEscPos)
        {
            this.isDuplicado = isDupli;
            this.withEscPos = withEscPos;
            this.copiaOriginal = cuo;

            trans = aCrear;
            setEncabezado();
            if (isDuplicado)
            {
                encabezado.Add(completeWithAsterisk("DUPLICADO") + "\n" + escPosString(ESCPOS.justificacionIzquierda));
                encabezado.Add("_____________________________________________\n" + escPosString(ESCPOS.justificacionIzquierda));
            }
            setInfoTicketCuatroPriLineas();
            if (!this.trans.tipoHost.Equals(TipoHost.AMEX))
            {
                setInfoTicketInfoDelOriginal();
            }

            if (trans.trxIdVtolUnico != null)
                infoTicket.Add("Nro. Unico: " + trans.trxIdVtolUnico);

            string caja = "";
            if (Configuration.getInstance().caja != null && !Configuration.getInstance().caja.Equals(""))
            {
                caja = "Caja: " + Configuration.getInstance().caja;
            }


            infoTicket.Add(devolverConPadding(ESCPOS.fontBold + "Nro. Unico AR: " + trans.trxReferenceId.ToString().ToBase36String(), caja));
            addSpace(infoTicket, 1);
            setInfoTrans();
            setInfoTicketTresUltLineas();

            if (aCrear.isAprobada())
            {
                setPie();
                setCopiaUOriginal(this.copiaOriginal);
            }

            if (!Configuration.getInstance().tipoPrinter.Equals(TipoPrinter.EPSON))
            {
                pie.Add(ESCPOS.eCut);
            }

            if (cuo.Equals(EnumCopiaUOriginal.COPIA))
            {
                this.isOriginal = false;
            }

        }

        public PrinterCupon(bool testing)
        {

            if (testing)
            {
                encabezado.Add(LogoPrinter.GetLogo());
                encabezado.Add("_____________________________________________\n" + escPosString(ESCPOS.justificacionIzquierda));
                encabezado.Add("PAGINA DE PRUEBA\n" + escPosString(ESCPOS.justificacionIzquierda));
                encabezado.Add("_____________________________________________\n" + escPosString(ESCPOS.justificacionIzquierda));

                addSpace(pie, 3);
                if (!Configuration.getInstance().tipoPrinter.Equals(TipoPrinter.EPSON))
                {
                    pie.Add(escPosString(ESCPOS.eCut));
                }
            }

        }
    }
}
