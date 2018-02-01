using RMSCORE.EF;
using RMSREPO.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RMSSERVICES.CandidateImage
{
    public class GetImageService:IGetImageService
    {
        IUnitOfWork UnitOfWork;
        IRepository<CandidatePhoto> CandidatePicRepository;
        public GetImageService(IUnitOfWork unitOfWork, IRepository<CandidatePhoto> candidatepicRepository)
        {
            UnitOfWork = unitOfWork;
            CandidatePicRepository = candidatepicRepository;
        }
        public byte[] GetImageFromDataBase(int Id)
        {
            Expression<Func<CandidatePhoto, bool>> SpecificImage = c => c.CandidateID == Id;            
             var candidatepic = CandidatePicRepository.FindBy(SpecificImage);
            if(candidatepic.Count>0)
            {
                var q = candidatepic.First(aa => aa.CandidateID == Id);
                byte[] cover = q.CandidatePic;
                return cover;
            }
            else
            {
                return null;
            }
        }
    }
}
