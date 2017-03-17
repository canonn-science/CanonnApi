using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RuinsApi.DatabaseModels;
using RuinsApi.Services.DataAccess;

namespace RuinsApi.Controllers
{
	[Route("v1/ruins/types")]
	public class RuinsController : BaseDataController<RuinType>
	{
		public RuinsController(ILogger<ArtifactsController> logger, IRuinTypeRepository repository)
			: base(logger, repository)
		{
		}

		[HttpPut("{id}")]
		[Authorize(Policy = "add:ruinbasedata")]
		[Authorize(Policy = "edit:ruinbasedata")]
		public override async Task<RuinType> CreateOrUpdate([FromBody] RuinType data, int id)
		{
			return await base.CreateOrUpdate(data, id);
		}

		[HttpPatch("{id}")]
		[Authorize(Policy = "edit:ruinbasedata")]
		public override async Task<RuinType> Update([FromBody] RuinType data, int id)
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
