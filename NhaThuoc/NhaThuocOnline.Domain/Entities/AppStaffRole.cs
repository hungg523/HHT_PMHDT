using Microsoft.AspNetCore.Identity;

namespace NhaThuocOnline.Domain.Entities
{
    public class AppStaffRole : IdentityRole<int>
    {
        public string Description { get; set; }
    }
}
