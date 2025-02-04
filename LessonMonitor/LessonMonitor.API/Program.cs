using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace LessonMonitor.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            System.Console.WriteLine(string.Join(", ", args));

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
        }
    }
}
