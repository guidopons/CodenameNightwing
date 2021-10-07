using System;
using System.Diagnostics;

namespace CodenameNightwing.Varios
{
    class OfertasServerHasar
    {
        public static void startServer()
        {
            try
            {
                ProcessStartInfo start = new ProcessStartInfo();
                start.Arguments = "";
                start.WorkingDirectory = "Visual_Basic";
                start.FileName = "ServerAplicarOfertas";
                start.WindowStyle = ProcessWindowStyle.Normal;
                start.CreateNoWindow = true;
                Process[] pname = Process.GetProcessesByName("ServerAplicarOfertas");
                if (pname.Length == 0)
                    Process.Start(start);
            }
            catch (Exception e)
            {
                Program.logger.Error("Error al iniciar servidor de aplicar ofertas", e);
            }
        }

        public static void stopServer()
        {
            try
            {
                Process[] proc = Process.GetProcessesByName("ServerAplicarOfertas");
                if(proc.Length>0)
	                proc[0].Kill();
            }
            catch(Exception e)
            {
                Program.logger.Error("Error al terminar servidor de aplicar ofertas", e);
            }
        }
    }
}
