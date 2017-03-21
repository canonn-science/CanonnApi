using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CanonnApi.Web.Services.DataAccess;

namespace CanonnApi.Web.Controllers
{
	[Route("v1/stellar/systems")]
	public class SystemController : BaseDataController<DatabaseModels.System>
	{
		public SystemController(ILogger<SystemController> logger, ISystemRepository repository)
			:base(logger, repository)
		{
		}

		[HttpPut("{id}")]
		[Authorize(Policy = "add:systemdata")]
		[Authorize(Policy = "edit:systemdata")]
		public override async Task<DatabaseModels.System> CreateOrUpdate([FromBody] DatabaseModels.System data, int id)
		{
			return await base.CreateOrUpdate(data, id);
		}

		[HttpPatch("{id}")]
		[Authorize(Policy = "edit:systemdata")]
		public override async Task<DatabaseModels.System> Update([FromBody] DatabaseModels.System data, int id)
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
