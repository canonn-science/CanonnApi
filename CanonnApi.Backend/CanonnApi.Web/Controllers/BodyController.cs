using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CanonnApi.Web.DatabaseModels;
using CanonnApi.Web.Services.DataAccess;
using CanonnApi.Web.Services.RemoteApis;

namespace CanonnApi.Web.Controllers
{
	[Route("v1/stellar/bodies")]
	public class BodyController : BaseDataController<Body>
	{
		private readonly IEdsmService _edsmService;
		protected new IBodyRepository Repository => (IBodyRepository)base.Repository;

		public BodyController(ILogger<BodyController> logger, IBodyRepository repository, IEdsmService edsmService)
			:base(logger, repository)
		{
			_edsmService = edsmService ?? throw new ArgumentNullException(nameof(edsmService));
		}

		[HttpPut("{id}")]
		[Authorize(Policy = "add:systemdata")]
		[Authorize(Policy = "edit:systemdata")]
		public override async Task<Body> CreateOrUpdate([FromBody] Body data, int id)
		{
			return await base.CreateOrUpdate(data, id);
		}

		[HttpPatch("{id}")]
		[Authorize(Policy = "edit:systemdata")]
		public override async Task<Body> Update([FromBody] Body data, int id)
		{
			return await base.Update(data, id);
		}

		[HttpDelete("{id}")]
		[Authorize(Policy = "delete:systemdata")]
		public override async Task<ActionResult> Delete(int id)
		{
			return await base.Delete(id);
		}

		[HttpGet("updateEdsmIds")]
		[Authorize(Policy = "edit:systemdata")]
		public async Task<object> UpdateEdsmIds()
		{
			var bodies = await Repository.GetAllWithSystems();

			var updatedBodies = await _edsmService.FetchBodyIds(bodies.Where(body => body.EdsmExtId == null || body.Distance == null));
			if (updatedBodies.All(us => us.Updated))
			{
				await Repository.SaveChanges();
			}

			foreach (var updatedBody in updatedBodies)
			{
				updatedBody.Body.System.Bodies = null;
			}

			return updatedBodies;
		}
	}
}
