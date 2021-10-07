using System;
using System.Windows.Forms;
using CodenameNightwing.Printer;
using CodenameNightwing.Printer.Cupones;
using CodenameNightwing.BusinessLogic;
using CodenameNightwing.Autorization.VTOL.Config.Elementos;
using System.Collections.Generic;
using CodenameNightwing.Autorization.VTOL.Config;
using CodenameNightwing.Autorization;
using CodenameNightwing.Forms;
using System.Security.Cryptography;
using System.Linq;
using System.Text;
using CodenameNightwing.Crypto;
using CodenameNightwing.Varios;
using CodenameNightwing.Config;
using CodenameNightwing.Sabre;
using CodenameNightwing.Valtech.FraudService;
using CodenameNightwing.Valtech.FraudService.Response;
using CodenameNightwing.Valtech.PromoService;
using CodenameNightwing.Valtech.PromoService.Response;
using CodenameNightwing.WebServices;
using Newtonsoft.Json;
using System.IO;
using System.Web.Script.Serialization;
using CodenameNightwing.Valtech.Objects;

namespace CodenameNightwing.Pruebas
{
    public partial class FrmPruebas : Form
    {
        public FrmPruebas()
        {
            InitializeComponent();
            Icon = Properties.Resources.logo_con_borde_grueso;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            ///*  prueba configuration manager */
            //ConfigurationReader.readAerolineasProperties();
            //Configuration conf = Configuration.getInstance();
            //Type tipo = conf.GetType();
            //foreach (PropertyInfo item in tipo.GetProperties())
            //{
            //    lblPrueba.Text += item.GetValue(conf, null).ToString() + Environment.NewLine;
            //}

            /*prueba webservice tarjetas */
            //WebServiceBase prueba = new WebServiceTarjetas();
            //prueba.writeQuery();
            //prueba.sendRequest();
            //foreach (var item in WebResponseParser.parseXMLTarjetas(prueba.getResponse()))
            //{
            //    textBox1.Text += item.codComercio + " | " + item.codNumTarjeta + " | " + item.codTarjetaSabre + " | " + item.descripcionTarjeta + " | " + item.tipo.ToString() + Environment.NewLine;
            //}

            /* PRUEBA WS BINES */
            //WebServiceBase prueba = new WebServiceBines("450799");
            //prueba.writeQuery();
            //prueba.sendRequest();
            //foreach (var item in WebResponseParser.parseXMLBines(prueba.getResponse()))
            //{
            //    textBox1.Text += item.banco + " | " + item.codTarjeta + " | " + item.codTarjetaSabre + " | " + item.cuotas + " | " + item.descripcionTarjeta + " | " + item.fechaDesde.ToShortDateString() + " | " + item.porcentaje.ToString() + Environment.NewLine;
            //}

            /*  PRUEBA LOAD VBREQUEST PAYMENT */
            //List<Pnr> aux = VBrequestReader.leerPago();
            //foreach (var a in aux) 
            //{
            //    textBox1.Text += "codigo reserva: " + a.codSabre + Environment.NewLine;
            //    textBox1.Text += "tipo itinerario: " + a.tipoItinerario + Environment.NewLine;
            //    foreach (var p in a.pasajeros)
            //    {
            //        textBox1.Text += "  nombre pasajero: " + p.nombre + ": " + p.fare + Environment.NewLine;
            //        foreach (var emd in p.emds)
            //        {
            //            textBox1.Text += "      descripcion emd:" + emd.descripcion + ":  " + emd.fare + Environment.NewLine;
            //        }
            //    }
            //}

            /* PRUEBA LOAD VBREQUEST VOID */
            //Devolucion aux = VBrequestReader.leerVoid();
            //textBox1.Text += "Ticket original: " + aux.ticketOriginal + Environment.NewLine;
            //textBox1.Text += "Fecha Original: " + aux.fechaOriginal + Environment.NewLine;
            //textBox1.Text += "Codigo de Tarjeta: " + aux.tarjeta.codSabre + Environment.NewLine;
            //textBox1.Text += "Primeros 6: " + aux.tarjeta.primeros6() + Environment.NewLine;
            //textBox1.Text += "Ultimos 4: " + aux.tarjeta.ultimos4() + Environment.NewLine;

            /* PRUEBA LOAD VBREQUEST ANULACION */
            //Devolucion aux = VBrequestReader.leerRefund();
            //textBox1.Text += "Ticket original: " + aux.ticketOriginal + Environment.NewLine;
            //textBox1.Text += "Fecha Original: " + aux.fechaOriginal + Environment.NewLine;
            //textBox1.Text += "Codigo de Tarjeta: " + aux.tarjeta.codSabre + Environment.NewLine;
            //textBox1.Text += "Primeros 6: " + aux.tarjeta.primeros6() + Environment.NewLine;
            //textBox1.Text += "Ultimos 4: " + aux.tarjeta.ultimos4() + Environment.NewLine;
            //textBox1.Text += "Importe: " + aux.importeTotal + Environment.NewLine;

            /* PRUEBAS POS: SOLICITAR TARJETA */
            //POSIntegrator aux = new POSIntegrator();
            //Tarjeta resultado = aux.solicitarNumeroTarjeta();
            //textBox1.Text += "Numero Tarjeta: " + resultado.numero + Environment.NewLine;

            /* PRUEBAS POS: COMPRA Y ANULACION */
            //POSIntegrator aux = new POSIntegrator();
            //Transaccion tran = new Transaccion();
            //tran.tipoTrans = TipoTransaccion.COMPRA;
            //tran.importeTotal = 1.0M;
            //Comercio auxCom = new Comercio();
            //auxCom.codigoComercio = "03659307";
            //tran.comercio = auxCom;
            //tran.cantCuotas = 1;
            //Tarjeta auxTar = new Tarjeta();
            //auxTar.codTarjeta = "042";
            //tran.tarjeta = auxTar;
            //Transaccion resultado = aux.realizarTransaccion(tran);
            //resultado.tipoTrans = TipoTransaccion.ANULACION;
            //textBox1.Text += "Numero de autorizacion: " + resultado.numAutorizacion + Environment.NewLine;
            //Transaccion resultadoDevolucion = aux.realizarTransaccion(resultado);
            //textBox1.Text += "Numero de autorizacion voideo: " + resultadoDevolucion.numAutorizacion + Environment.NewLine;

            //POSIntegrator aux = new POSIntegrator();
            //Transaccion tran = TransaccionBuilder.construirPago("03659307", 1, 1.0M, "042");
            //Transaccion resultado = aux.realizarTransaccion(tran);
            //textBox1.Text += "Numero de autorizacion: " + resultado.numAutorizacion + Environment.NewLine;
            //Transaccion devolucion = TransaccionBuilder.construirAnulacion(resultado.numTicket.ToString(), resultado.fecha, resultado.tarjeta);
            //Transaccion resultadoDevolucion = aux.realizarTransaccion(devolucion);
            //textBox1.Text += "Numero de autorizacion voideo: " + resultadoDevolucion.numAutorizacion + Environment.NewLine;

            /* PRUEBAS POS: CIERRE LOTE */
            //POSIntegrator aux = new POSIntegrator();
            //Transaccion tran = new Transaccion();
            //tran.tipoTrans = TipoTransaccion.CIERRE;
            //Transaccion resultado = aux.realizarCierreLote(tran);
            //textBox1.Text += "Estado: " + resultado.estado + Environment.NewLine;
            //textBox1.Text += "Fecha: " + resultado.fecha + Environment.NewLine;

            /* PRUEBA VOIDEO MANUAL */
            //Tarjeta tar = new Tarjeta();
            //tar.codTarjeta = "042";
            //tar.numero = "4507990000977787";
            //POSIntegrator aux = new POSIntegrator();
            //Transaccion devolucion = TransaccionBuilder.construirAnulacion("0216", new DateTime(2015,9,25), tar);
            //Transaccion resultadoDevolucion = aux.realizarTransaccion(devolucion);
            //textBox1.Text += "Numero de autorizacion voideo: " + resultadoDevolucion.numAutorizacion + Environment.NewLine;

            /* PRUEBA LOAD EXCHANGE */
            //Transaccion auxtrans = VBrequestReader.leerExchange();
            //textBox1.Text += "Importe a cambiar: " + auxtrans.importeTotal.ToString("#####0.0") + Environment.NewLine;
            //textBox1.Text += "Fecha: " + auxtrans.fecha.ToString() + Environment.NewLine;

            /* PRUEBAS INTEGRADOR UI PROPIO */
            //NativeMethods.VisuDisp("terminal", "esta es una pelotudez", "esta es otra pelotudez", 60);
            //NativeMethods.VisuError("terminal", "linea1", "linea2", 60);
            //NativeMethods.StartProcess("terminal", "linea1");
            //NativeMethods.EndProcess("terminal");
            //string auxHola="";
            //NativeMethods.CaptDato("terminal", "linea1",ref auxHola, 60, false, 60);
            //int largotrack1 = 20;
            //int largotrack2 = 20;
            //NativeMethods.ReadCardReader("terminal", "trackI", ref largotrack1, "trackII", ref largotrack2, "promociones", 60);
            //NativeMethods.setPosVirtual();

            //Transaccion devolucion = TransaccionBuilder.construirAnulacion(resultado.numTicket.ToString(), resultado.fecha, resultado.tarjeta);
            //Transaccion resultadoDevolucion = auth.realizarTransaccion(devolucion);
            //textBox1.Text += "Numero de autorizacion voideo: " + resultadoDevolucion.numAutorizacion + Environment.NewLine;

            /* PRUEBA AUTORIZACION HASAR */
            /*Autorizator auth = AutorizatorFactory.getAutorizator();
            Transaccion tran = TransaccionBuilder.construirPago("03659307", 1, 925.0M, "21", 1);
            Transaccion resultado = auth.realizarTransaccion(tran);
            if (resultado != null)
            {
                textBox1.Text += "Importe total: " + resultado.importeTotal + Environment.NewLine;
                textBox1.Text += "Cantidad de cuotas: " + resultado.cantCuotas + Environment.NewLine;
                textBox1.Text += "Numero de autorizacion: " + resultado.numAutorizacion + Environment.NewLine;
                textBox1.Text += "Numero de ticket: " + resultado.numTicket + Environment.NewLine;
                textBox1.Text += "Numero de lote: " + resultado.numLote + Environment.NewLine;
                textBox1.Text += "Numero unico: " + resultado.numeroUnico + Environment.NewLine;
                textBox1.Text += "Fecha: " + resultado.fecha + Environment.NewLine;
                textBox1.Text += "Tipo CUIT/CUIL dueño: " + resultado.tarjeta.owner.tipoCuitCuil + Environment.NewLine;
                textBox1.Text += "CUIT dueño: " + resultado.tarjeta.owner.cuitCuil + Environment.NewLine;
                textBox1.Text += "Documento dueño: " + resultado.tarjeta.owner.documento + Environment.NewLine;
                textBox1.Text += "Vencimiento tarjeta: " + resultado.tarjeta.vencimiento + Environment.NewLine;
                textBox1.Text += "Numero tarjeta: " + resultado.tarjeta.numero + Environment.NewLine;
                textBox1.Text += "Pais: " + resultado.pais.descripcionPais + Environment.NewLine;
                textBox1.Text += "--------------------------" + Environment.NewLine;
            }
            else
            {
                textBox1.Text += "Error en la transaccion" + Environment.NewLine;
                textBox1.Text += "--------------------------" + Environment.NewLine;
            }*/

            /* PRUEBA ANULACION HASAR */
            //Autorizator auth = AutorizatorFactory.getAutorizator();
            ////Transaccion tran = TransaccionBuilder.construirPago("03659307", 1, 925.0M, "21", 1);
            //Tarjeta tar = new Tarjeta();
            //tar.codTarjeta = "21";
            //tar.numero = "4507990000977787";
            //Transaccion tran = TransaccionBuilder.construirAnulacion("0", DateTime.Now, tar);
            //tran.importeTotal = 150.00M;
            //tran.cantCuotas = 1;
            //tran.numeroUnico = 1;
            //tran.numTicket = "1";
            //Transaccion resultado = auth.realizarTransaccion(tran);
            //if (resultado != null)
            //{
            //    textBox1.Text += "Importe total: " + resultado.importeTotal + Environment.NewLine;
            //    textBox1.Text += "Cantidad de cuotas: " + resultado.cantCuotas + Environment.NewLine;
            //    textBox1.Text += "Numero de autorizacion: " + resultado.numAutorizacion + Environment.NewLine;
            //    textBox1.Text += "Numero de ticket: " + resultado.numTicket + Environment.NewLine;
            //    textBox1.Text += "Numero de lote: " + resultado.numLote + Environment.NewLine;
            //    textBox1.Text += "Numero unico: " + resultado.numeroUnico + Environment.NewLine;
            //    textBox1.Text += "Fecha: " + resultado.fecha + Environment.NewLine;
            //    textBox1.Text += "Tipo CUIT/CUIL dueño: " + resultado.tarjeta.owner.tipoCuitCuil + Environment.NewLine;
            //    textBox1.Text += "CUIT dueño: " + resultado.tarjeta.owner.cuitCuil + Environment.NewLine;
            //    textBox1.Text += "Documento dueño: " + resultado.tarjeta.owner.documento + Environment.NewLine;
            //    textBox1.Text += "Vencimiento tarjeta: " + resultado.tarjeta.vencimiento + Environment.NewLine;
            //    textBox1.Text += "Numero tarjeta: " + resultado.tarjeta.numero + Environment.NewLine;
            //    textBox1.Text += "Pais: " + resultado.pais.descripcionPais + Environment.NewLine;
            //    textBox1.Text += "--------------------------" + Environment.NewLine;
            //}
            //else
            //{
            //    textBox1.Text += "Error en la transaccion" + Environment.NewLine;
            //    textBox1.Text += "--------------------------" + Environment.NewLine;
            //}

            /* PRUEBA APICLIENTE */
            //APICliente.StructsComunicacion.datosMP auxDatosMP = new APICliente.StructsComunicacion.datosMP();
            //auxDatosMP.bin = 450799;
            //APICliente.StructsComunicacion.datosPagosNet auxDatosPagos = new APICliente.StructsComunicacion.datosPagosNet(true);
            //auxDatosPagos.cantidad_pagos = 1;
            //APICliente.StructsComunicacion.RespuestaNet auxRespuesta = new APICliente.StructsComunicacion.RespuestaNet();
            //auxRespuesta.monto_original = 35075;
            //auxDatosPagos.rta[0] = auxRespuesta;
            //String auxDatosMP = "{'monto': '1533', 'bin': '450799', 'banco': 'GALICIA', 'es_tarjeta_valida': '1', 'es_banco_valido': '1', 'codigo_banco': '109', 'codigo_marca': '21', 'marca': 'visa', 'nroCuotas': '7', 'tipoTarjeta': 'C'}";
            //String auxRespuesta = new String('\0',1000);
            //NativeMethods.aplicarOfertasNet(ref auxDatosMP, ref auxRespuesta);
            //textBox1.Text += "Respuesta: " + auxRespuesta;
            //textBox1.Text += "Numero de comercio: " + auxDatosPagos.rta[0].nroComercio + Environment.NewLine;
            //textBox1.Text += "Cantidad de cuotas: " + auxDatosPagos.rta[0].nroCuotas + Environment.NewLine;
            //textBox1.Text += "Tipo de identificador: " + auxDatosPagos.rta[0].tipo_identificador + Environment.NewLine;
            //textBox1.Text += "Monto recargo: " + auxDatosPagos.rta[0].monto_rd + Environment.NewLine;
            //textBox1.Text += "Monto original: " + auxDatosPagos.rta[0].monto_original + Environment.NewLine;
            //textBox1.Text += "Tasa aplicada: " + auxDatosPagos.rta[0].tasa_aplicada + Environment.NewLine;

            /* PRUEBA APLICAR OFERTAS NET */
            /*   Request req = new Request(true);
               req.bin = "450799";
               req.monto = "100";
               DatosPagosNet resp = new DatosPagosNet(true);
               String sReq = JsonConvert.SerializeObject(req);
               String sResp = JsonConvert.SerializeObject(resp);
               int res = NativeMethods.aplicarOfertasNet(ref sReq, ref sResp);
               MessageBox.Show(sResp);*/

            /* PRUEBA WS GET NUMERO UNICO */
            //WebServiceGetNumeroUnico ws = new WebServiceGetNumeroUnico(Convert.ToInt32(Configuration.getInstance().sucursalHAS), Convert.ToInt32(Configuration.getInstance().cajaHAS), 26962);
            //int respuesta = WebResponseParser.parseXMLGetNumeroUnico(ws.getResponse());
            //if (respuesta > 0)
            //    textBox1.Text += "Numero unico: " + respuesta + Environment.NewLine;
            //else
            //    textBox1.Text += "Error al buscar numero unico" + Environment.NewLine;

            //Prueba impresion de cosas en impresora
            //Autorizator auth = AutorizatorFactory.getAutorizator();
            //Transaccion tran = TransaccionBuilder.construirPago("03659307", 1, 925.0M, "21", 1);
            //Transaccion resultado = auth.realizarTransaccion(tran);
            /* Transaccion resultado = TransaccionBuilder.construirPago(10.00m);
             resultado.cantCuotas = 6;
             resultado.codBanco = 1.ToString();
             resultado.comercio = new Comercio();
             resultado.comercio.caja = new Caja();
             resultado.comercio.codigoComercio = "12121";
             resultado.comercio.codigoTarjetaTrans = "3213213";
             resultado.estado = "1";
             resultado.fecha = DateTime.Now;
             resultado.modo = 1;
             resultado.moneda = "P";
             resultado.numAutorizacion = "123456";
             resultado.trxId = 12121212;
             resultado.numLote = 1;
             resultado.numTicket = "1234";
             resultado.nroTerminal = "1234";
             resultado.tarjeta.descripcion = "visa";
             resultado.tarjeta.vencimiento = "1219";
             resultado.tarjeta.numero = "450799xxxxxx7787";
             resultado.tarjeta.codPlan = "CUOTAS";
             resultado.tipoIngreso = TipoIngresoTarjeta.MSR;
             if (resultado != null)
             {
                 PrinterCupon imprimir = new PrintCompraCupon(resultado, EnumCopiaUOriginal.ORIGINAL);
                 PrinterHelper ph = new PrinterHelper(imprimir.devolverCupon());
                 ph.imprimir();
             }*/

            //PRUEBA AUTORIZACION CON VTOL
            /* Autorizator auth = Autorization.VTOL.VTOLIntegrator.Instance;
             Transaccion prueba = TransaccionBuilder.construirPago(10.00M);
             Transaccion resultado = auth.realizarTransaccion(prueba);*/

            //PRUEBA START, CHECK Y STOP VTOL FULL LIBRARY
            //VTOLFullLibrary.startServer();
            //if (VTOLFullLibrary.checkServer())
            //    VTOLFullLibrary.stopServer();

            //PRUEBA getPlanesDePagosSegunPrefijo : obteniendo plan de pagos para VTOL segun su archivo de configuracion
            /*string bin = "569477";
            int cantCuotas = 6;
            PlanPagos resultado = VTOLConfiguration.getInstance().getPlanesDePagosSegunPrefijo(bin,cantCuotas);*/

            //Prueba devolucion con VTOL
            /*string ticketOriginal = "10";
            int trxOriginal = 11;
            DateTime fechaOriginal = new DateTime(2016,10,18);
            decimal importeTotal = 150;
            Tarjeta auxTar = new Tarjeta();
            auxTar.numero = "4507990000977787";
            Transaccion auxDevol = TransaccionBuilder.construirDevolucion(ticketOriginal, trxOriginal, fechaOriginal, importeTotal, auxTar);
            auxDevol.cantCuotas = 1;
            Autorizator auth = AutorizatorFactory.getAutorizator();
            Transaccion resultado = auth.realizarTransaccion(auxDevol);
            if (resultado != null)
                MessageBox.Show("Ticket resultado de devolucion: " + resultado.trxId);*/

            //string[] lineas = { ESCPOS.checkPaper };
            //PrinterHelper ph = new PrinterHelper( lineas );
            //ph.imprimir();

            //Tarjeta auxTar = new Tarjeta();
            //auxTar.numero = "501063999000007007";
            //auxTar.codSabre = "MA";
            //FrmNumerosRestantes form = new FrmNumerosRestantes(auxTar);
            //form.ShowDialog();

            //FrmPruebas.testCrypto();

            //PRUEBA TRANSACCION CON IVR DE PAGOS
            /*
                        
                        Transaccion tran = TransaccionBuilder.construirPago(21);
                        tran.tarjeta.numero = "4507990000004907";
                        Autorizator auth = AutorizatorFactory.getAutorizator();
                        auth.obtenerDatosTarjetaIvr( tran );
            */

            //Prueba fraude
            /*FraudServiceController fraudController = new FraudServiceController();
            FraudServiceResponse fraudResponse = fraudController.checkFraud(tran);

            if (fraudResponse != null && fraudResponse.fraudCheckInformation != null && fraudResponse.fraudCheckInformation.result.Equals("REJECT"))
            {
                MessageBox.Show("El analisis de Fraude de la transaccion dio REJECT. Favor cambiar medio de pago, la transaccion se anulará/reversará.", "Rechazado por Fraude", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tran.respuestaHost = "FRAUD REJECTED";
            }

            auth.checkFraud(tran);
            */


            //PRUEBA WS Sabre
            /*
            SabreController sabreController = new SabreController();
            string pnr = "IQCAYX"; //PEDROR";//"HCODYR"; // "TMAOFI";
            string ccNumber = "4507990000000010";
            string ccVto = "12/20";
            string ccCode = "VI";

            if (!sabreController.addTBMtoPNR(pnr, ccNumber, ccVto, ccCode)) {
                MessageBox.Show("Error Web Services addTMBPNR", "Error de WS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            */


            //Prueba WS VALTECH promo TC

            //Prueba WS VALTECH EMD Inte TC

            /*
            EMDServiceController emdController = new EMDServiceController();

            Transaccion tran = TransaccionBuilder.construirPago(21);
            tran.tarjeta.numero = "4507990000004907";
            tran.tarjeta.codTarjeta = "BA";


           // EMDServiceResponse emdResponse = emdController.addEMD(tran);
            


            PromoServiceController promoController = new PromoServiceController();
            PromoServiceResponse promoResponse = promoController.checkPromo(tran);
            //string jsonResponse = promoController.checkPromo(ccCode, bin, 1000, null);
            // PromoServiceResponse promoResponse = promoController.checkPromo(bin);
            //Object m = JsonConvert.DeserializeObject<Movie>(promoResponse);

            Dictionary<string, List<cuota>> d = promoResponse.fixedInstallments;
            List<Promocion> lAux = new List<Promocion>();

            foreach (KeyValuePair<string, List<cuota>> kvp in d)
            {
                int cuotas = kvp.Value[0].installmentCount;
                decimal installmentAmount = kvp.Value[0].installmentAmount;
                decimal porcentaje = kvp.Value[0].interestRate;
                decimal totalAmount = kvp.Value[0].totalAmount;
                decimal totalInterest = kvp.Value[0].totalInterest;
                string codTc = kvp.Value[0].paymentMethod;
                string codTcSabre = kvp.Value[0].paymentMethod;
                string descTc = kvp.Value[0].paymentMethod;
                

                Promocion p = new Promocion (cuotas, "", codTc, codTcSabre, DateTime.Now, descTc, porcentaje, false);
                lAux.Add(p);
            }


    */


            //Promocion p = new Promocion(cuotas, banco, codTarjeta, codTarjetaSabre, DateTime.Now, descripcionTarjeta, porcentaje, binExcepcion);




            // promos = WebResponseParser.parseXMLBines(wsPromo.getResponse());


            //List <Promocion> promos;


        }

        public static void testCrypto()
        {
            try
            {

                string password = "Here is some data to encrypt!";
                string original = "Here is some data to encrypt!";

                string encStr = AESCrypto.Encrypt(original , password);


            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
            }

        }





    }
}
