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
    
    public partial class CandidateStrength
    {
        public int StrengthID { get; set; }
        public string Strengths { get; set; }
        public string AreaOfImprovement { get; set; }
        public string MeetRequirements { get; set; }
        public Nullable<int> CandidateID { get; set; }
    }
}