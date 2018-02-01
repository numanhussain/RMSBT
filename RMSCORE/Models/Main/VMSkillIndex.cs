using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMSCORE.Models.Main
{
    public class VMSkillIndex
    {
        public int SkillID { get; set; }
        public string SkillTitle { get; set; }
        public int? SkillLevelID { get; set; }
        public string skillLevelName { get; set; }
        public string Description { get; set; }
        public long? CandidateID { get; set; }
        public string CandidateName { get; set;}
    }
}
