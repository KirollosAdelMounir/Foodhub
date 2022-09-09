using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace WebApplication2.Repository
{
    public class Repository<T, K> : IRepository<T, K> where T : class
    {
        protected DbContext _context;
        private DbSet<T> _entitySet;

        public Repository(DbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _entitySet = _context.Set<T>();
        }

        public T Add(T item)
        {
            _entitySet.Add(item);
            _context.SaveChanges();
            _context.Entry(item).State = EntityState.Detached;
            return item;
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> predicate, bool includeSoftDeleted = false, params Expression<Func<T, object>>[] includes)
        {
            var items = _entitySet.AsNoTracking().AsQueryable<T>();

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    items = items.Include(include);
                }
            }
            return includeSoftDeleted ? items.IgnoreQueryFilters().Where(predicate) : items.Where(predicate);
        }
        public Task<List<T>> Findlist()
        {
            List<T> list = new List<T>();
            list = _entitySet.AsNoTracking().ToList();
            return Task.FromResult(list);
        }
        public bool Update(T item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
            _context.Entry(item).State = EntityState.Detached;
            return true;
        }

        public void AddRange(List<T> items)
        {
            _entitySet.AddRange(items);
            _context.SaveChanges();
        }

        public bool Delete(T item)
        {
            _entitySet.Attach(item);
            _entitySet.Remove(item);
            _context.SaveChanges();
            return true;

        }

        public bool DeleteById(K id)
        {
            var item = _entitySet.Find(id);
            _entitySet.Remove(item);
            _context.SaveChanges();
            return true;

        }


        public T GetById(K id)
        {
            var entity = _entitySet.Find(id);
            if (entity is null) return null;
            _context.Entry(entity).State = EntityState.Detached;
            return entity;
        }
        public T GetById2(Expression<Func<T, bool>> predicate, bool includeSoftDeleted = false, params Expression<Func<T, object>>[] includes)
        {
            var items = _entitySet.AsNoTracking().AsQueryable();

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    items = items.Include(include);
                }
            }
            return includeSoftDeleted ? items.AsNoTracking().IgnoreQueryFilters().FirstOrDefault(predicate) : items.AsNoTracking().FirstOrDefault(predicate);
        }

        public IQueryable<T> GetAll(bool includeSoftDeleted = false)
        {
            return includeSoftDeleted ? _entitySet.AsNoTracking().IgnoreQueryFilters() : _entitySet.AsNoTracking();
        }

        public IQueryable<T> GetByNameAndHMP(bool includeSoftDeleted = false)
        {
            return includeSoftDeleted ? _entitySet.AsNoTracking().IgnoreQueryFilters() : _entitySet.AsNoTracking();
        }

        public T UpdateTwoEntities(T item, K kitem, bool createItem = false)
        {
            using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                T Newitem = null;
                try
                {
                    if (createItem)
                    {
                        Newitem = _entitySet.Add(item).Entity;
                        _context.SaveChanges();
                    }
                    else
                    {
                        _context.Entry(item).State = EntityState.Modified;
                        _context.SaveChanges();
                        _context.Entry(item).State = EntityState.Detached;
                    }


                    _context.Entry(kitem).State = EntityState.Modified;
                    _context.SaveChanges();
                    _context.Entry(kitem).State = EntityState.Detached;
                    transaction.Commit();
                    Newitem = createItem == true ? Newitem : item;
                }
                catch
                {
                    transaction.Rollback();
                }
                return Newitem;
            }
        }

    }
}
