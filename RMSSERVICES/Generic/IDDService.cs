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
        List<Religion> GetReligion();
        List<EduDegreeLevel> GetEduLevel();
        List<EduDegreeType> GetEduDegreeType();
        List<User> GetUser();
        List<JobDetail> GetJob();
        List<HearAbout> GetHearAboutJob();
        List<EduInstitute> GetInstitute();
        List<SkillLevel> GetSkillLevel();
        List<ExperienceIndustry> GetIndustryList();
        List<Candidate> GetSpecificCandidate(Expression<Func<Candidate, bool>> predicate);
        List<MartialStatu> GetMartialStatusList();
        List<Location> GetLocationList();
        List<Catagory> GetCatagoryList();
        List<BloodGroup> GetBloodGroupList();
        List<Country> GetCountryList();
        List<City> GetCityList();
        List<Gender> GetGenderList();
        List<ExpCareerLevel> GetCareerLevelList();
        List<AreaOfInterest> GetAreaOfInterestList();
        List<Salutation> GetSalutationList();
        List<City> GetDomicileList();
        List<InterviewStatu> GetInterviewStatus();
        List<EduBoard> GetBoardList();
        void GenerateEmail(string TO, string CC, string Subject, string Body, int CandidateID, int NotiTypeID);
    }
}
