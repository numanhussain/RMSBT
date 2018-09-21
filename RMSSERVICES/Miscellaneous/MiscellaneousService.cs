using RMSCORE.EF;
using RMSCORE.Models.Operation;
using RMSREPO.Generic;
using RMSSERVICES.Generic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RMSSERVICES.Miscellaneous
{
    public class MiscellaneousService : IMiscellaneousService
    {
        #region -- Service Variables --
        IUnitOfWork UnitOfWork;
        IRepository<MiscellaneousDetail> MiscellaneousRepository;
        IRepository<Candidate> CandidateRepository;
        IRepository<User> UserRepository;
        public MiscellaneousService(IUnitOfWork unitOfWork, IRepository<Candidate> candidateRepository, IRepository<MiscellaneousDetail> miscellaneousRepository, IRepository<User> userRepository)
        {
            UnitOfWork = unitOfWork;
            MiscellaneousRepository = miscellaneousRepository;
            CandidateRepository = candidateRepository;
            UserRepository = userRepository;
        }
        #endregion
        #region -- Service Interface Implementation --
        public MiscellaneousDetail GetCreate(int id)
        {
            Expression<Func<MiscellaneousDetail, bool>> SpecificClient = c => c.CandidateID == id;
            MiscellaneousDetail dbMiscellaneous = new MiscellaneousDetail();
            if (MiscellaneousRepository.FindBy(SpecificClient).Count() > 0)
            {
                dbMiscellaneous = MiscellaneousRepository.FindBy(SpecificClient).First();
            }
            return dbMiscellaneous;
        }
        public ServiceMessage PostCreate(MiscellaneousDetail obj, V_UserCandidate LoggedInUser)
        {
            User dbUser = UserRepository.GetSingle((int)LoggedInUser.UserID);
            dbUser.HasCV = LoggedInUser.HasCV;
            dbUser.UserStage = LoggedInUser.UserStage;
            UserRepository.Edit(dbUser);
            UserRepository.Save();
            Expression<Func<MiscellaneousDetail, bool>> SpecificClient = c => c.CandidateID == obj.CandidateID;
            MiscellaneousDetail dbMiscellaneous = new MiscellaneousDetail();
            if (MiscellaneousRepository.FindBy(SpecificClient).Count() > 0)
            {
                dbMiscellaneous = ConvertMiscellaneousObject(obj);
                MiscellaneousRepository.Edit(dbMiscellaneous);
                MiscellaneousRepository.Save();
            }
            else
            {
                dbMiscellaneous = ConvertMiscellaneousObject(obj);
                MiscellaneousRepository.Add(dbMiscellaneous);
                MiscellaneousRepository.Save();
            }

            return new ServiceMessage();
        }
        #endregion
        #region -- Service Private Methods --
        private MiscellaneousDetail ConvertMiscellaneousObject(MiscellaneousDetail obj)
        {
            MiscellaneousDetail dbMiscellaneous = new MiscellaneousDetail();
            dbMiscellaneous.PMiscellaneousID = obj.PMiscellaneousID;
            dbMiscellaneous.CrimanalRecord = obj.CrimanalRecord;
            dbMiscellaneous.WorkingRelative = obj.WorkingRelative;
            dbMiscellaneous.WorkedBefore = obj.WorkedBefore;
            dbMiscellaneous.DateJoining = obj.DateJoining;
            dbMiscellaneous.DateLeavig = obj.DateLeavig;
            dbMiscellaneous.EmploymentNo = obj.EmploymentNo;
            dbMiscellaneous.Designation = obj.Designation;
            dbMiscellaneous.Location = obj.Location;
            dbMiscellaneous.ReasonLeaving = obj.ReasonLeaving;
            dbMiscellaneous.HearAboutJobID = obj.HearAboutJobID;
            dbMiscellaneous.TotalExp = obj.TotalExp;
            dbMiscellaneous.CementExp = obj.CementExp;
            dbMiscellaneous.NoticeTime = obj.NoticeTime;
            dbMiscellaneous.CandidateID = obj.CandidateID;
            dbMiscellaneous.CrimeDetail = obj.CrimeDetail;
            dbMiscellaneous.WorkingRelative = obj.WorkingRelative;
            dbMiscellaneous.WorkingRelativeName = obj.WorkingRelativeName;
            dbMiscellaneous.WorkingRelativeRelation = obj.WorkingRelativeRelation;
            dbMiscellaneous.WorkingRelativeDepartment = obj.WorkingRelativeDepartment;
            dbMiscellaneous.WorkingRelativeDesignation = obj.WorkingRelativeDesignation;
            dbMiscellaneous.WorkingRelativeLocation = obj.WorkingRelativeLocation;
            dbMiscellaneous.InterviewedBefore = obj.InterviewedBefore;
            dbMiscellaneous.InterviewedDate = obj.InterviewedDate;
            dbMiscellaneous.InterviewedLocation = obj.InterviewedLocation;
            dbMiscellaneous.AppliedPosition = obj.AppliedPosition;
            dbMiscellaneous.HearAboutDetail = obj.HearAboutDetail;
            dbMiscellaneous.Disability = obj.Disability;
            dbMiscellaneous.DisabilityDetail = obj.DisabilityDetail;
            dbMiscellaneous.InternshipRequirement = obj.InternshipRequirement;
            dbMiscellaneous.InternshipDuration = obj.InternshipDuration;
            dbMiscellaneous.MGSalary = obj.MGSalary;
            dbMiscellaneous.ExpectedSalary = obj.ExpectedSalary;
            dbMiscellaneous.DateCompleted = DateTime.Now;
            dbMiscellaneous.BloodGroupID = obj.BloodGroupID;
            dbMiscellaneous.ReligionID = obj.ReligionID;
            dbMiscellaneous.MaritalStatusID = obj.MaritalStatusID;
            dbMiscellaneous.WorkingRelativeCompanyName = obj.WorkingRelativeCompanyName;
            dbMiscellaneous.InterviewStatusID = obj.InterviewStatusID;
            dbMiscellaneous.WorkedBeforeCompanyName = obj.WorkedBeforeCompanyName;
            dbMiscellaneous.WorkedBeforeCurrentlyWorking = obj.WorkedBeforeCurrentlyWorking;
            dbMiscellaneous.InterviewBeforeCompanyName = obj.InterviewBeforeCompanyName;
            dbMiscellaneous.LinkedInLink = obj.LinkedInLink;
            dbMiscellaneous.NoticeTimeID = obj.NoticeTimeID;
            return dbMiscellaneous;
        }
        #endregion
    }
}
