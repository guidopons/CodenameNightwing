using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CodenameNightwing.Varios;
using CodenameNightwing.BusinessLogic;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using CodenameNightwing.WebServices.WSEspecificos;
using CodenameNightwing.WebServices;

namespace CodenameNightwing.Config
{
    public static class ConfigurationReader
    {

        public static string getCOMFromVTOL( )
        {
            List<String> lineas;
            string comVtol;
            try
            {
                string pinpadFileConfig = Application.StartupPath + "\\" + Configuration.getInstance().vtolPosClientLibFolder + "\\config\\serialPinPad.properties";
                lineas = File.ReadAllLines(pinpadFileConfig).ToList<String>();
                comVtol = lineas.Find(x => x.StartsWith("PPVX820POSNET.portName"));
                string auxParam = devolverParametro(comVtol);
                return auxParam;
            }
            catch (FileNotFoundException)
            {
                return null;
            }
        }

        public static string[] WriteSafeReadAllLines(String path)
        {
            using (var csv = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var sr = new StreamReader(csv))
            {
                List<string> file = new List<string>();
                while (!sr.EndOfStream)
                {
                    file.Add(sr.ReadLine());
                }

                return file.ToArray();
            }
        }

        public static bool checkLogWorkingKey()
        {
            string rutaCrypt = Application.StartupPath + "\\" + Configuration.getInstance().vtolPosClientLibFolder + "\\log\\lib.log";
            Configuration conf;
            List<String> lineas;
            string workingKeyLine = null;
            try
            {
                conf = Configuration.getInstance();
                lineas = WriteSafeReadAllLines(rutaCrypt).ToList<String>();
                workingKeyLine = lineas.Find(x => x.Contains("Working key is invalid"));
                return (workingKeyLine != null) ? false : true;
                

            }
            catch (FileNotFoundException)
            {
                return false;
            }
            catch (Exception e)
            {
                Program.logger.Error("Error al leer el archivo de Log de la librería", e);
                return false;
            }
            

        }

        public static bool checkSecurityWorkingKey()
        {
            string pathWorkingKeys = Application.StartupPath + "\\" + Configuration.getInstance().vtolPosClientLibFolder + "\\config\\workingKeys.properties";
            Configuration conf;
            List<String> lineas;
            string workingKeyLine = null;
            try
            {
                conf = Configuration.getInstance();
                lineas = WriteSafeReadAllLines(pathWorkingKeys).ToList<String>();
                workingKeyLine = lineas.Find(x => x.Contains("WKDATA"));
                if (workingKeyLine != null)
                {
                    string pattern = "WKDATA\"\\\\:\"(\\w+)";
                    Match result = Regex.Match(workingKeyLine, pattern);
                    if (result.Success)
                    {
                        return true;
                    }
                }
                
            }catch (FileNotFoundException)
            {
                return false;
            }
            catch (Exception e)
            {
                Program.logger.Error("Error al leer el archivo de Working Key", e);
                return false;
            }
            return false;

        }

        public static string getPinpadIdFromCryptProperties()
        {
            string pathCrypt = Application.StartupPath + "\\" + Configuration.getInstance().vtolPosClientLibFolder + "\\config\\crypt.properties";
            Configuration conf;
            List<String> lineas;
            string pinpadId = null;
            try
            {
                conf = Configuration.getInstance();
                lineas = WriteSafeReadAllLines(pathCrypt).ToList<String>();
                pinpadId = lineas.Find(x => x.StartsWith("nsf"));
                pinpadId = devolverParametro(pinpadId);
                return pinpadId;
            }
            catch (FileNotFoundException)
            {
                return "99999999";
            }
            catch (Exception e)
            {
                if ( Configuration.getInstance().tipoAuth.Equals( TipoAutorizador.VTOL_CALLCENTER ))
                    Program.logger.Error("Error al leer el Pinpad ID", e);
                return "99999999";
            }
        }

        public static bool checkConfAerolineasProperties(string ruta)
        {
            List<String> lineas;
            try
            {
                lineas = File.ReadAllLines(ruta).ToList<String>();

                foreach (string aux in lineas)
                {
                    string valor = devolverParametro(aux);
                    if ( valor != null && valor.Equals("COMPLETE"))
                    {
                        return false;
                    }
                }
                
                return true;
            }
            catch (FileNotFoundException)
            {
                return false;
            }
        }

        public static bool readInteactConfiguration()
        {
            Configuration conf;
            try
            {
                conf = Configuration.getInstance();

                WebServiceUser wsConf = new WebServiceUser("BAU");
                conf.codAuthBloqueados = WebResponseParser.parseXMLGetConfiguration(wsConf.getResponse());

                wsConf = new WebServiceUser("PXN");
                string wsResponse = WebResponseParser.parseXMLGetConfiguration(wsConf.getResponse());
                conf.printPaxName = (wsResponse != null && wsResponse.Equals("NO")) ? false : true;

                wsConf = new WebServiceUser("IVR");
                wsResponse = WebResponseParser.parseXMLGetConfiguration(wsConf.getResponse());
                conf.isIvrAvail = (wsResponse != null && wsResponse.Equals("YES")) ? true : false;

                wsConf = new WebServiceUser("TCM");
                wsResponse = WebResponseParser.parseXMLGetConfiguration(wsConf.getResponse());
                conf.btnIngresoTCManualEnable = (wsResponse != null && wsResponse.Equals("YES")) ? true : false;

                wsConf = new WebServiceUser("NPS");
                wsResponse = WebResponseParser.parseXMLGetConfiguration(wsConf.getResponse());
                conf.isNPSForzed = (wsResponse != null && wsResponse.Equals("YES")) ? true : false;

                wsConf = new WebServiceUser("FRA");
                wsResponse = WebResponseParser.parseXMLGetConfiguration(wsConf.getResponse());
                conf.isFraudAvail = (wsResponse != null && wsResponse.Equals("YES")) ? true : false;

                //Parametro para uso de Valtech WS 
                //YES: WS Valtech / NO: WS Tarcred
                wsConf = new WebServiceUser("PRO");
                wsResponse = WebResponseParser.parseXMLGetConfiguration(wsConf.getResponse());
                conf.isPromoAvail = (wsResponse != null && wsResponse.Equals("YES")) ? true : false;

                if (conf.usePromoValtech)
                {
                    conf.isPromoAvail = true;
                }

                wsConf = new WebServiceUser("SWS");
                wsResponse = WebResponseParser.parseXMLGetConfiguration(wsConf.getResponse());
                conf.sineSabreWs = wsResponse.Trim();

                wsConf = new WebServiceUser("CAR");
                wsResponse = WebResponseParser.parseXMLGetConfiguration(wsConf.getResponse());
                conf.confRegArg = wsResponse.Trim();

                wsConf = new WebServiceUser("NMI");
                wsResponse = WebResponseParser.parseXMLGetConfiguration(wsConf.getResponse());
                conf.npsMerchantId = wsResponse.Trim();

                wsConf = new WebServiceUser("NMN");
                wsResponse = WebResponseParser.parseXMLGetConfiguration(wsConf.getResponse());
                conf.npsMerchIdNat = wsResponse.Trim();

                wsConf = new WebServiceUser("VUR");
                wsResponse = WebResponseParser.parseXMLGetConfiguration(wsConf.getResponse());
                conf.baseUrlValtech = wsResponse.Trim();

                wsConf = new WebServiceUser("PRE");
                wsResponse = WebResponseParser.parseXMLGetConfiguration(wsConf.getResponse());
                conf.isProxyEnabled = (wsResponse.Trim().Equals("YES") ? true : false);

                wsConf = new WebServiceUser("PRA");
                wsResponse = WebResponseParser.parseXMLGetConfiguration(wsConf.getResponse());
                conf.proxyAddress = wsResponse.Trim();

                wsConf = new WebServiceUser("AUC");
                wsResponse = WebResponseParser.parseXMLGetConfiguration(wsConf.getResponse());
                conf.auth0clientId = wsResponse.Trim();

                wsConf = new WebServiceUser("AUK");
                wsResponse = WebResponseParser.parseXMLGetConfiguration(wsConf.getResponse());
                conf.auth0SecretKey = wsResponse.Trim();

                wsConf = new WebServiceUser("EPP");
                conf.emdPrintEndoPattern = WebResponseParser.parseXMLGetConfiguration(wsConf.getResponse());

                /*
                 * Parametros de tiempos IVRPagos: 
                    -Tiempo de derivacion 
                    -Tiempo de espera a localhost
                    -Timeout 
                */
                wsConf = new WebServiceUser("TIM");
                wsResponse = WebResponseParser.parseXMLGetConfiguration(wsConf.getResponse());
                conf.tiempoDerivacion = wsResponse.Trim().Split(';')[0];
                conf.tiempoLocalHost = wsResponse.Trim().Split(';')[1];
                conf.timeOutIVRPagos = wsResponse.Trim().Split(';')[2];

                //Importe minimo en ARS para comprobar fraude
                wsConf = new WebServiceUser("CYI");
                wsResponse = WebResponseParser.parseXMLGetConfiguration(wsConf.getResponse());
                conf.importeMinCS = wsResponse.Trim();

                //Listado PCs backoffice boton manual enabled
                wsConf = new WebServiceUser("BAC");
                wsResponse = WebResponseParser.parseXMLGetConfiguration(wsConf.getResponse());
                conf.backofficeWS = wsResponse.Trim();

                wsConf = new WebServiceUser("RSP");
                wsResponse = WebResponseParser.parseXMLGetConfiguration(wsConf.getResponse());
                conf.robotSineToPrint = wsResponse.Trim();

                // time out valtech connection in minutes
                wsConf = new WebServiceUser("VTO");
                wsResponse = WebResponseParser.parseXMLGetConfiguration(wsConf.getResponse());
                conf.valtechServicesTimeout = int.Parse(wsResponse.Trim());

                // time out valtech connection in minutes
                wsConf = new WebServiceUser("CPP");
                wsResponse = WebResponseParser.parseXMLGetConfiguration(wsConf.getResponse());
                conf.confirmationPaymentPage = wsResponse.Trim();

                wsConf = new WebServiceUser("TOP");
                wsResponse = WebResponseParser.parseXMLGetConfiguration(wsConf.getResponse());
                conf.timeOutPaymentForm = wsResponse.Trim();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public static bool readAerolineasProperties()
        {
            Configuration conf;
            List<String> lineas;
            bool tipoConexion;
            try
            {
                conf = Configuration.getInstance();

                lineas = File.ReadAllLines(conf.aerolineasPropertiesPath).ToList<String>();

                tipoConexion = lineas.Find(x => x.StartsWith("ar.qik.pos.conexionPOS")).Contains("USB");

                foreach (string aux in lineas)
                {
                    asignarValor(conf, aux, tipoConexion);
                }

                if (conf.tipoAuth == TipoAutorizador.VTOL)
                {
                    conf.idPinpad = getPinpadIdFromCryptProperties();
                }

                return true;
            }
            catch (FileNotFoundException)
            {
                return false;
            }
        }
     

        public static void setValueProperties(string key, string value)
        {
            PropertiesFile prop = new PropertiesFile(Configuration.getInstance().aerolineasPropertiesPath );
            //set value
            prop.set(key, value);
            //save
            prop.Save();
        }

        public static string getValueProperties(string key)
        {
            PropertiesFile prop = new PropertiesFile(Configuration.getInstance().aerolineasPropertiesPath);
            //set value
            string value = prop.get(key);
            //save
            return value;
        }

        private static void asignarValor(Configuration conf, string aux, bool tipoConexion)
        {

        string auxParam = devolverParametro(aux);
            if (!string.IsNullOrEmpty(auxParam))
            {
                switch (aux.Substring(0, aux.IndexOf("=")))
                {
                    case "ar.qik.citrix":
                        if (auxParam.Equals("YES"))
                            //Path por defecto de files poi
                            conf.poiFilesPath = conf.getPathPoiFiles(true);
                        else            
                            conf.poiFilesPath = conf.getPathPoiFiles(false);
                        break;
                    case "ar.qik.envorioment":
                        conf.environment = auxParam;
                        break;
                    //POI Filenames and Paths
                    case "ar.qik.filePaises":
                        conf.paisesFile = auxParam;
                        break;
                    case "ar.qik.fileTarjetas":
                        conf.tarjetasFile =  auxParam;
                        break;
                    case "ar.qik.filePayment":
                        conf.paymentFile = auxParam;
                        break;
                    case "ar.qik.fileVBrequest":
                        conf.vbRequestFile =  auxParam;
                        break;

                    case "ar.qik.vtol.fileOtherData":
                        conf.otherDataFile =  auxParam;
                        break;
                    case "ar.qik.vtol.fileAnulation":
                        conf.anulationFile =  auxParam;
                        break;
                    //Fin POI Filenames and Paths
                    case "ar.qik.vtol.vtolPosClientLibFolder":
                        conf.vtolPosClientLibFolder = auxParam;
                        break;
                    case "ar.qik.pos.conexionPOS":
                        conf.posCommunicationType = auxParam;
                        break;
                    case "ar.qik.pos.idPos":
                        conf.idPos = auxParam;
                        break;
                    case "ar.qik.pos.permiteEfectivo":
                        break;
                    case "ar.qik.pos.puerto":
                        if (tipoConexion)
                            conf.posComPort = USBFinder.findPort("Prolific USB");
                        else
                            conf.posComPort = auxParam;
                        break;
                    case "ar.qik.server.DEV":
                        conf.dirServDev = auxParam;
                        break;
                    case "ar.qik.server.PROD":
                        conf.dirServProd = auxParam;
                        break;
                    case "ar.qik.server.QA":
                        conf.dirServQa = auxParam;
                        break;
                    case "ar.qik.ws.bines":
                        conf.serverBines = auxParam;
                        break;
                    case "ar.qik.ws.codcomercio":
                        conf.serverCodComercio = auxParam;
                        break;
                    case "ar.qik.ws.cuit":
                        conf.serverCuitCuil = auxParam;
                        break;
                    case "ar.qik.ws.tarjetas":
                        conf.serverTarjetas = auxParam;
                        break;
                    case "ar.qik.ws.paises":
                        conf.serverPaises = auxParam;
                        break;
                    case "ar.qik.ws.voucher":
                        conf.serverVoucher = auxParam;
                        break;
                    case "ar.qik.ws.user":
                        conf.serverUser = auxParam;
                        break;
                    case "ar.qik.ws.transacciones":
                        conf.serverTransacciones = auxParam;
                        break;
                    case "ar.qik.autorizator":
                        asignarTipoAuth(conf, auxParam);
                        break;

                    case "ar.qik.vtol.caja":
                        conf.caja = auxParam;
                        break;
                    case "ar.qik.vtol.sucursal":
                        conf.sucursal = auxParam;
                        break;
                    case "ar.qik.vtol.puesto":
                        conf.puesto = auxParam;
                        break;
                    case "ar.qik.vtol.empresa":
                        conf.empresaHAS = auxParam;
                        break;

                    case "ar.qik.interact.emdOnePay":
                        foreach (var item in auxParam.Split('|'))
                            conf.emdsOnePay.Add(item.Trim());
                        break;
                    case "ar.qik.printer.printerList":
                        foreach (var item in auxParam.Split('|'))
                            conf.printerList.Add(item.Trim());
                        break;
                    case "ar.qik.pos.codAmexMM":
                        conf.codAmexMM = auxParam;
                        break;
                    case "ar.qik.printer.printerName":
                        conf.nombreImpresora = auxParam;
                        break;
                    case "ar.qik.printer.printerType":
                        asignarTipoPrinter(conf, auxParam);
                        break;
                    case "ar.qik.printer.modelName":
                        conf.modeloImpresora = auxParam;
                        break;
                    case "ar.qik.vtol.tipoDriverPinPad":
                        conf.tipoDriverPinPad = auxParam;
                        break;

                    case "ar.qik.printer.timeSleep":
                        conf.timeSleepPrinter = auxParam;
                        break;
                    case "ar.qik.vtol.printCupons":
                        conf.printCupons = auxParam;
                        break;
                    case "ar.qik.vtol.serviceVtolName":
                        conf.serviceVtolName = auxParam;
                        break;
                    case "ar.qik.vtol.sidUsuarios":
                        conf.sidUsuarios = auxParam;
                        break;
                    case "ar.qik.ws.terminales":
                        conf.serverTerminales = auxParam;
                        break;
                    case "ar.qik.vtol.timeOutService":
                        conf.timeOutService = auxParam;
                        break;
                    case "ar.qik.printer.idPrinter":
                        conf.idPrinter = auxParam;
                        break;
                    case "ar.qik.interact.ciudadSTAR":
                        conf.ciudadStar = auxParam;
                        break;

                    case "ar.qik.interact.cashier":
                        conf.cashier = auxParam;
                        break;
                    case "ar.qik.smtp.port":
                        conf.smtpPort = auxParam;
                        break;
                    case "ar.qik.smtp.host":
                        conf.smtpHost = auxParam;
                        break;
                    case "ar.qik.ftp.log":
                        conf.ftpLog = auxParam;
                        break;
                    case "ar.qik.vtol.timeOutLibraryResponse":
                        conf.timeOutLibraryResponse = auxParam;
                        break;
                    case "ar.qik.vtol.timeOutConnectionHost":
                        conf.timeOutConnectionHost = auxParam;
                        break;
                    case "ar.qik.printer.timeOutEpsonPrinter":
                        conf.timeOutEpsonPrinter = auxParam;
                        break;
                    case "ar.qik.mitrol.checkActualCall":
                        conf.checkActualCall = (auxParam != null && auxParam.Equals("true"));
                        break;
                    case "ar.qik.vtol.isServiceEnabled":
                        conf.isVtolServiceEnabled = (auxParam == null || (auxParam != null && auxParam.Equals("true")));
                        break;
                    case "ar.qik.usePromoValtech":
                        conf.usePromoValtech = (auxParam != null && auxParam.Equals("true"));
                        break;
                    case "ar.qik.mitrol.statusCall":
                        conf.callStatus = auxParam;
                        break;
                    case "ar.qik.mitrol.mockMitrol":
                        conf.mockMitrol = auxParam;
                        break;

                }
            }
        }

        private static void asignarTipoAuth(Configuration conf, string auxParam)
        {
         switch (auxParam)
            {
                case "ING":
                    conf.tipoAuth = TipoAutorizador.POS_INGENICO;
                    break;
                case "HAS":
                    conf.tipoAuth = TipoAutorizador.HASAR;
                    break;
                case "VTL":
                    conf.tipoAuth = TipoAutorizador.VTOL;
                    break;
                case "VCC":
                    conf.tipoAuth = TipoAutorizador.VTOL_CALLCENTER;
                    break;
                case "NPS":
                    conf.tipoAuth = TipoAutorizador.NPS;
                    break;
                default:
                    conf.tipoAuth = TipoAutorizador.VTOL_CALLCENTER;
                    break;
            }

            // se guarda el configurado, el tipoAuth puede cambiar por VTOL y NPS segun disponibilidad
            conf.tipoAuthConf = conf.tipoAuth;
        }

        public static string getTipoAuth(TipoAutorizador tipoAuth)
        {

            string strTipoAuth = null;

            switch (tipoAuth)
            {
                case TipoAutorizador.POS_INGENICO:
                    strTipoAuth = "ING";
                    break;
                case TipoAutorizador.HASAR:
                    strTipoAuth = "HAS";
                    break;
                case TipoAutorizador.VTOL:
                    strTipoAuth = "VTL";
                    break;
                case TipoAutorizador.VTOL_CALLCENTER:
                    strTipoAuth = "VCC";
                    break;
                case TipoAutorizador.NPS:
                    strTipoAuth = "NPS";
                    break;
                default:
                    strTipoAuth = "ING";
                    break;
            }

            return strTipoAuth;
        }


        private static void asignarTipoPrinter(Configuration conf, string auxParam)
        {
            switch (auxParam)
            {
                case "EPSON":
                    conf.tipoPrinter = TipoPrinter.EPSON;
                    break;
                case "GPRINTER":
                    conf.tipoPrinter = TipoPrinter.GPRINTER;
                    break;
                case "EPSON_NATIVE":
                    conf.tipoPrinter = TipoPrinter.EPSON_NATIVE;
                    break;
                default:
                    conf.tipoPrinter = TipoPrinter.EPSON;
                    break;
            }
        }
        private static string devolverParametro(string linea)
        {
            return linea.Contains("=") ? linea.Substring(linea.IndexOf("=") + 1).Trim() : "";
        }
    }
}