using RMSCORE.EF;
using RMSCORE.Models.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMSSERVICES.Job
{
    public interface IJobService
    {
        List<VMAppliedJobIndex> GetAppliedJob(int cid);
    }
}
