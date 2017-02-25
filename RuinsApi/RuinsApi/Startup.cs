using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RuinsApi.Models;
using Serilog;

namespace RuinsApi
{
	public class Startup
	{
		public IConfigurationRoot Configuration { get; }

		public Startup(IHostingEnvironment env)
		{
			// configuration
			var builder = new ConfigurationBuilder()
				 .SetBasePath(env.ContentRootPath)
				 .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
				 .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
				 .AddJsonFile("appsettings.Secrets.json", optional: true)
				 .AddEnvironmentVariables();

			Configuration = builder.Build();

			// logging
			Log.Logger = new LoggerConfiguration()
				.ReadFrom.Configuration(Configuration)
				.CreateLogger();
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			Log.Logger.Information("Configuring services...");

			// Add framework services.
			services.AddCors(options => options.AddPolicy("default", builder =>
			{
				builder.AllowAnyOrigin();
				builder.AllowAnyHeader();
				builder.AllowAnyMethod();
			}));

			services.AddMvc();
			services.AddLogging();

			services.Configure<SecretConfiguration>(Configuration.GetSection("clientSecrets"));
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IApplicationLifetime appLifetime, IOptions<SecretConfiguration> settings)
		{
			loggerFactory.AddSerilog();
			Log.Logger.Information("Configuring http request pipeline: {environment}", new { EnvironmentName = env.EnvironmentName, ApplicationName = env.ApplicationName});

			if (env.IsDevelopment())
			{
				app.UseCors("default");
				app.UseDeveloperExceptionPage();
			}

			SecretConfiguration secretConfiguration = settings.Value;
			app.UseJwtBearerAuthentication(new JwtBearerOptions
			{
				Audience = secretConfiguration.ClientId,
				Authority = $"https://{secretConfiguration.ClientDomain}/",
			});

			app.UseMvc();

			// Ensure any buffered events are sent at shutdown
			appLifetime.ApplicationStopped.Register(Log.CloseAndFlush);
		}
	}
}
