using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RMSSERVICES.Generic
{
    public class ServiceMessage
    {
        public List<string> ValidationMessage { get; set; }
    }

    public interface IService
    {
    }
    public interface IEntityService<T> :IService where T : class
    {
        List<T> GetIndex();
        List<T> GetIndexSpecific(Expression<Func<T, bool>> predicate);
        T GetEdit(int id);
        T GetDelete(int id);
        ServiceMessage PostEdit(T obj);
        ServiceMessage PostCreate(T obj);
        ServiceMessage PostDelete(T obj);
    }
}
