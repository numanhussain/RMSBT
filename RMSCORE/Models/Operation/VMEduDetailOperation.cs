using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMSCORE.Models.Operation
{
    public class VMEduDetailOperation
    {
        public int EduID { get; set; }
        public int? CandidateID { get; set; }
        public string CandidateName { get; set; }
        public int? DegreeID { get; set; }
        public string DegreeName { get; set; }
        public int? InstitutionID { get; set; }
        public string InstitutionName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string ObtainedMark { get; set; }
        public string TotalMark { get; set; }
        public string Percentage { get; set; }
        public string CGPA { get; set; }
        public string BoardName { get; set; }
        public string PassingYear { get; set; }
    }
}
