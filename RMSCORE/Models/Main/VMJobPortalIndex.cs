using RMSCORE.Models.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMSCORE.Models.Main
{
    public class VMJobPortalIndex
    {
        public string FilterBox { get; set; }
        public List<CustomModel> LocationList  { get; set; }
        public List<CustomModel> CatagoryList  { get; set; }
    }
}
