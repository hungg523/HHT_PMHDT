namespace NhaThuoc.Domain.ReQuest.Batch
{
    public class BatchCreateRequest
    {
        public int ProductId { get; set; }
        public double OriginPrice { get; set; }
        public int Quantity { get; set; }

        public int Stock { get; set; }
        public DateTime ManufacturingDate { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
