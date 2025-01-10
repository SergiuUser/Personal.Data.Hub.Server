using Microsoft.EntityFrameworkCore;
using Personal.Dara.Hub.Server.Data.Context;
using Personal.Dara.Hub.Server.Data.Interfaces;
using System.Linq.Expressions;

namespace Personal.Dara.Hub.Server.Data.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private ServerDbContext _context;
        private readonly DbSet<T> _dbSet;

        #region ctor
        public BaseRepository(ServerDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        #endregion

        #region CRUD operations
        public async Task Add(T entity){
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            await _dbSet.AddAsync(entity);
        }

        public void Delete(T entity)
        {
            if (entity == null) 
                throw new ArgumentNullException(nameof(entity));

            _dbSet.Remove(entity);
        }

        public async Task<IEnumerable<T>> GetAll() => await _dbSet.ToListAsync();

        public async Task<T?> GetByExpression(Expression<Func<T, bool>> expression) => await _dbSet.FirstOrDefaultAsync(expression);

        public async Task<bool> SaveAsync() => await _context.SaveChangesAsync() > 0;

        public void Update(T entity){
            var entry = _context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }

            entry.State = EntityState.Modified;
        }

        #endregion
    }
}
