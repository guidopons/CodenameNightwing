namespace CodenameNightwing.Autorization.VTOL.Config.Elementos
{
    class Moneda
    {
        private string _simbolo;
        public string simbolo
        {
            get { return _simbolo; }
            set { _simbolo = value; }
        }

        private string _descripcion;
        public string descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }

        public Moneda(string simbolo, string descripcion)
        {
            this.simbolo = simbolo;
            this.descripcion = descripcion;
        }
    }
}
