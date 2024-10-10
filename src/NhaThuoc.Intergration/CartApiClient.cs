using Newtonsoft.Json;
using NhaThuoc.Domain.ReQuest.Cart;
using System.Text;

namespace NhaThuoc.Intergration
{
    public class CartApiClient : ICartApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public CartApiClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<bool> CreateCartItem(CartCreateRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://localhost:7128/api/carts", httpContent);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return true;
            return false;
        }

        public async Task<List<CartItemVm>> GetByCartId(string cartId)
        {
            var client = _httpClientFactory.CreateClient();

            var response = await client.GetAsync($"https://localhost:7128/api/carts/{cartId}");
            var body = await response.Content.ReadAsStringAsync();
            var cartData = JsonConvert.DeserializeObject<List<CartItemVm>>(body);
            return cartData;
        }

        public async Task<string> GetCartIdRecently(int customerId)
        {
            var client = _httpClientFactory.CreateClient();

            var response = await client.GetAsync($"https://localhost:7128/api/carts/{customerId}/recently");
            var body = await response.Content.ReadAsStringAsync();
            return body;
        }
    }
}
