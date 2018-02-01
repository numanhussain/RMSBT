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
        public ExperienceDetailService(IUnitOfWork unitOfWork, IRepository<V_Candidate_Exp> experiencedetailRepository, IRepository<ExperienceDetail> experienceRepository, IRepository<Candidate> candidateRepository)
        {
            UnitOfWork = unitOfWork;
            CandidateRepository = candidateRepository;
            ExperienceDetailRepository = experiencedetailRepository;
            ExperienceRepository = experienceRepository;
        }
        #endregion
        #region -- Service Interface Implementation --
        public List<VMExperienceIndex> GetIndex(long cid)
        {
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
                vmExperienceDetail.City = dbExperienceDetail.City;
                vmExperienceDetail.Address = dbExperienceDetail.Address;
                vmExperienceDetail.Description = dbExperienceDetail.Description;
                vmExperienceDetail.EmployerName = dbExperienceDetail.EmployerName;
                vmExperienceDetail.CandidateID = dbExperienceDetail.CandidateID;
                vmExperienceDetail.CandidateName = dbExperienceDetail.CName;
                vmExperienceDetail.IndustryID = dbExperienceDetail.IndustryID;
                vmExperienceDetail.ExpIndustryName = dbExperienceDetail.ExpIndustryName;
                vmExperienceDetails.Add(vmExperienceDetail);
            }
            return vmExperienceDetails.OrderByDescending(aa => aa.ExpID).ToList();
        }
        public VMExperienceOperation GetCreate(int? id)
        {
            VMExperienceOperation vmExperienceDetail = new VMExperienceOperation();
            vmExperienceDetail.CandidateID = id;
            return vmExperienceDetail;
        }
        public ServiceMessage PostCreate(VMExperienceOperation obj)
        {
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
            vmExperienceDetail.City = dbExperienceDetail.City;
            vmExperienceDetail.Address = dbExperienceDetail.Address;
            vmExperienceDetail.Description = dbExperienceDetail.Description;
            vmExperienceDetail.EmployerName = dbExperienceDetail.EmployerName;
            vmExperienceDetail.CandidateID = dbExperienceDetail.CandidateID;
            vmExperienceDetail.IndustryID = dbExperienceDetail.IndustryID;
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
            obj.City = dbExperienceDetail.City;
            obj.Address = dbExperienceDetail.Address;
            obj.Description = dbExperienceDetail.Description;
            obj.EmployerName = dbExperienceDetail.EmployerName;
            obj.CandidateID = dbExperienceDetail.CandidateID;
            obj.IndustryID = dbExperienceDetail.IndustryID;
            return obj;
        }
        public ServiceMessage PostDelete(VMExperienceOperation obj, int? id)
        {
            Expression<Func<ExperienceDetail, bool>> TotalExperience = c => c.ExpID == id;
            List<ExperienceDetail> dbExperienceDetails = ExperienceRepository.FindBy(TotalExperience);
            foreach (var dbExperienceDetail in dbExperienceDetails)
            {
                ExperienceRepository.Delete(dbExperienceDetail);
                ExperienceRepository.Save();
            }
            return new ServiceMessage();
        }
        public ValidationMessage ValidateNewEntry(VMExperienceOperation obj)
        {
            throw new NotImplementedException();
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
            dbExperiencedetail.City = obj.City;
            dbExperiencedetail.Address = obj.Address;
            dbExperiencedetail.Description = obj.Description;
            dbExperiencedetail.CandidateID = obj.CandidateID;
            dbExperiencedetail.IndustryID = obj.IndustryID;
            return dbExperiencedetail;
        }
        #endregion
    }
}
