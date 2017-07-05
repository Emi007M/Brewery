using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace BreweryService
{
    public class Program
    {
        public static void Main(string[] args)
        {
			var config = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
                .AddCommandLine(args)
				.AddEnvironmentVariables(prefix: "ASPNETCORE_")
				.AddJsonFile("hosting.json", optional: true)
				.Build();
			
            var host = new WebHostBuilder()
				.UseConfiguration(config)
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .UseApplicationInsights()
                .Build();

            host.Run();
        }
    }
}