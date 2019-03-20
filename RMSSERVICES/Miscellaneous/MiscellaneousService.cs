using RMSCORE.EF;
using RMSCORE.Models.Operation;
using RMSREPO.Generic;
using RMSSERVICES.Generic;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
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
            Expression<Func<MiscellaneousDetail, bool>> SpecificClient = c => c.CandidateID == LoggedInUser.CandidateID;
            List<MiscellaneousDetail> dbMiscellaneouss = MiscellaneousRepository.FindBy(SpecificClient).ToList();
            MiscellaneousDetail dbMiscellaneous = new MiscellaneousDetail();
            if (MiscellaneousRepository.FindBy(SpecificClient).Count() > 0)
            {
                dbMiscellaneous = ConvertMiscellaneousObject(obj, LoggedInUser);
                MiscellaneousRepository.Edit(dbMiscellaneous);
                MiscellaneousRepository.Save();

            }
            else
            {
                dbMiscellaneous = ConvertMiscellaneousObject(obj, LoggedInUser);
                MiscellaneousRepository.Add(dbMiscellaneous);
                MiscellaneousRepository.Save();

            }

            return new ServiceMessage();
        }
        #endregion
        #region -- Service Private Methods --
        private MiscellaneousDetail ConvertMiscellaneousObject(MiscellaneousDetail obj, V_UserCandidate LoggedInUser)
        {
            MiscellaneousDetail dbMiscellaneous = new MiscellaneousDetail();
            dbMiscellaneous.PMiscellaneousID = obj.PMiscellaneousID;
            dbMiscellaneous.CandidateID = LoggedInUser.CandidateID;
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
            dbMiscellaneous.CrimeDetail = obj.CrimeDetail;
            dbMiscellaneous.WorkingRelative = obj.WorkingRelative;
            if (obj.WorkingRelativeName != null)
            {
                dbMiscellaneous.WorkingRelativeName = ConvertToTitleCase(obj.WorkingRelativeName);
            }
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
            if (obj.WorkingRelativeCompanyName != null)
            {
                dbMiscellaneous.WorkingRelativeCompanyName = ConvertToTitleCase(obj.WorkingRelativeCompanyName);
            }
            dbMiscellaneous.InterviewStatusID = obj.InterviewStatusID;
            if (obj.WorkedBeforeCompanyName != null)
            {
                dbMiscellaneous.WorkedBeforeCompanyName = ConvertToTitleCase(obj.WorkedBeforeCompanyName);
            }
            dbMiscellaneous.WorkedBeforeCurrentlyWorking = obj.WorkedBeforeCurrentlyWorking;
            if (obj.InterviewBeforeCompanyName != null)
            {
                dbMiscellaneous.InterviewBeforeCompanyName = ConvertToTitleCase(obj.InterviewBeforeCompanyName);
            }
            dbMiscellaneous.LinkedInLink = obj.LinkedInLink;
            dbMiscellaneous.NoticeTimeID = obj.NoticeTimeID;
            dbMiscellaneous.EditDate= DateTime.Now;
            return dbMiscellaneous;
        }
        #endregion
        public string ConvertToTitleCase(string obj)
        {
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            TextInfo textInfo = cultureInfo.TextInfo;
            string val = textInfo.ToLower(obj);
            string val2 = textInfo.ToTitleCase(val);
            return val2;
        }
    }
}
