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
    public partial class FrmNumerosRestantes : Form
    {
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

        private FrmNumerosRestantes()
        {
            InitializeComponent();
        }

        public FrmNumerosRestantes(Tarjeta tarjeta)
        {
            InitializeComponent();
            Icon = Properties.Resources.logo_con_borde_grueso;
            tar = tarjeta;
            cargarMascaras();
            txtPrimerosNumeros.Text = tar.primeros6();
            txtUltimosNumeros.Text = tar.ultimos4();
        }

        private void cargarMascaras()
        {

            if ( tar.codSabre == null || tar.codSabre.Trim().Length != 2) {
               POIutils.updateTarjetaFromBin(tar.primeros6(), tar);
            }

            switch (tar.codSabre)
            {
                case "AX":
                    txtPrimerosNumeros.Mask = "0000-00";
                    txtUltimosNumeros.Mask = "0000";
                    txtNumerosRestantes.Mask = "0000-0";
                    break;
                case "DC":
                    if (tar.primeros6().StartsWith("3"))
                    {
                        txtPrimerosNumeros.Mask = "0000-00";
                        txtUltimosNumeros.Mask = "0000";
                        txtNumerosRestantes.Mask = "0000-";
                    }
                    else
                    {
                        txtPrimerosNumeros.Mask = "0000-00";
                        txtUltimosNumeros.Mask = "-0000";
                        txtNumerosRestantes.Mask = "00-0000";
                    }
                    
                    break;
                case "MO":
                case "MA":
                    txtPrimerosNumeros.Mask = "000000";
                    //txtUltimosNumeros.Mask = "0000";
                    txtUltimosNumeros.Visible = false;
                    txtNumerosRestantes.Width = txtNumerosRestantes.Width * 2 + 6;
                    txtNumerosRestantes.Mask = "000000000000000";
                    break;
                default:
                    txtPrimerosNumeros.Mask = "0000-00";
                    txtUltimosNumeros.Mask = "-0000";
                    txtNumerosRestantes.Mask = "00-0000";
                    break;
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {

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

            if (tar.isMaestro())
            {
                tar.numero = tar.primeros6() + numResTarjeta;
            }
            else
            {
                Regex rgxNumero = new Regex("\\D+");
                tar.numero = rgxNumero.Replace(tar.numero, numResTarjeta);
            }

            tar.vencimiento = txtVencimiento.Text;

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


        private void txtVencimiento_TextChanged(object sender, EventArgs e)
        {
            if (txtVencimiento.MaskCompleted)
            {
                if (txtVencimiento.BackColor != Color.White)
                    txtVencimiento.BackColor = Color.White;
                btnAceptar.Focus();
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
    }
}
