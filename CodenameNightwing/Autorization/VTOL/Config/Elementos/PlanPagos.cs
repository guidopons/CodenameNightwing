namespace CodenameNightwing.Autorization.VTOL.Config.Elementos
{
    class PlanPagos
    {
        private string _idTarjeta;
        public string idTarjeta
        {
            get { return _idTarjeta; }
            set { _idTarjeta = value; }
        }

        private string _simboloMoneda;
        public string simboloMoneda
        {
            get { return _simboloMoneda; }
            set { _simboloMoneda = value; }
        }

        private string _condicionPago;
        public string condicionPago
        {
            get { return _condicionPago; }
            set { _condicionPago = value; }
        }

        private int _plan;
        public int plan
        {
            get { return _plan; }
            set { _plan = value; }
        }

        private int _cuotas;
        public int cuotas
        {
            get { return _cuotas; }
            set { _cuotas = value; }
        }

        private string _numeroComercio;
        public string numeroComercio
        {
            get { return _numeroComercio; }
            set { _numeroComercio = value; }
        }

        private int _idLote;
        public int idLote
        {
            get { return _idLote; }
            set { _idLote = value; }
        }

        private decimal _limiteASuperar;
        public decimal limiteASuperar
        {
            get { return _limiteASuperar; }
            set { _limiteASuperar = value; }
        }

        private decimal _limiteIntereses;
        public decimal limiteIntereses
        {
            get { return _limiteIntereses; }
            set { _limiteIntereses = value; }
        }

        private decimal _interes;
        public decimal interes
        {
            get { return _interes; }
            set { _interes = value; }
        }

        public PlanPagos(string idTarjeta, string simboloMoneda, string condicionPago, int plan, int cuotas, string numeroComercio, int idLote, decimal limiteASuperar, decimal limiteIntereses, decimal interes)
        {
            this.idTarjeta = idTarjeta;
            this.simboloMoneda = simboloMoneda;
            this.condicionPago = condicionPago;
            this.plan = plan;
            this.cuotas = cuotas;
            this.numeroComercio = numeroComercio;
            this.idLote = idLote;
            this.limiteASuperar = limiteASuperar;
            this.limiteIntereses = limiteIntereses;
            this.interes = interes;
        }
    }
}
