using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RuinsApi.Services;

namespace RuinsApi.Controllers
{
	[Authorize]
	[Route("v1/[controller]")]
	public class UserInformationController : Controller
	{
		private readonly IUserInformationService _userInformationService;

		public UserInformationController(IUserInformationService userInformationService)
		{
			if (userInformationService == null)
				throw new ArgumentNullException(nameof(userInformationService));

			_userInformationService = userInformationService;
		}

		[HttpGet]
		public async Task<JsonResult> Get()
		{
			return Json(await _userInformationService.GetFullUserInformation());
		}

		[HttpGet("permissions")]
		public async Task<JsonResult> GetPermissions()
		{
			return Json(await _userInformationService.GetUserPermission());
		}
	}
}
