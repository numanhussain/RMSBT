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
        IRepository<V_JobDetail> OpenJobRepository;
        IRepository<CandidateJob> AppliedJobRepository;
        IRepository<Candidate> CandidateProfileReporsitory;
        IRepository<EduDetail> EduDetailReporsitory;
        IRepository<ExperienceDetail> ExperienceDetailReporsitory;
        IRepository<CompensationDetail> CompensationDetailReporsitory;
        IRepository<MiscellaneousDetail> MiscellaneousDetailReporsitory;
        IRepository<CandidateStrength> CandidateStrengthReporsitory;
        public JobService(IUnitOfWork unitOfWork, IRepository<V_JobDetail> openJobRepository, IRepository<V_AppliedJob> jobRepository,
            IRepository<CandidateJob> appliedJobRepository, IRepository<Candidate> candidateProfileReporsitory,
        IRepository<EduDetail> eduDetailReporsitory, IRepository<ExperienceDetail> experienceDetailReporsitory,
        IRepository<CompensationDetail> compensationDetailReporsitory,
        IRepository<MiscellaneousDetail> miscellaneousDetailReporsitory,
        IRepository<CandidateStrength> candidateStrengthReporsitory)
        {
            UnitOfWork = unitOfWork;
            JobRepository = jobRepository;
            OpenJobRepository = openJobRepository;
            AppliedJobRepository = appliedJobRepository;
            CandidateProfileReporsitory = candidateProfileReporsitory;
            EduDetailReporsitory = eduDetailReporsitory;
            ExperienceDetailReporsitory = experienceDetailReporsitory;
            CompensationDetailReporsitory = compensationDetailReporsitory;
            MiscellaneousDetailReporsitory = miscellaneousDetailReporsitory;
            CandidateStrengthReporsitory = candidateStrengthReporsitory;
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
            return vmVAppliedJobs.OrderByDescending(aa => aa.CJobID).ToList();
        }

        public VMOpenJobIndex GetJobDetail(int JobID, V_UserCandidate LoggedInUser)
        {
            Expression<Func<CandidateJob, bool>> SpecificClient2 = c => c.CandidateID == LoggedInUser.CandidateID;
            List<CandidateJob> dbAppliedJobs = AppliedJobRepository.FindBy(SpecificClient2);
            V_JobDetail dbJobDetail = OpenJobRepository.GetSingle(JobID);
            VMOpenJobIndex vmJobDetail = new VMOpenJobIndex();
            vmJobDetail.JobID = dbJobDetail.JobID;
            vmJobDetail.JobTitle = dbJobDetail.JobTitle;
            vmJobDetail.CompanyName = dbJobDetail.CompanyName;
            vmJobDetail.LocID = dbJobDetail.LocID;
            vmJobDetail.CatagoryID = dbJobDetail.CatagoryID;
            vmJobDetail.CityID = dbJobDetail.CityID;
            vmJobDetail.DepatmentName = dbJobDetail.DepatmentName;
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

        public string CheckForProfileCompletion(V_UserCandidate LoggedInUser)
        {
            string message = "";
            Expression<Func<MiscellaneousDetail, bool>> SpecificClient4 = c => c.CandidateID == LoggedInUser.CandidateID;
            if (MiscellaneousDetailReporsitory.FindBy(SpecificClient4).Count == 0)
            {
                message = "Kindly enter your miscellaneous details in profile";
            }
            Expression<Func<CompensationDetail, bool>> SpecificClient3 = c => c.CandidateID == LoggedInUser.CandidateID;
            if (CompensationDetailReporsitory.FindBy(SpecificClient3).Count == 0)
            {
                message = "Kindly enter your compensation details in profile";
            }
            Expression<Func<ExperienceDetail, bool>> SpecificClient2 = c => c.CandidateID == LoggedInUser.CandidateID;
            if (ExperienceDetailReporsitory.FindBy(SpecificClient2).Count == 0)
            {
                message = "Kindly enter your experience details in profile";
            }
            Expression<Func<EduDetail, bool>> SpecificClient1 = c => c.CandidateID == LoggedInUser.CandidateID;
            if (EduDetailReporsitory.FindBy(SpecificClient1).Count == 0)
            {
                message = "Kindly enter your educations in profile";
            }
            Expression<Func<Candidate, bool>> SpecificClient = c => c.CandidateID == LoggedInUser.CandidateID;
            Candidate candidate = CandidateProfileReporsitory.GetSingle(LoggedInUser.CandidateID);
            if (candidate.CName == null || candidate.CName == "")
            {
                message = "Kindly enter your personal details in profile";
            }
            return message;
        }

        public VMOpenJobIndex GetJobDetailIndex(int JobID)
        {
            V_JobDetail dbJobDetail = OpenJobRepository.GetSingle(JobID);
            VMOpenJobIndex vmJobDetail = new VMOpenJobIndex();
            vmJobDetail.JobID = dbJobDetail.JobID;
            vmJobDetail.JobTitle = dbJobDetail.JobTitle;
            vmJobDetail.CompanyName = dbJobDetail.CompanyName;
            vmJobDetail.LocID = dbJobDetail.LocID;
            vmJobDetail.LocName = dbJobDetail.LocName;
            vmJobDetail.CityID = dbJobDetail.CityID;
            vmJobDetail.CityName = dbJobDetail.CityName;
            vmJobDetail.Description = dbJobDetail.Description;
            vmJobDetail.Resposibilties = dbJobDetail.Resposibilties;
            vmJobDetail.QualificationReq = dbJobDetail.QualificationReq;
            vmJobDetail.SkillReq = dbJobDetail.SkillReq;
            return vmJobDetail;
        }
        //public List<VMOpenJobIndex> GetOpenJob(V_UserCandidate LoggedInUser)
        //{
        //    Expression<Func<JobDetail, bool>> SpecificClient = c => c.DeadlineDate >= DateTime.Today;
        //    List<JobDetail> dbAllOpenJobs = OpenJobRepository.FindBy(SpecificClient);
        //    Expression<Func<CandidateJob, bool>> SpecificClient2 = c => c.CandidateID == LoggedInUser.CandidateID;
        //    List<CandidateJob> dbAppliedJobs = AppliedJobRepository.FindBy(SpecificClient2);
        //    List<VMOpenJobIndex> vmVAppliedJobs = new List<VMOpenJobIndex>();
        //    foreach (var dbVAppliedJob in dbAllOpenJobs)
        //    {

        //        VMOpenJobIndex vmVAppliedJob = new VMOpenJobIndex();
        //        vmVAppliedJob.JobID = dbVAppliedJob.JobID;
        //        vmVAppliedJob.JobTitle = dbVAppliedJob.JobTitle;
        //        vmVAppliedJob.LocName = dbVAppliedJob.LocName;
        //        vmVAppliedJob.CompanyName = dbVAppliedJob.CompanyName;
        //        vmVAppliedJob.Description = dbVAppliedJob.Description;
        //        if (dbAppliedJobs.Where(aa => aa.JobID == dbVAppliedJob.JobID).Count() > 0)
        //            vmVAppliedJob.IsApplied = true;
        //        else
        //            vmVAppliedJob.IsApplied = false;
        //        vmVAppliedJobs.Add(vmVAppliedJob);
        //    }
        //    return vmVAppliedJobs.OrderByDescending(aa => aa.JobID).ToList();

        //}
        public List<VMOpenJobIndex> GetOpenJobIndex()
        {
            Expression<Func<V_JobDetail, bool>> SpecificClient = c => c.DeadlineDate >= DateTime.Today;
            List<V_JobDetail> dbAllOpenJobs = OpenJobRepository.FindBy(SpecificClient);
            List<VMOpenJobIndex> vmVAppliedJobs = new List<VMOpenJobIndex>();
            foreach (var dbVAppliedJob in dbAllOpenJobs)
            {

                VMOpenJobIndex vmVAppliedJob = new VMOpenJobIndex();
                vmVAppliedJob.JobID = dbVAppliedJob.JobID;
                vmVAppliedJob.JobTitle = dbVAppliedJob.JobTitle;
                vmVAppliedJob.LocID = dbVAppliedJob.LocID;
                vmVAppliedJob.LocName = dbVAppliedJob.LocName;
                vmVAppliedJob.CompanyName = dbVAppliedJob.CompanyName;
                vmVAppliedJob.Description = dbVAppliedJob.Description;
                vmVAppliedJobs.Add(vmVAppliedJob);
            }
            return vmVAppliedJobs.OrderByDescending(aa => aa.CreatedDate).ToList();

        }
        public List<VMOpenJobIndex> JobIndex()
        {
            List<V_JobDetail> dbOpenJobs = OpenJobRepository.GetAll();
            List<VMOpenJobIndex> vmOpenJobs = new List<VMOpenJobIndex>();
            foreach (var dbOpenJob in dbOpenJobs)
            {
                VMOpenJobIndex vmOpenJobIndex = new VMOpenJobIndex();
                vmOpenJobIndex.JobID = dbOpenJob.JobID;
                vmOpenJobIndex.JobTitle = dbOpenJob.JobTitle;
                vmOpenJobIndex.Description = dbOpenJob.Description;
                vmOpenJobIndex.LocID = dbOpenJob.LocID;
                vmOpenJobIndex.LocName = dbOpenJob.LocName;
                vmOpenJobIndex.CityName = dbOpenJob.CityName;
                vmOpenJobIndex.CreatedDate = dbOpenJob.CreatedDate;
                vmOpenJobIndex.CompanyName = dbOpenJob.CompanyName;
                vmOpenJobIndex.Experience = dbOpenJob.Experience;
                vmOpenJobIndex.QualificationReq = dbOpenJob.QualificationReq;
                vmOpenJobIndex.Resposibilties = dbOpenJob.Resposibilties;
                vmOpenJobIndex.CatagoryID = dbOpenJob.CatagoryID;
                vmOpenJobIndex.CatName = dbOpenJob.CatName;
                vmOpenJobIndex.Status = dbOpenJob.Status;
                vmOpenJobIndex.DepatmentName = dbOpenJob.DepatmentName;
                vmOpenJobIndex.SkillReq = dbOpenJob.SkillReq;

                vmOpenJobIndex.DeadlineDate = dbOpenJob.DeadlineDate;
                vmOpenJobs.Add(vmOpenJobIndex);
            }
            return vmOpenJobs.OrderByDescending(aa => aa.CreatedDate).ToList();
        }
        #endregion
        #region -- Service Private Methods --
        #endregion
    }
}
