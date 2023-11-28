using EmployeeManagement.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.UnitOfWOrk.Interfaces
{
    public interface IRepository<T> where T: class
    {
        public Task<T> GetByIdAsync(Guid id);
        public Task<List<T>> GetAllAsync();
        public IQueryable<T> Querriable(Expression<Func<T, bool>> expression);
        public Task AddAsync(T entity);
        public Task AddRangeAsync(IEnumerable<T> entities);
        public void Remove(T entity);
        public void Update(T entity);
        public List<T> GetAll();
    }
}
