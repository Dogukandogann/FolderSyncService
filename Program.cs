using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace DocumentService
{
    public class Program
    {
       
        public static  void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("DocumentService/log.txt",rollingInterval:RollingInterval.Day)
                .CreateLogger();
            CreateHostBuilder(args).Build().Run();
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseSerilog()
            .UseWindowsService()
            .ConfigureServices((hostContext, services) =>
            {
                services.AddHostedService<Worker>();
            });
    }
}
