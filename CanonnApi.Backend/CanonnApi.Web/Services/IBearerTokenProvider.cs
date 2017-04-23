using System;
using System.IdentityModel.Tokens.Jwt;

namespace CanonnApi.Web.Services
{
	public interface IBearerTokenProvider
	{
		string GetBearerToken();

		JwtSecurityToken GetParsedToken();
		JwtSecurityToken GetParsedToken(string idToken);

		DateTime GetTokenExpiry();
		DateTime GetTokenExpiry(string idToken);
		DateTime GetTokenExpiry(JwtSecurityToken idToken);
	}
}
