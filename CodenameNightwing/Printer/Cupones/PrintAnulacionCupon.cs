using CodenameNightwing.BusinessLogic;

namespace CodenameNightwing.Printer.Cupones
{
    class PrintAnulacionCupon : PrinterCupon
    {
        public PrintAnulacionCupon(Transaccion trans, EnumCopiaUOriginal tipo, bool isDuplicado)
            : base(trans, tipo, isDuplicado,true)
        {
        }

        protected override void setInfoTicketInfoDelOriginal()
        {
            infoTicket.Add(devolverConPadding("Cargo orig.: " + trans.ticketOriginal , "Fecha orig.: " + trans.fechaOriginal.ToString("dd/MM/yy")));
        }

        public PrintAnulacionCupon(Transaccion trans, EnumCopiaUOriginal tipo)
            : base(trans, tipo)
        {
        }

        protected override string getNumeroDeFactura()
        {
            return "0000 - 00000000";
        }
        public override string getNombreOperacion() {
            string voidType = "COMPRA";
            if (trans.transaccionOriginal != null && trans.transaccionOriginal.tipoTrans.Equals(TipoTransaccion.DEVOLUCION))
            {
                voidType = "DEVOLUCION";
            }
            return "ANULACION " + voidType;
        }

    }
}
