using CodenameNightwing.Autorization;
using CodenameNightwing.Config;
using CodenameNightwing.Printer.Exceptions;
using CodenameNightwing.Printer.Models;
using System;
using System.Linq;
using System.Drawing;
using System.Drawing.Printing;
using System.Management;
using System.Printing;
using System.Windows.Forms;
using CodenameNightwing.Forms;
using CodenameNightwing.BusinessLogic;

namespace CodenameNightwing.Printer
{
    class PrinterHelper 
    {
        private string _printerName;
        public string printerName
        {
            get { return _printerName; }
            set { _printerName = value; }
        }


        private string _printerModelName;
        public string printerModelName
        {
            get { return _printerModelName; }
            set { _printerModelName = value; }
        }

        private PrintQueue _printerQueue = null;
        public PrintQueue printerQueue
        {
            get { return _printerQueue; }
            set { _printerQueue = value; }
        }


        private string[] _lineasAImprimir;
        public string[] lineasAImprimir
        {
            get { return _lineasAImprimir; }
            set { _lineasAImprimir = value; }
        }

        public PrinterHelper(string[] aImprimir)
        {
            printerName = Configuration.getInstance().nombreImpresora;
            printerModelName = Configuration.getInstance().modeloImpresora;
            lineasAImprimir = aImprimir;
        }

        public PrinterHelper()
        {
            printerName = Configuration.getInstance().nombreImpresora;
            printerModelName = Configuration.getInstance().modeloImpresora;
        }

        public static PrintQueue getPrinterQueue()
        {

            /*if (printerQueue != null) {
                return printerQueue;
            }*/
            PrintQueue printQueue = null;
            string printerName = Configuration.getInstance().nombreImpresora;

            if (printerName.Contains("\\"))
            {
                string auxName = printerName.Substring(2);
                string serverName = auxName.Split('\\')[0];
                string printerRemoteName = auxName.Split('\\')[1];
                PrintServer ps = new PrintServer(@"\\" + serverName);
                printQueue = ps.GetPrintQueue(printerRemoteName);

            }
            else
            {

                LocalPrintServer localPrintServer = new LocalPrintServer();
                // Retrieving collection of local printer on user machine
                PrintQueueCollection localPrinterCollection = localPrintServer.GetPrintQueues();
                System.Collections.IEnumerator localPrinterEnumerator = localPrinterCollection.GetEnumerator();

                while (localPrinterEnumerator.MoveNext())
                {
                    // Get PrintQueue from first available printer
                    printQueue = (PrintQueue)localPrinterEnumerator.Current;
                    if (!printQueue.Name.Contains(printerName))
                    {
                        printQueue = null;
                    }
                    else
                    {
                        break;
                    }
                }
                /*this.printerQueue = printQueue;

                return this.printerQueue;*/
            }
            return printQueue;
        }


        public bool deleteFromQueue()
        {

            MessageBox.Show("Canceling printing jobs");
            var ps = new PrintServer();

            var queues = ps.GetPrintQueues();

            var pq = queues.Where(t => t.FullName.Contains(this.printerModelName));

            if (pq == null)
            {
                MessageBox.Show("Print queue is null");
                return false;
            }

            int i = 0;
            foreach (var queue in pq)
            {
                MessageBox.Show("Queue " + i);
                i++;

                queue.Refresh();
                queue.Purge();

            }
            MessageBox.Show("Canceling printing jobs finished");

            return true;
        }

        
        /// <summary>
        /// Cancel the print job. This functions accepts the job number.
        /// An exception will be thrown if access denied.
        /// </summary>
        /// <param name="printJobID">int: Job number to cancel printing for.</param>
        /// <returns>bool: true if cancel successfull, else false.</returns>
        public bool deleteJobs( )
        {


            // Variable declarations.
            bool isActionPerformed = false;
            string searchQuery;
            System.String jobName;
            ManagementObjectSearcher searchPrintJobs;
            ManagementObjectCollection prntJobCollection;
            try
            {
                // Query to get all the queued printer jobs.
                searchQuery = "SELECT * FROM Win32_PrintJob";
                // Create an object using the above query.
                searchPrintJobs = new ManagementObjectSearcher(searchQuery);
                // Fire the query to get the collection of the printer jobs.
                prntJobCollection = searchPrintJobs.Get();

                // Look for the job you want to delete/cancel.
                foreach (ManagementObject prntJob in prntJobCollection)
                {
                    jobName = prntJob.Properties["Name"].Value.ToString();
                    if (jobName.Contains(this.printerName) || jobName.Contains(this.printerModelName))
                    {
                        try { 
                            prntJob.Delete();
                            prntJob.Dispose();
                            isActionPerformed = true;
                        }
                        catch ( Exception e)
                        {
                            Program.logger.Error("Error al borrar el JOB de printer: " + prntJob, e);
                        }

                    }
                }
                return isActionPerformed;
            }
            catch (Exception e)
            {
                Program.logger.Error("Error al borrar los JOBS de la printer", e);
                return false;
            }
        }

        public static int getNumberJobs()
        {
            // Variable declarations.
            int numberJobs = 0;
            string searchQuery;
            System.String jobName;
            char[] splitArr;
            int prntJobID;
            ManagementObjectSearcher searchPrintJobs;
            ManagementObjectCollection prntJobCollection;
            try
            {
                // Query to get all the queued printer jobs.
                searchQuery = "SELECT * FROM Win32_PrintJob";
                // Create an object using the above query.
                searchPrintJobs = new ManagementObjectSearcher("root\\CIMV2", searchQuery);
                // Fire the query to get the collection of the printer jobs.
                prntJobCollection = searchPrintJobs.Get();

                // Look for the job you want to delete/cancel.
                foreach (ManagementObject prntJob in prntJobCollection)
                {
                    jobName = prntJob.Properties["Name"].Value.ToString();
                    string printerName = Configuration.getInstance().nombreImpresora;
                    string printerModelName = Configuration.getInstance().modeloImpresora;
                    if (jobName.Contains(printerName) || jobName.Contains(printerModelName))
                    {
                        // Job name would be of the format [Printer name], [Job ID]
                        splitArr = new char[1];
                        splitArr[0] = Convert.ToChar(",");
                        // Get the job ID.
                        prntJobID = Convert.ToInt32(jobName.Split(splitArr)[1]);
                        // If the Job Id equals the input job Id, then cancel the job.
                        //if (prntJobID == printJobID)
                        //{
                        // Performs a action similar to the cancel
                        // operation of windows print console
                        numberJobs++;
                        //break;
                        //}
                    }
                }
                return numberJobs;
            }
            catch (Exception)
            {
                // Log the exception.
                return -1;
            }
        }
        /// <summary>
        /// 
        /// </summary>

        public static bool checkQueue(bool isJobInQueue)
        {

            bool isCheckedQueue = false;

            int numberJobs = getNumberJobs();
            if ((numberJobs == 0 && !isJobInQueue) || (numberJobs == 1 && isJobInQueue))
            {
                isCheckedQueue = true;
            }


            return isCheckedQueue;
        }


        public void printCupon(PrinterCupon imprimir , Form parent ) {
            this.printCupon( imprimir , 1 , parent );
        }

        public void printCupon(PrinterCupon imprimir, int copies , Form parent)
        {

            PrinterHelper ph = new PrinterHelper(imprimir.devolverCupon());

            try
            {
                TipoTransImpFallida estadoTransFallida = TipoTransImpFallida.NO_COMPLETADA;

                while (estadoTransFallida == TipoTransImpFallida.NO_COMPLETADA || estadoTransFallida == TipoTransImpFallida.REIMPRESION) {

                    bool impresionOk = false;
                    // Aca chequea que pudo imprimir y el trabajo esta en la cola
                    impresionOk = ph.imprimir( copies );


                    if (impresionOk)
                    {
                        MessageBox.Show("Impresión de " + imprimir.getNombreOperacion() + " correcta.", "Transaccion correcta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        estadoTransFallida = TipoTransImpFallida.IMPRESION_OK;
                    }
                    else
                    {
                        MessageBox.Show("Problemas con la impresión, verifique conexión, tapa y papel: " + Configuration.getInstance().nombreImpresora + "", "Transaccion incorrecta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        FrmImpresionFallidaComun frmImpreFallida = new FrmImpresionFallidaComun();
                        frmImpreFallida.ShowDialog( parent );
                        estadoTransFallida = frmImpreFallida.estadoTransFallida;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Problemas con la impresión, verifique conexión, tapa y papel : " + Configuration.getInstance().nombreImpresora + "", "Transaccion incorrecta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.logger.Error("Error impresión: ", e);
            }
            finally
            {
                ph.deleteJobs();
            }
        }

        public bool imprimir( int copies )
        {

            if (!string.IsNullOrEmpty(Configuration.getInstance().printCupons))
            {
                if (Configuration.getInstance().printCupons.Equals("NO"))
                {
                    return true;
                }
            }

            PrinterModel printer = PrinterModelFactory.getPrinterModel();
            printer.initializePrinter();
            
            return printer.printDocument(this.lineasAImprimir , copies);


        }

        public bool imprimir()
        {
            return this.imprimir(1);
        }
    }
}
