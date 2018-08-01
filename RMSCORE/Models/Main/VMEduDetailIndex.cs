using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMSCORE.Models.Main
{
    public class VMEduDetailIndex
    {
        public int EduID { get; set; }
        public int? CandidateID { get; set; }
        public string CandidateName { get; set; }
        public int? DegreeLevelID { get; set; }
        public string DegreeLevelName { get; set; }
        public int? InstitutionID { get; set; }
        public string InstitutionName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? ObtainedMark { get; set; }
        public int? TotalMark { get; set; }
        public string Percentage { get; set; }
        public double? CGPA { get; set; }
        public string BoardName { get; set; }
        public string IndustryName { get; set; }
        public string PassingYear { get; set; }
        public string DegreeTitle { get; set; }
        public string OtherInstitute { get; set; }
        public int? DegreeTypeID { get; set; }
        public string DegreeTypeName { get; set; }
        public bool? InProgress { get; set; }
        public string OtherDegreeLevelName { get; set; }
        public string GradeName { get; set; }
    }
}
