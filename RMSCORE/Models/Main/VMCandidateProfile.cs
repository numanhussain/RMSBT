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
        public Candidate PersonalDetails { get; set; }
        public List<V_Candidate_EduDetail> EducationalDetails { get; set; }
        public List<V_Candidate_Exp> ExperienceDetails { get; set; }
        public CompensationDetail CompensationDetails { get; set; }
        public MiscellaneousDetail MiscellaneousDetails { get; set; }
        public CandidateStrength SelfAssessment { get; set; }
    }
}
