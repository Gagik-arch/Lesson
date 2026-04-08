namespace Lesson.Repositories.core
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T?> GetById(string id);
        Task<IEnumerable<T>> GetAll();
        Task Add(T entity);
        void Update(T entity);
        void Remove(T entity);
    }
}
