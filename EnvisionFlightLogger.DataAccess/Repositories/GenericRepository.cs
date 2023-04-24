using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnvisionFlightLogger.DataAccess.Repositories
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class, new()
    {
        private readonly DataContext _context;

        public GenericRepository(DataContext context)
        {
            _context = context;
        }

        public IQueryable<T> GetAll()
        {
            return _context.Table<T>().AsQueryable();
        }

        public T GetById(object id)
        {
            return _context.Get<T>(id);
        }

        public void Insert(T entity)
        {
            _context.Insert(entity);
        }

        public void Update(T entity)
        {
            _context.Update(entity);
        }

        public void Delete(object id)
        {
            _context.Delete<T>(id);
        }

        public void Delete(T entity)
        {
            _context.Delete(entity);
        }
    }
}
