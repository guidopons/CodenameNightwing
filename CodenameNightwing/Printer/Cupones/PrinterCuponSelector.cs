using CodenameNightwing.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodenameNightwing.Printer.Cupones
{
    class PrinterCuponSelector
    {
        public static PrinterCupon getCupon(Transaccion aImprimir, EnumCopiaUOriginal tipo, bool isDuplicado)
        {
            switch (aImprimir.tipoTrans)
            {
                case TipoTransaccion.COMPRA:
                    return new PrintCompraCupon(aImprimir,tipo, isDuplicado);
                case TipoTransaccion.CIERRE:
                    return null;
                case TipoTransaccion.ANULACION:
                    return new PrintAnulacionCupon(aImprimir, tipo, isDuplicado);
                case TipoTransaccion.DEVOLUCION:
                    return new PrintDevolucionCupon(aImprimir, tipo, isDuplicado);
                case TipoTransaccion.NADA:
                    return null;
                default:
                    return null;
            }
        }

        public static PrinterCupon getCupon(Transaccion aImprimir, EnumCopiaUOriginal tipo )
        {
            return PrinterCuponSelector.getCupon(aImprimir, tipo, false);
        }
    }
}
