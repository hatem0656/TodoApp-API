using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.DAL.Specification;

namespace Todo.DAL.Interfaces
{
    public interface IBaseRespository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GetAllWithSpecs(ISpecification<T> specification );
        Task<T?> GetWithSpecs(ISpecification<T> specification);
        Task<T?> GetById(Guid id);
        Task<T> Add(T item);
        void Update( T item);
        void Delete(T item);
    }
}
