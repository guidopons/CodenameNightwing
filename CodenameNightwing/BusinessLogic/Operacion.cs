using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodenameNightwing.Config;

namespace CodenameNightwing.BusinessLogic
{
    public class Operacion
    {

        public Operacion(TipoOperacion tipoOperacion , decimal importe)
        {
            this.tipo = tipoOperacion;
            this.importe = importe;
        }

        public Operacion(TipoOperacion tipoOperacion , List<Pnr> lsPnr)
        {
            this.tipo = tipoOperacion;
            this.lsPnr = lsPnr;
        }

        private TipoOperacion _tipo;
        public TipoOperacion tipo
        {
            get { return _tipo; }
            set { _tipo = value; }
        }


        private decimal _importe = 0;
        public decimal importe
        {
            get {

                if ( lsPnr != null)
                {
                    decimal imporAux = 0;
                    foreach ( Pnr pnr in lsPnr)
                    {
                        imporAux = imporAux + pnr.getTotalAmount();
                    }
                    return imporAux;

                }
                return _importe; }
            set { _importe = value; }
        }

        private List<Pnr> _lsPnr = null;
        public List<Pnr> lsPnr
        {
            get { return _lsPnr; }
            set { _lsPnr = value; }
        }


        public string ciudadEmision
        {
            get { return Configuration.getInstance().ciudadStar; }
        }

        public string sucursal
        {
            get { return Configuration.getInstance().sucursal; }
        }

        public string caja
        {
            get { return Configuration.getInstance().caja; }
        }

        /* <!--Optional:-->
            <caja>2</caja>
            <!--Optional:-->
            <ciudadEmision>aep</ciudadEmision>
            <importe>200.00</importe>
            <!--Zero or more repetitions:-->
            <lsPnr>
               <!--Optional:-->
               <agentDesc>pepe</agentDesc>
               <!--Optional:-->
               <codSabre>XDFGTR</codSabre>
               <!--Optional:-->
               <emails>pepe@pepe-cp,</emails>
               <!--Zero or more repetitions:-->
               <ffNumbers>15151551</ffNumbers>
               <!--Optional:-->
               <groupName>PEPE</groupName>
               <!--Zero or more repetitions:-->
               <itins>AEPGIG</itins>
               <onlyEmd>YES</onlyEmd>
               <!--Zero or more repetitions:-->
               <pasajeros>
                  <!--Zero or more repetitions:-->
                  <emds>
                     <codEmd>2</codEmd>
                     <!--Optional:-->
                     <descripcion>pepe</descripcion>
                     <fare>200.00</fare>
                  </emds>
                  <emds>
                     <codEmd>1</codEmd>
                     <!--Optional:-->
                     <descripcion>pepe 2</descripcion>
                     <fare>100.00</fare>
                  </emds>

                  <fare>200.00</fare>
                  <!--Optional:-->
                  <nombre>PEPE</nombre>
                  <!--Optional:-->
                  <tipoPax>ADT</tipoPax>
               </pasajeros>
               <!--Zero or more repetitions:-->
               <seats>pepe</seats>
               <!--Zero or more repetitions:-->
               <tickets>
                  <!--Optional:-->
                  <descExpanded>ho</descExpanded>
                  <!--Optional:-->
                  <descripcion>pepe</descripcion>
                  <!--Zero or more repetitions:-->
                  <fops>crop</fops>
                  <!--Optional:-->
                  <from>pepe</from>
                  <!--Optional:-->
                  <tourCode>AT</tourCode>
               </tickets>
               <!--Optional:-->
               <tipoItinerario>INT</tipoItinerario>
            </lsPnr>
            <!--Optional:-->
            <sucursal>30</sucursal>
            <!--Optional:-->
            <tipo>2</tipo>
    */

        public string toXMLOperacion()
        {
            StringBuilder xmlOutput = new StringBuilder();

            if (lsPnr != null)
            {
                foreach (Pnr pnr in lsPnr)
                {
                    xmlOutput.Append(pnr.toXMLOperacion());
                }
            }
            xmlOutput.Append("<caja>" + caja + "</caja>");
            xmlOutput.Append("<sucursal>" + sucursal + "</sucursal>");
            xmlOutput.Append("<ciudadEmision>" + ciudadEmision + "</ciudadEmision>");

            xmlOutput.Append("<importe>" + importe + "</importe>");
            xmlOutput.Append("<tipo>" + tipo + "</tipo>");

            return xmlOutput.ToString();

        }

    }
}
