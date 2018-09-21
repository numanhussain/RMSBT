using RMSCORE.EF;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMSCORE.Models.Other
{
    public class VMLoggedUser : V_UserCandidate
    {
        public bool Reponse { get; set; }
        public string CompletePassword { get; set; }
    }
    public class UserModel : User
    {
        public string Captcha { get; set; }
        public int? CatagoryID { get; set; }
    }
    public class VMCandidateDetail : V_CandidateDetail
    {
        public string CompletePassword { get; set; }
    }
}
