using AssetServer.Enumerations;

namespace NhaThuoc.Share.Service
{
    public interface IFileService
    {
        Task<string> UploadFile(string fileName, string base64String, AssetType type);
    }
}