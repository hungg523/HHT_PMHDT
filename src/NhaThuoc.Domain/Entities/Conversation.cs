using NhaThuoc.Domain.Abtractions.Common;
using System.Text.Json.Serialization;

namespace NhaThuoc.Domain.Entities
{
    public class Conversation : BaseEntity
    {
        public int? Id { get; set; }
        public int? CustomerId { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? LastMessageAt { get; set; }
        
        //[JsonIgnore]
        //public ICollection<UserMessage>? UserMessages { get; set; }

        //[JsonIgnore]
        //public ICollection<AdminMessage>? AdminMessages { get; set; }
    }
}