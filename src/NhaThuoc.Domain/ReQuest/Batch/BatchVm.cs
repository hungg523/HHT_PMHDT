namespace NhaThuoc.Domain.ReQuest.Batch
{
    public class BatchVm
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public double OriginPrice { get; set; }
        public int Quantity { get; set; }
        public int Stock { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime ManufacturingDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
