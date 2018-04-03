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
            if (dbOperation.Strengths == null)
                ModelState.AddModelError("Strengths", "This is mandatory field");
            if (dbOperation.AreaOfImprovement == null)
                ModelState.AddModelError("AreaOfImprovement", "This is mandatory field");
            if (dbOperation.MeetRequirements == null)
                ModelState.AddModelError("MeetRequirements", "This is mandatory field");
            if (dbOperation.Strengths != null)
            {
                if (dbOperation.Strengths.Length > 15)
                    ModelState.AddModelError("Strengths", "String length exceeds!");
            }
            if (ModelState.IsValid)
            {
                CandidateStrengthService.PostIndex(dbOperation);
            }
            return View("Index", dbOperation);
        }
        #endregion
        #region -- Controller Private  Methods--
        #endregion
    }
}