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
<<<<<<< HEAD
        List<VMOpenJobIndex> GetOpenJob(V_UserCandidate obj);
        List<VMAppliedJobIndex> GetAppliedJob(int cid);
        VMOpenJobIndex GetJobDetail(int id, V_UserCandidate LoggedInUser);
=======
        List<VMAppliedJobIndex> GetAppliedJob(int cid);
>>>>>>> 1df9c303756e286c8f59847e4dacfe414b8b89f3
    }
}
