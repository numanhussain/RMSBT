//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RMSCORE.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class VRM_EduDetail
    {
        public int EduID { get; set; }
        public Nullable<int> DegreeLevelID { get; set; }
        public string DegreeLevel { get; set; }
        public Nullable<int> ObtainedMark { get; set; }
        public Nullable<int> TotalMark { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public Nullable<int> InstitutionID { get; set; }
        public string InstituteName { get; set; }
        public string MajorSubject { get; set; }
        public int CandidateID { get; set; }
        public string Percentage { get; set; }
        public Nullable<double> CGPA { get; set; }
        public string DegreeTitle { get; set; }
        public string PassingYear { get; set; }
        public string OtherInstitute { get; set; }
        public Nullable<bool> InProgress { get; set; }
        public Nullable<int> DegreeTypeID { get; set; }
        public string EduTypeName { get; set; }
        public string OtherDegreeType { get; set; }
        public string OtherDegreeLevelName { get; set; }
        public string GradeName { get; set; }
        public Nullable<int> BoardID { get; set; }
        public string BiseName { get; set; }
        public string OtherBoardName { get; set; }
        public Nullable<int> EduCriteriaID { get; set; }
        public Nullable<System.DateTime> EditDate { get; set; }
    }
}
