using MediatR;
using NhaThuoc.Application.DTOs;
using System.Text.Json.Serialization;

namespace NhaThuoc.Application.Request.Chat
{
    public class GetMessagaByConversationIdRequest : IRequest<ConversationDTO>
    {
        [JsonIgnore]
        public int? ConversationId { get; set; }
    }
}