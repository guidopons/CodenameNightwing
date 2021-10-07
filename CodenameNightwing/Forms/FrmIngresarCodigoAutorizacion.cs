using CodenameNightwing.BusinessLogic;
using System;
using System.Windows.Forms;

namespace CodenameNightwing.Forms
{
    public partial class FrmIngresarCodigoAutorizacion : Form
    {
        private bool ingresoCorrecto = false;

        public bool isCorrect()
        {
            return ingresoCorrecto;
        }

        private Transaccion _tran;
        public Transaccion tran
        {
            get { return _tran; }
            set { _tran = value; }
        }

        public FrmIngresarCodigoAutorizacion(Transaccion aIngresarCodAutorizacion)
        {
            InitializeComponent();
            tran = aIngresarCodAutorizacion;
            
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (txtNumeroAutorizacion.MaskCompleted)
            {
                tran.numAutorizacion = txtNumeroAutorizacion.Text;
                ingresoCorrecto = true;
                Close();
            }
            else
                MessageBox.Show("Debe completar el codigo de autorizacion brindado por el operador de la tarjeta", "Error de validacion", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            ingresoCorrecto = false;
            Close();
        }

        private void FrmIngresarCodigoAutorizacion_Load(object sender, EventArgs e)
        {

        }

        private void txtNumeroAutorizacion_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }
    }
}
