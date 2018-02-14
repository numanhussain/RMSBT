using RMSCORE.EF;
using RMSCORE.Models.Helper;
using RMSSERVICES.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace RMSAPPLICATION.Controllers
{
    public class NotificationController : Controller
    {
        IEntityService<Candidate> CandidateService;
        IEntityService<JobDetail> JobTitleService;
        IEntityService<V_Interview> InterviewService;
        IEntityService<NotificationDetail> NotificationService;
        IDDService DDService;
        public NotificationController(IEntityService<Candidate> candidateService, IEntityService<V_Interview> interviewService, IEntityService<NotificationDetail> notificationService, IDDService dDService, IEntityService<JobDetail> jobTitleService)
        {
            CandidateService = candidateService;
            DDService = dDService;
            JobTitleService = jobTitleService;
            InterviewService = interviewService;
            NotificationService = notificationService;
        }
        // GET: Notification
        public ActionResult Index()
        {
            List<V_Interview> vmlist = InterviewService.GetIndex();
            return View(vmlist);
        }
        [HttpGet]
        public ActionResult GetSystemNotification()
        {
            V_UserCandidate vmf = Session["LoggedInUser"] as V_UserCandidate;
            StringBuilder list = new StringBuilder();
            List<NotificationDetail> vmList = NotificationService.GetIndex();
            int NotificationCount = 0;
            VMNotification vmNotification = new VMNotification();
            if (vmList.Count > 0)
            {
                if (vmList.Where(aa => aa.CandidateID == vmf.CandidateID && aa.IsViewed == false).Count() > 0)
                {
                    NotificationCount++;
                    list.Append(GenerateSingleNotification("icon-git-pull-request", "btn border-primary text-primary btn-flat btn-rounded btn-icon btn-sm",
                        "Interviews Pending " + vmList.Where(aa => aa.IsViewed == false).Count().ToString() + "  interviews.", "Interview Information", GenerateLinkForSystemNotifications("Interview").ToString()));
                }
                if (vmList.Where(aa => aa.JobStatus == true).Count() > 0)
                {
                    NotificationCount++;
                    list.Append(GenerateSingleNotification("icon-git-pull-request", "btn border-primary text-primary btn-flat btn-rounded btn-icon btn-sm",
                        "Jobs Opening " + vmList.Where(aa => aa.JobStatus == true).Count().ToString() + "  jobs.", "Jobs Information", GenerateLinkForSystemNotifications("Job").ToString()));
                }
                if (vmList.Where(aa => aa.CandidateID ==vmf.CandidateID && aa.IsViewed == false).Count() > 0)
                {
                    NotificationCount++;
                    list.Append(GenerateSingleNotification("icon-git-pull-request", "btn border-primary text-primary btn-flat btn-rounded btn-icon btn-sm",
                        "Interviews Pending " + vmList.Where(aa => aa.IsViewed == false).Count().ToString() + "  interviews.", "Interview Information", GenerateLinkForSystemNotifications("Interview").ToString()));
                }
                if (vmList.Where(aa => aa.JobStatus == true).Count() > 0)
                {
                    NotificationCount++;
                    list.Append(GenerateSingleNotification("icon-git-pull-request", "btn border-primary text-primary btn-flat btn-rounded btn-icon btn-sm",
                        "Jobs Opening " + vmList.Where(aa => aa.JobStatus == true).Count().ToString() + "  jobs.", "Jobs Information", GenerateLinkForSystemNotifications("Job").ToString()));
                }
                if (vmList.Where(aa => aa.CandidateID == vmf.CandidateID && aa.IsViewed == false).Count() > 0)
                {
                    NotificationCount++;
                    list.Append(GenerateSingleNotification("icon-git-pull-request", "btn border-primary text-primary btn-flat btn-rounded btn-icon btn-sm",
                        "Interviews Pending " + vmList.Where(aa => aa.IsViewed == false).Count().ToString() + "  interviews.", "Interview Information", GenerateLinkForSystemNotifications("Interview").ToString()));
                }
                if (vmList.Where(aa => aa.JobStatus == true).Count() > 0)
                {
                    NotificationCount++;
                    list.Append(GenerateSingleNotification("icon-git-pull-request", "btn border-primary text-primary btn-flat btn-rounded btn-icon btn-sm",
                        "Jobs Opening " + vmList.Where(aa => aa.JobStatus == true).Count().ToString() + "  jobs.", "Jobs Information", GenerateLinkForSystemNotifications("Job").ToString()));
                }
            }
            vmNotification.Notification = list.ToString();
            vmNotification.NotificationCount = NotificationCount.ToString();
            return Json(vmNotification, JsonRequestBehavior.AllowGet);
        }

        private StringBuilder GenerateLinkForSystemNotifications(string Criteria)
        {
            StringBuilder link = new StringBuilder();
            switch (Criteria)
            {
                case "Interview":

                    link.Append("/Notification/Index");
                    break;
                case "Job":
                    link.Append("/Job/Index");
                    break;
                case "Interview1":

                    link.Append("/Notification/Index");
                    break;
                case "Job1":
                    link.Append("/Job/Index");
                    break;
                case "Interview2":

                    link.Append("/Notification/Index");
                    break;
                case "Job2":
                    link.Append("/Job/Index");
                    break;
            }
            return link;
        }
        private StringBuilder GenerateSingleNotification(string ClassName, string ButtonClass, string MessageTitle, string MessageDescription, string Link)
        {
            StringBuilder list = new StringBuilder();
            list.Append("<li class='media'>");
            list.Append("<div class='media-left'>");
            list.Append("<a href = '#' class='" + ButtonClass + "'><i class='" + ClassName + "'></i></a>");
            list.Append("</div>");
            list.Append("<div class='media-body'>");
            list.Append("<a href='" + Link + "'>" + MessageTitle + "</a>");
            list.Append("<div class='media-annotation'>" + MessageDescription + "</div>");
            list.Append(" </div>");
            list.Append(" </li>");
            return list;

        }
    }
}