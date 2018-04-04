using RMSCORE.EF;
using RMSCORE.Models.Main;
using RMSCORE.Models.Operation;
using RMSSERVICES.Generic;
using RMSSERVICES.Skill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RMSAPPLICATION.Controllers
{
    public class SkillController : Controller
    {
        #region -- Controller Initialization --
        //IEntityService<VMEduDetailIndex> EduDetailEntityService;
        ISkillService SkillDetailService;
        IDDService DDService;
        // Controller Constructor
        public SkillController(ISkillService skilldetailService, IDDService ddService)
        {
            DDService = ddService;
            SkillDetailService = skilldetailService;
        }
        #endregion
        #region -- Controller ActionResults  --
        // GET: Skill
        public ActionResult Index()
        {
            V_UserCandidate vmf = Session["LoggedInUser"] as V_UserCandidate;
            int cid = vmf.CandidateID;
            List<VMSkillIndex> vmlist = SkillDetailService.GetIndex(cid);
            return View(vmlist);
        }
        #region -- Controller Main View Actions  --
        [HttpGet]
        public ActionResult Create()
        {
            V_UserCandidate vmf = Session["LoggedInUser"] as V_UserCandidate;
            int cid = vmf.CandidateID;
            VMSkillOperation obj = SkillDetailService.GetCreate(cid);
            CreateHelper(obj);
            return View(obj);
        }
        [HttpPost]
        public ActionResult Create(VMSkillOperation obj)
        {
            V_UserCandidate vmf = Session["LoggedInUser"] as V_UserCandidate;
            if (ModelState.IsValid)
            {
                SkillDetailService.PostCreate(obj);
                return Json("OK", JsonRequestBehavior.AllowGet);
            }
            CreateHelper(obj);
            return PartialView("Create",obj);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            VMSkillOperation obj = SkillDetailService.GetEdit(id);
            EditHelper(obj);
            return PartialView(obj);
        }
        [HttpPost]
        public ActionResult Edit(VMSkillOperation obj)
        {
            if (ModelState.IsValid)
            {
                SkillDetailService.PostEdit(obj);
                return Json("OK", JsonRequestBehavior.AllowGet);
            }
            EditHelper(obj);
            return PartialView(obj);
        }
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            VMSkillOperation vmOperation = SkillDetailService.GetDelete((int)id);
            return View(vmOperation);
        }
        [HttpPost]
        public ActionResult Delete(VMSkillOperation obj)
        {
            SkillDetailService.PostDelete(obj);
            return PartialView(obj);
        }
        #endregion
        #endregion
        #region -- Controller Private  Methods--
        private void CreateHelper(VMSkillOperation obj)
        {
            ViewBag.SLevelID = new SelectList(DDService.GetSkillLevel().ToList().OrderBy(aa => aa.SkillLevelID).ToList(), "SkillLevelID", "SkillLevelName", obj.SLevelID);
        }
        private void EditHelper(VMSkillOperation obj)
        {
            ViewBag.SLevelID = new SelectList(DDService.GetSkillLevel().ToList().OrderBy(aa => aa.SkillLevelID).ToList(), "SkillLevelID", "SkillLevelName", obj.SLevelID);
        }
        #endregion
    }
}