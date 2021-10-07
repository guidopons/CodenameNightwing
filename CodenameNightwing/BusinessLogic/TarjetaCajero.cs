namespace CodenameNightwing.BusinessLogic
{
    public class TarjetaCajero
    {
        private string _codComercio;
        public string codComercio
        {
            get { return _codComercio; }
            set { _codComercio = value; }
        }

        private string _codNumTarjeta;
        public string codNumTarjeta
        {
            get { return _codNumTarjeta; }
            set { _codNumTarjeta = value; }
        }

        private string _codTarjetaSabre;
        public string codTarjetaSabre
        {
            get { return _codTarjetaSabre; }
            set { _codTarjetaSabre = value; }
        }

        private string _descripcionTarjeta;
        public string descripcionTarjeta
        {
            get { return _descripcionTarjeta; }
            set { _descripcionTarjeta = value; }
        }

        private string _codPlan;
        public string codPlan
        {
            get { return _codPlan; }
            set { _codPlan = value; }
        }

        private TipoTarjeta _tipo;
        public TipoTarjeta tipo
        {
            get { return _tipo; }
            set { _tipo = value; }
        }

        private TipoAutorizador _autorizador;
        public TipoAutorizador autorizador
        {
            get { return _autorizador; }
            set { _autorizador = value; }
        }
        

        public TarjetaCajero(string codComercio, string codNumTarjeta, string codTarjetaSabre, string descripcionTarjeta, string plan, TipoTarjeta tipo,TipoAutorizador autorizador)
        {
            this.codComercio = codComercio;
            this.codNumTarjeta = codNumTarjeta;
            this.codTarjetaSabre = codTarjetaSabre;
            this.descripcionTarjeta = descripcionTarjeta;
            this.codPlan = plan;
            this.tipo = tipo;
            this.autorizador = autorizador;
        }

        public override string ToString()
        {
            return descripcionTarjeta;
        }
    }
}
