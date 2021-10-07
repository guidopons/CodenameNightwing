using CodenameNightwing.BusinessLogic;
using CodenameNightwing.Config;
using CodenameNightwing.Printer;
using CodenameNightwing.Printer.Cupones;
using CodenameNightwing.Varios;
using CodenameNightwing.WebServices;
using CodenameNightwing.WebServices.WS_especificos.Transacciones;
using CodenameNightwing.WebServices.WSEspecificos.Transacciones;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CodenameNightwing.Forms
{
    public partial class FrmReimpresionCupones : Form
    {
        private List<Transaccion> lTran = new List<Transaccion>();
        private int cuponSeleccionado = -1;
        public FrmReimpresionCupones()
        {
            InitializeComponent();
            txtCaja.Text = Configuration.getInstance().caja;
            txtSucursal.Text = Configuration.getInstance().sucursal;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {

            if ( txtVtolId.Text != null && txtVtolId.Text.Length != 0)
            {

                dgwTransacciones.Rows.Clear();
                lTran.Clear();
                
                WebServiceGetTransaction ws = new WebServiceGetTransaction(int.Parse(txtVtolId.Text.ToLongFromBase36()));
                Transaccion item = WebResponseParser.parseXMLGetTransaction(ws.getResponse());

                if (item != null && item.numAutorizacion != null && !item.numAutorizacion.Equals("") && !item.numAutorizacion.Equals("0"))
                {
                    string modoStr = (item.modo == TipoModoTransaccion.ONLINE ? "ON" : (item.modo == TipoModoTransaccion.OFFLINE ? "OF" : "OH"));
                    dgwTransacciones.Rows.Add(new object[] { item.tipoTrans, item.tarjeta.numero, item.tarjeta.descripcion, item.importeTotal, item.fecha, item.pdv.sucursal, item.pdv.caja, item.numTicket, item.numAutorizacion, modoStr, item.trxReferenceId.ToString().ToBase36String() , item.isAnulado, item.isReversado });
                    lTran.Add(item);
                }else
                {
                    MessageBox.Show("No se encontraron registros de operaciones confirmadas con el ID: " + txtVtolId.Text, "No hay registros", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }


            }
            else
            {
                bool validoBusqueda = true;
                string ultimos4Numeros = "";
                string marca = "";
                DateTime fechaABuscar = DateTime.Now;
                if (calFecha.SelectionStart != null)
                    fechaABuscar = calFecha.SelectionStart;
                else
                    validoBusqueda = false;
                if (txtUltimos4Numeros.MaskCompleted)
                    ultimos4Numeros = txtUltimos4Numeros.Text;
                if (txtMarca.Text.Length > 0)
                    marca = txtMarca.Text;

                if (txtCaja.Text == "" || txtSucursal.Text == "")
                    validoBusqueda = false;

                if (validoBusqueda)
                {
                    dgwTransacciones.Rows.Clear();
                    lTran.Clear();
                    WebServiceSearchTransaction ws = new WebServiceSearchTransaction(fechaABuscar, ultimos4Numeros, marca, txtSucursal.Text, txtCaja.Text);
                    List<Transaccion> lTranAux = WebResponseParser.parseXMLSearchTransaction(ws.getResponse());
                    if (lTranAux != null)
                        foreach (var item in lTranAux)
                        {
                            if (item.numAutorizacion != null && !item.numAutorizacion.Equals("") && !item.numAutorizacion.Equals("0"))
                            {
                                string modoStr = (item.modo == TipoModoTransaccion.ONLINE ? "ON" : (item.modo == TipoModoTransaccion.OFFLINE ? "OF" : "OH"));
                                dgwTransacciones.Rows.Add(new object[] { item.tipoTrans, item.tarjeta.numero, item.tarjeta.descripcion, item.importeTotal, item.fecha, item.pdv.sucursal, item.pdv.caja, item.numTicket, item.numAutorizacion, modoStr, item.trxReferenceId.ToString().ToBase36String(), item.isAnulado, item.isReversado });
                                lTran.Add(item);
                            }
                        }
                }
                else
                    MessageBox.Show("Error en los datos ingresados. Revise parametros", "Seleccione cupon a imprimir", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

            

        private void btnVolver_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void mostrarCartel()
        {
            pnlImprimiendo.Visible = true;
            pnlImprimiendo.Refresh();
        }
        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (cuponSeleccionado != -1)
            {
                if (!string.IsNullOrEmpty(Configuration.getInstance().nombreImpresora))
                {
                    mostrarCartel();
                    Transaccion aImprimir = lTran[cuponSeleccionado];
                    if (aImprimir.isReversado)
                    {
                        MessageBox.Show("No se puede imprimir un cupon reversado.\n (No existe operacion)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        aImprimir.respuestaHost = "00 - APROBADA";
                        PrinterCupon imprimir = PrinterCuponSelector.getCupon(aImprimir, EnumCopiaUOriginal.ORIGINAL, true);
                        PrinterHelper ph = new PrinterHelper(imprimir.devolverCupon());
                        if (ph.imprimir())
                        {
                            imprimir = PrinterCuponSelector.getCupon(aImprimir, EnumCopiaUOriginal.COPIA, true);
                            ph = new PrinterHelper(imprimir.devolverCupon());
                            if (ph.imprimir())
                            {
                                MessageBox.Show("Se imprimieron los duplicados de manera correcta, Original y Copia para Cliente", "Impresión correcta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            };
                        }
                    }
                    pnlImprimiendo.Visible = false;
                }
            }
            else
                MessageBox.Show("Debe seleccionar el cupon para imprimir en la grilla", "Seleccione cupon a imprimir", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void dgwTransacciones_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            cuponSeleccionado = e.RowIndex;
        }

        private void txtUltimos4Numeros_Validated(object sender, EventArgs e)
        {
            if (!txtUltimos4Numeros.MaskCompleted)
                txtUltimos4Numeros.Text = "";
        }

        private void grpFiltros_Enter(object sender, EventArgs e)
        {

        }

        private void lblUltimos4Numeros_Click(object sender, EventArgs e)
        {

        }

        private void FrmReimpresionCupones_Load(object sender, EventArgs e)
        {

        }

        private void dgwTransacciones_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
