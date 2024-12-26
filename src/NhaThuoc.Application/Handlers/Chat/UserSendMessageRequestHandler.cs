using MediatR;
using Microsoft.AspNetCore.SignalR;
using NhaThuoc.Application.Request.Chat;
using NhaThuoc.Domain.Abtractions.IRepositories;
using NhaThuoc.Domain.Entities;
using NhaThuoc.Share.DependencyInjection.Extensions;
using NhaThuoc.Share.Exceptions;
using NhaThuoc.Share.Service;

namespace NhaThuoc.Application.Handlers.Chat
{
    public class UserSendMessageRequestHandler : IRequestHandler<UserSendMessageRequest, ApiResponse>
    {
        private readonly IUserMessageRepository userMessageRepository;
        private readonly IConversationRepository conversationRepository;
        private readonly IHubContext<ChatHub> hubContext;
        private readonly ICustomerRepository customerRepository;

        public UserSendMessageRequestHandler(IUserMessageRepository userMessageRepository, IConversationRepository conversationRepository, IHubContext<ChatHub> hubContext, ICustomerRepository customerRepository)
        {
            this.userMessageRepository = userMessageRepository;
            this.conversationRepository = conversationRepository;
            this.hubContext = hubContext;
            this.customerRepository = customerRepository;
        }

        public async Task<ApiResponse> Handle(UserSendMessageRequest request, CancellationToken cancellationToken)
        {
            await using (var transaction = userMessageRepository.BeginTransaction())
            {
                try
                {
                    var conversation = await conversationRepository.FindByIdAsync(request.ConversationId);
                    if (conversation is null) conversation.ThrowNotFound();

                    var customer = await customerRepository.FindSingleAsync(x => x.Id == conversation.CustomerId);

                    var userMessage = new UserMessage
                    {
                        ConversationId = request.ConversationId,
                        Message = request.Message,
                        CreateDate = DateTime.Now,
                    };
                    userMessageRepository.Create(userMessage);
                    await userMessageRepository.SaveChangesAsync(cancellationToken);

                    conversation.LastMessageAt = DateTime.Now;
                    conversationRepository.Update(conversation);
                    await conversationRepository.SaveChangesAsync(cancellationToken);

                    await hubContext.Clients.Group($"Conversation-{request.ConversationId}").SendAsync("ReceiveMessage", $"{customer.FirstName} {customer.LastName}", request.Message);
                    await transaction.CommitAsync(cancellationToken);
                    return ApiResponse.Success();
                }
                catch (Exception e)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    throw;
                }
            }
        }
    }
}