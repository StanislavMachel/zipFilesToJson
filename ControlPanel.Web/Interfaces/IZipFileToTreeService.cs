using Microsoft.AspNetCore.Http;
using ZipFilesToJson.Common;

namespace ControlPanel.Web.Interfaces
{
    public interface IZipFileToTreeService
    {
        TreeItem ToTreeItem(IFormFile formFile);
    }
}
