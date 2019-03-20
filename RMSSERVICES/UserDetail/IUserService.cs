using RMSCORE.EF;
using RMSCORE.Models.Other;
using RMSSERVICES.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMSSERVICES.UserDetail
{
    public interface IUserService
    {
        List<User> GetIndex();
        bool VerifyLink(string key);
        ServiceMessage RegisterUser(UserModel vmUserModel);
        List<VMCandidateDetail> GetAllIndex();
        List<VMAppliedJobDetail> GetAppliedJobDetails(int? JobID, V_UserCandidate LoggedInUser);
    }
}
