using BlobCommonAction.Models;

namespace BlobCommonAction.Services
{
    public interface IBlobStorageService
    {
        Task<BlobInformationDto> GetBlobInfoAsync(string blobContainer, string blobName);
        Task<IEnumerable<string>> ListBlobsAsync(string blobContainer);
        Task UploadFileBlobAsync(string filePath, string fileName);
        Task UploadContentBlobAsync(string content, string fileName);
        Task DeletBlobAsync(string blobName);
    }
}