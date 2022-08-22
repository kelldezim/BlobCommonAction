using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using BlobCommonAction.Extensions;
using BlobCommonAction.Models;
using System.Text;

namespace BlobCommonAction.Services
{
    public class BlobStorageService : IBlobStorageService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly string _blobContainerName = "learningblob"; 
        // its hardcoded for dev/test purposes but BlobClient containerName could be injected 

        public BlobStorageService(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }
        public async Task<BlobInformationDto> GetBlobInfoAsync(string blobContainer, string blobName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(blobContainer);
            var blobClient = containerClient.GetBlobClient(blobName);
            var blobDownloadResult = await blobClient.DownloadContentAsync();

            var blobInformationDto = new BlobInformationDto(blobDownloadResult.Value.Content.ToString());

            return blobInformationDto;
        }

        public async Task<IEnumerable<string>> ListBlobsAsync(string blobContainer)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(blobContainer);
            var items = new List<string>();

            await foreach(var blob in containerClient.GetBlobsAsync())
            {
                items.Add(blob.Name);
            }

            return items;
        }

        public async Task UploadFileBlobAsync(string filePath, string fileName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(_blobContainerName);
            var blobClient = containerClient.GetBlobClient(fileName);
            await blobClient.UploadAsync(filePath, new BlobHttpHeaders {ContentType = filePath.GetContentType()});
        }

        public async Task UploadContentBlobAsync(string content, string fileName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(_blobContainerName);
            var blobClient = containerClient.GetBlobClient(fileName);
            var bytes = Encoding.UTF8.GetBytes(content);
            await using var memoryStream = new MemoryStream(bytes);
            await blobClient.UploadAsync(memoryStream, new BlobHttpHeaders { ContentType = fileName.GetContentType() });
        }

        public async Task DeletBlobAsync(string blobName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(_blobContainerName);
            var blobClient = containerClient.GetBlobClient(blobName);

            await blobClient.DeleteIfExistsAsync();
        }
    }
}
