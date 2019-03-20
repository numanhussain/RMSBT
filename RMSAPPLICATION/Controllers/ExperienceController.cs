using RMSCORE.EF;
using RMSCORE.Models.Main;
using RMSCORE.Models.Operation;
using RMSSERVICES.Experience;
using RMSSERVICES.Generic;
using RMSSERVICES.Miscellaneous;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace RMSAPPLICATION.Controllers
{
    public class ExperienceController : Controller
    {
        #region -- Controller Initialization --
        //IEntityService<VMEduDetailIndex> EduDetailEntityService;
        IEntityService<CandidateStep> CandidateStepEntityService;
        IExperienceDetailService ExperienceDetailService;
        IEntityService<Candidate> CandidateEntityService;
        IEntityService<User> UserEntityService;
        IEntityService<MiscellaneousDetail> MiscellaneousDetailService;
        IMiscellaneousService MiscellaneousService;
        IDDService DDService;
        // Controller Constructor
        public ExperienceController(IExperienceDetailService experiencedetailService, IEntityService<User> userEntityService, IEntityService<Candidate> candidateEntityService, IDDService ddService, IMiscellaneousService miscellaneousService, IEntityService<MiscellaneousDetail> miscellaneousDetailService,
            IEntityService<CandidateStep> candidateStepEntityService)
        {
            DDService = ddService;
            ExperienceDetailService = experiencedetailService;
            CandidateEntityService = candidateEntityService;
            UserEntityService = userEntityService;
            MiscellaneousService = miscellaneousService;
            MiscellaneousDetailService = miscellaneousDetailService;
            CandidateStepEntityService = candidateStepEntityService;
        }
        #endregion
        #region -- Controller ActionResults  --
        // GET: ExperienceDetail
        public ActionResult Index()
        {
            V_UserCandidate vmf = Session["LoggedInUser"] as V_UserCandidate;
            int cid = vmf.CandidateID;
            float TotalExperience = 0;
            float CementExperience = 0;
            float TotalSum = 0;
            float CementSum = 0;
            List<VMExperienceIndex> vmlist = ExperienceDetailService.GetIndex(cid);
            Expression<Func<MiscellaneousDetail, bool>> SpecificPosition3 = c => c.CandidateID == cid;
            List<MiscellaneousDetail> dbMiscellaneousDetails = MiscellaneousDetailService.GetIndexSpecific(SpecificPosition3);
            foreach (var item in vmlist.OrderBy(aa => aa.StartDate))
            {
                if (MiscellaneousDetailService.GetIndexSpecific(SpecificPosition3).Count > 0)
                {
                    MiscellaneousDetail dbMiscellaneous = dbMiscellaneousDetails.First();
                    if (item.IndustryID == 68)
                    {
                        if (item.EndDate != null)
                        {
                            CementExperience = item.EndDate.Value.Year - item.StartDate.Value.Year;
                        }
                        else
                        {
                            CementExperience = DateTime.Now.Year - item.StartDate.Value.Year;
                        }
                        CementSum = CementSum + CementExperience;
                    }
                    if (item.IndustryID != 68)
                    {
                        if (item.EndDate != null)
                        {
                            TotalExperience = item.EndDate.Value.Year - item.StartDate.Value.Year;
                        }
                        else
                        {
                            TotalExperience = DateTime.Now.Year - item.StartDate.Value.Year;
                        }
                        TotalSum = TotalSum + TotalExperience;
                    }
                    dbMiscellaneous.TotalExp = TotalSum + CementSum;
                    dbMiscellaneous.CementExp = CementSum;
                    MiscellaneousDetailService.PostEdit(dbMiscellaneous);
                    if (dbMiscellaneous.TotalExp != null && dbMiscellaneous.CementExp != null)
                    {
                        ViewBag.Experience = dbMiscellaneous.TotalExp.Value.ToString("0.0");
                        ViewBag.CementExperience = dbMiscellaneous.CementExp.Value.ToString("0.0");
                    }
                }
            }

            return View(vmlist);
        }

        //public ActionResult SaveOverallExperience(int? Experience, int? CementExperience)
        //{
        //    V_UserCandidate vmf = Session["LoggedInUser"] as V_UserCandidate;
        //    int cid = vmf.CandidateID;
        //    ExperienceDetailService.PostGeneralExperience(Experience, CementExperience, vmf);
        //    return Json("OK", JsonRequestBehavior.AllowGet);
        //}

        public ActionResult SaveOverallExperience(float? Experience)
        {
            V_UserCandidate vmf = Session["LoggedInUser"] as V_UserCandidate;
            int cid = vmf.CandidateID;
            ExperienceDetailService.PostGeneralExperience(Experience, vmf);
            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveCementExperience(float? CementExp)
        {
            V_UserCandidate vmf = Session["LoggedInUser"] as V_UserCandidate;
            int cid = vmf.CandidateID;
            ExperienceDetailService.PostCementExperience(CementExp, vmf);
            return Json("OK", JsonRequestBehavior.AllowGet);
        }
        #region -- Controller Main View Actions  --
        [HttpGet]
        public ActionResult Create()
        {
            V_UserCandidate vmf = Session["LoggedInUser"] as V_UserCandidate;
            int cid = vmf.CandidateID;
            VMExperienceOperation obj = ExperienceDetailService.GetCreate(cid);
            CreateHelper(obj);
            return View(obj);
        }
        [HttpPost]
        public ActionResult Create(VMExperienceOperation obj)
        {
            ReadFromRadioButton(obj);
            V_UserCandidate vmf = Session["LoggedInUser"] as V_UserCandidate;
            if (obj.IndustryID == 0)
                ModelState.AddModelError("IndustryID", "Mandatory");
            if (obj.IndustryID != 110)
            {
                obj.OtherIndustryName = null;
            }
            if (obj.IndustryID == 110)
            {
                if (obj.OtherIndustryName == null || obj.OtherIndustryName == "")
                    ModelState.AddModelError("OtherIndustryName", "Mandatory");
            }
            if (obj.JobTitle == null || obj.JobTitle == "")
                ModelState.AddModelError("PositionTitle", "Mandatory ");
            if (obj.EmployerName == null || obj.EmployerName == "")
                ModelState.AddModelError("EmployerName", "Mandatory ");
            if (obj.StartDate == null)
                ModelState.AddModelError("StartDate", "Mandatory ");
            if (obj.CurrentlyWorking == true)
            {
                obj.EndDate = null;
            }
            else
            {
                if (obj.EndDate == null)
                    ModelState.AddModelError("EndDate", "Mandatory ");
            }
            if (!AssistantService.IsDateTime(Request.Form["StartDate"])) // check for valid date
                ModelState.AddModelError("StartDate", "Invalid date");
            else
                obj.StartDate = Convert.ToDateTime(Request.Form["StartDate"].ToString());
            if (obj.EndDate != null)
            {
                if (!AssistantService.IsDateTime(Request.Form["EndDate"])) // check for valid date
                    ModelState.AddModelError("EndDate", "Invalid date");
                else
                    obj.EndDate = Convert.ToDateTime(Request.Form["EndDate"].ToString());
            }
            if (obj.StartDate > DateTime.Today.Date)
                ModelState.AddModelError("StartDate", "Cannot be current date");
            if (obj.StartDate != null)
            {
                if (obj.StartDate >= obj.EndDate)
                    ModelState.AddModelError("StartDate", "Must be smaller than end date");
            }
            if (obj.CareerLevelID == 0)
                ModelState.AddModelError("CareerLevelID", "Mandatory ");
            if (obj.CurrentlyWorking == false)
            {
                if (obj.ReasonOfLeaving == null || obj.ReasonOfLeaving == "")
                    ModelState.AddModelError("ReasonOfLeaving", "Mandatory ");
            }
            if (obj.Address == null || obj.Address == "")
                ModelState.AddModelError("Address", "Mandatory ");
            if (obj.CountryID == 0)
                ModelState.AddModelError("CountryID", "Mandatory");
            if (obj.CountryID == 74)
            {
                obj.OtherCityName = null;
                if (obj.CityID == 0)
                    ModelState.AddModelError("CityID", "Mandatory");
            }
            if (obj.CountryID != 74)
            {
                if (obj.OtherCity == null || obj.OtherCity == "")
                    ModelState.AddModelError("OtherCity", "Mandatory");
            }
            if (obj.CityID == 117)
            {
                if (obj.OtherCityName == null || obj.OtherCityName == "")
                    ModelState.AddModelError("OtherCityName", "Mandatory");

            }
            if (ModelState.IsValid)
            {
                if (vmf.UserStage == 4)
                    vmf.UserStage = 5;
                ExperienceDetailService.PostCreate(obj, vmf);
                CandidateStep dbtickStep = CandidateStepEntityService.GetEdit(vmf.CandidateID);
                dbtickStep.StepThree = true;
                CandidateStepEntityService.PostEdit(dbtickStep);
                vmf.StepThree = dbtickStep.StepThree;
                Session["LoggedInUser"] = vmf;
                Session["ProfileStage"] = vmf.UserStage;
                return Json("OK", JsonRequestBehavior.AllowGet);
            }
            CreateHelper(obj);
            return PartialView("Create", obj);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            VMExperienceOperation obj = ExperienceDetailService.GetEdit((int)id);
            EditHelper(obj);
            return PartialView(obj);
        }
        [HttpPost]
        public ActionResult Edit(VMExperienceOperation obj)
        {
            ReadFromRadioButton(obj);
            V_UserCandidate vmf = Session["LoggedInUser"] as V_UserCandidate;
            if (obj.IndustryID == 0)
                ModelState.AddModelError("IndustryID", "Mandatory");
            if (obj.IndustryID != 110)
            {
                obj.OtherIndustryName = null;
                if (obj.IndustryID == 0)
                    ModelState.AddModelError("CityID", "Mandatory");
            }
            if (obj.IndustryID == 110)
            {
                if (obj.OtherIndustryName == null || obj.OtherIndustryName == "")
                    ModelState.AddModelError("OtherIndustryName", "Mandatory");
            }
            if (obj.JobTitle == null || obj.JobTitle == "")
                ModelState.AddModelError("PositionTitle", "Mandatory ");
            if (obj.EmployerName == null || obj.EmployerName == "")
                ModelState.AddModelError("EmployerName", "Mandatory ");
            if (obj.StartDate == null)
                ModelState.AddModelError("StartDate", "Mandatory ");
            if (obj.CurrentlyWorking == true)
            {
                obj.EndDate = null;
            }
            else
            {
                if (obj.EndDate == null)
                    ModelState.AddModelError("EndDate", "Mandatory ");
            }
            if (!AssistantService.IsDateTime(Request.Form["StartDate"])) // check for valid date
                ModelState.AddModelError("StartDate", "Invalid date");
            else
                obj.StartDate = Convert.ToDateTime(Request.Form["StartDate"].ToString());
            if (obj.EndDate != null)
            {
                if (!AssistantService.IsDateTime(Request.Form["EndDate"])) // check for valid date
                    ModelState.AddModelError("EndDate", "Invalid date");
                else
                    obj.EndDate = Convert.ToDateTime(Request.Form["EndDate"].ToString());
            }
            if (obj.StartDate > DateTime.Today.Date)
                ModelState.AddModelError("StartDate", "Cannot be current date");
            if (obj.StartDate != null)
            {
                if (obj.StartDate >= obj.EndDate)
                    ModelState.AddModelError("StartDate", "Must be smaller than end date");
            }
            if (obj.CareerLevelID == 0)
                ModelState.AddModelError("CareerLevelID", "Mandatory ");
            if (obj.CurrentlyWorking == false)
            {
                if (obj.ReasonOfLeaving == null || obj.ReasonOfLeaving == "")
                    ModelState.AddModelError("ReasonOfLeaving", "Mandatory ");
            }
            if (obj.Address == null || obj.Address == "")
                ModelState.AddModelError("Address", "Mandatory ");
            if (obj.CountryID == 0)
                ModelState.AddModelError("CountryID", "Mandatory");
            if (obj.CountryID == 74)
            {
                obj.OtherCity = null;
                if (obj.CityID == 0)
                    ModelState.AddModelError("CityID", "Mandatory");
            }
            if (obj.CountryID != 74)
            {
                if (obj.OtherCity == null || obj.OtherCity == "")
                    ModelState.AddModelError("OtherCity", "Mandatory");
            }
            if (obj.CityID == 117)
            {
                if (obj.OtherCityName == null || obj.OtherCityName == "")
                    ModelState.AddModelError("OtherCityName", "Mandatory");

            }
            if (ModelState.IsValid)
            {
                ExperienceDetailService.PostEdit(obj);
                return Json("OK", JsonRequestBehavior.AllowGet);
            }
            EditHelper(obj);
            return PartialView("Edit", obj);
        }
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            VMExperienceOperation vmOperation = ExperienceDetailService.GetDelete((int)id);
            EditHelper(vmOperation);
            return View(vmOperation);
        }
        [HttpPost]
        public ActionResult Delete(VMExperienceOperation obj)
        {
            ExperienceDetailService.PostDelete(obj);
            EditHelper(obj);
            return PartialView(obj);
        }
        [HttpPost]
        public ActionResult SaveExperienceDetail(bool? HaveExperience)
        {
            V_UserCandidate vmf = Session["LoggedInUser"] as V_UserCandidate;
            if (ModelState.IsValid)
            {
                Candidate obj = CandidateEntityService.GetEdit(vmf.CandidateID);
                obj.HaveExperience = HaveExperience;
                CandidateEntityService.PostEdit(obj);
                User dbuser = UserEntityService.GetEdit((int)vmf.UserID);
                dbuser.HaveExperience = HaveExperience;
                UserEntityService.PostEdit(dbuser);
                vmf.HaveExperience = obj.HaveExperience;
                Session["LoggedInUser"] = vmf;
                return Json("OK", JsonRequestBehavior.AllowGet);
            }
            return View();
        }
        #endregion
        #endregion
        #region -- Controller Private  Methods--
        private void ReadFromRadioButton(VMExperienceOperation obj)
        {

            #region -- Radio Buttons--
            // Adjust Bonus of Compensation Radio Button
            string radioContactEmployerValue = "";

            var ContactEmployerValue = ValueProvider.GetValue("ContactSelection");
            if (ContactEmployerValue != null)
            {
                if (radioContactEmployerValue != null)
                {
                    radioContactEmployerValue = ContactEmployerValue.AttemptedValue;
                }
                if (radioContactEmployerValue == "ContactEmployerYes")
                {
                    obj.ContactEmployerYes = "YES";
                    obj.ContactEmployerNo = "NO";
                }
                if (radioContactEmployerValue == "ContactEmployerNo")
                {
                    obj.ContactEmployerYes = "NO";
                    obj.ContactEmployerNo = "YES";
                }
            }
            #endregion
        }
        private void ReadFromRadioButtonExperience(VMExperienceIndex dbOperation)
        {
            #region -- Radio Buttons--
            // Adjust Bonus of Compensation Radio Button
            string radioHaveExperienceValue = "";

            var HaveExperienceValue = ValueProvider.GetValue("ExperienceSelection");
            if (HaveExperienceValue != null)
            {
                if (radioHaveExperienceValue != null)
                {
                    radioHaveExperienceValue = HaveExperienceValue.AttemptedValue;
                }
                if (radioHaveExperienceValue == "HaveExperienceYes")
                {
                    dbOperation.HaveExperienceYes = "YES";
                    dbOperation.HaveExperienceNo = "NO";
                }
                if (radioHaveExperienceValue == "HaveExperienceNo")
                {
                    dbOperation.HaveExperienceYes = "NO";
                    dbOperation.HaveExperienceNo = "YES";
                }
            }
            else
            {
                dbOperation.HaveExperienceYes = "NO";
                dbOperation.HaveExperienceNo = "NO";
            }
            #endregion
        }
        private void CreateHelper(VMExperienceOperation obj)
        {
            ViewBag.CountryID = new SelectList(DDService.GetCountryList().ToList().OrderBy(aa => aa.CCID).ToList(), "CCID", "CountryName", obj.CountryID);
            ViewBag.IndustryID = new SelectList(DDService.GetIndustryList().ToList().OrderBy(aa => aa.ExpIndustryName).ToList(), "ExpIndustryID", "ExpIndustryName");
            ViewBag.CityID = new SelectList(DDService.GetCityList().ToList().OrderBy(aa => aa.CityID).ToList(), "CityID", "CityName");
            ViewBag.CareerLevelID = new SelectList(DDService.GetCareerLevelList().ToList().OrderBy(aa => aa.CLevelID).ToList(), "CLevelID", "CareerLevelName");
        }
        private void EditHelper(VMExperienceOperation obj)
        {
            ViewBag.CountryID = new SelectList(DDService.GetCountryList().ToList().OrderBy(aa => aa.CCID).ToList(), "CCID", "CountryName", obj.CountryID);
            ViewBag.IndustryID = new SelectList(DDService.GetIndustryList().ToList().OrderBy(aa => aa.ExpIndustryID).ToList(), "ExpIndustryID", "ExpIndustryName", obj.IndustryID);
            ViewBag.CityID = new SelectList(DDService.GetCityList().ToList().OrderBy(aa => aa.CityID).ToList(), "CityID", "CityName", obj.CityID);
            ViewBag.CareerLevelID = new SelectList(DDService.GetCareerLevelList().ToList().OrderBy(aa => aa.CLevelID).ToList(), "CLevelID", "CareerLevelName", obj.CareerLevelID);
        }
        #endregion
    }
}