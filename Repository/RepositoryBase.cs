using FoodHub.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore;

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
    }
}
