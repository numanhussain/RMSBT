using RMSCORE.EF;
using RMSSERVICES.Generic;
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
        IUserService UserService;
        IDDService DDService;
        // Controller Constructor
        public HomeController(IEntityService<User> userEntityService, IUserService userService, IDDService ddService, IEntityService<V_UserCandidate> vUserEntityService)
        {
            UserEntityService = userEntityService;
            UserService = userService;
            DDService = ddService;
            VUserEntityService = vUserEntityService;
        }
        #endregion
        #region -- Controller ActionResults  -- 
        public ActionResult Index()
        {
            return View();
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
            //if (UserEntityService.GetIndex().Where(aa => aa.UserName == Obj.UserName).Count() > 0)
            //    return RedirectToAction("Index", "Job");
            //else
            //    return View("Login");

            if (VUserEntityService.GetIndex().Where(aa => aa.UserName == Obj.UserName && aa.Password == Obj.Password).Count() > 0)
            {
                V_UserCandidate vm = VUserEntityService.GetIndex().First(aa => aa.UserName == Obj.UserName && aa.Password == Obj.Password);
                Expression<Func<V_UserCandidate, bool>> SpecificEntries = c => c.UserID == vm.UserID;
                Session["LoggedInUser"] = VUserEntityService.GetIndexSpecific(SpecificEntries).First();
                if (vm.UserStage == "ProfileCompleted")
                {
                    return RedirectToAction("Index", "Job");
                }
                if (vm.UserStage == "SignUp")
                {
                    return RedirectToAction("Index", "Candidate");
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
                    var code = Obj.SecurityLink;
                    var callbackUrl = Url.Action("ConfirmEmail", "Account", new { User = Obj.UserID, code = code }, protocol: Request.Url.Scheme);
                    EmailGenerate.SendEmail(Obj.Email,"", "Activate Your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");
                    Expression<Func<V_UserCandidate, bool>> SpecificEntries = c => c.UserID == Obj.UserID && c.Password == Obj.Password;
                    Session["LoggedInUser"] = VUserEntityService.GetIndexSpecific(SpecificEntries).First();
                    return RedirectToAction("Index", "Job");
                }
            }

            return View(Obj);
        }
        public ActionResult VerifyLink(string key)
        {
            if (UserService.VerifyLink(key))
            {
                User user = UserEntityService.GetIndex().Where(aa => aa.SecurityLink == key).First();
               
                    V_UserCandidate vm = VUserEntityService.GetIndex().First(aa => aa.UserName == user.UserName && aa.Password == user.Password);
                    Expression<Func<V_UserCandidate, bool>> SpecificEntries = c => c.UserID == vm.UserID;
                    Session["LoggedInUser"] = VUserEntityService.GetIndexSpecific(SpecificEntries).First();
                        return RedirectToAction("Index", "Job");
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
        #endregion
        #endregion
        #region -- Controller Private  Methods--
        #endregion
    }
}