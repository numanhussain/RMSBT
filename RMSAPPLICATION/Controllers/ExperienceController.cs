using RMSCORE.Models.Main;
using RMSCORE.Models.Operation;
using RMSSERVICES.Experience;
using RMSSERVICES.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RMSAPPLICATION.Controllers
{
    public class ExperienceController : Controller
    {
        #region -- Controller Initialization --
        //IEntityService<VMEduDetailIndex> EduDetailEntityService;
        IExperienceDetailService ExperienceDetailService;
        IDDService DDService;
        // Controller Constructor
        public ExperienceController(IExperienceDetailService experiencedetailService, IDDService ddService)
        {
            DDService = ddService;
            ExperienceDetailService = experiencedetailService;
        }
        #endregion
        #region -- Controller ActionResults  --
        // GET: ExperienceDetail
        public ActionResult Index()
        {
            long cid = AssistantService.LoggedInUserID;
            List<VMExperienceIndex> vmlist = ExperienceDetailService.GetIndex(cid);
            return View(vmlist);
        }
        #region -- Controller Main View Actions  --
        [HttpGet]
        public ActionResult Create()
        {
            int cid = AssistantService.LoggedInUserID;
            VMExperienceOperation obj = ExperienceDetailService.GetCreate(cid);
            CreateHelper(obj);
            return View(obj);
        }
        [HttpPost]
        public ActionResult Create(VMExperienceOperation obj)
        {
            if (ModelState.IsValid)
            {
                ExperienceDetailService.PostCreate(obj);
                return Json("OK", JsonRequestBehavior.AllowGet);
            }
            return PartialView("Create", obj);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            VMExperienceOperation obj = ExperienceDetailService.GetEdit((int)id);
            EditHelper(obj);
            return PartialView(obj);
        }
        [HttpPost]
        public ActionResult Edit(VMExperienceOperation obj)
        {
            if (ModelState.IsValid)
            {
                ExperienceDetailService.PostEdit(obj);
                return Json("OK", JsonRequestBehavior.AllowGet);
            }
            return PartialView("Edit", obj);
        }
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            VMExperienceOperation vmOperation = ExperienceDetailService.GetDelete((int)id);
            return View(vmOperation);
        }
        [HttpPost]
        public ActionResult Delete(VMExperienceOperation obj, int? id)
        {
            ExperienceDetailService.PostDelete(obj, id);
            return RedirectToAction("Index", "Skill");
        }
        #endregion
        #endregion
        #region -- Controller Private  Methods--
        private void CreateHelper(VMExperienceOperation obj)
        {
            ViewBag.IndustryID = new SelectList(DDService.GetIndustryList().ToList().OrderBy(aa => aa.ExpIndustryID).ToList(), "ExpIndustryID", "ExpIndustryName", obj.IndustryID);
        }
        private void EditHelper(VMExperienceOperation obj)
        {
            ViewBag.IndustryID = new SelectList(DDService.GetIndustryList().OrderBy(aa => aa.ExpIndustryID).ToList(), "ExpIndustryID", "ExpIndustryName", obj.IndustryID);
        }
        #endregion
    }
}