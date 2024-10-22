namespace NhaThuoc.Domain.ReQuest.Product
{
    public class ProductVm
    {
        public int Id { get; set; }
        public string ProductName { get; set; } 
        public double RegularPrice { get; set; }
        public double DiscountPrice { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public string Brand { get; set; }
        public string Packaging { get; set; }
        public string Origin { get; set; }
        public string Manufacturer { get; set; }
        public string ProductionLocation { get; set; }
        public string Ingredients { get; set; }
        public string ImagePath { get; set; }
        public string SeoTitle { get; set; }
        public string SeoAlias { get; set; }
        public bool IsPrescriptionRequired { get; set; }
    }
}
