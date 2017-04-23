using System;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using CanonnApi.Web.Models;

namespace CanonnApi.Web.Controllers
{
	/// <summary>
	/// Provides endpoints to configure the client applications
	/// </summary>
	[Route("v1/clientconfiguration")]
	[Produces("application/json")]
	[ApiExplorerSettings(IgnoreApi = true)]
	public class ClientConfigurationController : Controller
	{
		private readonly SecretConfiguration _settings;

		/// <summary>
		/// Initializes a new instance of the <see cref="ClientConfigurationController"/>
		/// </summary>
		/// <param name="settings">The configuration settings to provide</param>
		public ClientConfigurationController(IOptions<SecretConfiguration> settings)
		{
			_settings = settings?.Value ?? throw new ArgumentNullException(nameof(settings));
		}

		/// <summary>
		/// Retrieves the configuration for the client
		/// </summary>
		/// <returns>A <see cref="ClientConfiguration"/> fo the client to consume</returns>
		[HttpGet()]
		public ClientConfiguration GetConfiguration()
		{
			return new ClientConfiguration()
			{
				Domain = _settings.ClientDomain,
				ClientId = _settings.ClientId,
				ApiVersion = GetApiVersion(),
				Audience = _settings.Audience,
			};
		}

		/// <summary>
		/// Determines the current version of this API
		/// </summary>
		/// <returns>A string representing the current API assembly informational version</returns>
		private string GetApiVersion()
		{
			return typeof(ClientConfigurationController)
				.GetTypeInfo()
				.Assembly
				.GetCustomAttribute<AssemblyInformationalVersionAttribute>()
				.InformationalVersion;
		}
	}
}
