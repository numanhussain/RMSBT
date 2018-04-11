using RMSCORE.EF;
using RMSREPO.Generic;
using RMSSERVICES.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RMSSERVICES.Self_Assessment
{
    public class SelfAssessmentService : ISelfAssessmentService
    {
        #region -- Service Variables --
        IUnitOfWork UnitOfWork;
        IRepository<CandidateStrength> CandidateStrengthRepository;
        IRepository<User> UserRepository;
        public SelfAssessmentService(IUnitOfWork unitOfWork, IRepository<CandidateStrength> candidateStrengthRepository, IRepository<User> userRepository)
        {
            UnitOfWork = unitOfWork;
            CandidateStrengthRepository = candidateStrengthRepository;
            UserRepository = userRepository;
        }
        #endregion
        #region -- Service Interface Implementation --    
        public CandidateStrength GetIndex(int id)
        {
            Expression<Func<CandidateStrength, bool>> SpecificClient = c => c.CandidateID == id;
            CandidateStrength dbCandidate = new CandidateStrength();
            dbCandidate.CandidateID = id;
            if (CandidateStrengthRepository.FindBy(SpecificClient).Count() > 0)
            {
                dbCandidate = CandidateStrengthRepository.FindBy(SpecificClient).First();
            }
            return dbCandidate;
        }
        public ServiceMessage PostIndex(CandidateStrength obj, V_UserCandidate LoggedInUser)
        {
            Expression<Func<User, bool>> SpecificEntries = c => c.UserID == LoggedInUser.UserID;
            List<User> images = UserRepository.FindBy(SpecificEntries);
            User image = images.First();
            image.UserStage = LoggedInUser.UserStage;
            UserRepository.Edit(image);
            UserRepository.Save();
            Expression<Func<CandidateStrength, bool>> SpecificClient = c => c.CandidateID == obj.CandidateID;
            CandidateStrength dbCandidate = new CandidateStrength();
            if (CandidateStrengthRepository.FindBy(SpecificClient).Count() > 0)
            {
                dbCandidate.StrengthID = obj.StrengthID;
                dbCandidate.Strengths = obj.Strengths;
                dbCandidate.AreaOfImprovement = obj.AreaOfImprovement;
                dbCandidate.MeetRequirements = obj.MeetRequirements;
                dbCandidate.CandidateID = obj.CandidateID;
                CandidateStrengthRepository.Edit(dbCandidate);
                CandidateStrengthRepository.Save();
            }
            else
            {
                dbCandidate.StrengthID = obj.StrengthID;
                dbCandidate.Strengths = obj.Strengths;
                dbCandidate.AreaOfImprovement = obj.AreaOfImprovement;
                dbCandidate.MeetRequirements = obj.MeetRequirements;
                dbCandidate.CandidateID = obj.CandidateID;
                CandidateStrengthRepository.Add(dbCandidate);
                CandidateStrengthRepository.Save();
            }
            return new ServiceMessage();
        }
        #endregion
        #region -- Service Private Methods --
        #endregion
    }
}
