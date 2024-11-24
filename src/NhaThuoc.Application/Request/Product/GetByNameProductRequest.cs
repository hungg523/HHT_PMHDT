using MediatR;
using NhaThuoc.Application.DTOs;
using NhaThuoc.Share.Exceptions;
using Entities = NhaThuoc.Domain.Entities;

namespace NhaThuoc.Application.Request.Product
{
    public class GetByNameProductRequest : IRequest<PagedResponse<ProductDTO>>
    {
        public string? ProductName { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public GetByNameProductRequest(int pageNumber = 1, int pageSize = 6)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
