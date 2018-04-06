﻿using RMSCORE.EF;
using RMSCORE.Models.Operation;
using RMSSERVICES.Generic;
using RMSSERVICES.Miscellaneous;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RMSAPPLICATION.Controllers
{
    public class MiscellaneousController : Controller
    {
        #region -- Controller Initialization --
        IEntityService<MiscellaneousDetail> MiscellaneousEntityService;
        IMiscellaneousService MiscellaneousService;
        IDDService DDService;
        // Controller Constructor
        public MiscellaneousController(IMiscellaneousService miscellaneousService, IEntityService<MiscellaneousDetail> miscellaneousentityService, IDDService ddService)
        {
            DDService = ddService;
            MiscellaneousService = miscellaneousService;
            MiscellaneousEntityService = miscellaneousentityService;
        }
        #endregion 
        // GET: Miscellaneous
        #region -- Controller Main View Actions  --
        [HttpGet]
        public ActionResult Create()
        {
            V_UserCandidate vmf = Session["LoggedInUser"] as V_UserCandidate;
            int cid = vmf.CandidateID;
            MiscellaneousDetail obj = MiscellaneousService.GetCreate(cid);
            CreateHelper(obj);
            return View(obj);
        }
        [HttpPost]
        public ActionResult Create(MiscellaneousDetail obj)
        {
            V_UserCandidate vmf = Session["LoggedInUser"] as V_UserCandidate;
            if (obj.CrimanalRecord == null || obj.CrimanalRecord == "")
                ModelState.AddModelError("CrimanalRecord", "This is mandatory field");
            if (obj.HearAboutJobID == null)
                ModelState.AddModelError("HearAboutJobID", "This is mandatory field");
            if (obj.TotalExp == null)
                ModelState.AddModelError("TotalExp", "This is mandatory field");
            if (obj.CementExp == null)
                ModelState.AddModelError("CementExp", "This is mandatory field");

            if (ModelState.IsValid)
            {
                if (vmf.UserStage == 6)
                    vmf.UserStage = 7;
                MiscellaneousService.PostCreate(obj, vmf);
                Session["LoggedInUser"] = vmf;
                Session["ProfileStage"] = vmf.UserStage;
                return Json("OK", JsonRequestBehavior.AllowGet);
            }
            CreateHelper(obj);
            return View(obj);
        }
        [HttpPost]
        public ActionResult UploadFiles()
        {
            // Checking no of files injected in Request object  
            if (Request.Files.Count > 0)
            {
                try
                {
                    V_UserCandidate vmf = Session["LoggedInUser"] as V_UserCandidate;
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
                        //string filename = Path.GetFileName(Request.Files[i].FileName);  

                        HttpPostedFileBase file = files[i];
                        string fname;

                        // Checking for Internet Explorer  
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            //fname = file.FileName;
                            fname = vmf.CandidateID.ToString() + ".pdf";
                        }

                        // Get the complete folder path and store the file inside it.  
                        fname = Path.Combine(Server.MapPath("~/UploadFiles/"), fname);
                        file.SaveAs(fname);
                    }
                    // Returns message that successfully uploaded  
                    return Json("File Uploaded Successfully!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }
        #endregion
        #region -- Controller Private  Methods--
        private void CreateHelper(MiscellaneousDetail obj)
        {
            ViewBag.HearAboutJobID = new SelectList(DDService.GetHearAboutJob().ToList().OrderBy(aa => aa.HearAboutID).ToList(), "HearAboutID", "HearAboutSource", obj.HearAboutJobID);
        }
        #endregion
    }
}