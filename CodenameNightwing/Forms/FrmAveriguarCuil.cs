using CodenameNightwing.FileManager;
using CodenameNightwing.WebServices;
using CodenameNightwing.WebServices.WSEspecificos;
using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CodenameNightwing.Forms
{
    public partial class FrmAveriguarCuil : Form
    {

        public static readonly ILog logger = LogManager.GetLogger(typeof(FrmAveriguarCuil));

        public FrmAveriguarCuil()
        {
            InitializeComponent();
        }

        private bool _esExtranjero = false;
        public bool esExtranjero
        {
            get { return _esExtranjero; }
            set { _esExtranjero = value; }
        }

        private bool validarForm()
        {
            bool bandera = true;
            if (!txtCuit.MaskCompleted)
            {
                bandera = false;
                txtCuit.BackColor = Color.LightCoral;
            }
            else
                txtCuit.BackColor = Color.White;
            
            if (esExtranjero)
            {
                if (cmbPais.SelectedItem == null)
                {
                    bandera = false;
                    cmbPais.Text = "";
                    cmbPais.BackColor = Color.LightCoral;
                }
                else
                    txtDocumento.BackColor = Color.White;
            }
            if (cmbTipoPersona.Enabled)
            {
                if (cmbTipoPersona.SelectedIndex >= 0)
                {
                    if (((KeyValuePair<string, string>)cmbTipoPersona.SelectedItem).Key != "E")
                    {
                        if (!txtDocumento.MaskCompleted)
                        {
                            bandera = false;
                            txtDocumento.BackColor = Color.LightCoral;
                        }
                        else
                            txtDocumento.BackColor = Color.White;
                    }
                }
                else
                {
                    cmbTipoPersona.Text = "";
                    bandera = false;
                    txtDocumento.BackColor = Color.LightCoral;
                }
            }
            else
            {
                if (txtDocumento.Text.Length == 0)
                {
                    bandera = false;
                    txtDocumento.BackColor = Color.LightCoral;
                }
                else
                    txtDocumento.BackColor = Color.White;
            }
            return bandera;
        }
        private void btnCompletarCuil_Click(object sender, EventArgs e)
        {
            if (txtDocumento.MaskFull)
            {
                if (cmbTipoPersona.SelectedIndex >= 0)
                {
                    if (((KeyValuePair<string, string>)cmbTipoPersona.SelectedItem).Key == "M" || ((KeyValuePair<string, string>)cmbTipoPersona.SelectedItem).Key == "F")
                    {
                        WebServiceObtenerCuit ws = new WebServiceObtenerCuit(cmbTipoPersona.SelectedValue.ToString(), txtDocumento.Text.Replace(",", ""));
                        txtCuit.Text = WebResponseParser.parseXMLObtenerCuit(ws.getResponse());
                    }
                    else
                    {
                        MessageBox.Show("Solo se puede determinar el CUIL de personas fisicas", "Error al determinar CUIL", MessageBoxButtons.OK);
                        this.Activate();
                    }
                }
                else
                {
                    cmbTipoPersona.Text = "";
                    MessageBox.Show("Tipo de persona no valido", "Tipo de persona invalido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Activate();
                }
            }
            else
            {
                MessageBox.Show("Para poder generar el CUIL debe completar el campo numero de documento y seleccionar el tipo de persona", "Error al determinar CUIL", MessageBoxButtons.OK);
                this.Activate();
            }
        }

        private void btnVerificarCuit_Click(object sender, EventArgs e)
        {
            if (txtCuit.MaskFull)
            {
                WebServiceVerificarCuit ws = new WebServiceVerificarCuit(txtCuit.Text.Substring(0, 2), txtCuit.Text.Substring(2, 8), txtCuit.Text.Substring(10, 1));
                if (WebResponseParser.parseXMLVerificarCuit(ws.getResponse()))
                    MessageBox.Show("El CUIT/CUIL ha sido validado correctamente", "CUIT/CUIL correcto", MessageBoxButtons.OK);
                else
                    MessageBox.Show("El CUIT/CUIL es erroneo", "CUIT/CUIL incorrecto", MessageBoxButtons.OK);
                this.Activate();
            }
            else
            {
                MessageBox.Show("El campo numero de numero de cuil debe encontrarse completo", "Error al validar CUIL/CUIT", MessageBoxButtons.OK);
                this.Activate();
            }
        }

        private void cmbTipoPersona_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((KeyValuePair<string, string>)cmbTipoPersona.SelectedItem).Key == "E")
            {
                txtDocumento.Enabled = false;
                txtDocumento.Text = "";
                btnCompletarCuil.Enabled = false;
                txtCuit.Focus();
            }
            else
            {
                txtDocumento.Enabled = true;
                btnCompletarCuil.Enabled = true;
                txtDocumento.Focus();
            }
            
        }

        private void cmbPais_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPais.BackColor == Color.LightCoral)
                cmbPais.BackColor = Color.White;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if ( validarForm()) { 
                PaymentFormWriter.grabarCuil( txtCuit.Text );
                this.Close();
            }
            else
            {
                MessageBox.Show("Faltan completar campos", "Error de validacion", MessageBoxButtons.OK);
                this.Activate();
            }
        }

        private void btnVolverOne_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtCuit_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void cargarPaises()
        {
            foreach (var item in EntityLoader.loadPaises())
            {
                cmbPais.AutoCompleteCustomSource.Add(item.descripcionPais);
                cmbPais.Items.Add(item);
            }
        }

        private void cargarComboTipoPersonas()
        {
            Dictionary<string, string> test = new Dictionary<string, string>();
            test.Add("M", "Masculino");
            test.Add("F", "Femenino");
            test.Add("E", "Empresa");
            cmbTipoPersona.DataSource = new BindingSource(test, null);
            cmbTipoPersona.AutoCompleteCustomSource.Add("Masculino");
            cmbTipoPersona.AutoCompleteCustomSource.Add("Femenino");
            cmbTipoPersona.AutoCompleteCustomSource.Add("Empresa");
            cmbTipoPersona.DisplayMember = "Value";
            cmbTipoPersona.ValueMember = "Key";
        }

        private void FrmAveriguarCuil_Load(object sender, EventArgs e)
        {
            try
            {
               
                if (esExtranjero)
                {
                    cargarPaises();
                    cmbTipoPersona.Enabled = false;
                    //      txtDocumento.Enabled = false;
                    txtDocumento.Mask = "";
                    btnCompletarCuil.Enabled = false;
                    txtCuit.Enabled = false;
                    txtCuit.Text = "30641405554";
                    cmbPais.Enabled = true;
                }
                else
                {
                    cmbPais.Enabled = false;
                    txtCuit.Enabled = true;
                    btnCompletarCuil.Enabled = true;
                    cmbTipoPersona.Enabled = true;
                    cargarComboTipoPersonas();
                }
               
            }
            catch (Exception ex)
            {
                logger.Error("Error cargando formulario: " + ex);
                this.Activate();
                MessageBox.Show("Error al cargar formulario", "Error de carga", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void IsExtranjero_CheckedChanged(object sender, EventArgs e)
        {
            if (IsExtranjero.Checked)
            {
                esExtranjero = true;
            }else
            {
                esExtranjero = false;
            }

            FrmAveriguarCuil_Load(sender, e);
        }
    }
}
