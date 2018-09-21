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
        IEntityService<CandidateStep> CandidateStepEntityService;
        IReferenceService ReferenceDetailService;
        IDDService DDService;
        // Controller Constructor
        public ReferenceController(IReferenceService referencedetailService, IDDService ddService, IEntityService<CandidateStep> candidateStepEntityService)
        {
            DDService = ddService;
            ReferenceDetailService = referencedetailService;
            CandidateStepEntityService = candidateStepEntityService;
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
            HelperClass(obj);
            return View(obj);
        }
        [HttpPost]
        public ActionResult Create(VMReferenceOperation obj)
        {
            V_UserCandidate vmf = Session["LoggedInUser"] as V_UserCandidate;
            if (obj.RefName1 == null || obj.RefName1 == "")
                ModelState.AddModelError("RefName1", "Mandatory ");
            if (obj.RefDesignation1 == null || obj.RefDesignation1 == "")
                ModelState.AddModelError("RefDesignation1", "Mandatory ");
            if (obj.Organization1 == null || obj.Organization1 == "")
                ModelState.AddModelError("Organization1", "Mandatory ");
            if (obj.RefContact1 == null || obj.RefContact1 == "")
                ModelState.AddModelError("RefContact1", "Mandatory ");
            if (obj.HowLongKnown1 == null || obj.HowLongKnown1 == "")
                ModelState.AddModelError("HowLongKnown1", "Mandatory ");
            if (obj.RefEmail1 == null || obj.RefEmail1 == "")
                ModelState.AddModelError("RefEmail1", "Mandatory ");
            if (obj.RefEmail1 != null)
            {
                Match match = Regex.Match(obj.RefEmail1, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                if (!match.Success)
                {
                    ModelState.AddModelError("RefEmail1", "Invalid Formate");
                }
            }
            if (obj.RefName2 == null || obj.RefName2 == "")
                ModelState.AddModelError("RefName2", "Mandatory ");
            if (obj.RefDesignation2 == null || obj.RefDesignation1 == "")
                ModelState.AddModelError("RefDesignation2", "Mandatory ");
            if (obj.Organization2 == null || obj.Organization2 == "")
                ModelState.AddModelError("Organization2", "Mandatory ");
            if (obj.RefContact2 == null || obj.RefContact2 == "")
                ModelState.AddModelError("RefContact2", "Mandatory ");
            if (obj.HowLongKnown2 == null || obj.HowLongKnown2 == "")
                ModelState.AddModelError("HowLongKnown2", "Mandatory ");
            if (obj.RefEmail2 == null || obj.RefEmail2 == "")
                ModelState.AddModelError("RefEmail2", "Mandatory ");
            if (obj.RefEmail2 != null)
            {
                Match match = Regex.Match(obj.RefEmail2, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                if (!match.Success)
                {
                    ModelState.AddModelError("RefEmail2", "Invalid Formate");
                }
            }
            if (ModelState.IsValid)
            {
                ReferenceDetailService.PostCreate(obj, vmf);
                CandidateStep dbtickStep = CandidateStepEntityService.GetEdit(vmf.CandidateID);
                dbtickStep.StepSix = true;
                CandidateStepEntityService.PostEdit(dbtickStep);
                vmf.StepSix = dbtickStep.StepSix;
                return Json("OK", JsonRequestBehavior.AllowGet);
            }
            HelperClass(obj);
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
            HelperClass(obj);
            return PartialView(obj);
        }
        [HttpPost]
        public ActionResult Edit(VMReferenceOperation obj)
        {
            V_UserCandidate vmf = Session["LoggedInUser"] as V_UserCandidate; if (obj.RefName1 == null || obj.RefName1 == "")
                ModelState.AddModelError("RefName1", "Mandatory ");
            if (obj.RefDesignation1 == null || obj.RefDesignation1 == "")
                ModelState.AddModelError("RefDesignation1", "Mandatory ");
            if (obj.Organization1 == null || obj.Organization1 == "")
                ModelState.AddModelError("Organization1", "Mandatory ");
            if (obj.RefContact1 == null || obj.RefContact1 == "")
                ModelState.AddModelError("RefContact1", "Mandatory ");
            if (obj.HowLongKnown1 == null || obj.HowLongKnown1 == "")
                ModelState.AddModelError("HowLongKnown1", "Mandatory ");
            if (obj.RefEmail1 == null || obj.RefEmail1 == "")
                ModelState.AddModelError("RefEmail1", "Mandatory ");
            if (obj.RefEmail1 != null)
            {
                Match match = Regex.Match(obj.RefEmail1, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                if (!match.Success)
                {
                    ModelState.AddModelError("RefEmail1", "Invalid Formate");
                }
            }
            if (obj.RefName2 == null || obj.RefName2 == "")
                ModelState.AddModelError("RefName2", "Mandatory ");
            if (obj.RefDesignation2 == null || obj.RefDesignation1 == "")
                ModelState.AddModelError("RefDesignation2", "Mandatory ");
            if (obj.Organization2 == null || obj.Organization2 == "")
                ModelState.AddModelError("Organization2", "Mandatory ");
            if (obj.RefContact2 == null || obj.RefContact2 == "")
                ModelState.AddModelError("RefContact2", "Mandatory ");
            if (obj.HowLongKnown2 == null || obj.HowLongKnown2 == "")
                ModelState.AddModelError("HowLongKnown2", "Mandatory ");
            if (obj.RefEmail2 == null || obj.RefEmail2 == "")
                ModelState.AddModelError("RefEmail2", "Mandatory ");
            if (obj.RefEmail2 != null)
            {
                Match match = Regex.Match(obj.RefEmail2, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                if (!match.Success)
                {
                    ModelState.AddModelError("RefEmail2", "Invalid Formate ");
                }
            }
            if (ModelState.IsValid)
            {
                ReferenceDetailService.PostEdit(obj);
                return Json("OK", JsonRequestBehavior.AllowGet);
            }
            HelperClass(obj);
            return PartialView(obj);
        }
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            VMReferenceOperation vmOperation = ReferenceDetailService.GetDelete((int)id);
            HelperClass(vmOperation);
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
        private void HelperClass(VMReferenceOperation obj)
        {
            ViewBag.SalutationID1 = new SelectList(DDService.GetSalutationList().ToList().OrderBy(aa => aa.CSalutationID).ToList(), "CSalutationID", "SalutationName", obj.SalutationID1);
            ViewBag.SalutationID2 = new SelectList(DDService.GetSalutationList().ToList().OrderBy(aa => aa.CSalutationID).ToList(), "CSalutationID", "SalutationName", obj.SalutationID2);
        }
        #endregion
    }
}