using System.Collections.Generic;
using System.Threading.Tasks;
using RuinsApi.Models;

namespace RuinsApi.Services
{
	public interface IUserInformationService
	{
		Task<UserInformationDto> GetFullUserInformation();
		Task<IEnumerable<string>> GetUserPermission();
	}
}
