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
    
    public partial class V_CandidateDetail
    {
        public int UserID { get; set; }
        public string Email { get; set; }
        public Nullable<int> UserStage { get; set; }
        public Nullable<bool> StepOne { get; set; }
        public Nullable<bool> StepTwo { get; set; }
        public Nullable<bool> StepThree { get; set; }
        public Nullable<bool> StepFour { get; set; }
        public Nullable<bool> StepFive { get; set; }
        public Nullable<bool> StepSix { get; set; }
        public Nullable<bool> StepSeven { get; set; }
        public Nullable<bool> StepEight { get; set; }
        public string Password { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public Nullable<bool> HasCV { get; set; }
        public string CName { get; set; }
    }
}