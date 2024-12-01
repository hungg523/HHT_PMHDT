using AutoMapper;
using NhaThuoc.Data.Repositories.Base;
using NhaThuoc.Domain.Abtractions.IRepositories;
using NhaThuoc.Domain.Entities;

namespace NhaThuoc.Data.Repositories
{
    public class ConversationRepository(ApplicationDbContext context, IMapper mapper) : GenericRepository<Conversation>(context, mapper), IConversationRepository
    {
    }
}