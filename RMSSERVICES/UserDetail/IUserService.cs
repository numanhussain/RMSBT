using RMSCORE.EF;
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
        ServiceMessage RegisterUser(User dbOperation,V_UserCandidate LoggedInUser);
    }
}
