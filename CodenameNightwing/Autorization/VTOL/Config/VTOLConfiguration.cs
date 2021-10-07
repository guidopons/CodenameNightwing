using System;
using CodenameNightwing.Autorization.VTOL.Config.Elementos;
using System.Collections.Generic;
using CodenameNightwing.Config;

namespace CodenameNightwing.Autorization.VTOL.Config
{
    class VTOLConfiguration
    {
        private string _local;
        public string local
        {
            get { return _local; }
            set { _local = value; }
        }

        private int _incremental;
        public int incremental
        {
            get { return _incremental; }
            set { _incremental = value; }
        }

        private string _crc;
        public string crc
        {
            get { return _crc; }
            set { _crc = value; }
        }

        private DateTime _fecha;
        public DateTime fecha
        {
            get { return _fecha; }
            set { _fecha = value; }
        }

        private List<Provider> proveedores = new List<Provider>();
        private List<Prefijo> prefijos = new List<Prefijo>();
        private List<Moneda> monedas = new List<Moneda>();
        private List<PlanPagos> planesDePagos = new List<PlanPagos>();
        private List<DefinicionLote> definicionesDeLote = new List<DefinicionLote>();
        private List<BinExcepcion> binesDeExcepcion = new List<BinExcepcion>();

        public void addProveedor(Provider prov)
        {
            proveedores.Add(prov);
        }

        public void addPrefijo(Prefijo pref)
        {
            prefijos.Add(pref);
        }

        public void addMoneda(Moneda moneda)
        {
            monedas.Add(moneda);
        }

        public void addPlan(PlanPagos plan)
        {
            planesDePagos.Add(plan);
        }

        public void addDefinicionDeLote(DefinicionLote definicion)
        {
            definicionesDeLote.Add(definicion);
        }

        public void addBinesExcepcion(BinExcepcion binExcepcion)
        {
            binesDeExcepcion.Add(binExcepcion);
        }


        public Provider getProvider(string codTarjeta)
        {
            Provider resultado = null;
            resultado = proveedores.Find(y => y.idTarjeta == codTarjeta);
            return resultado;
        }

        public DefinicionLote getDefinicionLote( PlanPagos plan )
        {
            DefinicionLote resultado = null;
            resultado = definicionesDeLote.Find(y => y.idLote.Equals(plan.idLote) && y.cajaONodo.ToString().Equals(Configuration.getInstance().caja ));
            return resultado;
        }


        public PlanPagos getPlanesDePagosSegunTarjeta(string tarjetaId, int cantCuotas)
        {

            PlanPagos resultado = null;
            resultado = planesDePagos.Find(y => y.idTarjeta == tarjetaId && y.cuotas == cantCuotas );
            return resultado;
        }


        private VTOLConfiguration() { }

        private static readonly VTOLConfiguration instancia = new VTOLConfiguration();

        public static VTOLConfiguration getInstance()
        {
            if (instancia == null)
                return new VTOLConfiguration();
            else
                return instancia;
        }
    }
}
