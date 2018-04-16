using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMSCORE.Models.Operation
{
    public class VMReferenceOperation
    {
        public int RefID { get; set; }
        public string RefName { get; set; }
        public string RefEmail { get; set; }
        public string RefContact { get; set; }
        public string RefDesignation { get; set; }
        public int? CandidateID { get; set; }
        public string CandidateName { get; set; }
        public string Organization { get; set; }
        public string HowLongKnown { get; set; }
    }
}
