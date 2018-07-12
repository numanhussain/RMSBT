﻿using RMSCORE.EF;
using RMSCORE.Models.Operation;
using RMSREPO.Generic;
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
        IEntityService<CandidateStep> CandidateStepEntityService;
        IMiscellaneousService MiscellaneousService;
        IDDService DDService;
        IRepository<Candidate> CandidateRepository;
        // Controller Constructor
        public MiscellaneousController(IMiscellaneousService miscellaneousService, IEntityService<MiscellaneousDetail> miscellaneousentityService, IDDService ddService,
             IRepository<Candidate> candidateRepository, IEntityService<CandidateStep> candidateStepEntityService)
        {
            DDService = ddService;
            MiscellaneousService = miscellaneousService;
            MiscellaneousEntityService = miscellaneousentityService;
            CandidateStepEntityService = candidateStepEntityService;
            CandidateRepository = candidateRepository;
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
            if (obj.InterviewedBefore == "Yes")
            {
                if (!AssistantService.IsDateTime(Request.Form["InterviewedDate"])) // check for valid date
                    ModelState.AddModelError("InterviewedDate", "Invalid date");
                else
                    obj.InterviewedDate = Convert.ToDateTime(Request.Form["InterviewedDate"].ToString());
            }
            if (obj.InterviewedBefore == "Yes" && obj.InterviewedLocation == null)
                ModelState.AddModelError("InterviewedLocation", "Mandatory ");
            if (obj.WorkedBefore == "Yes" && obj.DateJoining == null)
                ModelState.AddModelError("DateJoining", "Mandatory ");
            if (obj.WorkedBefore == "Yes")
            {
                if (!AssistantService.IsDateTime(Request.Form["DateJoining"])) // check for valid date
                    ModelState.AddModelError("DateJoining", "Invalid date");
                else
                    obj.DateJoining = Convert.ToDateTime(Request.Form["DateJoining"].ToString());
            }
            if (obj.WorkedBefore == "Yes" && obj.DateLeavig == null)
                ModelState.AddModelError("DateLeavig", "Mandatory ");
            if (obj.WorkedBefore == "Yes")
            {
                if (!AssistantService.IsDateTime(Request.Form["DateLeavig"])) // check for valid date
                    ModelState.AddModelError("DateLeavig", "Invalid date");
                else
                    obj.DateLeavig = Convert.ToDateTime(Request.Form["DateLeavig"].ToString());
            }
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
                CandidateStep dbtickStep = CandidateStepEntityService.GetEdit(vmf.CandidateID);
                dbtickStep.StepSeven = true;
                CandidateStepEntityService.PostEdit(dbtickStep);
                vmf.StepSeven = dbtickStep.StepSeven;
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
                var checkextension = Path.GetExtension(file.FileName).ToLower();
                // Checking for Internet Explorer  
                if (checkextension == ".pdf")
                {
                    fname = vmf.CandidateID.ToString() + ".pdf";
                }
                else if (checkextension == ".jpg")
                {
                    fname = vmf.CandidateID.ToString() + ".jpg";
                }
                else if (checkextension == ".docx")
                {
                    //fname = file.FileName;
                    fname = vmf.CandidateID.ToString() + ".docx";
                }
                else
                {
                    fname = vmf.CandidateID.ToString() + ".doc";
                }
                // Get the complete folder path and store the file inside it.  
                fname = Path.Combine(Server.MapPath("~/UploadFiles/"), fname);
                file.SaveAs(fname);
                Candidate dbCandidate = CandidateRepository.GetSingle(vmf.CandidateID);
                dbCandidate.CVName = file.FileName;
                CandidateRepository.Edit(dbCandidate);
                CandidateRepository.Save();
                vmf.CVName = dbCandidate.CVName;
                Session["LoggedInUser"] = vmf;
            }
            // Returns message that successfully uploaded  
        }
        public FilePathResult OpenCV(string fileName)
        {
            V_UserCandidate vmf = Session["LoggedInUser"] as V_UserCandidate;
            var checkextension = Path.GetExtension(vmf.CVName).ToLower();
            if (checkextension == ".pdf")
            {
                return new FilePathResult(string.Format(@"~\UploadFiles\" + vmf.CandidateID.ToString() + ".pdf"), "application/pdf");
            }
            else if (checkextension == ".docx")
            {
                return new FilePathResult(string.Format(@"~\UploadFiles\" + vmf.CandidateID.ToString() + ".docx"), "application/docx");
            }
            else if (checkextension == ".doc")
            {
                return new FilePathResult(string.Format(@"~\UploadFiles\" + vmf.CandidateID.ToString() + ".doc"), "application/doc");
            }
            else
            {
                return new FilePathResult(string.Format(@"~\UploadFiles\" + vmf.CandidateID.ToString() + ".jpg"), "application/jpg");
            }

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