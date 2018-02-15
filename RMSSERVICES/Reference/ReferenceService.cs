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

namespace RMSSERVICES.Reference
{
    public class ReferenceService : IReferenceService
    {
        #region -- Service Variables --
        IUnitOfWork UnitOfWork;
        IRepository<V_Candidate_Reference> ReferenceDetailRepository;
        IRepository<ReferenceDetail> ReferenceRepository;
        IRepository<Candidate> CandidateRepository;
        public ReferenceService(IUnitOfWork unitOfWork, IRepository<V_Candidate_Reference> referencedetailRepository, IRepository<ReferenceDetail> referenceRepository, IRepository<Candidate> candidateRepository)
        {
            UnitOfWork = unitOfWork;
            CandidateRepository = candidateRepository;
            ReferenceDetailRepository = referencedetailRepository;
            ReferenceRepository = referenceRepository;
        }
        #endregion
        #region -- Service Interface Implementation --
        public List<VMReferenceIndex> GetIndex(int cid)
        {
            Expression<Func<V_Candidate_Reference, bool>> SpecificCandidateReference = c => c.CandidateID == cid;
            List<V_Candidate_Reference> dbVReferenceDetails = ReferenceDetailRepository.FindBy(SpecificCandidateReference);
            List<VMReferenceIndex> vmReferenceDetails = new List<VMReferenceIndex>();
            foreach (var dbReferenceDetail in dbVReferenceDetails)
            {
                VMReferenceIndex vmReferenceDetail = new VMReferenceIndex();
                vmReferenceDetail.RefID = dbReferenceDetail.RefID;
                vmReferenceDetail.RefName = dbReferenceDetail.RefName;
                vmReferenceDetail.RefDesignation = dbReferenceDetail.RefDesignation;
                vmReferenceDetail.RefContact = dbReferenceDetail.RefContact;
                vmReferenceDetail.RefEmail = dbReferenceDetail.RefEmail;
                vmReferenceDetail.CandidateID = dbReferenceDetail.CandidateID;
                vmReferenceDetails.Add(vmReferenceDetail);
            }
            return vmReferenceDetails.OrderByDescending(aa => aa.RefID).ToList();
        }
        public VMReferenceOperation GetCreate(int id)
        {
            VMReferenceOperation vmReferenceDetail = new VMReferenceOperation();
            vmReferenceDetail.CandidateID = id;
            return vmReferenceDetail;
        }
        public ServiceMessage PostCreate(VMReferenceOperation obj)
        {
            ReferenceDetail dbReferenceDetail = new ReferenceDetail();
            dbReferenceDetail.CandidateID = obj.CandidateID;
            dbReferenceDetail = ConvertExperienceObject(obj);
            dbReferenceDetail = ReferenceRepository.Add(dbReferenceDetail);
            ReferenceRepository.Save();
            return new ServiceMessage();
        }
        public VMReferenceOperation GetEdit(int id)
        {
            ReferenceDetail dbReferenceDetail = ReferenceRepository.GetSingle(id);
            VMReferenceOperation vmReferenceDetail = new VMReferenceOperation();
            vmReferenceDetail.RefID = dbReferenceDetail.RefID;
            vmReferenceDetail.RefName = dbReferenceDetail.RefName;
            vmReferenceDetail.RefDesignation = dbReferenceDetail.RefDesignation;
            vmReferenceDetail.RefContact = dbReferenceDetail.RefContact;
            vmReferenceDetail.RefEmail = dbReferenceDetail.RefEmail;
            vmReferenceDetail.CandidateID = dbReferenceDetail.CandidateID;
            return vmReferenceDetail;
        }
        public ServiceMessage PostEdit(VMReferenceOperation obj)
        {
            ReferenceDetail dbExperienceDetail = new ReferenceDetail();
            dbExperienceDetail = ConvertExperienceObject(obj);
            ReferenceRepository.Edit(dbExperienceDetail);
            ReferenceRepository.Save();
            return new ServiceMessage();
        }
        public VMReferenceOperation GetDelete(int? id)
        {
            VMReferenceOperation obj = new VMReferenceOperation();
            Expression<Func<ReferenceDetail, bool>> TotalReferences = c => c.RefID == id;
            ReferenceDetail dbReferenceDetail = ReferenceRepository.GetSingle((int)id);
            obj.RefID = dbReferenceDetail.RefID;
            obj.RefName = dbReferenceDetail.RefName;
            obj.RefDesignation = dbReferenceDetail.RefDesignation;
            obj.RefContact = dbReferenceDetail.RefContact;
            obj.RefEmail = dbReferenceDetail.RefEmail;
            obj.CandidateID = dbReferenceDetail.CandidateID;
            return obj;
        }
        public ServiceMessage PostDelete(VMReferenceOperation obj)
        {
            Expression<Func<ReferenceDetail, bool>> TotalReference = c => c.RefID == obj.RefID;
            List<ReferenceDetail> dbReferenceDetails = ReferenceRepository.FindBy(TotalReference);
            foreach (var dbReferenceDetail in dbReferenceDetails)
            {
                ReferenceRepository.Delete(dbReferenceDetail);
                ReferenceRepository.Save();
            }
            return new ServiceMessage();
        }
        public ValidationMessage ValidateNewEntry(VMReferenceOperation obj)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region -- Service Private Methods --
        private ReferenceDetail ConvertExperienceObject(VMReferenceOperation obj)
        {
            ReferenceDetail dbReferenceDetail = new ReferenceDetail();
            dbReferenceDetail.RefID = obj.RefID;
            dbReferenceDetail.RefName = obj.RefName;
            dbReferenceDetail.RefDesignation = obj.RefDesignation;
            dbReferenceDetail.RefContact = obj.RefContact;
            dbReferenceDetail.RefEmail = obj.RefEmail;
            dbReferenceDetail.CandidateID = obj.CandidateID;
            return dbReferenceDetail;
        }
        #endregion
    }
}
