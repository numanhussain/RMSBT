using RMSCORE.EF;
using RMSCORE.Models.Helper;
using RMSCORE.Models.Operation;
using RMSSERVICES.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMSSERVICES.Miscellaneous
{
    public interface IMiscellaneousService
    {
        MiscellaneousDetail GetCreate(int id);
        ServiceMessage PostCreate(MiscellaneousDetail obj, V_UserCandidate LoggedInUser);
    }
}
