using RMSCORE.EF;
using RMSCORE.Models.Main;
using RMSSERVICES.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RMSCORE.Models.Helper;
using RMSSERVICES.Job;

namespace RMSAPPLICATION.Controllers
{
    public class JobController : Controller
    {
        IEntityService<JobDetail> JobEntityService;
        IEntityService<CandidateJob> JobApplyService;
        IJobService JobService;
        public JobController(IEntityService<JobDetail> jobEntityService,IEntityService<CandidateJob> jobApplyService, IJobService jobService)
        {
            JobEntityService = jobEntityService;
            JobService = jobService;
            JobApplyService = jobApplyService;
        }
        // GET: Job
        [HttpGet]
        public ActionResult Index()
        {
            VMJobPortalIndex vm = new VMJobPortalIndex();
            vm.LocationList = GetLocationList(JobEntityService.GetIndex().Select(aa => aa.LocName).Distinct().ToList());
            vm.CatagoryList = GetCatagoryList(JobEntityService.GetIndex().Select(aa => aa.CatagoryName).Distinct().ToList());
            return View(vm);
        }
        [HttpPost]
        public ActionResult Index(VMJobPortalIndex obj,string[] SelectedIds,string[] SelectedCatagoryIds)
        {
            List<JobDetail> vmAllJobList = JobEntityService.GetIndex();
            List<JobDetail> vmTempList = new List<JobDetail>();
            if (obj.FilterBox != null)
            {
            vmAllJobList=vmAllJobList.Where(aa => aa.JobTitle.Contains(obj.FilterBox)).ToList();
            }
            if (SelectedIds!=null)
            {
                foreach (var item in SelectedIds)
                {
                    vmTempList.AddRange(vmAllJobList.Where(aa => aa.LocName == item).ToList());
                }
                vmAllJobList = vmTempList.ToList();
            }
            else
                vmTempList = vmAllJobList.ToList();
            vmTempList.Clear();
            if (SelectedCatagoryIds!=null)
            {
                foreach (var item in SelectedCatagoryIds)
                {
                    vmTempList.AddRange(vmAllJobList.Where(aa => aa.CatagoryName == item).ToList());
                }
                vmAllJobList = vmTempList.ToList();
            }
            else
                vmTempList = vmAllJobList.ToList();
            vmTempList.Clear();
            return View("OpenJob",vmAllJobList);
        }

        private List<CustomModel> GetCatagoryList(List<string> list)
        {
            List<CustomModel> cmList = new List<CustomModel>();
            foreach(var item in list)
            {
                CustomModel cm = new CustomModel();
                cm.ID = item;
                cm.Name = item;
                cm.IsSelected = false;
                cmList.Add(cm);
            }
            return cmList;
        }

        private List<CustomModel> GetLocationList(List<string> list)
        {
            List<CustomModel> cmList = new List<CustomModel>();
            foreach (var item in list)
            {
                CustomModel cm = new CustomModel();
                cm.ID = item;
                cm.Name = item;
                cm.IsSelected = false;
                cmList.Add(cm);
            }
            return cmList;
        }

        public ActionResult OpenJob()
        {
            List<JobDetail> vmAllJobList = JobEntityService.GetIndex();
            return View(vmAllJobList);
        }
        public ActionResult JobDetail(int JobID)
        {
            JobDetail vmallJobDetail = JobEntityService.GetEdit(JobID);
            return View(vmallJobDetail);
        }
        [HttpGet]
        public ActionResult JobApply()
        {
            V_UserCandidate vmf = Session["LoggedInUser"] as V_UserCandidate;
            int cid = vmf.CandidateID;
            List<V_AppliedJob> vmlist = JobService.GetAppliedJob(cid);
            return View(vmlist);
        }
        [HttpPost]
        public ActionResult JobApply(CandidateJob obj,int JobID)
        {
            V_UserCandidate vmf = Session["LoggedInUser"] as V_UserCandidate;
            int cid = vmf.CandidateID;
            CandidateJob dbCandidateJob = new CandidateJob();
            dbCandidateJob.CandidateID = cid;
            dbCandidateJob.JobID = JobID;
            dbCandidateJob.CJobDate = DateTime.Now;
            JobApplyService.PostCreate(dbCandidateJob);
            return Json("OK",JsonRequestBehavior.AllowGet);
        }
    }
}