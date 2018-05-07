using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControlPanel.Web.Entities;
using Microsoft.AspNetCore.Http;

namespace ControlPanel.Web
{
    public interface IZipFileToTreeService
    {
        TreeItem ToTreeItem(IFormFile formFile);
    }
}
