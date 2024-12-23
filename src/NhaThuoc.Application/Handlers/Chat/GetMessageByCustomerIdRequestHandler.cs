using MediatR;
using NhaThuoc.Application.DTOs;
using NhaThuoc.Application.Request.Chat;
using NhaThuoc.Domain.Abtractions.IRepositories;
using NhaThuoc.Share.DependencyInjection.Extensions;

namespace NhaThuoc.Application.Handlers.Chat
{
    public class GetMessageByCustomerIdRequestHandler : IRequestHandler<GetMessageByCustomerIdRequest, ConversationDTO>
    {
        private readonly IConversationRepository conversationRepository;
        private readonly IUserMessageRepository userMessageRepository;
        private readonly IAdminMessageRepository adminMessageRepository;
        private readonly ICustomerRepository customerRepository;

        public GetMessageByCustomerIdRequestHandler(IConversationRepository conversationRepository, IUserMessageRepository userMessageRepository, IAdminMessageRepository adminMessageRepository, ICustomerRepository customerRepository)
        {
            this.conversationRepository = conversationRepository;
            this.userMessageRepository = userMessageRepository;
            this.adminMessageRepository = adminMessageRepository;
            this.customerRepository = customerRepository;
        }

        public async Task<ConversationDTO> Handle(GetMessageByCustomerIdRequest request, CancellationToken cancellationToken)
        {
            var conversation = await conversationRepository.FindSingleAsync(x => x.CustomerId == request.CustomerId);
            if (conversation is null) conversation.ThrowNotFound();

            var customer = await customerRepository.FindSingleAsync(x => x.Id == conversation.CustomerId);
            if (customer is null) customer.ThrowNotFound();

            var userMessage = userMessageRepository.FindAll(x => x.ConversationId == conversation.Id).ToList();
            var adminMessage = adminMessageRepository.FindAll(x => x.ConversationId == conversation.Id).ToList();
            var messages = userMessage.Select(message => new MessageDTO
            {
                From = $"{customer.FirstName} {customer.LastName}",
                Message = message.Message,
                TimeSend = message.CreateDate,
            }).Concat(adminMessage.Select(message => new MessageDTO
            {
                From = "Admin",
                Message = message.Message,
                TimeSend = message.CreateDate,
            })).OrderBy(x => x.TimeSend).ToList();

            var conversationMessages = new ConversationDTO
            {
                ConversationId = conversation.Id,
                Messages = messages,
            };

            return conversationMessages;
        }
    }
}