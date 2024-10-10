namespace NhaThuoc.Domain.ReQuest.Category
{
    public class CategoryCreateRequest
    {
        public int ParentId { get; set; }

        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }

        //public IFormFile ImagePath {get; set;}
        public string ImagePath { get; set; }
     
    }
}
