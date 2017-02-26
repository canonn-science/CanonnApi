using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace RuinsApi.Authorization
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
			if (permissions == null)
				throw new ArgumentNullException(nameof(permissions));

			RequiredPermissions = permissions;
		}
	}
}
