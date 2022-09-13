using FoodHub.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FoodHub.Repository
{
    public class RepositoryBase<T, K> : IRepositoryBase<T, K> where T : class
    {
        private DbContext _context { get; set; }
        private DbSet<T> _entity;
        public RepositoryBase(DbContext context)
        {
            _context = context;
            _entity = _context.Set<T>();
        }

        public IEnumerable<T> List() => _entity.ToList();

        public IQueryable<T> Find(Expression<Func<T, bool>> predicate, bool includeSoftDeleted = false, params Expression<Func<T, object>>[] includes)
        {
            var items = _entity.AsNoTracking().AsQueryable<T>();

            if (includes != null)
            {
                foreach (var include in includes) items = items.Include(include);
            }
            return includeSoftDeleted ? items.IgnoreQueryFilters().Where(predicate) : items.Where(predicate);
        }

        public T GetById(K id)
        {
            var entity = _entity.Find(id);
            if (entity != null)
            {
                _context.Entry(entity).State = EntityState.Detached;
                return entity;
            }
            return null;
        }

        public void Create(T entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
            _context.Entry(entity).State = EntityState.Detached;
        }

        public void Delete(T entity)
        {
            _entity.Remove(entity);
            _context.SaveChanges();
        }

        public object List(Func<object, bool> value)
        {
            throw new NotImplementedException();
        }
    }
}
