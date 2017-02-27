using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace RuinsApi
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var config = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("hosting.json", optional: true)
				.AddJsonFile("hosting.prod.json", optional: true)
				.Build();

			int port = 52685;
			if ((args.Length == 1) && !int.TryParse(args[0], out port))
			{
				port = 52685;
			}

			var host = new WebHostBuilder()
				 .UseKestrel()
				 .UseConfiguration(config)
				 .UseContentRoot(Directory.GetCurrentDirectory())
				 .UseIISIntegration()
				 .UseStartup<Startup>()
				 .UseApplicationInsights()
				 .Build();

			host.Run();
		}
	}
}
