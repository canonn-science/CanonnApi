using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CanonnApi.Web.Services
{
	public class BearerTokenProvider : IBearerTokenProvider
	{
		private readonly ILogger<BearerTokenProvider> _logger;
		private readonly IHttpContextAccessor _contextAccessor;

		public BearerTokenProvider(ILogger<BearerTokenProvider> logger, IHttpContextAccessor contextAccessor)
		{
			_logger = logger;
			_contextAccessor = contextAccessor ?? throw new ArgumentNullException(nameof(contextAccessor));
		}

		public string GetBearerToken()
		{
			var authHeader = _contextAccessor.HttpContext.Request
				.Headers["Authorization"].FirstOrDefault();

			if (!String.IsNullOrWhiteSpace(authHeader))
			{
				string tokenValue = authHeader.Replace("Bearer ", String.Empty);

				_logger.LogTrace("Retrieving bearer token value: {tokenValue}.", tokenValue);

				return tokenValue;
			}

			throw new UnauthorizedAccessException("No authorization bearer token was provided.");
		}

		public JwtSecurityToken GetParsedToken()
		{
			return GetParsedToken(GetBearerToken());
		}

		public JwtSecurityToken GetParsedToken(string idToken)
		{
			return new JwtSecurityToken(idToken ?? throw new ArgumentNullException(nameof(idToken)));
		}

		public DateTime GetTokenExpiry()
		{
			return GetTokenExpiry(GetBearerToken());
		}

		public DateTime GetTokenExpiry(string idToken)
		{
			if (String.IsNullOrEmpty(idToken))
				throw new ArgumentNullException(nameof(idToken));

			return GetTokenExpiry(GetParsedToken(idToken));
		}

		public DateTime GetTokenExpiry(JwtSecurityToken idToken)
		{
			if (idToken == null)
				throw new ArgumentNullException(nameof(idToken));

			return idToken.ValidTo;
		}
	}
}
