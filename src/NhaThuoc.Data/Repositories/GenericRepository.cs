using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using NhaThuoc.Domain.Abtractions.Common;
using NhaThuoc.Domain.Abtractions.IRepositories;

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

        public async Task<IReadOnlyList<T>> GetAll()
        {
            var units = await dbContext.Set<T>().AsNoTracking().ToListAsync();
            return mapper.Map<List<T>>(units);
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

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => dbContext.SaveChangesAsync();

        public IDbContextTransaction BeginTransaction()
        {
            return dbContext.Database.BeginTransaction();
        }
    }
}