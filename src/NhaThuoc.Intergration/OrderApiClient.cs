using Newtonsoft.Json;
using NhaThuoc.Domain.ReQuest.Order;
using System.Text;

namespace NhaThuoc.Intergration
{
    public class OrderApiClient : IOrderApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public OrderApiClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<bool> ChangeStatusOrder(ChangeStatusRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PatchAsync("https://localhost:7128/api/orders/status", httpContent);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return true;
            return false;
        }

        public async Task<bool> CreateOrder(OrderCreateRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://localhost:7128/api/orders", httpContent);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return true;
            return false;
        }

        public async Task<List<OrderItemVm>> GetProductByCartId(string cartId)
        {
            var client = _httpClientFactory.CreateClient();

            var response = await client.GetAsync($"https://localhost:7128/api/orders/checkout?cartId={cartId}");
            var body = await response.Content.ReadAsStringAsync();
            var items = JsonConvert.DeserializeObject<List<OrderItemVm>>(body);
            return items;
        }

    }
}
