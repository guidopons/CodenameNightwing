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
using CodenameNightwing.WebServices.Response;
using log4net;

namespace CodenameNightwing.Forms
{
    public partial class FrmConfiguracionPOI : Form
    {

        public static readonly ILog logger = LogManager.GetLogger(typeof(Program));

        private bool _isValid;
        public bool isValid
        {
            get { return _isValid; }
            set { _isValid = value; }
        }

        public FrmConfiguracionPOI()
        {
            InitializeComponent();
            Icon = Properties.Resources.logo_con_borde_grueso;
        }

        public void loadFrm( )
        {

            txtCaja.Text = Configuration.getInstance().caja;
            txtCiudad.Text = Configuration.getInstance().ciudadStar;
            txtIdPrinter.Text = Configuration.getInstance().idPrinter;
            txtSucursal.Text = Configuration.getInstance().sucursal;
            txtPuesto.Text = Configuration.getInstance().puesto;

        }


        private bool setPermisosServicio()
        {

            string output = CmdCommand.ExecuteCommandSync("sc sdshow \"EMV-KIT AR\"");
            Regex regex = new Regex("(D:(?:(?!\\w:).)*)(.*)");
            Match match = regex.Match(output);
            if (match.Success)
            {
                string dOutput = match.Groups[1].Value;
                string restOutput = match.Groups[2].Value;

                output = CmdCommand.ExecuteCommandSync("\"whoami /all\"");
                regex = new Regex("Usuarios\\s{2,}(?:(?!S\\-).)*(S-[^\\s]*)");
                match = regex.Match(output);
                string SID = null;

                if (match.Success)
                {
                    SID = match.Groups[1].Value;

                }
                else
                {
                    SID = Configuration.getInstance().sidUsuarios;
                }

                string commandPermisos = "sc sdset \"EMV-KIT AR\" " + dOutput + "(A;;RPWPDTLO;;;" + SID + ")" + restOutput;
                output = CmdCommand.ExecuteCommandSync(commandPermisos);
                
                if (output != null && output.Contains("CORRECTO"))
                {
                    return true;
                }
                else
                {
                    logger.Error("Mensaje de permisos: " + output);
                }

            }
            return false;
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            if (validarForm()) {

                isValid = true;
                PropertiesFile propFile = new PropertiesFile(Configuration.getInstance().aerolineasPropertiesPath);


                string cashier = "N";
                string cashierSeller = "N";

                if (cmbEsCajero.Text.Contains("Vendedor con Cajero"))
                {
                    // Pasar los tickets a NOGO
                    cashierSeller = "Y";
                }
                else
                {
                    if (cmbEsCajero.Text.Contains("Cajero solamente"))
                    {
                        cashier = "Y";
                    }
                }

                propFile.set("ar.qik.vtol.sucursal" , txtSucursal.Text );
                propFile.set("ar.qik.interact.cashier" , cashier);
                propFile.set("ar.qik.interact.cashierSeller" , cashierSeller);
                propFile.set("ar.qik.interact.ciudadSTAR" , txtCiudad.Text);
                propFile.set("ar.qik.printer.idPrinter" , txtIdPrinter.Text);
                propFile.set("ar.qik.vtol.caja" , txtCaja.Text);
                propFile.set("ar.qik.vtol.puesto", txtPuesto.Text);

                propFile.Save();
                MessageBox.Show("Datos guardados correctamente", "Datos Guardados", MessageBoxButtons.OK , MessageBoxIcon.Information);

                if (Configuration.getInstance().tipoAuth == TipoAutorizador.VTOL)
                {
                    MessageBox.Show("Se van a establecer los permisos sobre el servicio EMV-KIT AR", "Servicio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (setPermisosServicio())
                    {
                        MessageBox.Show("Permisos otorgados correctamente", "Servicio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Error al otorgar permisos, realizar manualmente", "Servicio", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }

                Close();
            }
            else
            {
                isValid = false;
                MessageBox.Show("Faltan completar campos o el formato no es el correcto. Revise los campos resaltados", "Error de validacion", MessageBoxButtons.OK , MessageBoxIcon.Error);
                this.Activate();
            }
        }


        private bool validarForm()
        {
            bool bandera = true;


            if (cmbEsCajero.Text.Trim().Length == 0)
            {
                bandera = false;
                cmbEsCajero.BackColor = Color.LightCoral;
            }
            else
                cmbEsCajero.BackColor = Color.White;

            //txtSucursal
            if (txtSucursal.Text.Trim().Length == 0 )
            {
                bandera = false;
                txtSucursal.BackColor = Color.LightCoral;
            }
            else
                txtSucursal.BackColor = Color.White;

            //txtCaja
            if (txtCaja.Text.Trim().Length == 0 )
            {
                bandera = false;
                txtCaja.BackColor = Color.LightCoral;
            }
            else
                txtCaja.BackColor = Color.White;

            //txtCiudad
            if (txtCiudad.Text.Trim().Length == 0 )
            {
                bandera = false;
                txtCiudad.BackColor = Color.LightCoral;
            }
            else
                txtCiudad.BackColor = Color.White;

            //txtIdPrinter
            if (txtIdPrinter.Text.Length == 0 )
            {
                bandera = false;
                txtIdPrinter.BackColor = Color.LightCoral;
            }
            else
                txtIdPrinter.BackColor = Color.White;


            //txtPuesto
            if (txtPuesto.Text.Length == 0)
            {
                bandera = false;
                txtPuesto.BackColor = Color.LightCoral;
            }
            else
                txtPuesto.BackColor = Color.White;

            Terminal terminal = new BusinessLogic.Terminal();
            terminal.baseArsa = txtCiudad.Text;
            terminal.caja = txtCaja.Text;
            terminal.printerId = txtIdPrinter.Text;
            terminal.sucursal = txtSucursal.Text;

            WebServiceTerminal ws = new WebServiceTerminal(terminal);
            TerminalResponse terminalResponse = WebResponseParser.parseXMLInsertTerminal(ws.getResponse());
            if (terminalResponse != null && terminalResponse.status.Equals("TERMINAL_EXISTE"))
            {
                
                logger.Error("La terminal que trata de configurar en esta maquina ya fue asignada");
                MessageBox.Show("La terminal que trata de configurar en esta maquina ya fue asignada. Caja: " + terminal.caja + " y Sucursal: " + terminal.sucursal + " MacAddress: " + terminalResponse.terminalEncontrada.macAddress, "Verifique campos de configuración", MessageBoxButtons.OK, MessageBoxIcon.Error);
                bandera = false;
                txtSucursal.BackColor = Color.LightCoral;
                txtCaja.BackColor = Color.LightCoral;

            }

            if (terminalResponse == null || (terminalResponse.status.Equals("NO_CONN") || terminalResponse.status.Equals("APP_ERROR")))
            {

                logger.Error("Ocurrio un error con el WS Terminal.");
                MessageBox.Show("No se puede comunicar con el web Service de Terminales. Contacte el CIP para su resolucion", "Verifique con CIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                bandera = false;
                txtSucursal.BackColor = Color.LightCoral;
                txtCaja.BackColor = Color.LightCoral;

            }


            return bandera;
        }

        private void lblEsCajero_Click(object sender, EventArgs e)
        {

        }

        private void cmbEsCajero_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
