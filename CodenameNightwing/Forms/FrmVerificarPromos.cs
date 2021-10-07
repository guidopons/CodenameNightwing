using CodenameNightwing.BusinessLogic;
using CodenameNightwing.FileManager;
using CodenameNightwing.Varios;
using CodenameNightwing.WebServices;
using CodenameNightwing.WebServices.WSEspecificos;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace CodenameNightwing.Forms
{
    public partial class FrmVerificarPromos : Form
    {
        private bool _esNoi;
        public bool esNoi
        {
            get { return _esNoi; }
            set { _esNoi = value; }
        }


        private bool _conImporte = true;
        public bool conImporte
        {
            get { return _conImporte; }
            set { _conImporte = value; }
        }

        private List<Pnr> _lPnr;
        public List<Pnr> lPnr
        {
            get { return _lPnr; }
            set { _lPnr = value; }
        }

        private Tarjeta _tar;
        public Tarjeta tar
        {
            get { return _tar; }
            set { _tar = value; }
        }

        private List<Promocion> _promos;
        public List<Promocion> promos
        {
            get { return _promos; }
            set { _promos = value; }
        }

        private decimal _importeAutorizar;
        public decimal importeAutorizar
        {
            get { return _importeAutorizar; }
            set { _importeAutorizar = value; }
        }

        private FrmVerificarPromos() { }

        public FrmVerificarPromos(bool cargarVBrequest)
        {
            InitializeComponent();
            Icon = Properties.Resources.logo_con_borde_grueso;
            esNoi = false;
            if (cargarVBrequest)
                lPnr = VBrequestReader.leerPnrs();
        }

        public FrmVerificarPromos(Tarjeta auxT)
        {
            InitializeComponent();
            Icon = Properties.Resources.logo_con_borde_grueso;
            esNoi = true;
            tar = auxT;
        }

        private void FrmVerificarPromos_Load(object sender, EventArgs e)
        {
            if (lPnr != null)
            {
                foreach (var item in lPnr)
                {
                    importeAutorizar += item.getTotalAmount();
                }
            }
            txtImporte.Text = importeAutorizar.ToString("#####0.00").Replace(",", ".");
            if (esNoi)
            {
                pnlBin.Visible = false;
                cargarPromos(tar.primeros6());
                cargarMascaras();
                txtDescripcionTarjeta.Text = promos[0].descripcionTarjeta;
                txtBanco.Text = promos[0].banco;
                txtPrimerosNumeros.Text = tar.primeros6();
                txtUltimosNumeros.Text = tar.ultimos4();
            }
            else
                pnlNumeroTarjeta.Visible = false;

            conImporte = true;
            chx_importe.Checked = true;
        }

        private void cargarPromos(string bin)
        {
            mostrarCartel();
            WebServiceBines wsPromo = null;
            if (!conImporte) {
                wsPromo = new WebServiceBines(bin, 10000);
            }else
            {
                wsPromo = new WebServiceBines(bin, importeAutorizar);
            }
            //  wsPromo.sendRequest();
            promos = WebResponseParser.parseXMLBines(wsPromo.getResponse());
            grdCuotas.Rows.Clear();
            if (!conImporte)
            {
                importeAutorizar = 0;
            }
            if (promos.Count > 0)
            {
                txtDescripcionTarjeta.Text = promos[0].descripcionTarjeta;
                txtBanco.Text = string.IsNullOrEmpty(promos[0].banco) ? "SIN PROMOCION" : promos[0].banco;
                foreach (var item in promos)
                {
                    decimal auxImpCuota = item.porcentaje == 0.0M ? importeAutorizar / PlanesAHORA.getCuotasDividir(item.cuotas) : (importeAutorizar + (importeAutorizar * item.porcentaje / 100)) / PlanesAHORA.getCuotasDividir(item.cuotas);
                    decimal auxTotal = item.porcentaje == 0.0M ? importeAutorizar : importeAutorizar + (importeAutorizar * item.porcentaje) / 100;
                    grdCuotas.Rows.Add(new object[] { PlanesAHORA.getCuotasMostrar(item.cuotas), item.porcentaje, Convert.ToDecimal(auxImpCuota.ToString("######0.00")), Convert.ToDecimal(auxTotal.ToString("######0.00")) });
                }
                grdCuotas.Rows[grdCuotas.CurrentCell.RowIndex].Selected = true;
                txtCuotas.Text = grdCuotas.Rows[grdCuotas.CurrentCell.RowIndex].Cells[0].Value.ToString();
                txtImporteAAutorizar.Text = grdCuotas.Rows[grdCuotas.CurrentCell.RowIndex].Cells[3].Value.ToString().Replace(",", ".");
                txtIntereses.Text = (Convert.ToDecimal(grdCuotas.Rows[grdCuotas.CurrentCell.RowIndex].Cells[3].Value.ToString()) - importeAutorizar).ToString("#####0.00").Replace(",", ".");
            }
            else
            {
                txtCuotas.Text = "";
                txtImporteAAutorizar.Text = "";
                txtIntereses.Text = "";
                txtDescripcionTarjeta.Text = "";
                txtBanco.Text = "";
                MessageBox.Show("Bin incorrecto", "Error de bin", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Activate();
            }
            pnlMensaje.Visible = false;
        }

        private void cargarMascaras()
        {
            switch (promos[0].codTarjeta)
            {
                case "AX":
                    txtPrimerosNumeros.Mask = "####-##";
                    txtUltimosNumeros.Mask = "####";
                    txtNumerosRestantes.Mask = "####-#";
                    break;
                case "DC":
                    txtPrimerosNumeros.Mask = "####-##";
                    txtUltimosNumeros.Mask = "####";
                    txtNumerosRestantes.Mask = "####-";
                    break;
                default:
                    txtPrimerosNumeros.Mask = "####-##";
                    txtUltimosNumeros.Mask = "-####";
                    txtNumerosRestantes.Mask = "##-####";
                    break;
            }
        }

        private void grdCuotas_Click(object sender, EventArgs e)
        {
            grdCuotas.Rows[grdCuotas.CurrentCell.RowIndex].Selected = true;
            txtCuotas.Text = grdCuotas.Rows[grdCuotas.CurrentCell.RowIndex].Cells[0].Value.ToString();
            txtImporteAAutorizar.Text = grdCuotas.Rows[grdCuotas.CurrentCell.RowIndex].Cells[3].Value.ToString().Replace(",", ".");
            txtIntereses.Text = (Convert.ToDecimal(grdCuotas.Rows[grdCuotas.CurrentCell.RowIndex].Cells[3].Value.ToString()) - importeAutorizar).ToString("#####0.00").Replace(",", ".");
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPlanes_Click(object sender, EventArgs e)
        {
            cargarPlanes();
        }

        void cargarPlanes()
        {
            if (esNoi)
            {
                cargarPromos(tar.primeros6());
            }
            else
            {
                if (txtBin.Text.Length == 6)
                {
                    cargarPromos(txtBin.Text);
                }
                else
                {
                    MessageBox.Show("El bin debe poseer 6 numeros", "Error de bin", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.Activate();
                }
            }
        }

        private void txtImporte_KeyPress(object sender, KeyPressEventArgs e)
        {
            char a = Convert.ToChar('.');
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == '\b' || (e.KeyChar == a && txtImporte.Text.ToCharArray().Count(x => x == a) < 1)))
                e.KeyChar = '\0';
        }

        private void txtImporte_TextChanged(object sender, EventArgs e)
        {
            if (txtImporte.Text.Length > 0)
                importeAutorizar = Convert.ToDecimal(txtImporte.Text, CultureInfo.InvariantCulture);
        }

        private void txtBin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == '\b') || (txtBin.Text.Length == 6 && !((e.KeyChar == '\b') || (char.IsDigit(e.KeyChar) && (txtBin.SelectedText != "")))))
                e.KeyChar = '\0';
        }

        private void mostrarCartel()
        {
            pnlMensaje.Visible = true;
            pnlMensaje.Refresh();
        }

        private void dataGridView1_KeyUp(object sender, KeyEventArgs e)
        {
            if (grdCuotas.CurrentCell != null)
            {
                int rowIndexActual = grdCuotas.CurrentCell.RowIndex;
                bool modificar = false;
                if (e.KeyCode == Keys.Tab)
                {
                    modificar = true;
                    if (rowIndexActual + 1 < grdCuotas.Rows.Count)
                        grdCuotas.CurrentCell = grdCuotas.Rows[rowIndexActual + 1].Cells[0];
                    else
                        grdCuotas.CurrentCell = grdCuotas.Rows[0].Cells[0];
                }
                else
                    if (e.KeyCode == Keys.Up)
                    {
                        modificar = true;
                        if (grdCuotas.CurrentCell.RowIndex >= 0)
                            grdCuotas.CurrentCell = grdCuotas.Rows[rowIndexActual].Cells[0];
                    }
                    else
                        if (e.KeyCode == Keys.Down)
                        {
                            modificar = true;
                            if (rowIndexActual <= (grdCuotas.Rows.Count - 1))
                                grdCuotas.CurrentCell = grdCuotas.Rows[rowIndexActual].Cells[0];
                        }
                        else
                            if (e.KeyCode == Keys.Left)
                                modificar = true;
                            else if (e.KeyCode == Keys.Right)
                                modificar = true;
                if (modificar)
                {
                    txtCuotas.Text = grdCuotas.Rows[grdCuotas.CurrentCell.RowIndex].Cells[0].Value.ToString();
                    txtImporteAAutorizar.Text = grdCuotas.Rows[grdCuotas.CurrentCell.RowIndex].Cells[3].Value.ToString().Replace(",", ".");
                    txtIntereses.Text = (Convert.ToDecimal(grdCuotas.Rows[grdCuotas.CurrentCell.RowIndex].Cells[3].Value.ToString()) - importeAutorizar).ToString("#####0.00").Replace(",", ".");
                    grdCuotas.Rows[grdCuotas.CurrentCell.RowIndex].Selected = true;
                }
            }
        }

        private void chx_importe_CheckedChanged(object sender, EventArgs e)
        {
            if (chx_importe.Checked)
            {
                conImporte = true;
            }else
            {
                conImporte = false;
            }
        }

        private void grdCuotas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
