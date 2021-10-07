
using CodenameNightwing.BusinessLogic;
using CodenameNightwing.Config;
using System;
using System.IO;
using System.Linq;

namespace CodenameNightwing.FileManager
{
    class HasarOtherDataComunicator
    {
        public static Transaccion recibirDatos()
        {
            try
            {
                Configuration conf = Configuration.getInstance();
                string otherDataFile = conf.poiFilesPath + conf.otherDataFile;

                string[] lineas = File.ReadAllLines(otherDataFile);
                Transaccion aux = new TransaccionBuilder(TipoTransaccion.COMPRA,TipoAutorizador.HASAR);
                aux.tarjeta.numero = lineas.First(x=>x.StartsWith("otherData.tarjeta.numero")).Split('=')[1].Trim();
                if (!string.IsNullOrEmpty(lineas.First(x => x.StartsWith("otherData.tarjeta.pais")).Split('=')[1].Trim()))
                    aux.pais = EntityLoader.loadPaises().First(x => x.codigoPais == lineas.First(y => y.StartsWith("otherData.tarjeta.pais")).Split('=')[1].Trim());
                aux.tarjeta.owner.documento = lineas.First(x => x.StartsWith("otherData.duenio.documento")).Split('=')[1].Trim();
                aux.tarjeta.owner.cuitCuil = lineas.First(x => x.StartsWith("otherData.duenio.cuitCuil")).Split('=')[1].Trim();
                aux.tarjeta.owner.tipoCuitCuil = lineas.First(x => x.StartsWith("otherData.cuitType")).Split('=')[1].Trim();
                File.Delete(otherDataFile);
                return aux;
            }
            catch (Exception e)
            {
                CodenameNightwing.Program.logger.Error("Error al leer el archivo otherData", e);
                return null;
            }
        }

        public static void sendExtranjero(Procedencia extranjero,bool onePay)
        {
            try
            {
                Configuration conf = Configuration.getInstance();
                string otherDataFile = conf.poiFilesPath + conf.otherDataFile;
                StreamWriter sw = File.CreateText(otherDataFile);
                string aEscribir = "otherData.extranjero = ";
                switch (extranjero)
                {
                    case Procedencia.NACIONAL:
                        aEscribir += false;
                        break;
                    case Procedencia.EXTRANJERO:
                        aEscribir += true;
                        break;
                    case Procedencia.PREGUNTAR:
                        aEscribir += "PREGUNTAR";
                        break;
                    default:
                        aEscribir += "PREGUNTAR";
                        break;
                }
                sw.WriteLine(aEscribir);
                sw.WriteLine("otherData.onePay = " + onePay);
                sw.Close();
            }
            catch (Exception e)
            {
                CodenameNightwing.Program.logger.Error("Error al escribir el archivo otherData", e);
            }
        }
    }
}
