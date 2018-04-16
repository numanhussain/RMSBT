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
        IRepository<EduDegreeType> EduDegreeTypeRepository;
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
        IRepository<Religion> ReligionRepository;
        IRepository<ExpCareerLevel> CareerLevelRepository;
        public DDService(IUnitOfWork unitOfWork,
        IRepository<SkillLevel> skilllevelRepository,
        IRepository<ExperienceIndustry> expIndustryRepository, IRepository<BloodGroup> bloodGroupRepository, IRepository<City> cityRepository, IRepository<Country> countryRepository, IRepository<Candidate> candidateRepository, IRepository<MartialStatu> martialStatusRepository,
        IRepository<EduDegreeLevel> edudetailRepository, IRepository<EduInstitute> eduinstituteRepository,
            IRepository<User> userRepository, IRepository<JobDetail> jobRepository, IRepository<HearAbout> hearAboutJobRepository, IRepository<Location> locationRepository, IRepository<Catagory> catagoryRepository, IRepository<Gender> genderRepository,
        IRepository<Religion> religionRepository, IRepository<EduDegreeType> eduDegreeTypeRepository, IRepository<ExpCareerLevel> careerLevelRepository)
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
            ReligionRepository = religionRepository;
            EduDegreeTypeRepository = eduDegreeTypeRepository;
            CareerLevelRepository = careerLevelRepository;
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
            List<HearAbout> list = new List<HearAbout>();
            list.Add(new HearAbout { HearAboutID = 0, HearAboutSource = "--------" });
            list.AddRange(HearAboutJobRepository.GetAll());
            return list;
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
            List<MartialStatu> list = new List<MartialStatu>();
            list.Add(new MartialStatu { PMID = 0, MartialStatusName = "--------" });
            list.AddRange(MartialStatusRepository.GetAll());
            return list;
        }
        public List<BloodGroup> GetBloodGroupList()
        {
            List<BloodGroup> list = new List<BloodGroup>();
            list.Add(new BloodGroup { CBID = 0, BGroupName = "--------" });
            list.AddRange(BloodGroupRepository.GetAll());
            return list;
        }
        public List<Country> GetCountryList()
        {
            List<Country> list = new List<Country>();
            list.Add(new Country { CCID = 0, CountryName = "--------" });
            list.AddRange(CountryRepository.GetAll());
            return list;
        }
        public List<City> GetCityList()
        {
            List<City> list = new List<City>();
            list.Add(new City { CityID = 0, CityName = "--------" });
            list.AddRange(CityRepository.GetAll());
            return list;
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
            List<Gender> list = new List<Gender>();
            list.Add(new Gender { CGenderID = 0, GenderName = "--------" });
            list.AddRange(GenderRepository.GetAll());
            return list;
        }
        public List<Candidate> GetSpecificCandidate(Expression<Func<Candidate, bool>> predicate)
        {
            return CandidateRepository.FindBy(predicate);
        }
        public List<Religion> GetReligion()
        {
            List<Religion> list = new List<Religion>();
            list.Add(new Religion { CReligionID = 0, ReligionName = "--------" });
            list.AddRange(ReligionRepository.GetAll());
            return list;
        }
        public List<EduDegreeType> GetEduDegreeType()
        {
            return EduDegreeTypeRepository.GetAll();
        }
        public List<ExpCareerLevel> GetCareerLevelList()
        {
            return CareerLevelRepository.GetAll();
        }
    }
}
