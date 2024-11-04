using NhaThuoc.Domain.ReQuest.Order;

namespace NhaThuoc.Application.Interface
{
    public interface IOrderService
    {
        Task<List<OrderItemVm>> GetProductByCartId(string cartId);

        Task<bool> CreateOrder(OrderCreateRequest request);
        Task<bool> ChangeStatusOrder(ChangeStatusRequest request);
        Task<OrderItemCustomerAddress> GetOrderById(int orderId);

        //
        Task<List<OrderDetailVm>> GetOrderRecently();

        Task GetOrderByCustomerId(int customerId);
        Task<bool> UpdateOrder();
        Task<bool> DeleteOrder();

    }
}
