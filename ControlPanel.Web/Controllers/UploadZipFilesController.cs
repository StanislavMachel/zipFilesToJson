using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ControlPanel.Web.Interfaces;
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
        private readonly IConfiguration _configuration;

        public UploadZipFilesController(IZipFileToTreeService zipFileToTreeService, IConfiguration configuration)
        {
            _zipFileToTreeService = zipFileToTreeService;
            _configuration = configuration;
        }

        [HttpPost(Name = "UploadZipFiles")]
        public async Task<IActionResult> Post(string username, string password, IFormFile zipFile)
        {
            var zipFileTreeStructure = _zipFileToTreeService.ToTreeItem(zipFile);
            
            HttpClient client = new HttpClient();

            var content = PrepareStringContent(zipFileTreeStructure);

            ProvideBasicAuthorization(client, username, password);

            HttpResponseMessage response = await client.PostAsync("https://localhost:44332/api/ZipFileTree", content);

            string message;
            if (response.IsSuccessStatusCode)
            {
                message = "Your files succesful submitted";
            }
            else if(response.StatusCode == HttpStatusCode.Forbidden)
            {
                message = "Please insert correct password and username";
            }
            else
            {
                message = "Something goes wrong";
            }

            TempData["Message"] = message;
            return RedirectToPage("/Index");
         //   return StatusCode(response.StatusCode);
        }

        private void ProvideBasicAuthorization(HttpClient client, string username, string password)
        {
            var passwordHash = BCryptPasswordHasher.HashPassword(password);

            var byteArray = new UTF8Encoding().GetBytes(string.Format($"{username}:{passwordHash}"));

            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
        }

        private StringContent PrepareStringContent(TreeItem root)
        {
            string jsonInString = JsonConvert.SerializeObject(root, Formatting.Indented,
                new EncryptingJsonConverter(new Aes128BitEcbMode(_configuration)));

            return new StringContent(jsonInString, Encoding.UTF8, "application/json");
        }
    }
}