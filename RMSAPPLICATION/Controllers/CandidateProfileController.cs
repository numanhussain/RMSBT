using RMSCORE.EF;
using RMSSERVICES.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RMSAPPLICATION.Controllers
{
    public class CandidateProfileController : Controller
    {
        IEntityService<V_CandidateProfile> VCandidateProfileService;
        public CandidateProfileController( IEntityService<V_CandidateProfile> vCandidateProfileService)
        {
            VCandidateProfileService = vCandidateProfileService;
        }
        // GET: CandidateProfile
        [HttpGet]
        public ActionResult CandidateProfile(int? CID)
        {
            V_CandidateProfile list = VCandidateProfileService.GetEdit((int)CID);

            return PartialView(list);
        }

    }
}