using CodenameNightwing.BusinessLogic;
using CodenameNightwing.FileManager;
using CodenameNightwing.Varios;
using System;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace CodenameNightwing.Forms
{
    public partial class FrmDatosTarjetaDirecto : Form
    {

        private bool _isComplete;
        public bool isComplete
        {
            get { return _isComplete; }
            set { _isComplete = value; }
        }

        private Tarjeta _tar;
        public Tarjeta tar
        {
            get { return _tar; }
            set { _tar = value; }
        }

        private string _numResTarjeta;
        public string numResTarjeta
        {
            get { return _numResTarjeta; }
            set { _numResTarjeta = value; }
        }

        private string _vencimiento;
        public string vencimiento
        {
            get { return _vencimiento; }
            set { _vencimiento = value; }
        }

        private FrmDatosTarjetaDirecto()
        {
            InitializeComponent();
        }

        public FrmDatosTarjetaDirecto(Tarjeta tarjeta)
        {
            InitializeComponent();
            Icon = Properties.Resources.logo_con_borde_grueso;
            tar = tarjeta;
            if ( tar.primeros6().Length != 0)
            {
                cargarMascaras();
                txtPrimerosNumeros.Text = tar.primeros6();
            }
            else
            {
                txtNumeroTarjeta.Visible = true;
                txtNumeroTarjeta.Enabled = true;
            }
            
            isComplete = false;
        }

        private void cargarMascaras()
        {

            if (tar.tipoTarjeta.Equals(TipoTarjeta.CREDITO) || tar.tipoTarjeta.Equals(TipoTarjeta.CREDITO_DEBITO))
                POIutils.updateTarjetaFromBin(tar.primeros6(), tar);

            switch (tar.codSabre)
            {
                case "AX":
                    txtPrimerosNumeros.Mask = "0000-00";
                    txtNumerosRestantes.Mask = "0000-00000";
                    break;
                case "DC":
                    if (tar.primeros6().StartsWith("3"))
                    {
                        txtPrimerosNumeros.Mask = "0000-00";
                        txtNumerosRestantes.Mask = "0000-0000";
                    }
                    else
                    {
                        txtPrimerosNumeros.Mask = "0000-00";
                        txtNumerosRestantes.Mask = "00-0000-0000";
                    }
                    
                    break;
                case "MO":
                case "MA":
                    txtPrimerosNumeros.Mask = "000000";
                    //txtUltimosNumeros.Mask = "0000";
                    txtNumerosRestantes.Width = txtNumerosRestantes.Width * 2 + 6;
                    txtNumerosRestantes.Mask = "000000000000000";
                    break;
                default:
                    txtPrimerosNumeros.Mask = "0000-00";
                    txtNumerosRestantes.Mask = "00-0000-0000";
                    break;
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {


            if ( tar.primeros6().Length != 0) { 

                if (txtNumerosRestantes.MaskCompleted)
                {
                    numResTarjeta = txtNumerosRestantes.Text;
                }
                else
                {
                    if (tar.isMaestro())
                    {
                        Regex rgx = new Regex(@"" + tar.ultimos4() + "0{0,9}$");

                        if ( !rgx.IsMatch(txtNumerosRestantes.Text ))
                        {
                            MessageBox.Show("Deben coincidir los digitos del final completando con ceros para llegar a 16 si lo requiere", "Complete el numero de tarjeta" ,MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtNumerosRestantes.BackColor = Color.LightCoral;
                            return;
                        }
                        else
                        {
                            if (txtNumerosRestantes.Text.Length >= 10)
                            {
                                numResTarjeta = txtNumerosRestantes.Text;
                            }else
                            {
                                MessageBox.Show("Minimo con Maestro debe tener 16 digitos, complete con ceros al final", "Complete el numero de tarjeta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                txtNumerosRestantes.BackColor = Color.LightCoral;
                                return;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Debe completar los numeros restantes de la tarjeta", "Complete el numero de tarjeta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtNumerosRestantes.BackColor = Color.LightCoral;
                        return;
                    }
                
                }
            }
            else
            {
                //TODO ver que validacion le ponemos a la tarjeta
            }

            if (!txtVencimiento.MaskCompleted) { 
                MessageBox.Show("Debe completar la fecha de vencimiento de la tarjeta", "Complete Fecha de Vencimiento", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNumerosRestantes.BackColor = Color.LightCoral;
                return;
            }
            else
            {
                txtVencimiento.BackColor = Color.White;
                vencimiento = txtVencimiento.Text;
            }

            // VALIDACION DE LA FECHA
            if (!POIutils.isMonthYearSmallerNow(txtVencimiento.Text))
            {
                txtVencimiento.BackColor = Color.LightCoral;
                MessageBox.Show("La fecha de vencimiento debe ser mayor igual al mes y año actual o es invalido", "Error Fecha Vencimiento", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
                txtVencimiento.BackColor = Color.White;

            if ( txtNumeroTarjeta.Text.Length != 0)
            {
                tar.numero = txtNumeroTarjeta.Text;
            }
            else
            {
                if (tar.isMaestro())
                {
                    tar.numero = tar.primeros6() + numResTarjeta;
                }
                else
                {
                    tar.numero = tar.primeros6() + numResTarjeta;
                }
            }
            

            tar.vencimiento = txtVencimiento.Text;

            // VALIDACION CODIGO DE SEGURIDAD
            if (txtCodSeguridad.Text.Length != 0)
            {
                tar.cvc = txtCodSeguridad.Text;
            }
            else
            {
                MessageBox.Show("Debe ingresar el codigo de seguridad de la tarjeta", "Complete el codigo de seguridad", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCodSeguridad.BackColor = Color.LightCoral;
                return;
            }

            isComplete = true;

            Close();
        }

        private void txtNumerosRestantes_TextChanged(object sender, EventArgs e)
        {
            if (txtNumerosRestantes.MaskCompleted)
            {
                if (txtNumerosRestantes.BackColor != Color.White)
                    txtNumerosRestantes.BackColor = Color.White;
                txtVencimiento.Focus();
            }
        }

        private void txtNumeroTarjeta_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtVencimiento_TextChanged(object sender, EventArgs e)
        {
            if (txtVencimiento.MaskCompleted)
            {
                if (txtVencimiento.BackColor != Color.White)
                    txtVencimiento.BackColor = Color.White;
                txtCodSeguridad.Focus();
            }
        }

        private void txtNumerosRestantes_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void grpAcciones_Enter(object sender, EventArgs e)
        {

        }

        private void FrmNumerosRestantes_Load(object sender, EventArgs e)
        {

        }

        private void txtVencimiento_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void lblCodSeguridad_Click(object sender, EventArgs e)
        {

        }

        private void txtNumeroTarjeta_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            isComplete = false;
            Close();
        }
    }
}
