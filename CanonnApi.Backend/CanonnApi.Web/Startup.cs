using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Serilog;
using Swashbuckle.AspNetCore.Swagger;
using CanonnApi.Web.Authorization;
using CanonnApi.Web.Automapper;
using CanonnApi.Web.DatabaseModels;
using CanonnApi.Web.Middlewares;
using CanonnApi.Web.Models;
using CanonnApi.Web.Services;
using CanonnApi.Web.Services.DataAccess;
using CanonnApi.Web.Services.Maps;
using CanonnApi.Web.Services.RemoteApis;
using CanonnApi.Web.Services.RuinSites;

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

			Log.Logger = new LoggerConfiguration()
				.ReadFrom.Configuration(Configuration)
				.CreateLogger();

			Log.Information("Starting up...");
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		public IServiceProvider ConfigureServices(IServiceCollection services)
		{
			Log.Information("Configuring services...");

			// Add framework services.
			services.AddCors(corsOptions => corsOptions.AddPolicy("default", corsPolicyBuilder =>
			{
				corsPolicyBuilder.AllowAnyOrigin();
				corsPolicyBuilder.AllowAnyHeader();
				corsPolicyBuilder.AllowAnyMethod();
			}));

			services.AddMvc()
				.AddXmlSerializerFormatters()
				.AddJsonOptions(mvcJsonOptions =>
				{
					mvcJsonOptions.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
				});

			var automapperConfiguration = new MappingConfiguration(Configuration);
			services.AddAutoMapper(config => automapperConfiguration.Configure(config));

			services.AddLogging();
			services.AddMemoryCache();
			services.AddAuthorization(authorizationOptions => AddPolicies(authorizationOptions));
			services.AddDbContext<RuinsContext>(ruinsDbOptions =>
			{
				var connectionString = Configuration.GetSection("connectionStrings:ruinsDb").Value;
				ruinsDbOptions.UseMySql(connectionString);
			});

			services.AddSwaggerGen(swaggerGenOptions =>
			{
				swaggerGenOptions.SwaggerDoc("v1", new Info()
				{
					Title = "Canonn API",
					Version = "v1",
					Description = "API for the Canonn research group.",
					Contact = new Contact()
					{
						Name = "Cmdr. Nopileos",
						Url = "https://github.com/gingters",
					},
					License = new License() { Name = "Licensed under the MIT license.", Url = "https://opensource.org/licenses/MIT" },
				});

				swaggerGenOptions.AddSecurityDefinition("oauth2", new OAuth2Scheme()
				{
					Type = "oauth2",
					Flow = "implicit",
					AuthorizationUrl = $"https://{Configuration.GetSection("clientSecrets:clientDomain").Value}/authorize",
				});

				swaggerGenOptions.OperationFilter<SwaggerSecurityRequirementsOperationFilter>();

				//Set the comments path for the swagger json and ui.
				var basePath = PlatformServices.Default.Application.ApplicationBasePath;
				var xmlPath = Path.Combine(basePath, "CanonnApi.Web.xml");
				swaggerGenOptions.IncludeXmlComments(xmlPath);
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
			builder.RegisterInstance(Configuration).AsImplementedInterfaces();
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
			Log.Information("Configuring http request pipeline: {environment}", new { EnvironmentName = env.EnvironmentName, ApplicationName = env.ApplicationName });

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

			app.UseSwagger(swaggerOptions =>
			{
				swaggerOptions.RouteTemplate = "docs/swagger/{documentName}/swagger.json";
			});

			app.UseSwaggerUI(swaggerUiOptions =>
			{
				swaggerUiOptions.RoutePrefix = "docs";
				swaggerUiOptions.SwaggerEndpoint("swagger/v1/swagger.json", "Canonn API v1");
				swaggerUiOptions.ConfigureOAuth2(secretConfiguration.ClientId, secretConfiguration.ClientSecret,
					secretConfiguration.ClientDomain, "Canonn API",
					additionalQueryStringParameters: new { audience = secretConfiguration.Audience });
			});

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
