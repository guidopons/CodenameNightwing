using CodenameNightwing.Config;
using System.Windows.Forms;
using VtolClientLib;

namespace CodenameNightwing.Autorization.VTOL
{
    class HostCodesVTOL
    {
        public static string HOST_00_APROBADA
        {
            get { return "00"; }
        }
        public static string HOST_01_PEDIR_AUTORIZACION_TELEFONICA
        {
            get { return "01"; }
        }
        public static string HOST_02_PEDIR_AUTORIZACION
        {
            get { return "02"; }
        }
        public static string HOST_03_COMERCIO_INVALIDO
        {
            get { return "03"; }
        }
        public static string HOST_04_CAPTURAR_TARJETA
        {
            get { return "04"; }
        }
        public static string HOST_05_DENEGADA
        {
            get { return "05"; }
        }
        public static string HOST_07_RETENGA_Y_LLAME
        {
            get { return "07"; }
        }
        public static string HOST_11_APROBADA
        {
            get { return "11"; }
        }
        public static string HOST_12_TRANSACCION_INVALIDA
        {
            get { return "12"; }
        }
        public static string HOST_13_MONTO_INVALIDO
        {
            get { return "13"; }
        }
        public static string HOST_14_TARJETA_INVALIDA
        {
            get { return "14"; }
        }
        public static string HOST_25_NO_EXISTE_ORIGINAL
        {
            get { return "25"; }
        }
        public static string HOST_30_ERROR_EN_FORMATO
        {
            get { return "30"; }
        }
        public static string HOST_38_EXCEDE_INGRESO_DE_PIN
        {
            get { return "38"; }
        }
        public static string HOST_43_RETENER_TARJETA
        {
            get { return "43"; }
        }
        public static string HOST_45_NO_OPERA_EN_CUOTAS
        {
            get { return "45"; }
        }
        public static string HOST_46_TARJETA_NO_VIGENTE
        {
            get { return "46"; }
        }
        public static string HOST_47_PIN_REQUERIDO
        {
            get { return "47"; }
        }
        public static string HOST_48_EXCEDE_MAX_CUOTAS
        {
            get { return "48"; }
        }
        public static string HOST_49_ERROR_FECHA_VENCIMIENTO
        {
            get { return "49"; }
        }
        public static string HOST_50_ENTREGA_SUPERA_LIMITE
        {
            get { return "50"; }
        }
        public static string HOST_51_FONDOS_INSUFICIENTES
        {
            get { return "51"; }
        }
        public static string HOST_53_CUENTA_INEXISTENTE
        {
            get { return "53"; }
        }
        public static string HOST_54_TARJETA_VENCIDA
        {
            get { return "54"; }
        }
        public static string HOST_55_PIN_INCORRECTO
        {
            get { return "55"; }
        }
        public static string HOST_56_TARJETA_NO_HABILITADA
        {
            get { return "56"; }
        }
        public static string HOST_57_TRANSACCION_NO_PERMITIDA
        {
            get { return "57"; }
        }
        public static string HOST_58_SERVICIO_INVALIDO
        {
            get { return "58"; }
        }
        public static string HOST_61_EXCEDE_LIMITE
        {
            get { return "61"; }
        }
        public static string HOST_65_EXCEDE_LIMITE_TARJETA
        {
            get { return "65"; }
        }
        public static string HOST_76_LLAMAR_AL_EMISOR
        {
            get { return "76"; }
        }
        public static string HOST_77_ERROR_PLAN_CUOTAS
        {
            get { return "77"; }
        }
        public static string HOST_85_APROBADA
        {
            get { return "85"; }
        }
        public static string HOST_86_NO_ENVIA_FECHA_ORIGINAL
        {
            get { return "86"; }
        }
        public static string HOST_89_TERMINAL_INVALIDA
        {
            get { return "89"; }
        }
        public static string HOST_91_EMISOR_FUERA_LINEA
        {
            get { return "91"; }
        }
        public static string HOST_94_NRO_SECUENCIA_DUPLICADO
        {
            get { return "94"; }
        }
        public static string HOST_95_RETRANSMITIENDO
        {
            get { return "95"; }
        }
        public static string HOST_96_ERROR_EN_SISTEMA
        {
            get { return "96"; }
        }
        public static string HOST_98_NO_APROBADA
        {
            get { return "98"; }
        }
        public static string HOST_XX_ERROR_NO_CLASIFICADO
        {
            get { return "99"; }
        }

        public static bool checkError(string aChequear)
        {
            if (aChequear == HOST_00_APROBADA || aChequear == HOST_01_PEDIR_AUTORIZACION_TELEFONICA || aChequear == HOST_02_PEDIR_AUTORIZACION
                || aChequear == HOST_11_APROBADA || aChequear == HOST_47_PIN_REQUERIDO || aChequear == HOST_85_APROBADA)
                return true;
            else
            {
                handleHostError(aChequear);
                return false;
            }
        }

        public static bool checkAutorizacionTelefonica(string aChequear)
        {
            if (aChequear == HOST_01_PEDIR_AUTORIZACION_TELEFONICA || aChequear == HOST_02_PEDIR_AUTORIZACION || aChequear == HOST_47_PIN_REQUERIDO)
                return true;
            else
                return false;
        }

        public static void handleHostError(string error)
        {
            string sTipoError = "";
            string sError = "";
            string sNombreError = "";
            switch (error)
            {
                case "03":
                    sTipoError = "Operación denegada - código: ";
                    sError = ". Comercio inválido. Presione OK para finalizar";
                    sNombreError = "DENEGADA";
                    break;
                case "04":
                    sTipoError = "Operación denegada - código: ";
                    sError = ". Capturar tarjeta. Presione OK para finalizar";
                    sNombreError = "DENEGADA";
                    break;
                case "05":
                    sTipoError = "Operación denegada - código: ";
                    sError = ". Denegada. Presione OK para finalizar";
                    sNombreError = "DENEGADA";
                    break;
                case "07":
                    sTipoError = "Operación denegada - código: ";
                    sError = ". Retener tarjeta y llamar al Centro de Autorizaciones. Presione OK para finalizar";
                    sNombreError = "DENEGADA";
                    break;
                case "12":
                    sTipoError = "TRANSACCION INVALIDA - código: ";
                    sError = ". Transacción no reconocida en el sistema. Presione OK para finalizar";
                    sNombreError = "OPERACION CANCELADA";
                    break;
                case "13":
                    sTipoError = "MONTO INVALIDO - código: ";
                    sError = ". Error en el formato del campo importe. Presione OK para finalizar";
                    sNombreError = "OPERACION CANCELADA";
                    break;
                case "14":
                    sTipoError = "Operación denegada - código: ";
                    sError = ". Tarjeta no corresponde. Presione OK para finalizar";
                    sNombreError = "DENEGADA";
                    break;
                case "19":
                    sTipoError = "REINICIE OPERACION - código: ";
                    sError = ".";
                    sNombreError = "REINICIO OPERACION";
                    break;
                case "25":
                    sTipoError = "Operación denegada - código: ";
                    sError = ". No existe original o los parametros no coinciden. Presione OK para finalizar";
                    sNombreError = "DENEGADA";
                    break;
                case "30":
                    sTipoError = "ERROR EN FORMATO - código: ";
                    sError = ". Error en el formato del mensaje. Presione OK para finalizar";
                    sNombreError = "OPERACION CANCELADA";
                    break;
                case "38":
                    sTipoError = "Operación denegada - código: ";
                    sError = ". Excede cantidad de reintentos de PIN permitidos. Presione OK para finalizar";
                    sNombreError = "DENEGADA";
                    break;
                case "43":
                    sTipoError = "Operación denegada - código: ";
                    sError = ". Retener tarjeta. Presione OK para finalizar";
                    sNombreError = "DENEGADA";
                    break;
                case "45":
                    sTipoError = "Operación denegada - código: ";
                    sError = ". Tarjeta inhibida para operar en cuotas. Presione OK para finalizar";
                    sNombreError = "DENEGADA";
                    break;
                case "46":
                    sTipoError = "Operación denegada - código: ";
                    sError = ". La tarjeta no se encuentra vigente. Presione OK para finalizar";
                    sNombreError = "DENEGADA";
                    break;
                case "47":
                    sTipoError = "Operación denegada - código: ";
                    sError = ". La tarjeta requiere el ingreso de PIN. Presione OK para finalizar";
                    sNombreError = "DENEGADA";
                    break;
                case "48":
                    sTipoError = "Operación denegada - código: ";
                    sError = ". Excede cantidad máxima de cuotas permitidas. Presione OK para finalizar";
                    sNombreError = "DENEGADA";
                    break;
                case "50":
                    sTipoError = "Operación denegada - código: ";
                    sError = ". El monto ingresado está fuera de los límites permitidos. Presione OK para finalizar";
                    sNombreError = "DENEGADA";
                    break;
                case "51":
                    sTipoError = "Operación denegada - código: ";
                    sError = ". No posee fondos suficientes. Presione OK para finalizar";
                    sNombreError = "DENEGADA";
                    break;
                case "53":
                    sTipoError = "Operación denegada - código: ";
                    sError = ". No existe cuenta asociada. Presione OK para finalizar";
                    sNombreError = "DENEGADA";
                    break;
                case "54":
                    sTipoError = "Operación denegada - código: ";
                    sError = ". Tarjeta expirada. Presione OK para finalizar";
                    sNombreError = "DENEGADA";
                    break;
                case "55":
                    sTipoError = "Operación denegada - código: ";
                    sError = ". El código de identificación personal es incorrecto. Presione OK para finalizar";
                    sNombreError = "DENEGADA";
                    break;
                case "56":
                    sTipoError = "Operación denegada - código: ";
                    sError = ". Emisor no habilitado en el sistema. Presione OK para finalizar";
                    sNombreError = "DENEGADA";
                    break;
                case "61":
                    sTipoError = "Operación denegada - código: ";
                    sError = ". Excede límite remanente de la tarjeta. Presione OK para finalizar";
                    sNombreError = "DENEGADA";
                    break;
                case "65":
                    sTipoError = "Operación denegada - código: ";
                    sError = ". Excede límite remanente de la tarjeta. Presione OK para finalizar";
                    sNombreError = "DENEGADA";
                    break;
                case "77":
                    sTipoError = "Operación denegada - código: ";
                    sError = ". Cantidad de cuotas inválido para el plan seleccionado";
                    sNombreError = "DENEGADA";
                    break;
                case "89":
                    sTipoError = "Operación denegada - código: ";
                    sError = ". Número de terminal no habilitado por el emisor. Presione OK para finalizar";
                    sNombreError = "DENEGADA";
                    break;
                case "94":
                    sTipoError = "Operación denegada - código: ";
                    sError = ". Error en mensaje. Presione OK para finalizar";
                    sNombreError = "DENEGADA";
                    break;
                case "98":
                    sTipoError = "Operación denegada - código: ";
                    sError = ". Ver información suministrada en ISO 63. Presione OK para finalizar";
                    sNombreError = "DENEGADA";
                    break;
                case "49":
                    sTipoError = "ERROR EN FECHA DE VENCIMIENTO - código: ";
                    sError = ". Error en el formato de la fecha de expiración. Presione OK para finalizar";
                    sNombreError = "OPERACION CANCELADA";
                    break;
                case "57":
                    sTipoError = "TRANSACCION NO PERMITIDA - código: ";
                    sError = ". La transacción no esta permitida para dicha tarjeta. Presione OK para finalizar";
                    sNombreError = "OPERACION CANCELADA";
                    break;
                case "58":
                    sTipoError = "SERVICIO INVALIDO - código: ";
                    sError = ". Transacción no permitida para dicha terminal. Presione OK para finalizar";
                    sNombreError = "OPERACION CANCELADA";
                    break;

                case "28":
                    sTipoError = "REINICIE OPERACION - código: ";
                    sError = ". Servicio no disponible, espere unos segundos y reintente la operación";
                    sNombreError = "REINICIO OPERACION";
                    break;
                case "95":
                    sTipoError = "REINICIE OPERACION - código: ";
                    sError = ". Retransmitiendo Batch Upload";
                    sNombreError = "REINICIO OPERACION";
                    break;
                case "88":
                case "99":
                default:
                    string responseMsgString = "";
                    if ( Configuration.getInstance().tipoAuth == BusinessLogic.TipoAutorizador.VTOL)
                    {
                        VTOLIntegrator vi = VTOLIntegrator.Instance;
                        responseMsgString = vi.getVTOLNode().GetField(FieldId.ISOResponseMessageFieldId);
                    }else
                    {
                        VTOLIntegratorCallCenter vi = VTOLIntegratorCallCenter.Instance;
                        responseMsgString = vi.getVTOLNode().GetField(FieldId.ISOResponseMessageFieldId);
                    }
                    
                    sTipoError = "Error " + responseMsgString + " - código: ";
                    sError = ". Error general del sistema. Presione OK para finalizar";
                    sNombreError = "REINICIO OPERACION";
                    break;
            }
            MessageBox.Show(Application.OpenForms[Application.OpenForms.Count - 1], sTipoError + error + sError, sNombreError, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
