using AutoMapper;
using NhaThuoc.Data.Repositories.Base;
using NhaThuoc.Domain.Abtractions.IRepositories;
using NhaThuoc.Domain.Entities;

namespace NhaThuoc.Data.Repositories
{
    public class AdminMessageRepository(ApplicationDbContext context, IMapper mapper) : GenericRepository<AdminMessage>(context, mapper), IAdminMessageRepository
    {
    }
}