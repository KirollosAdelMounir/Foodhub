using System.Linq.Expressions;

namespace FoodHub.Repository
{
    public interface IRepositoryBase<T, K> where T : class
    {
        IEnumerable<T> List();
        public IQueryable<T> Find(Expression<Func<T, bool>> predicate, bool includeSoftDeleted = false, params Expression<Func<T, object>>[] includes);
        T GetById(K id);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        object List(Func<object, bool> value);
    }
}
