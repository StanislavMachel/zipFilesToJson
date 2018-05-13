using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZipFilesToJson.Common;

namespace DataManagementSystem.Web.Interfaces
{
    public interface IStoreZipFileStructureService
    {
        void StoreToDataBase(TreeItem treeItem);
    }
}
