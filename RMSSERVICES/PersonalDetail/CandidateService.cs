using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMSCORE.Models.Helper;
using RMSCORE.Models.Main;
using RMSCORE.Models.Operation;
using RMSSERVICES.Generic;
using RMSREPO.Generic;
using RMSCORE.EF;
using System.Linq.Expressions;

namespace RMSSERVICES.PersonalDetail
{
    public class CandidateService : ICandidateService
    {
        IUnitOfWork UnitOfWork;
        IRepository<Candidate> CandidateRepository;
        IRepository<V_UserCandidate> VUserRepositiory;
        IRepository<User> UserRepository;
        IRepository<VMCandidateIndex> VMCandidateRepository;
        IRepository<CandidatePhoto> CandidatePhotoRepository;
        IRepository<V_Candidate_EduDetail> VEducationRepository;
        IRepository<V_Candidate_Exp> VCandidateExpRepository;
        IRepository<CompensationDetail> CompensationDetailRepository;
        IRepository<MiscellaneousDetail> MiscellaneousDetailRepository;
        IRepository<CandidateStrength> CandidateStrengthRepository;
        public CandidateService(IUnitOfWork unitOfWork,
        IRepository<CandidatePhoto> candidatePhotoRepository, IRepository<V_Candidate_EduDetail> vEducationRepository, IRepository<Candidate> candidateRepository, IRepository<User> userRepository, IRepository<VMCandidateIndex> vmCandidateRepository, IRepository<V_UserCandidate> vuserRepositiory,
        IRepository<V_Candidate_Exp> vCandidateExpRepository, IRepository<CompensationDetail> compensationDetailRepository, IRepository<MiscellaneousDetail> miscellaneousDetailRepository, IRepository<CandidateStrength> candidateStrengthRepository)
        {
            UnitOfWork = unitOfWork;
            CandidateRepository = candidateRepository;
            VMCandidateRepository = vmCandidateRepository;
            CandidatePhotoRepository = candidatePhotoRepository;
            VUserRepositiory = vuserRepositiory;
            UserRepository = userRepository;
            VEducationRepository = vEducationRepository;
            VCandidateExpRepository = vCandidateExpRepository;
            CompensationDetailRepository = compensationDetailRepository;
            MiscellaneousDetailRepository = miscellaneousDetailRepository;
            CandidateStrengthRepository = candidateStrengthRepository;
        }
        public List<Candidate> GetIndex()
        {
            return CandidateRepository.GetAll();
        }
        public Candidate GetCreate(int cid, int uid)
        {
            Candidate dbCandidate = CandidateRepository.GetSingle(cid);
            dbCandidate.UserID = uid;

            return dbCandidate;
        }
        public ServiceMessage PostCreate(Candidate dbOperation, V_UserCandidate LoggedInUser)
        {
            Expression<Func<User, bool>> SpecificEntries = c => c.UserID == LoggedInUser.UserID;
            List<User> images = UserRepository.FindBy(SpecificEntries);
            User image = images.First();
            image.UserStage = LoggedInUser.UserStage;
            UserRepository.Edit(image);
            UserRepository.Save();
            Candidate dbCandidate = new Candidate();
            dbCandidate = ConvertCandidateObject(dbOperation);
            CandidateRepository.Edit(dbCandidate);
            CandidateRepository.Save();
            return new ServiceMessage();
        }
        private Candidate ConvertCandidateObject(Candidate dbOperation)
        {
            Candidate dbCandidate = new Candidate();
            dbCandidate.CandidateID = dbOperation.CandidateID;
            dbCandidate.CName = dbOperation.CName;
            dbCandidate.FatherName = dbOperation.FatherName;
            dbCandidate.BloodGroupID = dbOperation.BloodGroupID;
            dbCandidate.CNICNo = dbOperation.CNICNo;
            dbCandidate.GenderID = dbOperation.GenderID;
            dbCandidate.MartialStatusID = dbOperation.MartialStatusID;
            dbCandidate.DOB = dbOperation.DOB;
            dbCandidate.DomicileCityID = dbOperation.DomicileCityID;
            dbCandidate.CountryID = dbOperation.CountryID;
            dbCandidate.CityID = dbOperation.CityID;
            dbCandidate.NationalityCountryID = dbOperation.NationalityCountryID;
            dbCandidate.EmailID = dbOperation.EmailID;
            dbCandidate.Address = dbOperation.Address;
            dbCandidate.Objective = dbOperation.Objective;
            dbCandidate.CImage = dbOperation.CImage;
            dbCandidate.UserID = dbOperation.UserID;
            dbCandidate.CellNo = dbOperation.CellNo;
            dbCandidate.LandlineNo = dbOperation.LandlineNo;
            dbCandidate.ReligionID = dbOperation.ReligionID;
            return dbCandidate;
        }
        public VMCandidateProfileView GetProfileDetails(int? CandidateID, int? JobID)
        {
            VMCandidateProfileView vmProfileView = new VMCandidateProfileView();
            Expression<Func<Candidate, bool>> SpecificPosition = c => c.CandidateID == CandidateID;
            vmProfileView.PersonalDetails = CandidateRepository.GetSingle((int)CandidateID);
            Expression<Func<V_Candidate_EduDetail, bool>> SpecificPosition2 = c => c.CandidateID == CandidateID;
            vmProfileView.EducationalDetails = VEducationRepository.FindBy(SpecificPosition2);
            Expression<Func<V_Candidate_Exp, bool>> SpecificPosition3 = c => c.CandidateID == CandidateID;
            vmProfileView.ExperienceDetails = VCandidateExpRepository.FindBy(SpecificPosition3);
            Expression<Func<CompensationDetail, bool>> SpecificPosition4 = c => c.CandidateID == CandidateID;
            vmProfileView.CompensationDetails = CompensationDetailRepository.FindBy(SpecificPosition4).First();
            Expression<Func<MiscellaneousDetail, bool>> SpecificPosition5 = c => c.CandidateID == CandidateID;
            vmProfileView.MiscellaneousDetails = MiscellaneousDetailRepository.FindBy(SpecificPosition5).First();
            Expression<Func<CandidateStrength, bool>> SpecificPosition6 = c => c.CandidateID == CandidateID;
            vmProfileView.SelfAssessment = CandidateStrengthRepository.FindBy(SpecificPosition6).First();
            return vmProfileView;
        }
        public byte[] GetImageFromDataBase(int id)
        {
            Expression<Func<CandidatePhoto, bool>> SpecificEntries = c => c.CandidateID == id;
            List<CandidatePhoto> images = CandidatePhotoRepository.FindBy(SpecificEntries);
            if (images.Count > 0)
                return images[0].CandidatePic;
            else
                return null;
        }
        public void SaveImageInDatabase(byte[] img, int empID)
        {
            Expression<Func<CandidatePhoto, bool>> SpecificEntries = c => c.CandidateID == empID;
            List<CandidatePhoto> images = CandidatePhotoRepository.FindBy(SpecificEntries);
            if (images.Count > 0)
            {
                CandidatePhoto empImage = new CandidatePhoto();
                empImage.CandidateID = empID;
                empImage.CandidatePic = img;
                CandidatePhotoRepository.Edit(empImage);
                UnitOfWork.Commit();
            }
            else
            {
                CandidatePhoto empImage = new CandidatePhoto();
                empImage.CandidateID = empID;
                empImage.CandidatePic = img;
                CandidatePhotoRepository.Add(empImage);
                UnitOfWork.Commit();
            }
        }
    }
}
