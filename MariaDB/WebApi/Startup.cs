using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace WebApi
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
            var connString = Configuration.GetConnectionString("MariaDbConnectionString");

            services.AddDbContextPool<ApplicationDbContext>(options =>
            {
                options.UseMySql(connString, ServerVersion.AutoDetect(connString));
            });

            services.AddControllers();
            //services.AddDbContext<ApplicationDbContext>(options =>
            //{

            //    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            //    {
            //        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            //    }
            //    else
            //    {
            //        options.UseSqlite("Data source=todo.db");
            //    }
            //});
            // services.AddAutoMapper(typeof(Services.Mapper));
            services.AddSwaggerGen(options =>
            {

                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "MariaDB.API",
                    Version = "v1"
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.InjectStylesheet("/swagger/SwaggerHeader.css");
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "MariaDB API v1");
            });

            app.UseStaticFiles();

            app.Run(context =>
            {
                context.Response.Redirect("/swagger");
                return Task.CompletedTask;
            });
        }
    }
}
