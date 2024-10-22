using Newtonsoft.Json;
using NhaThuoc.Application.Request.Category;
using NhaThuoc.Domain.Entities;
using NhaThuoc.Intergration.IApiClient;
using System.Text;

namespace NhaThuoc.Intergration.ApiClient
{
    public class CategoryApiClient : ICategoryApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public CategoryApiClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<bool> CreateCategory(CreateCategoryRequest request)
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

        public async Task<List<Category>> GetAllCategories()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7128/api/categories");
            var body = await response.Content.ReadAsStringAsync();
            var coupons = JsonConvert.DeserializeObject<List<Category>>(body);
            return coupons;
        }
    }
}
