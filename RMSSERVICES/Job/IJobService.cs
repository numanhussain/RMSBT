using RMSCORE.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMSSERVICES.Job
{
    public interface IJobService
    {
        List<V_AppliedJob> GetAppliedJob(int cid);
    }
}
