using CodenameNightwing.BusinessLogic;
using CodenameNightwing.Config;
using CodenameNightwing.IVR;
using CodenameNightwing.Varios;
using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodenameNightwing.Forms
{
    public partial class FrmIVR : Form
    {
        private TipoSeleccionIVR _seleccionIVR = TipoSeleccionIVR.CANCELAR;
        public static readonly ILog logger = LogManager.GetLogger(typeof(FrmIVR));

        private Transaccion _tran;
        private IVRManager ivrManager;
        

        public Transaccion tran
        {
            get { return _tran; }
            set { _tran = value; }
        }

        private Tarjeta _tarjetaIVR = null;

        public Tarjeta tarjetaIVR
        {
            get { return _tarjetaIVR; }
            set { _tarjetaIVR = value; }
        }


        public FrmIVR()
        {
            InitializeComponent();
            ivrManager = new IVRManager();


        }

        public TipoSeleccionIVR seleccionIVR
        {
            get { return _seleccionIVR; }
            set { _seleccionIVR = value; }
        }

        public FrmIVR(Transaccion trans):this()
        {
            this.tran = trans;
        }


        public void disableDerivar()
        {
            this.btnDerivar.Enabled = false;
        }

    
        private void btnCancelar_Click(object sender, System.EventArgs e)
        {
            seleccionIVR = TipoSeleccionIVR.CANCELAR;
            Close();
        }

        private void btnCortar_Click(object sender, EventArgs e)
        {
            ivrManager.cortarIVR();
            habilitarDerivar(false);
        }

        private async void btnDerivar_Click(object sender, System.EventArgs e)
        {

            seleccionIVR = TipoSeleccionIVR.DERIVAR_IVR;

            habilitarCortar();

            this.tarjetaIVR =  await callIVR(cmbIdioma.Text);

            if (this.tarjetaIVR != null)
                Close();
        }

        private void btnManual_Click(object sender, System.EventArgs e)
        {
            seleccionIVR = TipoSeleccionIVR.MANUAL;
            Close();
        }

        private void FrmIVR_Load(object sender, EventArgs e)
        {
            lblStopWatch.Text = (int.Parse(Configuration.getInstance().timeOutIVRPagos) * 60).ToString();

            string machineName = Configuration.getInstance().getTerminal().machineName;
            string backofficeWS = Configuration.getInstance().backofficeWS;
            if (backofficeWS.Contains(machineName)) {
                habilitarDerivar(true);
            }
            else {
                habilitarDerivar(Configuration.getInstance().btnIngresoTCManualEnable);
            }

            cargarIdiomasNPS();

            cmbIdioma.Text = "ESPAÑOL";

            cmbIdiomaNPS.SelectedItem = "es_AR";
            cmbIdiomaNPS.Text = "Spanish (Argentina)";


        }



        private void updateUI(IVRResponses ivrResp)
        {
            if (ivrResp.getStatus() != null)
            {
                lblStatus.Text = ivrResp.getStatus();
                lblStopWatch.Text = (ivrResp.timeElapsed/1000).ToString();
            }

            if (ivrResp.getNumeroTarjeta() != null)
            {
                completeImgCCNumber.Visible = true;
                imageWaitCCNumber.Visible = false;
            }
            if (ivrResp.getFechaExp() != null)
            {
                completeImageVencimiento.Visible = true;
                imageWaitVencimiento.Visible = false;
            }

            if (ivrResp.getCVC() != null)
            {
                completeImageCVC.Visible = true;
                imageWaitCVC.Visible = false;
            }
            
        }


        private async Task<Tarjeta> callIVR(string idioma)
        {
            Tarjeta tarjAux = null;
            try
            {

                var statusProgress = new Progress<IVRResponses>(ivrResp => updateUI(ivrResp));

                tarjAux = await Task.Run(() => ivrManager.obtenerTarjetaIVR(statusProgress, tran, EnumUtils.getIdioma(idioma)));

                if (tarjAux != null)
                {
                    tran.tarjeta.numero = tarjAux.numero;
                    tran.tarjeta.vencimiento = tarjAux.vencimiento;
                    tran.tarjeta.cvc = tarjAux.cvc;

                    return tran.tarjeta;
                }
                else
                {
                    logger.Info("callIVR()");
                    logger.Info("Error ivrManager.obtenerTarjetaIVR: No se puedo obtener tarjeta");
                    throw new WCentrixException("Error ivrManager.obtenerTarjetaIVR: No se puedo obtener tarjeta");
                }

            }

            catch (WCentrixException eMitrol)
            {
                string msg = eMitrol.getMsg();
                TipoMensaje tipoMsg = eMitrol.getTipoMsg();
                if (tipoMsg == TipoMensaje.ERROR)
                {
                    logger.Error(msg);
                    MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    logger.Info(msg);
                    MessageBox.Show(msg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                habilitarDerivar(true);


                return null;
            }

            catch (Exception e)
            {
                string msg = e.StackTrace + "Intente en forma manual";
                MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Error(msg);

                habilitarDerivar(true);

                return null;
            }

        }

        private void habilitarDerivar(bool btnManualEnable) {
            btnDerivar.Enabled = true;
            btnManual.Enabled = btnManualEnable;
            btnCancelar.Enabled = true;
            btnCortar.Enabled = false;

            completeImgCCNumber.Visible = false;
            imageWaitCCNumber.Visible = true;

            completeImageVencimiento.Visible = false;
            imageWaitVencimiento.Visible = true ;

            completeImageCVC.Visible = false;
            imageWaitCVC.Visible = true;

            grpEstados.Visible = false;
            grpNumeros.Visible = false;


        }

        private void cargarIdiomasNPS()
        {
            foreach (var item in loadIdiomaNPS())
            {
                this.cmbIdiomaNPS.AutoCompleteCustomSource.Add(item.descCodigoIdiomaNPS);
                cmbIdiomaNPS.Items.Add(item);
            }
        }


        private List<IdiomaNPS> loadIdiomaNPS()
        {
            try
            {
                List<IdiomaNPS> lAux = new List<IdiomaNPS>();
                lAux.Add(new IdiomaNPS("en_AU", "English(Australia)"));
                lAux.Add(new IdiomaNPS("en_CA", "English(Canada)"));
                lAux.Add(new IdiomaNPS("en_IE", "English(Ireland)"));
                lAux.Add(new IdiomaNPS("en_NZ", "English(New Zeland)"));
                lAux.Add(new IdiomaNPS("en_US", "English(United States)"));
                lAux.Add(new IdiomaNPS("es_AR", "Spanish (Argentina)"));
                lAux.Add(new IdiomaNPS("es_CO", "Spanish (Colombia)"));
                lAux.Add(new IdiomaNPS("es_ES", "Spanish(Spain)"));
                lAux.Add(new IdiomaNPS("es_MX", "Spanish(Mexico)"));
                lAux.Add(new IdiomaNPS("pt_BR", "Portuguese(Brazil)"));
                lAux.Add(new IdiomaNPS("pt_PT", "Portuguese(Portugal)"));
                lAux.Add(new IdiomaNPS("fr_BE", "French(Belgium)"));
                lAux.Add(new IdiomaNPS("fr_CA", "French(Canada)"));
                lAux.Add(new IdiomaNPS("fr_FR", "French(France)"));
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

    

        private void habilitarCortar()
        {
            btnDerivar.Enabled = false;
            btnManual.Enabled = false;
            btnCancelar.Enabled = false;
            btnCortar.Enabled = true;

            grpEstados.Visible = true;
            grpNumeros.Visible = true;


        }

        private void btnEnlace_Click(object sender, EventArgs e)
        {
            seleccionIVR = TipoSeleccionIVR.ENLACE;

            if (cmbIdiomaNPS.SelectedItem != null)
                this.tran.idiomaFormNPS = ((IdiomaNPS)cmbIdiomaNPS.SelectedItem).codigoIdiomaNPS;
            Close();


        }

        private void cmbIdioma_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbIdiomaNPS_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}


