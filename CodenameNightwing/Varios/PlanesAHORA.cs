using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodenameNightwing.Varios
{
    class PlanesAHORA
    {

        public static string getCuotasMostrar(int cuotas)
        {
            string cuotasMostrar = "";
            switch (cuotas)
            {
                case 7: cuotasMostrar = "AH12"; break;
                case 8: cuotasMostrar = "AH18"; break;
                case 13: cuotasMostrar = "AH3"; break;
                case 16: cuotasMostrar = "AH6"; break;
                default:
                    cuotasMostrar = cuotas.ToString();
                    break;
            }

            return cuotasMostrar;

        }

        public static string getCuotasMostrarNombre(int cuotas)
        {
            string cuotasMostrar = "";
            switch (cuotas)
            {
                case 7: cuotasMostrar = "AHORA 12"; break;
                case 8: cuotasMostrar = "AHORA 18"; break;
                case 13: cuotasMostrar = "AHORA 3"; break;
                case 16: cuotasMostrar = "AHORA 6"; break;
                default:
                    cuotasMostrar = "Cuotas" + cuotas.ToString();
                    break;
            }

            return cuotasMostrar;

        }

        public static int getCuotasDividir(int cuotas)
        {
            int cuotasMostrar = 1;
            switch (cuotas)
            {
                case 7: cuotasMostrar = 12; break;
                case 8: cuotasMostrar = 18; break;
                case 13: cuotasMostrar = 3; break;
                case 16: cuotasMostrar = 6; break;
                default:
                    cuotasMostrar = cuotas;
                    break;
            }

            return cuotasMostrar;

        }


        public static int getAhoraFromString(string cuotasSelected)
        {
            int cuotas = 1;
            switch (cuotasSelected)
            {
                case "AH12": cuotas = 7; break;
                case "AH18": cuotas = 8; break;
                case "AH3": cuotas = 13; break;
                case "AH6": cuotas = 16; break;
                default:
                    cuotas = int.Parse(cuotasSelected);
                    break;
            }

            return cuotas;

        }

        /*
            AO AHORA 3                                                     	
            AI AHORA 6                                                     	
            AJ AHORA 12                                                    	
            AK AHORA 18                                                    	
        */
        public static int getAhoraFromCode(string codeAhora, int cuotaNoAhora)
        {
            int cuotas = 1;
            switch (codeAhora)
            {
                case "AO": cuotas = 13; break;
                case "AI": cuotas = 16; break;
                case "AJ": cuotas = 7; break;
                case "AK": cuotas = 8; break;
                default: cuotas = cuotaNoAhora;break;
            }

            return cuotas;
        }
    }
}



