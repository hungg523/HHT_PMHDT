using MediatR;
using NhaThuoc.Application.DTOs;

namespace NhaThuoc.Application.Request.Product
{
    public class GetAllProductsRequest : IRequest<List<ProductDTO>>
    {
    }
}