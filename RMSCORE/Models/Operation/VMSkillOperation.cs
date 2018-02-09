using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMSCORE.Models.Operation
{
    public class VMSkillOperation
    {
        public int SkillID { get; set; }
        public string SkillTitle { get; set; }
        public int? SLevelID { get; set; }
        public string SLevelName { get; set; }
        public string Description { get; set; }
        public int CandidateID { get; set; }
        public string CandidateName { get; set; }
    }
}
