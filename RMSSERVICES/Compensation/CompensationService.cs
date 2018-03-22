using RMSCORE.EF;
using RMSCORE.Models.Operation;
using RMSREPO.Generic;
using RMSSERVICES.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RMSSERVICES.Compensation
{
    public class CompensationService : ICompensationService
    {
        #region -- Service Variables --
        IUnitOfWork UnitOfWork;
        IRepository<CompensationDetail> CompensationRepository;
        IRepository<Candidate> CandidateRepository;
        IRepository<User> UserRepository;
        public CompensationService(IUnitOfWork unitOfWork, IRepository<Candidate> candidateRepository, IRepository<CompensationDetail> compensationRepository, IRepository<User> userRepository)
        {
            UnitOfWork = unitOfWork;
            CompensationRepository = compensationRepository;
            CandidateRepository = candidateRepository;
            UserRepository = userRepository;
        }
        #endregion
        #region -- Service Interface Implementation --
        public CompensationDetail GetCreate(int id)
        {
            Expression<Func<CompensationDetail, bool>> SpecificClient = c => c.CandidateID == id;
            CompensationDetail dbCompensation = new CompensationDetail();
            dbCompensation.CandidateID = id;
            if (CompensationRepository.FindBy(SpecificClient).Count() > 0)
            {
                dbCompensation = CompensationRepository.FindBy(SpecificClient).First();
            }
            return dbCompensation;
        }
        public ServiceMessage PostCreate(CompensationDetail obj,V_UserCandidate LoggedInUser)
        {
            Expression<Func<User, bool>> SpecificEntries = c => c.UserID == LoggedInUser.UserID;
            List<User> images = UserRepository.FindBy(SpecificEntries);
            User image = images.First();
            image.UserStage = LoggedInUser.UserStage;
            UserRepository.Edit(image);
            UserRepository.Save();
            Expression<Func<CompensationDetail, bool>> SpecificClient = c => c.CandidateID == obj.CandidateID;
            CompensationDetail dbCompensation = new CompensationDetail();
            if (CompensationRepository.FindBy(SpecificClient).Count() > 0)
            {
                dbCompensation = ConvertCompensationObject(obj);
                CompensationRepository.Edit(dbCompensation);
                CompensationRepository.Save();

            }
            else
            {
                dbCompensation = ConvertCompensationObject(obj);
                CompensationRepository.Add(dbCompensation);
                CompensationRepository.Save();
            }
                
            return new ServiceMessage();
        }
        #endregion
        #region -- Service Private Methods --
        private CompensationDetail ConvertCompensationObject(CompensationDetail obj)
        {
            CompensationDetail dbCompensation = new CompensationDetail();
            dbCompensation.CID = obj.CID;
            dbCompensation.MBSalary = obj.MBSalary;
            dbCompensation.MGSalary = obj.MGSalary;
            dbCompensation.Bonus = obj.Bonus;
            dbCompensation.BBonus = obj.BBonus;
            dbCompensation.GBonus = obj.GBonus;
            dbCompensation.BonusPerYear = obj.BonusPerYear;
            dbCompensation.LFA = obj.LFA;
            dbCompensation.BLFA = obj.BLFA;
            dbCompensation.GLFA = obj.GLFA;
            dbCompensation.LFAPerYear = obj.LFAPerYear;
            dbCompensation.OT = obj.OT;
            dbCompensation.BOT = obj.BOT;
            dbCompensation.GOT = obj.GOT;
            dbCompensation.TransportAllowence = obj.TransportAllowence;
            dbCompensation.MobileAllowence = obj.MobileAllowence;
            dbCompensation.MobileUserLimit = obj.MobileUserLimit;
            dbCompensation.CarEntitlement = obj.CarEntitlement;
            dbCompensation.BuyBackOption = obj.BuyBackOption;
            dbCompensation.AccomdAllowence = obj.AccomdAllowence;
            dbCompensation.COLA = obj.AccomdAllowence;
            dbCompensation.Other = obj.Other;
            dbCompensation.Food = obj.Food;
            dbCompensation.Free = obj.Free;
            dbCompensation.Subsidized = obj.Subsidized;
            dbCompensation.OPDInsurance = obj.OPDInsurance;
            dbCompensation.IPInsurance = obj.IPInsurance;
            dbCompensation.LifeInsurance = obj.LifeInsurance;
            dbCompensation.ProvidentFund = obj.ProvidentFund;
            dbCompensation.BProvidentFund = obj.BProvidentFund;
            dbCompensation.GProvidentFund = obj.GProvidentFund;
            dbCompensation.ProvidentFundPerYear = obj.ProvidentFundPerYear;
            dbCompensation.Gratuity = obj.Gratuity;
            dbCompensation.BGratuity = obj.BGratuity;
            dbCompensation.GGratuity = obj.GGratuity;
            dbCompensation.AnnualTAllowence = obj.AnnualTAllowence;
            dbCompensation.CasualTAllowence = obj.CasualTAllowence;
            dbCompensation.MedTAllowence = obj.MedTAllowence;
            dbCompensation.TAllowenceWorkDay = obj.TAllowenceWorkDay;
            dbCompensation.ExpectedSalary = obj.ExpectedSalary;
            dbCompensation.OtherBenifits = obj.OtherBenifits;
            dbCompensation.CandidateID = obj.CandidateID;
            return dbCompensation;
        }
        #endregion
    }
}
