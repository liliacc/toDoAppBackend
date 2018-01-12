using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using toDoAppBackend.Entities;
using toDoAppBackend.Services;

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
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", new CorsPolicyBuilder()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin()
                    .AllowCredentials()
                    .Build());
            });
            
            services.AddDbContext<ToDoDbContext>(options =>
                options.UseSqlite("Data Source=MvcMovie.db"));

            services.AddMvc();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IToDoService, ToDoService>();

            services.AddSingleton<IConfiguration>(Configuration);

            services.AddOptions();

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors("AllowAll");
            app.UseDeveloperExceptionPage();
            app.UseMvc(); // Make Controllers work
        }
    }
}
