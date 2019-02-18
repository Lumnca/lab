using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace LabExam
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).
                UseKestrel(options =>
                {
                    options.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(1800);
                    options.Limits.RequestHeadersTimeout = TimeSpan.FromMinutes(20);
                })
                .Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
