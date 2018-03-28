using RMSCORE.EF;
using RMSCORE.Models.Main;
using RMSCORE.Models.Operation;
using RMSSERVICES.Education;
using RMSSERVICES.Generic;
using RMSSERVICES.PersonalDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Mime;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace RMSAPPLICATION.Controllers
{
    public class EduDetailController : Controller
    {
        #region -- Controller Initialization --
        //IEntityService<V_Candidate_EduDetail> EduDetailEntityService;
        IEduDetailService EduDetailService;
        IDDService DDService;
        // Controller Constructor
        public EduDetailController(IEduDetailService edudetailService, IDDService ddService)
        {
            DDService = ddService;
            EduDetailService = edudetailService;
        }
        #endregion
        #region -- Controller ActionResults  --
        // GET: EduDetail
        public ActionResult Index()
        {
            V_UserCandidate vmf = Session["LoggedInUser"] as V_UserCandidate;
            int cid = vmf.CandidateID;
            List<VMEduDetailIndex> vmlist = EduDetailService.GetIndex(cid);
            return View(vmlist);
        }
        #region -- Controller Main View Actions  --
        [HttpGet]
        public ActionResult Create()
        {
            V_UserCandidate vmf = Session["LoggedInUser"] as V_UserCandidate;
            int cid = vmf.CandidateID;
            VMEduDetailOperation obj = EduDetailService.GetCreate(cid);
            CreateHelper(obj);
            return View(obj);
        }
        [HttpPost]
        public ActionResult Create(VMEduDetailOperation obj)
        {
            V_UserCandidate vmf = Session["LoggedInUser"] as V_UserCandidate;
            if (obj.DegreeID == 0 || obj.DegreeName == "")
                ModelState.AddModelError("DegreeID", "Select Degree title");
            if (obj.StartDate == null)
                ModelState.AddModelError("StartDate", "StartDate cannot be empty");
            if (obj.EndDate == null)
                ModelState.AddModelError("EndDate", "EndDate cannot be empty");
            if (obj.ObtainedMark == null)
                ModelState.AddModelError("ObtainedMark", "ObtainedMark cannot be empty");
            if (obj.TotalMark == null)
                ModelState.AddModelError("TotalMark", "TotalMark cannot be empty");
            if (obj.Percentage == null)
                ModelState.AddModelError("Percentage", "Percentage cannot be empty");
            if (ModelState.IsValid)
            {
                if (vmf.UserStage == 3)
                    vmf.UserStage = 4;
                EduDetailService.PostCreate(obj, vmf);
                Session["LoggedInUser"] = vmf;
                Session["ProfileStage"] = vmf.UserStage;
                return Json("OK", JsonRequestBehavior.AllowGet);
            }
            CreateHelper(obj);
            return PartialView(obj);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            VMEduDetailOperation obj = EduDetailService.GetEdit(id);
            EditHelper(obj);
            return PartialView(obj);
        }
        [HttpPost]
        public ActionResult Edit(VMEduDetailOperation obj)
        {
            if (ModelState.IsValid)
            {
                EduDetailService.PostEdit(obj);
            }
            EditHelper(obj);
            return PartialView(obj);
        }
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            VMEduDetailOperation vmOperation = EduDetailService.GetDelete((int)id);
            return View(vmOperation);
        }
        [HttpPost]
        public ActionResult Delete(VMEduDetailOperation obj)
        {
            EduDetailService.PostDelete(obj);
            return PartialView(obj);
        }
        #endregion
        #endregion
        #region -- Controller Private  Methods--
        private void CreateHelper(VMEduDetailOperation obj)
        {
            //List<EduDegreeLevel> dbEdulevels = DDService.GetEduLevel().ToList().OrderBy(aa => aa.DegreeLevel).ToList();
            //dbEdulevels.Insert(0, new EduDegreeLevel { DLevelID = 0, DegreeLevel = "" });
            ViewBag.DegreeID = new SelectList(DDService.GetEduLevel().ToList().OrderBy(aa => aa.DLevelID).ToList(), "DLevelID", "DegreeLevel", obj.DegreeID);
            //List<EduInstitute> dbInstitutes = DDService.GetInstitute().ToList().OrderBy(aa => aa.InstituteName).ToList();
            //dbInstitutes.Insert(0, new EduInstitute { InstituteID = 0, InstituteName = "" });
            ViewBag.InstitutionID = new SelectList(DDService.GetInstitute().ToList().OrderBy(aa => aa.InstituteID).ToList(), "InstituteID", "InstituteName", obj.InstitutionID);
        }
        private void EditHelper(VMEduDetailOperation obj)
        {
            ViewBag.DegreeID = new SelectList(DDService.GetEduLevel().ToList().OrderBy(aa => aa.DLevelID).ToList(), "DLevelID", "DegreeLevel", obj.DegreeID);
            ViewBag.InstitutionID = new SelectList(DDService.GetInstitute().ToList().OrderBy(aa => aa.InstituteID).ToList(), "InstituteID", "InstituteName", obj.InstitutionID);
        }
        #endregion
    }
}