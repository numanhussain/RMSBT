using RMSCORE.EF;
using RMSREPO.Generic;
using RMSSERVICES.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMSSERVICES.UserDetail
{
    public class UserService : IUserService
    {
        #region -- Service Variables --
        IUnitOfWork UnitOfWork;
        IRepository<User> UserRepository;
        IRepository<Candidate> CandidateRepository;
        #endregion
        #region -- Service Interface Implementation --
        public UserService(IUnitOfWork unitOfWork, IRepository<User> userRepository,IRepository<Candidate> candidateRepository)
        {
            UnitOfWork = unitOfWork;
            UserRepository = userRepository;
            CandidateRepository = candidateRepository;
        }

        public List<User> GetIndex()
        {
            return UserRepository.GetAll();
        }
        public ServiceMessage RegisterUser(User dbOperation)
        {


            dbOperation.DateCreated = DateTime.Today;
            UserRepository.Add(dbOperation);
            UserRepository.Save();
            Candidate dbCandidate = new Candidate();
            dbCandidate.CName = dbOperation.UserName;
            dbCandidate.UserID = dbOperation.UserID;
            dbCandidate.EmailID = dbOperation.Email;
            CandidateRepository.Add(dbCandidate);
            CandidateRepository.Save();

            return new ServiceMessage();
        }
        #endregion
        #region -- Service Private Methods --
        #endregion
    }
}
