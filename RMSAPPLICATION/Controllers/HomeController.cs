using RMSCORE.EF;
using RMSSERVICES.Generic;
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
        IEntityService<User> UserService;
        IDDService DDService;
        // Controller Constructor
        public HomeController(IEntityService<User> userService, IDDService ddService)
        {
            UserService = userService;
            DDService = ddService;
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
            if (UserService.GetIndex().Where(aa => aa.UserName == Obj.UserName).Count() > 0)
                return RedirectToAction("Index", "Job");
            else
                return View("Login");
        }


        //if (UserService.GetIndex().Where(aa => aa.UserName == Obj.UserName).Count() > 0)
        //{
        //    User vm = UserService.GetIndex().First(aa => aa.UserName == Obj.UserName);
        //    Expression<Func<AppUserLocation, bool>> SpecificEntries = c => c.UserID == vm.PUserID;
        //    Session["LoggedInUser"] = GetLoggedInUser(vm, AppUserLocationService.GetIndexSpecific(SpecificEntries));
        //    return View("Index");

        //}
        //else
        //    return View("Login");

        //private VMLoggedUser GetLoggedInUser(VHR_AppUser vm, List<AppUserLocation> userLocationList)
        //{
        //    VMLoggedUser obj = new VMLoggedUser();
        //    obj.PUserID = vm.PUserID;
        //    obj.UserEmpID = vm.UserEmpID;
        //    obj.UserName = vm.UserName;
        //    obj.UserStatus = vm.UserStatus;
        //    obj.UserLastActiveDate = vm.UserLastActiveDate;
        //    obj.UserAccessTypeID = vm.UserAccessTypeID;
        //    obj.UserRoleID = vm.UserRoleID;
        //    obj.UserEmployeeName = vm.UserEmployeeName;
        //    obj.UserFPID = vm.UserFPID;
        //    obj.UserJobTitleID = vm.UserJobTitleID;
        //    obj.UserLocationID = vm.UserLocationID;
        //    obj.UserJobTitleName = vm.UserJobTitleName;
        //    obj.UserLocationName = vm.UserLocationName;
        //    obj.MLeave = vm.MLeave;
        //    obj.LeavePolicy = vm.LeavePolicy;
        //    obj.LeaveApplication = vm.LeaveApplication;
        //    obj.LeaveQuota = vm.LeaveQuota;
        //    obj.LeaveCF = vm.LeaveCF;
        //    obj.MShift = vm.MShift;
        //    obj.Shift = vm.Shift;
        //    obj.ShiftChange = vm.ShiftChange;
        //    obj.ShiftChangeEmp = vm.ShiftChangeEmp;
        //    obj.Roster = vm.Roster;
        //    obj.MOvertime = vm.MOvertime;
        //    obj.OvertimePolicy = vm.OvertimePolicy;
        //    obj.OvertimeAP = vm.OvertimeAP;
        //    obj.OvertimeENCPL = vm.OvertimeENCPL;
        //    obj.MAttendanceEditor = vm.MAttendanceEditor;
        //    obj.JobCard = vm.JobCard;
        //    obj.DailyAttEditor = vm.DailyAttEditor;
        //    obj.MonthlyAttEditor = vm.MonthlyAttEditor;
        //    obj.CompanyStructure = vm.CompanyStructure;
        //    obj.MSettings = vm.MSettings;
        //    obj.Reader = vm.Reader;
        //    obj.Holiday = vm.Holiday;
        //    obj.DownloadTime = vm.DownloadTime;
        //    obj.ServiceLog = vm.ServiceLog;
        //    obj.MUser = vm.MUser;
        //    obj.AppUser = vm.AppUser;
        //    obj.AppUserRole = vm.AppUserRole;
        //    obj.Employee = vm.Employee;
        //    obj.Crew = vm.Crew;
        //    obj.OUCommon = vm.OUCommon;
        //    obj.JTCommon = vm.JTCommon;
        //    obj.FinancialYear = vm.FinancialYear;
        //    obj.PayrollPeriod = vm.PayrollPeriod;
        //    obj.TMSAdd = vm.TMSAdd;
        //    obj.TMSEdit = vm.TMSEdit;
        //    obj.TMSView = vm.TMSView;
        //    obj.TMSDelete = vm.TMSDelete;
        //    obj.LMUserID = vm.LMUserID;
        //    obj.MCompany = vm.MCompany;
        //    obj.MAttendance = vm.MAttendance;
        //    obj.Reports = vm.Reports;
        //    obj.UserLoctions = userLocationList;
        //    return obj;
        //}
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
        #endregion
        #endregion
        #region -- Controller Private  Methods--
        #endregion
    }
}