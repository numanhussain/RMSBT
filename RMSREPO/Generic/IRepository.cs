using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace RMSREPO.Generic
{
    public interface IRepository<T> where T : class
    {
        List<T> GetAll();
        List<T> FindBy(Expression<Func<T, bool>> predicate);
        T Add(T entity);
        T Delete(T entity);
        void Edit(T entity);
        T GetSingle(int id);
        void Save();
        int TotalClientRows(Expression<Func<T, bool>> predicate);
    }
}
