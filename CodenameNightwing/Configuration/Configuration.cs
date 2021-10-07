using CodenameNightwing.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CodenameNightwing.Config
{
    public class Configuration
    {
        /* CON SINGLETON */
        public readonly string schemaWsComercio = "<soapenv:Envelope xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/' xmlns:ws='http://ws.objects.binesws.interact.aerolineas.com.ar/'><soapenv:Header/><soapenv:Body><ws:getCodigosComercio><!--Optional:--><arg0>?</arg0><!--Optional:--><arg1>?</arg1><!--Optional:--><arg2>?</arg2><!--Optional:--><arg3>?</arg3></ws:getCodigosComercio></soapenv:Body></soapenv:Envelope>";
        public readonly string schemaWsBines = "<soapenv:Envelope xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/' xmlns:ws='http://ws.objects.binesws.interact.aerolineas.com.ar/'><soapenv:Header/><soapenv:Body><ws:getInteresesHoyConImporte><!--Optional:--><arg0><!--Optional:--><bin>?</bin></arg0><arg1></arg1></ws:getInteresesHoyConImporte></soapenv:Body></soapenv:Envelope>";
        public readonly string schemaWsBinesOperacion = "<soapenv:Envelope xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/' xmlns:ws='http://ws.objects.binesws.interact.aerolineas.com.ar/'><soapenv:Header/><soapenv:Body><ws:getInteresesHoyConImporteOperacion><!--Optional:--><arg0><!--Optional:--><bin>?</bin></arg0><arg1></arg1><arg2></arg2></ws:getInteresesHoyConImporteOperacion></soapenv:Body></soapenv:Envelope>";
        public readonly string schemaWsExcepcionBines = "<soapenv:Envelope xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/' xmlns:ws='http://ws.objects.binesws.interact.aerolineas.com.ar/'><soapenv:Header/><soapenv:Body><ws:getExceptionBin><arg0><bin>?</bin></arg0></ws:getExceptionBin></soapenv:Body></soapenv:Envelope>";
        public readonly string schemaWsObtenerCuit = "<soapenv:Envelope xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/' xmlns:ws='http://ws.objects.binesws.interact.aerolineas.com.ar/'><soapenv:Header/><soapenv:Body><ws:generateCuit><!--ACA en arg0 va M: Masculino, F: Femenino, E: Empresa--><arg0>?</arg0><!--DOCUMENTO, sin puntos ni guiones--><arg1>?</arg1></ws:generateCuit></soapenv:Body></soapenv:Envelope>";
        public readonly string schemaWsVerificarCuit = "<soapenv:Envelope xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/' xmlns:ws='http://ws.objects.binesws.interact.aerolineas.com.ar/'><soapenv:Header/><soapenv:Body><ws:isValid><!--ACA en arg0 va el prefijo del cuit/cuil--><arg0>?</arg0><!--DOCUMENTO, sin puntos ni guiones--><arg1>?</arg1><!--ACA en arg0 va el digito verificador del cuit/cuil--><arg2>?</arg2></ws:isValid></soapenv:Body></soapenv:Envelope>";
        public readonly string schemaWsTarjetas = "<soapenv:Envelope xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/' xmlns:ws='http://ws.objects.binesws.interact.aerolineas.com.ar/'><soapenv:Header/><soapenv:Body><ws:getCCcashier><arg0><!--Optional:--></arg0></ws:getCCcashier></soapenv:Body></soapenv:Envelope>";
        public readonly string schemaWsPaises = "<soap:Envelope xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema' xmlns:soap='http://schemas.xmlsoap.org/soap/envelope/' xmlns:ws='http://ws.objects.binesws.interact.aerolineas.com.ar/'><soap:Body><ws:getCountryList></ws:getCountryList></soap:Body></soap:Envelope>";
        public readonly string schemaWsInsertTransaction = "<soapenv:Envelope xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/' xmlns:ws='http://ws.objects.binesws.interact.aerolineas.com.ar/'><soapenv:Header/><soapenv:Body><ws:insertTransaction><arg0></arg0><arg1></arg1><arg2></arg2><arg3></arg3><arg4></arg4><arg5></arg5><arg6></arg6><arg7></arg7><arg8></arg8><arg9></arg9><arg10></arg10><arg11></arg11><arg12></arg12><arg13></arg13><arg14></arg14><arg15></arg15><arg16></arg16><arg17></arg17><arg18></arg18><arg19></arg19><arg20></arg20><arg21></arg21><arg22></arg22><arg23></arg23><arg24></arg24><arg25></arg25></ws:insertTransaction></soapenv:Body></soapenv:Envelope>";
        public readonly string schemaWsSetDatosConfirmado = "<soapenv:Envelope xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/' xmlns:ws='http://ws.objects.binesws.interact.aerolineas.com.ar/'><soapenv:Header/><soapenv:Body><ws:setDatosConfirmado><arg0></arg0><arg1></arg1><arg2></arg2><arg3></arg3><arg4></arg4><arg5></arg5><arg6></arg6><arg7></arg7><arg8></arg8><arg9></arg9><arg10></arg10><arg11></arg11><arg12></arg12><arg13></arg13><arg14></arg14><arg15></arg15><arg16></arg16><arg17></arg17><arg18></arg18><arg19></arg19><arg20></arg20><arg21></arg21><arg22></arg22><arg23></arg23><arg24></arg24><arg25></arg25></ws:setDatosConfirmado></soapenv:Body></soapenv:Envelope>";
        public readonly string schemaWsGetTransaction = "<soapenv:Envelope xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/' xmlns:ws='http://ws.objects.binesws.interact.aerolineas.com.ar/'><soapenv:Header/><soapenv:Body><ws:getTransaction><arg0></arg0></ws:getTransaction></soapenv:Body></soapenv:Envelope>";
        public readonly string schemaWsSetAnulado = "<soapenv:Envelope xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/' xmlns:ws='http://ws.objects.binesws.interact.aerolineas.com.ar/'><soapenv:Header/><soapenv:Body><ws:setAnulado><arg0></arg0></ws:setAnulado></soapenv:Body></soapenv:Envelope>";
        public readonly string schemaWsSetReversado = "<soapenv:Envelope xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/' xmlns:ws='http://ws.objects.binesws.interact.aerolineas.com.ar/'><soapenv:Header/><soapenv:Body><ws:setReversado><arg0></arg0></ws:setReversado></soapenv:Body></soapenv:Envelope>";
        public readonly string schemaWsSearchTransaction = "<soapenv:Envelope xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/' xmlns:ws='http://ws.objects.binesws.interact.aerolineas.com.ar/'><soapenv:Header/><soapenv:Body><ws:searchTransaction><arg0></arg0><arg1></arg1><arg2></arg2><arg3></arg3><arg4></arg4></ws:searchTransaction></soapenv:Body></soapenv:Envelope>";
        public readonly string schemaWsinsertTerminal = "<soapenv:Envelope xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/' xmlns:ws='http://ws.objects.binesws.interact.aerolineas.com.ar/'><soapenv:Header/><soapenv:Body><ws:insertTerminal><arg0><base></base><caja></caja><pinpadId></pinpadId><printerId></printerId><sucursal></sucursal><macAddress></macAddress><machineName></machineName></arg0></ws:insertTerminal></soapenv:Body></soapenv:Envelope>";
        public readonly string schemaWsVoucher = "<soapenv:Envelope xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/' xmlns:ws='http://ws.objects.binesws.interact.aerolineas.com.ar/'><soapenv:Header/><soapenv:Body><ws:getVoucherDescription><arg0></arg0></ws:getVoucherDescription></soapenv:Body></soapenv:Envelope>";
        public readonly string schemaWsGetConfiguration = "<soapenv:Envelope xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/' xmlns:ws='http://ws.objects.binesws.interact.aerolineas.com.ar/'><soapenv:Header/><soapenv:Body><ws:getConfigurationValue><arg0></arg0></ws:getConfigurationValue></soapenv:Body></soapenv:Envelope>";

        //Path por defecto de files definicion WS SOAP SABRE
        public readonly string schemaWsSabreCreateSession = Application.StartupPath + "\\XMLSabreRQ\\" + "SessionCreateRQ.xml";
        public readonly string schemaWsSabreCommand = Application.StartupPath + "\\XMLSabreRQ\\" + "SabreCommandLLSRQ.xml";
        public readonly string schemaWsAddRemarkLLSRQ = Application.StartupPath + "\\XMLSabreRQ\\" + "AddRemarkLLSRQ.xml";
        public readonly string schemaWsEndTransaction = Application.StartupPath + "\\XMLSabreRQ\\" + "EndTransactionLLSRQ.xml";
        public readonly string schemaWsSabreGetReservation = Application.StartupPath + "\\XMLSabreRQ\\" + "GetReservationRQ.xml";
        public readonly string schemaWsSabreCloseSession = Application.StartupPath + "\\XMLSabreRQ\\" + "SessionCloseRQ.xml";
        
        //Path por defecto de files aerolineas.properties
        public readonly string aerolineasPropertiesPath = Application.StartupPath.Replace("Visual_Basic", "") + "aerolineas.properties";

        //****************
        //Files and paths
        //****************

        //Files Path
        public string poiFilesPath { get; set; }

        public string vtolPosClientLibFolder { get; set; }

        //FilesNames
        public string vbRequestFile { get; set; }
        public string tarjetasFile { get; set; }
        public string paisesFile { get; set; }
        public string paymentFile { get; set; }
        public string otherDataFile { get; set; }
        public string anulationFile { get; set; }

        
        //****************

        //****************
        //Encript key paymentFile
        //****************
        public string secretKey { get; set; }
        //****************

        public string ftpLog { get; set; }

        public string smtpPort { get; set; }

        public string smtpHost { get; set; }


        //****************
        //QA o PROD
        //****************
        public string environment { get; set; }

        //****************
        //VTOL Service
        //****************
        public bool isVtolServiceEnabled { get; set; }
        public string serviceVtolName { get; set; }
        public string timeOutService { get; set; }
        public string timeOutLibraryResponse { get; set; }
        public string timeOutConnectionHost { get; set; }
        //****************

        //****************
        //PINPAD
        //****************
        public string tipoDriverPinPad { get; set; }

        public string idPinpad { get; set; }
        /// COM o USB
        public string posCommunicationType { get; set; }

        /// Solo tiene sentido para la comunicacion por COM
        public string posComPort { get; set; }
        //****************

        //****************
        //Autorizadores
        //****************
        public TipoAutorizador tipoAuth { get; set; }
        public TipoAutorizador tipoAuthConf { get; set; }

        //NPS
        public bool isNPSForzed { get; set; }
        public string npsMerchantId { get; set; }
        public string npsMerchIdNat { get; set; }
        public string timeOutPaymentForm { get; set; }
        public string confirmationPaymentPage { get; set; } = null;
        //****************

        //****************
        //Valtech
        //****************
        public string baseUrlValtech { get; set; }
        public string auth0clientId { get; set; }
        public string auth0SecretKey { get; set; }
        public int valtechServicesTimeout { get; set; }
        public bool usePromoValtech { get; set; }

        public bool isFraudAvail { get; set; }
        //Importe Minimo Fraude
        public string importeMinCS { get; set; } = null;


        public bool isPromoAvail { get; set; }
        //****************

        //****************
        //Parametros IVR
        //****************
        public bool checkActualCall { get; set; }
        public string tiempoLocalHost { get; set; }

        public bool isIvrAvail { get; set; }
        public string callStatus { get; set; } = "Answered";
        public string mockMitrol { get; set; }
        public string tiempoDerivacion { get; set; }
        public string timeOutIVRPagos { get; set; }
        public string backofficeWS { get; set; } = null;
        //****************


        //****************
        //SABRE Sines
        //****************
        public string sineSabreWs { get; set; }
        public string robotSineToPrint { get; set; }
        //****************


        //****************
        //WS Sevices AR
        //****************
        public string serverBines { get; set; }
        public string serverCodComercio { get; set; }
        public string serverCuitCuil { get; set; }
        public string serverTarjetas { get; set; }
        public string serverPaises { get; set; }
        public string serverVoucher { get; set; }
        public string serverUser { get; set; }
        public string serverTerminales { get; set; }
        public string serverTransacciones { get; set; }
        public string dirServDev { get; set; }
        public string dirServProd { get; set; }
        public string dirServQa { get; set; }
        public bool isProxyEnabled { get; set; }
        public string proxyAddress { get; set; }
        //****************


        //****************
        //Printer
        //****************
        public TipoPrinter tipoPrinter { get; set; }

        public string nombreImpresora { get; set; }
        public string modeloImpresora { get; set; }
        public string idPrinter { get; set; }
        public string timeSleepPrinter { get; set; }
        public string printCupons { get; set; }
        public string timeOutEpsonPrinter { get; set; }
        //****************

        //Boton ingreso datos TC Manual
        public bool btnIngresoTCManualEnable { get; set; }
        public string confRegArg { get; set; }
        public string sidUsuarios { get; set; }
        public string codAuthBloqueados { get; set; }
        public bool printPaxName { get; set; }


        // Viaja en los request
        public string interactUser { get; set; }
        public string ciudadStar { get; set; }
        public string cashier { get; set; }
        public string idPos { get; set; }
        public string agentDesc { get; set; }
        public string caja { get; set; }
        public string sucursal { get; set; }
        public string puesto { get; set; }
        public string empresaHAS { get; set; }

        public string emdPrintEndoPattern { get; set; } = null;

        public string codAmexMM { get; set; }
        public List<string> emdsOnePay { get; set; } = new List<string>();

        public List<string> printerList { get; set; } = new List<string>();

        private Configuration()
        {
        }

        private static readonly Configuration instancia = new Configuration();


        public static Configuration getInstance()
        {
            if (instancia == null)
                return new Configuration();
            else
                return instancia;
        }


        public Terminal getTerminal()
        {
            Terminal terminal = new Terminal();

            terminal.baseArsa = (getInstance().ciudadStar != null && !getInstance().ciudadStar.Equals("")) ? getInstance().ciudadStar : null;
            terminal.caja = (getInstance().caja != null && !getInstance().caja.Equals("")) ? getInstance().caja : null;
            terminal.sucursal = (getInstance().sucursal != null && !getInstance().sucursal.Equals("")) ? getInstance().sucursal : null;
            terminal.pinpadId = (getInstance().idPinpad != null && !getInstance().idPinpad.Equals("")) ? getInstance().idPinpad : null;
            terminal.printerId = (getInstance().idPrinter != null && !getInstance().idPrinter.Equals("")) ? getInstance().idPrinter : null;

            return terminal;
        }


        public string getValtechURI(string serviceName)
        {
            string baseUrl = Configuration.getInstance().baseUrlValtech;

            string uri = null;
            switch (serviceName)
            {
                case "fraude":
                    uri = "fraud/case";
                    break;
                case "auth":
                    uri = "auth/token";
                    break;
                case "audit":
                    uri = "audit/records";
                    break;
                case "promos":
                    uri = "paymentMethods/rules/paymentOptions/evaluation";
                    break;
                case "reservation":
                    uri = "reservation/pnr/";
                    break;
            }

            string finalUrl = baseUrl + uri;
            return finalUrl;
        }

        public string getPathPoiFiles(bool isCitrix)
        {
            string path = "";
            if (isCitrix)
                path = Environment.ExpandEnvironmentVariables(@"%USERPROFILE%") + System.IO.Path.DirectorySeparatorChar + "poi" + System.IO.Path.DirectorySeparatorChar; 

            else
                path = Application.StartupPath.Replace("Visual_Basic", "") + "poi" +  System.IO.Path.DirectorySeparatorChar ;
           
            // Crea PATH ./poi
            System.IO.Directory.CreateDirectory(path);

            return path;
        }



    }
}

    


