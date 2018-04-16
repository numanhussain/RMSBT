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
            if (obj.DegreeLevelID == null)
            {
                obj.DegreeLevelID = DDService.GetEduLevel().ToList().OrderBy(aa => aa.DLevelID).First().DLevelID;
            }
            if (obj.DegreeTypeID == null)
            {
                ViewBag.DegreeTypeID = DDService.GetEduDegreeType().ToList().OrderBy(aa => aa.EduDegreeLevelID);
            }
            CreateHelper(obj);
            return View(obj);
        }
        [HttpPost]
        public ActionResult Create(VMEduDetailOperation obj)
        {
            V_UserCandidate vmf = Session["LoggedInUser"] as V_UserCandidate;
            if (obj.DegreeLevelID == 0)
            {
                ModelState.AddModelError("DegreeLevelID", "Mandatory !!");
            }
            else
            {
                if (obj.DegreeTitle == null)
                    ModelState.AddModelError("DegreeTitle", "Mandatory !!");
                if (obj.DegreeLevelID == 1 && obj.InstitutionID == 0|| obj.DegreeLevelID == 2 && obj.InstitutionID == 0|| obj.DegreeLevelID == 3 && obj.InstitutionID == 0)
                    obj.InstitutionID = null;
                if (obj.DegreeLevelID == 4 && obj.InstitutionID == 0 || obj.DegreeLevelID == 5 && obj.InstitutionID == 0 || obj.DegreeLevelID == 6 && obj.InstitutionID == 0)
                    ModelState.AddModelError("InstitutionID", "Mandatory !!");
                if (obj.StartDate == null)
                    ModelState.AddModelError("StartDate", "Mandatory !!");
                if (obj.MajorSubject == null || obj.MajorSubject=="")
                    ModelState.AddModelError("MajorSubject", "Mandatory !!");
                if (obj.StartDate != null)
                {
                    if (obj.StartDate >= obj.EndDate)
                        ModelState.AddModelError("StartDate", "Must be smaller than end date!!");
                }
                if (obj.InstitutionID == 0)
                    ModelState.AddModelError("InstitutionID", "Mandatory !!");

                if (obj.EndDate == null)
                    ModelState.AddModelError("EndDate", "Mandatory !!");
                if (obj.ObtainedMark == null)
                    ModelState.AddModelError("ObtainedMark", "Mandatory !!");
                if (obj.TotalMark == null)
                    ModelState.AddModelError("TotalMark", "Mandatory !!");
                if (obj.Percentage == null)
                    ModelState.AddModelError("Percentage", "Mandatory !!");
                if (obj.DegreeLevelID == 4 && obj.CGPA == null || obj.DegreeLevelID == 5 && obj.CGPA == null || obj.DegreeLevelID == 6 && obj.CGPA == null)
                    ModelState.AddModelError("CGPA", "Mandatory !!");
                if (obj.InstitutionID == 150 && obj.OtherInstitute == null)
                    ModelState.AddModelError("OtherInstitute", "Mandatory !!");
                if (ModelState.IsValid)
                {
                    if (vmf.UserStage == 3)
                        vmf.UserStage = 4;
                    EduDetailService.PostCreate(obj, vmf);
                    Session["LoggedInUser"] = vmf;
                    Session["ProfileStage"] = vmf.UserStage;
                    return Json("OK", JsonRequestBehavior.AllowGet);
                }
            }
            CreateHelper(obj);
            return PartialView("Create", obj);
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
            V_UserCandidate vmf = Session["LoggedInUser"] as V_UserCandidate;
            if (obj.DegreeLevelID == 0)
            {
                ModelState.AddModelError("DegreeLevelID", "Mandatory !!");
            }
            if (obj.DegreeLevelID == 4 || obj.DegreeLevelID == 5 || obj.DegreeLevelID == 6 && obj.CGPA == null || obj.CGPA == "")
                ModelState.AddModelError("CGPA", "This is mandatory field");
            if (obj.DegreeTitle == null)
                ModelState.AddModelError("DegreeTitle", "Mandatory !!");
            if (obj.DegreeLevelID == 1 && obj.InstitutionID == 0 || obj.DegreeLevelID == 2 && obj.InstitutionID == 0 || obj.DegreeLevelID == 3 && obj.InstitutionID == 0)
                obj.InstitutionID = null;
            if (obj.DegreeLevelID == 4 && obj.InstitutionID == 0 || obj.DegreeLevelID == 5 && obj.InstitutionID == 0 || obj.DegreeLevelID == 6 && obj.InstitutionID == 0)
                ModelState.AddModelError("InstitutionID", "Mandatory !!");
            if (obj.StartDate == null)
                ModelState.AddModelError("StartDate", "Mandatory !!");
            if (obj.MajorSubject == null || obj.MajorSubject == "")
                ModelState.AddModelError("MajorSubject", "Mandatory !!");
            if (obj.StartDate != null)
            {
                if (obj.StartDate >= obj.EndDate)
                    ModelState.AddModelError("StartDate", "Must be smaller than end date!!");
            }
            if (obj.InstitutionID == 0)
                ModelState.AddModelError("InstitutionID", "Mandatory !!");

            if (obj.EndDate == null)
                ModelState.AddModelError("EndDate", "Mandatory !!");
            if (obj.ObtainedMark == null)
                ModelState.AddModelError("ObtainedMark", "Mandatory !!");
            if (obj.TotalMark == null)
                ModelState.AddModelError("TotalMark", "Mandatory !!");
            if (obj.Percentage == null)
                ModelState.AddModelError("Percentage", "Mandatory !!");
            if (obj.DegreeLevelID == 4 && obj.CGPA == null || obj.DegreeLevelID == 5 && obj.CGPA == null || obj.DegreeLevelID == 6 && obj.CGPA == null)
                ModelState.AddModelError("CGPA", "Mandatory !!");
            if (obj.InstitutionID == 150 && obj.OtherInstitute == null)
                ModelState.AddModelError("OtherInstitute", "Mandatory !!");

            if (ModelState.IsValid)
            {
                EduDetailService.PostEdit(obj);
                return Json("OK", JsonRequestBehavior.AllowGet);
            }
            EditHelper(obj);
            return PartialView("Edit", obj);
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
            List<EduDegreeLevel> dbEdulevels = DDService.GetEduLevel().ToList().OrderBy(aa => aa.DLevelID).ToList();
            dbEdulevels.Insert(0, new EduDegreeLevel { DLevelID = 0, DegreeLevel = "All" });
            ViewBag.DegreeLevelID = new SelectList(dbEdulevels.ToList().OrderBy(aa => aa.DLevelID).ToList(), "DLevelID", "DegreeLevel");
            List<EduInstitute> dbInstitutes = DDService.GetInstitute().ToList().OrderBy(aa => aa.InstituteID).ToList();
            dbInstitutes.Insert(0, new EduInstitute { InstituteID = 0, InstituteName = "All" });
            ViewBag.InstitutionID = new SelectList(dbInstitutes.ToList().OrderBy(aa => aa.InstituteID).ToList(), "InstituteID", "InstituteName");
            List<EduDegreeType> dbDegreeTypes = DDService.GetEduDegreeType().ToList().OrderBy(aa => aa.EduTypeID).ToList();
            dbDegreeTypes.Insert(0, new EduDegreeType { EduTypeID = 0, EduTypeName = "All" });
            ViewBag.DegreeTypeID = new SelectList(dbDegreeTypes.ToList().OrderBy(aa => aa.EduTypeID).ToList(), "EduTypeID", "EduTypeName");
        }
        private void EditHelper(VMEduDetailOperation obj)
        {
            ViewBag.DegreeLevelID = new SelectList(DDService.GetEduLevel().ToList().OrderBy(aa => aa.DLevelID).ToList(), "DLevelID", "DegreeLevel", obj.DegreeLevelID);
            ViewBag.InstitutionID = new SelectList(DDService.GetInstitute().ToList().OrderBy(aa => aa.InstituteID).ToList(), "InstituteID", "InstituteName", obj.InstitutionID);
        }
        public ActionResult DegreeTypeList(string ID)
        {
            int Code = Convert.ToInt32(ID);
            var states = DDService.GetEduDegreeType().Where(aa => aa.EduDegreeLevelID == Code).OrderBy(aa => aa.EduDegreeLevelID);
            if (HttpContext.Request.IsAjaxRequest())
                return Json(new SelectList(
                                states.ToArray(),
                                "EduDegreeLevelID",
                                "EduTypeName")
                            , JsonRequestBehavior.AllowGet);

            return RedirectToAction("Index");
        }
        #endregion
    }
}