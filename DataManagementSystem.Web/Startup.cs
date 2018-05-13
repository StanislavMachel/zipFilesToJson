using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataManagementSystem.Web.Data;
using DataManagementSystem.Web.Interfaces;
using DataManagementSystem.Web.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ZipFilesToJson.Common;

namespace DataManagementSystem.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataManagmentSystemContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("DevelopmentConnection")));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddScoped<IStoreZipFileStructureService, StoreZipFileStructureService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            //app.UseCors(builder =>
            //    builder.WithOrigins("https://localhost:44362"));

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
