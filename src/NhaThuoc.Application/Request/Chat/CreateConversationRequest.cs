using MediatR;
using NhaThuoc.Share.Exceptions;

namespace NhaThuoc.Application.Request.Chat
{
    public class CreateConversationRequest : IRequest<ApiResponse>
    {
        public int? CustomerId { get; set; }
    }
}