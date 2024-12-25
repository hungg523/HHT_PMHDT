using MediatR;
using NhaThuoc.Domain.Entities;

namespace NhaThuoc.Application.Request.Chat
{
    public class GetAllConversationRequest : IRequest<List<Conversation>>
    {
    }
}