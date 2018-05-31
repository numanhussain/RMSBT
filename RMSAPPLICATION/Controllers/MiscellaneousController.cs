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
            if (obj.CrimanalRecord == "Yes" && obj.CrimeDetail == null)
                ModelState.AddModelError("CrimeDetail", "Mandatory ");
            if (obj.WorkingRelative == "Yes" && obj.WorkingRelativeName == null)
                ModelState.AddModelError("WorkingRelativeName", "Mandatory ");
            if (obj.WorkingRelative == "Yes" && obj.WorkingRelativeRelation == null)
                ModelState.AddModelError("WorkingRelativeRelation", "Mandatory ");
            if (obj.WorkingRelative == "Yes" && obj.WorkingRelativeDepartment == null)
                ModelState.AddModelError("WorkingRelativeDepartment", "Mandatory ");
            if (obj.WorkingRelative == "Yes" && obj.WorkingRelativeDesignation == null)
                ModelState.AddModelError("WorkingRelativeDesignation", "Mandatory ");
            if (obj.WorkingRelative == "Yes" && obj.WorkingRelativeLocation == null)
                ModelState.AddModelError("WorkingRelativeLocation", "Mandatory ");
            if (obj.Disability == "Yes" && obj.DisabilityDetail == null)
                ModelState.AddModelError("DisabilityDetail", "Mandatory ");
            if (obj.InterviewedBefore == "Yes" && obj.AppliedPosition == null)
                ModelState.AddModelError("AppliedPosition", "Mandatory ");
            if (obj.InterviewedBefore == "Yes" && obj.InterviewedDate == null)
                ModelState.AddModelError("InterviewedDate", "Mandatory ");
            if (!AssistantService.IsDateTime(Request.Form["InterviewedDate"])) // check for valid date
                ModelState.AddModelError("InterviewedDate", "Invalid date");
            else
                obj.InterviewedDate = Convert.ToDateTime(Request.Form["InterviewedDate"].ToString());
            if (obj.InterviewedBefore == "Yes" && obj.InterviewedLocation == null)
                ModelState.AddModelError("InterviewedLocation", "Mandatory ");
            if (obj.WorkedBefore == "Yes" && obj.DateJoining == null)
                ModelState.AddModelError("DateJoining", "Mandatory ");
            if (!AssistantService.IsDateTime(Request.Form["DateJoining"])) // check for valid date
                ModelState.AddModelError("DateJoining", "Invalid date");
            else
                obj.DateJoining = Convert.ToDateTime(Request.Form["DateJoining"].ToString());
            if (obj.WorkedBefore == "Yes" && obj.DateLeavig == null)
                ModelState.AddModelError("DateLeavig", "Mandatory ");
            if (!AssistantService.IsDateTime(Request.Form["DateLeavig"])) // check for valid date
                ModelState.AddModelError("DateLeavig", "Invalid date");
            else
                obj.DateLeavig = Convert.ToDateTime(Request.Form["DateLeavig"].ToString());
            if (obj.DateLeavig != null)
            {
                if (obj.DateLeavig >= DateTime.Today)
                    ModelState.AddModelError("DateLeavig", "Must be smaller than current date");
            }
            if (obj.WorkedBefore == "Yes" && obj.Designation == null)
                ModelState.AddModelError("Designation", "Mandatory");
            if (obj.WorkedBefore == "Yes" && obj.ReasonLeaving == null)
                ModelState.AddModelError("ReasonLeaving", "Mandatory ");
            if (obj.WorkedBefore == "Yes" && obj.EmploymentNo == null)
                ModelState.AddModelError("EmploymentNo", "Mandatory ");
            if (obj.WorkedBefore == "Yes" && obj.Location == null)
                ModelState.AddModelError("Location", "Mandatory ");
            if (obj.HearAboutJobID == 0)
                ModelState.AddModelError("HearAboutJobID", "Mandatory ");
            if (obj.NoticeTime == null)
                ModelState.AddModelError("NoticeTime", "Mandatory ");
            if (obj.HearAboutJobID == 8 && obj.HearAboutDetail == null)
                ModelState.AddModelError("HearAboutDetail", "Mandatory ");
            if (obj.InternshipDuration == "0")
                ModelState.AddModelError("InternshipDuration", "Mandatory ");
            if (vmf.AppliedAs == 5 || vmf.AppliedAs == 6)
            {
                if (obj.MGSalary == null || obj.MGSalary == "")
                    ModelState.AddModelError("MGSalary", "Mandatory ");
                if (obj.ExpectedSalary == null)
                    ModelState.AddModelError("ExpectedSalary", "Mandatory ");
            }
            //if (obj.BloodGroupID == 0)
            //    ModelState.AddModelError("BloodGroupID", "Mandatory ");
            //if (obj.ReligionID == 0)
            //    ModelState.AddModelError("ReligionID", "Mandatory ");
            //if (obj.MaritalStatusID == 0)
            //    ModelState.AddModelError("MaritalStatusID", "Mandatory ");
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
            V_UserCandidate vmf = Session["LoggedInUser"] as V_UserCandidate;
            if (vmf.HasCV == null)
            {
                if (Request.Files.Count > 0)
                {
                    try
                    {
                        SaveFile(vmf);
                        return Json("File Uploaded Successfully");
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
            else
            {
                if (Request.Files.Count > 0)
                {
                    try
                    {
                        SaveFile(vmf);
                        return Json("File Uploaded Successfully");
                    }
                    catch (Exception ex)
                    {
                        return Json("Error occurred. Error details: " + ex.Message);
                    }
                }
                else
                    return Json("OK");
            }


        }
        public void SaveFile(V_UserCandidate vmf)
        {
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
                    fname = vmf.CandidateID.ToString();
                }

                // Get the complete folder path and store the file inside it.  
                fname = Path.Combine(Server.MapPath("~/UploadFiles/"), fname);
                file.SaveAs(fname);
            }
            // Returns message that successfully uploaded  
        }
        public FilePathResult OpenCV(string fileName)
        {
            V_UserCandidate vmf = Session["LoggedInUser"] as V_UserCandidate;

            return new FilePathResult(string.Format(@"~\UploadFiles\" + vmf.CandidateID.ToString()), "application/.pdf");
        }
        #endregion
        #region -- Controller Private  Methods--
        private void CreateHelper(MiscellaneousDetail obj)
        {
            ViewBag.ReligionID = new SelectList(DDService.GetReligion().ToList().OrderBy(aa => aa.CReligionID).ToList(), "CReligionID", "ReligionName", obj.ReligionID);
            ViewBag.BloodGroupID = new SelectList(DDService.GetBloodGroupList().ToList().OrderBy(aa => aa.CBID).ToList(), "CBID", "BGroupName", obj.BloodGroupID);
            ViewBag.MaritalStatusID = new SelectList(DDService.GetMartialStatusList().ToList().OrderBy(aa => aa.PMID).ToList(), "PMID", "MartialStatusName", obj.MaritalStatusID);
            ViewBag.HearAboutJobID = new SelectList(DDService.GetHearAboutJob().ToList().OrderBy(aa => aa.HearAboutID).ToList(), "HearAboutID", "HearAboutSource", obj.HearAboutJobID);
        }
        #endregion
    }
}