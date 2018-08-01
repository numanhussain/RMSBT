using Newtonsoft.Json;
using RMSCORE.EF;
using RMSCORE.Models.Helper;
using RMSCORE.Models.Main;
using RMSREPO.Generic;
using RMSSERVICES.Generic;
using RMSSERVICES.Job;
using RMSSERVICES.UserDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using CaptchaMvc.HtmlHelpers;
using System.IO;
using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Drawing2D;
using System.ComponentModel.DataAnnotations;
using RMSCORE.Models.Other;
using RMSAPPLICATION.Models;

namespace RMSAPPLICATION.Controllers
{
    public class TestController : Controller
    {
        #region -- Controller Initialization --
        IEntityService<User> UserEntityService;
        IEntityService<Candidate> CandidateEntityService;
        IEntityService<V_UserCandidate> VUserEntityService;
        IEntityService<CandidateStep> CandidateStepEntityService;
        IEntityService<Location> LocationService;
        IEntityService<Catagory> CatagoryService;
        IRepository<User> UserRepository;
        IUserService UserService;
        IJobService JobService;
        IDDService DDService;
        // Controller Constructor
        public TestController(IEntityService<User> userEntityService, IRepository<User> userRepository, IJobService jobService, IEntityService<Location> locationService, IEntityService<Catagory> catagoryService, IUserService userService, IDDService ddService, IEntityService<V_UserCandidate> vUserEntityService,
            IEntityService<Candidate> candidateEntityService, IEntityService<CandidateStep> candidateStepEntityService)
        {
            UserEntityService = userEntityService;
            UserService = userService;
            DDService = ddService;
            VUserEntityService = vUserEntityService;
            LocationService = locationService;
            CatagoryService = catagoryService;
            JobService = jobService;
            CandidateEntityService = candidateEntityService;
            UserRepository = userRepository;
            CandidateStepEntityService = candidateStepEntityService;
        }
        #endregion
        #region -- Controller ActionResults  -- 
        [HttpGet]
        public ActionResult Index()
        {
            List<VMOpenJobIndex> vm = JobService.JobIndex();
            ViewBag.LocationID = new SelectList(DDService.GetLocationList().ToList().OrderBy(aa => aa.PLocationID).ToList(), "PLocationID", "LocName");
            ViewBag.CatagoryID = new SelectList(DDService.GetCatagoryList().ToList().OrderBy(aa => aa.PCatagoryID).ToList(), "PCatagoryID", "CatName");
            //ViewBag.LocationID = GetLocationList(LocationService.GetIndex().Select(aa => aa.LocName).Distinct().ToList());
            //ViewBag.CatagoryID = GetCatagoryList(CatagoryService.GetIndex().Select(aa => aa.CatName).Distinct().ToList());
            return View(vm);
        }
        [HttpPost]
        public ActionResult IndexSubmit(int? LocationID, int? CatagoryID)
        {
            List<VMOpenJobIndex> vmAllJobList = JobService.JobIndex();
            if (LocationID == 0 && CatagoryID == 0)
            {
                vmAllJobList = vmAllJobList.ToList();
            }
            if (LocationID > 0)
            {
                vmAllJobList = vmAllJobList.Where(aa => aa.LocID == LocationID).ToList();
            }
            if (CatagoryID > 0)
            {
                vmAllJobList = vmAllJobList.Where(aa => aa.CatagoryID == CatagoryID).ToList();
            }
            ViewBag.LocationID = new SelectList(DDService.GetLocationList().ToList().OrderBy(aa => aa.PLocationID).ToList(), "PLocationID", "LocName", LocationID);
            ViewBag.CatagoryID = new SelectList(DDService.GetCatagoryList().ToList().OrderBy(aa => aa.PCatagoryID).ToList(), "PCatagoryID", "CatName", CatagoryID);
            //ViewBag.LocationID = GetLocationList(LocationService.GetIndex().Select(aa => aa.LocName).Distinct().ToList());
            //ViewBag.CatagoryID = GetCatagoryList(CatagoryService.GetIndex().Select(aa => aa.CatName).Distinct().ToList());
            return PartialView("PVDTBody", vmAllJobList);
        }
        public ActionResult WebGrid()
        {
            List<VMOpenJobIndex> vm = JobService.JobIndex();
            ViewBag.LocationID = new SelectList(DDService.GetLocationList().ToList().OrderBy(aa => aa.PLocationID).ToList(), "PLocationID", "LocName");
            ViewBag.CatagoryID = new SelectList(DDService.GetCatagoryList().ToList().OrderBy(aa => aa.PCatagoryID).ToList(), "PCatagoryID", "CatName");
            return View(vm);

        }
        #region -- Controller Main View Actions  --
        #endregion
        #endregion
        #region -- Controller Private  Methods--
        private List<CustomModel> GetCatagoryList(List<string> list)
        {
            List<CustomModel> cmList = new List<CustomModel>();
            foreach (var item in list)
            {
                CustomModel cm = new CustomModel();
                cm.ID = item;
                cm.Name = item;
                cm.IsSelected = false;
                cmList.Add(cm);
            }
            return cmList;
        }
        private List<CustomModel> GetLocationList(List<string> list)
        {
            List<CustomModel> cmList = new List<CustomModel>();
            foreach (var item in list)
            {
                CustomModel cm = new CustomModel();
                cm.ID = item;
                cm.Name = item;
                cm.IsSelected = false;
                cmList.Add(cm);
            }
            return cmList;
        }
        #endregion
    }
}