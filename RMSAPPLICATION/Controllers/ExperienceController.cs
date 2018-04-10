using RMSCORE.EF;
using RMSCORE.Models.Main;
using RMSCORE.Models.Operation;
using RMSSERVICES.Experience;
using RMSSERVICES.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RMSAPPLICATION.Controllers
{
    public class ExperienceController : Controller
    {
        #region -- Controller Initialization --
        //IEntityService<VMEduDetailIndex> EduDetailEntityService;
        IExperienceDetailService ExperienceDetailService;
        IEntityService<Candidate> CandidateEntityService;
        IEntityService<User> UserEntityService;
        IDDService DDService;
        // Controller Constructor
        public ExperienceController(IExperienceDetailService experiencedetailService, IEntityService<User> userEntityService, IEntityService<Candidate> candidateEntityService, IDDService ddService)
        {
            DDService = ddService;
            ExperienceDetailService = experiencedetailService;
            CandidateEntityService = candidateEntityService;
            UserEntityService = userEntityService;
        }
        #endregion
        #region -- Controller ActionResults  --
        // GET: ExperienceDetail
        public ActionResult Index()
        {
            V_UserCandidate vmf = Session["LoggedInUser"] as V_UserCandidate;
            int cid = vmf.CandidateID;
            List<VMExperienceIndex> vmlist = ExperienceDetailService.GetIndex(cid);
            return View(vmlist);
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
            V_UserCandidate vmf = Session["LoggedInUser"] as V_UserCandidate;
            if (obj.PositionTitle == null || obj.PositionTitle == "")
                ModelState.AddModelError("PositionTitle", "Mandatory !!");
            if (obj.EmployerName == null || obj.EmployerName == "")
                ModelState.AddModelError("EmployerName", "Mandatory !!");
            if (obj.StartDate == null)
                ModelState.AddModelError("StartDate", "Mandatory !!");
            if (obj.StartDate > DateTime.Today)
                ModelState.AddModelError("StartDate","Start date greater then current date");
            if (obj.StartDate != null && obj.EndDate != null)
            {
                if (obj.EndDate < obj.StartDate)
                {
                    ModelState.AddModelError("StartDate", "Start date can never be greater than end date.");
                }
            }
            if (obj.AreaofInterest == null || obj.AreaofInterest == "")
                ModelState.AddModelError("AreaofInterest", "Mandatory !!");
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
            return View(vmOperation);
        }
        [HttpPost]
        public ActionResult Delete(VMExperienceOperation obj)
        {
            ExperienceDetailService.PostDelete(obj);
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
        private void CreateHelper(VMExperienceOperation obj)
        {
            List<ExperienceIndustry> dbExpIndutries = DDService.GetIndustryList().ToList().OrderBy(aa => aa.ExpIndustryID).ToList();
            dbExpIndutries.Insert(0, new ExperienceIndustry { ExpIndustryID = 0, ExpIndustryName = "All" });
            ViewBag.IndustryID = new SelectList(dbExpIndutries.ToList().OrderBy(aa => aa.ExpIndustryID).ToList(), "ExpIndustryID", "ExpIndustryName");
            List<City> dbCities = DDService.GetCityList().ToList().OrderBy(aa => aa.CityID).ToList();
            dbCities.Insert(0, new City { CityID = 0, CityName = "All" });
            ViewBag.CityID = new SelectList(dbCities.ToList().OrderBy(aa => aa.CityID).ToList(), "CityID", "CityName");
            List<ExpCareerLevel> dbCareerlevels = DDService.GetCareerLevelList().ToList().OrderBy(aa => aa.CLevelID).ToList();
            dbCareerlevels.Insert(0, new ExpCareerLevel { CLevelID = 0, CareerLevelName = "All" });
            ViewBag.CareerLevelID = new SelectList(dbCareerlevels.ToList().OrderBy(aa => aa.CLevelID).ToList(), "CLevelID", "CareerLevelName");
        }
        private void EditHelper(VMExperienceOperation obj)
        {
            ViewBag.IndustryID = new SelectList(DDService.GetIndustryList().ToList().OrderBy(aa => aa.ExpIndustryID).ToList(), "ExpIndustryID", "ExpIndustryName", obj.IndustryID);
            ViewBag.CityID = new SelectList(DDService.GetCityList().ToList().OrderBy(aa => aa.CityID).ToList(), "CityID", "CityName", obj.CityID);
            ViewBag.CareerLevelID = new SelectList(DDService.GetCareerLevelList().ToList().OrderBy(aa => aa.CLevelID).ToList(), "CLevelID", "CareerLevelName", obj.CareerLevelID);
        }
        #endregion
    }
}