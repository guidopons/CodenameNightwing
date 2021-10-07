namespace CodenameNightwing.Autorization.VTOL.Config.Elementos
{
    class Provider
    {
        private string _idTarjeta;
        public string idTarjeta
        {
            get { return _idTarjeta; }
            set { _idTarjeta = value; }
        }

        private string _nombre;
        public string nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        private string _medioPago;
        public string medioPago
        {
            get { return _medioPago; }
            set { _medioPago = value; }
        }

        public Provider(string idTarjeta, string nombre, string medioPago)
        {
            this.idTarjeta = idTarjeta;
            this.nombre = nombre;
            this.medioPago = medioPago;
        }
    }
}
