using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnvisionFlightLogger.DataAccess.Repositories
{
    public interface IGenericRepository<T> where T : class, new()
    {
        IQueryable<T> GetAll();
        T GetById(object id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(object id);
        void Delete(T entity);
    }
}
