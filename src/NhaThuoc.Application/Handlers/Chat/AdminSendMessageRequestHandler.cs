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
    public class AdminSendMessageRequestHandler : IRequestHandler<AdminSendMessageRequest, ApiResponse>
    {
        private readonly IAdminMessageRepository adminMessageRepository;
        private readonly IConversationRepository conversationRepository;
        private readonly IHubContext<ChatHub> hubContext;

        public AdminSendMessageRequestHandler(IAdminMessageRepository adminMessageRepository, IConversationRepository conversationRepository, IHubContext<ChatHub> hubContext)
        {
            this.adminMessageRepository = adminMessageRepository;
            this.conversationRepository = conversationRepository;
            this.hubContext = hubContext;
        }

        public async Task<ApiResponse> Handle(AdminSendMessageRequest request, CancellationToken cancellationToken)
        {
            await using (var transaction = adminMessageRepository.BeginTransaction())
            {
                try
                {
                    var conversation = await conversationRepository.FindByIdAsync(request.ConversationId);
                    if (conversation is null) conversation.ThrowNotFound();

                    var adminMessage = new AdminMessage
                    {
                        ConversationId = request.ConversationId,
                        Message = request.Message,
                        CreateDate = DateTime.Now,
                    };
                    adminMessageRepository.Create(adminMessage);
                    await adminMessageRepository.SaveChangesAsync(cancellationToken);

                    conversation.LastMessageAt = DateTime.Now;
                    conversationRepository.Update(conversation);
                    await conversationRepository.SaveChangesAsync(cancellationToken);
                    await hubContext.Clients.Group($"Conversation-{request.ConversationId}").SendAsync("ReceiveMessage", "Admin", request.Message);
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