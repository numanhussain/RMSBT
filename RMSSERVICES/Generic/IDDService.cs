using RMSCORE.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RMSSERVICES.Generic
{
    public interface IDDService
    {
        List<Candidate> GetCandidate();
        List<EduDegreeLevel> GetEduLevel();
        List<User> GetUser();
        List<Job> GetJob();
        List<HearAbout> GetHearAboutJob();
        List<EduInstitute> GetInstitute();
        List<SkillLevel> GetSkillLevel();
        List<ExperienceIndustry> GetIndustryList();
        List<Candidate> GetSpecificCandidate(Expression<Func<Candidate, bool>> predicate);
    }
}
