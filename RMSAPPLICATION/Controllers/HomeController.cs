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
    public class HomeController : Controller
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
        public HomeController(IEntityService<User> userEntityService, IRepository<User> userRepository, IJobService jobService, IEntityService<Location> locationService, IEntityService<Catagory> catagoryService, IUserService userService, IDDService ddService, IEntityService<V_UserCandidate> vUserEntityService,
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
            if (Obj.Password == null || Obj.Password == "")
                ModelState.AddModelError("Password", "Mandatory");
            if (ModelState.IsValid)
            {
                Expression<Func<User, bool>> SpecificEntries = c => c.UserName == Obj.UserName;
                User dbUsers = UserEntityService.GetIndexSpecific(SpecificEntries).First();
                string EncryptPassword;
                String DecryptPassword = StringCipher.Decrypt(dbUsers.Password);
                EncryptPassword = StringCipher.Encrypt(Obj.Password);
                if (UserEntityService.GetIndexSpecific(aa => aa.UserName == Obj.UserName && aa.Password == EncryptPassword && aa.UserStage > 1).Count() > 0)
                {
                    V_UserCandidate vm = VUserEntityService.GetIndexSpecific(aa => aa.Email == Obj.UserName && aa.Password == EncryptPassword).First();
                    Expression<Func<V_UserCandidate, bool>> SpecificEntries2 = c => c.UserID == vm.UserID;
                    Session["LoggedInUser"] = VUserEntityService.GetIndexSpecific(SpecificEntries2).First();
                    if (vm.UserStage == 8)
                    {
                        return RedirectToAction("Index", "Job");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Instruction");
                    }
                }
                else
                {
                    ModelState.AddModelError("Password", "The username or password is incorrect");
                }
            }
            return View("Login", Obj);
        }
        // 1: SignUp
        // 2: Login
        // 3: Personal Profile
        [HttpGet]
        public ActionResult RegisterUser()
        {
            ViewBag.CatagoryID = new SelectList(DDService.GetCatagoryList().ToList().OrderBy(aa => aa.PCatagoryID).ToList(), "PCatagoryID", "CatName");
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterUser(UserModel vmUserModel)
        {
            V_UserCandidate vmf = Session["LoggedInUser"] as V_UserCandidate;
            if (vmUserModel.Password != vmUserModel.RetypePassword)
            {
                ModelState.AddModelError("Password", "Password not matched");
            }
            if (vmUserModel.Email == null || vmUserModel.Email == "")
                ModelState.AddModelError("Email", "Mandatory");
            if (Session["Captcha"] == null || Session["Captcha"].ToString() != vmUserModel.Captcha)
            {
                ModelState.AddModelError("Captcha", "Captcha Entry Wrong");
                //dispay error and generate a new captcha 
            }
            Expression<Func<User, bool>> SpecificEntries1 = c => c.Email == vmUserModel.Email;
            //CaptchaResponse response = ValidateCaptcha(Request["g-recaptcha-response"]);
            if (UserEntityService.GetIndexSpecific(SpecificEntries1).ToList().Count == 0)
            {
                if (ModelState.IsValid)
                {
                    vmUserModel.UserName = vmUserModel.Email;
                    {
                        UserService.RegisterUser(vmUserModel, vmf);

                        var code = vmUserModel.SecurityLink;
                        var callbackUrl = Url.Action("VerifyLink", "Home", new { User = code }, protocol: Request.Url.Scheme);
                        EmailGenerate.SendEmail(vmUserModel.Email, "", "<html><head><meta content=\"text/html; charset = utf - 8\" /></head><body><p>Dear Candidate, " + " </p>" +
                            "<p>This is with reference to your request for creating online profile at Bestway Career Portal. </p>" +
                            "<p>Please click <a href=\"" + callbackUrl + "\">here</a> to activate your profile.</p>" +
                            "<div>Best Regards</div><div>Talent Acquisition Team</div><div>Bestway Cement Limited</div></body></html>", "", "Email Verification");
                        Expression<Func<V_UserCandidate, bool>> SpecificEntries = c => c.UserID == vmUserModel.UserID && c.Password == vmUserModel.Password;
                        Session["LoggedInUser"] = VUserEntityService.GetIndexSpecific(SpecificEntries).First();
                        return RedirectToAction("EmailSent", "Home");
                    }
                }
            }
            else
            {
                ModelState.AddModelError("Email", "Already registered account");
            }
            ViewBag.CatagoryID = new SelectList(DDService.GetCatagoryList().ToList().OrderBy(aa => aa.PCatagoryID).ToList(), "PCatagoryID", "CatName", vmUserModel.CatagoryID);
            return View("RegisterUser", vmUserModel);
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
                Candidate dbCandidate = CandidateEntityService.GetEdit(vm.CandidateID);
                CandidateStep dbTickSteps = new CandidateStep();
                dbTickSteps.StepOne = false;
                dbTickSteps.StepTwo = false;
                dbTickSteps.StepThree = false;
                dbTickSteps.StepFour = false;
                dbTickSteps.StepFive = false;
                dbTickSteps.StepSix = false;
                dbTickSteps.StepSeven = false;
                dbTickSteps.StepEight = false;
                dbTickSteps.CandidateID = vm.CandidateID;
                CandidateStepEntityService.PostCreate(dbTickSteps);
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
        [HttpPost]
        public ActionResult ForgetPassword(User dbuser)
        {
            if (dbuser.Email == null || dbuser.Email == "")
                ModelState.AddModelError("Email", "Mandatory");
            Expression<Func<User, bool>> SpecificEntries = c => c.Email == dbuser.Email;
            List<User> dbUsers = UserEntityService.GetIndexSpecific(SpecificEntries);
            if (UserEntityService.GetIndexSpecific(SpecificEntries).ToList().Count > 0)
            {
                if (ModelState.IsValid)
                {
                    {
                        User obj = UserEntityService.GetEdit(dbUsers.First().UserID);
                        var Password = obj.Password;
                        var callbackUrl = "http://localhost:65347/Home/Login";
                        EmailGenerate.SendEmail(obj.Email, "", "<html><head><meta content=\"text/html; charset = utf - 8\" /></head><body><p>Dear Candidate, " + " </p>" +
                            "<p>This is with reference to your request for for forgetting password at Bestway Career Portal. </p>" +
                            "<p>Please enter you password : <u><strong>" + obj.Password + "</u></strong><p>" +
                            " <p>Please click <a href=\"" + callbackUrl + "\">here</a> to login to your profile.</p>" +
                            "<div>Best Regards</div><div>Talent Acquisition Team</div><div>Bestway Cement Limited</div></body></html>", "", "Request for password");
                        return RedirectToAction("PasswordEmailSent", "Home");
                    }
                }
            }
            else
            {
                ModelState.AddModelError("Email", "Not registered email");
            }
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
            string EncryptedPassword;
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
                EncryptedPassword = StringCipher.Encrypt(newPassword);
                V_UserCandidate vmf = Session["LoggedInUser"] as V_UserCandidate;
                User user = UserEntityService.GetEdit((int)vmf.UserID);
                user.Password = EncryptedPassword;
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
        public ActionResult PasswordEmailSent()
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

        public ActionResult CaptchaImage(string prefix, bool noisy = true)
        {
            var rand = new Random((int)DateTime.Now.Ticks);
            //generate new question 
            int a = rand.Next(10, 99);
            int b = rand.Next(0, 9);
            var captcha = string.Format("{0} + {1} = ?", a, b);

            //store answer 
            Session["Captcha" + prefix] = a + b;

            //image stream 
            FileContentResult img = null;

            using (var mem = new MemoryStream())
            using (var bmp = new Bitmap(130, 30))
            using (var gfx = Graphics.FromImage((Image)bmp))
            {
                gfx.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                gfx.SmoothingMode = SmoothingMode.AntiAlias;
                gfx.FillRectangle(Brushes.White, new Rectangle(0, 0, bmp.Width, bmp.Height));

                //add noise 
                if (noisy)
                {
                    int i, r, x, y;
                    var pen = new Pen(Color.Yellow);
                    for (i = 1; i < 10; i++)
                    {
                        pen.Color = Color.FromArgb(
                        (rand.Next(0, 255)),
                        (rand.Next(0, 255)),
                        (rand.Next(0, 255)));

                        r = rand.Next(0, (130 / 3));
                        x = rand.Next(0, 130);
                        y = rand.Next(0, 30);

                        gfx.DrawEllipse(pen, x - r, y - r, r, r);
                    }
                }

                //add question 
                gfx.DrawString(captcha, new Font("Tahoma", 15), Brushes.Gray, 2, 3);

                //render as Jpeg 
                bmp.Save(mem, System.Drawing.Imaging.ImageFormat.Jpeg);
                img = this.File(mem.GetBuffer(), "image/Jpeg");
            }

            return img;
        }
        public ActionResult CaptchaValidate(UserModel vmUserModel)
        {
            //validate captcha 
            if (Session["Captcha"] == null || Session["Captcha"].ToString() != vmUserModel.Captcha)
            {
                ModelState.AddModelError("Captcha", "Captcha Entry Must");
                return RedirectToAction("RegisterUser");
                //dispay error and generate a new captcha 
            }
            return RedirectToAction("RegisterUser");
        }
        public JsonResult SessionInfo()
        {

            if (Session["LoggedInUser"] == null)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Contact()
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
        /// <summary>
        /// Validate Captcha
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public static CaptchaResponse ValidateCaptcha(string response)
        {
            string secret = System.Web.Configuration.WebConfigurationManager.AppSettings["recaptchaPrivateKey"];
            var client = new WebClient();
            var jsonResult = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secret, response));
            return JsonConvert.DeserializeObject<CaptchaResponse>(jsonResult.ToString());
        }
        #endregion
    }
}