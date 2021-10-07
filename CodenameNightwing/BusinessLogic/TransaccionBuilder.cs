using System;
using CodenameNightwing.Varios;
using CodenameNightwing.Config;
using CodenameNightwing.FileManager;
using System.Collections.Generic;

namespace CodenameNightwing.BusinessLogic
{
    public class TransaccionBuilder : Transaccion
    {

        public TransaccionBuilder(TipoTransaccion tipoTran, TipoAutorizador tipoAuth)
            : base(tipoTran, tipoAuth)
        {
        }

        public static Transaccion construirPago(TarjetaCajero tCajero, int cantCuotas, decimal importeTotal, TipoModoTransaccion modoAutorizacion)
        {
            Transaccion tran = new TransaccionBuilder(TipoTransaccion.COMPRA, Configuration.getInstance().tipoAuth);
            tran.importeTotal = importeTotal;
            Comercio auxCom = new Comercio();
            auxCom.codigoComercio = tCajero.codComercio;
            tran.comercio = auxCom;
            tran.cantCuotas = cantCuotas;
            tran.tarjeta = tCajero.ToTarjeta();
            tran.modo = modoAutorizacion;
            VBrequestReader.setCommonFields(tran);
            tran.tipoOperacion = TipoOperacion.COMPRA_PASAJE;
            tran.tipoTrans = TipoTransaccion.COMPRA;
            return tran;
        }

        public static Transaccion construirPago(string codComercio, int cantCuotas, decimal importeTotal, string codTarjeta, TipoModoTransaccion modoAutorizacion)
        {
            Transaccion tran = new TransaccionBuilder(TipoTransaccion.COMPRA, Configuration.getInstance().tipoAuth);
            tran.importeTotal = importeTotal;
            Comercio auxCom = new Comercio();
            auxCom.codigoComercio = codComercio;
            tran.comercio = auxCom;
            tran.cantCuotas = cantCuotas;
            Tarjeta auxTar = new Tarjeta();
            auxTar.codTarjeta = codTarjeta;
            tran.tarjeta = auxTar;
            tran.modo = modoAutorizacion;
            VBrequestReader.setCommonFields(tran);
            tran.tipoOperacion = TipoOperacion.COMPRA_PASAJE;
            tran.tipoTrans = TipoTransaccion.COMPRA;
            return tran;
        }

        public static Transaccion construirPago(decimal importeTotal)
        {
            Transaccion tran = new TransaccionBuilder(TipoTransaccion.COMPRA, Configuration.getInstance().tipoAuth);
            tran.importeTotal = importeTotal;
            tran.modo = TipoModoTransaccion.ONLINE;
            tran.cantCuotas = 1;
            VBrequestReader.setCommonFields(tran);
            tran.tipoOperacion = TipoOperacion.COMPRA_PASAJE;
            tran.tipoTrans = TipoTransaccion.COMPRA;
            return tran;
        }

        public static Transaccion construirAnulacion(string ticketOriginal, int trxReferenceIdOriginal, DateTime fechaOriginal, Tarjeta tarjeta)
        {
            Transaccion tran = new TransaccionBuilder(TipoTransaccion.ANULACION, Configuration.getInstance().tipoAuth);
            tran.trxReferenceIdOriginal = trxReferenceIdOriginal;
            tran.ticketOriginal = ticketOriginal;
            tran.fechaOriginal = fechaOriginal;
            tran.tarjeta = tarjeta;
            tran.modo = TipoModoTransaccion.ONLINE;
            VBrequestReader.setCommonFields(tran);
            tran.tipoOperacion = TipoOperacion.ANULACION;
            tran.tipoTrans = TipoTransaccion.ANULACION;
            return tran;
        }

        public static Transaccion construirAnulacion(string ticketOriginal, int trxReferenceIdOriginal, decimal importeTotal, int cuotas)
        {
            Transaccion tran = new TransaccionBuilder(TipoTransaccion.ANULACION, Configuration.getInstance().tipoAuth);
            tran.trxReferenceIdOriginal = trxReferenceIdOriginal;
            tran.numTicket = ticketOriginal;
            tran.importeTotal = importeTotal;
            tran.cantCuotas = cuotas;
            tran.modo = TipoModoTransaccion.ONLINE;
            VBrequestReader.setCommonFields(tran);
            tran.tipoOperacion = TipoOperacion.ANULACION;
            tran.tipoTrans = TipoTransaccion.ANULACION;
            return tran;
        }

        public static Transaccion construirAnulacion(int trxReferenceIdOriginal)
        {
            Transaccion tran = new TransaccionBuilder(TipoTransaccion.ANULACION, Configuration.getInstance().tipoAuth);
            tran.trxReferenceIdOriginal = trxReferenceIdOriginal;
            tran.modo = TipoModoTransaccion.ONLINE;
            tran = AnulationFile.updateTransaccion(trxReferenceIdOriginal, tran);
            VBrequestReader.setCommonFields(tran);
            tran.tipoOperacion = TipoOperacion.ANULACION;
            tran.tipoTrans = TipoTransaccion.ANULACION;
            return tran;
        }

        public static Transaccion construirDevolucion(string ticketOriginal, int trxReferenceIdOriginal, DateTime fechaOriginal, decimal importeTotal, Tarjeta tarjeta)
        {
            Transaccion tran = new TransaccionBuilder(TipoTransaccion.DEVOLUCION, Configuration.getInstance().tipoAuth);
            tran.ticketOriginal = ticketOriginal;
            tran.trxReferenceIdOriginal = trxReferenceIdOriginal;
            tran.fechaOriginal = fechaOriginal;
            tran.importeTotal = importeTotal;
            tran.tarjeta = tarjeta;
            tran.modo = TipoModoTransaccion.ONLINE;
            VBrequestReader.setCommonFields(tran);
            tran.tipoOperacion = TipoOperacion.DEVOLUCION;
            tran.tipoTrans = TipoTransaccion.DEVOLUCION;
            return tran;
        }

        public static Transaccion construirDevolucion(string ticketOriginal, int trxReferenceIdOriginal, decimal importeTotal)
        {
            Transaccion tran = new TransaccionBuilder(TipoTransaccion.DEVOLUCION, Configuration.getInstance().tipoAuth);
            tran.ticketOriginal = ticketOriginal;
            tran.trxReferenceIdOriginal = trxReferenceIdOriginal;
            tran.importeTotal = importeTotal;
            tran.modo = TipoModoTransaccion.ONLINE;
            VBrequestReader.setCommonFields(tran);
            tran.tipoOperacion = TipoOperacion.DEVOLUCION;
            tran.tipoTrans = TipoTransaccion.DEVOLUCION;
            return tran;
        }

        public static Transaccion construirCambio(DateTime fecha, decimal importeTotal, List<string> itins)
        {
            Transaccion tran = new TransaccionBuilder(TipoTransaccion.COMPRA, Configuration.getInstance().tipoAuth);
            tran.fecha = fecha;
            tran.importeTotal = importeTotal;
            tran.modo = TipoModoTransaccion.ONLINE;
            VBrequestReader.setCommonFields(tran);
            tran.tipoOperacion = TipoOperacion.CANJE;
            tran.tipoTrans = TipoTransaccion.COMPRA;
            List<Pnr> listPnr = new List<Pnr>();
            Pnr pnr = new Pnr();
            pnr.codSabre = tran.primaryPnr;
            pnr.itins = itins;
            listPnr.Add(pnr);
            tran.listPnr = listPnr;
            return tran;
        }

        public static Transaccion construirCierreLote()
        {
            return new TransaccionBuilder(TipoTransaccion.CIERRE, Configuration.getInstance().tipoAuth);
        }
    }
}