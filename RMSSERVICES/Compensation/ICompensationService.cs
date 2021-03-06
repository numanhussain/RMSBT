﻿using RMSCORE.EF;
using RMSCORE.Models.Operation;
using RMSSERVICES.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMSSERVICES.Compensation
{
    public interface ICompensationService
    {
        CompensationDetail GetCreate(int id);
        ServiceMessage PostCreate(CompensationDetail dbOperation, V_UserCandidate LoggedInUser);
    }
}
