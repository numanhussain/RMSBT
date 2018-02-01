using RMSCORE.EF;
using RMSCORE.Models.Helper;
using RMSCORE.Models.Main;
using RMSCORE.Models.Operation;
using RMSSERVICES.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMSSERVICES.PersonalDetail
{
    public interface ICandidateService
    {
        List<Candidate> GetIndex();
        Candidate GetCreate(long id);
        ServiceMessage PostCreate(Candidate dbOperation);
        byte[] GetImageFromDataBase(int id);
        void SaveImageInDatabase(byte[] img, int empID);
    }
}
