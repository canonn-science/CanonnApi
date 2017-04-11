using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CanonnApi.Web.DatabaseModels;
using CanonnApi.Web.Services.DataAccess;

namespace CanonnApi.Web.Controllers
{
	[Route("v1/codex/categories")]
	public class CodexCategoryController : BaseDataController<CodexCategory>
	{
		public CodexCategoryController(ILogger<CodexDataController> logger, ICodexCategoryRepository repository)
			:base(logger, repository)
		{
		}

		[HttpPost()]
		[Authorize(Policy = "add:codexdata")]
		public override async Task<CodexCategory> Create([FromBody] CodexCategory data)
		{
			return await base.Create(data);
		}

		[HttpPut("{id}")]
		[Authorize(Policy = "add:codexdata")]
		[Authorize(Policy = "edit:codexdata")]
		public override async Task<CodexCategory> CreateOrUpdate([FromBody] CodexCategory data, int id)
		{
			return await base.CreateOrUpdate(data, id);
		}

		[HttpPatch("{id}")]
		[Authorize(Policy = "edit:codexdata")]
		public override async Task<CodexCategory> Update([FromBody] CodexCategory data, int id)
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
