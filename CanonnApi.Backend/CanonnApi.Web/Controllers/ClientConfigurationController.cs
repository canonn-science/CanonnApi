using System;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using CanonnApi.Web.Models;

namespace CanonnApi.Web.Controllers
{
	[Produces("application/json")]
	[Route("v1/[controller]")]
	public class ClientConfigurationController : Controller
	{
		private readonly SecretConfiguration _settings;
		public ClientConfigurationController(IOptions<SecretConfiguration> settings)
		{
			if (settings == null)
				throw new ArgumentNullException(nameof(settings));

			_settings = settings.Value;
		}

		// GET v1/clientconfiguration
		[HttpGet()]
		public ClientConfiguration Client()
		{
			return new ClientConfiguration()
			{
				Domain = _settings.ClientDomain,
				ClientId = _settings.ClientId,
				ApiVersion = GetApiVersion(),
			};
		}

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
