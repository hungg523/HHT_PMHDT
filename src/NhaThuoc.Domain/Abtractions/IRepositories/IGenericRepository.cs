using Microsoft.EntityFrameworkCore.Storage;
using NhaThuoc.Domain.Abtractions.Common;
using System.Linq.Expressions;

namespace NhaThuoc.Domain.Abtractions.IRepositories
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        void RemoveMultiple(IEnumerable<T> entities);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        IQueryable<T> FindAll(Expression<Func<T, bool>>? predicate = null, params Expression<Func<T, object>>[] includeProperties);
        Task<T?> FindByIdAsync(object id);
        IDbContextTransaction BeginTransaction();
    }
}