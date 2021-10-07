using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodenameNightwing.Exceptions
{
    class ServiceException : Exception
    {

        public ServiceException(string msg)
        {
            this.msgService = msg;
        }

        private string msgService;

        public string _msgVtol
        {
            get { return _msgVtol; }
            set { _msgVtol = value; }
        }

        public string toString()
        {
            return msgService + "stack trace: " + this.StackTrace;
        }

        public string getMsg()
        {
            return msgService;
        }

    }
}
