using RMSCORE.EF;
using RMSCORE.Models.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMSSERVICES.Generic
{
    public static class EmailText
    {
        public static string GetRegisterEmailText(UserModel vmUserModel, String URL)
        {
            string Body = " <html><head><meta content =\"text/html; charset = utf - 8\" /></head><body><p>Dear Candidate, </p>" +
                                    "<p>This is with reference to your request for creating online profile at Bestway Career Portal. </p>" +
                                    "<p>Please click <a href=\"" + URL + "\">here</a> to activate your profile.</p>" +
                                     "<p>Note:The verification link expire after 24 hours.</p>" +
                                    "<div>Best Regards</div><div>Talent Acquisition Team</div><div>Bestway Cement Limited</div>" +
                                    " <br/><p> ***This is system generated email.Please do not reply.***</p></body></html>";
            return Body;
        }
        public static string RequestNewLinkText(User vmUserModel, String URL)
        {
            string Body = " <html><head><meta content =\"text/html; charset = utf - 8\" /></head><body><p>Dear Candidate, </p>" +
                                    "<p>This is with reference to your request for creating online profile at Bestway Career Portal. </p>" +
                                    "<p>Please click <a href=\"" + URL + "\">here</a> to activate your profile.</p>" +
                                     "<p>Note:The verification link expire after 24 hours.</p>" +
                                    "<div>Best Regards</div><div>Talent Acquisition Team</div><div>Bestway Cement Limited</div>" +
                                    " <br/><p> ***This is system generated email.Please do not reply.***</p></body></html>";
            return Body;
        }
        public static string GetForgetPasswordEmailText(String Password, String URL)
        {
            string Body = "<html><head><meta content=\"text/html; charset = utf - 8\" /></head><body><p>Dear Candidate, " + " </p>" +
                              "<p>This is with reference to your request for retrieving password at Bestway Career Portal. </p>" +
                              "<p>Please enter your password : <u><strong>" + Password + "</u></strong><p>" +
                              " <p>Click <a href=\"" + URL + "\">here</a> to login to your profile.</p>" +
                              "<div>Best Regards</div><div>Talent Acquisition Team</div><div>Bestway Cement Limited</div></body></html>";
            return Body;
        }
        public static string GetJobApplyEmailText(V_UserCandidate LoggedInUser, JobDetail dbJob, String URL)
        {
            string Body = "<html><head><meta content =\"text/html; charset = utf - 8\" /></head><body><p>Dear <strong><u>" + LoggedInUser.CName + " </u></strong>  </p><div><p>Thank you for your keen interest in applying for the position of <strong><u>" + dbJob.JobTitle + "</u></strong>.We have received your application for this post. </p>" +
                               "<p>Our Talent Acquisition Team will meticulously evaluate your profile in line with the requirements of the post you have applied for. Since, we receive a large number of applications for different positions, it is not possible to communicate with every candidate individually. Therefore, only the short-listed candidates will be contacted for interview and other assessments as deemed appropriate. </p></div>" +
                               "<div>You can check the status of your application by logging into your account at Bestway Career Portal.</div>" + "<p>Link:<u><a href=\"" + URL + "\">careers.bestway.com.pk</a></u>" + "</p>" +
                               "<div>Wish you best of luck in your quest to find a suitable career in accordance with your professional and academic qualifications.</div>" +
                               "<div>Kindly note if you have applied for multiple positions, you will receive a separate notification for each position.</div>" +
                               "<div>Best Regards</div><div>Talent Acquisition Team</div><div>Bestway Cement Limited</div></body></html>";
            return Body;
        }
    }
}
