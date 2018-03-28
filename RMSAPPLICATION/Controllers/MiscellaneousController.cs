using RMSCORE.EF;
using RMSCORE.Models.Operation;
using RMSSERVICES.Generic;
using RMSSERVICES.Miscellaneous;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RMSAPPLICATION.Controllers
{
    public class MiscellaneousController : Controller
    {
        #region -- Controller Initialization --
        IEntityService<MiscellaneousDetail> MiscellaneousEntityService;
        IMiscellaneousService MiscellaneousService;
        IDDService DDService;
        // Controller Constructor
        public MiscellaneousController(IMiscellaneousService miscellaneousService, IEntityService<MiscellaneousDetail> miscellaneousentityService, IDDService ddService)
        {
            DDService = ddService;
            MiscellaneousService = miscellaneousService;
            MiscellaneousEntityService = miscellaneousentityService;
        }
        #endregion 
        // GET: Miscellaneous
        #region -- Controller Main View Actions  --
        [HttpGet]
        public ActionResult Create()
        {
            V_UserCandidate vmf = Session["LoggedInUser"] as V_UserCandidate;
            int cid = vmf.CandidateID;
            MiscellaneousDetail obj = MiscellaneousService.GetCreate(cid);
            CreateHelper(obj);
            return View(obj);
        }
        [HttpPost]
        public ActionResult Create(MiscellaneousDetail obj)
        {
            V_UserCandidate vmf = Session["LoggedInUser"] as V_UserCandidate;
            if (obj.CrimanalRecord == null || obj.CrimanalRecord == "")
                ModelState.AddModelError("CrimanalRecord", "Cannot be empty");
            if (obj.HearAboutJobID == null)
                ModelState.AddModelError("HearAboutJobID", "Must select one !");
            if (obj.TotalExp == null)
                ModelState.AddModelError("TotalExp", "Cannot be empty !");
            if (obj.CementExp == null)
                ModelState.AddModelError("CementExp", "Cannot be empty !");

            if (ModelState.IsValid)
            {
                if (vmf.UserStage == 6)
                    vmf.UserStage = 7;
                MiscellaneousService.PostCreate(obj, vmf);
                Session["LoggedInUser"] = vmf;
                Session["ProfileStage"] = vmf.UserStage;
                return Json("OK", JsonRequestBehavior.AllowGet);
            }
            CreateHelper(obj);
            return View(obj);
        }
        #endregion
        #region -- Controller Private  Methods--
        private void CreateHelper(MiscellaneousDetail obj)
        {
            ViewBag.HearAboutJobID = new SelectList(DDService.GetHearAboutJob().ToList().OrderBy(aa => aa.HearAboutID).ToList(), "HearAboutID", "HearAboutSource", obj.HearAboutJobID);
        }
        #endregion
    }
}