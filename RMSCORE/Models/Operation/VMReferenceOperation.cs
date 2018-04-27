using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMSCORE.Models.Operation
{
    public class VMReferenceOperation
    {
        public int RefID1 { get; set; }
        public string RefName1 { get; set; }
        public string RefEmail1 { get; set; }
        public string RefContact1 { get; set; }
        public string RefDesignation1 { get; set; }
        public int? CandidateID{ get; set; }
        public string CandidateName { get; set; }
        public string Organization1 { get; set; }
        public string HowLongKnown1 { get; set; }
        public int RefID2 { get; set; }
        public string RefName2 { get; set; }
        public string RefEmail2 { get; set; }
        public string RefContact2 { get; set; }
        public string RefDesignation2 { get; set; }
        public string Organization2 { get; set; }
        public string HowLongKnown2 { get; set; }
    }
}
