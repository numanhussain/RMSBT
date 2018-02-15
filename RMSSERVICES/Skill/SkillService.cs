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
using System.Text;
using System.Threading.Tasks;

namespace RMSSERVICES.Skill
{
    public class SkillService : ISkillService
    {
        IUnitOfWork UnitOfWork;
        IRepository<V_Candidate_Skills> SkillDetailRepository;
        IRepository<SkillDetail> SkillRepository;
        IRepository<Candidate> CandidateRepository;
        public SkillService(IUnitOfWork unitOfWork, IRepository<V_Candidate_Skills> skilldetailRepository, IRepository<SkillDetail> skillRepository, IRepository<Candidate> candidateRepository)
        {
            UnitOfWork = unitOfWork;
            CandidateRepository = candidateRepository;
            SkillDetailRepository = skilldetailRepository;
            SkillRepository = skillRepository;
        }
        public List<VMSkillIndex> GetIndex(int cid)
        {
            Expression<Func<V_Candidate_Skills, bool>> SpecificClient = c => c.CandidateID == cid;
            List<V_Candidate_Skills> dbVSkillDetails = SkillDetailRepository.FindBy(SpecificClient);
            List<VMSkillIndex> vmSkillDetails = new List<VMSkillIndex>();
            foreach (var dbSkillDetail in dbVSkillDetails)
            {
                VMSkillIndex vmSkillDetail = new VMSkillIndex();
                vmSkillDetail.SkillID = dbSkillDetail.SkillID;
                vmSkillDetail.SkillTitle = dbSkillDetail.Title;
                vmSkillDetail.SkillLevelID = dbSkillDetail.SLevelID;
                vmSkillDetail.skillLevelName = dbSkillDetail.SkillLevelName;
                vmSkillDetail.Description = dbSkillDetail.Description;
                vmSkillDetail.CandidateID = dbSkillDetail.CandidateID;
                vmSkillDetails.Add(vmSkillDetail);
            }
            return vmSkillDetails.OrderByDescending(aa => aa.SkillID).ToList();
        }
        public VMSkillOperation GetCreate(int id)
        {
            VMSkillOperation vmSkillDetail = new VMSkillOperation();
            vmSkillDetail.CandidateID = id;
            return vmSkillDetail;
        }
        public ServiceMessage PostCreate(VMSkillOperation obj)
        {
            SkillDetail dbSkillDetail = new SkillDetail();
            dbSkillDetail.CandidateID = obj.CandidateID;
            dbSkillDetail = ConvertSkillObject(obj);
            dbSkillDetail = SkillRepository.Add(dbSkillDetail);
            SkillRepository.Save();
            return new ServiceMessage();
        }
        public VMSkillOperation GetEdit(int id)
        {
            SkillDetail dbSkillDetail = SkillRepository.GetSingle(id);
            VMSkillOperation vmSkillDetail = new VMSkillOperation();
            vmSkillDetail.SkillID = dbSkillDetail.SkillID;
            vmSkillDetail.SkillTitle = dbSkillDetail.Title;
            vmSkillDetail.SLevelID = dbSkillDetail.SLevelID;
            vmSkillDetail.Description = dbSkillDetail.Description;
            vmSkillDetail.CandidateID = dbSkillDetail.CandidateID;      
            return vmSkillDetail;
        }
        public ServiceMessage PostEdit(VMSkillOperation obj)
        {
            SkillDetail dbSkillDetail = new SkillDetail();
            dbSkillDetail = ConvertSkillObject(obj);
            SkillRepository.Edit(dbSkillDetail);
            SkillRepository.Save();
            return new ServiceMessage();
        }
        public VMSkillOperation GetDelete(int? id)
        {
            VMSkillOperation obj = new VMSkillOperation();
            Expression<Func<SkillDetail, bool>> TotalVersionRows = c => c.SkillID == id;
            SkillDetail dbEduDetail = SkillRepository.GetSingle((int)id);
            obj.SkillID = dbEduDetail.SkillID;
            obj.CandidateID = dbEduDetail.CandidateID;
            obj.SkillTitle = dbEduDetail.Title;
            obj.SLevelID = dbEduDetail.SLevelID;
            obj.Description = dbEduDetail.Description;
            return obj;
        }
        public ServiceMessage PostDelete(VMSkillOperation vmOperation)
        {
            Expression<Func<SkillDetail, bool>> TotalSkills = c => c.SkillID == vmOperation.SkillID;
            List<SkillDetail> dbSkillDetails = SkillRepository.FindBy(TotalSkills);
            foreach (var dbSkillDetail in dbSkillDetails)
            {
                SkillRepository.Delete(dbSkillDetail);
                SkillRepository.Save();
            }
            return new ServiceMessage();
        }
        private SkillDetail ConvertSkillObject(VMSkillOperation obj)
        {
            SkillDetail dbEdudetail = new SkillDetail();
            dbEdudetail.SkillID = obj.SkillID;
            dbEdudetail.Title = obj.SkillTitle;
            dbEdudetail.SLevelID = obj.SLevelID;
            dbEdudetail.Description = obj.Description;
            dbEdudetail.CandidateID = obj.CandidateID;
            return dbEdudetail;
        }
    }
}
