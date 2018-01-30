using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeBenefits.Data
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        void Add(TEntity entity);

        IEnumerable<TEntity> GetAll();

        TEntity GetById(int id);

        void Update(TEntity item);

        void Delete(int id);
    }
}