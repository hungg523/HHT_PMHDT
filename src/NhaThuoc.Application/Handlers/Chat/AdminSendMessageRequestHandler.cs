using MediatR;
using Microsoft.AspNetCore.Http;
using NhaThuoc.Application.Request.Chat;
using NhaThuoc.Domain.Abtractions.IRepositories;
using NhaThuoc.Domain.Entities;
using NhaThuoc.Share.DependencyInjection.Extensions;
using NhaThuoc.Share.Exceptions;

namespace NhaThuoc.Application.Handlers.Chat
{
    public class AdminSendMessageRequestHandler : IRequestHandler<AdminSendMessageRequest, ApiResponse>
    {
        private readonly IAdminMessageRepository adminMessageRepository;
        private readonly IConversationRepository conversationRepository;

        public AdminSendMessageRequestHandler(IAdminMessageRepository adminMessageRepository, IConversationRepository conversationRepository)
        {
            this.adminMessageRepository = adminMessageRepository;
            this.conversationRepository = conversationRepository;
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

                    await transaction.CommitAsync(cancellationToken);
                    return ApiResponse.Success();
                }
                catch (Exception e)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    return new ApiResponse
                    {
                        IsSuccess = false,
                        StatusCode = StatusCodes.Status500InternalServerError,
                        StageTrace = e.StackTrace
                    };
                }
            }
        }
    }
}