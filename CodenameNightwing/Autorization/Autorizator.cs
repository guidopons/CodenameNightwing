﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó mediante una herramienta.
//     Los cambios del archivo se perderán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

using CodenameNightwing.BusinessLogic;
using CodenameNightwing.Config;
using CodenameNightwing.Forms;
using CodenameNightwing.Printer;
using CodenameNightwing.Printer.Cupones;
using CodenameNightwing.Printer.Exceptions;
using CodenameNightwing.Sabre;
using CodenameNightwing.Valtech.FraudService;
using CodenameNightwing.Valtech.FraudService.Response;
using CodenameNightwing.Varios;
using log4net;
using System;
using System.Windows.Forms;

namespace CodenameNightwing.Autorization
{
    public abstract class Autorizator
    {
        public static readonly ILog logger = LogManager.GetLogger(typeof(Autorizator));

        public abstract Tarjeta solicitarNumeroTarjeta(string tipoTrans ,Transaccion tran);

        public abstract void cancelarLecturaTarjeta();

        public abstract void cancelarTransaccion();

        public abstract bool verificarConexion();

        public abstract bool reimprimirUltimoCupon();

        public abstract string getGatewayName();

        public abstract bool reimprimirCierreLote();

        protected abstract Transaccion realizarPago(Transaccion paraPagar);

        protected abstract Transaccion manejarAutorizacionTelefonica(Transaccion aAutorizarTelefonicamente);

        protected abstract Transaccion realizarDevolucion(Transaccion aDevolver);

        protected abstract Transaccion realizarVoideo(Transaccion aVoidear);

        protected abstract Transaccion realizarCierreLote(Transaccion aCerrar);

        public abstract string getNombreTransaccion(Transaccion trans);

        public string getParamValueFromPromo(string param, Promocion promoSelected , string defaultValue)
        {
            if (promoSelected != null && promoSelected.gatewayMetadataLs != null)
            {
                //Parametrización Esquemas de cuotas Valtech "Nombre de Gateway"
                string gatewayName = (getGatewayName() == "NPS" ? "webservice" : getGatewayName());
                GatewayMetadata meta = promoSelected.gatewayMetadataLs.Find(obj => obj.gatewayName == gatewayName);
                
                if (meta != null)
                {
                    if (meta.gatewayProps.ContainsKey(param))
                        return meta.gatewayProps[param];

                }
            }
            return defaultValue;


        }
        private Tarjeta obtenerTarjetaManual( Tarjeta tar )
        {

            // Armamos formulario para carga manual
            FrmDatosTarjetaDirecto frmTarjetaDirecto = new FrmDatosTarjetaDirecto(tar);
            frmTarjetaDirecto.ShowDialog();

            if (!frmTarjetaDirecto.isComplete)
            {
                return tar;
            }
            else
            {
                tar = frmTarjetaDirecto.tar;
            }

            return tar;

        }

        public bool checkFraud( Transaccion resultado )
        {
            bool isFraudOk = true;

            SabreController sabreController = new SabreController();
            if (resultado.isAprobada() && !sabreController.addTBMtoPNR(resultado.primaryPnr, resultado.tarjeta.numero, resultado.tarjeta.getVencimientoSabre(), resultado.tarjeta.codSabre))
            {
                logger.Error("Error ingresando TBM en la reserva: " + resultado.primaryPnr);
                return isFraudOk;
            }


            if ( Configuration.getInstance().isFraudAvail && resultado.isAprobada())
            {
                if ( resultado.isTransToCheckFraud() )
                {
                    FraudServiceController fraudController = new FraudServiceController();
                    FraudServiceResponse fraudResponse = fraudController.checkFraud(resultado);

                    if (fraudResponse != null && fraudResponse.fraudCheckInformation != null && fraudResponse.fraudCheckInformation.result.Equals("REJECT"))
                    {
                        MessageBox.Show("El analisis de Fraude de la transaccion dio REJECT. Favor cambiar medio de pago, la transaccion se anulará/reversará.", "Rechazado por Fraude", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        resultado.respuestaHost = "FRAUD REJECTED";
                        isFraudOk = false;
                    }

                }

            }
            return isFraudOk;

        }
        public virtual Tarjeta obtenerDatosTarjetaIvr( Transaccion tran )
        {
            FrmIVR frmIVR = null; 
            try
            { 
                frmIVR = new FrmIVR(tran);
                tran.tipoIngreso = TipoIngresoTarjeta.MANUAL;
                tran.isRedirectPayment = false;

                // Si no está habilitado el IVR por configuracion
                // deshabilitamos el boton
                if ( !Configuration.getInstance().isIvrAvail)
                {
                    frmIVR.disableDerivar();
                }
                
                frmIVR.ShowDialog();

                switch ( frmIVR.seleccionIVR)
                {

                    case TipoSeleccionIVR.CANCELAR:
                        return tran.tarjeta;
                    case TipoSeleccionIVR.MANUAL:
                        tran.tipoIngreso = TipoIngresoTarjeta.MANUAL;
                        return obtenerTarjetaManual(tran.tarjeta);
                    case TipoSeleccionIVR.ENLACE:
                        if (tran.tipoTrans == TipoTransaccion.COMPRA)
                            tran.isRedirectPayment = true;
                        tran.tipoIngreso = TipoIngresoTarjeta.FORM_3P;
                        return tran.tarjeta;
                        

                    case TipoSeleccionIVR.DERIVAR_IVR:
                        tran.tipoIngreso = TipoIngresoTarjeta.IVR;
                        if ( frmIVR.tarjetaIVR == null)
                        {
                            return obtenerDatosTarjetaIvr( tran );
                        }
                        return frmIVR.tarjetaIVR;
                        
                }

            }
            catch (Exception e)
            {
                string msg = "Error obteniendo datos de la tarjeta con NPS integrator";
                logger.Error(msg);
                logger.Error(e);
                throw new Exception(msg);
            }

            return null;
        }

        public virtual Transaccion realizarTransaccion(Transaccion aComputar)
        {

            Transaccion tran = null;

            try
            {

                if (aComputar.tarjeta != null && aComputar.tarjeta.tipoTarjeta.Equals(TipoTarjeta.CREDITO))
                {
                    if (aComputar.tarjeta.descripcion != null && POIutils.isDebitCard(aComputar.tarjeta.descripcion.ToUpperInvariant()))
                    {
                        string msg = "Selecciono Credito y esta deslizando una tarjeta de debito: " + aComputar.tarjeta.descripcion;
                        MessageBox.Show(msg, "Error al realizar transaccion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        logger.Error(msg);
                        cancelarLecturaTarjeta();
                        cancelarTransaccion();
                        return null;
                    }
                }
                else
                {
                    if (aComputar.tarjeta != null && aComputar.tarjeta.tipoTarjeta.Equals(TipoTarjeta.DEBITO))
                    {
                        if (aComputar.tarjeta.descripcion != null && !POIutils.isDebitCard(aComputar.tarjeta.descripcion.ToUpperInvariant()))
                        {
                            string msg = "Selecciono Debito y esta deslizando una tarjeta de credito: " + aComputar.tarjeta.descripcion;
                            MessageBox.Show(msg, "Error al realizar transaccion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            logger.Error(msg);
                            cancelarLecturaTarjeta();
                            cancelarTransaccion();
                            return null;
                        }
                        else
                        {
                            // Selecciono Debito y la tarjeta es de Debito. Validamos la correcta seleccion
                            if (aComputar.tarjeta.debitoSeleccionada != null && aComputar.tarjeta.descripcion != null && !aComputar.tarjeta.debitoSeleccionada.ToUpperInvariant().Equals(aComputar.tarjeta.descripcion.ToUpperInvariant()))
                            {
                                string msg = "Selecciono " + aComputar.tarjeta.debitoSeleccionada + " y esta deslizando:" + aComputar.tarjeta.descripcion;
                                MessageBox.Show(msg, "Error al realizar transaccion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                logger.Error(msg);
                                cancelarLecturaTarjeta();
                                cancelarTransaccion();
                                return null;
                            }

                        }
                    }
                }

            }
            catch (Exception)
            {
                // Si no pudo validar no hacer nada
            }

            try
            {

                if (!POIutils.isRegionalConfArgentina())
                {
                    try
                    {
                        cancelarLecturaTarjeta();
                        cancelarTransaccion();
                    }
                    catch (Exception e2)
                    {
                        logger.Error("Error al cerrar la sesion", e2);
                    }

                    return null;
                }

                switch (aComputar.tipoTrans)
                {
                    case TipoTransaccion.COMPRA:
                        tran = realizarPago(aComputar);
                        break;
                    case TipoTransaccion.ANULACION:
                        tran = realizarVoideo(aComputar);
                        break;
                    case TipoTransaccion.DEVOLUCION:
                        tran = realizarDevolucion(aComputar);
                        break;
                    case TipoTransaccion.CIERRE:
                        tran = realizarCierreLote(aComputar);
                        break;
                    default: tran = null; break;
                }
            }
            catch (Exception e)
            {

                MessageBox.Show("Error general no especificado al realizar transaccion", "Error al realizar transaccion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Error("Error general no especificado al realizar transaccion : ", e);

                try
                {
                    cancelarLecturaTarjeta();
                    cancelarTransaccion();
                }
                catch (Exception e2)
                {
                    logger.Error("Error al cerrar la sesion", e2);
                }

                return null;
            }
            if (tran != null)
            {
                tran.estadoDescripcion = TipoEstadoTransaccion.PASANDO_INFO_INTERACT;
            }
            return tran;
        }

        public static bool imprimir(Transaccion aImprimir)
        {

            // ESTADO: Transaccion Imprimiendo
            aImprimir.estadoDescripcion = TipoEstadoTransaccion.IMPRIMIENDO;

            bool impresionOk = false;
            string msgFromPrinter = "";

            PrinterCupon imprimir = PrinterCuponSelector.getCupon(aImprimir, EnumCopiaUOriginal.ORIGINAL);
            PrinterHelper ph = new PrinterHelper(imprimir.devolverCupon());

            ph.deleteJobs();

            try
            {

                if (!string.IsNullOrEmpty(Configuration.getInstance().nombreImpresora))
                {

                    impresionOk = ph.imprimir();

                    if (impresionOk && aImprimir.isAprobada())
                    {
                        imprimir = PrinterCuponSelector.getCupon(aImprimir, EnumCopiaUOriginal.COPIA);
                        ph = new PrinterHelper(imprimir.devolverCupon());
                        impresionOk = ph.imprimir();
                    }

                }

            }
            catch (PrinterException e)
            {
                msgFromPrinter = e.mensaje;
                Program.logger.Error("Error con la impresora: " + Configuration.getInstance().nombreImpresora + " Mensaje:" + msgFromPrinter);
                Program.logger.Error("No se pudo imprimir la siguiente transaccion: " + aImprimir + " Exception:" + e.ToString());
                impresionOk = false;
            }
            catch (Exception e)
            {
                msgFromPrinter = "Error de configuracion o conexion";
                Program.logger.Error("No se pudo imprimir la siguiente transaccion: " + aImprimir + " Exception:" + e.ToString());
                impresionOk = false;
            }
            finally
            {
                ph.deleteJobs();
            }

            if (!impresionOk)
            {
                aImprimir.estadoDescripcion = TipoEstadoTransaccion.REVERSADA;
            }
            else
            {
                // ESTADO: Transaccion Imprimiendo
                aImprimir.estadoDescripcion = TipoEstadoTransaccion.CUPONES_IMPRESOS;

            }

            return impresionOk;
        }

    }
}