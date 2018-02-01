using RMSREPO.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RMSSERVICES.Generic
{
    public class EntityService<T> : IEntityService<T> where T : class
    {
        IUnitOfWork _unitOfWork;
        IRepository<T> _repository;
        public EntityService(IUnitOfWork unitOfWork, IRepository<T> repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }
        public List<T> GetIndex()
        {
            return _repository.GetAll();
        }
        public T GetCreate()
        {
            throw new NotImplementedException();
        }
        public ServiceMessage PostCreate(T obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("entity");
            }
            _repository.Add(obj);
            _unitOfWork.Commit();
            return new ServiceMessage();
        }
        public T GetEdit(int id)
        {
            return _repository.GetSingle(id);
        }
        public ServiceMessage PostEdit(T obj)
        {
            if (obj == null) throw new ArgumentNullException("entity");
            _repository.Edit(obj);
            _unitOfWork.Commit();
            return new ServiceMessage();
        }
        public T GetDelete(int id)
        {
            return _repository.GetSingle(id);
        }
        public ServiceMessage PostDelete(T obj)
        {
            if (obj == null) throw new ArgumentNullException("entity");
            _repository.Delete(obj);
            _unitOfWork.Commit();
            return new ServiceMessage();
        }
        public T Detail(int id)
        {
            return _repository.GetSingle(id);
        }
        public List<T> GetIndexSpecific(Expression<Func<T, bool>> predicate)
        {
            return _repository.FindBy(predicate);
        }
    }
}
