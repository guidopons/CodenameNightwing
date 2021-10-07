using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodenameNightwing.BusinessLogic
{
    class IdiomaNPS
    {

        public string codigoIdiomaNPS { get; set; }

        public string descCodigoIdiomaNPS { get; set; }

 
        public IdiomaNPS(string codigo, string descripcion)
        {
            codigoIdiomaNPS = codigo;
            descCodigoIdiomaNPS = descripcion;
        }

        public IdiomaNPS() { }

        public override string ToString()
        {
            return descCodigoIdiomaNPS;
        }
    }
}

