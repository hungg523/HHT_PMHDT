namespace NhaThuocOnline.Domain.Entities
{
    public class Cart
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string CartId { get; set; }

        public DateTime CreatedAt { get; set; }


    }
}
