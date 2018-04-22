using RMSCORE.EF;
using RMSCORE.Models.Main;
using RMSCORE.Models.Operation;
using RMSSERVICES.Generic;
using RMSSERVICES.Reference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace RMSAPPLICATION.Controllers
{
    public class ReferenceController : Controller
    {
        #region -- Controller Initialization --
        //IEntityService<VMEduDetailIndex> EduDetailEntityService;
        IReferenceService ReferenceDetailService;
        IDDService DDService;
        // Controller Constructor
        public ReferenceController(IReferenceService referencedetailService, IDDService ddService)
        {
            DDService = ddService;
            ReferenceDetailService = referencedetailService;
        }
        #endregion
        #region -- Controller ActionResults  --
        // GET: Reference
        public ActionResult Index()
        {
            V_UserCandidate vmf = Session["LoggedInUser"] as V_UserCandidate;
            int cid = vmf.CandidateID;
            List<VMReferenceIndex> vmlist = ReferenceDetailService.GetIndex(cid);
            return View(vmlist);
        }
        #region -- Controller Main View Actions  --
        [HttpGet]
        public ActionResult Create()
        {
            V_UserCandidate vmf = Session["LoggedInUser"] as V_UserCandidate;
            int cid = vmf.CandidateID;
            VMReferenceOperation obj = ReferenceDetailService.GetCreate(cid);
            return View(obj);
        }
        [HttpPost]
        public ActionResult Create(VMReferenceOperation obj)
        {
            V_UserCandidate vmf = Session["LoggedInUser"] as V_UserCandidate;
            if (obj.RefName == null || obj.RefName == "")
                ModelState.AddModelError("RefName", "Mandatory !!");
            if (obj.RefDesignation == null || obj.RefDesignation == "")
                ModelState.AddModelError("RefDesignation", "Mandatory !!");
            if (obj.Organization == null || obj.Organization == "")
                ModelState.AddModelError("Organization", "Mandatory !!");
            if (obj.RefContact == null || obj.RefContact == "")
                ModelState.AddModelError("RefContact", "Mandatory !!");
            if (obj.HowLongKnown == null || obj.HowLongKnown == "")
                ModelState.AddModelError("HowLongKnown", "Mandatory !!");
            if (obj.RefEmail == null || obj.RefEmail == "")
                ModelState.AddModelError("RefEmail", "Mandatory !!");
            if (obj.RefEmail != null)
            {
                Match match = Regex.Match(obj.RefEmail, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                if (!match.Success)
                {
                    ModelState.AddModelError("RefEmail", "Invalid Formate !!");
                }
            }
            if (ModelState.IsValid)
            {
                ReferenceDetailService.PostCreate(obj, vmf);
                return Json("OK", JsonRequestBehavior.AllowGet);
            }
            return PartialView("Create", obj);
        }
        [HttpGet]
        public ActionResult Create2()
        {
            V_UserCandidate vmf = Session["LoggedInUser"] as V_UserCandidate;
            int cid = vmf.CandidateID;
            VMReferenceOperation obj = ReferenceDetailService.GetCreate(cid);
            return View(obj);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            VMReferenceOperation obj = ReferenceDetailService.GetEdit((int)id);
            return PartialView(obj);
        }
        [HttpPost]
        public ActionResult Edit(VMReferenceOperation obj)
        {
            V_UserCandidate vmf = Session["LoggedInUser"] as V_UserCandidate;
            if (ModelState.IsValid)
            {
                ReferenceDetailService.PostEdit(obj);
                return Json("OK", JsonRequestBehavior.AllowGet);
            }
            return PartialView(obj);
        }
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            VMReferenceOperation vmOperation = ReferenceDetailService.GetDelete((int)id);
            return View(vmOperation);
        }
        [HttpPost]
        public ActionResult Delete(VMReferenceOperation obj)
        {
            ReferenceDetailService.PostDelete(obj);
            return PartialView(obj);
        }
        #endregion
        #endregion
        #region -- Controller Private  Methods--
        #endregion
    }
}