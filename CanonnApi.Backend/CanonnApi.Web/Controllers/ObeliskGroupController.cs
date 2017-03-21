using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CanonnApi.Web.DatabaseModels;
using CanonnApi.Web.Services.DataAccess;

namespace CanonnApi.Web.Controllers
{
	[Route("v1/obelisks/groups")]
	public class ObeliskGroupController : BaseDataController<ObeliskGroup>
	{
		public ObeliskGroupController(ILogger<ArtifactsController> logger, IObeliskGroupRepository repository)
			: base(logger, repository)
		{
		}

		[HttpPut("{id}")]
		[Authorize(Policy = "add:ruinbasedata")]
		[Authorize(Policy = "edit:ruinbasedata")]
		public override async Task<ObeliskGroup> CreateOrUpdate([FromBody] ObeliskGroup data, int id)
		{
			return await base.CreateOrUpdate(data, id);
		}

		[HttpPatch("{id}")]
		[Authorize(Policy = "edit:ruinbasedata")]
		public override async Task<ObeliskGroup> Update([FromBody] ObeliskGroup data, int id)
		{
			return await base.Update(data, id);
		}

		[HttpDelete("{id}")]
		[Authorize(Policy = "delete:ruinbasedata")]
		public override async Task<ActionResult> Delete(int id)
		{
			return await base.Delete(id);
		}
	}
}
