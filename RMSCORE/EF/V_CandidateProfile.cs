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
    
    public partial class V_CandidateProfile
    {
        public int CandidateID { get; set; }
        public string CName { get; set; }
        public Nullable<System.DateTime> DOB { get; set; }
        public string CNICNo { get; set; }
        public Nullable<int> BloodGroupID { get; set; }
        public string BGroupName { get; set; }
        public Nullable<int> MartialStatusID { get; set; }
        public string MartialStatusName { get; set; }
        public string Address { get; set; }
        public string LandlineNo { get; set; }
        public string CellNo { get; set; }
        public string EmailID { get; set; }
        public Nullable<int> CityID { get; set; }
        public string DomicileCityName { get; set; }
        public Nullable<int> CountryID { get; set; }
        public string CountryName { get; set; }
        public Nullable<int> NationalityCountryID { get; set; }
        public string NationalityCountry { get; set; }
        public Nullable<int> DomicileCityID { get; set; }
        public string CityName { get; set; }
        public string Objective { get; set; }
        public Nullable<int> GenderID { get; set; }
        public string GenderName { get; set; }
        public Nullable<int> ReligionID { get; set; }
        public string ReligionName { get; set; }
        public string AreaOfInterest { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public string FatherName { get; set; }
    }
}
