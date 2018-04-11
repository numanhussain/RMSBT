using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMSCORE.Models.Operation
{
    public class VMMiscellaneousOperation
    {
        public int PMiscellaneousID { get; set; }
        public string CrimanalRecord { get; set; }
        public string WorkingRelative { get; set; }
        public bool WorkedBefore { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateJoining { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateLeaving { get; set; }
        public string EmploymentNo { get; set; }
        public string Designation { get; set; }
        public string Location { get; set; }
        public string ReasonLeaving { get; set; }
        public int? TotalExp { get; set; }
        public int? CementExp { get; set; }
        public int NoticeTime { get; set; }
        public int HearAboutJobID{ get; set; }
        public string HearAboutSource { get; set; }
        public long CandidateID { get; set; }
        public string CandidateName { get; set; }
    }
}
