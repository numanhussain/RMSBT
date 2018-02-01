using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMSCORE.Models.Main
{
    public class VMReferenceIndex
    {
        public long RefID { get; set; }
        public string RefName { get; set; }
        public string RefEmail { get; set; }
        public string RefContact { get; set; }
        public string RefDesignation { get; set; }
        public long? CandidateID { get; set; }
        public string CadidateName { get; set; }
    }
}
