﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó mediante una herramienta.
//     Los cambios del archivo se perderán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------
namespace CodenameNightwing.Autorization.POS.StructsComunicacionPOS
{
    public struct vpiBatchCloseOut_t
    {
        public string hostRespCode
        {
            get;
            set;
        }

        public string date
        {
            get;
            set;
        }

        public string time
        {
            get;
            set;
        }

        public string terminalID
        {
            get;
            set;
        }

        public vpiBatchCloseOut_t(bool inicializar)
            : this()
        {
            if (inicializar)
            {
                hostRespCode = new string('\0', 3);
                date = new string('\0', 11);
                time = new string('\0', 9);
                terminalID = new string('\0', 9);
            }
        }

    }
}
