using CodenameNightwing.Varios;
using System;
using System.IO;
using System.Xml;

namespace CodenameNightwing.FileManager
{
    class HasarFileConfiguration
    {
        public static void ChangeComHasar(string aparato)
        {
            try
            {
                string archivoHtef = File.ReadAllText("htefConfig.xml");
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(archivoHtef);
                XmlNode xCaja = doc.GetElementsByTagName("caja")[0];
                xCaja.Attributes["ttyPinpad"].Value = "COM" + USBFinder.findPort(aparato);
                File.WriteAllText("htefConfig.xml",doc.OuterXml);
            }
            catch (Exception e)
            {
                Program.logger.Error("Error al intentar cambiar el puerto COM del archivo htefConfig.xml", e);
            }
        }

        public static void ChangeDiaActual()
        {
            try
            {
                FileStream fs = File.Create("dia_actual.dat");
                StreamWriter sw = new StreamWriter(fs);
                sw.Write(DateTime.Now.ToString("yyMMdd"));
                sw.Close();
                fs.Close();
            }
            catch (Exception e)
            {
                Program.logger.Error("Error al reescribir el archivo dia_actual.dat", e);
            }
        }
    }
}
