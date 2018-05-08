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
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace RMSAPPLICATION.Controllers
{
    public class HomeController : Controller
    {
        #region -- Controller Initialization --
        IEntityService<User> UserEntityService;
        IEntityService<Candidate> CandidateEntityService;
        IEntityService<V_UserCandidate> VUserEntityService;
        IEntityService<Location> LocationService;
        IEntityService<Catagory> CatagoryService;
        IUserService UserService;
        IJobService JobService;
        IDDService DDService;
        // Controller Constructor
        public HomeController(IEntityService<User> userEntityService, IJobService jobService, IEntityService<Location> locationService, IEntityService<Catagory> catagoryService, IUserService userService, IDDService ddService, IEntityService<V_UserCandidate> vUserEntityService,
            IEntityService<Candidate> candidateEntityService)
        {
            UserEntityService = userEntityService;
            UserService = userService;
            DDService = ddService;
            VUserEntityService = vUserEntityService;
            LocationService = locationService;
            CatagoryService = catagoryService;
            JobService = jobService;
            CandidateEntityService = candidateEntityService;
        }
        #endregion
        #region -- Controller ActionResults  -- 
        [HttpGet]
        public ActionResult Index()
        {

            V_UserCandidate vmf = Session["LoggedInUser"] as V_UserCandidate;
            List<VMOpenJobIndex> vm = JobService.JobIndex();
            ViewBag.LocationID = new SelectList(DDService.GetLocationList().ToList().OrderBy(aa => aa.PLocationID).ToList(), "PLocationID", "LocName");
            ViewBag.CatagoryID = new SelectList(DDService.GetCatagoryList().ToList().OrderBy(aa => aa.PCatagoryID).ToList(), "PCatagoryID", "CatName");
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
            ViewBag.LocationID = new SelectList(DDService.GetLocationList().ToList().OrderBy(aa => aa.PLocationID).ToList(), "PLocationID", "LocName", LocationID);
            ViewBag.CatagoryID = new SelectList(DDService.GetCatagoryList().ToList().OrderBy(aa => aa.PCatagoryID).ToList(), "PCatagoryID", "CatName", CatagoryID);
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
            if (Obj.UserName == null || Obj.UserName == "")
                ModelState.AddModelError("UserName", "Mandatory");
            if (Obj.UserName != null)
            {
                Match match = Regex.Match(Obj.UserName, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                if (!match.Success)
                {
                    ModelState.AddModelError("UserName", "Enter a valid Email address.");
                }
            }
            if (UserEntityService.GetIndex().Where(aa => aa.UserName == Obj.UserName && aa.Password == Obj.Password && aa.UserStage > 1).Count() > 0)
            {
                V_UserCandidate vm = VUserEntityService.GetIndex().First(aa => aa.Email == Obj.UserName && aa.Password == Obj.Password);
                Expression<Func<V_UserCandidate, bool>> SpecificEntries = c => c.UserID == vm.UserID;
                Session["LoggedInUser"] = VUserEntityService.GetIndexSpecific(SpecificEntries).First();
                if (vm.UserStage == 8)
                {
                    return RedirectToAction("Index", "Job");
                }
                else
                {
                    return RedirectToAction("Index", "Candidate");
                }
            }
            else
            {
                //ModelState.AddModelError("UserName", "Please Activate You Email address");
                ModelState.AddModelError("Password", "The username or password is incorrect");
            }
            return View("Login",Obj);

        }
        // 1: SignUp
        // 2: Login
        // 3: Personal Profile
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
            V_UserCandidate vmf = Session["LoggedInUser"] as V_UserCandidate;
            if (Obj.Password != Obj.RetypePassword)
            {
                return RedirectToAction("RegisterUser");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    Obj.UserName = Obj.Email;
                    Expression<Func<User, bool>> SpecificEntries1 = c => c.Email == Obj.Email;
                    if (UserEntityService.GetIndexSpecific(SpecificEntries1).ToList().Count == 0)
                    {
                        UserService.RegisterUser(Obj, vmf);
                        var code = Obj.SecurityLink;
                        var callbackUrl = Url.Action("VerifyLink", "Home", new { User = code }, protocol: Request.Url.Scheme);
                        EmailGenerate.SendEmail(Obj.Email, "", "<html><head><meta content=\"text/html; charset = utf - 8\" /></head><body><p>Dear Candidate, " + " </p>" +
                            "<p>This is with reference to your request for creating online profile at Bestway Career Portal. </p>" +
                            "<p>Please click <a href=\"" + callbackUrl + "\">here</a> to activate your profile.</p>" +
                            "<div>Best Regards</div><div>Talent Acquisition Team</div><div>Bestway Cement Limited</div></body></html>", "Email Verification", "");
                        Expression<Func<V_UserCandidate, bool>> SpecificEntries = c => c.UserID == Obj.UserID && c.Password == Obj.Password;
                        Session["LoggedInUser"] = VUserEntityService.GetIndexSpecific(SpecificEntries).First();
                        return RedirectToAction("EmailSent", "Home");
                    }
                    else
                    {
                        // Show Message
                    }

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
                User user = UserEntityService.GetIndex().Where(aa => aa.UserStage == 1 && aa.SecurityLink == User).First();

                V_UserCandidate vm = VUserEntityService.GetIndex().First(aa => aa.UserName == user.UserName && aa.Password == user.Password);
                user.UserStage = 2;
                UserEntityService.PostEdit(user);
                Expression<Func<V_UserCandidate, bool>> SpecificEntries = c => c.UserID == vm.UserID;
                Session["LoggedInUser"] = VUserEntityService.GetIndexSpecific(SpecificEntries).First();
                return RedirectToAction("ConfirmedEmail", "Home");
            }
            else
            {
                ViewBag.Message = "Link is not valid or expired";
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
        [HttpGet]
        public ActionResult ConfirmedEmail()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UpdateAppliedAs(int? id)
        {
            V_UserCandidate vmf = Session["LoggedInUser"] as V_UserCandidate;
            vmf.AppliedAs = id;
            Expression<Func<User, bool>> SpecificEntries1 = c => c.UserID == vmf.UserID;
            User user = UserEntityService.GetEdit((int)vmf.UserID);
            user.AppliedAs = id;
            UserEntityService.PostEdit(user);
            Candidate candidate = CandidateEntityService.GetEdit((int)vmf.CandidateID);
            candidate.AppliedAs = id;
            CandidateEntityService.PostEdit(candidate);
            Session["LoggedInUser"] = vmf;
            return Json("OK", JsonRequestBehavior.AllowGet);
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