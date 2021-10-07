namespace CodenameNightwing.Autorization.VTOL.Config.Elementos
{
    class Prefijo
    {
        private string _desde;
        public string desde
        {
            get { return _desde; }
            set { _desde = value; }
        }

        private string _hasta;
        public string hasta
        {
            get { return _hasta; }
            set { _hasta = value; }
        }

        private int _largoPrefijo;
        public int largoPrefijo
        {
            get { return _largoPrefijo; }
            set { _largoPrefijo = value; }
        }

        private int _largoTarjeta;
        public int largoTarjeta
        {
            get { return _largoTarjeta; }
            set { _largoTarjeta = value; }
        }

        private string _idTarjeta;
        public string idTarjeta
        {
            get { return _idTarjeta; }
            set { _idTarjeta = value; }
        }

        private string _condicion;
        public string condicion
        {
            get { return _condicion; }
            set { _condicion = value; }
        }

        private int _largoCVC;
        public int largoCVC
        {
            get { return _largoCVC; }
            set { _largoCVC = value; }
        }

        private bool _validarDigito;
        public bool validarDigito
        {
            get { return _validarDigito; }
            set { _validarDigito = value; }
        }

        private bool _enviaTrack1;
        public bool enviaTrack1
        {
            get { return _enviaTrack1; }
            set { _enviaTrack1 = value; }
        }

        private bool _validaVencimiento;
        public bool validaVencimiento
        {
            get { return _validaVencimiento; }
            set { _validaVencimiento = value; }
        }

        private bool _permiteOffline;
        public bool permiteOffline
        {
            get { return _permiteOffline; }
            set { _permiteOffline = value; }
        }

        private int _limiteMontoOffline;
        public int limiteMontoOffline
        {
            get { return _limiteMontoOffline; }
            set { _limiteMontoOffline = value; }
        }

        private bool _habilitado;
        public bool habilitado
        {
            get { return _habilitado; }
            set { _habilitado = value; }
        }

        private bool _validaFechaEfectiva;
        public bool validaFechaEfectiva
        {
            get { return _validaFechaEfectiva; }
            set { _validaFechaEfectiva = value; }
        }

        private bool _validaCVC;
        public bool validaCVC
        {
            get { return _validaCVC; }
            set { _validaCVC = value; }
        }

        private string _serviceCode;
        public string serviceCode
        {
            get { return _serviceCode; }
            set { _serviceCode = value; }
        }

        private bool _permiteIngresoManual;
        public bool permiteIngresoManual
        {
            get { return _permiteIngresoManual; }
            set { _permiteIngresoManual = value; }
        }

        private bool _chequeaBoletines;
        public bool chequeaBoletines
        {
            get { return _chequeaBoletines; }
            set { _chequeaBoletines = value; }
        }

        private bool _esDebito;
        public bool esDebito
        {
            get { return _esDebito; }
            set { _esDebito = value; }
        }

        private bool _requierePin;
        public bool requierePin
        {
            get { return _requierePin; }
            set { _requierePin = value; }
        }

        private int _validaUltimosXNumeros;
        public int validaUltimosXNumeros
        {
            get { return _validaUltimosXNumeros; }
            set { _validaUltimosXNumeros = value; }
        }

        private bool _pideTipoCuenta;
        public bool pideTipoCuenta
        {
            get { return _pideTipoCuenta; }
            set { _pideTipoCuenta = value; }
        }

        private bool _solicitaNumeroDeCuenta;
        public bool solicitaNumeroDeCuenta
        {
            get { return _solicitaNumeroDeCuenta; }
            set { _solicitaNumeroDeCuenta = value; }
        }

        private bool _permiteCashBack;
        public bool permiteCashBack
        {
            get { return _permiteCashBack; }
            set { _permiteCashBack = value; }
        }

        private bool _usaPuntosDeLealtad;
        public bool usaPuntosDeLealtad
        {
            get { return _usaPuntosDeLealtad; }
            set { _usaPuntosDeLealtad = value; }
        }

        private bool _tarjetaEncripta;
        public bool tarjetaEncripta
        {
            get { return _tarjetaEncripta; }
            set { _tarjetaEncripta = value; }
        }

        public Prefijo(string desde, string hasta,int largoPrefijo,int largoTarjeta,string idTarjeta,string condicion,int largoCVC,bool validarDigito,
            bool enviaTrack1,bool validaVencimiento,bool permiteOffline,int limiteMontoOffline, bool habilitado,bool validaFechaEfectiva,bool validaCVC,
            string serviceCode,bool permiteIngresoManual,bool chequeaBoletines, bool esDebito,bool requierePin,int validaUltimosXNumeros,bool pideTipoCuenta,
            bool solicitaNumeroDeCuenta,bool permiteCashBack, bool usaPuntosDeLealtad, bool tarjetaEncripta)
        {
            this.desde = desde;
            this.hasta = hasta;
            this.largoPrefijo = largoPrefijo;
            this.largoTarjeta = largoTarjeta;
            this.idTarjeta = idTarjeta;
            this.condicion = condicion;
            this.largoCVC = largoCVC;
            this.validaCVC = validaCVC;
            this.enviaTrack1 = enviaTrack1;
            this.validaVencimiento = validaVencimiento;
            this.permiteOffline = permiteOffline;
            this.limiteMontoOffline = limiteMontoOffline;
            this.habilitado = habilitado;
            this.validaFechaEfectiva = validaFechaEfectiva;
            this.validaCVC = validaCVC;
            this.serviceCode = serviceCode;
            this.permiteIngresoManual = permiteIngresoManual;
            this.chequeaBoletines = chequeaBoletines;
            this.esDebito = esDebito;
            this.requierePin = requierePin;
            this.validaUltimosXNumeros = validaUltimosXNumeros;
            this.pideTipoCuenta = pideTipoCuenta;
            this.solicitaNumeroDeCuenta = solicitaNumeroDeCuenta;
            this.permiteCashBack = permiteCashBack;
            this.usaPuntosDeLealtad = usaPuntosDeLealtad;
            this.tarjetaEncripta = tarjetaEncripta;
        }
    }
}
