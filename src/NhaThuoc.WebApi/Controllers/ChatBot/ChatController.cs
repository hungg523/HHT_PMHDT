using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NhaThuoc.Application.Request.Chat;

namespace NhaThuoc.WebApi.Controllers.ChatBot
{
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public ChatController(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        [HttpPost("/create-conversation")]
        public async Task<IActionResult> CreateChat(int? customerId)
        {
            var command = new CreateConversationRequest();
            command.CustomerId = customerId;
            var result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("/user-send-message")]
        public async Task<IActionResult> UserChat(int? conversionId, [FromBody] UserSendMessageRequest request)
        {
            var command = mapper.Map<UserSendMessageRequest>(request);
            command.ConversationId = conversionId;
            var result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("/admin-send-message")]
        public async Task<IActionResult> AdminChat(int? conversionId, [FromBody] AdminSendMessageRequest request)
        {
            var command = mapper.Map<AdminSendMessageRequest>(request);
            command.ConversationId = conversionId;
            var result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("/get-all-chat")]
        public async Task<IActionResult> GetAllChat()
        {
            var command = new GetAllConversationRequest();
            var result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("/get-detail-message")]
        public async Task<IActionResult> GetDetailChat(int? conversionId)
        {
            var command = new GetMessagaByConversationIdRequest();
            command.ConversationId = conversionId;
            var result = await mediator.Send(command);
            return Ok(result);
        }
    }
}