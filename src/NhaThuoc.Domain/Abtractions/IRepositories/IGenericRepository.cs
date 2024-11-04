using Microsoft.EntityFrameworkCore.Storage;
using NhaThuoc.Domain.Abtractions.Common;

namespace NhaThuoc.Domain.Abtractions.IRepositories
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task<IReadOnlyList<T>> GetAll();
        Task<T?> FindByIdAsync(object id);
        IDbContextTransaction BeginTransaction();
    }
}