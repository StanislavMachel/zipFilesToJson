using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DataManagementSystem.Web.Data;
using DataManagementSystem.Web.Entities;
using DataManagementSystem.Web.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ZipFilesToJson.Common;

namespace DataManagementSystem.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ZipFileTreeController : Controller
    {
        private readonly IStoreZipFileStructureService _zipFileStructureService;
        private readonly IConfiguration _configuration;
        private readonly DataManagmentSystemContext _context;

        public ZipFileTreeController(IStoreZipFileStructureService zipFileStructureService, IConfiguration configuration)
        {
            _zipFileStructureService = zipFileStructureService;
            _configuration = configuration;
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TreeItem treeItem, [FromHeader]string authorization)
        {

            if (!IsAutificated(authorization))
            {
                return StatusCode(HttpStatusCode.Forbidden);
            }

            _zipFileStructureService.StoreToDataBase(treeItem);
          
            return Ok();
        }

        private bool IsAutificated(string authorization)
        {
            Encoding encoding = Encoding.GetEncoding("iso-8859-1");
            string encodedUsernamePassword = authorization.Substring("Basic ".Length).Trim();
            string usernamePassword = encoding.GetString(Convert.FromBase64String(encodedUsernamePassword));


            var splitedUsernamePassword = usernamePassword.Split(":");
            var username = splitedUsernamePassword[0];
            var passwordHash = splitedUsernamePassword[1];

            var credentials = _configuration.GetSection("Credentials");


            var usernameFromConfig = credentials["Username"];
            var passwordFromConfig = credentials["Password"];


            bool passwordIsValid = BCryptPasswordHasher.VerifyPassword(passwordFromConfig, passwordHash);

            bool isAutificated = username == usernameFromConfig && passwordIsValid;
            return isAutificated;
        }
    }
}