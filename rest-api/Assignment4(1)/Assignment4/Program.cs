// See https://aka.ms/new-console-template for more information


using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
namespace IMDBAPI { 
    public class Program
    {
        static void Main()
        {
            CreateHostBuilder().Build().Run();
        }
        public static IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder().ConfigureWebHostDefaults(webHost =>
            {
                webHost.UseStartup<Startup>();
            });
        }
    }
}