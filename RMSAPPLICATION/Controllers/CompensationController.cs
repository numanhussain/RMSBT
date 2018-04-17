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
using System.Web.UI.WebControls;

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
            RemoveCheckboxValues(obj);
            if (obj.MBSalary == null || obj.MBSalary == "")
                ModelState.AddModelError("MBSalary", "Mandatory !!");
            if (obj.MGSalary == null || obj.MGSalary == "")
                ModelState.AddModelError("MGSalary", "Mandatory !!");
            if (obj.Bonus == true && obj.BonusPerYear == null)
                ModelState.AddModelError("BonusPerYear", "Mandatory !!");
            if (obj.LFA == true && obj.LFAPerYear == null)
                ModelState.AddModelError("LFAPerYear", "Mandatory !!");
            if (obj.ProvidentFund == true && obj.ProvidentFundPerYear == null)
                ModelState.AddModelError("ProvidentFundPerYear", "Mandatory !!");
            if (obj.TransportAllowence == null)
                ModelState.AddModelError("TransportAllowence", "Mandatory !!");
            if (obj.CarEntitlement == null || obj.CarEntitlement == "")
                ModelState.AddModelError("CarEntitlement", "Mandatory !!");
            if (obj.BuyBackOption == true && obj.SpecifyYears == null)
                ModelState.AddModelError("SpecifyYears", "Mandatory !!");
            if (obj.FuelAllowance == null)
                ModelState.AddModelError("FuelAllowance", "Mandatory !!");
            if (obj.AccomdAllowence == null)
                ModelState.AddModelError("AccomdAllowence", "Mandatory !!");
            if (obj.MobileAllowence == null)
                ModelState.AddModelError("MobileAllowence", "Mandatory !!");
            if (obj.MobileUserLimit == null||obj.MobileUserLimit=="")
                ModelState.AddModelError("MobileUserLimit", "Mandatory !!");
            if (obj.COLA == null)
                ModelState.AddModelError("SpecifyYears", "Mandatory !!");
            if (obj.Other == null)
                ModelState.AddModelError("Other", "Mandatory !!");
            if (obj.OPDInsurance == null || obj.OPDInsurance == "")
                ModelState.AddModelError("OPDInsurance", "Mandatory !!");
            if (obj.IPInsurance == null || obj.IPInsurance == "")
                ModelState.AddModelError("IPInsurance", "Mandatory !!");
            if (obj.LifeInsurance == null || obj.LifeInsurance == "")
                ModelState.AddModelError("LifeInsurance", "Mandatory !!");
            if (obj.AnnualTAllowence == null)
                ModelState.AddModelError("AnnualTAllowence", "Mandatory !!");
            if (obj.CasualTAllowence == null)
                ModelState.AddModelError("CasualTAllowence", "Mandatory !!");
            if (obj.MedTAllowence == null)
                ModelState.AddModelError("MedTAllowence", "Mandatory !!");
            if (obj.TAllowenceWorkDay == null)
                ModelState.AddModelError("TAllowenceWorkDay", "Mandatory !!");
            if (obj.ExpectedSalary == null)
                ModelState.AddModelError("ExpectedSalary", "Mandatory !!");
            if (obj.OtherBenifits == null || obj.OtherBenifits == "")
                ModelState.AddModelError("OtherBenifits", "Mandatory !!");
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
        private void RemoveCheckboxValues(CompensationDetail obj)
        {
            #region -- Radio Buttons--
            if (obj.Bonus == false)
            {
                obj.BBonus = null;
                obj.GBonus = null;
                obj.BonusPerYear = null;
            }
            if (obj.LFA == false)
            {
                obj.BLFA = null;
                obj.GLFA = null;
                obj.LFAPerYear = null;
            }
            if (obj.OT == false)
            {
                obj.BOT = null;
                obj.GOT = null;
            }
            if (obj.Food == false)
            {
                obj.Free = null;
                obj.Subsidized = null;
            }
            if (obj.BuyBackOption == false)
            {
                obj.SpecifyYears = null;
            }
            if (obj.ProvidentFund == false)
            {
                obj.BProvidentFund = null;
                obj.GProvidentFund = null;
                obj.ProvidentFundPerYear = null;
            }
            if (obj.Gratuity == false)
            {
                obj.BGratuity = null;
                obj.GGratuity = null;
            }
            #endregion
        }
        #endregion
    }
}