﻿using System;
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
using System.Globalization;
using System.Threading;

namespace RMSSERVICES.PersonalDetail
{
    public class CandidateService : ICandidateService
    {
        IUnitOfWork UnitOfWork;
        IRepository<Candidate> CandidateRepository;
        IRepository<V_UserCandidate> VUserRepositiory;
        IRepository<User> UserRepository;
        IRepository<CandidatePhoto> CandidatePhotoRepository;
        IRepository<V_Candidate_EduDetail> VEducationRepository;
        IRepository<V_Candidate_Exp> VCandidateExpRepository;
        IRepository<CompensationDetail> CompensationDetailRepository;
        IRepository<V_Candidate_Miscellaneous> MiscellaneousDetailRepository;
        IRepository<CandidateStrength> CandidateStrengthRepository;
        IRepository<V_Candidate_Reference> VCandidateReferenceRepositiory;
        IRepository<V_Candidate_Skills> VCandidateSkillsRepositiory;
        IRepository<V_CandidateProfile> VCandidateRepositiory;
        public CandidateService(IUnitOfWork unitOfWork,
        IRepository<CandidatePhoto> candidatePhotoRepository, IRepository<V_Candidate_EduDetail> vEducationRepository, IRepository<Candidate> candidateRepository, IRepository<User> userRepository, IRepository<V_UserCandidate> vuserRepositiory,
        IRepository<V_Candidate_Exp> vCandidateExpRepository, IRepository<CompensationDetail> compensationDetailRepository, IRepository<V_Candidate_Miscellaneous> miscellaneousDetailRepository, IRepository<CandidateStrength> candidateStrengthRepository,
        IRepository<V_Candidate_Reference> vCandidateReferenceRepositiory,
        IRepository<V_Candidate_Skills> vCandidateSkillsRepositiory, IRepository<V_CandidateProfile> vCandidateRepositiory)
        {
            UnitOfWork = unitOfWork;
            CandidateRepository = candidateRepository;
            CandidatePhotoRepository = candidatePhotoRepository;
            VUserRepositiory = vuserRepositiory;
            UserRepository = userRepository;
            VEducationRepository = vEducationRepository;
            VCandidateExpRepository = vCandidateExpRepository;
            CompensationDetailRepository = compensationDetailRepository;
            MiscellaneousDetailRepository = miscellaneousDetailRepository;
            CandidateStrengthRepository = candidateStrengthRepository;
            VCandidateReferenceRepositiory = vCandidateReferenceRepositiory;
            VCandidateSkillsRepositiory = vCandidateSkillsRepositiory;
            VCandidateRepositiory = vCandidateRepositiory;
        }
        public List<Candidate> GetIndex()
        {
            return CandidateRepository.GetAll();
        }
        public Candidate GetCreate(int cid, int uid)
        {
            Candidate dbCandidate = CandidateRepository.GetSingle(cid);
            dbCandidate.UserID = uid;
            dbCandidate.CandidateID = cid;
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
            dbCandidate.CName = ConvertToTitleCase(dbOperation.CName);
            dbCandidate.FatherName = ConvertToTitleCase(dbOperation.FatherName);
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
            dbCandidate.AreaOfInterestID = dbOperation.AreaOfInterestID;
            if (dbOperation.OtherCityName != null)
            {
                dbCandidate.OtherCityName = ConvertToTitleCase(dbOperation.OtherCityName);
            }
            dbCandidate.SalutationID = dbOperation.SalutationID;
            if (dbOperation.OtherAreaName != null)
            {
                dbCandidate.OtherAreaName = ConvertToTitleCase(dbOperation.OtherAreaName);
            }
            dbCandidate.WorkPermitYes = dbOperation.WorkPermitYes;
            dbCandidate.WorkPermitNo = dbOperation.WorkPermitNo;
            if (dbOperation.OtherDomicileCityName != null)
            {
                dbCandidate.OtherDomicileCityName = ConvertToTitleCase(dbOperation.OtherDomicileCityName);
            }
            if (dbOperation.OtherPakistaniCityName != null)
            {
                dbCandidate.OtherPakistaniCityName = ConvertToTitleCase(dbOperation.OtherPakistaniCityName);
            }
            if (dbOperation.OtherPakistaniCityName != null)
            {
                dbCandidate.Tehsil = ConvertToTitleCase(dbOperation.Tehsil);
            }
            dbCandidate.PostalCode = dbOperation.PostalCode;
            dbCandidate.EditDate = DateTime.Now;
            return dbCandidate;
        }
        public VMCandidateProfileView GetProfileDetails(int? CandidateID, int? JobID)
        {
            VMCandidateProfileView vmProfileView = new VMCandidateProfileView();
            Expression<Func<V_CandidateProfile, bool>> SpecificPosition = c => c.CandidateID == CandidateID;
            vmProfileView.PersonalDetails = VCandidateRepositiory.GetSingle((int)CandidateID);
            Expression<Func<V_Candidate_EduDetail, bool>> SpecificPosition2 = c => c.CandidateID == CandidateID;
            vmProfileView.EducationalDetails = VEducationRepository.FindBy(SpecificPosition2);
            Expression<Func<V_Candidate_Miscellaneous, bool>> SpecificPosition3 = c => c.CandidateID == CandidateID;
            if (MiscellaneousDetailRepository.FindBy(SpecificPosition3).Count > 0)
            {
                vmProfileView.MiscellaneousDetails = MiscellaneousDetailRepository.FindBy(SpecificPosition3).First();
            }
            Expression<Func<V_Candidate_Skills, bool>> SpecificPosition4 = c => c.CandidateID == CandidateID;
            vmProfileView.Skill = VCandidateSkillsRepositiory.FindBy(SpecificPosition4);
            Expression<Func<V_Candidate_Exp, bool>> SpecificPosition5 = c => c.CandidateID == CandidateID;
            vmProfileView.ExperienceDetails = VCandidateExpRepository.FindBy(SpecificPosition5);
            //Expression<Func<CompensationDetail, bool>> SpecificPosition6 = c => c.CandidateID == CandidateID;
            //if (CompensationDetailRepository.FindBy(SpecificPosition6).Count > 0)
            //{
            //    vmProfileView.CompensationDetails = CompensationDetailRepository.FindBy(SpecificPosition6).First();
            //}
            Expression<Func<CandidateStrength, bool>> SpecificPosition7 = c => c.CandidateID == CandidateID;
            if (CandidateStrengthRepository.FindBy(SpecificPosition7).Count > 0)
            {
                vmProfileView.SelfAssessment = CandidateStrengthRepository.FindBy(SpecificPosition7).First();
            }
            Expression<Func<V_Candidate_Reference, bool>> SpecificPosition8 = c => c.CandidateID == CandidateID;
            vmProfileView.Referrence = VCandidateReferenceRepositiory.FindBy(SpecificPosition8);
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
        public string ConvertToTitleCase(string obj)
        {
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            TextInfo textInfo = cultureInfo.TextInfo;
            string val = textInfo.ToLower(obj);
            string val2 = textInfo.ToTitleCase(val);
            return val2;
        }
    }
}
