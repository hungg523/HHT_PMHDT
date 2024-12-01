using AutoMapper;
using MediatR;
using NhaThuoc.Application.Request.Chat;
using NhaThuoc.Domain.Abtractions.IRepositories;
using NhaThuoc.Domain.Entities;

namespace NhaThuoc.Application.Handlers.Chat
{
    public class GetAllConversationRequestHandler : IRequestHandler<GetAllConversationRequest, List<Conversation>>
    {
        private readonly IConversationRepository conversationRepository;
        private readonly IMapper mapper;

        public GetAllConversationRequestHandler(IConversationRepository conversationRepository, IMapper mapper)
        {
            this.conversationRepository = conversationRepository;
            this.mapper = mapper;
        }

        public async Task<List<Conversation>> Handle(GetAllConversationRequest request, CancellationToken cancellationToken)
        {
            var coversation = conversationRepository.FindAll();
            return mapper.Map<List<Conversation>>(coversation);
        }
    }
}