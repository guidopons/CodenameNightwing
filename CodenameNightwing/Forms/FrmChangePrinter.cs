using CodenameNightwing.Autorization;
using CodenameNightwing.BusinessLogic;
using CodenameNightwing.Config;
using CodenameNightwing.Exceptions;
using CodenameNightwing.FileManager;
using CodenameNightwing.Printer;
using CodenameNightwing.Printer.Cupones;
using CodenameNightwing.Varios;
using CodenameNightwing.WebServices;
using CodenameNightwing.WebServices.WSEspecificos;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace CodenameNightwing.Forms
{
    public partial class FrmChangePrinter : Form
    {

        public static readonly ILog logger = LogManager.GetLogger(typeof(FrmChangePrinter));

        private bool withExit = true;


        public FrmChangePrinter():this(true)
        {

        }

        public FrmChangePrinter( bool withExit )
        {
            InitializeComponent();
            Icon = Properties.Resources.logo_con_borde_grueso;
            this.withExit = withExit;

            List<string> lsPrinter = Configuration.getInstance().printerList;
            if (lsPrinter.Count > 1)
            {
                lblPrinterList.Visible = true;
                cmbPrinterList.Visible = true;
                foreach (string printerItem in lsPrinter)
                    cmbPrinterList.Items.Add(printerItem);
                cmbPrinterList.SelectedIndex = cmbPrinterList.FindStringExact(Configuration.getInstance().nombreImpresora);
            }
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            ejecutar();
        }


        private void FrmOtrasOperaciones_FormClosing(object sender, FormClosingEventArgs e)
        {
            if ( withExit )
                Application.ExitThread();
        }

        private void ejecutar()
        {
            try
            {
                string selected = this.cmbPrinterList.GetItemText(this.cmbPrinterList.SelectedItem);
                ConfigurationReader.setValueProperties("ar.qik.printer.printerName", selected);
                MessageBox.Show("Se actualizo la impresora correctamente a: " + selected, "Cambiar Impresora", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Configuration.getInstance().nombreImpresora = selected;

                Activate();
            }
            catch ( Exception e)
            {
                logger.Error("No se pudo cambiar la impresora seleccionada", e);
            }
            
        }

       

        private void btnVolver_Click(object sender, EventArgs e)
        {
            if(withExit)
                Application.ExitThread();
            Close();
        }


    
    }
}
