using RMSCORE.EF;
using RMSCORE.Models.Main;
using RMSCORE.Models.Operation;
using RMSSERVICES.Education;
using RMSSERVICES.Generic;
using RMSSERVICES.PersonalDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RMSAPPLICATION.Controllers
{
    public class EduDetailController : Controller
    {
        #region -- Controller Initialization --
        //IEntityService<VMEduDetailIndex> EduDetailEntityService;
        IEduDetailService EduDetailService;
        IDDService DDService;
        // Controller Constructor
        public EduDetailController(IEduDetailService edudetailService, IDDService ddService)
        {
            DDService= ddService;
            EduDetailService = edudetailService;
        }
        #endregion
        #region -- Controller ActionResults  --
        // GET: EduDetail
        public ActionResult Index()
        {
            long cid = AssistantService.LoggedInUserID;
            List<VMEduDetailIndex> vmlist = EduDetailService.GetIndex(cid);
            return View(vmlist);
        }
        #region -- Controller Main View Actions  --
        [HttpGet]
        public ActionResult Create()
        {
            int cid = AssistantService.LoggedInUserID;
            VMEduDetailOperation obj = EduDetailService.GetCreate(cid);
            CreateHelper(obj);
            return View(obj);
        }
        [HttpPost]
        public ActionResult Create(VMEduDetailOperation obj)
        {
            if (ModelState.IsValid)
            {
                EduDetailService.PostCreate(obj);
                return RedirectToAction("Index");
            }
            //CreateHelper(dbOperation);
            return View(obj);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            VMEduDetailOperation obj = EduDetailService.GetEdit((int) id);
            EditHelper(obj);
            return PartialView(obj);
        }
        [HttpPost]
        public ActionResult Edit(VMEduDetailOperation obj)
        {
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
        public ActionResult Delete(VMEduDetailOperation obj, int? id)
        {
            EduDetailService.PostDelete(obj, id);
            return RedirectToAction("Index", "Candidate");
        }
        #endregion
        #endregion
        #region -- Controller Private  Methods--
        private void CreateHelper(VMEduDetailOperation obj)
        {
            ViewBag.DegreeID = new SelectList(DDService.GetEduLevel().ToList().OrderBy(aa => aa.DLevelID).ToList(), "DLevelID", "DegreeLevel", obj.DegreeID);
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