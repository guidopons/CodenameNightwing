using CodenameNightwing.Config;
using CodenameNightwing.Autorization.VTOL.Config.Elementos;
using System;
using System.IO;

namespace CodenameNightwing.Autorization.VTOL.Config
{
    public static class VTOLConfigurationFile
    {
        public static bool writeConfiguration(string VTOLFileConfig, string configuration)
        {
            try
            {
                StreamWriter sw = File.CreateText(VTOLFileConfig);
                sw.Write(configuration);
                sw.Close();
                return true;
            }
            catch (Exception e)
            {
                Program.logger.Error("Error al escribir el archivo de configuracion de VTOL", e);
                return false;
            }
        }

        public static bool getConfiguration(string VTOLFileConfig)
        {
            try
            {
                string[] lineas = File.ReadAllLines(VTOLFileConfig);
                VTOLConfiguration config = VTOLConfiguration.getInstance();
                foreach (var item in lineas)
                {
                    string[] partes = item.Substring(3).Split(';');
                    switch (item.Substring(0,2))
                    {
                        case "HD":
                            config.local = partes[0];
                            config.incremental = Convert.ToInt32(partes[1]);
                            config.crc = partes[2];
                            config.fecha =  DateTime.ParseExact(partes[3],"yyyy/MM/dd HH:mm",System.Globalization.CultureInfo.InvariantCulture);
                            break;
                        case "PV":
                            Provider auxProv = new Provider(partes[0], partes[1], partes[2]);
                            config.addProveedor(auxProv);
                            break;
                        case "PF":
                            Prefijo auxPref = new Prefijo(partes[1], partes[0], Convert.ToInt32(partes[2]), Convert.ToInt32(partes[3]), partes[4], partes[5],
                                Convert.ToInt32(partes[6]), intToBoolean(partes[7]), intToBoolean(partes[8]), intToBoolean(partes[9]),
                                intToBoolean(partes[10]), Convert.ToInt32(partes[11]), intToBoolean(partes[12]), intToBoolean(partes[13]),
                                intToBoolean(partes[14]), partes[15], intToBoolean(partes[16]), intToBoolean(partes[17]),
                                intToBoolean(partes[18]), intToBoolean(partes[19]), Convert.ToInt32(partes[20]), intToBoolean(partes[21]),
                                intToBoolean(partes[22]), intToBoolean(partes[23]), intToBoolean(partes[24]), intToBoolean(partes[25]));
                            config.addPrefijo(auxPref);
                            break;
                        case "MN":
                            Moneda auxMon = new Moneda(partes[0], partes[1]);
                            config.addMoneda(auxMon);
                            break;
                        case "PP":
                            PlanPagos auxPlanPagos = new PlanPagos(partes[0], partes[1], partes[2], Convert.ToInt32(partes[3]), Convert.ToInt32(partes[4]),
                                partes[5], Convert.ToInt32(partes[6]), Convert.ToDecimal(partes[7], System.Globalization.CultureInfo.InvariantCulture),
                                Convert.ToDecimal(partes[8], System.Globalization.CultureInfo.InvariantCulture), Convert.ToDecimal(partes[9], System.Globalization.CultureInfo.InvariantCulture));
                            config.addPlan(auxPlanPagos);
                            break;
                        case "DL":
                            DefinicionLote auxDefLot = new DefinicionLote(Convert.ToInt32(partes[0]), Convert.ToInt32(partes[1]), partes[2]);
                            config.addDefinicionDeLote(auxDefLot);
                            break;
                        case "BE":
                            BinExcepcion auxBinEx = new BinExcepcion(Convert.ToInt32(partes[0]), Convert.ToInt32(partes[1]), partes[2], partes[3]);
                            config.addBinesExcepcion(auxBinEx);
                            break;
                        default:
                            break;
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                Program.logger.Error("Error al recuperar datos del archivo de configuracion de VTOL", e);
                return false;
            }
        }

        private static bool intToBoolean(string aConvertir)
        {
            return Convert.ToInt32(aConvertir) == 1 ? true : false;
        }
    }
}
