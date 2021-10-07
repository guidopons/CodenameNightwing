using System;
using System.Windows.Forms;
using CodenameNightwing.Forms;
using log4net;
using log4net.Config;
using CodenameNightwing.Varios;
using CodenameNightwing.Autorization;
using CodenameNightwing.FileManager;
using CodenameNightwing.BusinessLogic;
using CodenameNightwing.Config;
using CodenameNightwing.Autorization.VTOL.Config;
using CodenameNightwing.Printer;
using System.Collections.Generic;
using CodenameNightwing.Printer.Cupones;
using CodenameNightwing.WebServices.WSEspecificos;
using CodenameNightwing.WebServices;
using CodenameNightwing.WebServices.Response;
using CodenameNightwing.IVR;

namespace CodenameNightwing
{
    static class Program
    {
        public static readonly ILog logger = LogManager.GetLogger(typeof(Program));

        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {

            try
            {

                // Setup de principales
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                NativeMethods.SystemParametersInfo(NativeMethods.SPI_SETKEYBOARDCUES, 0, 1, 0);

                XmlConfigurator.Configure();

                Configuration conf = Configuration.getInstance();
                
                bool isPrintCommand = false;

                string argumento = "";
                try
                {
                    argumento = Environment.GetCommandLineArgs()[1];
                }
                catch (Exception e)
                {
                    logger.Error("El programa no recibió el parámetro esperado", e);
                    MessageBox.Show("Este programa requiere que se le pase un parametro de accion a realizar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                //***************************************************************
                // 1) Validacion si es conf regional Argentina
                //***************************************************************
                if (!POIutils.isRegionalConfArgentina())
                {
                    return;
                }
                

                logger.Info("Comienzo de Llamada al programa con argumento:" + (argumento != null?argumento:"nulo" ));


                //***************************************************************
                // 2) SET EXCEPCIONES DEL PROXY
                //***************************************************************
                // IMPORTANTE:
                // setProxyException y tarjetas. Se usan para el inicio de la aplicacion

                if (argumento != null && argumento.Equals("setProxyException"))
                {
                    RegistryProxyModifier.setearExcepcionesDeProxy();
                    NativeMethods.SystemParametersInfo(NativeMethods.SPI_SETKEYBOARDCUES, 0, 0, 0);
                    return;
                }


                //***************************************************************
                // 3) LECTURA DE AEROLINEAS PROPERTIES
                //***************************************************************
                bool estadoLecturaConf = ConfigurationReader.readAerolineasProperties();
                if (!estadoLecturaConf)
                {
                    logger.Error("Error al leer el archivo aerolineas.properties");
                    MessageBox.Show("Error al leer el archivo aerolineas.properties", "Error de lectura en archivo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }




                //***************************************************************
                // 4) LECTURA DE TABLA INTERACT CONFIGURATION EN BASE DE DATOS
                //***************************************************************
                bool estadoLecturaInteractConfig = ConfigurationReader.readInteactConfiguration();
                if (!estadoLecturaInteractConfig)
                {
                    logger.Error("[readInteactConfiguration] - Error conexion con los Web Services ARSA");
                    MessageBox.Show("Error conexion con los WS o base de datos", "Error conexion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                //***************************************************************
                //5) secretKey (encriptar / desincriptar )
                //***************************************************************
                string secretKey = "";
                try
                {
                    secretKey = Environment.GetCommandLineArgs()[2];
                    conf.secretKey = secretKey;
                }
                catch (Exception)
                {

                }

                //***************************************************************
                //6) Copias de cupones a Imprimir 
                //***************************************************************
                int copies = 1;
                try
                {
                    copies = int.Parse(Environment.GetCommandLineArgs()[3]);
                }
                catch (Exception)
                {
                    copies = 1;
                }


                //***************************************************************
                //7) argumento = "configuration"  
                /* 
                    Setea Caja, Ciudad, idPrinter, Sucursal, Puesto en aerolinas.properties
                */
                //***************************************************************
                if (argumento != null && argumento.Equals("configuration"))
                {

                    FrmConfiguracionPOI frmConf = new FrmConfiguracionPOI();
                    while ( !frmConf.isValid)
                    {
                        frmConf.loadFrm();
                        frmConf.ShowDialog();
                    }
                    return;
                }

                //************************************************************************************
                //8) isPrintCommand = true
                // ( argumento = "printItinerary"  o "printVoucher" o "rePrintCupons")
                //*************************************************************************************

                if (argumento != null && (argumento.Equals("printItinerary") || argumento.Equals("printVoucher") || argumento.Equals("rePrintCupons")))
                {
                    isPrintCommand = true;
                }

                
                //***************************************************************
                // 9) OBTENER CONFIGURACION DE VTOL de archivo VTOLConfig 
                //    (Solo para venta presente Pinpad (VTL)
                //***************************************************************
                if (conf.tipoAuth == TipoAutorizador.VTOL)
                {
                    string VTOLFileConfig = Application.StartupPath + "\\" + conf.vtolPosClientLibFolder + "\\config\\VTOLConfig.config";

                    bool estadoLecturaConfVtol = VTOLConfigurationFile.getConfiguration(VTOLFileConfig);

                    if (!estadoLecturaConfVtol)
                    {
                        logger.Error("Error al leer el archivo de configuracion de VTOL");
                        MessageBox.Show("Error al leer el archivo de configuracion de VTOL", "Error de lectura en archivo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (!conf.isVtolServiceEnabled)
                    {
                        string procId = ConfigurationReader.getValueProperties("ar.qik.vtol.procId");
                        string command = "custom_start.cmd";
                        if (procId == null || !CmdCommand.IsRunning(int.Parse(procId)))
                        {
                            int procIdNew = CmdCommand.ExecuteBAT(command);
                            if (procIdNew != 0)
                            {
                                ConfigurationReader.setValueProperties("ar.qik.vtol.procId", procIdNew.ToString());
                            }
                        }
                    }

                    Autorization.VTOL.VTOLIntegrator integrator = Autorization.VTOL.VTOLIntegrator.Instance;
                    if (!integrator.getConfiguration(VTOLFileConfig))
                    {
                        logger.Error("Error al obtener configuracion de VTOL server");
                        MessageBox.Show("al obtener configuracion de VTOL server", "Error VTOL Autorizator", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;

                    }




                    //***************************************************************
                    // VALIDACION DE TERMINAL
                    //***************************************************************
                    if (!isPrintCommand && argumento.Equals("tarjetas"))
                    {
                        // VERIFICO QUE LA TERMINAL ESTE BIEN CONFIGURADA
                        Terminal terminal = conf.getTerminal();
                        if (!terminal.isTerminalValid())
                        {
                            logger.Error("Error en el archivo aerolineas.properties configurando la terminal de VTOL");
                            MessageBox.Show("Error en el archivo aerolineas.properties configurando la terminal de VTOL", "Verifique campos de configuración", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        else
                        {
                            WebServiceTerminal ws = new WebServiceTerminal(terminal);
                            TerminalResponse terminalResponse = WebResponseParser.parseXMLInsertTerminal(ws.getResponse());
                            if (terminalResponse != null && terminalResponse.status.Equals("TERMINAL_EXISTE"))
                            {
                                logger.Error("La terminal que trata de configurar en esta maquina ya fue asignada");
                                MessageBox.Show("La terminal que trata de configurar en esta maquina ya fue asignada. Caja: " + terminal.caja + " y Sucursal: " + terminal.sucursal + " MacAddress: " + terminalResponse.terminalEncontrada.macAddress, "Verifique campos de configuración", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;

                            }
                        }
                    }

                }
                //***************************************************************
                // FIN OBTENER CONFIGURACION DE VTOL de archivo VTOLConfig 
                //***************************************************************


                //***************************************************************
                // 10) ( argumento = "tarjetas" )
                //***************************************************************

                if (argumento.Equals("tarjetas"))
                {
                    //Graba archivo paises y tarjetas
                    EntityWriter.writeFileTarjetas();
                    EntityWriter.writeFilePaises();

                    /*if (conf.tipoAuth == TipoAutorizador.VTOL_CALLCENTER)
                    {
                        //2) Si es VCC: inicializamos el Repositorio de Mitrol (IVRPAGOS)
                        MitrolController mitrolController = new MitrolController();
                        AsyncHelper.RunSync(() => mitrolController.cleanRepository());
                    }
                    */
                    return;
                }

                if (!argumento.Equals("tarjetas"))
                {
                    //Graba archivo paymentForms.properties
                    PaymentFormWriter.deleteAndSaveDefault();
                }

                switch (argumento)
                {
                    case "cashier":
                        if (conf.tipoAuth == TipoAutorizador.POS_INGENICO)
                            Application.Run(new FrmCajeros());
                        else
                            Application.Run(new FrmOnePayRed(false));
                        break;

                    case "payment":
                        Application.Run(new FrmPago());
                        break;

                    case "void":
                        Application.Run(new FrmAnulacion());
                        break;
                    case "voidAlone":
                        Application.Run(new FrmAnulacionAlone());
                        break;
                    case "refundAlone":
                        Application.Run(new FrmDevolucionAlone());
                        break;
                    case "refund":
                        Application.Run(new FrmDevolucion());
                        break;

                    case "exchange":
                        Application.Run(new FrmCanje());
                        break;

                    case "exchangeNoi":
                        Application.Run(new FrmVerificarPromos(false));
                        break;

                    case "emdIntereses":
                        FrmMostrarMsg frmEMDIntereses = new FrmMostrarMsg("Emitiendo EMD Intereses...");
                        frmEMDIntereses.Show();
                        frmEMDIntereses.emitirEMD();
                        frmEMDIntereses.Close();
                        break;

                    case "voidTkt":
                        FrmMostrarMsg frmVoidTkts = new FrmMostrarMsg("Voideando Tickets...");
                        frmVoidTkts.Show();
                        frmVoidTkts.voidEmision();
                        frmVoidTkts.Close();
                        break;

                    case "others":
                        Application.Run(new FrmOtrasOperaciones());
                        break;
                    case "lote":
                        Application.Run(new FrmCierreLote());
                        break;

                    case "changePrinter":
                        List<string> lsPrinter = conf.printerList;
                        if (lsPrinter.Count > 1)
                        {
                            Application.Run(new FrmChangePrinter());
                        }
                        else
                        {
                            MessageBox.Show("POI con VTOL: Usted está asignado a la impresora: " + conf.nombreImpresora + " si desea cambiar, comuniquese con Soporte de Sistemas de su Base.", "Cambiar Impresora", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        break;

                    case "changePrinterInit":

                        List<string> lsPrinters = conf.printerList;
                        if (lsPrinters.Count > 1)
                        {
                            Application.Run(new FrmChangePrinter());
                        }

                        break;


                    case "printItinerary":
                        List<Pnr> cargar = VBrequestReader.leerPnrs();
                        foreach (Pnr aux in cargar)
                        {
                            FrmImprimiendo formImprimir = new FrmImprimiendo();
                            formImprimir.Show();

                            PrintItin imprimir = new PrintItin(false, aux);
                            imprimir.infoPnr = aux;
                            PrinterHelper ph = new PrinterHelper(imprimir.devolverCupon());

                            if (argumento.Equals("printItineraryPax"))
                            {
                                imprimir.setNombreOperacion("ITINERARIO PARA PAX");
                            }

                            if (argumento.Equals("printItineraryCashier"))
                            {
                                imprimir.setNombreOperacion("ITINERARIO PARA CAJERO");
                            }


                            ph.printCupon(imprimir , copies , formImprimir);

                            formImprimir.Close();
                        }
                        
                        break;

                    case "getCuil":
                        FrmAveriguarCuil frmCuil = new FrmAveriguarCuil();
                        frmCuil.ShowDialog();
                        break;

                    //rePrintCupons
                    case "rePrintCupons":

                        FrmReimpresionCupones frm = new FrmReimpresionCupones();
                        frm.ShowDialog();
                        // Comment
                        break;
                    case "printVoucher":

                        FrmImprimiendo formImprimiendo = new FrmImprimiendo();
                        formImprimiendo.Show();

                        Voucher voucher = VBrequestReader.leerVoucher();
                        PrintVoucher imprimirVoucher = new PrintVoucher(false, voucher);
                        PrinterHelper phVoucher = new PrinterHelper(imprimirVoucher.devolverCupon());
                        phVoucher.printCupon(imprimirVoucher , copies, formImprimiendo);

                        formImprimiendo.Close();
                        // Comment
                        break;

                    case "resetPinpad":

                        if (conf.tipoAuth == TipoAutorizador.VTOL)
                        {
                            Autorizator auth = AutorizatorFactory.getAutorizator();
                            auth.cancelarTransaccion();
                        }
                        break;
                    case "killServer":
                        if (conf.tipoAuth == TipoAutorizador.VTOL) { 
                            VTOLFullLibrary.stopServer();
                        }
                        break;
                    case "ivr":
                        Application.Run(new FrmIVR());
                        break;

                    default:
                        logger.Error("Parámetro inválido:" + argumento);
                        MessageBox.Show("El argumento suministrado no es valido, el programa se cerrará.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
                NativeMethods.SystemParametersInfo(NativeMethods.SPI_SETKEYBOARDCUES, 0, 0, 0);
                Application.ExitThread();

            }
            catch (Exception e)
            {
                logger.Error("Error inesperado: " + e.StackTrace);
                MessageBox.Show("Error inesperado en el Sistema: Excepcion: " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

}
