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
        ServiceMessage RegisterUser(User dbOperation);
    }
}
