using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMSCORE.EF;
using RMSCORE.Models.Helper;
using RMSSERVICES.Generic;
using RMSREPO.Generic;
using System.Linq.Expressions;
using RMSCORE.Models.Main;
using RMSCORE.Models.Operation;

namespace RMSSERVICES.Education
{
    public class EduDetailService : IEduDetailService
    {
        IUnitOfWork UnitOfWork;
        IRepository<V_Candidate_EduDetail> EduDetailRepository;
        IRepository<EduDetail> EducationDetailRepository;
        IRepository<Candidate> CandidateRepository;
        IRepository<User> UserRepository;
        public EduDetailService(IUnitOfWork unitOfWork, IRepository<EduDetail> educationDetailRepository, IRepository<V_Candidate_EduDetail> edudetailRepository, IRepository<Candidate> candidateRepository, IRepository<User> userRepository)
        {
            UnitOfWork = unitOfWork;
            CandidateRepository = candidateRepository;
            EduDetailRepository = edudetailRepository;
            EducationDetailRepository = educationDetailRepository;
            UserRepository = userRepository;
        }
        public List<VMEduDetailIndex> GetIndex(int cid)
        {
            Expression<Func<V_Candidate_EduDetail, bool>> SpecificClient = c => c.CandidateID == cid;
            List<V_Candidate_EduDetail> dbVEduDetails = EduDetailRepository.FindBy(SpecificClient);
            List<VMEduDetailIndex> vmEduDetails = new List<VMEduDetailIndex>();
            foreach (var dbEduDetail in dbVEduDetails)
            {
                VMEduDetailIndex vmEduDetail = new VMEduDetailIndex();
                vmEduDetail.CandidateID = dbEduDetail.CandidateID;
                vmEduDetail.DegreeLevelID = dbEduDetail.DegreeLevelID;
                vmEduDetail.DegreeLevelName = dbEduDetail.DegreeLevel;
                vmEduDetail.ObtainedMark = dbEduDetail.ObtainedMark;
                vmEduDetail.TotalMark = dbEduDetail.TotalMark;
                vmEduDetail.Percentage = dbEduDetail.Percentage;
                vmEduDetail.EndDate = dbEduDetail.EndDate;
                vmEduDetail.InstitutionName = dbEduDetail.InstituteName;
                vmEduDetail.EduID = dbEduDetail.EduID;
                vmEduDetail.DegreeTitle = dbEduDetail.DegreeTitle;
                vmEduDetail.OtherInstitute = dbEduDetail.OtherInstitute;
                vmEduDetail.DegreeTypeID = dbEduDetail.DegreeTypeID;
                vmEduDetail.DegreeTypeName = dbEduDetail.EduTypeName;
                vmEduDetail.InProgress = dbEduDetail.InProgress;
                vmEduDetail.CGPA = dbEduDetail.CGPA;

                vmEduDetails.Add(vmEduDetail);
            }
            return vmEduDetails.OrderByDescending(aa => aa.EndDate).ToList();
        }
        public VMEduDetailOperation GetCreate(int id)
        {
            VMEduDetailOperation vmEduDetail = new VMEduDetailOperation();
            vmEduDetail.CandidateID = id;
            return vmEduDetail;
        }
        public ServiceMessage PostCreate(VMEduDetailOperation obj,V_UserCandidate LoggedInUser)
        {
            Expression<Func<User, bool>> SpecificEntries = c => c.UserID == LoggedInUser.UserID;
            List<User> images = UserRepository.FindBy(SpecificEntries);
            User image = images.First();
            image.UserStage = LoggedInUser.UserStage;
            UserRepository.Edit(image);
            UserRepository.Save();
            EduDetail dbEduDetail = new EduDetail();
            dbEduDetail.CandidateID = obj.CandidateID;
            dbEduDetail = ConvertEducationObject(obj);
            dbEduDetail = EducationDetailRepository.Add(dbEduDetail);
            EducationDetailRepository.Save();
            return new ServiceMessage();
        }
        public VMEduDetailOperation GetEdit(int id)
        {
            EduDetail dbEduDetail = EducationDetailRepository.GetSingle(id);
            VMEduDetailOperation vmEduDetail = new VMEduDetailOperation();
            vmEduDetail.EduID = dbEduDetail.EduID;
            vmEduDetail.CandidateID = dbEduDetail.CandidateID;
            vmEduDetail.DegreeLevelID = dbEduDetail.DegreeLevelID;
            vmEduDetail.InstitutionID = dbEduDetail.Institution;
            vmEduDetail.ObtainedMark = dbEduDetail.ObtainedMark;
            vmEduDetail.TotalMark = dbEduDetail.TotalMark;
            vmEduDetail.StartDate = dbEduDetail.StartDate;
            vmEduDetail.EndDate = dbEduDetail.EndDate;
            vmEduDetail.Percentage = dbEduDetail.Percentage;
            vmEduDetail.CGPA = dbEduDetail.CGPA;
            vmEduDetail.MajorSubject = dbEduDetail.MajorSubject;
            vmEduDetail.PassingYear = dbEduDetail.PassingYear;
            vmEduDetail.DegreeTitle = dbEduDetail.DegreeTitle;
            vmEduDetail.OtherInstitute = dbEduDetail.OtherInstitute;
            vmEduDetail.InProgress = dbEduDetail.InProgress;
            vmEduDetail.DegreeTypeID = dbEduDetail.DegreeTypeID;
            vmEduDetail.DegreeLevelID = dbEduDetail.DegreeLevelID;
            vmEduDetail.OtherDegreeType = dbEduDetail.OtherDegreeType;
            return vmEduDetail;
        }
        public ServiceMessage PostEdit(VMEduDetailOperation obj)
        {
            EduDetail dbEduDetail = new EduDetail();
            dbEduDetail = ConvertEducationObject(obj);
            EducationDetailRepository.Edit(dbEduDetail);
            EducationDetailRepository.Save();
            return new ServiceMessage();
        }
        public VMEduDetailOperation GetDelete(int? id)
        {
            VMEduDetailOperation obj = new VMEduDetailOperation();
            Expression<Func<EduDetail, bool>> TotalVersionRows = c => c.EduID == id;
            EduDetail dbEduDetail = EducationDetailRepository.GetSingle((int)id);
            obj.EduID = dbEduDetail.EduID;
            obj.CandidateID = dbEduDetail.CandidateID;
            obj.DegreeLevelID = dbEduDetail.DegreeLevelID;
            obj.InstitutionID = dbEduDetail.Institution;
            obj.StartDate = dbEduDetail.StartDate;
            obj.EndDate = dbEduDetail.EndDate;
            obj.ObtainedMark = dbEduDetail.ObtainedMark;
            obj.TotalMark = dbEduDetail.TotalMark;
            obj.Percentage = dbEduDetail.Percentage;
            obj.CGPA = dbEduDetail.CGPA;
            obj.MajorSubject = dbEduDetail.MajorSubject;
            obj.PassingYear = dbEduDetail.PassingYear;
            obj.DegreeTitle = dbEduDetail.DegreeTitle;
            obj.OtherInstitute = dbEduDetail.OtherInstitute;
            obj.InProgress = dbEduDetail.InProgress;
            obj.DegreeTypeID = dbEduDetail.DegreeTypeID;
            obj.OtherDegreeType = dbEduDetail.OtherDegreeType;
            return obj;
        }
        public ServiceMessage PostDelete(VMEduDetailOperation vmOperation)
        {
            Expression<Func<EduDetail, bool>> TotalVersions = c => c.EduID == vmOperation.EduID;
            List<EduDetail> dbEduDetails = EducationDetailRepository.FindBy(TotalVersions);
            foreach (var dbEduDetail in dbEduDetails)
            {
                EducationDetailRepository.Delete(dbEduDetail);
                EducationDetailRepository.Save();
            }
            return new ServiceMessage();
        }
        private EduDetail ConvertEducationObject(VMEduDetailOperation obj)
        {
            EduDetail dbEdudetail = new EduDetail();
            dbEdudetail.CandidateID = obj.CandidateID;
            dbEdudetail.DegreeLevelID = obj.DegreeLevelID;
            dbEdudetail.Institution = obj.InstitutionID;
            dbEdudetail.EduID = obj.EduID;
            dbEdudetail.ObtainedMark = obj.ObtainedMark;
            dbEdudetail.TotalMark = obj.TotalMark;
            dbEdudetail.StartDate = obj.StartDate;
            dbEdudetail.EndDate = obj.EndDate;
            dbEdudetail.Percentage = obj.Percentage;
            dbEdudetail.CGPA = obj.CGPA;
            dbEdudetail.MajorSubject = obj.MajorSubject;
            dbEdudetail.PassingYear = obj.PassingYear;
            dbEdudetail.DegreeTitle = obj.DegreeTitle;
            dbEdudetail.OtherInstitute = obj.OtherInstitute;
            dbEdudetail.InProgress = obj.InProgress;
            dbEdudetail.DegreeTypeID = obj.DegreeTypeID;
            dbEdudetail.OtherDegreeType = obj.OtherDegreeType;
            return dbEdudetail;
        }
    }
}
