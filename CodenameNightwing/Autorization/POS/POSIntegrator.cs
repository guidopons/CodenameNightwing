using CodenameNightwing.Forms;
using CodenameNightwing.Autorization.POS.RespuestasPOS;
using CodenameNightwing.Autorization.POS.RespuestasPOS.Errores;
using CodenameNightwing.Autorization.POS.StructsComunicacionPOS;
using System;
using System.Threading;
using System.Windows.Forms;
using CodenameNightwing.BusinessLogic;
using CodenameNightwing.Config;

namespace CodenameNightwing.Autorization.POS
{
    public class POSIntegrator : Autorizator
    {
        private void abrirPuerto()
        {
            vpiComParams_t aux = new vpiComParams_t();
            aux.com = "COM" + Configuration.getInstance().posComPort;
            aux.baudRate = 19200;
            aux.byteSize = 8;
            aux.parity = Convert.ToByte('N');
            aux.stopBits = 1;
            NativeMethods.vpiOpenPort(ref aux);
        }

        public override string getGatewayName()
        {

            return "ING";
        }

        private void cerrarPuerto()
        {
            NativeMethods.vpiClosePort();
        }

        public override Tarjeta solicitarNumeroTarjeta(string tipoTrans, Transaccion trans)
        {
            vpiPurchaseIn_t paramIn = new vpiPurchaseIn_t();
            paramIn.Amount = "0";
            paramIn.Cuit = "0";
            paramIn.instalmentCount = "0";
            paramIn.issuerCode = "0";
            paramIn.linemode = 0;
            paramIn.merchantCode = "0";
            paramIn.merchantName = "0";
            paramIn.planCode = "0";
            paramIn.receiptNumber = "0";
            paramIn.tip = "0";
            vpiTrxOut_t paramOut = new vpiTrxOut_t(true);
            int timeout = 60;
            int res = 0;
            Thread antiLock = new Thread((ThreadStart)delegate
            {
                abrirPuerto();
                res = NativeMethods.vpiPurchase(ref paramIn, ref paramOut, ref timeout);
                while (res == (int)Resultados.VPI_TIMEOUT_EXP || res == (int)InicioTransaccion.VPI_INVALID_CARD)
                {
                    handleError(res);
                    res = NativeMethods.vpiPurchase(ref paramIn, ref paramOut, ref timeout);
                }
                cerrarPuerto();
            });
            antiLock.Start();
            antiLock.Join();
            if (res == (int)Resultados.VPI_OK || res == (int)InicioTransaccion.VPI_DIF_CARD)
            {
                Tarjeta devolver = new Tarjeta();
                devolver.numero = paramOut.panFirst6 + "xxxxxx" + paramOut.panLast4;
                return devolver;
            }
            else
            {
                handleError(res);
                return null;
            }
        }

        public override bool verificarConexion()
        {
            abrirPuerto();
            short aux = NativeMethods.vpiTestConnection();
            cerrarPuerto();
            if (aux == (int)Resultados.VPI_OK)
                return true;
            else
                return false;
        }

        public override bool reimprimirUltimoCupon()
        {
            abrirPuerto();
            short aux = NativeMethods.vpiPrintTicket();
            cerrarPuerto();
            if (aux == (int)Resultados.VPI_OK)
                return true;
            else
            {
                handleError(aux);
                return false;
            }
        }

        public override bool reimprimirCierreLote()
        {
            abrirPuerto();
            short aux = NativeMethods.vpiPrintBatchClose();
            cerrarPuerto();
            if (aux == (int)Resultados.VPI_OK)
                return true;
            else
            {
                handleError(aux);
                return false;
            }
        }

        public override Transaccion realizarTransaccion(Transaccion aComputar)
        {
            switch (aComputar.tipoTrans)
            {
                case TipoTransaccion.COMPRA:
                    return realizarPago(aComputar);
                case TipoTransaccion.ANULACION:
                    return realizarVoideo(aComputar);
                case TipoTransaccion.DEVOLUCION:
                    return realizarDevolucion(aComputar);
                case TipoTransaccion.CIERRE:
                    return realizarCierreLote(aComputar);
                default: return null;
            }
        }

        protected override Transaccion realizarPago(Transaccion paraPagar)
        {
            vpiPurchaseIn_t paramIn = new vpiPurchaseIn_t();
            paramIn.Amount = paraPagar.importeTotal.ToString("#####0.00").Replace(",", "");
            paramIn.Cuit = "00-00000000-0";
            paramIn.instalmentCount = paraPagar.cantCuotas.ToString();
            paramIn.issuerCode = paraPagar.tarjeta.codTarjeta;
            paramIn.linemode = (byte)paraPagar.modo;
            paramIn.merchantCode = paraPagar.comercio.codigoComercio;
            paramIn.merchantName = "AEROLINEAS ARGENTINAS";
            if (paraPagar.cantCuotas > 1)
                paramIn.planCode = paraPagar.tarjeta.codPlan;
            else
                paramIn.planCode = " ";
            paramIn.receiptNumber = "000000000000";
            paramIn.tip = "0";
            vpiTrxOut_t paramOut = new vpiTrxOut_t(true);
            int timeout = 60;
            int res = 0;
            Thread antiLock = new Thread((ThreadStart)delegate
                {
                    abrirPuerto();
                    res = NativeMethods.vpiPurchase(ref paramIn, ref paramOut, ref timeout);
                    while (res == (int)Resultados.VPI_TIMEOUT_EXP || res == (int)InicioTransaccion.VPI_INVALID_CARD)
                    {
                        handleError(res);
                        res = NativeMethods.vpiPurchase(ref paramIn, ref paramOut, ref timeout);
                    }
                    cerrarPuerto();
                });
            antiLock.Start();
            antiLock.Join();
            Transaccion resultado = new TransaccionBuilder(TipoTransaccion.COMPRA, TipoAutorizador.POS_INGENICO);
            if (res == (int)Resultados.VPI_OK || res == (int)CerrarTransaccion.VPI_ERR_PRINT)
            {
                if (HostCodesPOS.checkError(paramOut.hostRespCode))
                {
                    resultado = paraPagar;
                    resultado.tarjeta.numero = paramOut.panFirst6 + "xxxxxx" + paramOut.panLast4;
                    resultado.numTicket = paramOut.TicketNumber;
                    resultado.numAutorizacion = paramOut.AuthCode;
                    resultado.numLote = Convert.ToInt32(paramOut.BatchNumber);
                    resultado.fecha = Convert.ToDateTime(paramOut.date + " " + paramOut.time);
                    resultado.estado = paramOut.hostRespCode;
                    if (res == (int)CerrarTransaccion.VPI_ERR_PRINT)
                    {
                        handleError(res);
                        reimprimirUltimoCupon();
                    }
                }
                else
                    resultado = null;
            }
            else
            {
                resultado = null;
                handleError(res);
            }
            return manejarAutorizacionTelefonica(resultado);
        }

        protected override Transaccion manejarAutorizacionTelefonica(Transaccion aAutorizarTelefonicamente)
        {
            if (aAutorizarTelefonicamente != null)
            {
                /*  if (string.IsNullOrEmpty(aAutorizarTelefonicamente.numAutorizacion.Trim()))
                  {*/
                FrmAutorizacionTelefonica fAut;
                while (aAutorizarTelefonicamente.estado == HostCodesPOS.HOST_01_PEDIR_AUTORIZACION || aAutorizarTelefonicamente.estado == HostCodesPOS.HOST_02_PEDIR_AUTORIZACION ||
                    aAutorizarTelefonicamente.estado == HostCodesPOS.HOST_76_LLAMAR_AL_EMISOR || aAutorizarTelefonicamente.estado == HostCodesPOS.HOST_91_EMISOR_FUERA_LINEA ||
                    aAutorizarTelefonicamente.estado == HostCodesPOS.HOST_96_ERROR_EN_SISTEMA)
                {
                    fAut = new FrmAutorizacionTelefonica(aAutorizarTelefonicamente);
                    fAut.ShowDialog();
                    if (fAut.tran == null)
                    {
                        return null;
                    }
                    aAutorizarTelefonicamente = fAut.tran;
                }
                /* }*/
                return aAutorizarTelefonicamente;
            }
            else
                return null;
        }

        protected override Transaccion realizarDevolucion(Transaccion aDevolver)
        {
            Transaccion resultado = new TransaccionBuilder(TipoTransaccion.ANULACION, TipoAutorizador.POS_INGENICO);
            vpiRefundIn_t paramIn = new vpiRefundIn_t();
            vpiTrxOut_t paramOut = new vpiTrxOut_t(true);
            paramIn.Amount = aDevolver.importeTotal.ToString("#####0.00").Replace(",", "");
            paramIn.instalmentCount = "1";
            paramIn.issuerCode = aDevolver.tarjeta.codTarjeta;
            paramIn.merchantCode = aDevolver.comercio.codigoComercio;
            paramIn.merchantName = "AEROLINEAS ARGENTINAS";
            paramIn.planCode = " ";
            paramIn.linemode = 1;
            paramIn.OriginalDate = aDevolver.fechaOriginal.ToShortDateString();
            paramIn.OriginalTicket = aDevolver.ticketOriginal;
            paramIn.receiptNumber = "000000000000";
            paramIn.Cuit = "";
            int timeout = 60;
            int res = 0;
            Thread antiLock = new Thread((ThreadStart)delegate
                {
                    abrirPuerto();
                    res = NativeMethods.vpiRefund(ref paramIn, ref paramOut, ref timeout);
                    while (res == (int)Resultados.VPI_TIMEOUT_EXP || res == (int)InicioTransaccion.VPI_INVALID_CARD)
                    {
                        handleError(res);
                        res = NativeMethods.vpiRefund(ref paramIn, ref paramOut, ref timeout);
                    }
                    cerrarPuerto();
                });
            antiLock.Start();
            antiLock.Join();
            if (res == (int)Resultados.VPI_OK || res == (int)CerrarTransaccion.VPI_ERR_PRINT)
            {
                if (HostCodesPOS.checkError(paramOut.hostRespCode))
                {
                    Comercio auxCom = new Comercio();
                    auxCom.codigoComercio = aDevolver.comercio.codigoComercio;
                    resultado.comercio = auxCom;
                    resultado.numTicket = paramOut.TicketNumber;
                    resultado.numAutorizacion = paramOut.AuthCode;
                    resultado.numLote = Convert.ToInt32(paramOut.BatchNumber);
                    resultado.estado = paramOut.hostRespCode;
                    resultado.fecha = Convert.ToDateTime(paramOut.date + " " + paramOut.time);
                    if (res == (int)CerrarTransaccion.VPI_ERR_PRINT)
                    {
                        handleError(res);
                        reimprimirUltimoCupon();
                    }
                }
                else
                    resultado = null;
            }
            else
            {
                resultado = null;
                handleError(res);
            }
            return resultado;
        }

        protected override Transaccion realizarVoideo(Transaccion aVoidear)
        {
            Transaccion resultado = new TransaccionBuilder(TipoTransaccion.ANULACION, TipoAutorizador.POS_INGENICO);
            vpiVoidIn_t paramIn = new vpiVoidIn_t();
            paramIn.OriginalTicket = aVoidear.ticketOriginal;
            paramIn.merchantName = "AEROLINEAS ARGENTINAS";
            paramIn.issuerCode = aVoidear.tarjeta.codTarjeta;
            paramIn.Cuit = "00-00000000-0";
            vpiTrxOut_t paramOut = new vpiTrxOut_t(true);
            int timeout = 60;
            int res = 0;
            Thread antiLock = new Thread((ThreadStart)delegate
                {
                    abrirPuerto();
                    res = NativeMethods.vpiVoid(ref paramIn, ref paramOut, ref timeout);
                    while (res == (int)Resultados.VPI_TIMEOUT_EXP || res == (int)InicioTransaccion.VPI_INVALID_CARD)
                    {
                        handleError(res);
                        res = NativeMethods.vpiVoid(ref paramIn, ref paramOut, ref timeout);
                    }
                    cerrarPuerto();
                });
            antiLock.Start();
            antiLock.Join();
            if (res == (int)Resultados.VPI_OK || res == (int)CerrarTransaccion.VPI_ERR_PRINT)
            {
                if (HostCodesPOS.checkError(paramOut.hostRespCode))
                {
                    resultado.numTicket = paramOut.TicketNumber;
                    resultado.numAutorizacion = paramOut.AuthCode;
                    resultado.numLote = Convert.ToInt32(paramOut.BatchNumber);
                    resultado.estado = paramOut.hostRespCode;
                    resultado.fecha = Convert.ToDateTime(paramOut.date + " " + paramOut.time);
                    if (res == (int)CerrarTransaccion.VPI_ERR_PRINT)
                    {
                        handleError(res);
                        reimprimirUltimoCupon();
                    }
                }
                else
                    resultado = null;
            }
            else
            {
                resultado = null;
                handleError(res);
            }
            return resultado;
        }

        protected override Transaccion realizarCierreLote(Transaccion aVoidear)
        {
            Transaccion resultado = new TransaccionBuilder(TipoTransaccion.CIERRE, TipoAutorizador.POS_INGENICO);
            vpiBatchCloseOut_t paramOut = new vpiBatchCloseOut_t(true);
            int timeout = 60;
            int res = 0;
            Thread antiLock = new Thread((ThreadStart)delegate
                {
                    abrirPuerto();
                    res = NativeMethods.vpiBatchClose(ref paramOut, ref timeout);
                    cerrarPuerto();
                });
            antiLock.Start();
            antiLock.Join();
            if (res == (int)Resultados.VPI_OK || res == (int)CerrarTransaccion.VPI_ERR_PRINT)
            {
                if (HostCodesPOS.checkError(paramOut.hostRespCode))
                {
                    if (res == (int)CerrarTransaccion.VPI_ERR_PRINT)
                    {
                        handleError(res);
                        reimprimirUltimoCupon();
                    }
                    resultado.estado = paramOut.hostRespCode;
                    resultado.fecha = Convert.ToDateTime(paramOut.date + " " + paramOut.time);
                    return resultado;
                }
                else
                    return null;
            }
            else
            {
                handleError(res);
                return null;
            }
        }

        private void handleError(int res)
        {
            string error = "";
            switch (res)
            {
                case (int)Resultados.VPI_FAIL:
                    error = "Falló la comunicacion con el host";
                    break;
                case (int)Resultados.VPI_TIMEOUT_EXP:
                    error = "Tiempo de espera superado. Por favor aprete OK y vuelva a pasar la tarjeta";
                    break;
                case (int)InicioTransaccion.VPI_DIF_CARD:
                    error = "La tarjeta difiere de la especificada";
                    break;
                case (int)InicioTransaccion.VPI_EXPIRED_CARD:
                    error = "La tarjeta ha expirado";
                    break;
                case (int)InicioTransaccion.VPI_INVALID_CARD:
                    error = "Error en la lectura de la tarjeta. Por favor aprete OK y vuelva a pasar la tarjeta";
                    break;
                case (int)InicioTransaccion.VPI_INVALID_TRX:
                    error = "Transacción inválida";
                    break;
                case (int)InicioTransaccion.VPI_NO_TRANS_BATCH:
                    error = "No hay transacciones en el lote";
                    break;
                case (int)InicioTransaccion.VPI_TRX_CANCELLED:
                    error = "Transacción cancelada por el usuario";
                    break;
                case (int)Parametros.VPI_INVALID_BATCH:
                    error = "Lote invalido";
                    break;
                case (int)Parametros.VPI_INVALID_INDEX:
                    error = "Índice inválido";
                    break;
                case (int)Parametros.VPI_INVALID_ISSUER:
                    error = "Código de tarjeta inválido";
                    break;
                case (int)Parametros.VPI_INVALID_PLAN:
                    error = "Plan de pagos inválido";
                    break;
                case (int)Parametros.VPI_INVALID_TICKET:
                    error = "Número de ticket inválido";
                    break;
                case (int)Implementacion.VPI_GENERAL_FAIL:
                    error = "Error general no especificado";
                    break;
                case (int)Implementacion.VPI_INVALID_IN_CMD:
                    error = "Comando inválido";
                    break;
                case (int)Implementacion.VPI_INVALID_IN_PARAM:
                    error = "Parámetros de transacción inválidos";
                    break;
                case (int)Implementacion.VPI_INVALID_OUT_CMD:
                    error = "Respuesta inválida del host";
                    break;
                case (int)CerrarTransaccion.VPI_ERR_COM:
                    error = "Error de comunicación con el host";
                    break;
                case (int)CerrarTransaccion.VPI_ERR_PRINT:
                    error = "Error en la impresión del ticket. Cambie el rollo y aprete OK para volver a imprimir";
                    break;
                default:
                    error = "Error no especificado";
                    break;
            }
            MessageBox.Show("Error " + res + ": " + error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public override void cancelarLecturaTarjeta()
        {
            throw new NotImplementedException();
        }

        public override void cancelarTransaccion()
        {

        }

        public override string getNombreTransaccion(Transaccion trans)
        {
            throw new NotImplementedException();
        }
    }
}

