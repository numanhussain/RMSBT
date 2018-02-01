using RMSCORE.EF;
using RMSCORE.Models.Main;
using RMSSERVICES.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RMSCORE.Models.Helper;

namespace RMSAPPLICATION.Controllers
{
    public class JobController : Controller
    {
        IEntityService<Job> JobService;
        IEntityService<CandidateJob> JobApplyService;
        public JobController(IEntityService<Job> jobService,IEntityService<CandidateJob> jobApplyService)
        {
            JobService = jobService;
            JobApplyService = jobApplyService;
        }
        // GET: Job
        [HttpGet]
        public ActionResult Index()
        {
            VMJobPortalIndex vm = new VMJobPortalIndex();
            vm.LocationList = GetLocationList(JobService.GetIndex().Select(aa => aa.LocName).Distinct().ToList());
            vm.CatagoryList = GetCatagoryList(JobService.GetIndex().Select(aa => aa.CatagoryName).Distinct().ToList());
            return View(vm);
        }
        [HttpPost]
        public ActionResult Index(VMJobPortalIndex obj,string[] SelectedIds,string[] SelectedCatagoryIds)
        {
            List<Job> vmAllJobList =JobService.GetIndex();
            List<Job> vmTempList = new List<Job>();
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
            List<Job> vmAllJobList = JobService.GetIndex();
            return View(vmAllJobList);
        }
        public ActionResult JobDetail(int JobID)
        {
            Job vmallJobDetail = JobService.GetEdit(JobID);
            return View(vmallJobDetail);
        }
        [HttpPost]
        public ActionResult JobApply(CandidateJob obj,int JobID)
        {
            int cid = AssistantService.LoggedInUserID;
            CandidateJob dbCandidateJob = new CandidateJob();
            dbCandidateJob.CandidateID = cid;
            dbCandidateJob.JobID = JobID;
            dbCandidateJob.CJobDate = DateTime.Now;
            JobApplyService.PostCreate(dbCandidateJob);
            return Json("OK",JsonRequestBehavior.AllowGet);
        }
    }
}