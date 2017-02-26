using Microsoft.AspNetCore.Mvc;
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
			};
		}
	}
}
