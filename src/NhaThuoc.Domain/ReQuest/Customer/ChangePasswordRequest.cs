namespace NhaThuoc.Domain.ReQuest.Customer
{
    public class ChangePasswordRequest
    {
        public string Email { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
