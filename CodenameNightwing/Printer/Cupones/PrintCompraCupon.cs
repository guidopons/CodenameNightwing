using CodenameNightwing.BusinessLogic;

namespace CodenameNightwing.Printer.Cupones
{
    class PrintCompraCupon : PrinterCupon
    {
        public PrintCompraCupon(Transaccion trans, EnumCopiaUOriginal tipo, bool isDuplicado)
            : base(trans, tipo, isDuplicado,true)
        {
        }

        public PrintCompraCupon(Transaccion trans, EnumCopiaUOriginal tipo)
            : base(trans, tipo)
        {
        }

        public override string getNombreOperacion()
        {
            return "COMPRA";
        }


        protected override void setInfoTicketInfoDelOriginal()
        {
                
        }

        protected override string getNumeroDeFactura()
        {
            return "0000 - 00000000";
        }


    }
}
