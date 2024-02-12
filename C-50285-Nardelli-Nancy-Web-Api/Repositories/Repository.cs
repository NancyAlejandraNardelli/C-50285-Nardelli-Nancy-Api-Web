using C_50285_Nardelli_Nancy_Web_Api.DataAccess;
using C_50285_Nardelli_Nancy_Web_Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;

namespace C_50285_Nardelli_Nancy_Web_Api.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly AppDbContext _dbContext;
        internal DbSet<T> dbset;

        public Repository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            this.dbset = dbContext.Set<T>();
        }

        public async Task Create(T entity)
        {
            await dbset.AddAsync(entity);
            await Save();
        }
        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }
        public virtual async Task<T> GetById(int id)
        {
            return await dbset.FindAsync(id);
        }
        public async Task<List<T>> GetAll(Expression<Func<T, bool>> filter = null, string? includeProperties = null)
        {
            IQueryable<T> query = dbset;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null) // Modelo
            {
                foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
            }

            return await query.ToListAsync();
        }

        public async Task<T> Get(Expression<Func<T, bool>> filter = null, bool tracked = true, string? includeProperties = null)
        {
            IQueryable<T> query = dbset;
            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null) // Modelo
            {
                foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
            }

            return await query.FirstOrDefaultAsync();
        }

        public virtual async Task<bool> LogicalDelete(int id)
        {
            var entity = await dbset.FindAsync(id);

            if (entity == null) return false;

            PropertyInfo? propertyInfo = entity.GetType().GetProperty("EndDate");

            if (propertyInfo == null) return false;

            propertyInfo.SetValue(entity, DateTime.Now);

            dbset.Update(entity);

            return true;
        }

        public async Task Delete(T entity)
        {
            dbset.Remove(entity);
            await Save();
        }
    }
}
