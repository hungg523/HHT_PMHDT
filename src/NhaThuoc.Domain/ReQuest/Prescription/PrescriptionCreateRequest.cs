using Microsoft.AspNetCore.Http;

namespace NhaThuoc.Domain.ReQuest.Prescription
{
    public class PrescriptionCreateRequest
    {
        public IFormFile ImagePath { get; set; }
        public string CustomerNote { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
    }
}
