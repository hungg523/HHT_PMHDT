namespace NhaThuoc.Domain.ReQuest.Customer
{
    public class CustomerUpdateRequest
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string AvatarImagePath { get; set; }
    }
}
