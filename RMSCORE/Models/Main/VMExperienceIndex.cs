using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMSCORE.Models.Main
{
    public class VMExperienceIndex
    {
        public int ExpID { get; set; }
        public string PositionTitle { get; set; }
        public string EmployerName { get; set; }
        public string JobTitle { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? CurrentlyWorking { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public string Salary { get; set; }
        public long? CandidateID { get; set; }
        public string CandidateName { get; set; }
        public int? IndustryID { get; set; }
        public string ExpIndustryName { get; set; }
    }
}
