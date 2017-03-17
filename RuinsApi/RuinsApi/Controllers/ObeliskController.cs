using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RuinsApi.DatabaseModels;
using RuinsApi.Services.DataAccess;

namespace RuinsApi.Controllers
{
	[Route("v1/obelisks")]
	public class ObeliskController : BaseDataController<Obelisk>
	{
		protected new IObeliskRepository Repository => (IObeliskRepository) base.Repository;

		public ObeliskController(ILogger<ObeliskController> logger, IObeliskRepository repository)
			: base(logger, repository)
		{
		}

		[HttpGet("search")]
		public async Task<List<Obelisk>> Search(int ruintypeId = 0, int obeliskgroupId = 0)
		{
			return await Repository.Search(ruintypeId, obeliskgroupId);
		}

		[HttpPut("{id}")]
		[Authorize(Policy = "add:ruinbasedata")]
		[Authorize(Policy = "edit:ruinbasedata")]
		public override async Task<Obelisk> CreateOrUpdate([FromBody] Obelisk data, int id)
		{
			return await base.CreateOrUpdate(data, id);
		}

		[HttpPatch("{id}")]
		[Authorize(Policy = "edit:ruinbasedata")]
		public override async Task<Obelisk> Update([FromBody] Obelisk data, int id)
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
