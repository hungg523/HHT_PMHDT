using MediatR;
using NhaThuoc.Application.DTOs;
using NhaThuoc.Share.Exceptions;

namespace NhaThuoc.Application.Request.Product
{
    public class GetAllProductRequest : IRequest<PagedResponse<ProductDTO>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public GetAllProductRequest(int pageNumber = 1, int pageSize = 6)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}