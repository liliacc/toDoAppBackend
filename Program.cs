using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace toDoAppBackend
{
    class Program
    {
        static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }
        
        public static IWebHost BuildWebHost(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseUrls("http://*:5000")
                .UseStartup<Startup>()
                .Build();
        }
    }
}
