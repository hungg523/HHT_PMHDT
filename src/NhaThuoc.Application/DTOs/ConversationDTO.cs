namespace NhaThuoc.Application.DTOs
{
    public class ConversationDTO
    {
        public int? ConversationId { get; set; }
        public List<MessageDTO>? Messages { get; set; } = new List<MessageDTO>();
    }
}