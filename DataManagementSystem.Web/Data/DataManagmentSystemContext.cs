using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataManagementSystem.Web.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataManagementSystem.Web.Data
{
    public class DataManagmentSystemContext : DbContext
    {
        public DataManagmentSystemContext(DbContextOptions<DataManagmentSystemContext> options): base(options)
        {
            
        }

        public DbSet<ZipFileJson> ZipFilesToJsons { get; set; }
    }
}
