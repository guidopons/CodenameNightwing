using log4net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CodenameNightwing.Varios
{
    public class CmdCommand
    {
        public static readonly ILog logger = LogManager.GetLogger(typeof(CmdCommand));

        public static bool IsRunning( int procId )
        {
            try
            {
                Process.GetProcessById( procId );
            }
            catch (ArgumentException)
            {
                return false;
            }
            return true;
        }

        public static int ExecuteBAT( string command)
        {

            try
            {
                Process proc = new Process();
                proc.StartInfo.FileName = command;
                proc.StartInfo.CreateNoWindow = false;
                proc.Start();
                return proc.Id;
            }
            catch( Exception e)
            {
                return 0;
            }

        }
        public static string ExecuteCommandSync(object command)
        {
            try
            {
                // create the ProcessStartInfo using "cmd" as the program to be run,
                // and "/c " as the parameters.
                // Incidentally, /c tells cmd that we want it to execute the command that follows,
                // and then exit.
                System.Diagnostics.ProcessStartInfo procStartInfo =
                    new System.Diagnostics.ProcessStartInfo("cmd", "/c " + command);

                // The following commands are needed to redirect the standard output.
                // This means that it will be redirected to the Process.StandardOutput StreamReader.
                procStartInfo.RedirectStandardOutput = true;
                procStartInfo.UseShellExecute = false;
                // Do not create the black window.
                procStartInfo.CreateNoWindow = true;
                // Now we create a process, assign its ProcessStartInfo and start it
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.StartInfo = procStartInfo;
                proc.Start();
                // Get the output into a string
                string result = proc.StandardOutput.ReadToEnd();

                return result;
            }
            catch (Exception objException)
            {
                logger.Error(objException);
                return null;
            }
        }
    }
}
