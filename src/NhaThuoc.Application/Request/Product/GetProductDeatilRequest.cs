using MediatR;
using NhaThuoc.Application.DTOs;

namespace NhaThuoc.Application.Request.Product
{
    public class GetProductDeatilRequest : IRequest<ProductDTO>
    {
        public int? Id { get; set; }
    }
}