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
    
    public partial class V_Candidate_Exp
    {
        public int ExpID { get; set; }
        public string PostionTitle { get; set; }
        public string EmployerName { get; set; }
        public string JobTitle { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public Nullable<bool> CurrentlyWorking { get; set; }
        public string Address { get; set; }
        public int CandidateID { get; set; }
        public string CName { get; set; }
        public Nullable<int> IndustryID { get; set; }
        public string ExpIndustryName { get; set; }
        public string Responsibility1 { get; set; }
        public string Responsibility2 { get; set; }
        public string Responsibility3 { get; set; }
        public Nullable<int> CityID { get; set; }
        public string CityName { get; set; }
        public Nullable<int> CareerLevelID { get; set; }
        public string CareerLevelName { get; set; }
        public Nullable<bool> ContactEmployer { get; set; }
        public string AreaofInterest { get; set; }
        public string ReasonOfLeaving { get; set; }
        public string SupervisorName { get; set; }
        public string OtherCity { get; set; }
        public Nullable<int> CountryID { get; set; }
        public string CountryName { get; set; }
        public string PostalCode { get; set; }
        public string OtherCityName { get; set; }
        public string OtherIndustryName { get; set; }
        public Nullable<System.DateTime> EditDate { get; set; }
        public Nullable<double> TotalExp { get; set; }
        public Nullable<double> Cement { get; set; }
    }
}
