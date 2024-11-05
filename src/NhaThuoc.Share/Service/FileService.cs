using AssetServer.Enumerations;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using NhaThuoc.Share.Constant;
using NhaThuoc.Share.Exceptions;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace NhaThuoc.Share.Service
{
    public class FileService : IFileService
    {
        private readonly HttpClient httpClient;
        private readonly string assetServerUrl;

        /// <summary>
        /// Constructor of <see cref="FileService"/>
        /// </summary>
        /// <param name="configuration">Configuration to get url address of asset server</param>
        public FileService(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            assetServerUrl = configuration[Const.ASSET_SERVER_ADDRESS];

            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;

            httpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri(assetServerUrl)
            };
        }

        /// <summary>
        /// Use HTTP client to upload file
        /// </summary>
        /// <param name="fileName">Name of file</param>
        /// <param name="base64String">File content</param>
        /// <param name="type">Asset type</param>
        /// <returns>File path</returns>
        public async Task<string> UploadFile(string fileName, string base64String, AssetType type)
        {
            try
            {
                var requestContent = new
                {
                    FileName = fileName,
                    Content = base64String,
                    AssetType = (int)type
                };

                // Convert payload sang JSON
                var jsonContent = new StringContent(JsonSerializer.Serialize(requestContent), Encoding.UTF8, "application/json");

                // Gửi yêu cầu POST
                HttpResponseMessage response = await httpClient.PostAsync($"{assetServerUrl}/upload", jsonContent);

                // Kiểm tra nếu yêu cầu thất bại
                if (!response.IsSuccessStatusCode)
                {
                    throw new NhaThuocException(StatusCodes.Status400BadRequest, new List<string> {"Validate Fail"});
                }

                // Đọc kết quả
                var result = await response.Content.ReadFromJsonAsync<ApiResponse>();

                return result.Data;
            }
            catch (HttpRequestException e)
            {
                throw new NhaThuocException(StatusCodes.Status500InternalServerError);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}