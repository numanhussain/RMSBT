using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMSCORE.Models.Operation
{
    public class VMCompensationOperation
    {
        public int CID { get; set; }
        public string MBSalary { get; set; }
        public string MGSalary { get; set; }
        public bool Bonus { get; set; }
        public bool BBonus { get; set; }
        public bool GBonus { get; set; }
        public string BonusPerYear { get; set; }
        public bool LFA { get; set; }
        public bool BLFA { get; set; }
        public bool GLFA { get; set; }
        public string LFAPerYear { get; set; }
        public bool OT { get; set; }
        public bool BOT { get; set; }
        public bool GOT { get; set; }
        public string TransportAllowence { get; set; }
        public string MobileAllowence { get; set; }
        public string CarEntitlement { get; set; }
        public bool BuyBackOption { get; set; }
        public string MobileUserLimit { get; set; }
        public string AccomdAllowence { get; set; }
        public string COLA { get; set; }
        public string Other { get; set; }
        public bool Food { get; set; }
        public bool Free { get; set; }
        public bool Subscribed { get; set; }
        public string OPDInsurance { get; set; }
        public string IPInsurance { get; set; }
        public string LifeInsurance { get; set; }
        public bool ProvidentFund { get; set; }
        public bool GProvidentFund { get; set; }
        public bool BProvidentFund { get; set; }
        public string ProvidentFundPerYear { get; set; }
        public bool Gratuity { get; set; }
        public bool BGratuity { get; set; }
        public bool GGratuity { get; set; }
        public string AnnualTAllowence { get; set; }
        public string CasualTAllowence { get; set; }
        public string MedTAllowence { get; set; }
        public string TAllowenceWorkDay { get; set; }
        public string ExpectedSalary { get; set; }
        public string OtherBenifits { get; set; }
        public long CandidateID { get; set; }
    }
}
