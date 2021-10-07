using CodenameNightwing.BusinessLogic;
using System;

namespace CodenameNightwing.IVR
{
    class WCentrixException : Exception
    {
        string msg = null;
        TipoMensaje tipoMsg = TipoMensaje.ERROR;


        public string getMsg()
        {
            return msg;
        }

        public TipoMensaje getTipoMsg()
        {
            return tipoMsg;
        }


        public WCentrixException( string msg)
        {
            this.msg = msg;    
        }

        public WCentrixException(string msg, TipoMensaje tipoMsg)
        {
            this.msg = msg;
            this.tipoMsg = tipoMsg;
        }
    }
}
