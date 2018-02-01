using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Validation;

namespace RMSREPO.Generic
{
    public class Repository<T> : IRepository<T> where T:class
    {
        protected DbContext _entities;
        protected readonly IDbSet<T> _dbset;
        public Repository(DbContext context)
        {
            _entities = context;
            _dbset = context.Set<T>();
        }
        public T Add(T entity)
        {
            try { return _dbset.Add(entity); }
            catch (DbEntityValidationException dbEx)
            {
                string errorMessage = "";
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        errorMessage += Environment.NewLine + string.Format("Property: {0} Error: {1}",
                        validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
                throw new Exception(errorMessage, dbEx);
            }
        }

        public T Delete(T entity)
        {
            try
            {
                return _dbset.Remove(entity);
            }
            catch (DbEntityValidationException dbEx)
            {
                string errorMessage = "";
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        errorMessage += Environment.NewLine + string.Format("Property: {0} Error: {1}",
                        validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
                throw new Exception(errorMessage, dbEx);
            }
        }

        public void Edit(T entity)
        {
            try
            {
                _entities.Entry(entity).State = EntityState.Modified;
            }
            catch (DbEntityValidationException dbEx)
            {
                string errorMessage = "";
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        errorMessage += Environment.NewLine + string.Format("Property: {0} Error: {1}",
                        validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
                throw new Exception(errorMessage, dbEx);
            }
        }

        public List<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            IEnumerable<T> query = _dbset.Where(predicate).AsNoTracking().AsEnumerable();
            return query.ToList();
        }

        public virtual List<T> GetAll()
        {
            return _dbset.AsNoTracking().AsEnumerable<T>().ToList();
        }

        public T GetSingle(int id)
        {
            return _dbset.Find(id);
        }

        public void Save()
        {
            try
            {
                _entities.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                string errorMessage = "";
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        errorMessage += Environment.NewLine + string.Format("Property: {0} Error: {1}",
                        validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
                throw new Exception(errorMessage, dbEx);
            }
        }

        public int TotalClientRows(Expression<Func<T, bool>> predicate)
        {
            return _dbset.Where(predicate).Count();
        }
    }
}
