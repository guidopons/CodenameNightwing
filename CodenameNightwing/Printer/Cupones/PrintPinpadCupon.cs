using CodenameNightwing.BusinessLogic;

namespace CodenameNightwing.Printer.Cupones
{
    class PrintPinpadCupon : PrinterCupon
    {
        public PrintPinpadCupon(Transaccion trans, EnumCopiaUOriginal tipo, bool isDuplicado)
            : base(trans, tipo, isDuplicado,true)
        {
        }

        public PrintPinpadCupon(Transaccion trans, EnumCopiaUOriginal tipo, bool isDuplicado, bool withEscPos)
            : base(trans, tipo, isDuplicado, withEscPos)
        {
        }

        public PrintPinpadCupon(Transaccion trans, EnumCopiaUOriginal tipo)
            : base(trans, tipo)
        {
        }

        public override string getNombreOperacion()
        {
            return "";
        }


        protected override void setInfoTicketInfoDelOriginal()
        {
                
        }

        protected override string getNumeroDeFactura()
        {
            return "";
        }


    }
}
