using CodenameNightwing.Config;
using System;
using System.Diagnostics;
using System.Management;
using System.ServiceProcess;
using System.Threading;

namespace CodenameNightwing.Varios
{
    class VTOLFullLibrary
    {
        public static void startServer()
        {
            try
            {
                ServicesLibrary.StartService();
            }
            catch (Exception e)
            {
                Program.logger.Error("Error al iniciar libreria full de VTOL", e);
            }
        }

        public static bool checkServer()
        {
            try
            {
                if (ServicesLibrary.getServiceStatus().Equals(ServiceControllerStatus.Running))
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                Program.logger.Error("Error al chequear estado de libreria full de VTOL", e);
                return false;
            }
        }

        public static void stopServer()
        {
            try
            {
                ServicesLibrary.StopService();
            }
            catch (Exception e)
            {
                Program.logger.Error("Error al terminar libreria full de VTOL", e);
            }
        }
    }
}
