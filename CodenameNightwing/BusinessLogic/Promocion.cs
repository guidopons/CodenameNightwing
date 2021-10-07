using System;
using System.Collections.Generic;

namespace CodenameNightwing.BusinessLogic
{
    public class Promocion : IEquatable<Promocion>
    {

        public string Name { get; set; }
        public int Code { get; set; }

        public int cuotas { get; set; }

        public string banco { get; set; }
  
        
        public string codTarjeta { get; set; }

        public string codTarjetaSabre { get; set; }
 
        public DateTime fechaDesde { get; set; }

        public string descripcionTarjeta { get; set; }

        public decimal porcentaje { get; set; }

        public bool esBinExcepcion { get; set; }

        public string idPromo { get; set; }

        public string nombre { get; set; }

        public List<GatewayMetadata> gatewayMetadataLs { get; set; }


        public Promocion(string idPromo, string nombre, int cuotas, string banco, string codTarjeta, string codTarjetaSabre, DateTime fecha, string descripcionTarjeta, decimal porcentaje, bool binExcepcion)
        {
            this.idPromo = idPromo;
            this.banco = banco;
            this.cuotas = cuotas;
            this.codTarjeta = codTarjeta;
            this.codTarjetaSabre = codTarjetaSabre;
            this.fechaDesde = fecha;
            this.descripcionTarjeta = descripcionTarjeta;
            this.porcentaje = porcentaje;
            this.nombre = nombre;
            esBinExcepcion = binExcepcion;
        }


        public Promocion(List<GatewayMetadata> gatewayMetadataLs, string idPromo, string nombre, int cuotas, string banco, string codTarjeta, string codTarjetaSabre, DateTime fecha, string descripcionTarjeta, decimal porcentaje, bool binExcepcion) : this(idPromo, nombre, cuotas, banco, codTarjeta, codTarjetaSabre, fecha, descripcionTarjeta, porcentaje, binExcepcion) {
            this.gatewayMetadataLs = gatewayMetadataLs;
        }

        public bool Equals(Promocion other)
        {

            //Check whether the compared object is null. 
            if (Object.ReferenceEquals(other, null)) return false;

            //Check whether the compared object references the same data. 
            if (Object.ReferenceEquals(this, other)) return true;

            //Check whether the cuotas property are equal. 
            return cuotas == other.cuotas;
        }

        public override int GetHashCode()
        {
            return cuotas.GetHashCode();
        }

    }
}
