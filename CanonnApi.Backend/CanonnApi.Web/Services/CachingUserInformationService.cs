using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using CanonnApi.Web.Models;

namespace CanonnApi.Web.Services
{
	public class CachingUserInformationService : UserInformationService
	{
		private readonly IMemoryCache _memoryCache;

		public CachingUserInformationService(IMemoryCache memoryCache, IOptions<SecretConfiguration> secretConfiguration, IIdTokenProvider idTokenProvider)
			: base(secretConfiguration, idTokenProvider)
		{
			if (memoryCache == null)
				throw new ArgumentNullException(nameof(memoryCache));

			_memoryCache = memoryCache;
		}

		protected override async Task<UserInformationDto> GetFullUserInformation(string idToken)
		{
			UserInformationDto userInformation;

			if (!_memoryCache.TryGetValue(idToken, out userInformation))
			{
				userInformation = await base.GetFullUserInformation(idToken);

				var tokenExpiry = IdTokenProvider.GetTokenExpiry(idToken);
				var options = new MemoryCacheEntryOptions()
					.SetAbsoluteExpiration(tokenExpiry);

				_memoryCache.Set(idToken, userInformation, options);
			}

			return userInformation;
		}
	}
}
