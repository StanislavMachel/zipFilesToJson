using System.IO.Compression;
using System.Linq;
using ControlPanel.Web.Interfaces;
using Microsoft.AspNetCore.Http;
using ZipFilesToJson.Common;

namespace ControlPanel.Web.Services
{
    public class ZipFileToTreeService : IZipFileToTreeService
    {
        public TreeItem ToTreeItem(IFormFile formFile)
        {

            TreeItem root;

            using (var stream = formFile.OpenReadStream())
            {
                using (ZipArchive archive = new ZipArchive(stream))
                {
                    root = new TreeItem("", formFile.FileName, "archive");

                    var entries = archive.Entries
                        .Select(x => x.FullName.Substring(0, x.FullName.Length - x.Name.Length))
                        .Distinct()
                        .Where(entry => entry != string.Empty)
                        .ToList();

                    var files = archive.Entries.Where(x => !x.FullName.EndsWith("/")).Select(x => x.FullName).ToList();

                    var allEntries = entries.Concat(files).OrderBy(x => x).ThenBy(x => x.Length)
                        .ThenBy(x => x.EndsWith("/")).ToList();


                    TreeItem tempTreeItem = root;
                    foreach (var entry in allEntries)
                    {
                        bool isFolder = entry.EndsWith("/");
                        var spliedEntries = entry.Split("/").Where(x => x != string.Empty).ToArray();
                        var name = spliedEntries.Last();

                        if (isFolder)
                        {
                            var directory = new TreeItem(entry, name, "directory");
                            tempTreeItem.AddChildren(directory);
                            tempTreeItem = directory;
                        }
                        else
                        {
                            var file = new TreeItem(entry, name, "file");
                            tempTreeItem.AddChildren(file);
                        }
                    }
                }
            }

            return root;
        }
    }
}
