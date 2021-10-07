using CodenameNightwing.BusinessLogic;
using CodenameNightwing.Config;
using CodenameNightwing.WebServices;
using CodenameNightwing.WebServices.WSEspecificos;
using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace CodenameNightwing.FileManager
{
    public static class EntityWriter
    {

        public static readonly ILog logger = LogManager.GetLogger(typeof(EntityWriter));
        private static List<Pais> fetchPaises()
        {
            WebServicePaises wsPaises = new WebServicePaises();
            //  wsPaises.sendRequest();
            return WebResponseParser.parseXMLPaises(wsPaises.getResponse());
        }

        public static void writeFilePaises()
        {
            string paisesFile = null;
            try
            {
                List<Pais> paisesTraidos = fetchPaises();
                if (paisesTraidos.Count > 0)
                {
                    Configuration conf = Configuration.getInstance();
                    paisesFile = conf.poiFilesPath + conf.paisesFile;

                    StreamWriter sw = File.CreateText(paisesFile);
                    foreach (var item in paisesTraidos)
                    {
                        if (item.codigoPais != "AR")
                            sw.WriteLine(item.codigoPais + "|" + item.descripcionPais);
                    }
                    sw.Close();
                }
            }
            catch (Exception e)
            {
                logger.Error("Error al escribir el archivo " + paisesFile, e);
                MessageBox.Show("Error al escribir el archivo paises", "Error de escritura en archivo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        public static List<TarjetaCajero> fetchTarjetas()
        {
            WebServiceTarjetas wsTarjetas = new WebServiceTarjetas();
            //   wsTarjetas.sendRequest();
            return WebResponseParser.parseXMLTarjetas(wsTarjetas.getResponse());
        }

        public static void writeFileTarjetas()
        {
            string tarjetasFile = null;
            try
            {
                List<TarjetaCajero> tarjetasTraidas = fetchTarjetas();
                if (tarjetasTraidas.Count > 0)
                {
                    Configuration conf = Configuration.getInstance();
                    tarjetasFile = conf.poiFilesPath + conf.tarjetasFile;

                    StreamWriter sw = File.CreateText(tarjetasFile);
                    foreach (var item in tarjetasTraidas)
                    {
                        sw.WriteLine(item.codComercio + "|" + item.codNumTarjeta + "|" + item.codTarjetaSabre + "|" + item.descripcionTarjeta + "|" + (!string.IsNullOrEmpty(item.codPlan) ? item.codPlan : " ") + "|" + (item.tipo == TipoTarjeta.CREDITO ? "CC" : "CK") + "|" + (item.autorizador == TipoAutorizador.NPS ? "NPS" : "ING"));
                    }
                    sw.Close();
                }
            }
            catch (Exception e)
            {
                logger.Error("Error al escribir el archivo " + tarjetasFile  , e);
                MessageBox.Show("Error al escribir el archivo tarjetas", "Error de escritura en archivo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }
    }
}