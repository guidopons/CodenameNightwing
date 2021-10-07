using System;
using System.IO;
using System.Linq;
using CodenameNightwing.Varios;
using CodenameNightwing.BusinessLogic;
using CodenameNightwing.Config;
using log4net;

namespace CodenameNightwing.FileManager
{
    class AnulationFile
    {

        public static readonly ILog logger = LogManager.GetLogger(typeof(AnulationFile));

        public static Transaccion buscarTransaccion(long trxReferenceNumber)
        {
            try
            {
                string[] lineas = leerArchivo();
                foreach (var item in lineas)
                {
                    Transaccion aux = item.ToTransaccion();
                    if (aux != null && aux.trxReferenceId == trxReferenceNumber)
                        return aux;
                }
            }
            catch (Exception e)
            {
                logger.Error("Error en la carga de anulacion con autorizador por red", e);
            }
            return null;
        }


        public static Transaccion updateTransaccion(long trxReferenceNumber , Transaccion tranToUpdate)
        {
            try
            {
                Transaccion tran = buscarTransaccion(trxReferenceNumber);
                if ( tran != null)
                {
                    tranToUpdate.numTicket = tran.numTicket;
                    tranToUpdate.trxReferenceId = tran.trxReferenceId;
                    tranToUpdate.importeTotal = tran.importeTotal;
                    tranToUpdate.cantCuotas = tran.cantCuotas;
                    tranToUpdate.trxId = tran.trxId;
                }
                return tranToUpdate;
            }
            catch (Exception e)
            {
                logger.Error("Error en la carga de anulacion con autorizador por red", e);
            }
            return null;
        }

        public static void grabarTransaccion(Transaccion tran)
        {
            try
            {
                Configuration conf = Configuration.getInstance();
                string anulationFile = conf.poiFilesPath + conf.anulationFile;
                string aGrabar = tran.fecha.ToString("yyyyMMdd HHmmss") + "|" + tran.numTicket + "|" + tran.trxReferenceId.ToString().ToBase36String() + "|" + tran.importeTotal.ToString("#####0.00").Replace(",", ".") +
                    "|" + tran.cantCuotas.ToString() + "|" + (tran.tipoAuth == TipoAutorizador.VTOL ? "VTL" :  "NPS") +  "|" + tran.trxId + Environment.NewLine;
                File.AppendAllText(anulationFile, aGrabar);
            }
            catch (Exception e)
            {
                logger.Error("Error al grabar transaccion para posterior anulacion con autorizador por red", e);
            }
        }

        public static bool deleteTransaccion(Transaccion tran)
        {
            try
            {
                string[] lineas = leerArchivo();
                string[] aGuardar;
                Configuration conf = Configuration.getInstance();
                string anulationFile = conf.poiFilesPath + conf.anulationFile;
                foreach (var item in lineas)
                {
                    if (item.Split('|')[2] == tran.trxIdOriginal.ToBase36String())
                    {
                        aGuardar = lineas.Where(x => x != item).ToArray();
                        File.WriteAllLines(anulationFile, aGuardar);
                        return true;
                    }
                }
                return false;
            }
            catch (Exception e)
            {
                logger.Error("Error al eliminar transaccion ya anulada de archivo de anulacion", e);
                return false;
            }
        }

        private static string[] leerArchivo()
        {
            Configuration conf = Configuration.getInstance();
            string anulationFile = conf.poiFilesPath + conf.anulationFile;
            return File.ReadAllLines(anulationFile);
        }
    }
}
