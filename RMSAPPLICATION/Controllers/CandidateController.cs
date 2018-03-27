using RMSCORE.EF;
using RMSCORE.Models.Helper;
using RMSSERVICES.CandidateImage;
using RMSSERVICES.Generic;
using RMSSERVICES.PersonalDetail;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace RMSAPPLICATION.Controllers
{
    public class CandidateController : Controller
    {
        #region -- Controller Initialization --
        IEntityService<Candidate> CandidateEntityService;
        ICandidateService CandidateService;
        IDDService DDService;
        // Controller Constructor
        public CandidateController(IEntityService<Candidate> candidateEntityService, IDDService ddService, ICandidateService candidateservice)
        {
            CandidateEntityService = candidateEntityService;
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
            ViewBag.AppliedAs = new SelectList(GetAppliedAs().ToList(), "ID", "Name", vmf.AppliedAs);
            return View(obj);
        }
        [HttpGet]
        public ActionResult Create()
        {

            V_UserCandidate vmf = Session["LoggedInUser"] as V_UserCandidate;
            int cid = vmf.CandidateID;
            int? uid = vmf.UserID;
            Candidate vmOperation = CandidateService.GetCreate(cid, (int)uid);
            if(vmOperation.CountryID==null)
            {
                vmOperation.CountryID = DDService.GetCountryList().ToList().OrderBy(aa => aa.CCID).First().CCID;
            }
            if(vmOperation.CityID == null)
            {
                ViewBag.CityID = DDService.GetCityList().ToList().OrderBy(aa => aa.CityID);
            }
            CreateHelper(vmOperation);
            return View(vmOperation);
        }
        [HttpPost]
        public ActionResult Create(Candidate dbOperation)
        {
            V_UserCandidate vmf = Session["LoggedInUser"] as V_UserCandidate;
            if (dbOperation.CName == null || dbOperation.CName == "")
                ModelState.AddModelError("CName", "Candidate name cannot be empty");
            if (dbOperation.FatherName == null || dbOperation.CName == "")
                ModelState.AddModelError("FatherName", "Father name cannot be empty");
            if (dbOperation.DOB == null)
                ModelState.AddModelError("DOB", "DOB cannot be empty");
            if (dbOperation.Address == null || dbOperation.Address == "")
                ModelState.AddModelError("Address", "Address cannot be empty");
            if (dbOperation.CNICNo != null)
            {
                if (dbOperation.CNICNo.Length > 15)
                    ModelState.AddModelError("CNICNo", "String length exceeds!");
                Match match = Regex.Match(dbOperation.CNICNo, @"\d{1,5}\-\d{1,7}\-\d{1,1}");
                if (!match.Success)
                {
                    ModelState.AddModelError("CNICNo", "Enter a valid CNIC No");
                }
            }
            if (dbOperation.CNICNo == null)
                ModelState.AddModelError("CNICNo", "CNIC No cannot be empty");
            if (dbOperation.EmailID == null || dbOperation.EmailID == "")
                ModelState.AddModelError("EmailID", "EmailID name cannot be empty");

            if (dbOperation.EmailID != null)
            {
                Match match = Regex.Match(dbOperation.EmailID, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                if (!match.Success)
                {
                    ModelState.AddModelError("EmailID", "Enter a valid Email ID");
                }
            }
            if (dbOperation.CellNo == null || dbOperation.CellNo == "")
                ModelState.AddModelError("CellNo", "Cell no cannot be empty");
            if (ModelState.IsValid)
            {
                if(vmf.UserStage==2)
                    vmf.UserStage = 3;
                CandidateService.PostCreate(dbOperation, vmf);
                Session["LoggedInUser"] = vmf;
                Session["ProfileStage"] = vmf.UserStage;
                return Json("OK",JsonRequestBehavior.AllowGet);
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

        #region-- 
        private void CreateHelper(Candidate obj)
        {
            ViewBag.MartialStatusID = new SelectList(DDService.GetMartialStatusList().ToList().OrderBy(aa => aa.PMID).ToList(), "PMID", "MartialStatusName", obj.MartialStatusID);
            ViewBag.BloodGroupID = new SelectList(DDService.GetBloodGroupList().ToList().OrderBy(aa => aa.CBID).ToList(), "CBID", "BGroupName", obj.BloodGroupID);
            ViewBag.CountryID = new SelectList(DDService.GetCountryList().ToList().OrderBy(aa => aa.CCID).ToList(), "CCID", "CountryName", obj.CountryID);
            ViewBag.NationalityCountryID = new SelectList(DDService.GetCountryList().ToList().OrderBy(aa => aa.CCID).ToList(), "CCID", "CountryName", obj.NationalityCountryID);
            ViewBag.CityID = new SelectList(DDService.GetCityList().ToList().OrderBy(aa => aa.CityID).ToList(), "CityID", "CityName", obj.CityID);
            ViewBag.DomicileCityID = new SelectList(DDService.GetCityList().ToList().OrderBy(aa => aa.CityID).ToList(), "CityID", "CityName", obj.DomicileCityID);
            ViewBag.GenderID = new SelectList(DDService.GetGenderList().ToList().OrderBy(aa => aa.CGenderID).ToList(), "CGenderID", "GenderName", obj.GenderID);
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
        #endregion
    }
}