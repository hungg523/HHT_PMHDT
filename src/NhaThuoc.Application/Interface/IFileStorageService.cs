namespace NhaThuoc.Application.Interface
{
    public interface IFileStorageService
    {
        string GetFileUrl(string fileName);
        Task SaveFileAsync(Stream mediaBinaryStream, string fileName);
        Task DeleteFileAsync(string fileName);
    }
}
