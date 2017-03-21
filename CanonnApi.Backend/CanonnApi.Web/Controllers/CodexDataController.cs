using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CanonnApi.Web.DatabaseModels;
using CanonnApi.Web.Services.DataAccess;

namespace CanonnApi.Web.Controllers
{
	[Route("v1/codex/data")]
	public class CodexDataController : BaseDataController<CodexData>
	{
		public CodexDataController(ILogger<CodexDataController> logger, ICodexDataRepository repository)
			: base(logger, repository)
		{
		}

		[HttpPut("{id}")]
		[Authorize(Policy = "add:codexdata")]
		[Authorize(Policy = "edit:codexdata")]
		public override async Task<CodexData> CreateOrUpdate([FromBody] CodexData data, int id)
		{
			return await base.CreateOrUpdate(data, id);
		}

		[HttpPatch("{id}")]
		[Authorize(Policy = "edit:codexdata")]
		public override async Task<CodexData> Update([FromBody] CodexData data, int id)
		{
			return await base.Update(data, id);
		}

		[HttpDelete("{id}")]
		[Authorize(Policy = "delete:codexdata")]
		public override async Task<ActionResult> Delete(int id)
		{
			return await base.Delete(id);
		}
	}
}
