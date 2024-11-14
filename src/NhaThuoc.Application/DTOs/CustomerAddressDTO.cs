using System.Text.Json.Serialization;

namespace NhaThuoc.Application.DTOs
{
    public class CustomerAddressDTO
    {
        [JsonIgnore]
        public string Address { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }

        [JsonIgnore]
        public string Province { get; set; }

        [JsonIgnore]
        public string District { get; set; }

        [JsonIgnore]
        public string Ward { get; set; }
        public string FinalAddress => $"{Address}, {Ward}, {District}, {Province}";
    }
}