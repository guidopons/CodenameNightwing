namespace CodenameNightwing.BusinessLogic
{
    public class DuenioTarjeta
    {


        private bool _esExtranjero;
        public bool esExtranjero
        {
            get { return _esExtranjero; }
            set { _esExtranjero = value; }
        }

        private string _fechaNacimiento;
        public string fechaNacimiento
        {
            get { return _fechaNacimiento; }
            set { _fechaNacimiento = value; }
        }

        private string _genero;
        public string genero
        {
            get { return _genero; }
            set { _genero = value; }
        }

        private string _documento;
        public string documento
        {
            get { return _documento; }
            set { _documento = value; }
        }

        private string _cuitCuil;
        public string cuitCuil
        {
            get { return _cuitCuil; }
            set { _cuitCuil = value; }
        }

        private string _tipoCuitCuil;
        public string tipoCuitCuil
        {
            get { return _tipoCuitCuil; }
            set { _tipoCuitCuil = value; }
        }

        private string _nombre;
        public string nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        private string _provincia;
        public string provincia
        {
            get { return _provincia; }
            set { _provincia = value; }
        }

        private string _ciudad;
        public string ciudad
        {
            get { return _ciudad; }
            set { _ciudad = value; }
        }

        private string _codPostal;
        public string codPostal
        {
            get { return _codPostal; }
            set { _codPostal = value; }
        }


        private string _direccionCalle;
        public string direccionCalle
        {
            get { return _direccionCalle; }
            set { _direccionCalle = value; }
        }

        private string _direccionNro;
        public string direccionNro
        {
            get { return _direccionNro; }
            set { _direccionNro = value; }
        }

        public string direccion
        {
            get { return string.Concat(this.direccionCalle, " ", this.direccionNro); }
        }

    }
}