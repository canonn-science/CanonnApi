using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using CanonnApi.Web.Services;

namespace CanonnApi.Web.Authorization
{
	public class PermissionAuthorizationHandler : AuthorizationHandler<HasPermissionRequirement>
	{
		private readonly IUserInformationService _userInformationService;

		public PermissionAuthorizationHandler(IUserInformationService userInformationService)
		{
			_userInformationService = userInformationService ?? throw new ArgumentNullException(nameof(userInformationService));
		}

		protected override Task HandleRequirementAsync(
			AuthorizationHandlerContext context,
			HasPermissionRequirement requirement)
		{
			if (context.User.Identity.IsAuthenticated)
			{
				var currentUserPermissions = _userInformationService.GetUserPermissions().Result;

				// check if current user has all required permissions
				var hasAllRequiredPermissions = requirement.RequiredPermissions
					.All(perm => currentUserPermissions.Contains(perm));

				if (hasAllRequiredPermissions)
				{
					context.Succeed(requirement);
				}
			}

			return Task.CompletedTask;
		}
	}
}
