using EmployeeManagement.DAL.Context;
using EmployeeManagement.DAL.Model;
using EmployeeManagement.UnitOfWOrk.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.UnitOfWOrk.Implementations
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly EmployeeManagementContext _context;
        public Repository(EmployeeManagementContext context)
        {
            _context = context;
        }
        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _context.Set<T>().AddRangeAsync(entities);
        }

        public List<T> GetAll()
        {
            return _context.Set<T>().Where(x =>
                !(x as ModelBase).IsDeleted && (x as ModelBase).IsActive).ToList();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().Where(x => 
                !(x as ModelBase).IsDeleted && (x as ModelBase).IsActive).ToListAsync();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public IQueryable<T> Querriable(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Where(expression);
        }

        public void Remove(T entity)
        {
            if(typeof(ModelBase).IsAssignableFrom(typeof(T)))
            {
                (entity as ModelBase).IsDeleted = true;
                (entity as ModelBase).ModifiedTime = DateTime.Now;
                _context.Set<T>().Attach(entity);
                _context.Entry(entity).State = EntityState.Modified;
            }
            else
            {
                _context.Set<T>().Remove(entity);
            }
        }

        public void Update(T entity)
        {
            if (typeof(ModelBase).IsAssignableFrom(typeof(T)))
            {
                (entity as ModelBase).ModifiedTime = DateTime.Now;
                _context.Set<T>().Attach(entity);
                _context.Entry(entity).State = EntityState.Modified;
            }
            else
            {
                _context.Set<T>().Remove(entity);
            }
        }
    }
}
