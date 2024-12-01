using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using NhaThuoc.Application.Request.Chat;
using NhaThuoc.Domain.Abtractions.IRepositories;
using NhaThuoc.Domain.Entities;
using NhaThuoc.Share.DependencyInjection.Extensions;
using NhaThuoc.Share.Exceptions;

namespace NhaThuoc.Application.Handlers.Chat
{
    public class UserSendMessageRequestHandler : IRequestHandler<UserSendMessageRequest, ApiResponse>
    {
        private readonly IUserMessageRepository userMessageRepository;
        private readonly IConversationRepository conversationRepository;
        private readonly IMapper mapper;

        public UserSendMessageRequestHandler(IUserMessageRepository userMessageRepository, IConversationRepository conversationRepository, IMapper mapper)
        {
            this.userMessageRepository = userMessageRepository;
            this.conversationRepository = conversationRepository;
            this.mapper = mapper;
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