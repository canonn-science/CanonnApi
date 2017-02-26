using System;
using System.IdentityModel.Tokens.Jwt;

namespace RuinsApi.Services
{
	public interface IIdTokenProvider
	{
		string GetIdToken();

		JwtSecurityToken GetParsedToken();
		JwtSecurityToken GetParsedToken(string idToken);

		DateTime GetTokenExpiry();
		DateTime GetTokenExpiry(string idToken);
		DateTime GetTokenExpiry(JwtSecurityToken idToken);
	}
}
