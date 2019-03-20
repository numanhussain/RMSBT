using PagedList;
using RMSCORE.EF;
using RMSCORE.Models.Other;
using RMSSERVICES.Generic;
using RMSSERVICES.PersonalDetail;
using RMSSERVICES.UserDetail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RMSAPPLICATION.Controllers
{
    public class AdminController : Controller
    {
        #region -- Controller Initialization --
        IEntityService<V_CandidateDetail> V_CandidateDetailEntityService;
        IEntityService<V_AppliedJob> V_AppliedJobEntityService;
        IEntityService<JobDetail> JobDetailService;
        IUserService UserService;
        // Controller Constructor
        public AdminController(IEntityService<V_CandidateDetail> v_CandidateDetailEntityService, IEntityService<V_AppliedJob> v_AppliedJobEntityService, IUserService userService, IEntityService<JobDetail> jobDetailService)

        {
            V_CandidateDetailEntityService = v_CandidateDetailEntityService;
            V_AppliedJobEntityService = v_AppliedJobEntityService;
            JobDetailService = jobDetailService;
            UserService = userService;
        }
        #endregion
        // GET: Candidate
        public ActionResult Index(string searchString, string currentFilter, int? page)
        {
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;
            List<VMCandidateDetail> vmCandidateDetails = new List<VMCandidateDetail>();
            try
            {
                V_UserCandidate vmf = Session["LoggedInUser"] as V_UserCandidate;
                //vmCandidateDetails.AddRange(UserService.GetAllIndex().ToList());
                vmCandidateDetails = UserService.GetAllIndex();
                //return View(vmCandidateDetails);
            }
            catch (Exception ex)
            {

            }
            if (!String.IsNullOrEmpty(searchString))
            {
                vmCandidateDetails = vmCandidateDetails.Where(s => s.CName == searchString || s.Email == searchString || s.UserID.ToString() == searchString.ToString()).ToList();
                //vmCandidateDetails = vmCandidateDetails.Where(aa => aa.CName.ToUpper().Contains(searchString.ToUpper())).ToList();

            }
            //int pageSize = 20;
            //int pageNumber = (page ?? 1);
            return View(vmCandidateDetails/*ToPagedList(pageNumber, pageSize)*/);
        }
        public ActionResult OpenCV(string fileName)
        {
            int CandidateID = Convert.ToInt32(fileName);
            V_UserCandidate vmf = Session["LoggedInUser"] as V_UserCandidate;
            V_CandidateDetail dbCandidate = V_CandidateDetailEntityService.GetEdit(CandidateID);
            var checkextension = Path.GetExtension(dbCandidate.CVName).ToLower();
            if (checkextension == ".pdf")
            {
                var file = Server.MapPath("~/UploadFiles/" + fileName + ".pdf");
                return File(file, "application/pdf");
            }
            else if (checkextension == ".docx")
            {
                var file = Server.MapPath("~/UploadFiles/" + fileName + ".docx");
                return File(file, "application/docx");
            }
            else if (checkextension == ".doc")
            {
                var file = Server.MapPath("~/UploadFiles/" + fileName + ".doc");
                return File(file, "application/doc");
            }
            else
            {
                var file = Server.MapPath("~/UploadFiles/" + fileName + ".jpg");
                return File(file, "application/jpg");
            }
        }
        public ActionResult Download(string fileName)
        {
            var file = Server.MapPath("~/UploadFiles/" + fileName + ".pdf");
            return File(file, "application/octet-stream", fileName);
        }
        public ActionResult AppliedJobIndex(int? JobID)
        {
            if (JobID == null || JobID == 0)
            {
                V_UserCandidate vmf = Session["LoggedInUser"] as V_UserCandidate;
                List<VMAppliedJobDetail> dbVAppliedJobs = UserService.GetAppliedJobDetails(JobID, vmf);
                List<JobDetail> dbJobDetails = JobDetailService.GetIndex().ToList();
                ViewBag.JobID = new SelectList(dbJobDetails.ToList().OrderBy(aa => aa.JobID).ToList(), "JobID", "JobTitle");
                return View(dbVAppliedJobs);
            }
            else
            {
                V_UserCandidate vmf = Session["LoggedInUser"] as V_UserCandidate;
                List<VMAppliedJobDetail> dbVAppliedJobs = UserService.GetAppliedJobDetails(JobID, vmf);
                List<JobDetail> dbJobDetails = JobDetailService.GetIndex().ToList();
                ViewBag.JobID = new SelectList(dbJobDetails.ToList().OrderBy(aa => aa.JobID).ToList(), "JobID", "JobTitle", JobID);
                return View(dbVAppliedJobs);
            }
        }
        #region-- 
        #endregion
    }
}