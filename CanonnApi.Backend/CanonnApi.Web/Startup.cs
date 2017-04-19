using System;
using System.Collections.Generic;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using CanonnApi.Web.Authorization;
using CanonnApi.Web.Services.DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using CanonnApi.Web.DatabaseModels;
using CanonnApi.Web.Json.Serialization;
using CanonnApi.Web.Middlewares;
using CanonnApi.Web.Models;
using CanonnApi.Web.Services;
using CanonnApi.Web.Services.Maps;
using CanonnApi.Web.Services.RemoteApis;
using CanonnApi.Web.Services.RuinSites;
using Serilog;

namespace CanonnApi.Web
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
				 .AddJsonFile("appsettings.Secrets.prod.json", optional: true) // for the servers, so that copying the dev secrets does not destroy prod config ;-)
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

			services.AddMvc()
				.AddJsonOptions(mvcJsonOptions => mvcJsonOptions.SerializerSettings.ContractResolver = new IgnoreEmptyEnumerablesResolver());

			services.AddLogging();
			services.AddMemoryCache();
			services.AddAuthorization(authorizationOptions => AddPolicies(authorizationOptions));
			services.AddDbContext<RuinsContext>(ruinsDbOptions =>
			{
				var connectionString = Configuration.GetSection("connectionStrings:ruinsDb").Value;
				ruinsDbOptions.UseMySql(connectionString);
			});

			services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
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

		private void AddPolicies(AuthorizationOptions authorizationOptions)
		{
			// currently we rely on policies - one for each permission. More dynamically would be cool,
			// but that's quite some magic or tons of effort required, so that'll do for now...
			var permissions = GetPermissions();
			foreach (var permission in permissions)
			{
				authorizationOptions.AddPolicy(permission, policyBuilder =>
				{
					policyBuilder.AddRequirements(new DenyAnonymousAuthorizationRequirement());
					policyBuilder.AddRequirements(new HasPermissionRequirement(permission));
				});
			}
		}

		private void RegisterAutofacDependencies(ContainerBuilder builder)
		{
			builder.RegisterType<BearerTokenProvider>().AsImplementedInterfaces();

			// Codex base data
			builder.RegisterType<ArtifactRepository>().AsImplementedInterfaces();
			builder.RegisterType<CodexCategoryRepositoy>().AsImplementedInterfaces();
			builder.RegisterType<CodexDataRepository>().AsImplementedInterfaces();
			// Ruin base data
			builder.RegisterType<ObeliskGroupRepository>().AsImplementedInterfaces();
			builder.RegisterType<RuinTypeRepository>().AsImplementedInterfaces();
			builder.RegisterType<ObeliskRepository>().AsImplementedInterfaces();
			// System base data
			builder.RegisterType<SystemRepository>().AsImplementedInterfaces();
			builder.RegisterType<BodyRepository>().AsImplementedInterfaces();
			// Ruin site data
			builder.RegisterType<RuinSiteRepository>().AsImplementedInterfaces();
			builder.RegisterType<MapsRepository>().AsImplementedInterfaces();

			// helper services
			builder.RegisterType<EdsmService>().AsImplementedInterfaces();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IApplicationLifetime appLifetime, IOptions<SecretConfiguration> settings)
		{
			loggerFactory.AddSerilog();
			Log.Logger.Information("Configuring http request pipeline: {environment}", new { EnvironmentName = env.EnvironmentName, ApplicationName = env.ApplicationName});

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseCors("default");

			SecretConfiguration secretConfiguration = settings.Value;
			app.UseJwtBearerAuthentication(new JwtBearerOptions()
			{
				Audience = secretConfiguration.Audience,
				Authority = $"https://{secretConfiguration.ClientDomain}/",
			});

			app.UseMiddleware<HttpErrorHandleMiddleware>();
			app.UseMvc();

			// Ensure any buffered events are sent at shutdown
			appLifetime.ApplicationStopped.Register(Log.CloseAndFlush);
			// Ensure container and dependencies are cleaned up accordingly
			appLifetime.ApplicationStopped.Register(ApplicationContainer.Dispose);
		}

		private IEnumerable<string> GetPermissions()
		{
			return new[]
			{
				"add:codexdata",
				"edit:codexdata",
				"delete:codexdata",

				"add:ruinbasedata",
				"edit:ruinbasedata",
				"delete:ruinbasedata",

				"add:ruinsitedata",
				"edit:ruinsitedata",
				"delete:ruinsitedata",

				"add:systemdata",
				"edit:systemdata",
				"delete:systemdata",
			};
		}
	}
}
