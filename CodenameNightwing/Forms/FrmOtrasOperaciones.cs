using CodenameNightwing.Autorization;
using CodenameNightwing.BusinessLogic;
using CodenameNightwing.Config;
using CodenameNightwing.Exceptions;
using CodenameNightwing.FileManager;
using CodenameNightwing.Printer;
using CodenameNightwing.Printer.Cupones;
using CodenameNightwing.Varios;
using CodenameNightwing.WebServices;
using CodenameNightwing.WebServices.WSEspecificos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace CodenameNightwing.Forms
{
    public partial class FrmOtrasOperaciones : Form
    {
        public FrmOtrasOperaciones()
        {
            InitializeComponent();
            Icon = Properties.Resources.logo_con_borde_grueso;
            if (Config.Configuration.getInstance().tipoAuth == TipoAutorizador.VTOL)
            {
                rdbReimprimirCupon.Text = "Reimpresion de cupones";
                rdbReimprimirCupon.Visible = false;
                rdbReimprimirCierre.Visible = false;
                rdbVerificarConexion.Visible = false;
                rdbVerificarPinpad.Visible = true;
                rdbPrinterTest.Visible = true;
                rdbResetPinpad.Visible = true;
                rdbResetLibrary.Visible = true;
                rdnVerInfo.Visible = true;
                rdnVerificarSeguridad.Visible = true;
                rdnVerificarLogWk.Visible = true;
            }

            rdnEnviarLog.Visible = true;

        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            ejecutar();
        }


        private void FrmOtrasOperaciones_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.ExitThread();
        }

        private void ejecutar()
        {
            var rbAux = from rb in grpOpciones.Controls.OfType<RadioButton>() where rb.Checked select rb;
            if (rbAux.Count() == 0)
            {
                MessageBox.Show("Debe seleccionar una opcion!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Activate();
            }
            switch (rbAux.ToList()[0].Name)
            {
                case "rdbVerificarConexion":
                    lblMensaje.Text = "Verificando la conexión";
                    mostrarCartel();
                    verificarConexion();
                    pnlMensaje.Visible = false;
                    break;

                case "rdbReimprimirCupon":
                    lblMensaje.Text = "Reimprimiendo último cupón";
                    mostrarCartel();
                    reimprimirCupon();
                    pnlMensaje.Visible = false;
                    break;
                case "rdbReimprimirCierre":
                    lblMensaje.Text = "Reimprimiendo cierre de lote";
                    mostrarCartel();
                    reimprimirCierreLote();
                    pnlMensaje.Visible = false;
                    break;
                case "rdbVerificarPromocion":
                    lblMensaje.Text = "Pase la tarjeta por el POS";
                    mostrarCartel();
                    verificarPromocion();
                    pnlMensaje.Visible = false;
                    break;
                case "rdbCompraPrueba":
                    lblMensaje.Text = "Pase la tarjeta por el POS y" + Environment.NewLine + " siga las intrucciones del mismo";
                    mostrarCartel();
                    compraPrueba();
                    pnlMensaje.Visible = false;
                    break;
                case "rdbVerificarPinpad":
                    lblMensaje.Text = "Pase la tarjeta por el Pinpad y" + Environment.NewLine + " siga las intrucciones del mismo";
                    mostrarCartel();
                    verificarPinpad();
                    pnlMensaje.Visible = false;
                    break;
                case "rdbPrinterTest":
                    lblMensaje.Text = "Imprimiendo página de prueba";
                    mostrarCartel();
                    imprimirTest();
                    pnlMensaje.Visible = false;
                    break;
                case "rdbResetPinpad":
                    lblMensaje.Text = "Reseteando el Pinpad";
                    mostrarCartel();
                    resetPinpad();
                    pnlMensaje.Visible = false;
                    break;

                //rdnEnviarLog
                case "rdnEnviarLog":
                    lblMensaje.Text = "Enviando LOG a Sistemas";
                    mostrarCartel();
                    enviarLog();
                    pnlMensaje.Visible = false;
                    break;
                case "rdnEnviarLogPoi":
                    lblMensaje.Text = "Enviando LOG a Sistemas";
                    mostrarCartel();
                    enviarLogPoi();
                    pnlMensaje.Visible = false;
                    break;


                case "rdnVerInfo":
                    MessageBox.Show("Número de Serie de Pinpad es: " + Configuration.getInstance().idPinpad + "\nNúmero de Impresora: " + Configuration.getInstance().idPrinter + "\nNúmero de Caja es: " + Configuration.getInstance().caja, "Ver Información de la Caja", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case "rdnVerificarSeguridad":
                    
                    if (ConfigurationReader.checkSecurityWorkingKey())
                    {
                        MessageBox.Show("Número de Serie de Pinpad es: " + Configuration.getInstance().idPinpad + "\n" + "El pinpad está correctamente funcionando en materia de seguridad con sus working key.", "Pinpad Seguro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }else
                    {
                        MessageBox.Show("Número de Serie de Pinpad es: " + Configuration.getInstance().idPinpad + "\nHay problemas con el Working Key de este Pinpad. Pruebe lo siguiente:\n1. Reinicie Libreria\n2.Verifique conexión Pinpad\n3.Vuelva a ejecutar esta opción\n\nSino comuniquese con el proveedor de Sistema Propio", "Verificar Pinpad", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    
                    break;
                case "rdnVerificarLogWk":

                    if (ConfigurationReader.checkLogWorkingKey())
                    {
                        MessageBox.Show("Número de Serie de Pinpad es: " + Configuration.getInstance().idPinpad + "\n" + "La Working Key no presenta errores en el LOG.", "Pinpad Seguro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Número de Serie de Pinpad es: " + Configuration.getInstance().idPinpad + "\nLa Working Key presenta errores en el LOG. Llame a Synthesis", "Verificar Pinpad", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    break;

                case "rdbResetLibrary":
                    lblMensaje.Text = "Reiniciando Librería VTOL";
                    mostrarCartel();
                    bool outOk = true;
                    MessageBox.Show("Desconecte el Pinpad. Cuando esté listo click en OK", "Desconecte Pinpad", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    try
                    {
                        ServicesLibrary.RestartService();
                    }
                    catch ( ServiceException e)
                    {
                        outOk = false;
                        Program.logger.Error("Error en reset servicio" , e);
                    }

                    if ( outOk)
                    {
                        MessageBox.Show("Se reinicio correctamente la libreria. Conecte el Pinpad", "Reinicio OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Contacte a soporte tecnico para revisar el servicio: " + Configuration.getInstance().serviceVtolName, "Reinicio Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    
                    pnlMensaje.Visible = false;
                    break;
                default:
                    break;
            }
            Activate();
        }



        private void verificarConexion()
        {
            Autorizator aux = AutorizatorFactory.getAutorizator();
            if (aux.verificarConexion())
                MessageBox.Show("Éxito verificando conexión", "Conexion correcta", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Falló la verificación de conexión", "Conexion incorrecta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Activate();
        }

        private void reimprimirCupon()
        {
            if (Configuration.getInstance().tipoAuth == TipoAutorizador.POS_INGENICO)
            {
                Autorizator aux = AutorizatorFactory.getAutorizator();
                aux.reimprimirUltimoCupon();
            }
            else
            {
                FrmReimpresionCupones frm = new FrmReimpresionCupones();
                frm.ShowDialog();
            }
        }

        private void reimprimirCierreLote()
        {
            Autorizator aux = AutorizatorFactory.getAutorizator();
            aux.reimprimirCierreLote();
        }

        private void verificarPromocion()
        {
            Autorizator auxA = AutorizatorFactory.getAutorizator();
            Tarjeta auxT = auxA.solicitarNumeroTarjeta("Sale" , TransaccionBuilder.construirPago(1));
            FrmVerificarPromos auxFrm = new FrmVerificarPromos(auxT);
            auxFrm.ShowDialog();
        }

        private void verificarPinpad()
        {

            Autorizator auth = null;
            try
            {
                auth = AutorizatorFactory.getAutorizator();
                Tarjeta auxTar = auth.solicitarNumeroTarjeta("Sale", TransaccionBuilder.construirPago(1));
           
                if (auxTar != null)
                {
                    MessageBox.Show("Conexión con Pinpad exitosa!", "Transaccion correcta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Activate();
                }
                else
                {
                    MessageBox.Show("Problema con la conexión, revisa el Pinpad, el cable USB o Servicio de VTOL", "Transaccion incorrecta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Activate();
                }

            }catch( Exception e)
            {
                Program.logger.Error("Error al leer tarjeta", e);
            }
            finally
            {
                if (auth != null)
                    auth.cancelarLecturaTarjeta();
            }
            

        }


        private void imprimirTest()
        {

            PrinterCupon imprimir = new PrintPaginaPrueba(true);
            PrinterHelper ph = new PrinterHelper(imprimir.devolverCupon());
            ph.printCupon(imprimir , null );

        }

        private void enviarLogPoi()
        {

            try
            {

                FTPutil ftpUtil = new FTPutil(@"ftp://10.100.6.128/", "ftpmessint", "Jmj38vpz");
                string fileName = System.Environment.MachineName + "_" + Configuration.getInstance().sucursal + "_" + Configuration.getInstance().caja + "_POI.log";
                string fileLog = Application.StartupPath + "\\logs\\" + Environment.ExpandEnvironmentVariables(@"%USERNAME%") + "_" + DateTime.Now.ToString("dd.MM.yyyy") + ".log";
                string fileNamePath = Application.StartupPath + "\\logs\\" + fileName;

                System.IO.File.Copy(fileLog, fileNamePath, true);

                ftpUtil.upload("/opt/jboss/data_message_interact/logpoi/" + fileName, fileNamePath);
                MessageBox.Show("Se envió correctamente a Sistemas. Avisar para que puedan agarrar el log", "Transaccion correcta", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception e)
            {
                MessageBox.Show("Problema al enviar Log", "Transaccion incorrecta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Activate();
                Program.logger.Error("Error al enviar log", e);

            }

        }

        private void enviarLog()
        {

            try
            {

                FTPutil ftpUtil = new FTPutil(@"ftp://10.100.6.128/", "ftpmessint", "Jmj38vpz");
                string fileName = System.Environment.MachineName + "_" + Configuration.getInstance().sucursal + "_" + Configuration.getInstance().caja + "_VTOL.log";
                string fileLog = Application.StartupPath + "\\" + Configuration.getInstance().vtolPosClientLibFolder + "\\log\\lib.log";
                string fileNamePath = Application.StartupPath + "\\" + Configuration.getInstance().vtolPosClientLibFolder + "\\log\\" + fileName;


                System.IO.File.Copy(fileLog, fileNamePath, true);

                ftpUtil.upload("/opt/jboss/data_message_interact/logpoi/" +  fileName, fileNamePath);
                MessageBox.Show("Se envió correctamente a Sistemas. Avisar para que puedan agarrar el log", "Transaccion correcta", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception e)
            {
                MessageBox.Show("Problema al enviar Log", "Transaccion incorrecta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Activate();
                Program.logger.Error("Error al enviar log", e);

            }

        }

        private void resetPinpad()
        {

            try
            {
                if (Configuration.getInstance().tipoAuth == TipoAutorizador.VTOL)
                {
                    Autorizator auth = AutorizatorFactory.getAutorizator();

                    auth.cancelarLecturaTarjeta();
                    auth.cancelarTransaccion();

                    MessageBox.Show("Pinpad reseteado con éxito", "Transaccion correcta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Activate();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Problema el reinicio el pinpad", "Transaccion incorrecta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Activate();
                Program.logger.Error("Error al reiniciar pinpad", e );
               
            }
        
                
        }


        private void compraPrueba()
        {

            Autorizator auth = AutorizatorFactory.getAutorizator();
            try
            {
                Tarjeta auxTar = auth.solicitarNumeroTarjeta("Sale" , TransaccionBuilder.construirPago(1));
                if (auxTar != null)
                {

                    WebServiceBines wsBin = new WebServiceBines(auxTar.primeros6(), 1);
                    string codTarjeta = WebResponseParser.parseXMLBines(wsBin.getResponse())[0].codTarjetaSabre;
                    TarjetaCajero auxTc = EntityLoader.loadTarjetas().First(x => x.codTarjetaSabre == codTarjeta);

                    Transaccion tran = TransaccionBuilder.construirPago(auxTc, 1, 10, TipoModoTransaccion.ONLINE);
                    tran.eventHandler += this.handleEventTransaction;

                    tran.tarjeta = auxTar;
                    Transaccion resultado = auth.realizarTransaccion(tran);
                    if (resultado != null)
                    {
                        MessageBox.Show("Compra realizada con éxito. Anulando transacción", "Transaccion correcta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Activate();
                        Transaccion anular = TransaccionBuilder.construirAnulacion(resultado.trxReferenceId);
                        anular.eventHandler += this.handleEventTransaction;
                        Transaccion resultadoAnulacion = auth.realizarTransaccion(anular);
                        if (resultadoAnulacion != null)
                        {
                            MessageBox.Show("Anulación realizada con éxito", "Transaccion correcta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Activate();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Problema con la compra", "Transaccion incorrecta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Activate();
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Problema con la compra" + e, "Transaccion incorrecta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                auth.cancelarTransaccion();
                Program.logger.Error("Error compra de prueba: ", e);
            }
            
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            //Application.ExitThread();
            Close();
        }

        private void mostrarCartel()
        {
            pnlMensaje.Visible = true;
            pnlMensaje.Refresh();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rdbCompraPrueba_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rdbPrinterTest_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged_1(object sender, EventArgs e)
        {

        }

        private void FrmOtrasOperaciones_Load(object sender, EventArgs e)
        {

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

        private void pnlMensaje_Paint(object sender, PaintEventArgs e)
        {

        }

        private void rdnVerInfo_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rdnVerificarSeguridad_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void grpAcciones_Enter(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged_2(object sender, EventArgs e)
        {

        }

        private void rdnEnviarLog_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
