using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CanonnApi.Web.Services;

namespace CanonnApi.Web.Controllers
{
	[Authorize]
	[Route("v1/[controller]")]
	public class UserInformationController : Controller
	{
		private readonly IUserInformationService _userInformationService;

		public UserInformationController(IUserInformationService userInformationService)
		{
			_userInformationService = userInformationService ?? throw new ArgumentNullException(nameof(userInformationService));
		}

		[HttpGet]
		public async Task<JsonResult> Get()
		{
			return Json(await _userInformationService.GetFullUserInformation());
		}

		[HttpGet("permissions")]
		public async Task<JsonResult> GetPermissions()
		{
			return Json(await _userInformationService.GetUserPermissions());
		}
	}
}
