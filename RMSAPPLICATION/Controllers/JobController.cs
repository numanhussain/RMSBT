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
        IDDService DDService;
        public JobController(IEntityService<JobDetail> jobEntityService,
        IEntityService<V_AppliedJob> vJobApplyService, IEntityService<CandidateJob> jobApplyService, IDDService ddService, IJobService jobService,
        IEntityService<Location> locationService, IEntityService<Catagory> catagoryService)
        {
            JobEntityService = jobEntityService;
            JobService = jobService;
            JobApplyService = jobApplyService;
            VJobApplyService = vJobApplyService;
            LocationService = locationService;
            CatagoryService = catagoryService;
            DDService = ddService;
        }
        // GET: Job
        [HttpGet]
        public ActionResult Index()
        {
            V_UserCandidate vmf = Session["LoggedInUser"] as V_UserCandidate;
            Session["ProfileStage"] = vmf.UserStage;
            List<VMOpenJobIndex> vm = JobService.JobIndex();
            List<Location> dbLocations = DDService.GetLocationList().ToList().OrderBy(aa => aa.LocName).ToList();
            dbLocations.Insert(0, new Location { PLocationID = 0, LocName = "All Locations" });
            List<Catagory> dbCatagories = DDService.GetCatagoryList().ToList().OrderBy(aa => aa.CatName).ToList();
            dbCatagories.Insert(0, new Catagory { PCatagoryID = 0, CatName = "All Catagories" });
            ViewBag.LocationID = new SelectList(dbLocations.ToList().OrderBy(aa => aa.PLocationID).ToList(), "PLocationID", "LocName");
            ViewBag.CatagoryID = new SelectList(dbCatagories.ToList().OrderBy(aa => aa.PCatagoryID).ToList(), "PCatagoryID", "CatName");
            //ViewBag.LocationID = GetLocationList(LocationService.GetIndex().Select(aa => aa.LocName).Distinct().ToList());
            //ViewBag.CatagoryID = GetCatagoryList(CatagoryService.GetIndex().Select(aa => aa.CatName).Distinct().ToList());
            return View(vm);
        }
        [HttpPost]
        public ActionResult IndexSubmit(int? LocationID, int? CatagoryID, string FilterBox)
        {
            List<VMOpenJobIndex> vmAllJobList = JobService.JobIndex();
            if (FilterBox != "")
                vmAllJobList = vmAllJobList.Where(aa => aa.JobTitle == FilterBox).ToList();
            if (LocationID > 0)
            {
                vmAllJobList = vmAllJobList.Where(aa => aa.LocID == LocationID).ToList();
            }
            if (CatagoryID > 0)
            {
                vmAllJobList = vmAllJobList.Where(aa => aa.CatagoryID == CatagoryID).ToList();
            }
            List<Location> dbLocations = DDService.GetLocationList().ToList().OrderBy(aa => aa.LocName).ToList();
            dbLocations.Insert(0, new Location { PLocationID = 0, LocName = "All Locations" });
            List<Catagory> dbCatagories = DDService.GetCatagoryList().ToList().OrderBy(aa => aa.CatName).ToList();
            dbCatagories.Insert(0, new Catagory { PCatagoryID = 0, CatName = "All Catagories" });
            ViewBag.LocationID = new SelectList(dbLocations.ToList().OrderBy(aa => aa.PLocationID).ToList(), "PLocationID", "LocName", LocationID);
            ViewBag.CatagoryID = new SelectList(dbCatagories.ToList().OrderBy(aa => aa.PCatagoryID).ToList(), "PCatagoryID", "CatName", CatagoryID);
            //ViewBag.LocationID = GetLocationList(LocationService.GetIndex().Select(aa => aa.LocName).Distinct().ToList());
            //ViewBag.CatagoryID = GetCatagoryList(CatagoryService.GetIndex().Select(aa => aa.CatName).Distinct().ToList());
            return View("Index", vmAllJobList);
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
                    vmTempList.AddRange(vmAllJobList.Where(aa => aa.CatName == item).ToList());
                }
                vmAllJobList = vmTempList.ToList();
            }
            else
                vmTempList = vmAllJobList.ToList();
            vmTempList.Clear();
            return PartialView("OpenJob", vmAllJobList);
        }

        //private List<CustomModel> GetCatagoryList(List<string> list)
        //{
        //    List<CustomModel> cmList = new List<CustomModel>();
        //    foreach (var item in list)
        //    {
        //        CustomModel cm = new CustomModel();
        //        cm.ID = item;
        //        cm.Name = item;
        //        cm.IsSelected = false;
        //        cmList.Add(cm);
        //    }
        //    return cmList;
        //}

        //private List<CustomModel> GetLocationList(List<string> list)
        //{
        //    List<CustomModel> cmList = new List<CustomModel>();
        //    foreach (var item in list)
        //    {
        //        CustomModel cm = new CustomModel();
        //        cm.ID = item;
        //        cm.Name = item;
        //        cm.IsSelected = false;
        //        cmList.Add(cm);
        //    }
        //    return cmList;
        //}

        //public ActionResult OpenJob()
        //{
        //    V_UserCandidate vmf = Session["LoggedInUser"] as V_UserCandidate;
        //    List<VMOpenJobIndex> vmAllJobList = JobService.GetOpenJob(vmf);
        //    return View(vmAllJobList);
        //}
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
            int cid = vmf.CandidateID;
            JobDetail dbJob = JobEntityService.GetEdit(JobID);
            string Message = JobService.CheckForProfileCompletion(vmf);
            if (Message == "")
            {
                CandidateJob dbCandidateJob = new CandidateJob();
                dbCandidateJob.CandidateID = cid;
                dbCandidateJob.JobID = JobID;
                dbCandidateJob.CJobDate = DateTime.Now;
                JobApplyService.PostCreate(dbCandidateJob);
                EmailGenerate.SendEmail(vmf.Email, "", "<html><head><meta content=\"text/html; charset = utf - 8\" /></head><body><p>From Bestway Career Portal:</p><p>Dear Candidate " + vmf.CName + "  : </p><div><p>Thank you for your keen interest in applying for the position:" + dbJob.JobTitle + " .</p>We have received your application for this post. </p><p>Our Talent Acquisition Team will meticulously evaluate your profile in line with the requirements of the post you have applied for. Since, we receive a large number of applications for different positions, it is not possible to communicate with every candidate individually. Therefore, only the short-listed candidates will be contacted by the Talent Acquisition Team for interview and other assessments as deemed appropriate. </p></div><div><p>You can check your status from our career portal.</p></div>" + "<div><p>Wish you best of luck in your quest to find a suitable career in accordance with your professional and academic qualifications.</p></div><div>Best regards:</div><div>Bestway Talent Acquisition Team</div><div>Bestway Cement Limited</div></body></html>", "Job Application");
                return Json("OK", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(Message, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult DeclarationStatement(int? JobID)
        {
            VMOpenJobIndex vmJobDetail = JobService.GetJobDetailIndex((int)JobID);
            return View(vmJobDetail);
        }

      
    }
}