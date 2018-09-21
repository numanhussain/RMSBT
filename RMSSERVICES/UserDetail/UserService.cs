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
        IRepository<CandidateStep> CandidateStepRepository;
        IRepository<V_CandidateDetail> V_CandidateDetailRepositiory;
        #endregion
        #region -- Service Interface Implementation --
        public UserService(IRepository<User> userRepository, IRepository<Candidate> candidateRepository, IRepository<CandidateStep> candidateStepRepository, IRepository<V_CandidateDetail> v_CandidateDetailRepositiory)
        {
            UserRepository = userRepository;
            CandidateRepository = candidateRepository;
            CandidateStepRepository = candidateStepRepository;
            V_CandidateDetailRepositiory = v_CandidateDetailRepositiory;
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
        public ServiceMessage RegisterUser(UserModel vmUserModel)
        {
            User dbUser = new User();
            dbUser.DateCreated = DateTime.Today;
            dbUser.EmailSentTime = DateTime.Now;
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
            CandidateStep dbCandidateSteps = new CandidateStep();
            dbCandidateSteps.CandidateID = dbCandidate.CandidateID;
            dbCandidateSteps.StepOne = false;
            dbCandidateSteps.StepTwo = false;
            dbCandidateSteps.StepThree = false;
            dbCandidateSteps.StepFour= false;
            dbCandidateSteps.StepFive = false;
            dbCandidateSteps.StepSix = false;
            dbCandidateSteps.StepSeven = false;
            dbCandidateSteps.StepEight= false;
            CandidateStepRepository.Add(dbCandidateSteps);
            CandidateStepRepository.Save();
            vmUserModel.UserID = dbUser.UserID;
            vmUserModel.Password = dbUser.Password;
            vmUserModel.SecurityLink = dbUser.SecurityLink;
            return new ServiceMessage();
        }

        public List<VMCandidateDetail> GetAllIndex()
        {
            List<V_CandidateDetail> dbVCandidateDetails = V_CandidateDetailRepositiory.GetAll();
            List<VMCandidateDetail> VMLoggedUsers = new List<VMCandidateDetail>();
            foreach (var dbVCandidateDetail in dbVCandidateDetails)
            {
                VMCandidateDetail vmCandidateDetail = new VMCandidateDetail();
                vmCandidateDetail.UserID = dbVCandidateDetail.UserID;
                vmCandidateDetail.Email = dbVCandidateDetail.Email;
                vmCandidateDetail.UserStage = dbVCandidateDetail.UserStage;
                vmCandidateDetail.CName= dbVCandidateDetail.CName;
                vmCandidateDetail.CompletePassword = StringCipher.Decrypt(dbVCandidateDetail.Password);
                vmCandidateDetail.DateCreated = dbVCandidateDetail.DateCreated;
                vmCandidateDetail.StepOne = dbVCandidateDetail.StepOne;
                vmCandidateDetail.StepTwo = dbVCandidateDetail.StepTwo;
                vmCandidateDetail.StepThree = dbVCandidateDetail.StepThree;
                vmCandidateDetail.StepFour = dbVCandidateDetail.StepFour;
                vmCandidateDetail.StepFive = dbVCandidateDetail.StepFive;
                vmCandidateDetail.StepSix = dbVCandidateDetail.StepSix;
                vmCandidateDetail.StepSeven = dbVCandidateDetail.StepSeven;
                vmCandidateDetail.StepEight = dbVCandidateDetail.StepEight;
                vmCandidateDetail.HasCV = dbVCandidateDetail.HasCV;
                VMLoggedUsers.Add(vmCandidateDetail);
            }
            return VMLoggedUsers.ToList();
        }
        #endregion
        #region -- Service Private Methods --
        #endregion
    }
}
