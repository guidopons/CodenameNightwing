using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using CodenameNightwing.Config;

namespace CodenameNightwing.Varios
{
    class MailManager
    {


        public static bool sendMailLog() {

            if (Configuration.getInstance().smtpPort == null || Configuration.getInstance().smtpHost == null)
            {
                string msg = "No se encuentra el smtp configurado en aerolineas properties";
                Program.logger.Error(msg);
                return false;
            }

            try
            {

                MailMessage mail = new MailMessage("sistemapoi@aerolineas.com.ar", "mcroce@aerolineas.com.ar");
                SmtpClient client = new SmtpClient();
                client.Port = int.Parse(Configuration.getInstance().smtpPort);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Host = Configuration.getInstance().smtpHost;
                mail.Subject = "Log enviado desde PC: " + Configuration.getInstance().getTerminal().machineName;
                StringBuilder sb = new StringBuilder();
                sb.Append("PC: " + Configuration.getInstance().getTerminal().machineName);
                sb.Append("Sucursal: " + Configuration.getInstance().getTerminal().sucursal);
                sb.Append("Caja: " + Configuration.getInstance().getTerminal().caja);
                sb.Append("Printer: " + Configuration.getInstance().getTerminal().printerId);
                mail.Body = sb.ToString();
                client.Send(mail);

            }catch ( Exception e )
            {
                string msg = "Ocurrio un error al enviar el mail de Logs. Revisar con Sistemas";
                Program.logger.Error(msg , e);
                return false;
            }

            return true;
        }


    }
}
