namespace NhaThuoc.Domain.Exceptions
{
    public class NhaThuocException : Exception
    {
        public NhaThuocException()
        {
        }

        public NhaThuocException(string message)
            : base(message)
        {
        }

        public NhaThuocException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
