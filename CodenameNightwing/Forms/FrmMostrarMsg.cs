using CodenameNightwing.BusinessLogic;
using CodenameNightwing.FileManager;
using CodenameNightwing.Valtech.PromoService;
using CodenameNightwing.Valtech.PromoService.Request;
using CodenameNightwing.Valtech.PromoService.Response;
using CodenameNightwing.Valtech.VoidService;
using CodenameNightwing.Valtech.VoidService.Request;
using CodenameNightwing.Valtech.VoidService.Response;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodenameNightwing.Forms
{
    public partial class FrmMostrarMsg : Form
    {
        public string msg { get; set; }
        public FrmMostrarMsg(string msg)
        {
            InitializeComponent();
            Icon = Properties.Resources.logo_con_borde_grueso;
            this.msg = msg;
            this.Text = msg;
        }

        private void FrmMostrarMsg_Load(object sender, EventArgs e)
        {
            lblMensaje.Text = msg;
            mostrarCartel();
        }


        private void mostrarCartel()
        {
            pnlMensaje.Visible = true;
            pnlMensaje.Refresh();
        }

        private void lblMensaje_Click(object sender, EventArgs e)
        {

        }

        public void emitirEMD()
        {
            EMDServiceRequest emdRequest = VBrequestReader.leerIntereses();
            if (emdRequest != null)
            {
                EMDServiceResponse emdResponse;

                bool flag = true;

                while (flag)
                {
                    EMDServiceController emdController = new EMDServiceController();
                    emdResponse = emdController.addEMD(emdRequest);

                    if (emdResponse.tickets != null && emdResponse.tickets.Count > 0)
                    {
                        string tickets = String.Join(", ", emdResponse.tickets.ToArray());
                        string msg = "EMD emitido correctamente.\nPNR: " + emdResponse.pnr + " , emd:" + tickets;
                        flag = false;
                        MessageBox.Show(msg, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {

                        string msg = "No se pudo emitir el importe "  + emdRequest.optionalFields.emdAmount + " correspondiente a EMD de intereses. \n Descripcion: " + emdResponse.errorResponseMsg + "\n ¿ Desea reintentar ? ";
                        DialogResult Result = MessageBox.Show(msg, "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                        if (Result == DialogResult.Yes)
                        {
                            flag = true;
                        }
                        else
                        {
                            MessageBox.Show("Debe emitir el EMD de intereses de forma manual", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            flag = false;
                        }
                    }
                }
            }

        }

        public void voidEmision()
        {
            VoidServiceRequest voidServiceRequest = VBrequestReader.leerVoidTickets();
            if (voidServiceRequest != null)
            {
                VoidServiceResponse voidResponse;
                bool flag = true;

                while (flag)
                {
                    VoidServiceController voidServiceController = new VoidServiceController();
                    voidResponse = voidServiceController.voidEmision(voidServiceRequest);

                    if (voidResponse.success)
                    {
                        string tkts = "";
                        foreach (Vcr tkt in voidResponse.vcr) {
                            tkts = tkts + "tktNro: " + tkt.vcr + "tktStatus: " + tkt.success;
                        }
                        string msg = "VCR voideado correctamente.\nPNR: " + tkts;
                        flag = false;
                        MessageBox.Show(msg, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        string msg = "No se pudo voidear los tickets \n Descripcion: " + voidResponse.errorResponseMsg + "\n ¿ Desea reintentar ? ";
                        DialogResult Result = MessageBox.Show(msg, "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                        if (Result == DialogResult.Yes)
                            flag = true;
                        else
                            flag = false;
                    }

                    //Escribir paymentForms.properties con respuesta Error
                    PaymentFormWriter.grabarVoidWS(voidResponse.vcr);
                }


            }
        }
    }
}



