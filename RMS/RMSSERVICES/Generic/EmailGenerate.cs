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
        public static void SendEmail(string To, string CC, string Body, string Subject)
        {
            try
            {
                NetworkCredential cred = new NetworkCredential();
                MailAddress _from = new MailAddress("essp.tms@bestway.com.pk", "Time Management System");
                MailAddress _to = new MailAddress(To, "");
                MailMessage mail = new MailMessage(_from, _to);
                SmtpClient client = new SmtpClient();
                client.Port = 25;
                //client.EnableSsl = true;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential("essp.tms@bestway.com.pk", "Bcl#0099");
                client.Host = "mail.bestway.com.pk";
                client.UseDefaultCredentials = true;
                mail.IsBodyHtml = true;
                mail.Subject = Subject;
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
