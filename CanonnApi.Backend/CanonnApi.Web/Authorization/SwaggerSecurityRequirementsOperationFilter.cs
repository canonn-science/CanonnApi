using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CanonnApi.Web.Authorization
{
	/// <summary>
	/// Filters operations (actions) based on the <see cref="AuthorizeAttribute"/> values for SwaggerGen
	/// </summary>
	public class SwaggerSecurityRequirementsOperationFilter : IOperationFilter
	{
		private readonly IOptions<AuthorizationOptions> _authorizationOptions;

		/// <summary>
		/// Initializes a new instance of the <see cref="SwaggerSecurityRequirementsOperationFilter"/>
		/// </summary>
		/// <param name="authorizationOptions">An instance of <see cref="AuthorizationOptions"/> to use for this filter</param>
		public SwaggerSecurityRequirementsOperationFilter(IOptions<AuthorizationOptions> authorizationOptions)
		{
			_authorizationOptions = authorizationOptions ?? throw new ArgumentNullException(nameof(authorizationOptions));
		}

		/// <summary>
		/// Applies the filter to the given operation based on the context
		/// </summary>
		/// <param name="operation">An instance of the <see cref="Operation"/> to apply the filter on</param>
		/// <param name="context">An instance of the <see cref="OperationFilterContext"/> that will be used to apply the filter</param>
		public void Apply(Operation operation, OperationFilterContext context)
		{
			var controllerPolicies = context.ApiDescription.ControllerAttributes()
				.OfType<AuthorizeAttribute>()
				.Select(attr => attr.Policy);

			var actionPolicies = context.ApiDescription.ActionAttributes()
				.OfType<AuthorizeAttribute>()
				.Select(attr => attr.Policy);

			var policies = controllerPolicies.Union(actionPolicies).Distinct();

			var requiredPermissions = policies
				.Select(x => this._authorizationOptions.Value.GetPolicy(x))
				.SelectMany(x => x.Requirements)
				.OfType<HasPermissionRequirement>()
				.SelectMany(x => x.RequiredPermissions);

			var permissions = requiredPermissions as string[] ?? requiredPermissions.ToArray();

			if (!permissions.Any())
				return;

			operation.Responses.Add("401", new Response { Description = "Unauthorized" });
			operation.Responses.Add("403", new Response { Description = "Forbidden" });

			operation.Security = new List<IDictionary<string, IEnumerable<string>>>
			{
				new Dictionary<string, IEnumerable<string>>
				{
					{ "oauth2", permissions }
				}
			};
		}
	}
}