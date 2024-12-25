using Microsoft.AspNetCore.SignalR;

namespace NhaThuoc.Share.Service
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message, int conversationId)
        {
            await Clients.Group($"Conversation-{conversationId}").SendAsync("ReceiveMessage", user, message);
        }

        public async Task JoinConversation(int conversationId)
        {
            try
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, $"Conversation-{conversationId}");
                Console.WriteLine($"User {Context.ConnectionId} joined conversation {conversationId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error joining conversation {conversationId}: {ex.Message}");
            }
        }


        public async Task LeaveConversation(int conversationId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"Conversation-{conversationId}");
        }
    }
}