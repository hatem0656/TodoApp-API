using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.DAL.Interfaces;
using Todo.DAL.Specification;

namespace Todo.DAL.EF
{
    public class BaseRepository<T> : IBaseRespository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _set;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
            _set = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _set.ToListAsync();
        }
        public async Task<IEnumerable<T>> GetAllWithSpecs(ISpecification<T> specification )
        {
            return await GetQueryWithSpecs(specification).ToListAsync();
        }
        public async Task<T?> GetById(Guid id)
        {
           return await _set.FindAsync(id);
        }
        public async Task<T?> GetWithSpecs(ISpecification<T> specification)
        {
            return await GetQueryWithSpecs(specification).FirstOrDefaultAsync();
        }
        public async Task<T> Add(T item)
        {
           await _set.AddAsync(item);
            _context.SaveChanges();
            return item; 
        }
        public void Update( T item)
        {
            _context.Update(item);
            _context.SaveChanges();
            
        }
        public void Delete(T item)
        {
            _context.Remove(item);
            _context.SaveChanges();

        }
        private IQueryable<T> GetQueryWithSpecs(ISpecification<T> specification)
        {
            return SpecificationEvaluator<T>.GetQuery(_set.AsQueryable(), specification);
        }



    }
}
