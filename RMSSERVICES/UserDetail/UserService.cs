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
        IRepository<CementExperience> CementExperienceRepository;
        #endregion
        #region -- Service Interface Implementation --
        public UserService(IUnitOfWork unitOfWork, IRepository<User> userRepository, IRepository<Candidate> candidateRepository, IRepository<CementExperience> cementExperienceRepository)
        {
            UnitOfWork = unitOfWork;
            UserRepository = userRepository;
            CandidateRepository = candidateRepository;
            CementExperienceRepository = cementExperienceRepository;
        }

        public List<User> GetIndex()
        {
            return UserRepository.GetAll();
        }

        public bool VerifyLink(string key)
        {
            if (UserRepository.GetAll().Where(aa => aa.SecurityLink == key).Count() > 0)
            {
                // Todo -- Change User Status to Login
                return true;
            }
            else
                return false;
        }
        private Random random = new Random();
        public string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public ServiceMessage RegisterUser(User dbOperation,V_UserCandidate LoggedInUser)
        {
            dbOperation.DateCreated = DateTime.Today;
            dbOperation.AppliedAs = dbOperation.AppliedAs;
            dbOperation.UserStage = 1;
            dbOperation.SecurityLink = RandomString(15);
            UserRepository.Add(dbOperation);
            UserRepository.Save();
            Candidate dbCandidate = new Candidate();
            dbCandidate.UserID = dbOperation.UserID;
            dbCandidate.EmailID = dbOperation.Email;
            dbCandidate.AppliedAs = dbOperation.AppliedAs;
            dbCandidate.DateCreated = DateTime.Today;
            CandidateRepository.Add(dbCandidate);
            CandidateRepository.Save();
            CementExperience dbCementExperience = new CementExperience();
            dbCementExperience.CandidateID = dbCandidate.CandidateID;
            CementExperienceRepository.Add(dbCementExperience);
            CementExperienceRepository.Save();
            return new ServiceMessage();
        }
        #endregion
        #region -- Service Private Methods --
        #endregion
    }
}
