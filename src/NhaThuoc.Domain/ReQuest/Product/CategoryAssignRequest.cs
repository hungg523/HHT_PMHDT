using NhaThuoc.Domain.ReQuest.Common;

namespace NhaThuoc.Domain.ReQuest.Product
{
    public class CategoryAssignRequest
    {
        public int Id { get; set; }
        public List<SelectItem> Categories { get; set; } = new List<SelectItem>();
    }
}
