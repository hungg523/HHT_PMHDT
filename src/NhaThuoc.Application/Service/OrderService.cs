using Microsoft.EntityFrameworkCore;
using NhaThuoc.Application.Interface;
using NhaThuoc.Domain.Data;
using NhaThuoc.Domain.Entities;
using NhaThuoc.Domain.ReQuest.Order;

namespace NhaThuoc.Application.Service
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _dbContext;

        public OrderService(ApplicationDbContext dbContext) { 
            _dbContext = dbContext; 
        }

        public async Task<bool> CreateOrder(OrderCreateRequest request)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    // tạo đơn hàng mới
                    var newOrder = new Order()
                    {
                        CouponId = -1,
                        CustomerId = request.CustomerId ?? -1,
                    };
                    _dbContext.Orders.Add(newOrder);
                    // lấy id đơn hàng mới
                    var newOrderData = _dbContext.Orders.OrderByDescending(x=>x.Id).FirstOrDefault();

                    // thêm danh sách sản phẩm đã chọn
                    var getProductItems = request.OrderItems;
                    _dbContext.OrderItems.AddRange(getProductItems);


                    // thêm thông tin trạng thái đơn hàng
                    var orderStatus = new OrderDetail() { 
                        OrderId = newOrderData.Id,
                        Status = OrderStatus.Pending, //chờ xác nhận
                        UpdatedAt = DateTime.Now,
                    };
                    _dbContext.OrderDetails.Add(orderStatus);

                    //xóa giỏ hàng hiện tại

                    var cart = await _dbContext.Carts.OrderByDescending(c => c.CreatedAt).FirstOrDefaultAsync();

                    var cartItem = await _dbContext.CartItems.Where(x=>x.CartId == cart.CartId).ToListAsync();
                    _dbContext.CartItems.RemoveRange(cartItem);
                    _dbContext.Carts.Remove(cart);


                    await _dbContext.SaveChangesAsync();
                    transaction.Commit();
                    return true;
                }
                catch (DbUpdateException ex)
                {
                    //ghi log
                    // --
                    transaction.Rollback();
                    return false;
                }
                catch (Exception ex)
                {
                    //ghi log
                    // --
                    transaction.Rollback();
                    return false;
                }

            }
        }

        public async Task<List<OrderItemVm>> GetProductByCartId(string cartId)
        { 
            var productInCart = await (from ci in _dbContext.CartItems
                               join p in _dbContext.Products on ci.ProductId equals p.Id
                               select new { ci, p }).Where(x => x.ci.CartId == cartId).ToListAsync();



            var orderItems = productInCart.Select(x=> new OrderItemVm
            {
                ProductName = x.p.ProductName,
                ProductImagePath = x.p.ImagePath,
                Price = x.p.DiscountPrice,
                Quantity = x.ci.Quantity,
                TotalPrice = x.p.DiscountPrice * x.ci.Quantity,
            }).ToList();
            return orderItems;
        }


        public async Task<bool> ChangeStatusOrder(ChangeStatusRequest request)
        {
            var getOrderData = await _dbContext.OrderDetails.FirstOrDefaultAsync(x => x.Id == request.OrderDetailsId);
            if (getOrderData != null)
            {
                getOrderData.Status = request.Status;
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public Task<OrderItemCustomerAddress> GetOrderById(int orderId)
        {
            var query = (from oi in _dbContext.OrderItems
                         join o in _dbContext.Orders on oi.OrderId equals o.Id
                         join c in _dbContext.Customers on o.CustomerId equals c.Id
                         join ca in _dbContext.CustomerAddresses on c.Id equals ca.CustomerId
                         select new { oi, o, c, ca }).AsQueryable();

            throw new NotImplementedException();
        }

        public Task<List<OrderDetailVm>> GetOrderRecently()
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteOrder()
        {
            throw new NotImplementedException();
        }

        public Task GetOrderByCustomerId(int customerId)
        {
            throw new NotImplementedException();
        }

       

        public Task<bool> UpdateOrder()
        {
            throw new NotImplementedException();
        }

   
    }
}
