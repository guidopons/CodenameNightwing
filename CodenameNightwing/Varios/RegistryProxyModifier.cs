using Microsoft.Win32;
using System;
using System.Collections.Generic;

namespace CodenameNightwing.Varios
{
    public class RegistryProxyModifier
    {
        private static string keyDiezCien = "*10.100.6.128*";
        private static string keyDiezCienQA = "*10.100.36.30*";
        private static string keyAerolineasATiempo = "*aerolineasatiempo*";
        private static string keyIntranet = "*aeroweb*";
        private static string keyLocal = "<local>";
        public static void setearExcepcionesDeProxy()
        {
            try
            {
                Registry.CurrentUser.Flush();
                RegistryKey myKey = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings", true);//Registry.LocalMachine.OpenSubKey("SOFTWARE\\Company\\Compfolder", true);
                if (myKey != null)
                {
                    List<string> keysQueDeberianEstar = new List<string>();
                    keysQueDeberianEstar.Add(keyDiezCien);
                    keysQueDeberianEstar.Add(keyDiezCienQA);
                    keysQueDeberianEstar.Add(keyAerolineasATiempo);
                    keysQueDeberianEstar.Add(keyLocal);
                    keysQueDeberianEstar.Add(keyIntranet);
                    string valorActual = "";
                    foreach (var item in keysQueDeberianEstar)
                    {
                        if (myKey.GetValue("ProxyOverride") != null) { 
                            valorActual = myKey.GetValue("ProxyOverride").ToString();
                            if (!valorActual.Contains(item))
                                myKey.SetValue("ProxyOverride", valorActual + ";" + item, RegistryValueKind.String);
                        }else
                        {
                            myKey.SetValue("ProxyOverride", valorActual + ";" + item, RegistryValueKind.String);
                        }
                    }
                    myKey.Flush();
                    myKey.Close();

                    Registry.CurrentUser.Close();
                    Registry.CurrentUser.Flush();

                }
            }
            catch (Exception e)
            {
                Program.logger.Error("Error al modificar claves en el registro para agregar excepciones al proxy: ", e);
            }
        }
    }
}
