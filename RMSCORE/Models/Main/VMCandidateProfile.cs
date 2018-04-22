using RMSCORE.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMSCORE.Models.Main
{
    public class VMCandidateProfileView
    {
        public int? CandidateID { get; set; }
        public int? JobID { get; set; }
        public V_CandidateProfile PersonalDetails { get; set; }
        public List<V_Candidate_EduDetail> EducationalDetails { get; set; }
        public V_Candidate_Miscellaneous MiscellaneousDetails { get; set; }
        public List<V_Candidate_Exp> ExperienceDetails { get; set; }
        public CompensationDetail CompensationDetails { get; set; }
        public CandidateStrength SelfAssessment { get; set; }
        public List<V_Candidate_Reference> Referrence { get; set; }
        public List<V_Candidate_Skills> Skill { get; set; }
    }
}
