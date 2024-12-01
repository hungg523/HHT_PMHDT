using MediatR;
using NhaThuoc.Share.Exceptions;
using System.Text.Json.Serialization;

namespace NhaThuoc.Application.Request.Chat
{
    public class UserSendMessageRequest : IRequest<ApiResponse>
    {
        [JsonIgnore]
        public int? ConversationId { get; set; }
        public string? Message { get; set; }
    }
}