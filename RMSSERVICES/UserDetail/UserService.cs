using RMSCORE.EF;
using RMSREPO.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMSSERVICES.UserDetail
{
    public class UserService:IUserService
    {
        #region -- Service Variables --
        IUnitOfWork UnitOfWork;
        IRepository<User> UserRepository;
        #endregion
        #region -- Service Interface Implementation --
        public UserService(IUnitOfWork unitOfWork, IRepository<User> userRepository)
        {
            UnitOfWork = unitOfWork;
            UserRepository = userRepository;
        }

        public List<User> GetIndex()
        {
           return UserRepository.GetAll();
        }
        #endregion
        #region -- Service Private Methods --
        #endregion
    }
}
