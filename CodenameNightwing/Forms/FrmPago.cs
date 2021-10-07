using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CodenameNightwing.Varios;
using CodenameNightwing.FileManager;
using CodenameNightwing.Autorization;
using CodenameNightwing.BusinessLogic;
using CodenameNightwing.Config;
using log4net;
using CodenameNightwing.Valtech.Exceptions;

namespace CodenameNightwing.Forms
{
    public partial class FrmPago : Form
    {
       
        public static readonly ILog logger = LogManager.GetLogger(typeof(FrmPago));

        private List<Pnr> _lsPnrs;
        public List<Pnr> lsPnrs
        {
            get { return _lsPnrs; }
            set { _lsPnrs = value; }
        }


        private decimal _aAutorizar;
        public decimal aAutorizar
        {
            get { return _aAutorizar; }
            set { _aAutorizar = value; }
        }

        private decimal _faltaAutorizar;
        public decimal faltaAutorizar
        {
            get { return _faltaAutorizar; }
            set { _faltaAutorizar = value; }
        }

        private List<Transaccion> _transaccionesAGuardar;
        public List<Transaccion> transaccionesAGuardar
        {
            get { return _transaccionesAGuardar; }
            set { _transaccionesAGuardar = value; }
        }

        public FrmPago()
        {
            InitializeComponent();
            Icon = Properties.Resources.logo_con_borde_grueso;
        }

        private void FrmPago_Load(object sender, EventArgs e)
        {
            cargarArbolAPagar();
            txtTotal.Text = aAutorizar.ToString("######0.00").Replace(",", ".");
            faltaAutorizar = aAutorizar;
            txtFaltaAutorizar.Text = faltaAutorizar.ToString("######0.00").Replace(",", ".");
            treeAutorizado.Nodes.Add("noAutorizacion", "No se registran autorizaciones").HideCheckBox();
            transaccionesAGuardar = new List<Transaccion>();

            Transaccion tranAuxPrice = TransaccionBuilder.construirPago(1);
            lblPriceInformation.Text = tranAuxPrice.getPriceInformation();

            if ( Configuration.getInstance().tipoAuth.Equals(TipoAutorizador.VTOL_CALLCENTER))
            {
                btnCreditoExt.Text = "Tarj. &Extranjeras";
            }

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

        private void cargarArbolAPagar()
        {
            this.lsPnrs = VBrequestReader.leerPnrs();

            treeAutorizar.Nodes.Add("aAutorizar", "PNRs a autorizar");
            foreach (Pnr aux in lsPnrs)
            {
                if (aux.onlyEmd)
                {
                    TreeNode auxNodo = treeAutorizar.TopNode.Nodes.Add(aux.codSabre, aux.codSabre + ": $" + aux.getTotalEmds().ToString("######0.00").Replace(",", "."));
                    auxNodo.Nodes.Add(aux.codSabre + "-" + "fare", "EMD: $" + aux.getTotalEmds().ToString("######0.00").Replace(",", "."));
                    aAutorizar += aux.getTotalEmds();
                }
                else
                {
                    TreeNode auxNodo = treeAutorizar.TopNode.Nodes.Add(aux.codSabre, aux.codSabre + ": $" + aux.getTotalAmount().ToString("######0.00").Replace(",", "."));
                    auxNodo.Nodes.Add(aux.codSabre + "-" + "fare", "Fare: $" + (aux.getTotalAmount() - aux.getTotalEmds()).ToString("######0.00").Replace(",", "."));
                    if (aux.getTotalEmds() > 0)
                        auxNodo.Nodes.Add(aux.codSabre + "-" + "fare", "EMD: $" + aux.getTotalEmds().ToString("######0.00").Replace(",", "."));
                    aAutorizar += aux.getTotalAmount();
                }
            }
            treeAutorizar.TopNode.ExpandAll();
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            volver();
        }

        private void volver()
        {
            if (faltaAutorizar != 0)
            {
                DialogResult resultado = MessageBox.Show("Hay importes no asignados a una forma de pago," + Environment.NewLine + " quiere volver a Interact de todas formas?", "Diferencia", MessageBoxButtons.YesNo);
                this.Activate();
                if (resultado == DialogResult.Yes)
                {
                    PaymentFormWriter.grabarPago(transaccionesAGuardar);
                    Application.Exit();
                }
            }
            else
            {
                lblMensaje.Text = "Pasando información a Interact..";
                lblMensaje.Refresh();
                mostrarCartel();
                PaymentFormWriter.grabarPago(transaccionesAGuardar);
                Application.Exit();
            }
        }

        private void btnCreditoArg_Click(object sender, EventArgs e)
        {
            logger.Info("Se selecciono Tarjeta de Credito Argentina");
            credito(false);
        }

        private void credito(bool extranjero)
        {
            try
            {
                FrmImportes formImportes = new FrmImportes(TipoTarjeta.CREDITO, lsPnrs, faltaAutorizar);
                formImportes.ShowDialog();
                logger.Debug("Importe ingresado y formulario cerrado : Importe: " + formImportes.aAutorizar);
                if (formImportes.continuar)
                {
                    logger.Info("Comienzo de construccion de transaccion");
                    Transaccion tran = TransaccionBuilder.construirPago(formImportes.aAutorizar);

                    tran.listPnr = lsPnrs;

                    if (tran.isOnlyEmdTrx())
                        tran.tipoOperacion = TipoOperacion.COMPRA_SOLO_EMD;
                    else
                        tran.tipoOperacion = TipoOperacion.COMPRA_PASAJE;

                    logger.Debug("Fin de construccion de transaccion");

                    tran.eventHandler += this.handleEventTransaction;
                    logger.Debug("Comienzo de Get Autorizador");
                    Autorizator autorizador = AutorizatorFactory.getAutorizator(tran);
                    logger.Debug("Fin de Get Autorizador");
                    if (POIutils.isVtolNpsPosAutorizator())
                    {
                        mostrarCartel();
                        Tarjeta auxtar = null;
                        if (autorizador != null)
                        {
                            auxtar = autorizador.solicitarNumeroTarjeta("Sale", tran);
                        }
                        else
                        {
                            MessageBox.Show("Error al obtener interface de autorizacion", "Error en autorizador", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        if (auxtar == null)
                        {
                            pnlMensaje.Visible = false;
                            return;
                        }

                        auxtar.tipoTarjeta = TipoTarjeta.CREDITO;
                        if (extranjero)
                        {
                            auxtar.tipoTarjeta = TipoTarjeta.CREDITO_DEBITO;
                        }

                        lblMensaje.Text = "Cargando promociones, aguarde un instante...";
                        pnlMensaje.Refresh();
                        bool solo1cuota = false;
                        foreach (var item in lsPnrs)
                            foreach (var item2 in item.pasajeros)
                                foreach (var item3 in item2.emds)
                                    foreach (var item4 in Configuration.getInstance().emdsOnePay)
                                        if (item4 == item3.descripcion.Trim())
                                            solo1cuota = true;

                        // Si es Contact Center y selecciona extranjero, tenemos que preguntar
                        // si la tarjera es del pais del esta parado. Si es así, buscar las promo
                        // sino una sola cuota

                        if (extranjero && (Configuration.getInstance().tipoAuth.Equals(TipoAutorizador.VTOL_CALLCENTER) || Configuration.getInstance().tipoAuth.Equals(TipoAutorizador.NPS)))
                        {

                            DialogResult opcion;

                            using (new CenterWinDialog(this))
                            {
                                string pseudoCityCode = (tran == null) ? VBrequestReader.getPropiedad("pcc") : tran.pcc;
                                string country = POIutils.getCountryFromPCC(pseudoCityCode);
                                opcion = MessageBox.Show("La tarjeta es emitida en " + country + "? Sino no traera cuotas", "Selección de tarjeta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            }

                            if (opcion == DialogResult.Yes)
                                solo1cuota = false;
                            else
                                solo1cuota = true;

                        }
                        tran.tarjeta = auxtar;
                        Operacion operacion = new Operacion(TipoOperacion.COMPRA_PASAJE, lsPnrs);
                        FrmOtrosDatos formOtrosDatos = new FrmOtrosDatos(tran, formImportes.aAutorizar, extranjero, solo1cuota, operacion);
                        if (!formOtrosDatos.cargoOK)
                        {
                            autorizador.cancelarTransaccion();
                        }

                        formOtrosDatos.ShowDialog();
                        tran = formOtrosDatos.tran;
                        pnlMensaje.Visible = false;
                        lblMensaje.Text = "Revise la operación en el Pinpad";

                    }
                    else
                    {
                        lblMensaje.Text = "Pase la tarjeta, ingrese datos, seleccione cuotas" + Environment.NewLine + " y siga las indicaciones del PinPad";
                        mostrarCartel();
                        HasarOtherDataComunicator.sendExtranjero(extranjero ? Procedencia.EXTRANJERO : Procedencia.NACIONAL, false);
                        Transaccion aComprar = TransaccionBuilder.construirPago(formImportes.aAutorizar);
                        aComprar.listPnr = lsPnrs;
                        tran = autorizador.realizarTransaccion(aComprar);
                        pnlMensaje.Visible = false;
                        lblMensaje.Text = "Revise la operación en el Pinpad";
                    }

                    if (tran != null && tran.isAprobada())
                    {
                        if (treeAutorizado.Nodes.ContainsKey("noAutorizacion"))
                            treeAutorizado.TopNode.Remove();
                        TreeNode auxNodoTrans;
                        if (treeAutorizado.Nodes.ContainsKey("CREDITO"))
                        {
                            TreeNode auxCredito = treeAutorizado.Nodes.Find("CREDITO", false)[0];
                            auxNodoTrans = auxCredito.Nodes.Add("CREDITO-" + formImportes.aAutorizar + "-" + tran.tarjeta.codTarjeta, tran.tarjeta.primeros6() + "xxxxxx" + tran.tarjeta.ultimos4() + ": $ " + formImportes.aAutorizar.ToString("######0.00").Replace(",", "."));
                        }
                        else
                        {
                            TreeNode auxCredito = treeAutorizado.Nodes.Add("CREDITO", "Tarjetas de crédito");
                            auxCredito.HideCheckBox();
                            auxNodoTrans = auxCredito.Nodes.Add("CREDITO-" + formImportes.aAutorizar + "-" + tran.tarjeta.codTarjeta, tran.tarjeta.primeros6() + "xxxxxx" + tran.tarjeta.ultimos4() + ": $ " + formImportes.aAutorizar.ToString("######0.00").Replace(",", "."));
                        }
                        auxNodoTrans.Tag = tran;
                        transaccionesAGuardar.Add(tran);
                        faltaAutorizar -= formImportes.aAutorizar;
                        txtFaltaAutorizar.Text = faltaAutorizar.ToString("######0.00").Replace(",", ".");
                    }
                }
            }
            catch (ValtchException e)
            {
                pnlMensaje.Visible = false;
                MessageBox.Show(e.getMsg(), "Error cargando promociones", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.logger.Error("Error cargando promociones: ", e);
            }
            catch (Exception e)
            {
                pnlMensaje.Visible = false;
            }

        }

        private void btnCreditoExt_Click(object sender, EventArgs e)
        {
            logger.Info("Se selecciono Tarjeta de Extranjeras");
            credito(true);
        }

        private void btnDebito_Click(object sender, EventArgs e)
        {
            logger.Info("Se selecciono Tarjeta de Debito");
            debito();
        }

        private void debito()
        {

            FrmImportes formImportes = new FrmImportes(TipoTarjeta.DEBITO, lsPnrs, faltaAutorizar);
            formImportes.ShowDialog();
            if (formImportes.continuar)
            {
                Transaccion tran = null;
                
                if (POIutils.isVtolNpsPosAutorizator() )
                {
                    mostrarCartel();
                    Transaccion resultado = null;
                    Transaccion auxTran = null;
                    if (Configuration.getInstance().tipoAuth == TipoAutorizador.POS_INGENICO)
                    {
                        auxTran = TransaccionBuilder.construirPago(formImportes.tarjSeleccionada, 1, formImportes.aAutorizar, TipoModoTransaccion.ONLINE);
                    }
                        
                    else { 
                        
                        auxTran = TransaccionBuilder.construirPago(formImportes.aAutorizar);
                        auxTran.tarjeta.codSabre = formImportes.tarjSeleccionada.codTarjetaSabre;
                        
                    }
                    auxTran.listPnr = lsPnrs;

                    Autorizator autorizador = AutorizatorFactory.getAutorizator(auxTran);

                    auxTran.eventHandler += this.handleEventTransaction;

                    if (autorizador != null)
                    {
                        if (Configuration.getInstance().tipoAuth == TipoAutorizador.VTOL || Configuration.getInstance().tipoAuth == TipoAutorizador.NPS)
                        {
                            
                            auxTran.tarjeta = autorizador.solicitarNumeroTarjeta("Sale", auxTran);

                            auxTran.cantCuotas = 1;
                            auxTran.modo = TipoModoTransaccion.ONLINE;
                            
                            if (auxTran.tarjeta != null)
                            {
                                auxTran.tarjeta.tipoTarjeta = TipoTarjeta.DEBITO;


                                bool esExtranjero = false;

                                DialogResult opcion;

                                using (new CenterWinDialog(this))
                                {
                                    opcion = MessageBox.Show("La tarjeta es extranjera?", "Selección de tarjeta extranjera o nacional", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                }

                                if (opcion == DialogResult.Yes)
                                    esExtranjero = true;
                                else
                                    esExtranjero = false;


                                FrmOtrosDatos formOtrosDatos = new FrmOtrosDatos(auxTran, esExtranjero);
                                formOtrosDatos.setBotonesDebito();
                                formOtrosDatos.ShowDialog();
                                if ( formOtrosDatos.continuar) { 
                                    formOtrosDatos.tran.tarjeta.debitoSeleccionada = formImportes.tarjSeleccionada.descripcionTarjeta;
                                    formOtrosDatos.tran.tarjeta.codSabre = formImportes.tarjSeleccionada.codTarjetaSabre;
                                    resultado = autorizador.realizarTransaccion(formOtrosDatos.tran);
                                }
                            }
                        }
                        else
                            resultado = autorizador.realizarTransaccion(auxTran);
                    }
                    else
                        MessageBox.Show("Error al obtener interface de autorizacion", "Error en autorizador", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (resultado == null)
                    {
                        pnlMensaje.Visible = false;
                        return;
                    }
                    resultado.tarjeta.tipoTarjeta = TipoTarjeta.DEBITO;
                    resultado.tarjeta.codSabre = formImportes.tarjSeleccionada.codTarjetaSabre;
                    //resultado.tarjeta.descripcion = formImportes.tarjSeleccionada.descripcionTarjeta;

                    if (Configuration.getInstance().tipoAuth == TipoAutorizador.POS_INGENICO)
                        resultado.tarjeta.codTarjeta = formImportes.tarjSeleccionada.codNumTarjeta;

                    tran = resultado;
                }
                else
                {
                    lblMensaje.Text = "Pase la tarjeta, ingrese datos, seleccione cuotas" + Environment.NewLine + " y siga las indicaciones del PinPad";
                    mostrarCartel();
                    HasarOtherDataComunicator.sendExtranjero(Procedencia.PREGUNTAR, true);
                    Transaccion aComprar = TransaccionBuilder.construirPago(formImportes.aAutorizar);
                    Autorizator autorizador = AutorizatorFactory.getAutorizator(aComprar);
                    aComprar.listPnr = lsPnrs;
                    tran = autorizador.realizarTransaccion(aComprar);
                    lblMensaje.Text = "Revise la operación en el Pinpad";
                }
                pnlMensaje.Visible = false;
                if (tran != null)
                {
                    if (treeAutorizado.Nodes.ContainsKey("noAutorizacion"))
                        treeAutorizado.TopNode.Remove();
                    TreeNode auxNodoTrans;
                    if (treeAutorizado.Nodes.ContainsKey("DEBITO"))
                    {
                        TreeNode auxCredito = treeAutorizado.Nodes.Find("DEBITO", false)[0];
                        auxNodoTrans = auxCredito.Nodes.Add("DEBITO-" + formImportes.aAutorizar + "-" + tran.tarjeta.codTarjeta, tran.tarjeta.primeros6() + "xxxxxx" + tran.tarjeta.ultimos4() + ": $ " + formImportes.aAutorizar.ToString("######0.00").Replace(",", "."));
                    }
                    else
                    {
                        TreeNode auxDebito = treeAutorizado.Nodes.Add("DEBITO", "Tarjetas de débito");
                        auxDebito.HideCheckBox();
                        auxNodoTrans = auxDebito.Nodes.Add("DEBITO-" + formImportes.aAutorizar + "-" + tran.tarjeta.codTarjeta, tran.tarjeta.primeros6() + "xxxxxx" + tran.tarjeta.ultimos4() + ": $ " + formImportes.aAutorizar.ToString("######0.00").Replace(",", "."));
                    }
                    auxNodoTrans.Tag = tran;
                    transaccionesAGuardar.Add(tran);
                    faltaAutorizar -= formImportes.aAutorizar;
                    txtFaltaAutorizar.Text = faltaAutorizar.ToString("######0.00").Replace(",", ".");
                }
            }
        }

        private void btnOtros_Click(object sender, EventArgs e)
        {
            otros();
        }

        private void otros()
        {
            FrmImportes formImportes = new FrmImportes(TipoTarjeta.EFECTIVO, this.lsPnrs, faltaAutorizar);
            formImportes.ShowDialog();
            if (formImportes.continuar)
            {
                if (treeAutorizado.Nodes.ContainsKey("noAutorizacion"))
                    treeAutorizado.TopNode.Remove();
                TreeNode auxNodoTrans;
                if (treeAutorizado.Nodes.ContainsKey("OTROS"))
                {
                    TreeNode auxOtros = treeAutorizado.Nodes.Find("OTROS", false)[0];
                    auxNodoTrans = auxOtros.Nodes.Add("OTROS-" + formImportes.aAutorizar, "Otros............: $" + formImportes.aAutorizar.ToString("######0.00").Replace(",", "."));
                }
                else
                {
                    TreeNode auxOtros = treeAutorizado.Nodes.Add("OTROS", "Otras formas de pago");
                    auxOtros.HideCheckBox();
                    auxNodoTrans = auxOtros.Nodes.Add("OTROS-" + formImportes.aAutorizar, "Otros............: $" + formImportes.aAutorizar.ToString("######0.00").Replace(",", "."));
                }
                Transaccion auxTran = TransaccionBuilder.construirPago("", 1, formImportes.aAutorizar, "", TipoModoTransaccion.ONLINE);
                auxTran.tarjeta.tipoTarjeta = TipoTarjeta.EFECTIVO;
                auxNodoTrans.Tag = auxTran;
                transaccionesAGuardar.Add(auxTran);
                faltaAutorizar -= formImportes.aAutorizar;
                txtFaltaAutorizar.Text = faltaAutorizar.ToString("######0.00").Replace(",", ".");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            eliminar();
        }

        private void eliminar()
        {
            
            TreeNode auxNodo = buscarNodoChequeado();
            Transaccion remover = transaccionesAGuardar.Find(x => x == auxNodo.Tag);
            // Reinicio el modo a 1 para que sea siempre ONLINE
            remover.modo = TipoModoTransaccion.ONLINE;
            Autorizator auth = AutorizatorFactory.getAutorizator( remover );
            Transaccion aEliminar = TransaccionBuilder.construirAnulacion(remover.numTicket, remover.trxReferenceId, remover.fecha, remover.tarjeta);
            aEliminar.modo = TipoModoTransaccion.ONLINE;
            aEliminar.eventHandler += this.handleEventTransaction;
            if ( POIutils.isVtolNpsAutorizator() )
            {
                aEliminar.importeTotal = remover.importeTotal;
                aEliminar.cantCuotas = remover.cantCuotas;
                aEliminar.trxId = remover.trxId;
                aEliminar.numTicket = remover.numTicket;
                //aEliminar.trxReferenceId = remover.trxReferenceId;
            }
            Transaccion resultado = null;
            switch (auxNodo.Name.Split('-')[0])
            {
                case "DEBITO":
                case "CREDITO":
                    if (Configuration.getInstance().tipoAuth == TipoAutorizador.VTOL)
                        lblMensaje.Text = "Revise la operación en el Pinpad";
                    if (Configuration.getInstance().tipoAuth == TipoAutorizador.NPS)
                        lblMensaje.Text = "Comunicandose con NPS...";
                    mostrarCartel();
                    if (auth != null)
                    {
                        aEliminar.tipoTrans = TipoTransaccion.ANULACION;
                        aEliminar.tipoOperacion = TipoOperacion.ANULACION;
                        resultado = auth.realizarTransaccion(aEliminar);
                    }
                        
                    else
                        MessageBox.Show("Error al obtener interface de autorizacion", "Error en autorizador", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    pnlMensaje.Visible = false;
                    lblMensaje.Text = "Revise la operación en el Pinpad";
                    break;
                case "OTROS":
                    resultado = remover;
                    break;
                default:
                    resultado = null;
                    break;
            }
            if ((resultado != null && resultado.isAprobada()) || (auxNodo.Name.Split('-')[0] == "OTROS"))
            {
                faltaAutorizar += remover.importeSinIntereses();
                txtFaltaAutorizar.Text = faltaAutorizar.ToString("######0.00").Replace(",", ".");
                transaccionesAGuardar.Remove(remover);
                if (auxNodo.Parent.Nodes.Count > 1)
                    treeAutorizado.Nodes.Remove(auxNodo);
                else
                {
                    treeAutorizado.Nodes.Remove(auxNodo.Parent);
                }
                if (treeAutorizado.Nodes.Count == 0)
                    treeAutorizado.Nodes.Add("noAutorizacion", "No se registran autorizaciones").HideCheckBox();
                deshabilitarEliminar();
            }
            else
            {
                MessageBox.Show("No se pudo eliminar la autorizacion, intentelo nuevamente", "Error al eliminar autorizacion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Activate();
            }
        }

        private TreeNode buscarNodoChequeado()
        {
            foreach (TreeNode item in treeAutorizado.Nodes)
            {
                foreach (TreeNode item2 in item.Nodes)
                {
                    if (item2.Checked)
                        return item2;
                }
            }
            return new TreeNode();
        }

        private void treeAutorizado_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Action != TreeViewAction.Unknown)
            {
                if (e.Node.Level != 1)
                {
                    e.Node.Checked = false;
                    MessageBox.Show("No se puede eliminar este nodo", "Error al eliminar autorizacion", MessageBoxButtons.OK);
                    this.Activate();
                }
                else
                {
                    if (e.Node.Checked)
                        habilitarEliminar();
                    else
                        deshabilitarEliminar();
                }
            }
        }

        private void deshabilitarEliminar()
        {
            btnEliminar.Enabled = false;
            btnCreditoArg.Enabled = true;
            btnCreditoExt.Enabled = true;
            btnDebito.Enabled = true;
            btnOtros.Enabled = true;
            btnVolver.Enabled = true;
        }

        private void habilitarEliminar()
        {
            btnEliminar.Enabled = true;
            btnCreditoArg.Enabled = false;
            btnCreditoExt.Enabled = false;
            btnDebito.Enabled = false;
            btnOtros.Enabled = false;
            btnVolver.Enabled = false;
        }

        private void mostrarCartel()
        {
            pnlMensaje.Visible = true;
            pnlMensaje.Refresh();
        }

        private void treeAutorizado_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void lblMensaje_Click(object sender, EventArgs e)
        {

        }

        private void grpAcciones_Enter(object sender, EventArgs e)
        {

        }

        private void btnOtras_Click(object sender, EventArgs e)
        {
            FrmOtrasOperaciones formOtros = new FrmOtrasOperaciones();
            formOtros.ShowDialog();
        }
    }
}
