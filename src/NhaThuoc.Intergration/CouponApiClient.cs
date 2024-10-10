using Newtonsoft.Json;
using NhaThuoc.Domain.ReQuest.Coupon;
using System.Text;

namespace NhaThuocOnline.ApiIntergration
{
    public class CouponApiClient : ICouponApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public CouponApiClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<bool> CreateCouponCode(CouponCreateRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://localhost:7128/api/coupons", httpContent);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return true;
            return false;
        }

        public async Task<List<CouponVm>> GetAllCouponList()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7128/api/coupons");
            var body= await response.Content.ReadAsStringAsync();
            var coupons= JsonConvert.DeserializeObject<List<CouponVm>>(body);
            return coupons;
        }
    }
}
