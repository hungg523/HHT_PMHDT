using NhaThuoc.Application.Request.Common;

namespace NhaThuoc.Domain.ReQuest.Product
{
    public class GetPublicProductPagingRequest: PagingRequestBase
    {
        public int? CategoryId { get; set; }
        public string? Keyword { get; set; }
        public string? OrderBy { get; set; }
    }
}
