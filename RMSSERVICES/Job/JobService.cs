using RMSCORE.EF;
using RMSCORE.Models.Main;
using RMSREPO.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
        public List<VMAppliedJobIndex> GetAppliedJob(int cid)
        {
            Expression<Func<V_AppliedJob, bool>> SpecificClient = c => c.CandidateID == cid;
            List<V_AppliedJob> dbVAppliedJobs = JobRepository.FindBy(SpecificClient);
            List<VMAppliedJobIndex> vmVAppliedJobs = new List<VMAppliedJobIndex>();
            foreach (var dbVAppliedJob in dbVAppliedJobs)
            {
                VMAppliedJobIndex vmVAppliedJob = new VMAppliedJobIndex();
                vmVAppliedJob.CJobID = dbVAppliedJob.CJobID;
                vmVAppliedJob.CandidateID = dbVAppliedJob.CandidateID;
                vmVAppliedJob.CName = dbVAppliedJob.CName;
                vmVAppliedJob.CJobDate = dbVAppliedJob.CJobDate;
                vmVAppliedJob.JobID = dbVAppliedJob.JobID;
                vmVAppliedJob.JobTitle = dbVAppliedJob.JobTitle;
                vmVAppliedJobs.Add(vmVAppliedJob);
            }
            return vmVAppliedJobs.OrderByDescending(aa=>aa.CJobID).ToList();
        }
        #endregion
        #region -- Service Private Methods --
        #endregion
    }
}
