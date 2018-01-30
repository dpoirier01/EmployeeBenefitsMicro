using EmployeeBenefits.Commands.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeBenefits.Data
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, IEntity
    {
        BenefitsContext _context;

        public GenericRepository(BenefitsContext context)
        {
            _context = context;
        }

        public void Add(TEntity item)
        {
            _context.Set<TEntity>().Add(item);
            _context.SaveChanges();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().ToList();
        }

        public TEntity GetById(int id)
        {
            return _context.Set<TEntity>().FirstOrDefault(e => e.Id == id);
        }

        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = GetById(id);
            _context.Set<TEntity>().Remove(entity);
            _context.SaveChanges();
        }
    }
}
