using AutoMapper;
using MediatR;
using NhaThuoc.Application.Request.Chat;
using NhaThuoc.Domain.Abtractions.IRepositories;
using NhaThuoc.Domain.Entities;
using NhaThuoc.Share.DependencyInjection.Extensions;
using NhaThuoc.Share.Exceptions;

namespace NhaThuoc.Application.Handlers.Chat
{
    public class CreateConversationRequestHandler : IRequestHandler<CreateConversationRequest, ApiResponse>
    {
        private readonly IConversationRepository conversationRepository;
        private readonly ICustomerRepository customerRepository;
        private readonly IMapper mapper;
        private readonly IAdminMessageRepository adminMessageRepository;

        public CreateConversationRequestHandler(IConversationRepository conversationRepository, ICustomerRepository customerRepository, IMapper mapper, IAdminMessageRepository adminMessageRepository)
        {
            this.conversationRepository = conversationRepository;
            this.customerRepository = customerRepository;
            this.mapper = mapper;
            this.adminMessageRepository = adminMessageRepository;
        }

        public async Task<ApiResponse> Handle(CreateConversationRequest request, CancellationToken cancellationToken)
        {
            await using (var transaction = conversationRepository.BeginTransaction())
            {
                try
                {
                    var customer = await customerRepository.FindByIdAsync(request.CustomerId);
                    if (customer == null) customer.ThrowNotFound();

                    var conversation = await conversationRepository.FindSingleAsync(x => x.CustomerId == request.CustomerId);
                    if (conversation is not null) conversation.ThrowConflict();

                    conversation = new Conversation
                    {
                        CustomerId = request.CustomerId,
                        CreateDate = DateTime.Now,
                        LastMessageAt = DateTime.Now,
                    };
                    conversationRepository.Create(conversation);
                    await conversationRepository.SaveChangesAsync(cancellationToken);

                    var adminMessage = new AdminMessage
                    {
                        ConversationId = conversation.Id,
                        Message = "Xin chào! Tôi có thể giúp gì cho bạn.",
                        CreateDate = DateTime.Now,
                    };
                    adminMessageRepository.Create(adminMessage);
                    await adminMessageRepository.SaveChangesAsync(cancellationToken);

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