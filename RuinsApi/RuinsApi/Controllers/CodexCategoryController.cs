using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RuinsApi.DatabaseModels;
using RuinsApi.Services.DataAccess;

namespace RuinsApi.Controllers
{
	[Route("v1/codex/categories")]
	public class CodexCategoryController : BaseDataController<CodexCategory>
	{
		public CodexCategoryController(ILogger<CodexDataController> logger, ICodexCategoryRepository repository)
			:base(logger, repository)
		{
		}

		[HttpPost()]
		[Authorize(Policy = "add:codexcategory")]
		public override async Task<CodexCategory> Create([FromBody] CodexCategory data)
		{
			return await base.Create(data);
		}

		[HttpPut("{id}")]
		[Authorize(Policy = "add:codexcategory")]
		[Authorize(Policy = "edit:codexcategory")]
		public override async Task<CodexCategory> CreateOrUpdate([FromBody] CodexCategory data, int id)
		{
			return await base.CreateOrUpdate(data, id);
		}

		[HttpPatch("{id}")]
		[Authorize(Policy = "edit:codexcategory")]
		public override async Task<CodexCategory> Update([FromBody] CodexCategory data, int id)
		{
			return await base.Update(data, id);
		}

		[HttpDelete("{id}")]
		[Authorize(Policy = "delete:codexcategory")]
		public override async Task<ActionResult> Delete(int id)
		{
			return await base.Delete(id);
		}
	}
}
