using System.Windows.Forms;
using System.Management;

namespace CodenameNightwing.Varios
{
    class USBFinder
    {
        public static string findPort(string driver)
        {
            try
            {
                ManagementObjectSearcher entitySearcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PnPEntity where Description like '%" + driver + "%'");
                foreach (ManagementObject entidad in entitySearcher.Get())
                {
                   // if (entidad["Description"] != null)
                     //   if (entidad["Description"].ToString().ToUpper().Contains("PROLIFIC USB"))
                            return entidad["Name"].ToString().Substring(entidad["Name"].ToString().IndexOf("COM") + 3, entidad["Name"].ToString().Length - entidad["Name"].ToString().IndexOf("COM") - 4);
                }
                return "0";
            }
            catch (ManagementException e)
            {
                MessageBox.Show("An error occurred while querying for WMI data: " + e.Message);
                CodenameNightwing.Program.logger.Error("Error en la busqueda del dispositivo USB", e);
                return "0";
            }
        }
    }
}


