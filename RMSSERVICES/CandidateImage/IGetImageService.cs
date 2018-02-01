using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMSSERVICES.CandidateImage
{
    public interface IGetImageService
    {
         byte[] GetImageFromDataBase(int Id);
    }
}
