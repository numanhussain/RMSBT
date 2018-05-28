﻿using RMSCORE.EF;
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
        IExperienceDetailService ExperienceDetailService;
        IEntityService<CementExperience> CementExperienceService;
        IEntityService<Candidate> CandidateEntityService;
        IEntityService<User> UserEntityService;
        IEntityService<MiscellaneousDetail> MiscellaneousDetailService;
        IMiscellaneousService MiscellaneousService;
        IDDService DDService;
        // Controller Constructor
        public ExperienceController(IExperienceDetailService experiencedetailService, IEntityService<User> userEntityService, IEntityService<Candidate> candidateEntityService, IDDService ddService,
             IEntityService<CementExperience> cementExperienceService, IMiscellaneousService miscellaneousService, IEntityService<MiscellaneousDetail> miscellaneousDetailService)
        {
            DDService = ddService;
            ExperienceDetailService = experiencedetailService;
            CandidateEntityService = candidateEntityService;
            UserEntityService = userEntityService;
            CementExperienceService = cementExperienceService;
            MiscellaneousService = miscellaneousService;
            MiscellaneousDetailService = miscellaneousDetailService;
        }
        #endregion
        #region -- Controller ActionResults  --
        // GET: ExperienceDetail
        public ActionResult Index()
        {
            V_UserCandidate vmf = Session["LoggedInUser"] as V_UserCandidate;
            int cid = vmf.CandidateID;
            List<VMExperienceIndex> vmlist = ExperienceDetailService.GetIndex(cid);
            Expression<Func<MiscellaneousDetail, bool>> SpecificPosition3 = c => c.CandidateID == cid;
            if (MiscellaneousDetailService.GetIndexSpecific(SpecificPosition3).Count > 0)
            {
                List<MiscellaneousDetail> dbVRMPositions = MiscellaneousDetailService.GetIndexSpecific(SpecificPosition3);
                MiscellaneousDetail dbVRMPosition = dbVRMPositions.First();
                ViewBag.Experience = dbVRMPosition.TotalExp;
                ViewBag.CementExperience = dbVRMPosition.CementExp;
            }
            return View(vmlist);
        }

        public ActionResult SaveOverallExperience(int? Experience)
        {
            V_UserCandidate vmf = Session["LoggedInUser"] as V_UserCandidate;
            int cid = vmf.CandidateID;
            ExperienceDetailService.PostGeneralExperience(Experience, vmf);
            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveCementExperience(int? Experience)
        {
            V_UserCandidate vmf = Session["LoggedInUser"] as V_UserCandidate;
            int cid = vmf.CandidateID;
            ExperienceDetailService.PostCementExperience(Experience, vmf);
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
            if (obj.CurrentlyWorking == true)
            {
                obj.EndDate = null;
            }
            if (obj.IndustryID == 0)
                ModelState.AddModelError("IndustryID", "Mandatory ");
            if (obj.JobTitle == null || obj.JobTitle == "")
                ModelState.AddModelError("PositionTitle", "Mandatory ");
            if (obj.EmployerName == null || obj.EmployerName == "")
                ModelState.AddModelError("EmployerName", "Mandatory ");
            if (obj.StartDate == null)
                ModelState.AddModelError("StartDate", "Mandatory ");
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
            if (obj.CountryID == 74)
            {
                obj.OtherCityName = null;
                if (obj.CityID == 0)
                    ModelState.AddModelError("CityID", "Mandatory");
            }
            if (obj.CountryID != 74)
            {
                obj.CityID = null;
                if (obj.OtherCityName == null || obj.OtherCityName == "")
                    ModelState.AddModelError("OtherCityName", "Mandatory");
            }
            if (obj.CountryID == 0)
                ModelState.AddModelError("CountryID", "Mandatory");
            if (ModelState.IsValid)
            {
                if (vmf.UserStage == 4)
                    vmf.UserStage = 5;
                ExperienceDetailService.PostCreate(obj, vmf);
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
            if (obj.CurrentlyWorking == true)
            {
                obj.EndDate = null;
            }
            if (obj.IndustryID == 0)
                ModelState.AddModelError("IndustryID", "Mandatory ");
            if (obj.JobTitle == null || obj.JobTitle == "")
                ModelState.AddModelError("PositionTitle", "Mandatory ");
            if (obj.EmployerName == null || obj.EmployerName == "")
                ModelState.AddModelError("EmployerName", "Mandatory ");
            if (obj.StartDate == null)
                ModelState.AddModelError("StartDate", "Mandatory ");
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
            if (obj.CountryID == 74)
            {
                obj.OtherCityName = null;
                if (obj.CityID == 0)
                    ModelState.AddModelError("CityID", "Mandatory");
            }
            if (obj.CountryID != 74)
            {
                obj.CityID = null;
                if (obj.OtherCityName == null || obj.OtherCityName == "")
                    ModelState.AddModelError("OtherCityName", "Mandatory");
            }
            if (obj.CountryID == 0)
                ModelState.AddModelError("CountryID", "Mandatory");

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
            else
            {
                obj.ContactEmployerYes = "NO";
                obj.ContactEmployerNo = "NO";
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