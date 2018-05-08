using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZipFilesToJson.Common;

namespace DataManagementSystem.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ZipFileTreeController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TreeItem treeItem)
        {
            return Ok(treeItem);
        }
    }
}