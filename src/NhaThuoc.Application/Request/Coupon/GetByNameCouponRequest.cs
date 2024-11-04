using MediatR;
using Entities = NhaThuoc.Domain.Entities;

namespace NhaThuoc.Application.Request.Coupon
{
    public class GetByNameCouponRequest : IRequest<Entities.Coupon>
    {
        public int? Id { get; set; }
    }
}
