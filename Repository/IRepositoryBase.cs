namespace FoodHub.Repository
{
    public interface IRepositoryBase<T, K> where T : class
    {
        IEnumerable<T> List();
        T GetById(K id);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
