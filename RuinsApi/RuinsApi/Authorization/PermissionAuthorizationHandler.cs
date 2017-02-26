using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using RuinsApi.Services;

namespace RuinsApi.Authorization
{
	public class PermissionAuthorizationHandler : AuthorizationHandler<HasPermissionRequirement>
	{
		private readonly IUserInformationService _userInformationService;

		public PermissionAuthorizationHandler(IUserInformationService userInformationService)
		{
			if (userInformationService == null)
				throw new ArgumentNullException(nameof(userInformationService));

			_userInformationService = userInformationService;
		}

		protected override Task HandleRequirementAsync(
			AuthorizationHandlerContext context,
			HasPermissionRequirement requirement)
		{
			var currentUserPermissions = _userInformationService.GetUserPermissions().Result;

			// check if current user has all required permissions
			var hasAllRequiredPermissions = requirement.RequiredPermissions
				.All(perm => currentUserPermissions.Contains(perm));

			if (hasAllRequiredPermissions)
				context.Succeed(requirement);

			return Task.CompletedTask;
		}
	}
}
