using System.Linq.Expressions;

namespace Personal.Dara.Hub.Server.Data.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T?> GetByExpression(Expression<Func<T, bool>> expression);
        Task Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task SaveAsync();
    }
}
