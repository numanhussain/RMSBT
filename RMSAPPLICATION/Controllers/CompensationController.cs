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
            // Adjust Bonus of Compensation Radio Button
            string radioBonusValue = "";

            var BonusValue = ValueProvider.GetValue("BonusSelection");
            if (BonusValue != null)
            {
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
            }
            else
            {
                obj.BBonus = false;
                obj.GBonus = false;
            }
            // Adjust LFA of Compensation Radio Button
            string radioLFAValue = "";
            var LFAValue = ValueProvider.GetValue("LFASelection");
            if (LFAValue != null)
            {
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
            }
            else
            {
                obj.BLFA = false;
                obj.GLFA = false;
            }
            // Adjust OT of Compensation Radio Button
            string radioOTValue = "";
            var OTValue = ValueProvider.GetValue("OTselection");
            if (OTValue != null)
            {
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
            }
            else
            {
                obj.BOT = false;
                obj.GOT = false;
            }
            // Adjust ProvidentFund of Compensation Radio Button
            string radioProvidentValue = "";
            var ProvidentValue = ValueProvider.GetValue("ProvidentSelection");
            if (ProvidentValue != null)
            {
                if (radioProvidentValue != null)
                {
                    radioProvidentValue = ProvidentValue.AttemptedValue;
                }
                if (radioProvidentValue == "BProvidentFund")
                {
                    obj.BProvidentFund = true;
                    obj.GProvidentFund = false;
                }
                if (radioProvidentValue == "GProvidentFund")
                {
                    obj.BProvidentFund = false;
                    obj.GProvidentFund = true;
                }
            }
            else
            {
                obj.BProvidentFund = false;
                obj.GProvidentFund = false;
            }
            // Adjust Gratuity of Compensation Radio Button
            string radioGratuityValue = "";
            var GratuityValue = ValueProvider.GetValue("GratuitySelection");
            if (GratuityValue != null)
            {
                if (radioGratuityValue != null)
                {
                    radioGratuityValue = GratuityValue.AttemptedValue;
                }
                if (radioGratuityValue == "BGratuity")
                {
                    obj.BGratuity = true;
                    obj.GGratuity = false;
                }
                if (radioGratuityValue == "GGratuity")
                {
                    obj.BGratuity = false;
                    obj.GGratuity = true;
                }
            }
            else
            {
                obj.BProvidentFund = false;
                obj.GProvidentFund = false;
            }
            // Adjust Food of Compensation Radio Button
            string radioFoodValue = "";
            var FoodValue = ValueProvider.GetValue("FoodSelection");
            if (FoodValue != null)
            {
                if (radioFoodValue != null)
                {
                    radioFoodValue = FoodValue.AttemptedValue;
                }
                if (radioFoodValue == "Free")
                {
                    obj.Free = true;
                    obj.Subsidized = false;
                }
                if (radioFoodValue == "Subsidized")
                {
                    obj.Free = false;
                    obj.Subsidized = true;
                }
            }
            else
            {
                obj.BProvidentFund = false;
                obj.GProvidentFund = false;
            }
            #endregion
        }
        #endregion
    }
}