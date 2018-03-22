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
        IRepository<Catagory> CatagoryRepository;
        IRepository<Location> LocationRepository;
        IRepository<BloodGroup> BloodGroupRepository;
        IRepository<Country> CountryRepository;
        IRepository<City> CityRepository;
        IRepository<Gender> GenderRepository;
        public DDService(IUnitOfWork unitOfWork,
        IRepository<SkillLevel> skilllevelRepository,
        IRepository<ExperienceIndustry> expIndustryRepository, IRepository<BloodGroup> bloodGroupRepository, IRepository<City> cityRepository, IRepository<Country> countryRepository, IRepository<Candidate> candidateRepository, IRepository<MartialStatu> martialStatusRepository,
        IRepository<EduDegreeLevel> edudetailRepository, IRepository<EduInstitute> eduinstituteRepository,
            IRepository<User> userRepository, IRepository<JobDetail> jobRepository, IRepository<HearAbout> hearAboutJobRepository, IRepository<Location> locationRepository, IRepository<Catagory> catagoryRepository, IRepository<Gender> genderRepository)
        {
            _unitOfWork = unitOfWork;
            CityRepository = cityRepository;
            BloodGroupRepository = bloodGroupRepository;
            CandidateRepository = candidateRepository;
            EduDetailRepository = edudetailRepository;
            EduInstituteRepository = eduinstituteRepository;
            UserRepository = userRepository;
            JobRepository = jobRepository;
            HearAboutJobRepository = hearAboutJobRepository;
            SkillLevelRepository = skilllevelRepository;
            ExpIndustryRepository = expIndustryRepository;
            MartialStatusRepository = martialStatusRepository;
            LocationRepository = locationRepository;
            CatagoryRepository = catagoryRepository;
            CountryRepository = countryRepository;
            GenderRepository = genderRepository;
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
        public List<BloodGroup> GetBloodGroupList()
        {
            return BloodGroupRepository.GetAll();
        }
        public List<Country> GetCountryList()
        {
            return CountryRepository.GetAll();
        }
        public List<City> GetCityList()
        {
            return CityRepository.GetAll();
        }
        public List<Location> GetLocationList()
        {
            return LocationRepository.GetAll();
        }
        public List<Catagory> GetCatagoryList()
        {
            return CatagoryRepository.GetAll();
        }
        public List<Gender> GetGenderList()
        {
            return GenderRepository.GetAll();
        }
        public List<Candidate> GetSpecificCandidate(Expression<Func<Candidate, bool>> predicate)
        {
            return CandidateRepository.FindBy(predicate);
        }
    }
}
