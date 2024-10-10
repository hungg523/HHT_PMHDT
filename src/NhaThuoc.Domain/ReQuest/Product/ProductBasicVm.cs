namespace NhaThuoc.Domain.ReQuest.Product
{
    public class ProductBasicVm
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ImagePath { get; set; }
        public double RegularPrice { get; set; }
        public double DiscountPrice { get; set; }
        public bool isPrescriptionRequired { get; set; }
    }
}
