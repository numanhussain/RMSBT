using RMSCORE.EF;
using RMSSERVICES.CandidateImage;
using RMSSERVICES.Generic;
using RMSSERVICES.PersonalDetail;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RMSAPPLICATION.Controllers
{
    public class CandidateController : Controller
    {
        #region -- Controller Initialization --
        IEntityService<Candidate> CandidateEntityService;
        ICandidateService CandidateService;
        // Controller Constructor
        public CandidateController(IEntityService<Candidate> candidateEntityService, ICandidateService candidateservice)
        {
            CandidateEntityService = candidateEntityService;
            CandidateService = candidateservice;
        }
        #endregion
        // GET: Candidate
        public ActionResult Index()
        {
            V_UserCandidate vmf = Session["LoggedInUser"] as V_UserCandidate;
            Candidate obj = new Candidate();
            obj.CandidateID = vmf.CandidateID;
            obj.UserID = vmf.UserID;
            return View(obj);
        }
        [HttpGet]
        public ActionResult Create()
        {
            V_UserCandidate vmf = Session["LoggedInUser"] as V_UserCandidate;
            int cid = vmf.CandidateID;
            int? uid = vmf.UserID;
            Candidate vmOperation = CandidateService.GetCreate(cid,(int)uid);
            return View(vmOperation);
        }
        [HttpPost]
        public ActionResult Create(Candidate dbOperation)
        {
            if (ModelState.IsValid)
            {
                CandidateService.PostCreate(dbOperation);
            }
            //CreateHelper(dbOperation);
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
        #endregion
    }
}