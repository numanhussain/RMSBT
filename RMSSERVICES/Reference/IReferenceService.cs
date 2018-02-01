using RMSCORE.Models.Helper;
using RMSCORE.Models.Main;
using RMSCORE.Models.Operation;
using RMSSERVICES.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMSSERVICES.Reference
{
    public interface IReferenceService
    {
        List<VMReferenceIndex> GetIndex(Int64 cid);
        VMReferenceOperation GetCreate(long? id);
        ServiceMessage PostCreate(VMReferenceOperation obj);
        VMReferenceOperation GetEdit(int id);
        ServiceMessage PostEdit(VMReferenceOperation obj);
        VMReferenceOperation GetDelete(int? id);
        ServiceMessage PostDelete(VMReferenceOperation obj, int? id);
        ValidationMessage ValidateNewEntry(VMReferenceOperation obj);
    }
}
