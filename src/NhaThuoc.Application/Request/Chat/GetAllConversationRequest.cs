using MediatR;
using NhaThuoc.Domain.Entities;
using NhaThuoc.Share.Exceptions;
using System.Text.Json.Serialization;

namespace NhaThuoc.Application.Request.Chat
{
    public class GetAllConversationRequest : IRequest<List<Conversation>>
    {
    }
}