using RMSCORE.EF;
using RMSREPO.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMSSERVICES.Job
{
    public class JobService : IJobService
    {
        #region -- Service Variables --
        IUnitOfWork UnitOfWork;
        IRepository<V_AppliedJob> JobRepository;
        public JobService(IUnitOfWork unitOfWork, IRepository<V_AppliedJob> jobRepository)
        {
            UnitOfWork = unitOfWork;
            JobRepository = jobRepository;
        }
        #endregion
        #region -- Service Interface Implementation --
        public List<V_AppliedJob> GetAppliedJob(int cid)
        {
            return JobRepository.GetAll();
        }
        #endregion
        #region -- Service Private Methods --
        #endregion
    }
}
