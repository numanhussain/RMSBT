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
        IRepository<User> UserRepositiory;
        IRepository<VMCandidateIndex> VMCandidateRepository;
        IRepository<CandidatePhoto> CandidatePhotoRepository;
        public CandidateService(IUnitOfWork unitOfWork,
        IRepository<CandidatePhoto> candidatePhotoRepository,IRepository<Candidate> candidateRepository, IRepository<User> userRepositiory, IRepository<VMCandidateIndex> vmCandidateRepository, IRepository<V_UserCandidate> vuserRepositiory)
        {
            UnitOfWork = unitOfWork;
            CandidateRepository = candidateRepository;
            VMCandidateRepository = vmCandidateRepository;
            CandidatePhotoRepository = candidatePhotoRepository;
           VUserRepositiory = vuserRepositiory;
            UserRepositiory = userRepositiory;
        }
        public List<Candidate> GetIndex()
        {
            return CandidateRepository.GetAll();
        }
        public Candidate GetCreate(int cid,int uid)
        {
            Candidate dbCandidate = CandidateRepository.GetSingle(cid);
            dbCandidate.UserID = uid;
            return dbCandidate;
        }
        public ServiceMessage PostCreate(Candidate dbOperation, V_UserCandidate LoggedInUser)
        {
            Expression<Func<User, bool>> SpecificEntries = c => c.UserID == LoggedInUser.UserID;
            List<User> images = UserRepositiory.FindBy(SpecificEntries);
            User image = images.First();
            image.UserStage = "ProfileCompleted";
            UserRepositiory.Edit(image);
            UserRepositiory.Save();
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
            dbCandidate.MartialStatusID = dbOperation.MartialStatusID;
            dbCandidate.DOB = dbOperation.DOB;
            dbCandidate.DomicileCityID = dbOperation.DomicileCityID;
            dbCandidate.CountryID = dbOperation.CountryID;
            dbCandidate.CityID = dbOperation.CityID;
            dbCandidate.NationalityCountryID= dbOperation.NationalityCountryID;
            dbCandidate.EmailID = dbOperation.EmailID;
            dbCandidate.Address = dbOperation.Address;
            dbCandidate.Objective = dbOperation.Objective;
            dbCandidate.CImage = dbOperation.CImage;
            dbCandidate.UserID = dbOperation.UserID;
            dbCandidate.CellNo =dbOperation.CellNo;
            dbCandidate.LandlineNo = dbOperation.LandlineNo;
            return dbCandidate;
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
