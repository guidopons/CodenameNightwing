using CodenameNightwing.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodenameNightwing.WebServices.Response
{
    public class TerminalResponse
    {

        private string _status;
        public string status
        {
            get { return _status; }
            set { _status = value; }
        }

        private string _outputMsg;
        public string outputMsg
        {
            get { return _outputMsg; }
            set { _outputMsg = value; }
        }


        private Terminal _terminalEncontrada;
        public Terminal terminalEncontrada
        {
            get { return _terminalEncontrada; }
            set { _terminalEncontrada = value; }
        }


    }
}
