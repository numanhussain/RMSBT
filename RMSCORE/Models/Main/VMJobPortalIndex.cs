using RMSCORE.EF;
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
        public List<CustomModel> LocationList { get; set; }
        public List<CustomModel> CatagoryList { get; set; }
    }
    public class VMAppliedJobIndex
    {
        public int CJobID { get; set; }
        public int? CandidateID { get; set; }
        public string CName { get; set; }
        public DateTime? CJobDate { get; set; }
        public int? JobID { get; set; }
        public string JobTitle { get; set; }
    }
    public class VMOpenJobIndex : V_JobDetail
    {
        public bool IsApplied { get; set; }
        public string FilterBox { get; set; }

    }
}
