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

namespace RMSSERVICES.Skill
{
    public interface ISkillService
    {
        List<VMSkillIndex> GetIndex(int cid);
        VMSkillOperation GetCreate(int id);
        ServiceMessage PostCreate(VMSkillOperation obj);
        VMSkillOperation GetEdit(int id);
        ServiceMessage PostEdit(VMSkillOperation obj);
        VMSkillOperation GetDelete(int? id);
        ServiceMessage PostDelete(VMSkillOperation obj);
    }
}
