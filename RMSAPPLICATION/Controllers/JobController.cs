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
using System.Linq.Expressions;

namespace RMSAPPLICATION.Controllers
{
    public class JobController : Controller
    {
        IEntityService<JobDetail> JobEntityService;
        IEntityService<CandidateJob> JobApplyService;
        IEntityService<Location> LocationService;
        IEntityService<Catagory> CatagoryService;
        IEntityService<V_AppliedJob> VJobApplyService;
        IJobService JobService;
        public JobController(IEntityService<JobDetail> jobEntityService,
        IEntityService<V_AppliedJob> vJobApplyService, IEntityService<CandidateJob> jobApplyService, IJobService jobService, IEntityService<Location> locationService, IEntityService<Catagory> catagoryService)
        {
            JobEntityService = jobEntityService;
            JobService = jobService;
            JobApplyService = jobApplyService;
            VJobApplyService = vJobApplyService;
            LocationService = locationService;
            CatagoryService = catagoryService;
        }
        // GET: Job
        [HttpGet]
        public ActionResult Index()
        {
            VMJobPortalIndex vm = new VMJobPortalIndex();
            vm.LocationList = GetLocationList(LocationService.GetIndex().Select(aa => aa.LocName).Distinct().ToList());
            vm.CatagoryList = GetCatagoryList(CatagoryService.GetIndex().Select(aa => aa.CatName).Distinct().ToList());
            return View(vm);
        }
        [HttpPost]
        public ActionResult Index(VMJobPortalIndex obj, string[] SelectedIds, string[] SelectedCatagoryIds)
        {
            List<VMOpenJobIndex> vmlist = JobService.JobIndex();
            List<VMOpenJobIndex> vmAllJobList = JobService.JobIndex();
            List<VMOpenJobIndex> vmTempList = new List<VMOpenJobIndex>();
            if (obj.FilterBox != null)
            {
                vmAllJobList = vmAllJobList.Where(aa => aa.JobTitle.Contains(obj.FilterBox)).ToList();
            }
            if (SelectedIds != null)
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
            if (SelectedCatagoryIds != null)
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
            return PartialView("OpenJob", vmAllJobList);
        }

        private List<CustomModel> GetCatagoryList(List<string> list)
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
            V_UserCandidate vmf = Session["LoggedInUser"] as V_UserCandidate;
            List<VMOpenJobIndex> vmAllJobList = JobService.GetOpenJob(vmf);
            return View(vmAllJobList);
        }
        public ActionResult OpenJobIndex()
        {
            List<VMOpenJobIndex> vmAllJobList = JobService.GetOpenJobIndex();
            return View(vmAllJobList);
        }
        public ActionResult JobDetail(int JobID)
        {
            V_UserCandidate vmf = Session["LoggedInUser"] as V_UserCandidate;
            VMOpenJobIndex vmJobDetail = JobService.GetJobDetail(JobID, vmf);
            return View(vmJobDetail);
        }
        public ActionResult JobDetailIndex(int JobID)
        {
            VMOpenJobIndex vmJobDetail = JobService.GetJobDetailIndex(JobID);
            return View(vmJobDetail);
        }
        [HttpGet]
        public ActionResult JobApply()
        {
            V_UserCandidate vmf = Session["LoggedInUser"] as V_UserCandidate;
            int cid = vmf.CandidateID;
            List<VMAppliedJobIndex> vmlist = JobService.GetAppliedJob(cid);
            return View(vmlist);
        }
        [HttpPost]
        public ActionResult JobApply(CandidateJob obj, int JobID)
        {
            V_UserCandidate vmf = Session["LoggedInUser"] as V_UserCandidate;
            if (ModelState.IsValid == true)
            {
                if (vmf.UserStage == "SignUp")
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                }
                else if (vmf.UserStage == "ProfileCompleted")
                {
                    int cid = vmf.CandidateID;
                    CandidateJob dbCandidateJob = new CandidateJob();
                    dbCandidateJob.CandidateID = cid;
                    dbCandidateJob.JobID = JobID;
                    dbCandidateJob.CJobDate = DateTime.Now;
                    JobApplyService.PostCreate(dbCandidateJob);
                    return Json("OK", JsonRequestBehavior.AllowGet);

                }
            }
            return View(obj);
        }
    }
}