using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ZipFilesToJson.Common;

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
        public async Task<IActionResult> Post(IFormFile zipFile)
        {
            var root = _zipFileToTreeService.ToTreeItem(zipFile);
            

            HttpClient client = new HttpClient();

            var jsonInString = JsonConvert.SerializeObject(root);
            var content = new StringContent(jsonInString, Encoding.UTF8, "application/json");
            
            HttpResponseMessage response = await client.PostAsync("https://localhost:44332/api/ZipFileTree", content);




            return Ok(root);
        }
    }
}