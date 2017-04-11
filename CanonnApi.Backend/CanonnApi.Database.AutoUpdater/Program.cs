using System;
using System.IO;
using System.Reflection;
using DbUp;
using DbUp.Helpers;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace Canonn.Database.AutoUpdater
{
	public class Program
	{
		static int Main(string[] args)
		{
			var logger = new LoggerConfiguration().WriteTo.LiterateConsole().CreateLogger();

			var config = new ConfigurationBuilder()
				 .SetBasePath(Directory.GetCurrentDirectory())
				 .AddJsonFile("config.json", optional: true)
				 .Build();

			string connectionString = config["connectionString"];

			// string connectionString = ConfigurationManager.ConnectionStrings["canonnDb"]?.ConnectionString;
			// override if connection string is given as argument
			if (args.Length >= 1)
			{
				connectionString = args[0];
			}

			if (String.IsNullOrWhiteSpace(connectionString))
			{
				logger.Error("No configuration string provided via app config or command line");
				return -1;
			}

			var upgrader = DeployChanges.To.MySqlDatabase($"{connectionString};Allow User Variables=True")
				 .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
				 .JournalTo(new NullJournal())
				 .LogTo(new UpgradeLoggerSerilog(logger))
				 .Build();

			var result = upgrader.PerformUpgrade();

			if (!result.Successful)
			{
				logger.Error(result.Error, "Error while performing database update");

#if DEBUG
				Console.ReadKey();
#endif
				return -1;
			}

#if DEBUG
			Console.ReadKey();
#endif
			return 0;
		}
	}
}
