#if AUTOMATEDTESTS
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace CanonnApi.Web.Authorization
{
	public class NonValidationSecurityTokenValidator : ISecurityTokenValidator
	{
		public NonValidationSecurityTokenValidator()
		{
			CanValidateToken = true;
		}

		public bool CanReadToken(string securityToken)
		{
			return true;
		}

		public ClaimsPrincipal ValidateToken(string securityToken, TokenValidationParameters validationParameters,
			out SecurityToken validatedToken)
		{
			validatedToken = new JwtSecurityTokenHandler().ReadToken(securityToken);

			var claims = new List<Claim>()
			{
				new Claim(ClaimTypes.Name, "ApiTestUser"),
			};

			return new ClaimsPrincipal(new ClaimsIdentity(claims, "ApiTestAuth"));
		}

		public bool CanValidateToken { get; }
		public int MaximumTokenSizeInBytes { get; set; }
	}
}
#endif
