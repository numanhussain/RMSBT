using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace RMSSERVICES.Generic
{
    public static class EmailGenerate
    {
        public static void SendEmail(string To, string CC, string Body, string Subject, string name)
        {
            try
            {
                NetworkCredential cred = new NetworkCredential();
                MailAddress _from = new MailAddress(AssistantService.Username, "Careers at Bestway Cement Ltd.");
                MailAddress _to = new MailAddress(To, "");
                MailMessage mail = new MailMessage(_from, _to);
                SmtpClient client = new SmtpClient(AssistantService.Hostname, AssistantService.Port);
                //client.EnableSsl = true;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(AssistantService.Username, AssistantService.Password);
                mail.IsBodyHtml = true;
                if (Subject == null || Subject == "")
                {
                    mail.Subject = name;
                }
                else
                {
                    mail.Subject = "Position for" + Subject;
                }
                if (CC != null && CC != "")
                    mail.CC.Add(CC);
                mail.Body = Body;
                client.Send(mail);
                mail.Dispose();
                client.Dispose();
                //MyCustomFunctions.WriteToLogFile("Email send TO:" + item.EmailAddress + ", CC: " + item.CCAddress);
            }
            catch (Exception ex)
            {
            }
        }
    }
}
