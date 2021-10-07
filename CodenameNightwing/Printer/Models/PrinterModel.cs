using CodenameNightwing.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CodenameNightwing.Printer.Models
{
    public abstract class PrinterModel
    {
        protected string printerName = Configuration.getInstance().nombreImpresora;

        public abstract bool printDocument(string[] lineasAImprimir , int copies);

        public abstract void initializePrinter();

    }
}
