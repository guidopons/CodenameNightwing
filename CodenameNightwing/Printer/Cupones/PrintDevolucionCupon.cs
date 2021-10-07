using CodenameNightwing.BusinessLogic;

namespace CodenameNightwing.Printer.Cupones
{
    class PrintDevolucionCupon : PrinterCupon
    {
        public PrintDevolucionCupon(Transaccion trans, EnumCopiaUOriginal tipo, bool isDuplicado)
            : base(trans, tipo, isDuplicado,true)
        {
        }

        public PrintDevolucionCupon(Transaccion trans, EnumCopiaUOriginal tipo)
            : base(trans, tipo)
        {
        }

        public override string getNombreOperacion()
        {
            return "DEVOLUCION COMPRA";
        }

        protected override void setInfoTicketInfoDelOriginal()
        {
            infoTicket.Add(devolverConPadding("Cargo orig.: " + trans.ticketOriginal, "Fecha orig.: " + trans.fechaOriginal.ToString("dd/MM/yy")));
        }

        protected override string getNumeroDeFactura()
        {
            return "0000 - 00000000";
        }

    }
}
