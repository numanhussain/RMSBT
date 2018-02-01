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

namespace RMSSERVICES.Education
{
    public interface IEduDetailService
    {
        List<VMEduDetailIndex> GetIndex( Int64 cid);
        VMEduDetailOperation GetCreate(int? id);
        ServiceMessage PostCreate(VMEduDetailOperation obj);
        VMEduDetailOperation GetEdit(int id);
        ServiceMessage PostEdit(VMEduDetailOperation obj);
        VMEduDetailOperation GetDelete(int? id);
        ServiceMessage PostDelete(VMEduDetailOperation obj, int? id);
        ValidationMessage ValidateNewEntry(VMEduDetailOperation obj);
    }
}
