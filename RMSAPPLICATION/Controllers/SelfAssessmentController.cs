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
        IEntityService<CandidateStep> CandidateStepEntityService;
        ISelfAssessmentService CandidateStrengthService;
        public SelfAssessmentController(ISelfAssessmentService candidateStrengthService, IEntityService<CandidateStep> candidateStepEntityService, IEntityService<CandidateStrength> candidateStrengthEntityService)
        {
            CandidateStrengthEntityService = candidateStrengthEntityService;
            CandidateStepEntityService = candidateStepEntityService;
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
                if (vmf.UserStage == 5)
                    vmf.UserStage = 6;
                CandidateStrengthService.PostIndex(dbOperation, vmf);
                CandidateStep dbtickStep = CandidateStepEntityService.GetEdit(vmf.CandidateID);
                dbtickStep.StepFive = true;
                CandidateStepEntityService.PostEdit(dbtickStep);
                vmf.StepFive = dbtickStep.StepFive;
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