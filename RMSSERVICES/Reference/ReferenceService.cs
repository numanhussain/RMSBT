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
        IRepository<User> UserRepository;
        public ReferenceService(IUnitOfWork unitOfWork, IRepository<V_Candidate_Reference> referencedetailRepository, IRepository<ReferenceDetail> referenceRepository, IRepository<Candidate> candidateRepository, IRepository<User> userRepository)
        {
            UnitOfWork = unitOfWork;
            CandidateRepository = candidateRepository;
            ReferenceDetailRepository = referencedetailRepository;
            ReferenceRepository = referenceRepository;
            UserRepository = userRepository;
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
                vmReferenceDetail.HowLongKnown = dbReferenceDetail.HowLongKnown;
                vmReferenceDetail.Organization = dbReferenceDetail.Organization;
                vmReferenceDetails.Add(vmReferenceDetail);
            }
            return vmReferenceDetails.OrderByDescending(aa => aa.RefID).ToList();
        }
        public VMReferenceOperation GetCreate(int id)
        {
            Expression<Func<ReferenceDetail, bool>> SpecificClient = c => c.CandidateID == id;
            List<ReferenceDetail> dbVReferenceDetails = ReferenceRepository.FindBy(SpecificClient);
            List<VMReferenceOperation> vmReferenceDetails = new List<VMReferenceOperation>();
            VMReferenceOperation vmReference = new VMReferenceOperation();
            if (ReferenceRepository.FindBy(SpecificClient).Count() > 0)
            {
                for (int i = 0; i <= dbVReferenceDetails.Count(); i++)
                {
                    if (i == 0)
                    {
                        vmReference.CandidateID = id;
                        vmReference.RefID1 = dbVReferenceDetails[0].RefID;
                        vmReference.RefName1 = dbVReferenceDetails[0].RefName;
                        vmReference.SalutationID1 = dbVReferenceDetails[0].SalutationID;
                        vmReference.RefDesignation1 = dbVReferenceDetails[0].RefDesignation;
                        vmReference.RefEmail1 = dbVReferenceDetails[0].RefEmail;
                        vmReference.Organization1 = dbVReferenceDetails[0].Organization;
                        vmReference.RefContact1 = dbVReferenceDetails[0].RefContact;
                        vmReference.HowLongKnown1 = dbVReferenceDetails[0].HowLongKnown;
                    }
                    if (i == 1)
                    {
                        vmReference.CandidateID = id;
                        vmReference.RefID2 = dbVReferenceDetails[1].RefID;
                        vmReference.SalutationID2 = dbVReferenceDetails[1].SalutationID;
                        vmReference.RefName2 = dbVReferenceDetails[1].RefName;
                        vmReference.RefEmail2 = dbVReferenceDetails[1].RefEmail;
                        vmReference.RefDesignation2 = dbVReferenceDetails[1].RefDesignation;
                        vmReference.Organization2 = dbVReferenceDetails[1].Organization;
                        vmReference.RefContact2 = dbVReferenceDetails[1].RefContact;
                        vmReference.HowLongKnown2 = dbVReferenceDetails[1].HowLongKnown;
                    }
                }
            }
            return vmReference;
        }
        public ServiceMessage PostCreate(VMReferenceOperation obj, V_UserCandidate LoggedInUser)
        {
            Expression<Func<User, bool>> SpecificEntries = c => c.UserID == LoggedInUser.UserID;
            List<User> images = UserRepository.FindBy(SpecificEntries);
            User image = images.First();
            image.UserStage = LoggedInUser.UserStage;
            UserRepository.Edit(image);
            UserRepository.Save();
            Expression<Func<ReferenceDetail, bool>> SpecificClient = c => c.CandidateID == obj.CandidateID;
            ReferenceDetail dbReferenceDetail = new ReferenceDetail();
            if (ReferenceRepository.FindBy(SpecificClient).Count() > 0)
            {
                dbReferenceDetail = ConvertReference1Object(obj);
                dbReferenceDetail.CandidateID = LoggedInUser.CandidateID;
                ReferenceRepository.Edit(dbReferenceDetail);
                ReferenceRepository.Save();
                dbReferenceDetail = ConvertReference2Object(obj);
                dbReferenceDetail.CandidateID = LoggedInUser.CandidateID;
                ReferenceRepository.Edit(dbReferenceDetail);
                ReferenceRepository.Save();
            }
            else
            {
                dbReferenceDetail = ConvertReference1Object(obj);
                dbReferenceDetail.CandidateID = LoggedInUser.CandidateID;
                dbReferenceDetail = ReferenceRepository.Add(dbReferenceDetail);
                ReferenceRepository.Save();
                dbReferenceDetail = ConvertReference2Object(obj);
                dbReferenceDetail.CandidateID = LoggedInUser.CandidateID;
                dbReferenceDetail = ReferenceRepository.Add(dbReferenceDetail);
                ReferenceRepository.Save();
            }
            return new ServiceMessage();
        }
        public VMReferenceOperation GetEdit(int id)
        {
            ReferenceDetail dbReferenceDetail = ReferenceRepository.GetSingle(id);
            VMReferenceOperation vmReferenceDetail = new VMReferenceOperation();
            vmReferenceDetail.RefID1 = dbReferenceDetail.RefID;
            vmReferenceDetail.RefName1 = dbReferenceDetail.RefName;
            vmReferenceDetail.RefDesignation1 = dbReferenceDetail.RefDesignation;
            vmReferenceDetail.RefContact1 = dbReferenceDetail.RefContact;
            vmReferenceDetail.RefEmail1 = dbReferenceDetail.RefEmail;
            vmReferenceDetail.CandidateID = dbReferenceDetail.CandidateID;
            vmReferenceDetail.HowLongKnown1 = dbReferenceDetail.HowLongKnown;
            vmReferenceDetail.Organization1 = dbReferenceDetail.Organization;
            return vmReferenceDetail;
        }
        public ServiceMessage PostEdit(VMReferenceOperation obj)
        {
            ReferenceDetail dbReferenceDetail = new ReferenceDetail();
            dbReferenceDetail = ConvertReference1Object(obj);
            ReferenceRepository.Edit(dbReferenceDetail);
            ReferenceRepository.Save();
            dbReferenceDetail = ConvertReference2Object(obj);
            ReferenceRepository.Edit(dbReferenceDetail);
            ReferenceRepository.Save();
            return new ServiceMessage();
        }
        public VMReferenceOperation GetDelete(int? id)
        {
            VMReferenceOperation obj = new VMReferenceOperation();
            Expression<Func<ReferenceDetail, bool>> TotalReferences = c => c.RefID == id;
            ReferenceDetail dbReferenceDetail = ReferenceRepository.GetSingle((int)id);
            obj.RefID1 = dbReferenceDetail.RefID;
            obj.RefName1 = dbReferenceDetail.RefName;
            obj.RefDesignation1 = dbReferenceDetail.RefDesignation;
            obj.RefContact1 = dbReferenceDetail.RefContact;
            obj.RefEmail1 = dbReferenceDetail.RefEmail;
            obj.CandidateID = dbReferenceDetail.CandidateID;
            obj.HowLongKnown1 = dbReferenceDetail.HowLongKnown;
            obj.Organization1 = dbReferenceDetail.Organization;
            return obj;
        }
        public ServiceMessage PostDelete(VMReferenceOperation obj)
        {
            Expression<Func<ReferenceDetail, bool>> TotalReference = c => c.RefID == obj.RefID1;
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
        private ReferenceDetail ConvertReference1Object(VMReferenceOperation obj)
        {
            ReferenceDetail dbReferenceDetail = new ReferenceDetail();
            dbReferenceDetail.RefID = obj.RefID1;
            dbReferenceDetail.SalutationID = obj.SalutationID1;
            dbReferenceDetail.RefName = obj.RefName1;
            dbReferenceDetail.RefDesignation = obj.RefDesignation1;
            dbReferenceDetail.RefContact = obj.RefContact1;
            dbReferenceDetail.RefEmail = obj.RefEmail1;
            dbReferenceDetail.HowLongKnown = obj.HowLongKnown1;
            dbReferenceDetail.Organization = obj.Organization1;
            return dbReferenceDetail;
        }
        private ReferenceDetail ConvertReference2Object(VMReferenceOperation obj)
        {
            ReferenceDetail dbReferenceDetail = new ReferenceDetail();
            dbReferenceDetail.RefID = obj.RefID2;
            dbReferenceDetail.SalutationID = obj.SalutationID2;
            dbReferenceDetail.RefName = obj.RefName2;
            dbReferenceDetail.RefDesignation = obj.RefDesignation2;
            dbReferenceDetail.RefContact = obj.RefContact2;
            dbReferenceDetail.RefEmail = obj.RefEmail2;
            dbReferenceDetail.HowLongKnown = obj.HowLongKnown2;
            dbReferenceDetail.Organization = obj.Organization2;
            return dbReferenceDetail;
        }
        #endregion
    }
}
