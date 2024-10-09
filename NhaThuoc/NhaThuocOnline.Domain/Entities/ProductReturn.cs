namespace NhaThuocOnline.Domain.Entities
{
    public class ProductReturn
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public string ReturnReason { get; set; }
        public DateTime DateReturn { get; set; }
        public string Status { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }


    }
}
