using MediatR;
using Entities = NhaThuoc.Domain.Entities;

namespace NhaThuoc.Application.Request.Coupon
{
    public class GetAllCouponRequest : IRequest<List<Entities.Coupon>>
    {
    }
}
