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
    
    public partial class CompensationDetail
    {
        public int CID { get; set; }
        public string MBSalary { get; set; }
        public string MGSalary { get; set; }
        public Nullable<bool> Bonus { get; set; }
        public Nullable<bool> BBonus { get; set; }
        public Nullable<bool> GBonus { get; set; }
        public string BonusPerYear { get; set; }
        public Nullable<bool> LFA { get; set; }
        public Nullable<bool> BLFA { get; set; }
        public Nullable<bool> GLFA { get; set; }
        public string LFAPerYear { get; set; }
        public Nullable<bool> OT { get; set; }
        public Nullable<bool> BOT { get; set; }
        public Nullable<bool> GOT { get; set; }
        public string TransportAllowence { get; set; }
        public string MobileAllowence { get; set; }
        public string CarEntitlement { get; set; }
        public string MobileUserLimit { get; set; }
        public Nullable<bool> BuyBackOption { get; set; }
        public string AccomdAllowence { get; set; }
        public string COLA { get; set; }
        public string Other { get; set; }
        public Nullable<bool> Food { get; set; }
        public Nullable<bool> Free { get; set; }
        public Nullable<bool> Subsidized { get; set; }
        public string OPDInsurance { get; set; }
        public string IPInsurance { get; set; }
        public string LifeInsurance { get; set; }
        public Nullable<bool> ProvidentFund { get; set; }
        public Nullable<bool> GProvidentFund { get; set; }
        public Nullable<bool> BProvidentFund { get; set; }
        public string ProvidentFundPerYear { get; set; }
        public Nullable<bool> Gratuity { get; set; }
        public Nullable<bool> BGratuity { get; set; }
        public Nullable<bool> GGratuity { get; set; }
        public string AnnualTAllowence { get; set; }
        public string CasualTAllowence { get; set; }
        public string MedTAllowence { get; set; }
        public string TAllowenceWorkDay { get; set; }
        public string ExpectedSalary { get; set; }
        public string OtherBenifits { get; set; }
        public Nullable<int> CandidateID { get; set; }
    }
}
