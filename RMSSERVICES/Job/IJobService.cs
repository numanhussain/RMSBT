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
        //List<VMOpenJobIndex> GetOpenJob(V_UserCandidate obj);
        List<VMAppliedJobIndex> GetAppliedJob(int cid);
        List<VMOpenJobIndex> JobIndex();
        VMOpenJobIndex GetJobDetail(int id, V_UserCandidate LoggedInUser);
        VMOpenJobIndex GetJobDetailIndex(int id);
        List<VMOpenJobIndex> GetOpenJobIndex();
        string CheckForProfileCompletion(V_UserCandidate LoggedInUser);
    }
}
