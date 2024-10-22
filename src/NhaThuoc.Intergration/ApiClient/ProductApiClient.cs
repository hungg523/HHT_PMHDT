using Newtonsoft.Json;
using NhaThuoc.Domain.ReQuest.Common;
using NhaThuoc.Domain.ReQuest.Product;
using NhaThuoc.Intergration.IApiClient;
using System.Text;

namespace NhaThuoc.Intergration.ApiClient
{
    public class ProductApiClient : IProductApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ProductApiClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<bool> Create(ProductCreateRequest request)
        {
            var client = _httpClientFactory.CreateClient();

            var requestContent = new MultipartFormDataContent();

            // Thêm dữ liệu hình ảnh vào yêu cầu
            if (request.ImagePath != null)
            {
                byte[] data;
                using (var br = new BinaryReader(request.ImagePath.OpenReadStream()))
                {
                    data = br.ReadBytes((int)request.ImagePath.OpenReadStream().Length);
                }
                ByteArrayContent bytes = new ByteArrayContent(data);
                requestContent.Add(bytes, "ImagePath", request.ImagePath.FileName);
            }

            // Thêm dữ liệu JSON vào yêu cầu
            var json = JsonConvert.SerializeObject(request);
            var jsonContent = new StringContent(json, Encoding.UTF8, "application/json");
            requestContent.Add(jsonContent, "request");

            var response = await client.PostAsync("https://localhost:7128/api/products", requestContent);
            var result = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
                return true;

            return false;
        }

        public async Task<PagedResult<ProductVm>> GetAllProductsPaging(GetManageProductPagingRequest request)
        {
            var client = _httpClientFactory.CreateClient();

            var response = await client.GetAsync($"https://localhost:7128/api/products?CategoryId={request.CategoryId}&Keyword={request.Keyword}&OrderBy={request.OrderBy}" +
                $"&PageIndex={request.PageIndex}&PageSize={request.PageSize}");
            var body = await response.Content.ReadAsStringAsync();
            var products = JsonConvert.DeserializeObject<PagedResult<ProductVm>>(body);
            return products;
        }

        public async Task<ProductVm> GetProductById(int id)
        {
            var client = _httpClientFactory.CreateClient();

            var response = await client.GetAsync($"https://localhost:7128/api/products/{id}");
            var body = await response.Content.ReadAsStringAsync();
            var product = JsonConvert.DeserializeObject<ProductVm>(body);
            return product;
        }

        public async Task<PagedResult<ProductBasicVm>> GetProductByCategoryIdPaging(GetPublicProductPagingRequest request)
        {
            var client = _httpClientFactory.CreateClient();

            var response = await client.GetAsync($"https://localhost:7128/api/products/public?CategoryId={request.CategoryId}&Keyword={request.Keyword}&OrderBy={request.OrderBy}" +
                $"&PageIndex={request.PageIndex}&PageSize={request.PageSize}");
            var body = await response.Content.ReadAsStringAsync();
            var products = JsonConvert.DeserializeObject<PagedResult<ProductBasicVm>>(body);
            return products;
        }

        public Task<bool> SoftDelete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
