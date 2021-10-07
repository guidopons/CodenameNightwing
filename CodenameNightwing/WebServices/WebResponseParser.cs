﻿using CodenameNightwing.Autorization;
using CodenameNightwing.BusinessLogic;
using CodenameNightwing.Printer;
using CodenameNightwing.Sabre;
using CodenameNightwing.Varios;
using CodenameNightwing.WebServices.Response;
using log4net;
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó mediante una herramienta.
//     Los cambios del archivo se perderán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using System.Xml;

namespace CodenameNightwing.WebServices
{
    /// <summary>
    /// Es estatica
    /// </summary>
    public static class WebResponseParser
    {

        public static readonly ILog logger = LogManager.GetLogger(typeof(WebResponseParser));

        public static String parseXMLSessionCreateRS(string aParsear)
        {
            String token = null;
            try
            {
                XmlDocument parser = new XmlDocument();
                parser.LoadXml(aParsear);
                if (parser.GetElementsByTagName("wsse:BinarySecurityToken")[0].InnerText != "")  {
                    token = parser.GetElementsByTagName("wsse:BinarySecurityToken")[0].InnerText;
                }
            }
            catch (Exception e)
            {
                logger.Error("parseXMLSessionCreateRS: " + aParsear);
                throw e;
            }
            return token;
        }

        //Busca el RecordLocator en la RTA del WS
        public static void parseXMLGetReservationRS(string aParsear, string pnr)
        {
            try
            {
                XmlDocument parser = new XmlDocument();
                parser.LoadXml(aParsear);
                if (parser.GetElementsByTagName("stl19:RecordLocator")[0].InnerText != "")
                {
                    string pnrRS = parser.GetElementsByTagName("stl19:RecordLocator")[0].InnerText;
                    if (pnrRS.Equals(pnr))
                    {
                        logger.Info("parseXMLGetReservationRS OK. PNR encontrado: " + pnr);
                    }
                    else
                    {
                        logger.Error("parseXMLGetReservationRS - PNR no encontrado:" + pnr);
                        throw new Exception("PNR no encontrado " + pnr);
                    }
                }
                else {
                    logger.Error("parseXMLGetReservationRS - XML TAG incorrecto: stl19: RecordLocator" );
                    throw new Exception(" XML TAG incorrecto: stl19: RecordLocator ");

                }

            }
            catch (Exception e)
            {
                logger.Error("parseXMLGetReservationRS: " + aParsear);
                throw e;
            }
        }

        //Validar que tenga un * en la RTA
        public static void parseXMLSabreCommnandRS(string aParsear)
        {
            try
            {
                XmlDocument parser = new XmlDocument();
                parser.LoadXml(aParsear);

                if (parser.GetElementsByTagName("Response")[0].InnerText != "")  {
                   string responseCMD = parser.GetElementsByTagName("Response")[0].InnerText;
                    if (!responseCMD.Contains("*")) {
                        logger.Error("parseXMLSabreCommnandRS: Se esperaba *");
                        throw new Exception("parseXMLSabreCommnandRS: Se esperaba * ");
                    }
                    else
                        logger.Info("parseXMLSabreCommnandRS OK ");
                }
                else
                {
                    logger.Error("parseXMLSabreCommnandRS: XML TAG incorrecto: GetElementsByTagName(Response)");
                    throw new Exception("parseXMLSabreCommnandRS: XML TAG incorrecto: GetElementsByTagName(Response)");
                }
            }
            catch (Exception e)
            {
                logger.Error("parseXMLSabreCommnandRS: " + aParsear);
                throw e;
            }

        }

        //Validar que tenga un nodo stl:Success en la RTA
        public static void parseXMLSabreAddRemarkRS(string aParsear)
        {
            try
            {
                XmlDocument parser = new XmlDocument();
                parser.LoadXml(aParsear);

                if (parser.GetElementsByTagName("stl:Success").Count == 0)  {
                    logger.Error("parseXMLSabreAddRemarkRS: Se esperaba stl: Success");
                    throw new Exception("parseXMLSabreAddRemarkRS: Se esperaba stl: Success");
                }
                else
                    logger.Info("parseXMLSabreAddRemarkRS OK ");

            }
            catch (Exception e)
            {
                logger.Error("parseXMLSabreAddRemarkRS: " + aParsear);
                throw e;
            }
        }

        //Validar que tenga un OK en la RTA
        public static void parseXMLSabreEndTransactionRS(string aParsear)
        {
            try
            {
                String textRTA = null;
                XmlDocument parser = new XmlDocument();
                parser.LoadXml(aParsear);
                if (parser.GetElementsByTagName("Text")[0].InnerText != "") {
                    textRTA = parser.GetElementsByTagName("Text")[0].InnerText;

                    if (!textRTA.Contains("OK")) {
                        logger.Error("parseXMLSabreEndTransactionRS: Se esperaba OK");
                        throw new Exception("parseXMLSabreEndTransactionRS: Se esperaba OK ");
                    }
                    else
                        logger.Info("parseXMLSabreEndTransactionRS OK ");
                }
                else {
                    logger.Error("parseXMLSabreEndTransactionRS: XML TAG incorrecto: GetElementsByTagName(Text)");
                    throw new Exception("parseXMLSabreEndTransactionRS: XML TAG incorrecto: GetElementsByTagName(Text)");
                }

            }
            catch (Exception e)
            {
                logger.Error("parseXMLSabreEndTransactionRS: " + aParsear);
                throw e;
            }
        }

        //Validar que tenga un OK en la RTA
        public static void parseXMLSabreCloseSessionRS(string aParsear)
        {
            try
            {
                string sessionCloseRTA = null;
                XmlDocument parser = new XmlDocument();
                parser.LoadXml(aParsear);
                if (parser.GetElementsByTagName("eb:Action")[0].InnerText != "")  {
                    sessionCloseRTA = parser.GetElementsByTagName("eb:Action")[0].InnerText;
                    if (!sessionCloseRTA.Contains("SessionCloseRS"))
                    {
                        logger.Error("parseXMLSabreCloseSessionRS: Se esperaba TAG SessionCloseRS ");
                        throw new Exception("parseXMLSabreCloseSessionRS: Se esperaba TAG SessionCloseRS ");
                    }
                    else
                        logger.Info("parseXMLSabreCloseSessionRS OK ");
                }
                else {
                    logger.Error("parseXMLSabreCloseSessionRS: XML TAG incorrecto: eb:Action");
                    throw new Exception("parseXMLSabreEndTransactionRS: XML TAG incorrecto: eb:Action");
                }

            }
            catch (Exception e)
            {
                logger.Error("parseXMLSabreCloseSessionRS: " + aParsear);
                throw e;
            }
        }


        public static Comercio parseXMLComercio(string aParsear)
        {
            try
            {
                Comercio aux = new Comercio();
                XmlDocument parser = new XmlDocument();
                parser.LoadXml(aParsear);
                if (parser.GetElementsByTagName("status")[0].InnerText == "CODE_FOUNDED")
                {
                    aux.codigoComercio = parser.GetElementsByTagName("codComercio")[0].InnerText.Trim();
                    aux.codigoTarjetaTrans = parser.GetElementsByTagName("codNumTarjeta")[0].InnerText.Trim();
                }
                return aux;
            }
            catch (Exception e)
            {
                logger.Error("Error en el parseo de respuesta de WS comercio", e);
                MessageBox.Show("REINICIAR LA PC. Error en el parseo de respuesta de WS comercio", "Error de parseo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new Comercio();
            }
        }

        public static bool parseXMLVerificarCuit(string aParsear)
        {
            try
            {
                XmlDocument parser = new XmlDocument();
                parser.LoadXml(aParsear);
                bool respuesta = false;
                if (parser.GetElementsByTagName("valid")[0].InnerText == "true")
                    respuesta = true;
                else
                    respuesta = false;
                return respuesta;
            }
            catch (Exception e)
            {
                logger.Error("Error en el parseo de respuesta de WS verificar cuit", e);
                MessageBox.Show("REINICIAR LA PC. Error en el parseo de respuesta de WS verificar cuit", "Error de parseo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static string parseXMLObtenerCuit(string aParsear)
        {
            try
            {
                XmlDocument parser = new XmlDocument();
                parser.LoadXml(aParsear);
                string respuesta = "";
                if (parser.GetElementsByTagName("status")[0].InnerText == "CUIT_CREATED")
                    respuesta = parser.GetElementsByTagName("cuit")[0].InnerText.Trim();
                else
                    respuesta = "00-00000000-0";
                return respuesta;
            }
            catch (Exception e)
            {
                logger.Error("Error en el parseo de respuesta de WS obtener cuit", e);
                MessageBox.Show("REINICIAR LA PC. Error en el parseo de respuesta de WS obtener cuit", "Error de parseo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }
        }

        public static List<TarjetaCajero> parseXMLTarjetas(string aParsear)
        {
            try
            {
                List<TarjetaCajero> respuesta = new List<TarjetaCajero>();
                XmlDocument parser = new XmlDocument();
                parser.LoadXml(aParsear);
                for (int i = 0; i < parser.GetElementsByTagName("codComercio").Count; i++)
                {
                    TipoTarjeta auxTT;
                    switch (parser.GetElementsByTagName("tipoCC")[i].InnerText)
                    {
                        case "CC":
                            auxTT = TipoTarjeta.CREDITO;
                            break;
                        case "CK":
                            auxTT = TipoTarjeta.DEBITO;
                            break;
                        default:
                            auxTT = TipoTarjeta.SUBLO;
                            break;
                    }
                    // Se agrega auxTA para tener el tipo de autorizador
                    TipoAutorizador auxTA;
                    switch (parser.GetElementsByTagName("tipoAparato")[i].InnerText)
                    {
                        case "ING":
                            auxTA = TipoAutorizador.POS_INGENICO;
                            break;
                        case "HAS":
                            auxTA = TipoAutorizador.HASAR;
                            break;
                        case "NPS":
                            auxTA = TipoAutorizador.NPS;
                            break;
                        default:
                            auxTA = TipoAutorizador.POS_INGENICO;
                            break;
                    }
                    respuesta.Add(new TarjetaCajero(parser.GetElementsByTagName("codComercio")[i].InnerText.Trim(),
                       parser.GetElementsByTagName("codNumTarjeta")[i].InnerText.Trim(),
                       parser.GetElementsByTagName("codTarjetaSabre")[i].InnerText.Trim(),
                       parser.GetElementsByTagName("descripcionTarjeta")[i].InnerText.Trim(),
                        /* Configuration.getInstance().environment == "QA" ?*/ parser.GetElementsByTagName("codPlan")[i].InnerText.Trim() /*: " "*/,
                       auxTT, auxTA));
                }
                return respuesta;
            }
            catch (Exception e)
            {
                logger.Error("Error en el parseo de respuesta de WS tarjetas", e);
                MessageBox.Show("REINICIAR LA PC. Error en el parseo de respuesta de WS tarjetas", "Error de parseo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<TarjetaCajero>();
            }
        }


        public static List<Promocion> parseXMLBines(string aParsear)
        {
            try
            {
                List<Promocion> lAux = new List<Promocion>();
                XmlDocument parser = new XmlDocument();
                parser.LoadXml(aParsear);
                bool esExcepcion = Convert.ToBoolean(parser.GetElementsByTagName("hasException")[0].InnerText.Trim());
                int idPromo = 1;
                for (int i = 0; i < parser.GetElementsByTagName("cuotas").Count; i++)
                {
                    decimal auxPorcentaje = Convert.ToDecimal(parser.GetElementsByTagName("porcentaje")[i].InnerText.Trim(), CultureInfo.InvariantCulture);
                    int cuotas = int.Parse(parser.GetElementsByTagName("cuotas")[i].InnerText.Trim());
                    if ( cuotas != 0)
                        lAux.Add(new Promocion(idPromo.ToString(), "Cuotas " + PlanesAHORA.getCuotasMostrar(cuotas), cuotas,
                           parser.GetElementsByTagName("banco")[i].InnerText.Trim(),
                           parser.GetElementsByTagName("codTarjeta")[i].InnerText.Trim(),
                           parser.GetElementsByTagName("codTarjetaSabre")[i].InnerText.Trim(),
                           DateTime.Parse(parser.GetElementsByTagName("fechaDesde")[i].InnerText.Trim()),
                           (parser.GetElementsByTagName("descripcionTarjeta")[i] == null)? parser.GetElementsByTagName("codTarjetaSabre")[i].InnerText.Trim() : parser.GetElementsByTagName("descripcionTarjeta")[i].InnerText.Trim(),
                           auxPorcentaje,
                           esExcepcion));
                        idPromo++;
                }
                return lAux.OrderBy(x => x.cuotas).ToList();
            }
            catch (Exception e)
            {
                logger.Error("Error en el parseo de respuesta de WS bines", e);
                MessageBox.Show("REINICIAR LA PC. Error en el parseo de respuesta de WS bines", "Error de parseo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<Promocion>();
            }
        }


        public static TerminalResponse parseXMLInsertTerminal(string aParsear)
        {
            try
            {
                XmlDocument parser = new XmlDocument();
                parser.LoadXml(aParsear);

                TerminalResponse response = new TerminalResponse();
                string status = parser.GetElementsByTagName("status")[0].InnerText.Trim();
                response.status = status;
                if (status.Equals("TERMINAL_EXISTE"))
                {
                    Terminal terminal = new BusinessLogic.Terminal();
                    terminal.caja = parser.GetElementsByTagName("caja")[0].InnerText.Trim();
                    terminal.baseArsa = parser.GetElementsByTagName("base")[0].InnerText.Trim();
                    terminal.pinpadId = parser.GetElementsByTagName("pinpadId")[0].InnerText.Trim();
                    terminal.printerId = parser.GetElementsByTagName("printerId")[0].InnerText.Trim();
                    terminal.sucursal = parser.GetElementsByTagName("sucursal")[0].InnerText.Trim();
                    terminal.macAddress = parser.GetElementsByTagName("macAddress")[0].InnerText.Trim();
                    response.terminalEncontrada = terminal;

                }

                return response;
            }
            catch (Exception e)
            {
                logger.Error("Error en el parseo de respuesta de WS Terminales", e);
                MessageBox.Show("REINICIAR LA PC. Error en el parseo de respuesta de WS Terminales", "Error de parseo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public static TarjetaCajero parseXMLExceptionBin(string aParsear)
        {
            try
            {
                XmlDocument parser = new XmlDocument();
                parser.LoadXml(aParsear);
                TarjetaCajero respuesta = new TarjetaCajero(parser.GetElementsByTagName("codComercio")[0].InnerText.Trim(),
                    parser.GetElementsByTagName("codNumTarjeta")[0].InnerText.Trim(),
                    parser.GetElementsByTagName("codTarjeta")[0].InnerText.Trim(),
                    parser.GetElementsByTagName("descripcionTarjeta")[0].InnerText.Trim(),
                    string.IsNullOrEmpty(parser.GetElementsByTagName("plan")[0].InnerText) ? " " : parser.GetElementsByTagName("plan")[0].InnerText,
                    string.Compare(parser.GetElementsByTagName("tipoTarjeta")[0].InnerText.Trim(), "C", true) == 0 ? TipoTarjeta.CREDITO : TipoTarjeta.DEBITO,
                    Config.Configuration.getInstance().tipoAuth);
                return respuesta;
            }
            catch (Exception e)
            {
                logger.Error("Error en el parseo de respuesta de WS bines", e);
                MessageBox.Show("REINICIAR LA PC. Error en el parseo de respuesta de WS bines", "Error de parseo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public static List<Pais> parseXMLPaises(string aParsear)
        {
            try
            {
                List<Pais> respuesta = new List<Pais>();
                XmlDocument parser = new XmlDocument();
                parser.LoadXml(aParsear);
                for (int i = 0; i < parser.GetElementsByTagName("countryCode").Count; i++)
                {
                    respuesta.Add(new Pais(parser.GetElementsByTagName("countryCode")[i].InnerText.Trim(),
                        parser.GetElementsByTagName("countryDescription")[i].InnerText.Trim()));
                }
                return respuesta;
            }
            catch (Exception e)
            {
                logger.Error("Error en el parseo de respuesta de WS paises", e);
                MessageBox.Show("REINICIAR LA PC. Error en el parseo de respuesta de WS paises", "Error de parseo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<Pais>();
            }
        }

        public static int parseXMLInsert(string aParsear)
        {
            try
            {
                XmlDocument parser = new XmlDocument();
                parser.LoadXml(aParsear);
                return Convert.ToInt32(parser.GetElementsByTagName("idTrxReferencia")[0].InnerText); ;
            }
            catch (Exception e)
            {
                logger.Error("Error en el parseo de respuesta de WS insert transaccion", e);
                MessageBox.Show("REINICIAR LA PC. Error en el parseo de respuesta de WS insert transaccion", "Error de parseo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -3;
            }
        }

        public static bool parseXMLAnularTransaccion(string aParsear)
        {
            try
            {
                XmlDocument parser = new XmlDocument();
                parser.LoadXml(aParsear);
                return Convert.ToBoolean(parser.GetElementsByTagName("anulado")[0].InnerText); ;
            }
            catch (Exception e)
            {
                logger.Error("Error en el parseo de respuesta de WS anular transaccion", e);
                MessageBox.Show("REINICIAR LA PC. Error en el parseo de respuesta de WS anular transaccion", "Error de parseo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }


        public static bool parseXMLDatosConfirmado(string aParsear)
        {
            try
            {
                XmlDocument parser = new XmlDocument();
                parser.LoadXml(aParsear);
                return Convert.ToBoolean(parser.GetElementsByTagName("updateDone")[0].InnerText); ;
            }
            catch (Exception e)
            {
                logger.Error("Error en el parseo de respuesta de WS Update Confirmado transaccion", e);
                MessageBox.Show("REINICIAR LA PC. Error en el parseo de respuesta de WS Update Confirmado transaccion", "Error de parseo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }


        public static string parseXMLGetConfiguration(string aParsear)
        {
            string resultado = null;
            try
            {
                XmlDocument parser = new XmlDocument();
                parser.LoadXml(aParsear);

                if (parser.GetElementsByTagName("status")[0].InnerText.Trim().Equals("NO_CONNECTION"))
                    throw new Exception("NO_CONNECTION WS");

                if (parser.GetElementsByTagName("confValue") != null)
                {
                    resultado = parser.GetElementsByTagName("confValue")[0].InnerText.Trim();
                    return resultado;
                }
                else
                {
                    String msg = "ID no encontrado con xml: " + aParsear;
                    logger.Error(msg);
                    return null;
                }
            }
            catch (Exception e)
            {
                logger.Error("Error en el parseo de respuesta  GetConfiguration", e);
                MessageBox.Show("REINICIAR LA PC. Error en el parseo de respuesta de WS GetConfiguration", "Error de parseo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //return null;
               // throw e;
                throw new Exception("Error en el parseo de respuesta  GetConfiguration - NO_CONNECTION WS");
            }
        }


        public static Voucher parseXMLGetDescripcionVoucher(string aParsear)
        {
            Voucher resultado = null;
            try
            {
                XmlDocument parser = new XmlDocument();
                parser.LoadXml(aParsear);

                if (parser.GetElementsByTagName("idVoucher") != null)
                {
                    resultado = new BusinessLogic.Voucher();
                    resultado.idVoucher = parser.GetElementsByTagName("idVoucher")[0].InnerText.Trim();
                    resultado.descEspanol = parser.GetElementsByTagName("descEspanol")[0].InnerText.Trim();
                    resultado.descIngles = parser.GetElementsByTagName("descIngles")[0].InnerText.Trim();

                    return resultado;
                }
                else
                {
                    String msg = "Voucher no encontrado con xml: " + aParsear;
                    logger.Error(msg);
                    return null;
                }
            }
            catch (Exception e)
            {
                logger.Error("Error en el parseo de respuesta  get Descriptcion Voucher", e);
                MessageBox.Show("REINICIAR LA PC. Error en el parseo de respuesta de WS get Descriptcion Voucher", "Error de parseo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public static Transaccion parseXMLGetTransaction(string aParsear)
        {
            Transaccion resultado = null;
            try
            {
                XmlDocument parser = new XmlDocument();
                parser.LoadXml(aParsear);
                string estado = parser.GetElementsByTagName("outputMsg")[0].InnerText.Trim();
                if (estado.StartsWith("OK") ||  estado.StartsWith("REGISTRO ANULADO") )
                {
                    TipoTransaccion auxTipoTrans = setTransaccion(parser.GetElementsByTagName("codigo_tipo_transaccion")[0].InnerText.Trim());
                    TipoAutorizador auxTipoAuth = setAutorizador(parser.GetElementsByTagName("codigo_autorizador")[0].InnerText.Trim());
                    string sTipoIngreso = parser.GetElementsByTagName("codigo_tipo_ingreso")[0].InnerText.Trim();
                    TipoIngresoTarjeta auxTipoIngreso = setTipoIngreso( sTipoIngreso);
                    resultado = new TransaccionBuilder(auxTipoTrans, auxTipoAuth);
                    resultado.trxReferenceId = Convert.ToInt32(parser.GetElementsByTagName("id_trx_referencia")[0].InnerText);
                    resultado.pdv.sucursal = parser.GetElementsByTagName("id_sucursal")[0].InnerText;
                    resultado.pdv.caja = parser.GetElementsByTagName("id_caja")[0].InnerText;
                    resultado.trxId = parser.GetElementsByTagName("id_transaccion")[0].InnerText;
                    string sAuxFecha = parser.GetElementsByTagName("fecha")[0].InnerText;
                    resultado.fecha = new DateTime(Convert.ToInt32(sAuxFecha.Substring(0, 4)), Convert.ToInt32(sAuxFecha.Substring(4, 2)), Convert.ToInt32(sAuxFecha.Substring(6, 2)),
                        Convert.ToInt32(sAuxFecha.Substring(9, 2)), Convert.ToInt32(sAuxFecha.Substring(11, 2)), Convert.ToInt32(sAuxFecha.Substring(13, 2)));
                    resultado.modo = parser.GetElementsByTagName("codigo_modo")[0].InnerText.Trim() == "1" ? TipoModoTransaccion.ONLINE : (parser.GetElementsByTagName("codigo_modo")[0].InnerText.Trim() == "0" ? TipoModoTransaccion.OFFLINE : TipoModoTransaccion.OFFHOST);
                    resultado.importeTotal = Convert.ToDecimal(parser.GetElementsByTagName("importe_total")[0].InnerText.Trim(), CultureInfo.InvariantCulture);
                    resultado.cantCuotas = Convert.ToInt32(parser.GetElementsByTagName("cant_cuotas")[0].InnerText.Trim());
                    resultado.numLote = Convert.ToInt32(parser.GetElementsByTagName("numero_lote")[0].InnerText.Trim());
                    resultado.numTicket = parser.GetElementsByTagName("numero_ticket")[0].InnerText.Trim();
                    resultado.numAutorizacion = parser.GetElementsByTagName("codigo_autorizacion")[0].InnerText.Trim();
                    resultado.comercio.codigoComercio = parser.GetElementsByTagName("codigo_comercio")[0].InnerText.Trim();
                    resultado.tarjeta.numero = parser.GetElementsByTagName("tarjeta_numero")[0].InnerText.Trim();
                    resultado.tarjeta.descripcion = parser.GetElementsByTagName("tarjeta_descripcion")[0].InnerText.Trim();
                    resultado.tarjeta.codPlan = parser.GetElementsByTagName("codigo_plan")[0].InnerText.Trim();
                    resultado.nroTerminal = parser.GetElementsByTagName("numero_terminal")[0].InnerText.Trim();
                    resultado.tarjeta.owner.nombre = parser.GetElementsByTagName("nombre_duenio_tarjeta")[0].InnerText.Trim();
                    resultado.isAnulado = estado.StartsWith("REGISTRO ANULADO");
                    resultado.isReversado = Boolean.Parse(parser.GetElementsByTagName("reversado")[0].InnerText.Trim());
                    string sAuxFechaOriginal = parser.GetElementsByTagName("fecha_original")[0].InnerText.Trim();
                    if (!string.IsNullOrEmpty(sAuxFechaOriginal))
                        resultado.fechaOriginal = new DateTime(Convert.ToInt32(sAuxFechaOriginal.Substring(0, 4)), Convert.ToInt32(sAuxFechaOriginal.Substring(4, 2)), Convert.ToInt32(sAuxFechaOriginal.Substring(6, 2)),
                            Convert.ToInt32(sAuxFechaOriginal.Substring(9, 2)), Convert.ToInt32(sAuxFechaOriginal.Substring(11, 2)), Convert.ToInt32(sAuxFechaOriginal.Substring(13, 2)));
                    string sucursalOriginal = parser.GetElementsByTagName("id_sucursal_original")[0].InnerText.Trim();
                    if (!string.IsNullOrEmpty(sucursalOriginal))
                        resultado.pdvOriginal.sucursal = sucursalOriginal;
                    string cajaOriginal = parser.GetElementsByTagName("id_caja_original")[0].InnerText.Trim();
                    if (!string.IsNullOrEmpty(cajaOriginal))
                        resultado.pdvOriginal.caja = cajaOriginal;
                    string ticketOriginal = parser.GetElementsByTagName("numero_ticket_original")[0].InnerText.Trim();
                    if (!string.IsNullOrEmpty(ticketOriginal))
                        resultado.ticketOriginal = ticketOriginal;
                    string trxIdOriginal = parser.GetElementsByTagName("id_transaccion_original")[0].InnerText.Trim();
                    if (!string.IsNullOrEmpty(trxIdOriginal))
                        resultado.trxIdOriginal = trxIdOriginal;
                    string tipoHost = parser.GetElementsByTagName("tipo_host")[0].InnerText.Trim();
                    if (!string.IsNullOrEmpty(tipoHost))
                        resultado.tipoHost = setTipoHost(tipoHost);
                    string interact_user = parser.GetElementsByTagName("interact_user")[0].InnerText.Trim();
                    if (!string.IsNullOrEmpty(interact_user))
                        resultado.interactUser = interact_user;
                    return resultado;
                }
                else
                {
                    String msg = "";
                    if (estado.StartsWith("REGISTRO ANULADO"))
                    {
                        msg = "El registro se encuentra anulado no puede realizar una devolución sobre él.";
                    }
                    else
                    {
                        msg = "Error en busqueda de numero unico";
                    }

                    logger.Error( msg );
                    return null;
                }
            }
            catch (Exception e)
            {
                logger.Error("Error en el parseo de respuesta de WS insert transaccion", e);
                MessageBox.Show("REINICIAR LA PC. Error en el parseo de respuesta de WS getNumeroUnico Hasar", "Error de parseo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public static List<Transaccion> parseXMLSearchTransaction(string aParsear)
        {
            List<Transaccion> lResultado = new List<Transaccion>();
            Transaccion resultado = null;
            try
            {
                XmlDocument parser = new XmlDocument();
                parser.LoadXml(aParsear);
                string estado = parser.GetElementsByTagName("outputMessage")[0].InnerText.Trim();
                if (estado.StartsWith("OK"))
                {
                    for (int i = 0; i < parser.GetElementsByTagName("id_trx_referencia").Count; i++)
                    {
                        TipoTransaccion auxTipoTrans = setTransaccion(parser.GetElementsByTagName("codigo_tipo_transaccion")[i].InnerText.Trim());
                        TipoAutorizador auxTipoAuth = setAutorizador(parser.GetElementsByTagName("codigo_autorizador")[i].InnerText.Trim());
                        string sTipoIngreso = parser.GetElementsByTagName("codigo_tipo_ingreso")[i].InnerText.Trim();
                        TipoIngresoTarjeta auxTipoIngreso = setTipoIngreso(sTipoIngreso);
                        resultado = new TransaccionBuilder(auxTipoTrans, auxTipoAuth);
                        resultado.tipoIngreso = auxTipoIngreso;
                        resultado.trxReferenceId = Convert.ToInt32(parser.GetElementsByTagName("id_trx_referencia")[i].InnerText);
                        resultado.pdv.sucursal = parser.GetElementsByTagName("id_sucursal")[i].InnerText;
                        resultado.pdv.caja = parser.GetElementsByTagName("id_caja")[i].InnerText;
                        resultado.trxId = parser.GetElementsByTagName("id_transaccion")[i].InnerText;
                        string sAuxFecha = parser.GetElementsByTagName("fecha")[i].InnerText;
                        resultado.fecha = new DateTime(Convert.ToInt32(sAuxFecha.Substring(0, 4)), Convert.ToInt32(sAuxFecha.Substring(4, 2)), Convert.ToInt32(sAuxFecha.Substring(6, 2)),
                            Convert.ToInt32(sAuxFecha.Substring(9, 2)), Convert.ToInt32(sAuxFecha.Substring(11, 2)), Convert.ToInt32(sAuxFecha.Substring(13, 2)));
                        resultado.modo = parser.GetElementsByTagName("codigo_modo")[i].InnerText.Trim() == "1" ? TipoModoTransaccion.ONLINE : (parser.GetElementsByTagName("codigo_modo")[i].InnerText.Trim() == "0" ? TipoModoTransaccion.OFFLINE : TipoModoTransaccion.OFFHOST);
                        resultado.importeTotal = Convert.ToDecimal(parser.GetElementsByTagName("importe_total")[i].InnerText.Trim(), CultureInfo.InvariantCulture);
                        resultado.cantCuotas = Convert.ToInt32(parser.GetElementsByTagName("cant_cuotas")[i].InnerText.Trim());
                        resultado.numLote = Convert.ToInt32(parser.GetElementsByTagName("numero_lote")[i].InnerText.Trim());
                        resultado.numTicket = parser.GetElementsByTagName("numero_ticket")[i].InnerText.Trim();
                        resultado.numAutorizacion = parser.GetElementsByTagName("codigo_autorizacion")[i].InnerText.Trim();
                        resultado.comercio.codigoComercio = parser.GetElementsByTagName("codigo_comercio")[i].InnerText.Trim();
                        resultado.tarjeta.numero = parser.GetElementsByTagName("tarjeta_numero")[i].InnerText.Trim();
                        resultado.tarjeta.descripcion = parser.GetElementsByTagName("tarjeta_descripcion")[i].InnerText.Trim();
                        resultado.tarjeta.codPlan = parser.GetElementsByTagName("codigo_plan")[i].InnerText.Trim();
                        resultado.nroTerminal = parser.GetElementsByTagName("numero_terminal")[i].InnerText.Trim();
                        resultado.tarjeta.owner.nombre = parser.GetElementsByTagName("nombre_duenio_tarjeta")[i].InnerText.Trim();

                        resultado.isAnulado = Convert.ToBoolean(parser.GetElementsByTagName("anulado")[i].InnerText.Trim());
                        resultado.isReversado = Convert.ToBoolean(parser.GetElementsByTagName("reversado")[i].InnerText.Trim());

                        string sAuxFechaOriginal = parser.GetElementsByTagName("fecha_original")[i].InnerText.Trim();
                        if (!string.IsNullOrEmpty(sAuxFechaOriginal))
                            resultado.fechaOriginal = new DateTime(Convert.ToInt32(sAuxFechaOriginal.Substring(0, 4)), Convert.ToInt32(sAuxFechaOriginal.Substring(4, 2)), Convert.ToInt32(sAuxFechaOriginal.Substring(6, 2)),
                                Convert.ToInt32(sAuxFechaOriginal.Substring(9, 2)), Convert.ToInt32(sAuxFechaOriginal.Substring(11, 2)), Convert.ToInt32(sAuxFechaOriginal.Substring(13, 2)));
                        string sucursalOriginal = parser.GetElementsByTagName("id_sucursal_original")[i].InnerText.Trim();
                        if (!string.IsNullOrEmpty(sucursalOriginal))
                            resultado.pdvOriginal.sucursal = sucursalOriginal;
                        string cajaOriginal = parser.GetElementsByTagName("id_caja_original")[i].InnerText.Trim();
                        if (!string.IsNullOrEmpty(cajaOriginal))
                            resultado.pdvOriginal.caja = cajaOriginal;
                        string ticketOriginal = parser.GetElementsByTagName("numero_ticket_original")[i].InnerText.Trim();
                        if (!string.IsNullOrEmpty(ticketOriginal))
                            resultado.ticketOriginal = ticketOriginal;
                        string trxIdOriginal = parser.GetElementsByTagName("id_transaccion_original")[i].InnerText.Trim();
                        if (!string.IsNullOrEmpty(trxIdOriginal))
                            resultado.trxIdOriginal = trxIdOriginal;
                        string tipoHost = parser.GetElementsByTagName("tipo_host")[i].InnerText.Trim();
                        if (!string.IsNullOrEmpty(tipoHost))
                            resultado.tipoHost = setTipoHost(tipoHost);
                        string interact_user = parser.GetElementsByTagName("interact_user")[i].InnerText.Trim();
                        if (!string.IsNullOrEmpty(interact_user))
                            resultado.interactUser = interact_user;
                        lResultado.Add(resultado);
                    }
                }
                else
                    MessageBox.Show(estado, "No hay registros", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return lResultado;
            }
            catch (Exception e)
            {
                logger.Error("Error en el parseo de respuesta de WS search transaction", e);
                MessageBox.Show("REINICIAR LA PC. Error en el parseo de respuesta de WS searchTransaction", "Error de parseo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }


        private static TipoIngresoTarjeta setTipoIngreso(string codigoTran)
        {
            switch (Convert.ToInt32(codigoTran))
            {
                case 1:
                    return TipoIngresoTarjeta.BAND;
                case 2:
                    return TipoIngresoTarjeta.EMV;
                case 3:
                    return TipoIngresoTarjeta.MANUAL;
                case 4:
                    return TipoIngresoTarjeta.FALLBACK;
                default:
                    return TipoIngresoTarjeta.BAND;
            }
        }
        private static TipoTransaccion setTransaccion(string codigoTran)
        {
            switch (Convert.ToInt32(codigoTran))
            {
                case 1:
                    return TipoTransaccion.COMPRA;
                case 2:
                    return TipoTransaccion.ANULACION;
                case 3:
                    return TipoTransaccion.DEVOLUCION;
                default:
                    return TipoTransaccion.NADA;
            }
        }

        private static TipoAutorizador setAutorizador(string codigoAutorizador)
        {
            switch (Convert.ToInt32(codigoAutorizador))
            {
                case 1:
                    return TipoAutorizador.POS_INGENICO;
                case 2:
                    return TipoAutorizador.HASAR;
                case 3:
                    return TipoAutorizador.VTOL;
                default:
                    return TipoAutorizador.NADA;
            }
        }

        private static TipoHost setTipoHost(string tipo_host)
        {
            switch (Convert.ToInt32(tipo_host))
            {
                case 1:
                    return TipoHost.VISA;
                case 2:
                    return TipoHost.FIRST_DATA;
                case 3:
                    return TipoHost.AMEX;
                default:
                    return TipoHost.VISA;
            }
        }
    }
}
