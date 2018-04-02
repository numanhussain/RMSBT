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
        public CompensationController(ICompensationService compensationService, IEntityService<CompensationDetail> compensationEntityService, IDDService ddService)
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
            ReadFromRadioButton(obj);
            if (obj.MGSalary == null || obj.MGSalary == "")
                ModelState.AddModelError("MGSalary", "This is mandatory field");
            if (ModelState.IsValid)
            {
                V_UserCandidate vmf = Session["LoggedInUser"] as V_UserCandidate;
                if (vmf.UserStage == 5)
                    vmf.UserStage = 6;
                CompensationDetailService.PostCreate(obj, vmf);
                Session["LoggedInUser"] = vmf;
                Session["ProfileStage"] = vmf.UserStage;
                return Json("OK", JsonRequestBehavior.AllowGet);
            }
            return View(obj);
        }
        #endregion
        #region -- Controller Private  Methods--
        private void ReadFromRadioButton(CompensationDetail obj)
        {

            #region -- Radio Buttons--
            // Adjust Bonus of Leave Radio Button
            string radioBonusValue = "";
            var BonusValue = ValueProvider.GetValue("BonusSelection");
            if (radioBonusValue != null)
            {
                radioBonusValue = BonusValue.AttemptedValue;
            }
            if (radioBonusValue == "BBonus")
            {
                obj.BBonus = true;
                obj.GBonus = false;
            }
            if (radioBonusValue == "GBonus")
            {
                obj.BBonus = false;
                obj.GBonus = true;
            }
            // Adjust LFA of Leave Radio Button
            string radioLFAValue = "";
            var LFAValue = ValueProvider.GetValue("LFASelection");
            if (radioLFAValue != null)
            {
                radioLFAValue = LFAValue.AttemptedValue;
            }
            if (radioLFAValue == "BLFA")
            {
                obj.BLFA = true;
                obj.GLFA = false;
            }
            if (radioLFAValue == "GLFA")
            {
                obj.BLFA = false;
                obj.GLFA = true;
            }
            // Adjust OT of Leave Radio Button
            string radioOTValue = "";
            var OTValue = ValueProvider.GetValue("OTselection");
            if (radioOTValue != null)
            {
                radioOTValue = OTValue.AttemptedValue;
            }
            if (radioOTValue == "BOT")
            {
                obj.BOT = true;
                obj.GOT = false;
            }
            if (radioOTValue == "GOT")
            {
                obj.BOT = false;
                obj.GOT = true;
            }
            #endregion
        }
        #endregion
    }
}