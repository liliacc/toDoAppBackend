using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

using toDoAppBackend.Entities;
using toDoAppBackend.Services.ToDoService;

namespace toDoAppBackend
{
    public class Startup
    {
        private IHostingEnvironment _env { get; set; }
        public IConfigurationRoot Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            _env = env;
            var builder = new ConfigurationBuilder()
                      .SetBasePath(env.ContentRootPath)
                      .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                      .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<ToDoDbContext>(
                builder => builder.UseSqlServer(
                    Configuration.GetConnectionString("Default"),
                    dbContextOptions => dbContextOptions.MigrationsHistoryTable("__EFMigrationsHistory")
                )
            );

            Action<MvcOptions> mvcOptionAction = new Action<MvcOptions>(config => { });

            var mvcCoreBuilder = services.AddMvcCore(mvcOptionAction)
                .AddJsonOptions(options => { options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore; })
                .AddApiExplorer();

            services.AddScoped<IToDoService, ToDoService>();

            services.AddSingleton<IConfiguration>(Configuration);

            services.AddOptions();
        }

        public void Configure(IApplicationBuilder app,
                              IHostingEnvironment env,
                              ILoggerFactory loggerFactory,
                              IApplicationLifetime appLifetime)
        {
            app.UseDeveloperExceptionPage();

            app.UseCors("AllowAll");

            app.UseMvc(); // Make Controllers work
        }
    }
}
