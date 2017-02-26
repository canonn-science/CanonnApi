using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
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

		public Task<string> GetIdToken()
		{
			return Task.Run(() => _contextAccessor.HttpContext.Request
				.Headers["Authorization"].First()
				.Replace("Bearer ", String.Empty)
			);
		}

		public async Task<JwtSecurityToken> GetParsedToken()
		{
			// parse token to know how long we may cache this information
			var token = await GetIdToken();
			return new JwtSecurityToken(token);
		}

		public async Task<DateTime> GetTokenExpiry()
		{
			var token = await GetParsedToken();
			return token.ValidTo;
		}
	}
}
