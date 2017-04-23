using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace CanonnApi.Web.Authorization
{
	/// <summary>
	/// Defines the authorization requirement for specific permissions
	/// </summary>
	public class HasPermissionRequirement : IAuthorizationRequirement
	{
		/// <summary>
		/// Gets a collection of permissions that are required
		/// </summary>
		public IEnumerable<string> RequiredPermissions { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="HasPermissionRequirement"/> class
		/// </summary>
		/// <param name="permission">A permission that is required</param>
		public HasPermissionRequirement(string permission)
			: this(new [] { permission })
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="HasPermissionRequirement"/> class
		/// </summary>
		/// <param name="permissions">A collection of permissions that are required</param>
		public HasPermissionRequirement(IEnumerable<string> permissions)
		{
			RequiredPermissions = permissions ?? throw new ArgumentNullException(nameof(permissions));
		}
	}
}
