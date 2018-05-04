using RMSCORE.EF;
using RMSSERVICES.Generic;
using RMSSERVICES.Self_Assessment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RMSAPPLICATION.Controllers
{
    public class SelfAssessmentController : Controller
    {
        #region -- Controller Initialization --
        IEntityService<CandidateStrength> CandidateStrengthEntityService;
        ISelfAssessmentService CandidateStrengthService;
        public SelfAssessmentController(ISelfAssessmentService candidateStrengthService, IEntityService<CandidateStrength> candidateStrengthEntityService)
        {
            CandidateStrengthEntityService = candidateStrengthEntityService;
            CandidateStrengthService = candidateStrengthService;
        }
        #endregion 
        // GET: Miscellaneous
        #region -- Controller Main View Actions  --
        // GET: SelfAssessment  
        [HttpGet]
        public ActionResult Index()
        {
            V_UserCandidate vmf = Session["LoggedInUser"] as V_UserCandidate;
            int cid = vmf.CandidateID;
            CandidateStrength obj = CandidateStrengthService.GetIndex(cid);
            return View(obj);
        }
        [HttpPost]
        public ActionResult Index(CandidateStrength dbOperation)
        {
            V_UserCandidate vmf = Session["LoggedInUser"] as V_UserCandidate;
            if (dbOperation.Objective == null || dbOperation.Objective == "")
                ModelState.AddModelError("Objective", "Mandatory ");
            if (dbOperation.Strengths == null || dbOperation.Strengths == "")
                ModelState.AddModelError("Strengths", "Mandatory ");
            if (dbOperation.AreaOfImprovement == null || dbOperation.AreaOfImprovement == "")
                ModelState.AddModelError("AreaOfImprovement", "Mandatory ");
            if (dbOperation.MeetRequirements == null || dbOperation.MeetRequirements == "")
                ModelState.AddModelError("MeetRequirements", "Mandatory ");
            if (dbOperation.Objective != null)
            {
                if (dbOperation.Objective.Length > 250)
                    ModelState.AddModelError("Objective ", "String length exceeds");
            }
            if (dbOperation.Strengths != null)
            {
                if (dbOperation.Strengths.Length > 250)
                    ModelState.AddModelError("Strengths", "String length exceeds");
            }
            if (dbOperation.AreaOfImprovement != null)
            {
                if (dbOperation.AreaOfImprovement.Length > 250)
                    ModelState.AddModelError("AreaOfImprovement", "String length exceeds");
            }
            if (dbOperation.MeetRequirements != null)
            {
                if (dbOperation.MeetRequirements.Length > 250)
                    ModelState.AddModelError("MeetRequirements", "String length exceeds");
            }
            if (ModelState.IsValid)
            {
                if (vmf.UserStage == 7)
                    vmf.UserStage = 8;
                CandidateStrengthService.PostIndex(dbOperation, vmf);
                Session["LoggedInUser"] = vmf;
                Session["ProfileStage"] = vmf.UserStage;
                return Json("OK", JsonRequestBehavior.AllowGet);
            }
            return View("Index", dbOperation);
        }
        #endregion
        #region -- Controller Private  Methods--
        #endregion
    }
}