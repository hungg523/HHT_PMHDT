using NhaThuoc.Domain.Abtractions.Common;
using System.Text.Json.Serialization;

namespace NhaThuoc.Domain.Entities
{
    public class UserMessage : BaseEntity
    {
        public int? Id { get; set; }
        public string? Message { get; set; }
        public int? ConversationId { get; set; }
        public DateTime? CreateDate { get; set; }

        //[JsonIgnore]
        //public Conversation? Conversations { get; set; }
    }
}