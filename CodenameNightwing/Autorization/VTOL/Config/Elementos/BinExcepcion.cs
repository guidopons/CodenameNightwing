namespace CodenameNightwing.Autorization.VTOL.Config.Elementos
{
    class BinExcepcion
    {
        private int _desde;
        public int desde
        {
            get { return _desde; }
            set { _desde = value; }
        }

        private int _hasta;
        public int hasta
        {
            get { return _hasta; }
            set { _hasta = value; }
        }

        private string _nombreTarjeta;
        public string nombreTarjeta
        {
            get { return _nombreTarjeta; }
            set { _nombreTarjeta = value; }
        }

        private string _informacionAdicional;
        public string informacionAdicional
        {
            get { return _informacionAdicional; }
            set { _informacionAdicional = value; }
        }

        public BinExcepcion(int desde, int hasta, string nombreTarjeta, string infoAdicional)
        {
            this.desde = desde;
            this.hasta = hasta;
            this.nombreTarjeta = nombreTarjeta;
            informacionAdicional = infoAdicional;
        }
    }
}
