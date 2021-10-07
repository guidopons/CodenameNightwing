using CodenameNightwing.BusinessLogic;
using CodenameNightwing.Config;
using System;
using System.Globalization;

namespace CodenameNightwing.Printer.Cupones
{
    class PrintVoucher: PrinterCupon
    {
        private Voucher _voucher;
        public Voucher voucher
        {
            get { return _voucher; }
            set { _voucher = value; }
        }

        public PrintVoucher(Transaccion trans, EnumCopiaUOriginal tipo, bool isDuplicado)
            : base(false)
        {
        }

        public PrintVoucher(Transaccion trans, EnumCopiaUOriginal tipo)
            : base(false)
        {
        }

        public PrintVoucher(bool testing)
            : base(false)
        {
        }

        public PrintVoucher(bool testing , Voucher voucherPass)
            : base(false)
        {
            this.voucher = voucherPass;
            setInfoTicketInfoDelOriginal();
        }

        public override string getNombreOperacion()
        {
            return "E-VOUCHER";
        }


        protected override void setInfoTicketInfoDelOriginal()
        {
            encabezado.Add(LogoPrinter.GetLogo());
            encabezado.Add("_____________________________________________\n" + ESCPOS.justificacionIzquierda);
            encabezado.Add("E-Voucher: " + voucher.tktNumber + "\n" +  ESCPOS.justificacionIzquierda);
            encabezado.Add("Status: " + voucher.status + "\n" + ESCPOS.justificacionIzquierda);
            encabezado.AddRange(getStringForCupon("Pax Nombre / Name: " + voucher.name , ESCPOS.justificacionIzquierda).Split('|'));
            encabezado.Add("Voucher ID:" + voucher.idVoucher + "\n" + ESCPOS.justificacionIzquierda);
            addSpace(encabezado, 1);
            if ( ! (voucher.descEspanol != null && voucher.descEspanol.Trim().Equals(""))) { 
                encabezado.Add("Voucher Desc:" + "\n" + ESCPOS.justificacionIzquierda);
                encabezado.AddRange(getStringForCupon(voucher.descEspanol).Split('|'));
                encabezado.AddRange(getStringForCupon( voucher.descIngles).Split('|'));
            }
            encabezado.Add("_____________________________________________\n" + ESCPOS.justificacionIzquierda);
            DateTime dt = DateTime.ParseExact(voucher.issueDate.Trim(), "ddMMMyy", new CultureInfo("en-US"));
            encabezado.Add("Fecha de emisión / Issue Date:" + dt.ToString("dd.MM.yy", new CultureInfo("en-US")).ToUpper() + "\n" + ESCPOS.justificacionIzquierda);
            dt = dt.AddYears(1);
            string validTo = dt.ToString("dd.MM.yy", new CultureInfo("en-US")).ToUpper();
            encabezado.Add("Válido hasta / Valid Through:" + validTo + "\n" + ESCPOS.justificacionIzquierda);
            encabezado.Add("Agente / Agent:" + voucher.agent + "\n" + ESCPOS.justificacionIzquierda);
            encabezado.Add("Ciudad / City:" + voucher.city + "\n" + ESCPOS.justificacionIzquierda);
            encabezado.Add("Estación / Station:" + voucher.station + "\n" + ESCPOS.justificacionIzquierda);
            encabezado.Add("_____________________________________________\n" + ESCPOS.justificacionIzquierda);
            encabezado.Add("Ticket Original:" + voucher.tktAsoc + "\n" + ESCPOS.justificacionIzquierda);
            encabezado.Add("Tarifa / Fare:" + voucher.totalFare + " (" +  voucher.currency +  ")\n" + ESCPOS.fontBold);
            encabezado.Add("_____________________________________________\n" + ESCPOS.justificacionIzquierda);
            encabezado.Add("CUIT: 30-64140555-4\n" + ESCPOS.justificacionIzquierda);

            addSpace(encabezado, 1);

            encabezado.AddRange(getStringForCupon("El presente es intransferible y valido únicamente para la compra de Tickets en oficinas de ventas, sucursales de Aerolíneas Argentinas S.A. y/o a través del Call Center, para ser aplicado en vuelos de Aerolíneas Argentinas S.A. y Austral Líneas Aéreas - Cielos del Sur S.A. El mismo no podrá utilizarse para el pago de los impuestos ni de los cargos de emisión.").ToUpper().Split('|'));

            addSpace(encabezado, 1);

            encabezado.AddRange(getStringForCupon("The present is non-transferable and valid only for the purchase of tickets in sales offices and/or branches of Aerolíneas Argentinas S.A. and/or through Call Center, to be applied on flights of Aerolíneas Argentinas S.A.and Austral Líneas Aéreas - Cielos del Sur S.A. It can not be used for the payment of taxes or emission charges.").ToUpper().Split('|'));


            addSpace(pie, 4);
            if (!Configuration.getInstance().tipoPrinter.Equals(TipoPrinter.EPSON))
            {
                pie.Add(ESCPOS.eCut);
            }
        }

        protected override string getNumeroDeFactura()
        {
            return "0000 - 00000000";
        }


    }
}
