using Application.IRepository.IFileRepository;
using Microsoft.AspNetCore.Mvc;

namespace FileUploadDownloadAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : Controller
    {
        private readonly IFileRepository _fileRepository;

        public FilesController(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file, string filePath)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Le fichier est vide ou null.");
            }

            using (MemoryStream memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                byte[] fileContent = memoryStream.ToArray();

                await _fileRepository.UploadFile(file.FileName, fileContent);

                return Ok("Le fichier a été téléchargé avec succès en base de données.");
            }
        }

        [HttpGet("{fileName}/info")]
        public async Task<IActionResult> GetFileInfo(string fileName)
        {
            var file = await _fileRepository.GetFileByNameAsync(fileName);

            if (file == null)
            {
                return NotFound("File not found!");
            }

            return Ok(new { fileName = file.FileName, fileSize = file.FileSize });
        }

        [HttpGet("{fileName}/download")]
        public async Task<IActionResult> DownloadFile(string fileName, [FromQuery] long startByte = 0)
        {
            var file = await _fileRepository.GetFileByNameAsync(fileName);

            if (file == null)
            {
                return NotFound();
            }

            if (startByte >= file.FileSize)
            {
                return BadRequest("Invalid startByte provided.");
            }

            byte[] fileContent = file.Contents.Skip((int)startByte).ToArray();

            string contentType = "application/octet-stream";

            return File(fileContent, contentType, fileName);
        }
    }
}
