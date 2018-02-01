using RMSCORE.EF;
using RMSCORE.Models.Operation;
using RMSREPO.Generic;
using RMSSERVICES.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RMSSERVICES.Miscellaneous
{
   public class MiscellaneousService:IMiscellaneousService
    {
        #region -- Service Variables --
        IUnitOfWork UnitOfWork;
        IRepository<MiscellaneousDetail> MiscellaneousRepository;
        IRepository<Candidate> CandidateRepository;
        public MiscellaneousService(IUnitOfWork unitOfWork, IRepository<Candidate> candidateRepository, IRepository<MiscellaneousDetail> miscellaneousRepository)
        {
            UnitOfWork = unitOfWork;
            MiscellaneousRepository = miscellaneousRepository;
            CandidateRepository = candidateRepository;
        }
        #endregion
        #region -- Service Interface Implementation --
        public MiscellaneousDetail GetCreate(long id)
        {
            Expression<Func<MiscellaneousDetail, bool>> SpecificClient = c => c.CandidateID == id;
            MiscellaneousDetail dbMiscellaneous = MiscellaneousRepository.FindBy(SpecificClient).First();
            return dbMiscellaneous;
        }
        public ServiceMessage PostCreate(MiscellaneousDetail obj)
        {
            MiscellaneousDetail dbMiscellaneous = new MiscellaneousDetail();
            dbMiscellaneous = ConvertMiscellaneousObject(obj);
            MiscellaneousRepository.Edit(dbMiscellaneous);
            MiscellaneousRepository.Save();
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
            return dbMiscellaneous;
        }
        #endregion
    }
}
