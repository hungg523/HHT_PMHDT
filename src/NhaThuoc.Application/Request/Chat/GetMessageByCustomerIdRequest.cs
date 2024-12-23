using MediatR;
using NhaThuoc.Application.DTOs;

namespace NhaThuoc.Application.Request.Chat
{
    public class GetMessageByCustomerIdRequest : IRequest<ConversationDTO>
    {
        public int? CustomerId { get; set; }
    }
}