using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceProcess;
using CodenameNightwing.Exceptions;
using CodenameNightwing.Config;
using System.IO;

namespace CodenameNightwing.Varios
{
    class ServicesLibrary
    {

        private static string serviceName = Configuration.getInstance().serviceVtolName;
        private static int timeoutMilliseconds = int.Parse(Configuration.getInstance().timeOutService);


        public static ServiceControllerStatus getServiceStatus()
        {

            ServiceController sc = new ServiceController(serviceName);

            return sc.Status;

        }

        public static void StartService()
        {
          ServiceController service = new ServiceController(serviceName);
          try
          {
            TimeSpan timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds);

            service.Start();
            service.WaitForStatus(ServiceControllerStatus.Running, timeout);
          }
          catch (Exception e)
          {
                string msg = "Error al inicializar el servicio con nombre: " + serviceName;
                Program.logger.Error(msg , e);
                throw new ServiceException(msg);
          }
        }


        public static void StopService()
        {
            ServiceController service = new ServiceController(serviceName);
            try
            {
                TimeSpan timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds);

                service.Stop();
                service.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
            }
            catch (Exception e)
            {
                string msg = "Error al hacer stop el servicio con nombre: " + serviceName;
                Program.logger.Error(msg, e);
                throw new ServiceException(msg);
            }
        }

        public static void RestartService()
        {
            ServiceController service = new ServiceController(serviceName);
            try
            {
                int millisec1 = Environment.TickCount;
                TimeSpan timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds);

                if ( service.Status.Equals(ServiceControllerStatus.Running)) { 
                    service.Stop();
                    service.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
                }

                // count the rest of the timeout
                int millisec2 = Environment.TickCount;
                timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds - (millisec2 - millisec1));

                // Borramos los archivos del PINPAD
                string path = Application.StartupPath + "\\" + Configuration.getInstance().vtolPosClientLibFolder + "\\config\\";
                string crypthPath = path + "crypt.properties";
                string wkeyPath = path + "workingKeys.properties";
                File.Delete(crypthPath );
                File.Delete(wkeyPath);


                service.Start();
                service.WaitForStatus(ServiceControllerStatus.Running, timeout);
            }
            catch (Exception e)
            {
                string msg = "Error al hacer stop el servicio con nombre: " + serviceName;
                Program.logger.Error(msg, e);
                throw new ServiceException(msg);
            }
        }
    }


}
