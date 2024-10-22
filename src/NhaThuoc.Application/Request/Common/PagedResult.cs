namespace NhaThuoc.Application.Request.Common
{
    public class PagedResult<T>: PagedResultBase
    {
        public List<T> Items { get; set; }
    }
}
