using RMSCORE.EF;
using RMSCORE.Models.Main;
using RMSCORE.Models.Operation;
using RMSSERVICES.Compensation;
using RMSSERVICES.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RMSAPPLICATION.Controllers
{
    public class CompensationController : Controller
    {

        #region -- Controller Initialization --
        IEntityService<CompensationDetail> CompensationEntityService;
        ICompensationService CompensationDetailService;
        IDDService DDService;
        //IDDService DDService;
        // Controller Constructor
        public CompensationController(ICompensationService compensationService,IEntityService<CompensationDetail> compensationEntityService,IDDService ddService)
        {
            CompensationEntityService = compensationEntityService;
            CompensationDetailService = compensationService;
            DDService = ddService;
        }
        #endregion 
        // GET: Miscellaneous
        #region -- Controller Main View Actions  --
        [HttpGet]
        public ActionResult Create()
        {
            V_UserCandidate vmf = Session["LoggedInUser"] as V_UserCandidate;
            int cid = vmf.CandidateID;
            CompensationDetail obj = CompensationDetailService.GetCreate(cid);
            return View(obj);
        }
        [HttpPost]
        public ActionResult Create(CompensationDetail obj)
        {
            if (ModelState.IsValid)
            {
                CompensationDetailService.PostCreate(obj);
                return Json("OK", JsonRequestBehavior.AllowGet);
            }
            return PartialView("Create", obj);
        }
        #endregion
        #region -- Controller Private  Methods--
        #endregion
    }
}