using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using CanonnApi.Web.Services;
using Microsoft.Extensions.Logging;

namespace CanonnApi.Web.Authorization
{
	/// <summary>
	/// Handles permissions authorization for AspNet Core
	/// </summary>
	public class PermissionAuthorizationHandler : AuthorizationHandler<HasPermissionRequirement>
	{
		private readonly ILogger<PermissionAuthorizationHandler> _logger;
		private readonly IBearerTokenProvider _bearerTokenProvider;

		/// <summary>
		/// Initializes a new instance of the <see cref="PermissionAuthorizationHandler"/> class
		/// </summary>
		/// <param name="logger">An instance of an <see cref="ILogger"/> to log to</param>
		/// <param name="bearerTokenProvider">An instance of an <see cref="IBearerTokenProvider"/> to retrieve an authentication token from</param>
		public PermissionAuthorizationHandler(ILogger<PermissionAuthorizationHandler> logger, IBearerTokenProvider bearerTokenProvider)
		{
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
			_bearerTokenProvider = bearerTokenProvider ?? throw new ArgumentNullException(nameof(bearerTokenProvider));
		}

		/// <summary>
		/// Asynchronously checks whether a user is authenticated and has the required permissions
		/// </summary>
		/// <remarks>When all permissions were checked successfully, the <pre>.Succeed()</pre> method on the <see cref="AuthorizationHandlerContext"/> will be called</remarks>
		/// <param name="context">An <see cref="AuthorizationHandlerContext"/> to retrieve user information from</param>
		/// <param name="requirement">The <see cref="HasPermissionRequirement"/> instance to check against for required permissions</param>
		/// <returns>When the task completed.</returns>
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
