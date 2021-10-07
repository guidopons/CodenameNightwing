using CodenameNightwing.BusinessLogic;
using CodenameNightwing.Config;
using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace CodenameNightwing.FileManager
{
    public static class EntityLoader
    {

        public static readonly ILog logger = LogManager.GetLogger(typeof(EntityLoader));
        public static List<Pais> loadPaises()
        {
            try
            {
                Configuration conf = Configuration.getInstance();
                string paisesFile = conf.poiFilesPath + conf.paisesFile;

                string[] lineas = File.ReadAllLines(paisesFile);
                List<Pais> lAux = new List<Pais>();
                foreach (var item in lineas)
                {
                    lAux.Add(new Pais(item.Split('|')[0], item.Split('|')[1]));
                }
                return lAux;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al leer el archivo paises", "Error de lectura en archivo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Error("Error al leer el archivo paises", e);
                Application.Exit();
                return null;
            }
        }

        public static List<TarjetaCajero> loadTarjetas()
        {
            try
            {
                Configuration conf = Configuration.getInstance();
                string tarjetasFile = conf.poiFilesPath + conf.tarjetasFile;

                string[] lineas = File.ReadAllLines(tarjetasFile);
                List<TarjetaCajero> lAux = new List<TarjetaCajero>();
                foreach (var item in lineas)
                {
                    string[] valores = item.Split('|');
                    int aux = 0;
                    if (valores[2] == "AX" && Configuration.getInstance().tipoAuth == TipoAutorizador.POS_INGENICO)
                        if (int.TryParse(Configuration.getInstance().codAmexMM, out aux))
                            valores[1] = aux.ToString();
                    lAux.Add(new TarjetaCajero(valores[0], valores[1], valores[2], valores[3], valores[4], valores[5] == "CC" ? TipoTarjeta.CREDITO : TipoTarjeta.DEBITO, valores[6] == "ING" ? TipoAutorizador.POS_INGENICO : TipoAutorizador.HASAR));
                }
                return lAux;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al leer el archivo tarjetas", "Error de lectura en archivo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Error("Error al leer el archivo tarjetas", e);
                Application.Exit();
                return null;
            }
        }

        public static bool existenArchivos()
        {
            Configuration conf = Configuration.getInstance();
            string tarjetasFile = conf.poiFilesPath + conf.tarjetasFile;
            string paisesFile = conf.poiFilesPath + conf.paisesFile;

            return File.Exists(paisesFile) && File.Exists(tarjetasFile);
        }
    }
}
