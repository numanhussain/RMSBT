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
        IRepository<V_AppliedJob> V_AppliedJobRepositiory;
        #endregion
        #region -- Service Interface Implementation --
        public UserService(IRepository<User> userRepository, IRepository<Candidate> candidateRepository, IRepository<CandidateStep> candidateStepRepository, IRepository<V_CandidateDetail> v_CandidateDetailRepositiory, IRepository<V_AppliedJob> v_AppliedJobRepositiory)
        {
            UserRepository = userRepository;
            CandidateRepository = candidateRepository;
            CandidateStepRepository = candidateStepRepository;
            V_CandidateDetailRepositiory = v_CandidateDetailRepositiory;
            V_AppliedJobRepositiory = v_AppliedJobRepositiory;
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
            dbUser.DateCreated = DateTime.Now;
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
            dbCandidate.DateCreated = DateTime.Now;
            CandidateRepository.Add(dbCandidate);
            CandidateRepository.Save();
            CandidateStep dbCandidateSteps = new CandidateStep();
            dbCandidateSteps.CandidateID = dbCandidate.CandidateID;
            dbCandidateSteps.StepOne = false;
            dbCandidateSteps.StepTwo = false;
            dbCandidateSteps.StepThree = false;
            dbCandidateSteps.StepFour = false;
            dbCandidateSteps.StepFive = false;
            dbCandidateSteps.StepSix = false;
            dbCandidateSteps.StepSeven = false;
            dbCandidateSteps.StepEight = false;
            CandidateStepRepository.Add(dbCandidateSteps);
            CandidateStepRepository.Save();
            vmUserModel.UserID = dbUser.UserID;
            vmUserModel.Password = dbUser.Password;
            vmUserModel.SecurityLink = dbUser.SecurityLink;
            return new ServiceMessage();
        }

        public List<VMCandidateDetail> GetAllIndex()
        {
            List<V_CandidateDetail> dbVCandidateDetails = V_CandidateDetailRepositiory.GetAll().Where(aa => aa.UserID == 28991).ToList();
            List<VMCandidateDetail> VMLoggedUsers = new List<VMCandidateDetail>();
            foreach (var dbVCandidateDetail in dbVCandidateDetails)
            {
                VMCandidateDetail vmCandidateDetail = new VMCandidateDetail();
                vmCandidateDetail.UserID = dbVCandidateDetail.UserID;
                vmCandidateDetail.Email = dbVCandidateDetail.Email;
                vmCandidateDetail.UserStage = dbVCandidateDetail.UserStage;
                vmCandidateDetail.CName = dbVCandidateDetail.CName;
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

        public List<VMAppliedJobDetail> GetAppliedJobDetails(int? JobID, V_UserCandidate LoggendInUser)
        {
            List<VMAppliedJobDetail> vmAppliedJobDetails = new List<VMAppliedJobDetail>();
            if (JobID == 0 || JobID == null)
            {
                List<V_AppliedJob> dbVAppliedJobs = V_AppliedJobRepositiory.GetAll();
                foreach (var dbVAppliedJob in dbVAppliedJobs)
                {
                    VMAppliedJobDetail vmAppliedJobDetail = new VMAppliedJobDetail();
                    vmAppliedJobDetail.JobID = dbVAppliedJob.JobID;
                    vmAppliedJobDetail.JobTitle = dbVAppliedJob.JobTitle;
                    vmAppliedJobDetail.CandidateID = dbVAppliedJob.CandidateID;
                    vmAppliedJobDetail.CName = dbVAppliedJob.CName;
                    vmAppliedJobDetail.CatName = dbVAppliedJob.CatName;
                    vmAppliedJobDetail.DepatmentName = dbVAppliedJob.DepatmentName;
                    vmAppliedJobDetail.JStatusName = dbVAppliedJob.JStatusName;
                    vmAppliedJobDetail.HasCV = dbVAppliedJob.HasCV;
                    vmAppliedJobDetails.Add(vmAppliedJobDetail);
                }
            }
            else
            {
                List<V_AppliedJob> dbVAppliedJobs = V_AppliedJobRepositiory.GetAll().Where(aa => aa.JobID == JobID).ToList();
                foreach (var dbVAppliedJob in dbVAppliedJobs)
                {
                    VMAppliedJobDetail vmAppliedJobDetail = new VMAppliedJobDetail();
                    vmAppliedJobDetail.JobID = dbVAppliedJob.JobID;
                    vmAppliedJobDetail.JobTitle = dbVAppliedJob.JobTitle;
                    vmAppliedJobDetail.CandidateID = dbVAppliedJob.CandidateID;
                    vmAppliedJobDetail.CName = dbVAppliedJob.CName;
                    vmAppliedJobDetail.CatName = dbVAppliedJob.CatName;
                    vmAppliedJobDetail.DepatmentName = dbVAppliedJob.DepatmentName;
                    vmAppliedJobDetail.JStatusName = dbVAppliedJob.JStatusName;
                    vmAppliedJobDetail.HasCV = dbVAppliedJob.HasCV;
                    vmAppliedJobDetails.Add(vmAppliedJobDetail);
                }
            }
            return vmAppliedJobDetails.ToList();
        }
        #endregion
        #region -- Service Private Methods --
        #endregion
    }
}
