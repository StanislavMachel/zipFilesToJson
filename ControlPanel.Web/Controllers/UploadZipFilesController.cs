using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ControlPanel.Web.Controllers
{
    [Route("api/[controller]")]
    public class UploadZipFilesController : Controller
    {
        private readonly IZipFileToTreeService _zipFileToTreeService;

        public UploadZipFilesController(IZipFileToTreeService zipFileToTreeService)
        {
            _zipFileToTreeService = zipFileToTreeService;
        }

        [HttpPost(Name = "UploadZipFiles")]
        public IActionResult Post(IFormFile zipFile)
        {
            var root = _zipFileToTreeService.ToTreeItem(zipFile);

            return Ok(root);
        }
    }
}