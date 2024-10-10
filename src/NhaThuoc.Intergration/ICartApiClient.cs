using NhaThuoc.Domain.ReQuest.Cart;

namespace NhaThuoc.Intergration
{
    public interface ICartApiClient
    {
        Task<bool> CreateCartItem(CartCreateRequest request);

        Task<List<CartItemVm>> GetByCartId(string cartId);
        Task<string> GetCartIdRecently(int customerId);
    }
}
