using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using NhaThuoc.Domain.Abtractions.Common;
using NhaThuoc.Domain.Abtractions.IRepositories;
using System.Linq.Expressions;
using System.Linq;
using System.Threading;

namespace NhaThuoc.Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public GenericRepository(ApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public void Delete(T entity)
        {
            dbContext.Set<T>().Remove(entity);
        }

        public void Update(T entity)
        {
            dbContext.Entry(entity).State = EntityState.Modified;
        }

        public void Create(T entity)
        {
            dbContext.Add(entity);
        }

        public IQueryable<T> FindAll(Expression<Func<T, bool>>? predicate = null, params Expression<Func<T, object>>[] includeProperties)
        {
            var query = dbContext.Set<T>().AsQueryable();
            if (includeProperties.Any())
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }
            return predicate is not null ? query.Where(predicate) : query;
        }

        public async Task<T?> FindByIdAsync(object id)
        {
            var unit = await dbContext.Set<T>().FindAsync(id);
            if (unit is null)
            {
                return null;
            }

            return mapper.Map<T>(unit);
        }

        public async Task<T?> FindSingleAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            var query = dbContext.Set<T>().AsQueryable();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            var result = predicate is not null ? await query.FirstOrDefaultAsync(predicate) : await query.FirstOrDefaultAsync();
            return result;
        }

        public void RemoveMultiple(IEnumerable<T> entities)
        {
            dbContext.Set<T>().RemoveRange(entities);
        }
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => dbContext.SaveChangesAsync();

        public IDbContextTransaction BeginTransaction()
        {
            return dbContext.Database.BeginTransaction();
        }
    }
}