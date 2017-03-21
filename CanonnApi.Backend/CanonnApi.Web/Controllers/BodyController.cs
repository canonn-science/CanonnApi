using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CanonnApi.Web.DatabaseModels;
using CanonnApi.Web.Services.DataAccess;

namespace CanonnApi.Web.Controllers
{
	[Route("v1/stellar/bodies")]
	public class BodyController : BaseDataController<Body>
	{
		public BodyController(ILogger<BodyController> logger, IBodyRepository repository)
			:base(logger, repository)
		{
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
	}
}
