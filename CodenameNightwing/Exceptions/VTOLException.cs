using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodenameNightwing.Exceptions
{
    class VTOLException:Exception
    {

        public VTOLException( string msg) {
            this.msgVtol = msg;
        }

        private string msgVtol;

        public string _msgVtol
        {
            get { return _msgVtol; }
            set { _msgVtol = value; }
        }

        public string toString() {
            return msgVtol + "stack trace: " + this.StackTrace;   
        }

        public string getMsg()
        {
            return msgVtol;
        }

    }
}
