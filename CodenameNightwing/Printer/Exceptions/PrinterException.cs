using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodenameNightwing.Printer.Exceptions
{
    class PrinterException: Exception
    {

        public PrinterException( string msg)
        {
            this.mensaje = msg;
        }

        private string _mensaje;
        public string mensaje
        {
            get { return _mensaje; }
            set { _mensaje = value; }
        }
    }
}
