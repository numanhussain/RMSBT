using RMSCORE.EF;
using RMSCORE.Models.Other;
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
        IRepository<User> UserRepository;
        IRepository<Candidate> CandidateRepository;
        #endregion
        #region -- Service Interface Implementation --
        public UserService(IRepository<User> userRepository, IRepository<Candidate> candidateRepository)
        {
            UserRepository = userRepository;
            CandidateRepository = candidateRepository;
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
        public ServiceMessage RegisterUser(UserModel vmUserModel, V_UserCandidate LoggedInUser)
        {
            User dbUser = new User();
            dbUser.DateCreated = DateTime.Today;
            dbUser.AppliedAs = vmUserModel.CatagoryID;
            dbUser.UserStage = 1;
            dbUser.UserName = vmUserModel.Email;
            dbUser.Email = vmUserModel.Email;
            dbUser.Password = StringCipher.Encrypt(vmUserModel.Password);
            dbUser.RetypePassword = StringCipher.Encrypt(vmUserModel.RetypePassword);
            dbUser.SecurityLink = RandomString(15);
            UserRepository.Add(dbUser);
            UserRepository.Save();
            Candidate dbCandidate = new Candidate();
            dbCandidate.CandidateID = dbUser.UserID;
            dbCandidate.UserID = dbUser.UserID;
            dbCandidate.EmailID = dbUser.Email;
            dbCandidate.AppliedAs = dbUser.AppliedAs;
            dbCandidate.DateCreated = DateTime.Today;
            CandidateRepository.Add(dbCandidate);
            CandidateRepository.Save();
            vmUserModel.UserID = dbUser.UserID;
            vmUserModel.Password = dbUser.Password;
            vmUserModel.SecurityLink = dbUser.SecurityLink;
            return new ServiceMessage();
        }
        #endregion
        #region -- Service Private Methods --
        #endregion
    }
}
