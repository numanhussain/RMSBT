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
        IRepository<AreaOfInterest> AreaOfInterestRepository;
        IRepository<Salutation> SalutationRepository;
        public DDService(IUnitOfWork unitOfWork,
        IRepository<SkillLevel> skilllevelRepository,
        IRepository<ExperienceIndustry> expIndustryRepository, IRepository<BloodGroup> bloodGroupRepository, IRepository<City> cityRepository, IRepository<Country> countryRepository, IRepository<Candidate> candidateRepository, IRepository<MartialStatu> martialStatusRepository,
        IRepository<EduDegreeLevel> edudetailRepository, IRepository<EduInstitute> eduinstituteRepository,
            IRepository<User> userRepository, IRepository<JobDetail> jobRepository, IRepository<HearAbout> hearAboutJobRepository, IRepository<Location> locationRepository, IRepository<Catagory> catagoryRepository, IRepository<Gender> genderRepository,
        IRepository<Religion> religionRepository, IRepository<EduDegreeType> eduDegreeTypeRepository, IRepository<ExpCareerLevel> careerLevelRepository, IRepository<AreaOfInterest> areaOfInterestRepository, IRepository<Salutation> salutationRepository)
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
            AreaOfInterestRepository = areaOfInterestRepository;
            SalutationRepository = salutationRepository;
        }
        public List<Candidate> GetCandidate()
        {
            return CandidateRepository.GetAll();
        }
        public List<EduDegreeLevel> GetEduLevel()
        {
            List<EduDegreeLevel> list = new List<EduDegreeLevel>();
            list.Add(new EduDegreeLevel { DLevelID = 0, DegreeLevel = "--------" });
            list.AddRange(EduDetailRepository.GetAll());
            return list;
        }
        public List<EduInstitute> GetInstitute()
        {
            List<EduInstitute> list = new List<EduInstitute>();
            list.Add(new EduInstitute { InstituteID = 0, InstituteName = "--------" });
            list.AddRange(EduInstituteRepository.GetAll());
            return list;
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
            List<SkillLevel> list = new List<SkillLevel>();
            list.Add(new SkillLevel { SkillLevelID = 0, SkillLevelName = "--------" });
            list.AddRange(SkillLevelRepository.GetAll());
            return list;
        }
        public List<ExperienceIndustry> GetIndustryList()
        {
            List<ExperienceIndustry> list = new List<ExperienceIndustry>();
            list.Add(new ExperienceIndustry { ExpIndustryID = 0, ExpIndustryName = "--------" });
            list.AddRange(ExpIndustryRepository.GetAll());
            return list;
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
            List<Location> list = new List<Location>();
            list.Add(new Location { PLocationID = 0, LocName = "All Locations" });
            list.AddRange(LocationRepository.GetAll());
            return list;
        }
        public List<Catagory> GetCatagoryList()
        {
            List<Catagory> list = new List<Catagory>();
            list.Add(new Catagory { PCatagoryID = 0, CatName = "All Categories" });
            list.AddRange(CatagoryRepository.GetAll());
            return list;
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
            List<EduDegreeType> list = new List<EduDegreeType>();
            list.Add(new EduDegreeType { EduDegreeLevelID = 0, EduTypeName = "--------" });
            list.AddRange(EduDegreeTypeRepository.GetAll());
            return list;
        }
        public List<ExpCareerLevel> GetCareerLevelList()
        {
            List<ExpCareerLevel> list = new List<ExpCareerLevel>();
            list.Add(new ExpCareerLevel { CLevelID = 0, CareerLevelName = "--------" });
            list.AddRange(CareerLevelRepository.GetAll());
            return list;
        }

        public List<AreaOfInterest> GetAreaOfInterestList()
        {
            List<AreaOfInterest> list = new List<AreaOfInterest>();
            list.Add(new AreaOfInterest { CAreaID = 0, AreaOfInterestName = "--------" });
            list.AddRange(AreaOfInterestRepository.GetAll());
            return list;
        }
        public List<Salutation> GetSalutationList()
        {
            List<Salutation> list = new List<Salutation>();
            list.Add(new Salutation { CSalutationID = 0, SalutationName = "--------" });
            list.AddRange(SalutationRepository.GetAll());
            return list;
        }
    }
}
