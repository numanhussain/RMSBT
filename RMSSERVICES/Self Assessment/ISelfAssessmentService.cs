using RMSCORE.EF;
using RMSSERVICES.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMSSERVICES.Self_Assessment
{
    public interface ISelfAssessmentService
    {
        CandidateStrength GetIndex(int id);
        ServiceMessage PostIndex(CandidateStrength dbOperation, V_UserCandidate LoggedInUser);
    }
}
