namespace NhaThuoc.Domain.ReQuest.Category
{
    public class CategoryCreateRequest
    {
        public int ParentId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
