using RMSCORE.EF;
using RMSREPO.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RMSSERVICES.Generic
{
    public class DDService : IDDService
    {
        IUnitOfWork _unitOfWork;
        IRepository<Candidate> CandidateRepository;
        IRepository<EduDegreeLevel> EduDetailRepository;
        IRepository<EduInstitute> EduInstituteRepository;
        IRepository<MartialStatu> MartialStatusRepository;
        IRepository<User> UserRepository;
        IRepository<JobDetail> JobRepository;
        IRepository<HearAbout> HearAboutJobRepository;
        IRepository<SkillLevel> SkillLevelRepository;
        IRepository<ExperienceIndustry> ExpIndustryRepository;
        public DDService(IUnitOfWork unitOfWork,
        IRepository<SkillLevel> skilllevelRepository,
        IRepository<ExperienceIndustry> expIndustryRepository, IRepository<Candidate> candidateRepository, IRepository<MartialStatu> martialStatusRepository,
        IRepository<EduDegreeLevel> edudetailRepository, IRepository<EduInstitute> eduinstituteRepository,
            IRepository<User> userRepository, IRepository<JobDetail> jobRepository, IRepository<HearAbout> hearAboutJobRepository)
        {
            _unitOfWork = unitOfWork;
            CandidateRepository = candidateRepository;
            EduDetailRepository = edudetailRepository;
            EduInstituteRepository = eduinstituteRepository;
            UserRepository = userRepository;
            JobRepository = jobRepository;
            HearAboutJobRepository = hearAboutJobRepository;
            SkillLevelRepository = skilllevelRepository;
            ExpIndustryRepository = expIndustryRepository;
            MartialStatusRepository = martialStatusRepository;
        }
        public List<Candidate> GetCandidate()
        {
            return CandidateRepository.GetAll();
        }
        public List<EduDegreeLevel> GetEduLevel()
        {
            return EduDetailRepository.GetAll();
        }
        public List<EduInstitute> GetInstitute()
        {
            return EduInstituteRepository.GetAll();
        }
        public List<User> GetUser()
        {
            return UserRepository.GetAll();
        }
        public List<JobDetail> GetJob()
        {
            return JobRepository.GetAll();
        }
        public List<HearAbout> GetHearAboutJob()
        {
            return HearAboutJobRepository.GetAll();
        }
        public List<SkillLevel> GetSkillLevel()
        {
            return SkillLevelRepository.GetAll();
        }
        public List<ExperienceIndustry> GetIndustryList()
        {
            return ExpIndustryRepository.GetAll();
        }
        public List<MartialStatu> GetMartialStatusList()
        {
            return MartialStatusRepository.GetAll();
        }
        public List<Candidate> GetSpecificCandidate(Expression<Func<Candidate, bool>> predicate)
        {
            return CandidateRepository.FindBy(predicate);
        }
    }
}
