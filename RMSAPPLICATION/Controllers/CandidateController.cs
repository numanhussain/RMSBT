﻿using RMSCORE.EF;
using RMSCORE.Models.Helper;
using RMSCORE.Models.Main;
using RMSSERVICES.CandidateImage;
using RMSSERVICES.Generic;
using RMSSERVICES.PersonalDetail;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Web.Mvc;
 
namespace RMSAPPLICATION.Controllers
{
    public class CandidateController : Controller
    {
        #region -- Controller Initialization --
        IEntityService<Candidate> CandidateEntityService;
        IEntityService<CandidateStep> CandidateStepEntityService;
        ICandidateService CandidateService;
        IDDService DDService;
        // Controller Constructor
        public CandidateController(IEntityService<Candidate> candidateEntityService, IEntityService<CandidateStep> candidateStepEntityService, IDDService ddService, ICandidateService candidateservice)

        {
            CandidateEntityService = candidateEntityService;
            CandidateStepEntityService = candidateStepEntityService;
            CandidateService = candidateservice;
            DDService = ddService;
        }
        #endregion
        // GET: Candidate
        public ActionResult Index()
        {
            V_UserCandidate vmf = Session["LoggedInUser"] as V_UserCandidate;
            Candidate obj = new Candidate();
            obj.CandidateID = vmf.CandidateID;
            obj.UserID = vmf.UserID;
            Session["ProfileStage"] = vmf.UserStage;
            ViewBag.CategoryID = new SelectList(DDService.GetCatagoryList().ToList().OrderBy(aa => aa.PCatagoryID), "PCatagoryID", "CatName", vmf.AppliedAs);
            return View(obj);
        }
        [HttpGet]
        public ActionResult Create()
        {

            V_UserCandidate vmf = Session["LoggedInUser"] as V_UserCandidate;
            int cid = vmf.CandidateID;
            int? uid = vmf.UserID;
            Candidate vmOperation = CandidateService.GetCreate(cid, (int)uid);
            if (vmOperation.CountryID == null)
            {
                vmOperation.CountryID = DDService.GetCountryList().ToList().OrderBy(aa => aa.CCID).First().CCID;
            }
            if (vmOperation.CityID == null)
            {
                ViewBag.CityID = DDService.GetCityList().ToList().OrderBy(aa => aa.CityID);
            }
            CreateHelper(vmOperation);
            return View(vmOperation);
        }
        [HttpPost]
        public ActionResult Create(Candidate dbOperation)
        {
            ReadFromRadioButton(dbOperation);
            V_UserCandidate vmf = Session["LoggedInUser"] as V_UserCandidate;
            if (dbOperation.SalutationID == 0)
                ModelState.AddModelError("SalutationID", "Mandatory");
            if (dbOperation.CName == null || dbOperation.CName == "")
                ModelState.AddModelError("CName", "Mandatory");
            if (dbOperation.FatherName == null || dbOperation.CName == "")
                ModelState.AddModelError("FatherName", "Mandatory");
            if (!AssistantService.IsDateTime(Request.Form["DOB"])) // check for valid date
                ModelState.AddModelError("DOB", "Date is not valid");
            else
                dbOperation.DOB = Convert.ToDateTime(Request.Form["DOB"].ToString());
            if (dbOperation.DOB != null)
            {
                if (dbOperation.DOB >= DateTime.Today)
                    ModelState.AddModelError("DOB", "Must be smaller than current date");
            }
            if (dbOperation.GenderID == 0)
                ModelState.AddModelError("GenderID", "Mandatory");
            if (dbOperation.MartialStatusID == 0)
                ModelState.AddModelError("MartialStatusID", "Mandatory");
            if (dbOperation.ReligionID == 0)
                ModelState.AddModelError("ReligionID", "Mandatory");
            //if (dbOperation.BloodGroupID == 0)
            //    ModelState.AddModelError("BloodGroupID", "Mandatory");
            if (dbOperation.AreaOfInterestID == 0)
                ModelState.AddModelError("AreaOfInterestID", "Mandatory");
            if (dbOperation.AreaOfInterestID == 28)
            {
                if (dbOperation.OtherAreaName == null || dbOperation.OtherAreaName == "")
                    ModelState.AddModelError("OtherAreaName", "Mandatory");
            }
            if (dbOperation.AreaOfInterestID != 28)
            {
                dbOperation.OtherAreaName = null;
            }
            //if (dbOperation.DomicileCityID == 0)
            //    ModelState.AddModelError("DomicileCityID", "Mandatory");
            if (dbOperation.DomicileCityID != 117)
            {
                dbOperation.OtherDomicileCityName = null;
            }
            if (dbOperation.Address == null || dbOperation.Address == "")
                ModelState.AddModelError("Address", "Mandatory");
            if (dbOperation.CountryID == 0)
                ModelState.AddModelError("CountryID", "Mandatory");
            if (dbOperation.CountryID != 74)
            {
                dbOperation.OtherPakistaniCityName = null;
                if (dbOperation.OtherCityName == null || dbOperation.OtherCityName == "")
                    ModelState.AddModelError("OtherCityName", "Mandatory");
            }
            if (dbOperation.CountryID == 74)
            {
                dbOperation.OtherCityName = null;
            }
            if (dbOperation.CityID == 117)
            {
                if (dbOperation.OtherPakistaniCityName == null || dbOperation.OtherPakistaniCityName == "")
                    ModelState.AddModelError("OtherPakistaniCityName", "Mandatory");
            }
            if (dbOperation.CityID != 117)
            {
                dbOperation.OtherPakistaniCityName = null;
            }
            if (dbOperation.NationalityCountryID == 0)
                ModelState.AddModelError("NationalityCountryID", "Mandatory");
            if (dbOperation.NationalityCountryID == 74)
            {
                dbOperation.PassportNumber = null;
                if (dbOperation.CNICNo == null || dbOperation.CNICNo == "")
                    ModelState.AddModelError("CNICNo", "Mandatory");
                else if (dbOperation.CNICNo != null)
                {
                    if (dbOperation.CNICNo.Length > 15)
                        ModelState.AddModelError("CNICNo", "String length exceeds");
                    Match match = Regex.Match(dbOperation.CNICNo, @"\d{1,5}\-\d{1,7}\-\d{1,1}");
                    if (!match.Success)
                    {
                        ModelState.AddModelError("CNICNo", "Enter a valid CNIC No");
                    }
                }
            }
            if (dbOperation.NationalityCountryID != 74)
            {
                dbOperation.CNICNo = null;
                if (dbOperation.PassportNumber == null || dbOperation.PassportNumber == "")
                    ModelState.AddModelError("PassportNumber", "Mandatory");
            }
            if (dbOperation.EmailID == null || dbOperation.EmailID == "")
                ModelState.AddModelError("EmailID", "Mandatory ");

            if (dbOperation.EmailID != null)
            {
                Match match = Regex.Match(dbOperation.EmailID, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                if (!match.Success)
                {
                    ModelState.AddModelError("EmailID", "Mandatory");
                }
            }
            if (dbOperation.CellNo == null || dbOperation.CellNo == "")
                ModelState.AddModelError("CellNo", "Mandatory");
            if (dbOperation.CellNo != null)
            {
                Match match = Regex.Match(dbOperation.CellNo, "[0-9]{4}-[0-9]{7}$");
                if (!match.Success)
                {
                    ModelState.AddModelError("CellNo", "Formate: e.g 0300-1234567");
                }
            }
            if (ModelState.IsValid)
            {
                if (vmf.UserStage == 2)
                    vmf.UserStage = 3;
                CandidateService.PostCreate(dbOperation, vmf);
                CandidateStep dbtickStep = CandidateStepEntityService.GetEdit(vmf.CandidateID);
                dbtickStep.StepOne = true;
                CandidateStepEntityService.PostEdit(dbtickStep);
                vmf.StepOne = dbtickStep.StepOne;
                Session["LoggedInUser"] = vmf;
                Session["ProfileStage"] = vmf.UserStage;
                return Json("OK", JsonRequestBehavior.AllowGet);
            }
            CreateHelper(dbOperation);
            return View("Create", dbOperation);
        }
        [HttpPost]
        public void CandidateImage()
        {
            try
            {
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var file = Request.Files[i];
                    if (file != null)
                    {
                        int empid = Convert.ToInt32(Request.Form["CID"].ToString());
                        CandidateService.SaveImageInDatabase(ConvertToBytes(file), empid);

                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ActionResult RetrieveImage(int? id)
        {
            try
            {
                var img = CandidateService.GetImageFromDataBase((int)id);
                if (img != null)
                {
                    return File(img, "image/jpg");
                }
                else
                {
                    return File("~/Theme/assets/images/image.png", "image/png");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        public ActionResult ViewProfileIndex(int? JobID)
        {
            V_UserCandidate vmf = Session["LoggedInUser"] as V_UserCandidate;
            int cid = vmf.CandidateID;
            VMCandidateProfileView vmViewProfile = CandidateService.GetProfileDetails(cid, JobID);
            return View(vmViewProfile);
        }
        #region-- 
        private void CreateHelper(Candidate obj)
        {
            ViewBag.CountryID = new SelectList(DDService.GetCountryList().ToList().OrderBy(aa => aa.CCID).ToList(), "CCID", "CountryName", obj.CountryID);
            ViewBag.NationalityCountryID = new SelectList(DDService.GetCountryList().ToList().OrderBy(aa => aa.CCID).ToList(), "CCID", "CountryName", obj.NationalityCountryID);
            ViewBag.CityID = new SelectList(DDService.GetCityList().ToList().OrderBy(aa => aa.CityID).ToList(), "CityID", "CityName", obj.CityID);
            ViewBag.DomicileCityID = new SelectList(DDService.GetDomicileList().ToList().OrderBy(aa => aa.CityID).ToList(), "CityID", "CityName", obj.DomicileCityID);
            ViewBag.GenderID = new SelectList(DDService.GetGenderList().ToList().OrderBy(aa => aa.CGenderID).ToList(), "CGenderID", "GenderName", obj.GenderID);
            ViewBag.AreaOfInterestID = new SelectList(DDService.GetAreaOfInterestList().ToList().OrderBy(aa => aa.CAreaID).ToList(), "CAreaID", "AreaOfInterestName", obj.AreaOfInterestID);
            ViewBag.SalutationID = new SelectList(DDService.GetSalutationList().ToList().OrderBy(aa => aa.CSalutationID).ToList(), "CSalutationID", "SalutationName", obj.SalutationID);
        }
        public byte[] ConvertToBytes(HttpPostedFileBase image)
        {
            Image img = Image.FromStream(image.InputStream);
            Image conImage = ScaleImage(img, 230, 500);
            byte[] imageBytes = null;
            imageBytes = imgToByteArray(conImage);
            return imageBytes;
        }
        public static Image ScaleImage(Image image, int maxWidth, int maxHeight)
        {
            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            var newImage = new Bitmap(newWidth, newHeight);

            using (var graphics = Graphics.FromImage(newImage))
                graphics.DrawImage(image, 0, 0, newWidth, newHeight);
            return newImage;
        }
        public byte[] imgToByteArray(Image img)
        {
            var ms = new MemoryStream();
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return ms.ToArray();
        }
        public ActionResult CityList(string ID)
        {
            int Code = Convert.ToInt32(ID);
            var states = DDService.GetCityList().Where(aa => aa.CountryID == Code).OrderBy(aa => aa.CityID);
            if (HttpContext.Request.IsAjaxRequest())
                return Json(new SelectList(
                                states.ToArray(),
                                "CityID",
                                "CityName")
                            , JsonRequestBehavior.AllowGet);

            return RedirectToAction("Index");
        }
        public List<VMDropDown> GetAppliedAs()
        {
            List<VMDropDown> list = new List<VMDropDown>();

            {
                VMDropDown vm = new VMDropDown();
                vm.ID = 1;
                vm.Name = "Internship";
                list.Add(vm);
            }
            {
                VMDropDown vm = new VMDropDown();
                vm.ID = 2;
                vm.Name = "Apprentices";
                list.Add(vm);
            }
            {
                VMDropDown vm = new VMDropDown();
                vm.ID = 3;
                vm.Name = "MTO";
                list.Add(vm);
            }
            {
                VMDropDown vm = new VMDropDown();
                vm.ID = 4;
                vm.Name = "Contractual";
                list.Add(vm);
            }
            {
                VMDropDown vm = new VMDropDown();
                vm.ID = 5;
                vm.Name = "Permanent";
                list.Add(vm);
            }
            return list;
        }
        private void ReadFromRadioButton(Candidate obj)
        {
            #region -- Radio Buttons--
            // Adjust Bonus of Compensation Radio Button
            string radioWorkPermitValue = "";

            var WorkPermitValue = ValueProvider.GetValue("WorkPermitSelection");
            if (WorkPermitValue != null)
            {
                if (radioWorkPermitValue != null)
                {
                    radioWorkPermitValue = WorkPermitValue.AttemptedValue;
                }
                if (radioWorkPermitValue == "WorkPermitYes")
                {
                    obj.WorkPermitYes = true;
                    obj.WorkPermitYes = false;
                }
                if (radioWorkPermitValue == "WorkPermitNo")
                {
                    obj.WorkPermitYes = false;
                    obj.WorkPermitYes = true;
                }
            }
            #endregion
        }

        #endregion
    }
}