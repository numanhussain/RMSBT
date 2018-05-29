using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMSSERVICES.Generic
{
    public static class AssistantService
    {
        public static string Hostname = "mail.bestway.com.pk";
        public static int Port = 25;
        public static string Username = "essp.tms@bestway.com.pk";
        public static string Password = "Bcl#0099";
        public static bool IsDateTime(string txtDate)
        {
            DateTime tempDate;
            return DateTime.TryParse(txtDate, out tempDate);
        }
        //public static string Hostname = "smtp.gmail.com";
        //public static int Port = 587;
        //public static string Username = "cnssoftwaretesting@gmail.com";
        //public static string Password = "Cns12345#";
        //public static string Hostname = "mail.cns.com.pk";
        //public static int Port = 587;
        //public static string Username = "numan@cns.com.pk";
        //public static string Password = "Numan2251303";
        //public static string Hostname = "10.227.16.65";
        //public static int Port = 25;
        //public static string Username = "essp.tms@bestway.com.pk";
        //public static string Password = "Bcl#0099";
    }
}
