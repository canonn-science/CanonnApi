using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RuinsApi.DatabaseModels;
using RuinsApi.Services.DataAccess;

namespace RuinsApi.Controllers
{
	[Route("v1/ruins/layouts")]
	public class RuinLayoutController : BaseDataController<RuinLayout>
	{
		public RuinLayoutController(ILogger<RelictsController> logger, IRuinLayoutRepository repository)
			: base(logger, repository)
		{
		}

		[HttpPut("{id}")]
		[Authorize(Policy = "add:ruinlayout")]
		[Authorize(Policy = "edit:ruinlayout")]
		public override async Task<RuinLayout> CreateOrUpdate([FromBody] RuinLayout data, int id)
		{
			return await base.CreateOrUpdate(data, id);
		}

		[HttpPatch("{id}")]
		[Authorize(Policy = "edit:ruinlayout")]
		public override async Task<RuinLayout> Update([FromBody] RuinLayout data, int id)
		{
			return await base.Update(data, id);
		}

		[HttpDelete("{id}")]
		[Authorize(Policy = "delete:ruinlayout")]
		public override async Task<ActionResult> Delete(int id)
		{
			return await base.Delete(id);
		}
	}
}
