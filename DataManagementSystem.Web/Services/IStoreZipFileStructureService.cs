using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataManagementSystem.Web.Data;
using DataManagementSystem.Web.Entities;
using DataManagementSystem.Web.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ZipFilesToJson.Common;

namespace DataManagementSystem.Web.Services
{
    public class StoreZipFileStructureService : IStoreZipFileStructureService
    {
        private readonly DataManagmentSystemContext _context;
        private readonly IConfiguration _configuration;

        public StoreZipFileStructureService(DataManagmentSystemContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public void StoreToDataBase(TreeItem treeItem)
        {
            var treeItemJson = JsonConvert.SerializeObject(treeItem, Formatting.Indented,
                new DecryptingJsonConverter(new Aes128BitEcbMode(_configuration)));

            var zipFileJson = new ZipFileJson()
            {
                ZipFileStructureJson = treeItemJson
            };

            _context.ZipFilesToJsons.Add(zipFileJson);
            _context.SaveChanges();
        }
    }
}
