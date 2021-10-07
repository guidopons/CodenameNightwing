using CodenameNightwing.BusinessLogic;
using CodenameNightwing.Varios;
using log4net;
using System;
using System.Windows.Forms;

namespace CodenameNightwing.Forms
{
    public partial class FrmEnlace : Form
    {
        public static readonly ILog logger = LogManager.GetLogger(typeof(FrmEnlace));

        private TipoSeleccionEnlace _seleccionEnlace = TipoSeleccionEnlace.CANCELAR;

        private string enlace = null;

        public Transaccion tran { get; set; }


        public FrmEnlace()
        {
            InitializeComponent();

        }

        public TipoSeleccionEnlace seleccionEnlace 
        {
            get { return _seleccionEnlace; }
            set { _seleccionEnlace = value; }
        }

        public FrmEnlace(Transaccion trans, string enlace):this()
        {
            this.tran = trans;
            this.enlace = enlace;
        }



        private void FrmEnlace_Load(object sender, EventArgs e)
        {
            this.txtEnlace.Text = this.enlace;

            //Datos de la transaccion
            this.lblTarjeta.Text = "Tarjeta: " + tran.tarjeta.descripcion;
            this.lblImporteTotal.Text = "Importe Total: " +  tran.importeTotal.ToString();
            this.lblCuotas.Text = "Cuotas: " +  tran.cantCuotas.ToString();
            this.lblIntereses.Text = "Interes: " + tran.interes.ToString();

            string strImpoCuotas = (tran.cantCuotas != 0 ? (tran.importeTotal / tran.cantCuotas).ToString("######0.00") : "");
            this.lblImporteCuotas.Text = "Importe por Cuota: " + strImpoCuotas;
            
        }


        private void btnCopyText_Click(object sender, EventArgs e)
        {
            
            string strImpoCuotas = (tran.cantCuotas != 0 ? (tran.importeTotal / tran.cantCuotas).ToString("######0.00") : "");
            
            string strMensaje = string.Format("Usted selecciono:{0}Tarjeta: {1}{0}Importe Total: {2}{0}Cuotas: {3}{0}Intereses: {4}{0}Importe por Cuota: {5}", Environment.NewLine, tran.tarjeta.descripcion, tran.importeTotal.ToString() , tran.cantCuotas.ToString(), tran.interes.ToString(), strImpoCuotas);
            
            Clipboard.SetDataObject(strMensaje);

            MessageBox.Show("Plan seleccionado copiado al portapapeles", "Plan seleccionado copiado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.seleccionEnlace = TipoSeleccionEnlace.COPIAR_TEXTO;
            Close();
        }

        private void btnCopyEnlace_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Clipboard.SetText(enlace);
            MessageBox.Show("Enlace copiado al portapapeles", "Enlace Copiado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.seleccionEnlace = TipoSeleccionEnlace.COPIAR_ENLACE;
            Close();
        }
    
        private void btnVerificar_Click(object sender, EventArgs e)
        {
            this.seleccionEnlace = TipoSeleccionEnlace.VERIFICAR;
            Close();
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            DialogResult opcion = MessageBox.Show("Atención: El enlace de pago generado perderá validez y si la transacción fue aprobada producirá la anulación.", "¿Está seguro de que desea volver?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
   
            if (opcion == DialogResult.Yes)
            {
                System.Windows.Forms.Clipboard.Clear();
                this.seleccionEnlace = TipoSeleccionEnlace.CANCELAR;
                Close();

            }
        }

        private void txtEnlace_TextChanged(object sender, EventArgs e)
        {

        }
    }
}


