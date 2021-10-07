namespace CodenameNightwing.Autorization.VTOL.Config.Elementos
{
    class DefinicionLote
    {
        private int _idLote;
        public int idLote
        {
            get { return _idLote; }
            set { _idLote = value; }
        }

        private int _cajaONodo;
        public int cajaONodo
        {
            get { return _cajaONodo; }
            set { _cajaONodo = value; }
        }

        private string _numeroSerieTerminal;
        public string numeroSerieTerminal
        {
            get { return _numeroSerieTerminal; }
            set { _numeroSerieTerminal = value; }
        }

        public DefinicionLote(int idLote, int cajaONodo, string numeroSerieTerminal)
        {
            this.idLote = idLote;
            this.cajaONodo = cajaONodo;
            this.numeroSerieTerminal = numeroSerieTerminal;
        }
    }
}
