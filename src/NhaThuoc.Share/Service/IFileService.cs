using AssetServer.Enumerations;

namespace NhaThuoc.Share.Service
{
    public interface IFileService
    {
        string GetFileExtensionFromBase64(string base64String);
        Task<string> UploadFile(string fileName, string base64String, AssetType type);
    }
}