namespace NhaThuoc.Domain.Exceptions
{
    public class NhaThuocException : Exception
    {
        public bool IsSuccess { get; private set; }
        public int StatusCode { get; private set; }
        public List<string>? Errors { get; private set; }

        public NhaThuocException(int statusCode, List<string>? errors = null)
            : base(errors != null && errors.Any() ? string.Join("; ", errors) : "An error occurred")
        {
            IsSuccess = false;
            StatusCode = statusCode;
            Errors = errors;
        }

        public NhaThuocException(string message) : base(message)
        {
            IsSuccess = false;
            StatusCode = 500;
            Errors = new List<string> { message };
        }
    }
}
