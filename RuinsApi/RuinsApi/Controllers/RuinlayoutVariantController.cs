using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RuinsApi.DatabaseModels;
using RuinsApi.Services.DataAccess;

namespace RuinsApi.Controllers
{
	[Route("v1/ruinlayouts/variants")]
	public class RuinlayoutVariantController : BaseDataController<RuinlayoutVariant>
	{
		public RuinlayoutVariantController(ILogger<RuinlayoutVariantController> logger, IRuinlayoutVariantRepository repository)
			: base(logger, repository)
		{
		}

		[HttpPut("{id}")]
		[Authorize(Policy = "add:ruinlayoutvariant")]
		[Authorize(Policy = "edit:ruinlayoutvariant")]
		public override async Task<RuinlayoutVariant> CreateOrUpdate([FromBody] RuinlayoutVariant data, int id)
		{
			return await base.CreateOrUpdate(data, id);
		}

		[HttpPatch("{id}")]
		[Authorize(Policy = "edit:ruinlayoutvariant")]
		public override async Task<RuinlayoutVariant> Update([FromBody] RuinlayoutVariant data, int id)
		{
			return await base.Update(data, id);
		}

		[HttpDelete("{id}")]
		[Authorize(Policy = "delete:ruinlayoutvariant")]
		public override async Task<ActionResult> Delete(int id)
		{
			return await base.Delete(id);
		}
	}
}
