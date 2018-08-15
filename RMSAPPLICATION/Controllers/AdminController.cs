using RMSCORE.EF;
using RMSCORE.Models.Other;
using RMSSERVICES.Generic;
using RMSSERVICES.UserDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RMSAPPLICATION.Controllers
{
    public class AdminController : Controller
    {
        #region -- Controller Initialization --
        IEntityService<User> UserEntityService;
        IUserService UserService;
        // Controller Constructor
        public AdminController(IEntityService<User> userEntityService, IUserService userService)

        {
            UserEntityService = userEntityService;
            UserService = userService;
        }
        #endregion
        // GET: Candidate
        public ActionResult Index()
        {
            List<VMLoggedUser> vmUserModel = UserService.GetAllIndex().OrderByDescending(aa=>aa.DateCreated).ToList();
            return View(vmUserModel);
        }
        #region-- 
        #endregion
    }
}