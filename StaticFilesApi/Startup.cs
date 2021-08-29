using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using FilesServices;
using FolderService;
using Microsoft.OpenApi.Models;
using System.IO;
using System.Reflection;

namespace StaticFilesApi
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IFolderHandlerService, FolderHandlerService>();
            services.AddTransient<IFilesService, FilesService>();
            services.AddTransient<IFilesProvider, FilesProvider>();
            services.AddTransient<IFileModelProvider, FileModelProvider>();

            services.AddDbContext<FileModelsContext>(options =>
            {
                options.UseSqlServer(Environment.GetEnvironmentVariable("ConnectionString"), builder =>
                {
                    builder.CommandTimeout(300);
                    builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                });
            });

            services.AddControllers();

            //TODO:add swagger
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new OpenApiInfo { Title = "StaticFilesApi", Version = "v1" });
            });
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "StaticFilesApi v1"));
         
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
