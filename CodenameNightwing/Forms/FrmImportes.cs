using CodenameNightwing.BusinessLogic;
using CodenameNightwing.Config;
using CodenameNightwing.FileManager;
using CodenameNightwing.Varios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace CodenameNightwing.Forms
{
    public partial class FrmImportes : Form
    {
        private TarjetaCajero _tarjSeleccionada;
        public TarjetaCajero tarjSeleccionada
        {
            get { return _tarjSeleccionada; }
            set { _tarjSeleccionada = value; }
        }

        private bool _continuar;
        public bool continuar
        {
            get { return _continuar; }
            set { _continuar = value; }
        }

        private decimal _restaAutorizar;
        public decimal restaAutorizar
        {
            get { return _restaAutorizar; }
            set { _restaAutorizar = value; }
        }


        private List<Pnr> _pnrs;
        public List<Pnr> pnrs
        {
            get { return _pnrs; }
            set { _pnrs = value; }
        }

        private TipoTarjeta _tipoTarjeta;
        public TipoTarjeta tipoTarjeta
        {
            get { return _tipoTarjeta; }
            set { _tipoTarjeta = value; }
        }

        private decimal _aAutorizar;
        public decimal aAutorizar
        {
            get { return _aAutorizar; }
            set { _aAutorizar = value; }
        }

        private bool _noSeleccionaTarjeta;
        public bool noSeleccionaTarjeta
        {
            get { return _noSeleccionaTarjeta; }
            set { _noSeleccionaTarjeta = value; }
        }

        private FrmImportes()
        {
            InitializeComponent();
        }

        public FrmImportes(TipoTarjeta tipo, List<Pnr> pasajeros, decimal faltaAutorizar)
        {
            InitializeComponent();
            Icon = Properties.Resources.logo_con_borde_grueso;
            tipoTarjeta = tipo;
            pnrs = pasajeros;
            restaAutorizar = faltaAutorizar;
            aAutorizar = faltaAutorizar;
        }

        private void FrmImportes_Load(object sender, EventArgs e)
        {
            switch (tipoTarjeta)
            {
                case TipoTarjeta.CREDITO:
                    grpTarjeta.Hide();
                    grpImporte.Top = grpACobrar.Bottom + 5;
                    grpAcciones.Top = grpImporte.Bottom + 5;
                    break;
                case TipoTarjeta.DEBITO:
                    cargarTarjetas();
                    break;
                case TipoTarjeta.PASAJERO_FRECUENTE:
                    break;
                case TipoTarjeta.CUENTA_CORRIENTE:
                    break;
                case TipoTarjeta.SUBLO:
                    break;
                case TipoTarjeta.EFECTIVO:
                    grpTarjeta.Hide();
                    grpImporte.Top = grpACobrar.Bottom + 5;
                    grpAcciones.Top = grpImporte.Bottom + 5;
                    break;
            }
            txtAutorizar.Text = aAutorizar.ToString("######0.00").Replace(",", ".");
            cargarPasajeros();
            this.Width = grpImporte.Right + 30;
            this.Height = grpAcciones.Bottom + 50;
            cargarShortcutsTarjetasDebito();

            Transaccion tranAuxPrice = TransaccionBuilder.construirPago(1);
            lblPriceInformation.Text = tranAuxPrice.getPriceInformation();

        }

        private void cargarShortcutsTarjetasDebito()
        {
            List<char> letrasDisponibles = buscarLetrasDisponibles();
            foreach (Control item in grpTarjeta.Controls)
            {
                bool seAsignoLetra = false;
                int i = 0;
                if (item.GetType() == typeof(RadioButton))
                    while (!seAsignoLetra)
                    {
                        if (letrasDisponibles.Contains(char.ToLower(((RadioButton)item).Text[i])))
                        {
                            letrasDisponibles.Remove(char.ToLower(((RadioButton)item).Text[i]));
                            letrasDisponibles.Remove(char.ToUpper(((RadioButton)item).Text[i]));
                            ((RadioButton)item).Text = ((RadioButton)item).Text.Insert(i, "&");
                            seAsignoLetra = true;

                        }
                        i++;
                    }
            }
        }

        private List<char> buscarLetrasDisponibles()
        {
            List<char> letrasYaUsadas = new List<char>();
            List<char> letrasYNumeros = "abcdefghijklmnopqrstuvwxyz0123456789".ToCharArray().ToList<char>();
            buscarRecursivo(this.Controls);
            foreach (var item in auxL)
            {
                System.Reflection.PropertyInfo pi = item.GetType().GetProperty("Text");
                if (pi != null)
                    if (((string)pi.GetValue(item, null)).Contains("&"))
                        letrasYaUsadas.Add(((string)pi.GetValue(item, null)).Substring(((string)pi.GetValue(item, null)).IndexOf('&'))[1]);

            }
            foreach (char item in letrasYaUsadas)
            {
                letrasYNumeros.Remove(char.ToUpper(item));
                letrasYNumeros.Remove(char.ToLower(item));
            }
            return letrasYNumeros;
        }

        List<Control> auxL = new List<Control>();
        private void buscarRecursivo(System.Windows.Forms.Control.ControlCollection controles)
        {
            foreach (Control item in controles)
                if (item.HasChildren)
                    buscarRecursivo(item.Controls);
                else
                    auxL.Add(item);
        }

        private void cargarPasajeros()
        {
            foreach (var item in pnrs)
            {
                TreeNode nodoPnr = treePasajeros.Nodes.Add(item.codSabre, "PNR : " + item.codSabre);
                foreach (var auxPax in item.pasajeros)
                {
                    TreeNode nodoPasajero;
                    if (item.onlyEmd)
                        nodoPasajero = nodoPnr.Nodes.Add(auxPax.nombre, auxPax.nombre);
                    else
                        nodoPasajero = nodoPnr.Nodes.Add(auxPax.nombre, auxPax.nombre + "        FARE : " + auxPax.fare);
                    foreach (var auxEmd in auxPax.emds)
                        nodoPasajero.Nodes.Add(auxEmd.descripcion, auxEmd.descripcion + "     : " + auxEmd.fare);
                }
            }
        }

        private void cargarTarjetas()
        {
            if ( POIutils.isVtolNpsPosAutorizator() )
            {
                 List<TarjetaCajero> auxTarjetas = EntityLoader.loadTarjetas().Where(x => x.tipo == TipoTarjeta.DEBITO).ToList();
                foreach (TarjetaCajero item in auxTarjetas)
                {
                    RadioButton rAux = new RadioButton();
                    rAux.Tag = item;
                    rAux.Text = item.descripcionTarjeta;
                    rAux.Dock = DockStyle.Top;
                    rAux.CheckedChanged += rAux_CheckedChanged;
                    grpTarjeta.Controls.Add(rAux);
                    if (POIutils.isVtolNpsCallCenterAutorizator() && rAux.Text.Equals("Maestro"))
                        rAux.Enabled = false;
                }
            }
            else
            {
                noSeleccionaTarjeta = true;
                grpImporte.Top = grpTarjeta.Top;
                grpAcciones.Top = grpImporte.Bottom;
                grpTarjeta.Visible = false;
                this.Height = this.Height - grpTarjeta.Height;
            }
        }

        private void rAux_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton aux = sender as RadioButton;
            tarjSeleccionada = (TarjetaCajero)aux.Tag;
        }

        private void chkElegirPax_CheckedChanged(object sender, EventArgs e)
        {
            if (chkElegirPax.Checked == true)
            {
                aAutorizar = 0.0M;
                txtAutorizar.Text = aAutorizar.ToString("######0.00").Replace(",", ".");
                grpPasajeros.Show();
                this.Width = grpPasajeros.Right + 30;
                this.Height = grpPasajeros.Bottom + 50;
            }
            else
            {
                aAutorizar = restaAutorizar;
                txtAutorizar.Text = aAutorizar.ToString("######0.00").Replace(",", ".");
                grpPasajeros.Hide();
                checkRecursivo(treePasajeros.Nodes, false);
                this.Width = grpImporte.Right + 30;
                this.Height = grpAcciones.Bottom + 50;
            }
        }

        private void checkRecursivo(TreeNodeCollection arbol, bool checkOUncheck)
        {
            foreach (TreeNode item in arbol)
            {
                if (item.Nodes.Count > 0)
                    checkRecursivo(item.Nodes, checkOUncheck);
                if (checkOUncheck)
                {
                    item.Checked = true;
                    item.Expand();
                }
                else
                {
                    item.Checked = false;
                    item.Collapse();
                }
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            continuar = false;
            this.Close();
        }

        private void treePasajeros_BeforeCheck(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Action != TreeViewAction.Unknown)
            {
                switch (e.Node.Level)
                {
                    case 0:
                        treeCheckUncheckLvl0(e);
                        break;
                    case 1:
                        treeCheckUncheckLvl1(e);
                        break;
                    case 2:
                        treeCheckUncheckLvl2(e);
                        break;
                    default:
                        break;
                }
                txtAutorizar.Text = aAutorizar.ToString("######0.00").Replace(",", ".");
            }
        }

        private void treeCheckUncheckLvl0(TreeViewCancelEventArgs e)
        {
            decimal aRestar = 0.0M;
            foreach (TreeNode item in e.Node.Nodes)
            {
                if (item.Checked)
                    if (!pnrs.First(x => x.codSabre == e.Node.Name).onlyEmd)
                        aRestar += pnrs.First(x => x.codSabre == e.Node.Name).pasajeros.First(x => x.nombre == item.Name).fare;
                foreach (TreeNode itemEmd in item.Nodes)
                {
                    if (itemEmd.Checked)
                        aRestar += pnrs.First(x => x.codSabre == e.Node.Name).pasajeros.First(x => x.nombre == item.Name).emds.First(x => x.descripcion == itemEmd.Name).fare;
                }
            }
            if (!e.Node.Checked)
            {
                if (!pnrs.First(x => x.codSabre == e.Node.Name).onlyEmd)
                    aAutorizar += pnrs.First(x => x.codSabre == e.Node.Name).getTotalAmount() - aRestar;
                else
                    aAutorizar += pnrs.First(x => x.codSabre == e.Node.Name).getTotalEmds() - aRestar;
                checkRecursivo(e.Node.Nodes, true);
            }
            else
            {
                aAutorizar -= aRestar;
                checkRecursivo(e.Node.Nodes, false);
            }
        }

        private void treeCheckUncheckLvl1(TreeViewCancelEventArgs e)
        {
            decimal aRestarPax = 0.0M;
            if (e.Node.Checked)
                if (!pnrs.First(x => x.codSabre == e.Node.Parent.Name).onlyEmd)
                    aRestarPax += pnrs.First(x => x.codSabre == e.Node.Parent.Name).pasajeros.First(x => x.nombre == e.Node.Name).fare;
            foreach (TreeNode itemEmd in e.Node.Nodes)
            {
                if (itemEmd.Checked)
                    aRestarPax += pnrs.First(x => x.codSabre == e.Node.Parent.Name).pasajeros.First(x => x.nombre == e.Node.Name).emds.First(x => x.descripcion == itemEmd.Name).fare;
            }
            if (!e.Node.Checked)
            {
                if (!pnrs.First(x => x.codSabre == e.Node.Parent.Name).onlyEmd)
                    aAutorizar += pnrs.First(x => x.codSabre == e.Node.Parent.Name).pasajeros.First(x => x.nombre == e.Node.Name).getTotalPax() - aRestarPax;
                else
                    aAutorizar += pnrs.First(x => x.codSabre == e.Node.Parent.Name).pasajeros.First(x => x.nombre == e.Node.Name).getTotalEmd() - aRestarPax;
                checkRecursivo(e.Node.Nodes, true);
            }
            else
            {
                aAutorizar -= aRestarPax;
                checkRecursivo(e.Node.Nodes, false);
            }
        }

        private void treeCheckUncheckLvl2(TreeViewCancelEventArgs e)
        {
            if (!e.Node.Checked)
            {
                aAutorizar += pnrs.First(x => x.codSabre == e.Node.Parent.Parent.Name).pasajeros.First(x => x.nombre == e.Node.Parent.Name).emds.First(x => x.descripcion == e.Node.Name).fare;
            }
            else
            {
                aAutorizar -= pnrs.First(x => x.codSabre == e.Node.Parent.Parent.Name).pasajeros.First(x => x.nombre == e.Node.Parent.Name).emds.First(x => x.descripcion == e.Node.Name).fare;
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            aceptar();
        }

        private void aceptar()
        {
            if (tipoTarjeta == TipoTarjeta.DEBITO)
            {
                if (tarjSeleccionada != null || noSeleccionaTarjeta)
                {
                    continuar = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Debe seleccionar una tarjeta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Activate();
                }
            }
            else
            {
                continuar = true;
                this.Close();
            }
        }

        private void txtAutorizar_Validated(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(txtAutorizar.Text, CultureInfo.InvariantCulture) <= restaAutorizar)
            {
                aAutorizar = Convert.ToDecimal(txtAutorizar.Text, CultureInfo.InvariantCulture);
            }
            else
            {
                MessageBox.Show("No puede autorizar un importe superior al que resta autorizar", "Error de importe", MessageBoxButtons.OK);
                this.Activate();
                aAutorizar = restaAutorizar;
                txtAutorizar.Text = aAutorizar.ToString("#####0.00").Replace(",", ".");
            }
        }

        private void txtAutorizar_KeyPress(object sender, KeyPressEventArgs e)
        {
            char a = Convert.ToChar('.');
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == '\b' || (e.KeyChar == a && txtAutorizar.Text.ToCharArray().Count(x => x == a) < 1)))
                e.KeyChar = '\0';
        }

        private void treePasajeros_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void grpTarjeta_Enter(object sender, EventArgs e)
        {

        }
    }
}
