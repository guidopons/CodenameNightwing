using System;
using System.Linq;
using System.Windows.Forms;
using CodenameNightwing.Varios;
using CodenameNightwing.WebServices.WSEspecificos;
using CodenameNightwing.WebServices;
using CodenameNightwing.Autorization;
using CodenameNightwing.FileManager;
using CodenameNightwing.BusinessLogic;
using CodenameNightwing.Config;
using CodenameNightwing.WebServices.WSEspecificos.Transacciones;
using System.Drawing;
using System.Text.RegularExpressions;
using CodenameNightwing.Exceptions;

namespace CodenameNightwing.Forms
{
    public partial class FrmDevolucionAlone
        : Form
    {
        private Transaccion _tran;
        public Transaccion tran
        {
            get { return _tran; }
            set { _tran = value; }
        }

        public void handleEventTransaction(object sender, EventArgs args)
        {
            Transaccion tran = (Transaccion)sender;
            string estadoDesc = tran.getEstadoDescription();
            if (estadoDesc != null)
            {
                lblMensaje.Text = estadoDesc;
                lblMensaje.Refresh();
                mostrarCartel();
            }
        }


        public FrmDevolucionAlone()
        {
            InitializeComponent();
            Icon = Properties.Resources.logo_con_borde_grueso;
        }

        private void FrmAnulacionAlone_Load(object sender, EventArgs e)
        {

        }

        private void btnAutorizar_Click(object sender, EventArgs e)
        {
            if (validarForm()) { 
                autorizar();
            }
            else
            {
                MessageBox.Show("Faltan completar campos o el formato no es el correcto. Revise los campos resaltados", "Error de validacion", MessageBoxButtons.OK);
                this.Activate();
            }
        }


        private bool validarForm()
        {
            bool bandera = true;

            //txtNumeroTkt
            if (txtNumeroTkt.Text.Length == 0 || !Regex.IsMatch(txtNumeroTkt.Text, @"^\d+$"))
            {
                bandera = false;
                txtNumeroTkt.BackColor = Color.LightCoral;
            }
            else
                txtNumeroTkt.BackColor = Color.White;

            //txtBoxIDArsa
            if (txtBoxIDArsa.Text.Length == 0 )
            {
                bandera = false;
                txtBoxIDArsa.BackColor = Color.LightCoral;
            }
            else
                txtBoxIDArsa.BackColor = Color.White;


            //txtCuotas
            if (txtCuotas.Text.Length == 0 || !Regex.IsMatch(txtCuotas.Text, @"^\d+$"))
            {
                bandera = false;
                txtCuotas.BackColor = Color.LightCoral;
            }
            else
                txtCuotas.BackColor = Color.White;

            //txtImporte
            if (txtImporte.Text.Length == 0 || !Regex.IsMatch(txtImporte.Text, @"^\d+\,\d{2}$"))
            {
                bandera = false;
                txtImporte.BackColor = Color.LightCoral;
            }
            else
                txtImporte.BackColor = Color.White;


            //txtSeisPrimeros
            if (txtSeisPrimeros.Text.Length == 0)
            {
                bandera = false;
                txtSeisPrimeros.BackColor = Color.LightCoral;
            }
            else
                txtSeisPrimeros.BackColor = Color.White;


            return bandera;
        }

        private void autorizar()
        {
            

            
            try
            {
                mostrarCartel();

                tran = TransaccionBuilder.construirDevolucion(txtNumeroTkt.Text, int.Parse(txtBoxIDArsa.Text.ToLongFromBase36()), Decimal.Parse(txtImporte.Text));
                tran.eventHandler += this.handleEventTransaction;
                tran.tarjeta.numero = txtSeisPrimeros.Text + "0000000000";

                WebServiceGetTransaction ws = new WebServiceGetTransaction( int.Parse(txtBoxIDArsa.Text.ToLongFromBase36()) );
                Transaccion aux = WebResponseParser.parseXMLGetTransaction(ws.getResponse());

                if (aux != null)
                {
                    tran.trxIdOriginal = aux.trxId;
                    tran.ticketOriginal = aux.numTicket;
                    tran.fechaOriginal = aux.fecha;
                    tran.cantCuotas = aux.cantCuotas;
                    tran.tarjeta.numero = aux.tarjeta.numero;
                    tran.trxId = aux.trxId;
                }
                else
                {
                    throw new VTOLException("No se pudo encontrar la transacción con ID: " + txtBoxIDArsa.Text);       
                }

                Autorizator auth = AutorizatorFactory.getAutorizator( tran );
                Transaccion resultado = null;
                if (auth != null)
                    resultado = auth.realizarTransaccion(tran);

                if (resultado != null)
                {
                    MessageBox.Show("Devolucion realizada con exito", "Devolucion Alone", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    PaymentFormWriter.grabarVoid(resultado);
                    Close();
                }
                else
                {
                    throw new VTOLException("No se pudo realizar la devolución del ID: " + txtBoxIDArsa.Text);
                }
            }
            catch (VTOLException e)
            {
                string msgVtol = e.getMsg();
                Program.logger.Error("Error al devolver alone. Msg VTOL: " + msgVtol, e);
                MessageBox.Show("Error al devolver alone. Detalle: " + msgVtol, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception e)
            {
                Program.logger.Error("Error al devolver alone" , e);
                MessageBox.Show("Error al devolver alone. Revise parametros", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            pnlMensaje.Visible = false;
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            PaymentFormWriter.grabarVoid(null);
            Close();
        }

        private void mostrarCartel()
        {
            pnlMensaje.Visible = true;
            pnlMensaje.Refresh();
        }

        private void lblFecha_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void txtImporte_Click(object sender, EventArgs e)
        {
            txtImporte.Enabled = true;

        }
    }
}
