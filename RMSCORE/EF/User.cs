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
    
    public partial class User
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public Nullable<int> UserTypeID { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<bool> CanEdit { get; set; }
        public Nullable<bool> CanDelete { get; set; }
        public Nullable<bool> CanView { get; set; }
        public Nullable<bool> CanAdd { get; set; }
        public string Email { get; set; }
        public string RetypePassword { get; set; }
        public string UserStage { get; set; }
        public string SecurityLink { get; set; }
    }
}
