using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RuinsApi.Models;
using RuinsApi.Services;
using Serilog;

namespace RuinsApi
{
	public class Startup
	{
		public IConfigurationRoot Configuration { get; private set; }
		public IContainer ApplicationContainer { get; private set; }

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
		public IServiceProvider ConfigureServices(IServiceCollection services)
		{
			Log.Logger.Information("Configuring services...");

			// Add framework services.
			services.AddCors(corsOptions => corsOptions.AddPolicy("default", corsPolicyBuilder =>
			{
				corsPolicyBuilder.AllowAnyOrigin();
				corsPolicyBuilder.AllowAnyHeader();
				corsPolicyBuilder.AllowAnyMethod();
			}));

			services.AddMvc();
			services.AddLogging();
			services.AddMemoryCache();

			services.Configure<SecretConfiguration>(Configuration.GetSection("clientSecrets"));

			var builder = new ContainerBuilder();
			RegisterAutofacDependencies(builder);

			// When you do service population, it will include your controller types automatically.
			builder.Populate(services);
			// If you want to set up a controller for, say, property injection
			// you can override the controller registration after populating services.

			ApplicationContainer = builder.Build();

			// Create the IServiceProvider based on the container
			return new AutofacServiceProvider(ApplicationContainer);
		}

		private void RegisterAutofacDependencies(ContainerBuilder builder)
		{
			builder.RegisterType<CachingUserInformationService>().AsImplementedInterfaces();
			builder.RegisterType<IdTokenProvider>().AsImplementedInterfaces();
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
			// Ensure container and dependencies are cleaned up accordingly
			appLifetime.ApplicationStopped.Register(ApplicationContainer.Dispose);
		}
	}
}
