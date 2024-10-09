using Microsoft.AspNetCore.Identity;

namespace NhaThuocOnline.Domain.Entities
{
    public class AppStaffAccount : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string NationId { get; set; }
        public bool isActive { get; set; }
        public DateTime CreatedAt { get; set; }


    }
}
