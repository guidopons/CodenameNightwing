using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodenameNightwing.Valtech.Exceptions
{
    class ValtchException:Exception
    {

        public ValtchException( string msg) {
            this.msg = msg;
        }

        private string msg;

        public string _msg
        {
            get { return _msg; }
            set { _msg = value; }
        }

        public string toString() {
            return msg + "stack trace: " + this.StackTrace;   
        }

        public string getMsg()
        {
            return msg;
        }

    }
}
