using BlobCommonAction.Models;
using BlobCommonAction.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlobCommonAction.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlobStorageController : ControllerBase
    {
        private readonly IBlobStorageService _blobStorageService;

        public BlobStorageController(IBlobStorageService blobStorageService)
        {
            _blobStorageService = blobStorageService;
        }

        [HttpGet("{blobContainer}/{blobName}")]
        public async Task<ActionResult> GetByName(string blobContainer, string blobName)           
        {
            return Ok(await _blobStorageService.GetBlobInfoAsync(blobContainer, blobName));
        }

        [HttpGet("{blobContainer}")]
        public async Task<ActionResult<IEnumerable<string>>> GetAll(string blobContainer)
        {
            return Ok(await _blobStorageService.ListBlobsAsync(blobContainer));
        }

        [HttpPost]
        [Route("/uploadblob")]
        public async Task<ActionResult> UploadFile([FromBody] BlobToUploadDto blobToUploadDto)
        {
            await _blobStorageService.UploadFileBlobAsync(blobToUploadDto.FilePath, blobToUploadDto.FileName);

            return Ok();
        }

        [HttpPost]
        [Route("/uploadcontent")]
        public async Task<ActionResult> UploadContent([FromBody] UploadContentRequestDto uploadContentRequestDto)
        {
            await _blobStorageService.UploadContentBlobAsync(uploadContentRequestDto.Content, uploadContentRequestDto.FileName);

            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(string blobName)
        {
            await _blobStorageService.DeletBlobAsync(blobName);

            return Ok();
        }
    }
}
