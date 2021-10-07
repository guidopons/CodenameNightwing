using CodenameNightwing.Autorization.POS.StructsComunicacionPOS;
using System;
using System.Runtime.InteropServices;

namespace CodenameNightwing.Autorization
{
    class NativeMethods
    {
        [DllImport("VpiPc.dll", EntryPoint = "vpiOpenPort", CharSet = CharSet.Unicode)]
        public static extern short vpiOpenPort(ref vpiComParams_t aux);

        [DllImport("VpiPc.dll", EntryPoint = "vpiClosePort", CharSet = CharSet.Unicode)]
        public static extern short vpiClosePort();

        [DllImport("VpiPc.dll", EntryPoint = "vpiTestConnection", CharSet = CharSet.Unicode)]
        public static extern short vpiTestConnection();

        [DllImport("VpiPc.dll", EntryPoint = "vpiPrintTicket", CharSet = CharSet.Unicode)]
        public static extern short vpiPrintTicket();

        [DllImport("VpiPc.dll", EntryPoint = "vpiPrintBatchClose", CharSet = CharSet.Unicode)]
        public static extern short vpiPrintBatchClose();

        [DllImport("VpiPc.dll", EntryPoint = "vpiBatchClose", CharSet = CharSet.Unicode)]
        public static extern short vpiBatchClose(ref vpiBatchCloseOut_t paramOut, ref int timeout);

        [DllImport("VpiPc.dll", EntryPoint = "vpiPurchase", CharSet = CharSet.Unicode)]
        public static extern short vpiPurchase(ref vpiPurchaseIn_t paramIn, ref vpiTrxOut_t paramOut, ref int timeout);

        [DllImport("VpiPc.dll", EntryPoint = "vpiVoid", CharSet = CharSet.Unicode)]
        public static extern short vpiVoid(ref vpiVoidIn_t paramIn, ref vpiTrxOut_t paramOut, ref int timeout);

        [DllImport("VpiPc.dll", EntryPoint = "vpiRefund", CharSet = CharSet.Unicode)]
        public static extern short vpiRefund(ref vpiRefundIn_t paramIn, ref vpiTrxOut_t paramOut, ref int timeout);

        [DllImport("VpiPc.dll", EntryPoint = "vpiRefundVoid", CharSet = CharSet.Unicode)]
        public static extern short vpiRefundVoid(ref vpiVoidIn_t paramIn, ref vpiTrxOut_t paramOut, ref int timeout);

        [DllImport("VpiPc.dll", EntryPoint = "vpiGetLastTrxData", CharSet = CharSet.Unicode)]
        public static extern short vpiGetLastTrxData(ref short trxCode, ref vpiTrxOut_t paramOut);

        [DllImport("VpiPc.dll", EntryPoint = "vpiGetBatchCloseData", CharSet = CharSet.Unicode)]
        public static extern short vpiGetBatchCloseData(ref short recIndex, ref vpiBatchCloseDataOut_t paramOut);

        [DllImport("VpiPc.dll", EntryPoint = "vpiGetIssuer", CharSet = CharSet.Unicode)]
        public static extern short vpiGetIssuer(ref short recIndex, ref vpiIssuerOut_t paramOut);

        [DllImport("VpiPc.dll", EntryPoint = "vpiGetPlan", CharSet = CharSet.Unicode)]
        public static extern short vpiGetPlan(ref short recIndex, ref vpiPlanOut_t paramOut);

        /* para esconder checkboxes */
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam,
                                                 ref TVITEM lParam);

        [StructLayout(LayoutKind.Sequential, Pack = 8, CharSet = CharSet.Auto)]
        public struct TVITEM
        {
            public int mask;
            public IntPtr hItem;
            public int state;
            public int stateMask;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string lpszText;
            public int cchTextMax;
            public int iImage;
            public int iSelectedImage;
            public int cChildren;
            public IntPtr lParam;
        }

        public const int SPI_SETKEYBOARDCUES = 4107; //100B
        [DllImport("user32.dll", EntryPoint = "SystemParametersInfo")]
        public static extern int SystemParametersInfo(int uAction, int uParam, int lpvParam, int fuWinIni);


        [DllImport(@"IntegradorUI.dll", EntryPoint = "VisuDisp", CharSet = CharSet.Ansi)]
        //Declaramos el prototipo con los parametros que solicita "compraNet"
        public static extern void VisuDisp(string terminal, string linea1, string linea2, int timeOut);

        /// pruebas dll IntegradorUI
        [DllImport("IntegradorUI.dll", EntryPoint = "VisuError", CharSet = CharSet.Ansi)]
        public static extern void VisuError(string terminal, string linea1, string linea2, int timeOut);
        [DllImport("IntegradorUI.dll", EntryPoint = "CaptDato", CharSet = CharSet.Ansi)]
        public static extern int CaptDato(string terminal, string linea1, ref string buffer, int tamano, bool enmascara, int timeOut);
        [DllImport("IntegradorUI.dll", EntryPoint = "ReadCardReader", CharSet = CharSet.Ansi)]
        public static extern int ReadCardReader(string terminal, string trackI, ref int largoTrackI, string trackII, ref int largoTrackII, string promociones, int timeOut = 0);
        [DllImport("IntegradorUI.dll", EntryPoint = "StartProcess", CharSet = CharSet.Ansi)]
        public static extern void StartProcess(string terminal, string linea1);
        [DllImport("IntegradorUI.dll", EntryPoint = "EndProcess", CharSet = CharSet.Ansi)]
        public static extern void EndProcess(string terminal);
        [DllImport("IntegradorUI.dll", EntryPoint = "setPosVirtual", CharSet = CharSet.Ansi)]
        public static extern void setPosVirtual();

        //pruebas dll APICliente
        [DllImport("APIClienteNet.dll", EntryPoint = "aplicarOfertasNet", CharSet = CharSet.Ansi)]
        public static extern int aplicarOfertasNet(ref string datosMedioDePago, ref string datosParaPagos);

        // INTEGRACION CON HASAR
        [DllImport(@"Integrador.dll", EntryPoint = "compraEmvNet", CharSet = CharSet.Ansi)]
        public static extern int compraEmvNet(ref string terminal, ref string parametros, ref string respuesta);

        [DllImport(@"Integrador.dll", EntryPoint = "anulacionEmvNet", CharSet = CharSet.Ansi)]
        public static extern int anulacionEmvNet(ref string terminal, ref string parametros, ref string respuesta);

        [DllImport(@"Integrador.dll", EntryPoint = "devolucionEmvNet", CharSet = CharSet.Ansi)]
        public static extern int devolucionEmvNet(ref string terminal, ref string parametros, ref string respuesta);

        [DllImport(@"Integrador.dll", EntryPoint = "cierreTerminalJson", CharSet = CharSet.Ansi)]
        public static extern int cierreTerminalJson(ref string terminal);

        [DllImport(@"Integrador.dll", EntryPoint = "actualizarConfiguracionJson", CharSet = CharSet.Ansi)]
        public static extern int actualizarConfiguracionJson(ref string terminal);
    }
}
