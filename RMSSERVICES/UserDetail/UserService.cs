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
        public UserService(IUnitOfWork unitOfWork, IRepository<User> userRepository, IRepository<Candidate> candidateRepository)
        {
            UnitOfWork = unitOfWork;
            UserRepository = userRepository;
            CandidateRepository = candidateRepository;
        }

        public List<User> GetIndex()
        {
            return UserRepository.GetAll();
        }

        public bool VerifyLink(string key)
        {
            string kk = StringCipher.Encrypt("5","1234");
            string email = StringCipher.Decrypt(key,"1234");
            if (UserRepository.GetAll().Where(aa => aa.Email == email).Count() > 0)
            {
                // Todo -- Change User Status to Login
                return true;
            }
            else
                return false;
        }

        public ServiceMessage RegisterUser(User dbOperation)
        {
            dbOperation.DateCreated = DateTime.Today;
            dbOperation.UserStage = "SignUp";
            dbOperation.SecurityLink = StringCipher.Encrypt(dbOperation.Email, "1234");
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
