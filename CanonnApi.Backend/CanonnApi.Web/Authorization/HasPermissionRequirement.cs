using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace CanonnApi.Web.Authorization
{
	public class HasPermissionRequirement : IAuthorizationRequirement
	{
		public IEnumerable<string> RequiredPermissions { get; }

		public HasPermissionRequirement(string permission)
			: this(new [] { permission })
		{
		}

		public HasPermissionRequirement(IEnumerable<string> permissions)
		{
			RequiredPermissions = permissions ?? throw new ArgumentNullException(nameof(permissions));
		}
	}
}
