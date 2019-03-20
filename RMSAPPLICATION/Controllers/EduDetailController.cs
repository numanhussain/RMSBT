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
        IEntityService<CandidateStep> CandidateStepEntityService;
        IEduDetailService EduDetailService;
        IDDService DDService;
        // Controller Constructor
        public EduDetailController(IEduDetailService edudetailService, IDDService ddService, IEntityService<CandidateStep> candidateStepEntityService)
        {
            DDService = ddService;
            EduDetailService = edudetailService;
            CandidateStepEntityService = candidateStepEntityService;
        }
        #endregion
        #region -- Controller ActionResults  --
        // GET: EduDetail
        public ActionResult Index()
        {
            V_UserCandidate vmf = Session["LoggedInUser"] as V_UserCandidate;
            int cid = vmf.CandidateID;
            List<VMEduDetailIndex> vmlist = EduDetailService.GetIndex(cid).OrderByDescending(aa => aa.EduID).ToList();
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
                ViewBag.DegreeTypeID = DDService.GetEduDegreeType().ToList().OrderBy(aa => aa.EduTypeID);
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
                if (obj.DegreeLevelID == 11)
                {
                    if (obj.OtherDegreeLevelName == null || obj.OtherDegreeLevelName == "")
                        ModelState.AddModelError("OtherDegreeLevelName", "Mandatory");
                }
                if (obj.DegreeLevelID != 10)
                {
                    if (obj.DegreeTitle == null)
                        ModelState.AddModelError("DegreeTitle", "Mandatory ");
                }
                if (obj.DegreeTypeID == 68 || obj.DegreeTypeID == 72 || obj.DegreeTypeID == 73 || obj.DegreeTypeID == 74 || obj.DegreeTypeID == 75 || obj.DegreeTypeID == 76)
                {
                    if (obj.OtherDegreeType == null || obj.OtherDegreeType == "")
                        ModelState.AddModelError("OtherDegreeType", "Mandatory");
                }
                if (obj.DegreeTypeID == 0)
                {
                    if (obj.DegreeTypeID == null)
                        ModelState.AddModelError("DegreeTypeID", "Mandatory ");
                }
                if ((obj.DegreeLevelID == 1 || obj.DegreeLevelID == 2 || obj.DegreeLevelID == 3 || obj.DegreeLevelID == 4 || obj.DegreeLevelID == 5 || obj.DegreeLevelID == 6) && obj.InstitutionID == 0)
                    obj.InstitutionID = null;
                if ((obj.DegreeLevelID == 7 || obj.DegreeLevelID == 8 || obj.DegreeLevelID == 9 || obj.DegreeLevelID == 10) && obj.InstitutionID == 0)
                    ModelState.AddModelError("InstitutionID", "Mandatory ");
                if (obj.InProgress == true)
                {
                    obj.EndDate = null;
                    if (obj.StartDate >= DateTime.Now)
                    {
                        ModelState.AddModelError("StartDate", "Must be smaller than today's date ");
                    }
                }
                else
                {
                    if (obj.EndDate == null)
                        ModelState.AddModelError("EndDate", "Mandatory ");
                }
                if (!AssistantService.IsDateTime(Request.Form["StartDate"])) // check for valid date
                    ModelState.AddModelError("StartDate", "Invalid date");
                else
                    obj.StartDate = Convert.ToDateTime(Request.Form["StartDate"].ToString());
                if (obj.StartDate == null)
                    ModelState.AddModelError("StartDate", "Mandatory ");
                if (obj.EndDate != null)
                {
                    if (!AssistantService.IsDateTime(Request.Form["EndDate"])) // check for valid date
                        ModelState.AddModelError("EndDate", "Invalid date");
                    else
                        obj.EndDate = Convert.ToDateTime(Request.Form["EndDate"].ToString());
                }

                if (obj.MajorSubject == null || obj.MajorSubject == "")
                    ModelState.AddModelError("MajorSubject", "Mandatory");

                if (obj.StartDate != null)
                {
                    if (obj.StartDate >= obj.EndDate)
                        ModelState.AddModelError("StartDate", "Must be smaller than end date");
                }
                if (obj.StartDate != null)
                {
                    if (obj.EndDate >= DateTime.Now)
                        ModelState.AddModelError("EndDate", "Must be smaller than today's date");
                }
                if ((obj.DegreeLevelID == 1 || obj.DegreeLevelID == 2 || obj.DegreeLevelID == 3 || obj.DegreeLevelID == 4 || obj.DegreeLevelID == 5 || obj.DegreeLevelID == 6) && obj.BoardID == 0)
                {
                    ModelState.AddModelError("BoardID", "Mandatory");
                }
                if (obj.BoardID == 48)
                {
                    if (obj.OtherBoardName == null || obj.OtherBoardName == "")
                        ModelState.AddModelError("OtherBoardName", "Mandatory");
                }
                if (obj.BoardID != 48)
                {
                    obj.OtherBoardName = null;
                }
                if ((obj.DegreeLevelID == 2 || obj.DegreeLevelID == 3 || obj.DegreeLevelID == 4 || obj.DegreeLevelID == 5 || obj.DegreeLevelID == 6 || obj.DegreeLevelID == 7 || obj.DegreeLevelID == 8 || obj.DegreeLevelID == 9) && obj.EduCriteriaID == 0)
                {
                    ModelState.AddModelError("EduCriteriaID", "Mandatory");
                }
                if (obj.EduCriteriaID == 1)
                {
                    obj.CGPA = null;
                    obj.GradeName = null;
                    if (obj.TotalMark == null)
                        ModelState.AddModelError("TotalMark", "Mandatory");
                    if (obj.ObtainedMark == null)
                        ModelState.AddModelError("ObtainedMark", "Mandatory");
                    if (obj.Percentage == null || obj.Percentage == "")
                        ModelState.AddModelError("Percentage", "Mandatory");
                }
                if (obj.EduCriteriaID == 2)
                {
                    obj.TotalMark = null;
                    obj.ObtainedMark = null;
                    obj.Percentage = null;
                    obj.CGPA = null;
                    if (obj.GradeName == null || obj.GradeName == "")
                        ModelState.AddModelError("GradeName", "Mandatory");
                }
                if (obj.EduCriteriaID == 3)
                {
                    obj.TotalMark = null;
                    obj.ObtainedMark = null;
                    obj.Percentage = null;
                    obj.GradeName = null;
                    if (obj.CGPA == null)
                        ModelState.AddModelError("CGPA", "Mandatory");
                    if (obj.CGPA > 4)
                    {
                        ModelState.AddModelError("CGPA", "CGPA less than 4");
                    }
                }
                if (obj.InstitutionID == 148 && obj.OtherInstitute == null)
                    ModelState.AddModelError("OtherInstitute", "Mandatory ");
                if ((obj.DegreeTypeID == 3 || obj.DegreeTypeID == 4) && obj.GradeName == null)
                {
                    ModelState.AddModelError("GradeName", "Grade is mandatory in O-Levels/A-Levels");
                    ModelState.AddModelError("TotalMark", "Grade is mandatory in O-Levels/A-Levels");
                    ModelState.AddModelError("CGPA", "Grade is mandatory in O-Levels/A-Levels");
                }
                if (obj.ObtainedMark > obj.TotalMark)
                {
                    ModelState.AddModelError("ObtainedMark", "Mandatory ");
                }
                if (ModelState.IsValid)
                {
                    if (vmf.UserStage == 3)
                        vmf.UserStage = 4;
                    EduDetailService.PostCreate(obj, vmf);
                    CandidateStep dbtickStep = CandidateStepEntityService.GetEdit(vmf.CandidateID);
                    dbtickStep.StepTwo = true;
                    CandidateStepEntityService.PostEdit(dbtickStep);
                    vmf.StepTwo = dbtickStep.StepTwo;
                    Session["LoggedInUser"] = vmf;
                    Session["ProfileStage"] = vmf.UserStage;
                    return Json("OK", JsonRequestBehavior.AllowGet);
                }
            }
            CreateHelper(obj);
            return PartialView("Create", obj);
        }
        public ActionResult CalculatePercentage(int? TotalMark, int? ObtainedMark)
        {
            var tot = ((ObtainedMark * 100) / TotalMark).ToString();

            return Json(tot, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            VMEduDetailOperation obj = EduDetailService.GetEdit(id);
            string myDate = obj.StartDate.Value.ToString("dd/MM/yyyy");
            ViewBag.TotalMark = obj.TotalMark;
            ViewBag.obtainedMark = obj.ObtainedMark;
            ViewBag.Percentage = obj.Percentage;
            EditHelper(obj);
            return PartialView(obj);
        }
        [HttpPost]
        public ActionResult Edit(VMEduDetailOperation obj)
        {
            if (obj.DegreeLevelID == 0)
            {
                ModelState.AddModelError("DegreeLevelID", "Mandatory !!");
            }
            else
            {
                if (obj.DegreeLevelID == 11)
                {
                    if (obj.OtherDegreeLevelName == null || obj.OtherDegreeLevelName == "")
                        ModelState.AddModelError("OtherDegreeLevelName", "Mandatory");
                }
                if (obj.DegreeLevelID != 10)
                {
                    if (obj.DegreeTitle == null)
                        ModelState.AddModelError("DegreeTitle", "Mandatory ");
                }
                if (obj.DegreeTypeID == 68 || obj.DegreeTypeID == 72 || obj.DegreeTypeID == 73 || obj.DegreeTypeID == 74 || obj.DegreeTypeID == 75 || obj.DegreeTypeID == 76)
                {
                    if (obj.OtherDegreeType == null || obj.OtherDegreeType == "")
                        ModelState.AddModelError("OtherDegreeType", "Mandatory");
                }
                if (obj.DegreeTypeID == 0)
                {
                    if (obj.DegreeTypeID == null)
                        ModelState.AddModelError("DegreeTypeID", "Mandatory ");
                }
                if ((obj.DegreeLevelID == 1 || obj.DegreeLevelID == 2 || obj.DegreeLevelID == 3 || obj.DegreeLevelID == 4 || obj.DegreeLevelID == 5 || obj.DegreeLevelID == 6) && obj.InstitutionID == 0)
                    obj.InstitutionID = null;
                if ((obj.DegreeLevelID == 7 || obj.DegreeLevelID == 8 || obj.DegreeLevelID == 9 || obj.DegreeLevelID == 10) && obj.InstitutionID == 0)
                    ModelState.AddModelError("InstitutionID", "Mandatory ");
                if (obj.InProgress == true)
                {
                    obj.EndDate = null;
                    if (obj.StartDate >= DateTime.Now)
                    {
                        ModelState.AddModelError("StartDate", "Must be smaller than today's date ");
                    }
                }
                else
                {
                    if (obj.EndDate == null)
                        ModelState.AddModelError("EndDate", "Mandatory ");
                }
                if (!AssistantService.IsDateTime(Request.Form["StartDate"])) // check for valid date
                    ModelState.AddModelError("StartDate", "Invalid date");
                else
                    obj.StartDate = Convert.ToDateTime(Request.Form["StartDate"].ToString());
                if (obj.StartDate == null)
                    ModelState.AddModelError("StartDate", "Mandatory ");
                if (obj.EndDate != null)
                {
                    if (!AssistantService.IsDateTime(Request.Form["EndDate"])) // check for valid date
                        ModelState.AddModelError("EndDate", "Invalid date");
                    else
                        obj.EndDate = Convert.ToDateTime(Request.Form["EndDate"].ToString());
                }

                if (obj.MajorSubject == null || obj.MajorSubject == "")
                    ModelState.AddModelError("MajorSubject", "Mandatory");

                if (obj.StartDate != null)
                {
                    if (obj.StartDate >= obj.EndDate)
                        ModelState.AddModelError("StartDate", "Must be smaller than end date");
                }
                if (obj.StartDate != null)
                {
                    if (obj.EndDate >= DateTime.Now)
                        ModelState.AddModelError("EndDate", "Must be smaller than today's date");
                }
                if ((obj.DegreeLevelID == 1 || obj.DegreeLevelID == 2 || obj.DegreeLevelID == 3 || obj.DegreeLevelID == 4 || obj.DegreeLevelID == 5 || obj.DegreeLevelID == 6) && obj.BoardID == 0)
                {
                    ModelState.AddModelError("BoardID", "Mandatory");
                }
                if (obj.BoardID == 48)
                {
                    if (obj.OtherBoardName == null || obj.OtherBoardName == "")
                        ModelState.AddModelError("OtherBoardName", "Mandatory");
                }
                if (obj.BoardID != 48)
                {
                    obj.OtherBoardName = null;
                }
                if ((obj.DegreeLevelID == 2 || obj.DegreeLevelID == 3 || obj.DegreeLevelID == 4 || obj.DegreeLevelID == 5 || obj.DegreeLevelID == 6 || obj.DegreeLevelID == 7 || obj.DegreeLevelID == 8 || obj.DegreeLevelID == 9) && obj.EduCriteriaID == 0)
                {
                    ModelState.AddModelError("EduCriteriaID", "Mandatory");
                }
                if (obj.EduCriteriaID == 1)
                {
                    obj.CGPA = null;
                    obj.GradeName = null;
                    if (obj.TotalMark == null)
                        ModelState.AddModelError("TotalMark", "Mandatory");
                    if (obj.ObtainedMark == null)
                        ModelState.AddModelError("ObtainedMark", "Mandatory");
                    if (obj.Percentage == null || obj.Percentage == "")
                        ModelState.AddModelError("Percentage", "Mandatory");
                }
                if (obj.EduCriteriaID == 2)
                {
                    obj.TotalMark = null;
                    obj.ObtainedMark = null;
                    obj.Percentage = null;
                    obj.CGPA = null;
                    if (obj.GradeName == null || obj.GradeName == "")
                        ModelState.AddModelError("GradeName", "Mandatory");
                }
                if (obj.EduCriteriaID == 3)
                {
                    obj.TotalMark = null;
                    obj.ObtainedMark = null;
                    obj.Percentage = null;
                    obj.GradeName = null;
                    if (obj.CGPA == null)
                        ModelState.AddModelError("CGPA", "Mandatory");
                    if (obj.CGPA > 4)
                    {
                        ModelState.AddModelError("CGPA", "CGPA less than 4");
                    }
                }
                if (obj.InstitutionID == 148 && obj.OtherInstitute == null)
                    ModelState.AddModelError("OtherInstitute", "Mandatory ");
                if ((obj.DegreeTypeID == 3 || obj.DegreeTypeID == 4) && obj.GradeName == null)
                {
                    ModelState.AddModelError("GradeName", "Mandatory ");
                }
                if (obj.ObtainedMark > obj.TotalMark)
                {
                    ModelState.AddModelError("ObtainedMark", "Mandatory ");
                }
                if (ModelState.IsValid)
                {
                    EduDetailService.PostEdit(obj);
                    return Json("OK", JsonRequestBehavior.AllowGet);
                }
            }
            EditHelper(obj);
            return PartialView("Edit", obj);
        }
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            VMEduDetailOperation vmOperation = EduDetailService.GetDelete((int)id);
            ViewBag.TotalMark = vmOperation.TotalMark;
            ViewBag.obtainedMark = vmOperation.ObtainedMark;
            ViewBag.Percentage = vmOperation.Percentage;
            EditHelper(vmOperation);
            return View(vmOperation);
        }
        [HttpPost]
        public ActionResult Delete(VMEduDetailOperation obj)
        {
            EduDetailService.PostDelete(obj);
            EditHelper(obj);
            return PartialView(obj);
        }
        #endregion
        #endregion
        #region -- Controller Private  Methods--
        private void CreateHelper(VMEduDetailOperation obj)
        {
            ViewBag.DegreeLevelID = new SelectList(DDService.GetEduLevel().ToList().OrderBy(aa => aa.DLevelID).ToList(), "DLevelID", "DegreeLevel");
            ViewBag.InstitutionID = new SelectList(DDService.GetInstitute().ToList().OrderBy(aa => aa.InstituteName).ToList(), "InstituteID", "InstituteName");
            ViewBag.DegreeTypeID = new SelectList(DDService.GetEduDegreeType().ToList().OrderBy(aa => aa.EduTypeID).ToList(), "EduTypeID", "EduTypeName");
            ViewBag.BoardID = new SelectList(DDService.GetBoardList().ToList().OrderBy(aa => aa.BISEID).ToList(), "BISEID", "BiseName");
        }
        private void EditHelper(VMEduDetailOperation obj)
        {
            ViewBag.DegreeLevelID = new SelectList(DDService.GetEduLevel().ToList().OrderBy(aa => aa.DLevelID).ToList(), "DLevelID", "DegreeLevel", obj.DegreeLevelID);
            ViewBag.InstitutionID = new SelectList(DDService.GetInstitute().ToList().OrderBy(aa => aa.InstituteName).ToList(), "InstituteID", "InstituteName", obj.InstitutionID);
            ViewBag.DegreeTypeID = new SelectList(DDService.GetEduDegreeType().ToList().OrderBy(aa => aa.EduTypeID).ToList(), "EduTypeID", "EduTypeName", obj.DegreeTypeID);
            ViewBag.BoardID = new SelectList(DDService.GetBoardList().ToList().OrderBy(aa => aa.BISEID).ToList(), "BISEID", "BiseName", obj.BoardID);
        }
        public ActionResult DegreeTypeList(string ID)
        {
            int Code = Convert.ToInt32(ID);
            var states = DDService.GetEduDegreeType().Where(aa => aa.EduDegreeLevelID == Code).OrderBy(aa => aa.EduTypeName);
            if (HttpContext.Request.IsAjaxRequest())
                return Json(new SelectList(
                                states.ToArray(),
                                "EduTypeID",
                                "EduTypeName")
                            , JsonRequestBehavior.AllowGet);

            return RedirectToAction("Index");
        }
        #endregion
    }
}