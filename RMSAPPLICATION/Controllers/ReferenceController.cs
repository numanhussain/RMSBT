using RMSCORE.EF;
using RMSCORE.Models.Main;
using RMSCORE.Models.Operation;
using RMSSERVICES.Generic;
using RMSSERVICES.Reference;
using System;
using System.Collections.Generic;
using System.Linq;
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
            if (ModelState.IsValid)
            {
                ReferenceDetailService.PostCreate(obj);
            }
            return PartialView(obj);
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
            if (ModelState.IsValid)
            {
                ReferenceDetailService.PostEdit(obj);
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