using System;
using com.epson.pos.driver;
using System.Drawing.Printing;
using System.Windows.Forms;
using System.Drawing;
using CodenameNightwing.Config;

namespace CodenameNightwing.Printer.Models
{
    class PrinterGPrinter : PrinterModel
    {

        public override void initializePrinter()
        {

        }

        public override bool printDocument(string[] lineasAImprimir , int copies)
        {

            RawPrinter rawPrinter = new RawPrinter();
            rawPrinter.OpenPrint( this.printerName );
            if (rawPrinter.PrinterIsOpen == true)
            {
                foreach (var line in lineasAImprimir)
                {
                    if (!rawPrinter.SendStringToPrinter(printerName, line + "\n"))
                    {
                        rawPrinter.ClosePrint();
                        return false;
                    }
                }
                // Chequeamos que no este más en la cola
                int timeSleep = int.Parse(Configuration.getInstance().timeSleepPrinter);
                System.Threading.Thread.Sleep(timeSleep);
                // Chequeamos que el trabajo haya llegado bien a la cola
                if (!PrinterHelper.checkQueue(true))
                {
                    rawPrinter.ClosePrint();
                    return false;
                }
                rawPrinter.ClosePrint();
            }
            return true;
        }


    }
}
