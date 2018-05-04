using RMSCORE.EF;
using RMSCORE.Models.Helper;
using RMSCORE.Models.Main;
using RMSCORE.Models.Operation;
using RMSSERVICES.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMSSERVICES.Experience
{
    public interface IExperienceDetailService
    {
        List<VMExperienceIndex> GetIndex(int cid);
        VMExperienceOperation GetCreate(int id);
        ServiceMessage PostCreate(VMExperienceOperation obj, V_UserCandidate LoggedInUser);
        VMExperienceOperation GetEdit(int id);
        ServiceMessage PostEdit(VMExperienceOperation obj);
        VMExperienceOperation GetDelete(int? id);
        ServiceMessage PostDelete(VMExperienceOperation obj);
        ServiceMessage PostIndex(int? TotalExp, int? CementExp, V_UserCandidate LoggedInUser);
    }
}
