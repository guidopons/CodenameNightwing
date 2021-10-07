using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Security.Principal;
using System.Text;
using System.Threading;

namespace CodenameNightwing.Varios
{
    public class NetworkUtils
    {

        public static string GetMacAddress()
        {
            string mac = "";
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {

                if (nic.OperationalStatus == OperationalStatus.Up && (!nic.Description.Contains("Virtual") && !nic.Description.Contains("Pseudo")))
                {
                    if (nic.GetPhysicalAddress().ToString() != "")
                    {
                        mac = nic.GetPhysicalAddress().ToString();
                    }
                }
            }
            return mac;

        }

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
        /*
        public string getGroups()
        {

            AppDomain myDomain = Thread.GetDomain();
            myDomain.SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal);
            WindowsPrincipal myP = (WindowsPrincipal)Thread.CurrentPrincipal;
            ICollection groups = GetWindowsGroups((WindowsIdentity)myP.Identity);

            
            Console.WriteLine(myP.Identity.Name);
            Console.WriteLine();
            Console.WriteLine("Member of:");
            Console.WriteLine("—————————–");
            foreach (string name in groups)
            {
                Console.WriteLine(name);
            }
        }
        private static ICollection GetWindowsGroups(WindowsIdentity id)
        {
            System.Collections.ArrayList groups = new System.Collections.ArrayList();
            IdentityReferenceCollection irc = id.Groups;
            foreach (IdentityReference ir in irc) { NTAccount acc = (NTAccount)ir.Translate(typeof(NTAccount)); groups.Add(acc.Value);  }
            return groups;

        }*/
    }



}
