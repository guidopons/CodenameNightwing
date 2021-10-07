using CodenameNightwing.BusinessLogic;
using CodenameNightwing.FileManager;
using System;
using System.Windows.Forms;

namespace CodenameNightwing.Forms
{
    public partial class FrmIngresarBin : Form
    {
        private bool ingresoCorrecto = false;

        public bool isCorrect()
        {
            return ingresoCorrecto;
        }

        private string _bin;
        public string bin
        {
            get { return _bin; }
            set { _bin = value; }
        }

        private TarjetaCajero _tarjeta;
        public TarjetaCajero tarjeta
        {
            get { return _tarjeta; }
            set { _tarjeta = value; }
        }

        public FrmIngresarBin()
        {
            InitializeComponent();
            tarjeta = null;
        }

        private void cargarTarjetas()
        {
            foreach (var item in EntityLoader.loadTarjetas())
            {
                cmbTarjetas.AutoCompleteCustomSource.Add(item.descripcionTarjeta);
                cmbTarjetas.Items.Add(item);
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (txtBIN.MaskCompleted)
            {
                bin = txtBIN.Text ;
                ingresoCorrecto = true;

                tarjeta = (TarjetaCajero)cmbTarjetas.SelectedItem;

                Close();
            }
            else
                MessageBox.Show("Debe completar el BIN (seis primeros digitos de la tarjeta) para ver las promociones", "Error de validacion", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            ingresoCorrecto = false;
            Close();
        }

        private void FrmIngresarBin_Load(object sender, EventArgs e)
        {
            cargarTarjetas();
        }

        private void txtBin_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }
    }
}
