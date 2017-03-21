using System.Collections.Generic;
using System.Threading.Tasks;
using CanonnApi.Web.Models;

namespace CanonnApi.Web.Services
{
	public interface IUserInformationService
	{
		Task<UserInformationDto> GetFullUserInformation();
		Task<IEnumerable<string>> GetUserPermissions();
	}
}
