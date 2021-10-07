using System;
using com.epson.pos.driver;
using System.Drawing.Printing;
using System.Windows.Forms;
using System.Drawing;
using CodenameNightwing.Config;
using CodenameNightwing.Printer.Exceptions;
using System.Management;
using System.Diagnostics;
using log4net;

namespace CodenameNightwing.Printer.Models
{
    class PrinterEpson : PrinterModel
    {

        private StatusAPI m_objAPI = null;
        Boolean isFinish;
        Boolean cancelErr;
        ASB printStatus;
        string[] lineasAImprimir;
        private int copies;
        public static readonly ILog logger = LogManager.GetLogger(typeof(PrinterEpson));

        public override void initializePrinter()
        {

            m_objAPI = new StatusAPI();

            this.printerName = Configuration.getInstance().nombreImpresora;

            isFinish = false;
            cancelErr = false;
            printStatus = ASB.ASB_UNRECOVER_ERR;

                
        }

        public override bool printDocument(string[] lineasAImprimir , int copies)
        {
            this.copies = copies;
            this.lineasAImprimir = lineasAImprimir;
            PrintDocument pdPrint = new PrintDocument();
            PrintController printController = new StandardPrintController();
            pdPrint.PrintController = printController;
            pdPrint.PrintPage += new PrintPageEventHandler(pdPrint_PrintPage);
            pdPrint.PrinterSettings.PrinterName = this.printerName;
            pdPrint.PrinterSettings.Copies = (short)copies;

            try
            {

                // Open a printer status monitor for the selected printer.

                // PASO 1: ABRIR MONITOR
                logger.Info("Abriendo monitor de printer EPSON: " + pdPrint.PrinterSettings.PrinterName);
                if (m_objAPI.OpenMonPrinter(OpenType.TYPE_PRINTER, pdPrint.PrinterSettings.PrinterName) == ErrorCode.SUCCESS)
                {
                    logger.Info("Se abrio el monitor de la EPSON: " + pdPrint.PrinterSettings.PrinterName);

                    // PASO 2: LOCK PRINTER
                    logger.Info("Comienzo de Lock de Printer EPSON");
                    if ( m_objAPI.LockPrinter(10) != ErrorCode.SUCCESS)
                    {
                        string msg = "La impresora está en uso, intente nuevamente reimprimir";
                        throw new PrinterException(msg);
                    }else
                    {
                        logger.Info("Se lockeo la EPSON: " + pdPrint.PrinterSettings.PrinterName);
                    }

                    // Associate the created callback function to the event handler of StatusAPI .
                    // PASO 3: STATUS CALLBACK EX
                    //m_objAPI.StatusCallback += new StatusAPI.StatusCallbackHandler(StatusMonitoring);
                    m_objAPI.StatusCallbackEx += new StatusAPI.StatusCallbackHandlerEx(StatusMonitoringEx);
                    
                    // PASO 4: SET STATUS BACK
                    // Set the callback function that will monitor printer status.
                    if (m_objAPI.SetStatusBack() == ErrorCode.SUCCESS)
                    {
                        logger.Info("SetStatusBack de la EPSON correcta");
                        isFinish = false;
                        cancelErr = false;

                        if (pdPrint.PrinterSettings.IsValid )
                        {
                            pdPrint.DocumentName = "Cupon de Tarjeta";

                            ASB dwStatus = m_objAPI.Status;

                            // Start printing.
                            pdPrint.Print();
                            logger.Info("Se termino la ejecucion de Print de la EPSON");

                            if (m_objAPI.UnlockPrinter() != ErrorCode.SUCCESS)
                            {
                                throw new PrinterException("No se pudo realizar el unlock de la printer");
                            }
                            else
                            {
                                logger.Info("Unlock de la Printer EPSON correcta");
                            }
                            
                            // Wait until callback function will say that the task is done.
                            // When done, end the monitoring of printer status.

                            Stopwatch s = new Stopwatch();
                            s.Start();

                            // timeout esperando la printer
                            int epsonTimeOut = 15000;
                            try
                            {
                                epsonTimeOut = int.Parse(Configuration.getInstance().timeOutEpsonPrinter);
                            }
                            catch ( Exception)
                            {
                                epsonTimeOut = 15000;
                            }

                            logger.Info("Timeout de la EPSON es: " + epsonTimeOut);

                            do
                            {
                                if (isFinish)
                                    // End the monitoring of printer status.
                                    m_objAPI.CancelStatusBack();
                            } while (!isFinish && s.Elapsed < TimeSpan.FromMilliseconds(epsonTimeOut));

                            s.Stop();

                            logger.Info("Termino por Finish: " + isFinish + " Tiempo transcurrido: " + s.Elapsed);

                            // Display the status/error message.
                            string msg = checkStatusMessage();

                            logger.Info("Print Status" + printStatus);

                            

                            // If roll paper end is detected, cancel the print job
                            if (!this.printStatus.ToString().Contains(ASB.ASB_PRINT_SUCCESS.ToString()))
                            {
                                PrinterHelper ph = new PrinterHelper();
                                ph.deleteJobs();
                            }
                                

                            // If an error occurred, restore the recoverable error.
                            if (cancelErr)
                                m_objAPI.CancelError();

                            if ( msg != null)
                            {
                                throw new PrinterException(msg);
                            }
                        }
                        else
                        {
                            string msg = "Impresora no esta disponible. Printer: " + this.printerName;
                            throw new PrinterException(msg);
                        }
                            
                    }
                    else
                    {
                        string msg = "Error al setear la funcion de callback.  Printer: " + this.printerName;
                        throw new PrinterException(msg);
                    }
                        
                }
                else
                {
                    string msg = "Error al abrir el monitor de status. Printer: " + this.printerName;
                    throw new PrinterException(msg);
                }

            }
            catch (PrinterException exPrinter)
            {
                cancelErr = true;
                logger.Error(exPrinter.mensaje);
                MessageBox.Show("Problemas con la impresión, verifique lo siguiente : " + exPrinter.mensaje, "Problema con la impresión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception exPrinter)
            {
                cancelErr = true;
                logger.Error(exPrinter);
                MessageBox.Show("Problemas con la impresión" , "Problema con la impresión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ClosePrinter();
            }
            return !cancelErr;
        }


        private void ClosePrinter()
        {
            // Close Printer Monitor.
            if (m_objAPI != null && m_objAPI.IsValid) { 
                if (m_objAPI.CloseMonPrinter() != ErrorCode.SUCCESS)
                {
                    string msg = "Error al cerrar el status monitor. Printer: " + this.printerName;
                    logger.Error(msg);
                    throw new PrinterException(msg);
                }
                else
                {
                    logger.Info("Impresora Cerrada correctamente");
                }
            }

        }

        // The event handler function when pdPrint.Print is called.
        // This is where the actual printing of sample data to the printer.
        private void pdPrint_PrintPage(object sender, PrintPageEventArgs e)
        {

            float x, y, lineOffset;

            e.Graphics.PageUnit = GraphicsUnit.Point;

            Font printFont = new Font("Lucida Console", (float)8, FontStyle.Regular, GraphicsUnit.Point); // Substituted to FontA Font
            // Print the receipt text
            lineOffset = printFont.GetHeight(e.Graphics);
            x = 0;
            y = 0;

            foreach (var line in lineasAImprimir)
            {
                if ( line != null) { 
                    string auxLine = convertEscPosCommands(line , out printFont);
                    e.Graphics.DrawString(auxLine, printFont, Brushes.Black, x, y);
                    y += lineOffset;
                }
            }

            e.HasMorePages = false;
        }


        public string centrarTexto(string line )
        {
            int cantEspaciosAInsertar = (PrinterCupon.CANT_CARACT_POR_FILA - line.Length) / 2;
            string devolver = "";
            if (cantEspaciosAInsertar > 0)
                devolver = devolver.Insert(0, new string(' ', cantEspaciosAInsertar));
            devolver += line;
            return devolver;
        }

        private string convertEscPosCommands( string line,  out Font printFont ) {

            // Instantiate font objects used in printing.
            printFont = new Font("Lucida Console", (float)8, FontStyle.Regular, GraphicsUnit.Point); // Substituted to FontA Font
            Font printFontUnderline = new Font("Lucida Console", (float)8, FontStyle.Underline, GraphicsUnit.Point); // Substituted to FontA Font
            Font printFontBold = new Font("Lucida Console", (float)8, FontStyle.Bold, GraphicsUnit.Point); // Substituted to FontA Font
            Font printFontBigChar = new Font("Lucida Console", (float)12, FontStyle.Bold, GraphicsUnit.Point); // Substituted to FontA Font

            if ( line.Contains( ESCPOS.justificacionIzquierda))
            {
                line = line.Replace(ESCPOS.justificacionIzquierda, "");
            }

            if (line.Contains(ESCPOS.justificacionCentro))
            {
                line = line.Replace(ESCPOS.justificacionCentro, "");
                line = centrarTexto( line );
            }


            if (line.Contains(ESCPOS.underlineStart))
            {
                line = line.Replace(ESCPOS.underlineStart, "");
                line = line.Replace(ESCPOS.underlineEnd, "");
                printFont = printFontUnderline;
            }

            if (line.Contains(ESCPOS.bigCharStart))
            {
                line = line.Replace(ESCPOS.bigCharStart, "");
                line = line.Replace(ESCPOS.bigCharEnd, "");
                printFont = printFontBigChar;
            }

            if (line.Contains(ESCPOS.fontBold))
            {
                line = line.Replace(ESCPOS.fontBold, "");
                printFont = printFontBold;
            }

            if (line.Contains(ESCPOS.offlineRelieve))
            {
                line = line.Replace(ESCPOS.offlineRelieve, "");
            }


            return line;
        }

        private string checkStatusMessage()
        {
            if (this.printStatus.ToString().Contains(ASB.ASB_PRINT_SUCCESS.ToString()))
                return null;
            else if (this.printStatus.ToString().Contains(ASB.ASB_NO_RESPONSE.ToString()))
                return "No responde la impresora";
            else if (this.printStatus.ToString().Contains(ASB.ASB_COVER_OPEN.ToString()))
                return "Tapa abierta";
            else if (this.printStatus.ToString().Contains(ASB.ASB_AUTOCUTTER_ERR.ToString()))
                return "Error con el auto cortador. autocutter";
            else if (this.printStatus.ToString().Contains(ASB.ASB_PAPER_END.ToString()))
                return "Sensor de papel: papel no presente";
            else
                return "No se reconoce el error en la impresora, verifique o reinicie para que tome la operación";

        }

        // @Deprecated
        // Usado anteriormente como en el ejemplo
        public void StatusMonitoring(ASB dwStatus)
        {
            if (dwStatus.ToString().Contains(ASB.ASB_PRINT_SUCCESS.ToString()))
            {
                printStatus = dwStatus;
                isFinish = true;
            }
            if (dwStatus.ToString().Contains(ASB.ASB_NO_RESPONSE.ToString()) ||
                dwStatus.ToString().Contains(ASB.ASB_COVER_OPEN.ToString()) ||
                dwStatus.ToString().Contains(ASB.ASB_AUTOCUTTER_ERR.ToString()) ||
                dwStatus.ToString().Contains(ASB.ASB_PAPER_END.ToString()))
            {
                printStatus = dwStatus;
                isFinish = true;
                cancelErr = true;
            }
        }

        public void StatusMonitoringEx(ASB dwStatus , string port)
        {

            logger.Info("Llamanda de Status Callback:" + dwStatus);

            if (dwStatus.ToString().Contains(ASB.ASB_PRINT_SUCCESS.ToString()))
            {
                printStatus = dwStatus;
                isFinish = true;
            }

            if (dwStatus.ToString().Contains(ASB.ASB_NO_RESPONSE.ToString()) ||
                dwStatus.ToString().Contains(ASB.ASB_COVER_OPEN.ToString()) ||
                dwStatus.ToString().Contains(ASB.ASB_AUTOCUTTER_ERR.ToString()) ||
                dwStatus.ToString().Contains(ASB.ASB_PAPER_END.ToString()))
            {
                printStatus = dwStatus;
                isFinish = true;
                cancelErr = true;
            }
        }
    }
}
