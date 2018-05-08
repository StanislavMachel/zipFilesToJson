using Microsoft.AspNetCore.Http;
using ZipFilesToJson.Common;

namespace ControlPanel.Web
{
    public interface IZipFileToTreeService
    {
        TreeItem ToTreeItem(IFormFile formFile);
    }
}
