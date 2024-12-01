using AutoMapper;
using NhaThuoc.Data.Repositories.Base;
using NhaThuoc.Domain.Abtractions.IRepositories;
using NhaThuoc.Domain.Entities;

namespace NhaThuoc.Data.Repositories
{
    public class UserMessageRepository(ApplicationDbContext context, IMapper mapper) : GenericRepository<UserMessage>(context, mapper), IUserMessageRepository
    {
    }
}