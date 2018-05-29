using RMSCORE.EF;
using RMSCORE.Models.Helper;
using RMSCORE.Models.Main;
using RMSCORE.Models.Operation;
using RMSREPO.Generic;
using RMSSERVICES.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace RMSSERVICES.Experience
{
    public class ExperienceDetailService : IExperienceDetailService
    {
        #region -- Service Variables --
        IUnitOfWork UnitOfWork;
        IRepository<V_Candidate_Exp> ExperienceDetailRepository;
        IRepository<ExperienceDetail> ExperienceRepository;
        IRepository<Candidate> CandidateRepository;
        IRepository<User> UserRepository;
        IRepository<MiscellaneousDetail> MiscellaneousRepository;
        public ExperienceDetailService(IUnitOfWork unitOfWork, IRepository<V_Candidate_Exp> experiencedetailRepository, IRepository<ExperienceDetail> experienceRepository, IRepository<Candidate> candidateRepository, IRepository<User> userRepository,
            IRepository<MiscellaneousDetail> miscellaneousRepository)
        {
            UnitOfWork = unitOfWork;
            CandidateRepository = candidateRepository;
            ExperienceDetailRepository = experiencedetailRepository;
            ExperienceRepository = experienceRepository;
            UserRepository = userRepository;
            MiscellaneousRepository = miscellaneousRepository;
        }
        #endregion
        #region -- Service Interface Implementation --
        public List<VMExperienceIndex> GetIndex(int cid)
        {
            Expression<Func<Candidate, bool>> HaveExperience = c => c.CandidateID == cid;
            Candidate dbCandidates = CandidateRepository.GetSingle(cid);
            Expression<Func<V_Candidate_Exp, bool>> SpecificCandidateExperience = c => c.CandidateID == cid;
            List<V_Candidate_Exp> dbVExperienceDetails = ExperienceDetailRepository.FindBy(SpecificCandidateExperience);
            List<VMExperienceIndex> vmExperienceDetails = new List<VMExperienceIndex>();
            foreach (var dbExperienceDetail in dbVExperienceDetails)
            {
                VMExperienceIndex vmExperienceDetail = new VMExperienceIndex();
                vmExperienceDetail.ExpID = dbExperienceDetail.ExpID;
                vmExperienceDetail.PositionTitle = dbExperienceDetail.PostionTitle;
                vmExperienceDetail.JobTitle = dbExperienceDetail.JobTitle;
                vmExperienceDetail.StartDate = dbExperienceDetail.StartDate;
                vmExperienceDetail.EndDate = dbExperienceDetail.EndDate;
                vmExperienceDetail.CurrentlyWorking = dbExperienceDetail.CurrentlyWorking;
                vmExperienceDetail.CityID = dbExperienceDetail.CityID;
                vmExperienceDetail.Address = dbExperienceDetail.Address;
                vmExperienceDetail.Reaponsibility1 = dbExperienceDetail.Responsibility1;
                vmExperienceDetail.Reaponsibility2 = dbExperienceDetail.Responsibility2;
                vmExperienceDetail.Reaponsibility3 = dbExperienceDetail.Responsibility3;
                vmExperienceDetail.EmployerName = dbExperienceDetail.EmployerName;
                vmExperienceDetail.CandidateID = dbExperienceDetail.CandidateID;
                vmExperienceDetail.CandidateName = dbExperienceDetail.CName;
                vmExperienceDetail.IndustryID = dbExperienceDetail.IndustryID;
                vmExperienceDetail.ExpIndustryName = dbExperienceDetail.ExpIndustryName;
                vmExperienceDetail.HaveExperience = dbCandidates.HaveExperience;
                vmExperienceDetail.ContactEmployer = dbExperienceDetail.ContactEmployer;
                vmExperienceDetail.AreaofInterest = dbExperienceDetail.AreaofInterest;
                vmExperienceDetail.ReasonOfLeaving = dbExperienceDetail.ReasonOfLeaving;
                vmExperienceDetail.SupervisorName = dbExperienceDetail.SupervisorName;
                vmExperienceDetail.CareerLevelID = dbExperienceDetail.CareerLevelID;
                //vmExperienceDetail.Experience=dbCementExperienceDetail.
                vmExperienceDetails.Add(vmExperienceDetail);
            }
            return vmExperienceDetails.OrderByDescending(aa => aa.ExpID).ToList();
        }
        public VMExperienceOperation GetCreate(int id)
        {
            VMExperienceOperation vmExperienceDetail = new VMExperienceOperation();
            vmExperienceDetail.CandidateID = id;
            return vmExperienceDetail;
        }
        public ServiceMessage PostCreate(VMExperienceOperation obj, V_UserCandidate LoggedInUser)
        {
            Expression<Func<User, bool>> SpecificEntries = c => c.UserID == LoggedInUser.UserID;
            List<User> images = UserRepository.FindBy(SpecificEntries);
            User image = images.First();
            image.UserStage = LoggedInUser.UserStage;
            UserRepository.Edit(image);
            UserRepository.Save();
            ExperienceDetail dbExperienceDetail = new ExperienceDetail();
            dbExperienceDetail.CandidateID = obj.CandidateID;
            dbExperienceDetail = ConvertExperienceObject(obj);
            dbExperienceDetail = ExperienceRepository.Add(dbExperienceDetail);
            ExperienceRepository.Save();
            return new ServiceMessage();
        }
        public VMExperienceOperation GetEdit(int id)
        {
            ExperienceDetail dbExperienceDetail = ExperienceRepository.GetSingle(id);
            VMExperienceOperation vmExperienceDetail = new VMExperienceOperation();
            vmExperienceDetail.ExpID = dbExperienceDetail.ExpID;
            vmExperienceDetail.PositionTitle = dbExperienceDetail.PostionTitle;
            vmExperienceDetail.JobTitle = dbExperienceDetail.JobTitle;
            vmExperienceDetail.StartDate = dbExperienceDetail.StartDate;
            vmExperienceDetail.EndDate = dbExperienceDetail.EndDate;
            vmExperienceDetail.CurrentlyWorking = dbExperienceDetail.CurrentlyWorking;
            vmExperienceDetail.CityID = dbExperienceDetail.CityID;
            vmExperienceDetail.Address = dbExperienceDetail.Address;
            vmExperienceDetail.Responsibility1 = dbExperienceDetail.Responsibility1;
            vmExperienceDetail.Responsibility2 = dbExperienceDetail.Responsibility2;
            vmExperienceDetail.Responsibility3 = dbExperienceDetail.Responsibility3;
            vmExperienceDetail.EmployerName = dbExperienceDetail.EmployerName;
            vmExperienceDetail.CandidateID = dbExperienceDetail.CandidateID;
            vmExperienceDetail.IndustryID = dbExperienceDetail.IndustryID;
            if (dbExperienceDetail.ContactEmployer == true)
            {

                vmExperienceDetail.ContactEmployerYes = "YES";
            }
            else
            {
                vmExperienceDetail.ContactEmployerNo = "NO";
            }
            vmExperienceDetail.AreaofInterest = dbExperienceDetail.AreaofInterest;
            vmExperienceDetail.ReasonOfLeaving = dbExperienceDetail.ReasonOfLeaving;
            vmExperienceDetail.SupervisorName = dbExperienceDetail.SupervisorName;
            vmExperienceDetail.CareerLevelID = dbExperienceDetail.CareerLevelID;
            vmExperienceDetail.CountryID = dbExperienceDetail.CountryID;
            vmExperienceDetail.OtherCityName = dbExperienceDetail.OtherCity;
            return vmExperienceDetail;
        }
        public ServiceMessage PostEdit(VMExperienceOperation obj)
        {
            ExperienceDetail dbExperienceDetail = new ExperienceDetail();
            dbExperienceDetail = ConvertExperienceObject(obj);
            ExperienceRepository.Edit(dbExperienceDetail);
            ExperienceRepository.Save();
            return new ServiceMessage();
        }
        public VMExperienceOperation GetDelete(int? id)
        {
            VMExperienceOperation obj = new VMExperienceOperation();
            Expression<Func<ExperienceDetail, bool>> TotalExperiences = c => c.ExpID == id;
            ExperienceDetail dbExperienceDetail = ExperienceRepository.GetSingle((int)id);
            obj.ExpID = dbExperienceDetail.ExpID;
            obj.PositionTitle = dbExperienceDetail.PostionTitle;
            obj.JobTitle = dbExperienceDetail.JobTitle;
            obj.StartDate = dbExperienceDetail.StartDate;
            obj.EndDate = dbExperienceDetail.EndDate;
            obj.CurrentlyWorking = dbExperienceDetail.CurrentlyWorking;
            obj.CityID = dbExperienceDetail.CityID;
            obj.Address = dbExperienceDetail.Address;
            obj.Responsibility1 = dbExperienceDetail.Responsibility1;
            obj.Responsibility2 = dbExperienceDetail.Responsibility2;
            obj.Responsibility3 = dbExperienceDetail.Responsibility3;
            obj.EmployerName = dbExperienceDetail.EmployerName;
            obj.CandidateID = dbExperienceDetail.CandidateID;
            obj.IndustryID = dbExperienceDetail.IndustryID;
            if (dbExperienceDetail.ContactEmployer == true)
            {

                obj.ContactEmployerYes = "YES";
            }
            else
            {
                obj.ContactEmployerNo = "NO";
            }
            obj.AreaofInterest = dbExperienceDetail.AreaofInterest;
            obj.ReasonOfLeaving = dbExperienceDetail.ReasonOfLeaving;
            obj.SupervisorName = dbExperienceDetail.SupervisorName;
            obj.CareerLevelID = dbExperienceDetail.CareerLevelID;
            obj.CountryID = dbExperienceDetail.CountryID;
            obj.OtherCityName = dbExperienceDetail.OtherCity;
            return obj;
        }
        public ServiceMessage PostDelete(VMExperienceOperation obj)
        {
            Expression<Func<ExperienceDetail, bool>> TotalExperience = c => c.ExpID == obj.ExpID;
            List<ExperienceDetail> dbExperienceDetails = ExperienceRepository.FindBy(TotalExperience);
            foreach (var dbExperienceDetail in dbExperienceDetails)
            {
                ExperienceRepository.Delete(dbExperienceDetail);
                ExperienceRepository.Save();
            }
            return new ServiceMessage();
        }
        public ServiceMessage PostGeneralExperience(int? TotalExp, V_UserCandidate LoggedInUser)
        {
            Expression<Func<MiscellaneousDetail, bool>> SpecificClient = c => c.CandidateID == LoggedInUser.CandidateID;
            List<MiscellaneousDetail> dbVRMPositions = MiscellaneousRepository.FindBy(SpecificClient);
            MiscellaneousDetail dbTotalExp = new MiscellaneousDetail();
            if (MiscellaneousRepository.FindBy(SpecificClient).Count() > 0)
            {
                dbTotalExp = dbVRMPositions.First();
                dbTotalExp.CandidateID = LoggedInUser.CandidateID;
                dbTotalExp.TotalExp = TotalExp;
                MiscellaneousRepository.Edit(dbTotalExp);
                MiscellaneousRepository.Save();
            }
            else
            {
                dbTotalExp.CandidateID = LoggedInUser.CandidateID;
                dbTotalExp.TotalExp = TotalExp;
                MiscellaneousRepository.Add(dbTotalExp);
                MiscellaneousRepository.Save();
            }

            return new ServiceMessage();
        }
        public ServiceMessage PostCementExperience(int? CementExp, V_UserCandidate LoggedInUser)
        {
            Expression<Func<MiscellaneousDetail, bool>> SpecificClient = c => c.CandidateID == LoggedInUser.CandidateID;
            List<MiscellaneousDetail> dbVRMPositions = MiscellaneousRepository.FindBy(SpecificClient);
            MiscellaneousDetail dbCementExp = new MiscellaneousDetail();
            if (MiscellaneousRepository.FindBy(SpecificClient).Count() > 0)
            {
                dbCementExp = dbVRMPositions.First();
                dbCementExp.CandidateID = LoggedInUser.CandidateID;
                dbCementExp.CementExp = CementExp;
                MiscellaneousRepository.Edit(dbCementExp);
                MiscellaneousRepository.Save();
            }
            else
            {
                dbCementExp.CandidateID = LoggedInUser.CandidateID;
                dbCementExp.CementExp = CementExp;
                MiscellaneousRepository.Add(dbCementExp);
                MiscellaneousRepository.Save();
            }

            return new ServiceMessage();
        }
        #endregion
        #region -- Service Private Methods --
        private ExperienceDetail ConvertExperienceObject(VMExperienceOperation obj)
        {
            ExperienceDetail dbExperiencedetail = new ExperienceDetail();
            dbExperiencedetail.ExpID = obj.ExpID;
            dbExperiencedetail.PostionTitle = obj.PositionTitle;
            dbExperiencedetail.EmployerName = obj.EmployerName;
            dbExperiencedetail.JobTitle = obj.JobTitle;
            dbExperiencedetail.StartDate = obj.StartDate;
            dbExperiencedetail.EndDate = obj.EndDate;
            dbExperiencedetail.CurrentlyWorking = obj.CurrentlyWorking;
            dbExperiencedetail.CityID = obj.CityID;
            dbExperiencedetail.Address = obj.Address;
            dbExperiencedetail.Responsibility1 = obj.Responsibility1;
            dbExperiencedetail.Responsibility2 = obj.Responsibility2;
            dbExperiencedetail.Responsibility3 = obj.Responsibility3;
            dbExperiencedetail.CandidateID = obj.CandidateID;
            dbExperiencedetail.IndustryID = obj.IndustryID;
            if (obj.ContactEmployerYes == "YES")
            {

                dbExperiencedetail.ContactEmployer = true;
            }
            else
            {
                dbExperiencedetail.ContactEmployer = false;
            }
            dbExperiencedetail.AreaofInterest = obj.AreaofInterest;
            dbExperiencedetail.ReasonOfLeaving = obj.ReasonOfLeaving;
            dbExperiencedetail.SupervisorName = obj.SupervisorName;
            dbExperiencedetail.CareerLevelID = obj.CareerLevelID;
            dbExperiencedetail.CountryID = obj.CountryID;
            dbExperiencedetail.OtherCity = obj.OtherCityName;
            return dbExperiencedetail;
        }
        #endregion
    }
}

