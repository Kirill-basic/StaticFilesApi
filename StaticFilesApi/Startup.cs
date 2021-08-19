using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using FilesServices;
using FolderService;

namespace StaticFilesApi
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IFolderHandlerService, FolderHandlerService>();
            services.AddTransient<IFilesService, FilesService>();
            services.AddTransient<IFilesProvider, FilesProvider>();

            services.AddDbContext<FileModelsContext>(options =>
            {
                options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=FilesDb;Trusted_Connection=True;", builder =>
                {
                    builder.CommandTimeout(300);
                    builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                });
            });
            

            services.AddControllers();
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
