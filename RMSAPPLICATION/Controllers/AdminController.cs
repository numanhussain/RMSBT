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
        IUserService UserService;
        // Controller Constructor
        public AdminController(IEntityService<V_CandidateDetail> v_CandidateDetailEntityService, IUserService userService)

        {
            V_CandidateDetailEntityService = v_CandidateDetailEntityService;
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
                vmCandidateDetails.AddRange(UserService.GetAllIndex().ToList());
                //vmCandidateDetails = UserService.GetAllIndex();
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
            //int pageSize = 10;
            //int pageNumber = (page ?? 1);
            return View(vmCandidateDetails/*.ToPagedList(pageNumber, pageSize)*/);
        }
        public FilePathResult OpenCV(string fileName)
        {
            V_UserCandidate vmf = Session["LoggedInUser"] as V_UserCandidate;
            var checkextension = Path.GetExtension(vmf.CVName).ToLower();
            if (checkextension == ".pdf")
            {
                return new FilePathResult(string.Format(@"~\UploadFiles\" + fileName.ToString() + ".pdf"), "application/pdf");
            }
            else if (checkextension == ".docx")
            {
                return new FilePathResult(string.Format(@"~\UploadFiles\" + fileName.ToString() + ".docx"), "application/docx");
            }
            else if (checkextension == ".doc")
            {
                return new FilePathResult(string.Format(@"~\UploadFiles\" + fileName.ToString() + ".doc"), "application/doc");
            }
            else
            {
                return new FilePathResult(string.Format(@"~\UploadFiles\" + fileName.ToString() + ".jpg"), "application/jpg");
            }
        }
        #region-- 
        #endregion
    }
}