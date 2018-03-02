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
        IRepository<JobDetail> OpenJobRepository;
        IRepository<CandidateJob> AppliedJobRepository;
        public JobService(IUnitOfWork unitOfWork,IRepository<JobDetail> openJobRepository, IRepository<V_AppliedJob> jobRepository, IRepository<CandidateJob> appliedJobRepository)
        {
            UnitOfWork = unitOfWork;
            JobRepository = jobRepository;
            OpenJobRepository = openJobRepository;
            AppliedJobRepository = appliedJobRepository;
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

        public VMOpenJobIndex GetJobDetail(int JobID, V_UserCandidate LoggedInUser)
        {
            Expression<Func<CandidateJob, bool>> SpecificClient2 = c => c.CandidateID == LoggedInUser.CandidateID;
            List<CandidateJob> dbAppliedJobs = AppliedJobRepository.FindBy(SpecificClient2);
            JobDetail dbJobDetail = OpenJobRepository.GetSingle(JobID);
            VMOpenJobIndex vmJobDetail = new VMOpenJobIndex();
            vmJobDetail.JobID = dbJobDetail.JobID;
            vmJobDetail.JobTitle = dbJobDetail.JobTitle;
            vmJobDetail.CompanyName = dbJobDetail.CompanyName;
            vmJobDetail.LocName = dbJobDetail.LocName;
            vmJobDetail.Description = dbJobDetail.Description;
            vmJobDetail.Resposibilties = dbJobDetail.Resposibilties;
            vmJobDetail.QualificationReq = dbJobDetail.QualificationReq;
            vmJobDetail.SkillReq = dbJobDetail.SkillReq;
            if (dbAppliedJobs.Where(aa => aa.JobID == dbJobDetail.JobID).Count() > 0)
                vmJobDetail.IsApplied = true;
            else
                vmJobDetail.IsApplied = false;
            return vmJobDetail;
        }

        public List<VMOpenJobIndex> GetOpenJob(V_UserCandidate LoggedInUser)
        {
            Expression<Func<JobDetail, bool>> SpecificClient = c => c.DeadlineDate >=DateTime.Today;
            List<JobDetail> dbAllOpenJobs = OpenJobRepository.FindBy(SpecificClient);
            Expression<Func<CandidateJob, bool>> SpecificClient2 = c => c.CandidateID == LoggedInUser.CandidateID;
            List<CandidateJob> dbAppliedJobs = AppliedJobRepository.FindBy(SpecificClient2);
            List<VMOpenJobIndex> vmVAppliedJobs = new List<VMOpenJobIndex>();
            foreach (var dbVAppliedJob in dbAllOpenJobs)
            {
               
                VMOpenJobIndex vmVAppliedJob = new VMOpenJobIndex();
                vmVAppliedJob.JobID = dbVAppliedJob.JobID;
                vmVAppliedJob.JobTitle = dbVAppliedJob.JobTitle;
                vmVAppliedJob.LocName = dbVAppliedJob.LocName;
                vmVAppliedJob.CompanyName = dbVAppliedJob.CompanyName;
                vmVAppliedJob.Description = dbVAppliedJob.Description;
                if (dbAppliedJobs.Where(aa => aa.JobID == dbVAppliedJob.JobID).Count() > 0)
                    vmVAppliedJob.IsApplied = true;
                else
                    vmVAppliedJob.IsApplied = false;
                    vmVAppliedJobs.Add(vmVAppliedJob);
            }
            return vmVAppliedJobs.OrderByDescending(aa => aa.JobID).ToList();

        }
        #endregion
        #region -- Service Private Methods --
        #endregion
    }
}
