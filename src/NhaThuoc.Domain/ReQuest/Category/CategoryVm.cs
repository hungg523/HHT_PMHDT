namespace NhaThuoc.Domain.ReQuest.Category
{
    public class CategoryVm
    {
        public int Id { get; set; }
        public int ParentId { get; set; }

        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public string ImagePath { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
