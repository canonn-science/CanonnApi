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
			// TODO: make this a bit more safe. When no auth header is there, this fails hard
			return _contextAccessor.HttpContext.Request
				.Headers["Authorization"].First()
				.Replace("Bearer ", String.Empty);
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
