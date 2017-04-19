using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using CanonnApi.Web.Services;
using Microsoft.Extensions.Logging;

namespace CanonnApi.Web.Authorization
{
	public class PermissionAuthorizationHandler : AuthorizationHandler<HasPermissionRequirement>
	{
		private readonly ILogger<PermissionAuthorizationHandler> _logger;
		private readonly IBearerTokenProvider _bearerTokenProvider;

		public PermissionAuthorizationHandler(ILogger<PermissionAuthorizationHandler> logger, IBearerTokenProvider boearerTokenProvider)
		{
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
			_bearerTokenProvider = boearerTokenProvider ?? throw new ArgumentNullException(nameof(boearerTokenProvider));
		}

		protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HasPermissionRequirement requirement)
		{
			if (!context.User.Identity.IsAuthenticated)
			{
				_logger.LogDebug("User not authenticated, no need to further check permissions.");
				return Task.CompletedTask;
			}

			var token = _bearerTokenProvider.GetParsedToken();

			var currentUserPermissions = token.Claims
				.Where(c => c.Type == "https://api.canonn.technology/permissions")
				.Select(c => c.Value);

			// check if current user has all required permissions
			var hasAllRequiredPermissions = requirement.RequiredPermissions
				.All(perm => currentUserPermissions.Contains(perm));

			_logger.LogDebug("Checking permissions: Required: {requiredPermissions}, available: {currentUserPermissions}, successful: {hasAllRequiredPermissions}", requirement.RequiredPermissions, currentUserPermissions, hasAllRequiredPermissions);

			if (hasAllRequiredPermissions)
			{
				context.Succeed(requirement);
			}

			return Task.CompletedTask;
		}
	}
}
