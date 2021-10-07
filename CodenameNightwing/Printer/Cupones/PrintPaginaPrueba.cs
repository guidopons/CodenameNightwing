using CodenameNightwing.BusinessLogic;

namespace CodenameNightwing.Printer.Cupones
{
    class PrintPaginaPrueba : PrinterCupon
    {
        public PrintPaginaPrueba(Transaccion trans, EnumCopiaUOriginal tipo, bool isDuplicado)
            : base(true)
        {
        }

        public PrintPaginaPrueba(Transaccion trans, EnumCopiaUOriginal tipo)
            : base(true)
        {
        }

        public PrintPaginaPrueba(bool testing)
            : base(true)
        {
            this.withEscPos = true;
        }

        public override string getNombreOperacion()
        {
            return "PAGINA DE PRUEBA";
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
