using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RuinsApi.Models;
using System;

namespace RuinsApi.Controllers
{
	[Produces("application/json")]
	[Route("v1/[controller]")]
	public class ClientConfigurationController : Controller
	{
		private readonly SecretConfiguration _settings;
		private ILogger<ClientConfigurationController> _logger;

		public ClientConfigurationController(ILogger<ClientConfigurationController> logger, IOptions<SecretConfiguration> settings)
		{
			if (settings == null)
				throw new ArgumentNullException(nameof(settings));

			if (settings.Value == null)
				throw new ArgumentNullException(nameof(settings.Value));

			_logger = logger;
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
			};
		}
	}
}