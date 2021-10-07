using CodenameNightwing.Config;
using log4net;
using System;
using System.IO;
using System.Windows.Forms;

namespace CodenameNightwing.FileManager
{
    public static class VTOLLastTrxId
    {

        public static readonly ILog logger = LogManager.GetLogger(typeof(VTOLLastTrxId));

        public static bool writeFileVTOLLastTrxId(int lastTrxId,bool seImprimio)
        {
            try
            {
                Configuration conf = Configuration.getInstance();
                string VTOLLastTrxIdFile = conf.poiFilesPath + "VTOLLastTrxId.dat";

                StreamWriter sw = File.CreateText(VTOLLastTrxIdFile);
                sw.WriteLine(lastTrxId.ToString());
                sw.WriteLine(seImprimio);
                sw.Close();
                return true;
            }
            catch (Exception e)
            {
                logger.Error("Error al escribir el archivo VTOLLastTrxId", e);
                return false;
            }
        }

        public static int recoverVTOLLastTrxId()
        {
            try
            {
                int lastTrxId = Convert.ToInt32(File.ReadAllLines(Application.StartupPath + "\\VTOLLastTrxId.dat")[0]);
                return lastTrxId;
            }
            catch (Exception e)
            {
                logger.Error("Error al leer el archivo VTOLLastTrxId", e);
                return -1;
            }
        }

        public static bool recoverVTOLLastTrxIdPrintStatus()
        {
            try
            {
                bool printstatus = Convert.ToBoolean(File.ReadAllLines(Application.StartupPath + "\\VTOLLastTrxId.dat")[1]);
                return printstatus;
            }
            catch (Exception e)
            {
                logger.Error("Error al leer el archivo VTOLLastTrxId", e);
                return false;
            }
        }
    }
}
