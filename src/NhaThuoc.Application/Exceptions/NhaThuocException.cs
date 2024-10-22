namespace NhaThuoc.Domain.Exceptions
{
    public class NhaThuocException(bool IsSuccess, int StatusCode, List<string>? Errors = null)
    {
    }
}
