using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using CanonnApi.Web.Models;
using CanonnApi.Web.Net.Http;

namespace CanonnApi.Web.Services
{
	public class UserInformationService : IUserInformationService
	{
		private readonly string _endpoint;

		protected IIdTokenProvider IdTokenProvider { get; }

		public UserInformationService(IOptions<SecretConfiguration> settings, IIdTokenProvider idTokenProvider)
		{
			if (settings == null)
				throw new ArgumentNullException(nameof(settings));
			if (idTokenProvider == null)
				throw new ArgumentNullException(nameof(idTokenProvider));

			_endpoint = $"https://{settings.Value.ClientDomain}/tokeninfo";
			IdTokenProvider = idTokenProvider;
		}

		public virtual async Task<UserInformationDto> GetFullUserInformation()
		{
			string idToken = IdTokenProvider.GetIdToken();
			return await GetFullUserInformation(idToken);
		}

		protected virtual async Task<UserInformationDto> GetFullUserInformation(string idToken)
		{
			using (var client = new HttpClient())
			{
				var result = await client.PostAsync(new Uri(_endpoint), new JsonContent(new { id_token = idToken } ));
				var resultString = await result.Content.ReadAsStringAsync();
				return JsonConvert.DeserializeObject<UserInformationDto>(resultString);
			}
		}

		public async Task<IEnumerable<string>> GetUserPermissions()
		{
			var fullInfo = await GetFullUserInformation();
			return fullInfo.Permissions;
		}
	}
}
