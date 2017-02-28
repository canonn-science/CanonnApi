using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace RuinsApi.Services
{
	public class IdTokenProvider : IIdTokenProvider
	{
		private readonly IHttpContextAccessor _contextAccessor;

		public IdTokenProvider(IHttpContextAccessor contextAccessor)
		{
			if (contextAccessor == null)
				throw new ArgumentNullException(nameof(contextAccessor));

			_contextAccessor = contextAccessor;
		}

		public string GetIdToken()
		{
			var authHeader = _contextAccessor.HttpContext.Request
				.Headers["Authorization"].FirstOrDefault();

			if (!String.IsNullOrWhiteSpace(authHeader))
				return authHeader.Replace("Bearer ", String.Empty);

			throw new UnauthorizedAccessException("No authorization bearer token was provided.");
		}

		public JwtSecurityToken GetParsedToken()
		{
			return GetParsedToken(GetIdToken());
		}

		public JwtSecurityToken GetParsedToken(string idToken)
		{
			if (String.IsNullOrEmpty(idToken))
				throw new ArgumentNullException(nameof(idToken));

			return new JwtSecurityToken(idToken);
		}

		public DateTime GetTokenExpiry()
		{
			return GetTokenExpiry(GetIdToken());
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
