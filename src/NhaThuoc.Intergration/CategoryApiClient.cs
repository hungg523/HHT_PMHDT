using Newtonsoft.Json;
using NhaThuoc.Domain.ReQuest.Category;
using System.Text;

namespace NhaThuocOnline.ApiIntergration
{
    public class CategoryApiClient: ICategoryApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public CategoryApiClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<bool> CreateCategory(CategoryCreateRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://localhost:7128/api/categories", httpContent);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return true;
            return false;
        }

        public async Task<List<CategoryVm>> GetAllCategories()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7128/api/categories");
            var body = await response.Content.ReadAsStringAsync();
            var coupons = JsonConvert.DeserializeObject<List<CategoryVm>>(body);
            return coupons;
        }
    }
}
