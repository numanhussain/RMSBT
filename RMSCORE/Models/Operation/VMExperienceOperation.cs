using RMSCORE.EF;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMSCORE.Models.Operation
{
    public class VMExperienceOperation : ExperienceDetail
    {
        public string ContactEmployerYes { get; set; }
        public string ContactEmployerNo { get; set; }
    }
}
