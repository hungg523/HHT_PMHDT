namespace NhaThuoc.Domain.ReQuest.Common
{
    public class PagedResult<T>: PagedResultBase
    {
        public List<T> Items { get; set; }
    }
}
