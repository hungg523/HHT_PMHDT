using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
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

        public UserSendMessageRequestHandler(IUserMessageRepository userMessageRepository, IConversationRepository conversationRepository, IHubContext<ChatHub> hubContext)
        {
            this.userMessageRepository = userMessageRepository;
            this.conversationRepository = conversationRepository;
            this.hubContext = hubContext;
        }

        public async Task<ApiResponse> Handle(UserSendMessageRequest request, CancellationToken cancellationToken)
        {
            await using (var transaction = userMessageRepository.BeginTransaction())
            {
                try
                {
                    var conversation = await conversationRepository.FindByIdAsync(request.ConversationId);
                    if (conversation is null) conversation.ThrowNotFound();

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

                    await hubContext.Clients.Group($"Conversation-{request.ConversationId}").SendAsync("ReceiveMessage", "User", request.Message);
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