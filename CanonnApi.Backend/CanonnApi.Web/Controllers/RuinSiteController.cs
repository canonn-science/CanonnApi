using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CanonnApi.Web.DatabaseModels;
using CanonnApi.Web.Services.DataAccess;

namespace CanonnApi.Web.Controllers
{
	[Route("v1/ruinsites")]
	public class RuinSiteController : BaseDataController<RuinSite>
	{
		protected new IRuinSiteRepository Repository => (IRuinSiteRepository)base.Repository;

		public RuinSiteController(ILogger<RuinSiteController> logger, IRuinSiteRepository repository)
			: base(logger, repository)
		{
		}

		[HttpPut("{id}")]
		[Authorize(Policy = "add:ruinsitedata")]
		[Authorize(Policy = "edit:ruinsitedata")]
		public override async Task<RuinSite> CreateOrUpdate([FromBody] RuinSite data, int id)
		{
			return await base.CreateOrUpdate(data, id);
		}

		[HttpPatch("{id}")]
		[Authorize(Policy = "edit:ruinsitedata")]
		public override async Task<RuinSite> Update([FromBody] RuinSite data, int id)
		{
			return await base.Update(data, id);
		}

		[HttpDelete("{id}")]
		[Authorize(Policy = "delete:ruinsitedata")]
		public override async Task<ActionResult> Delete(int id)
		{
			return await base.Delete(id);
		}

		[HttpGet("{id}/obeliskgroups")]
		public async Task<List<ObeliskGroup>> GetObeliskGroups(int id)
		{
			return await Repository.LoadObeliskGroupsForSite(id);
		}

		[HttpPut("{id}/obeliskgroups")]
		[HttpPatch("{id}/obeliskgroups")]
		[Authorize(Policy = "edit:ruinsitedata")]
		public async Task<ActionResult> SaveObeliskGroupsForSite(int id, [FromBody] ObeliskGroup[] obeliskGroups)
		{
			return (await Repository.SaveObeliskGroupsForSite(id, obeliskGroups))
				? (ActionResult)Ok()
				: NotFound();
		}

		[HttpGet("{id}/activeobelisks")]
		public async Task<List<Obelisk>> GetActiveObelisks(int id)
		{
			return await Repository.LoadActiveObelisksForSite(id);
		}

		[HttpPut("{id}/activeobelisks")]
		[HttpPatch("{id}/activeobelisks")]
		[Authorize(Policy = "edit:ruinsitedata")]
		public async Task<ActionResult> SaveActiveObelisksForSite(int id, [FromBody] Obelisk[] obelisks)
		{
			return (await Repository.SaveActiveObelisksForSite(id, obelisks))
				? (ActionResult)Ok()
				: NotFound();
		}
	}
}
