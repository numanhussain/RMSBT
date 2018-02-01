using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMSCORE.Models.Helper
{
    public class CustomModel
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public bool IsSelected { get; set; }
    }

    public class VMNotification
    {
        public string Notification { get; set; }
        public string NotificationCount { get; set; }
    }
}
