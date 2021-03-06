﻿using RMSCORE.EF;
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
        List<VMEduDetailIndex> GetIndex( int cid);
        VMEduDetailOperation GetCreate(int id);
        ServiceMessage PostCreate(VMEduDetailOperation obj,V_UserCandidate LoggedInUser);
        VMEduDetailOperation GetEdit(int id);
        ServiceMessage PostEdit(VMEduDetailOperation obj);
        VMEduDetailOperation GetDelete(int? id);
        ServiceMessage PostDelete(VMEduDetailOperation obj);
    }
}
