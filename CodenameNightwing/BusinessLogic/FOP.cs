using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodenameNightwing.BusinessLogic
{
    public class FOP
    {

        private string _descripcion;
        public string descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }


        private string _currency;
        public string currency
        {
            get { return _currency; }
            set { _currency = value; }
        }

        public override string ToString()
        {
            return this.descripcion;
        }

    }
}
