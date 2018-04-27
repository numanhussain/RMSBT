using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMSCORE.Models.Operation
{
    public class VMExperienceOperation
    {
        public int ExpID { get; set; }
        public string PositionTitle { get; set; }
        public string EmployerName { get; set; }
        public string JobTitle { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? CurrentlyWorking { get; set; }
        public int? CityID { get; set; }
        public string Address { get; set; }
        [Required(ErrorMessage = "Mandatory !!")]
        [StringLength(250)]
        public string Responsibility1 { get; set; }
        [Required(ErrorMessage = "Mandatory !!")]
        [StringLength(250)]
        public string Responsibility2 { get; set; }
        [Required(ErrorMessage = "Mandatory !!")]
        [StringLength(250)]
        public string Responsibility3 { get; set; }
        public string Salary { get; set; }
        public int CandidateID { get; set; }
        public string CandidateName { get; set; }
        public int? IndustryID { get; set; }
        public string IndustryName { get; set; }
        public int? CareerLevelID { get; set; }
        public string HaveExperience { get; set; }
        public bool? ContactEmployer { get; set; }
        public string AreaofInterest { get; set; }
        public string ReasonOfLeaving { get; set; }
        public string SupervisorName { get; set; }
    }
}
