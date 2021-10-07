using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodenameNightwing.Exceptions
{
    class NPSException:Exception
    {

        public NPSException( string msg) {
            this.msgNps = msg;
        }

        private string msgNps;

        public string _msgNps
        {
            get { return _msgNps; }
            set { _msgNps = value; }
        }

        public string toString() {
            return msgNps + "stack trace: " + this.StackTrace;   
        }

        public string getMsg()
        {
            return msgNps;
        }

    }
}
