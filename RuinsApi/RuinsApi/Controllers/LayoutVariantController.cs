using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RuinsApi.DatabaseModels;
using RuinsApi.Services.DataAccess;

namespace RuinsApi.Controllers
{
	[Route("v1/layout/variants")]
	public class LayoutVariantController : BaseDataController<LayoutVariant>
	{
		public LayoutVariantController(ILogger<LayoutVariantController> logger, ILayoutVariantRepository repository)
				: base(logger, repository)
		{
		}

		[HttpPut("{id}")]
		[Authorize(Policy = "add:layoutvariant")]
		[Authorize(Policy = "edit:layoutvariant")]
		public override async Task<LayoutVariant> CreateOrUpdate([FromBody] LayoutVariant data, int id)
		{
			return await base.CreateOrUpdate(data, id);
		}

		[HttpPatch("{id}")]
		[Authorize(Policy = "edit:layoutvariant")]
		public override async Task<LayoutVariant> Update([FromBody] LayoutVariant data, int id)
		{
			return await base.Update(data, id);
		}

		[HttpDelete("{id}")]
		[Authorize(Policy = "delete:layoutvariant")]
		public override async Task<ActionResult> Delete(int id)
		{
			return await base.Delete(id);
		}
	}
}
