using RMSCORE.EF;
using RMSCORE.Models.Helper;
using RMSCORE.Models.Main;
using RMSSERVICES.Generic;
using RMSSERVICES.Job;
using RMSSERVICES.UserDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace RMSAPPLICATION.Controllers
{
    public class HomeController : Controller
    {
        #region -- Controller Initialization --
        IEntityService<User> UserEntityService;
        IEntityService<V_UserCandidate> VUserEntityService;
        IEntityService<Location> LocationService;
        IEntityService<Catagory> CatagoryService;
        IUserService UserService;
        IJobService JobService;
        IDDService DDService;
        // Controller Constructor
        public HomeController(IEntityService<User> userEntityService, IJobService jobService, IEntityService<Location> locationService, IEntityService<Catagory> catagoryService, IUserService userService, IDDService ddService, IEntityService<V_UserCandidate> vUserEntityService)
        {
            UserEntityService = userEntityService;
            UserService = userService;
            DDService = ddService;
            VUserEntityService = vUserEntityService;
            LocationService = locationService;
            CatagoryService = catagoryService;
            JobService = jobService;
        }
        #endregion
        #region -- Controller ActionResults  -- 
        [HttpGet]
        public ActionResult Index()
        {
            V_UserCandidate vmf = Session["LoggedInUser"] as V_UserCandidate;
            List<VMOpenJobIndex> vm = JobService.JobIndex();
            List<Location> dbLocations = DDService.GetLocationList().ToList().OrderBy(aa => aa.LocName).ToList();
            dbLocations.Insert(0, new Location { PLocationID = 0, LocName = "All" });
            List<Catagory> dbCatagories = DDService.GetCatagoryList().ToList().OrderBy(aa => aa.CatName).ToList();
            dbCatagories.Insert(0, new Catagory { PCatagoryID = 0, CatName = "All" });
            ViewBag.LocationID = new SelectList(dbLocations.ToList().OrderBy(aa => aa.PLocationID).ToList(), "PLocationID", "LocName");
            ViewBag.CatagoryID = new SelectList(dbCatagories.ToList().OrderBy(aa => aa.PCatagoryID).ToList(), "PCatagoryID", "CatName");
            //ViewBag.LocationID = GetLocationList(LocationService.GetIndex().Select(aa => aa.LocName).Distinct().ToList());
            //ViewBag.CatagoryID = GetCatagoryList(CatagoryService.GetIndex().Select(aa => aa.CatName).Distinct().ToList());
            return View(vm);
        }
        [HttpPost]
        public ActionResult IndexSubmit(int? LocationID, int? CatagoryID, string FilterBox)
        {
            List<VMOpenJobIndex> vmAllJobList = JobService.JobIndex();
            if (FilterBox != "")
                vmAllJobList = vmAllJobList.Where(aa => aa.JobTitle == FilterBox).ToList();
            if (LocationID > 0)
            {
                vmAllJobList = vmAllJobList.Where(aa => aa.LocID == LocationID).ToList();
            }
            if (CatagoryID > 0)
            {
                vmAllJobList = vmAllJobList.Where(aa => aa.CatagoryID == CatagoryID).ToList();
            }
            List<Location> dbLocations = DDService.GetLocationList().ToList().OrderBy(aa => aa.LocName).ToList();
            dbLocations.Insert(0, new Location { PLocationID = 0, LocName = "All" });
            List<Catagory> dbCatagories = DDService.GetCatagoryList().ToList().OrderBy(aa => aa.CatName).ToList();
            dbCatagories.Insert(0, new Catagory { PCatagoryID = 0, CatName = "All" });
            ViewBag.LocationID = new SelectList(dbLocations.ToList().OrderBy(aa => aa.PLocationID).ToList(), "PLocationID", "LocName", LocationID);
            ViewBag.CatagoryID = new SelectList(dbCatagories.ToList().OrderBy(aa => aa.PCatagoryID).ToList(), "PCatagoryID", "CatName", CatagoryID);
            //ViewBag.LocationID = GetLocationList(LocationService.GetIndex().Select(aa => aa.LocName).Distinct().ToList());
            //ViewBag.CatagoryID = GetCatagoryList(CatagoryService.GetIndex().Select(aa => aa.CatName).Distinct().ToList());
            return View("Index", vmAllJobList);
        }
        #region -- Controller Main View Actions  --
        [HttpGet]
        public ActionResult Login()
        {
            //return RedirectToAction("Index","ReportManager",new { @area="Reporting"});
            return View();
        }
        [HttpPost]
        public ActionResult Login(User Obj)
        {
            if (VUserEntityService.GetIndex().Where(aa => aa.UserName == Obj.UserName && aa.Password == Obj.Password).Count() > 0)
            {
                V_UserCandidate vm = VUserEntityService.GetIndex().First(aa => aa.UserName == Obj.UserName && aa.Password == Obj.Password);
                Expression<Func<V_UserCandidate, bool>> SpecificEntries = c => c.UserID == vm.UserID;
                Session["LoggedInUser"] = VUserEntityService.GetIndexSpecific(SpecificEntries).First();
                if (vm.UserStage == "1")
                {
                    return RedirectToAction("Index", "Job");
                }
                if (vm.UserStage == "")
                {
                    return RedirectToAction("Login", "Home");
                }
            }
            return View();

        }
        [HttpGet]
        public ActionResult RegisterUser()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterUser(User Obj)
        {
            if (Obj.Password != Obj.RetypePassword)
            {
                return RedirectToAction("RegisterUser");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    UserService.RegisterUser(Obj);
                    //var code = Obj.SecurityLink;
                    //var callbackUrl = Url.Action("VerifyLink", "Home", new { User = Obj.UserID, code = code }, protocol: Request.Url.Scheme);
                    //EmailGenerate.SendEmail(Obj.Email, "", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>", "Account Activation");
                    Expression<Func<V_UserCandidate, bool>> SpecificEntries = c => c.UserID == Obj.UserID && c.Password == Obj.Password;
                    Session["LoggedInUser"] = VUserEntityService.GetIndexSpecific(SpecificEntries).First();
                    return RedirectToAction("Login", "Home");
                }
            }

            return View(Obj);
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult VerifyLink(string User)
        {
            if (UserService.VerifyLink(User))
            {
                User user = UserEntityService.GetIndex().Where(aa => aa.SecurityLink == User).First();

                V_UserCandidate vm = VUserEntityService.GetIndex().First(aa => aa.UserName == user.UserName && aa.Password == user.Password);
                vm.UserStage = "1";
                Expression<Func<V_UserCandidate, bool>> SpecificEntries = c => c.UserID == vm.UserID;
                Session["LoggedInUser"] = VUserEntityService.GetIndexSpecific(SpecificEntries).First();
                return RedirectToAction("EmailConfirm", "Home");
            }
            else
            {
                return View();
            }
        }
        [HttpGet]
        public ActionResult ForgetPassword()
        {
            // throw new System.ArgumentException("Parameter cannot be null", "original");
            return View();
        }
        public ActionResult LogOut()
        {
            Session["LoggedInUser"] = null;
            return View("Login");
        }
        public ActionResult ChangePass()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ChangePass(FormCollection form)
        {
            string newPassword = Request.Form["newPassword"].ToString();
            string ConfirmnewPassword = Request.Form["ConfirmnewPassword"].ToString();
            if (newPassword == "")
            {
                ViewBag.Message = "Please Enter Password";
                return View("ChangePass");
            }
            if (ConfirmnewPassword == "")
            {
                ViewBag.Message = "Please Re-Confirm Password";
                return View("ChangePass");
            }
            if (newPassword == ConfirmnewPassword)
            {
                V_UserCandidate vmf = Session["LoggedInUser"] as V_UserCandidate;
                User user = UserEntityService.GetEdit((int)vmf.UserID);
                user.Password = newPassword;
                UserEntityService.PostEdit(user);
            }
            ViewBag.Message = "Password Not Matched!";
            return RedirectToAction("Login");
        }
        [HttpGet]
        public ActionResult EmailSent()
        {
            return View();
        }
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