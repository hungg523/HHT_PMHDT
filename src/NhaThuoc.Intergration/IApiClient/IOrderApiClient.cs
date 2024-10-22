using NhaThuoc.Domain.ReQuest.Order;

namespace NhaThuoc.Intergration.IApiClient
{
    public interface IOrderApiClient
    {
        Task<List<OrderItemVm>> GetProductByCartId(string cartId);
        Task<bool> CreateOrder(OrderCreateRequest request);
        Task<bool> ChangeStatusOrder(ChangeStatusRequest request);

    }
}
