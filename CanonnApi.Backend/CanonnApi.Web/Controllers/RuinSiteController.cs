using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CanonnApi.Web.DatabaseModels;
using CanonnApi.Web.Middlewares;
using CanonnApi.Web.Services.RuinSites;

namespace CanonnApi.Web.Controllers
{
	[Route("v1/ruinsites")]
	public class RuinSiteController : Controller
	{
		protected IRuinSiteRepository Repository { get; set; }
		protected ILogger Logger { get; set; }

		public RuinSiteController(ILogger<RuinSiteController> logger, IRuinSiteRepository repository)
		{
			Repository = repository ?? throw new ArgumentNullException(nameof(repository));

			Logger = logger;
		}

		[HttpGet]
		public virtual async Task<List<RuinSite>> Get()
		{
			return await Repository.GetAll();
		}

		[HttpGet("{id}")]
		public virtual async Task<RuinSite> Get(int id)
		{
			var entry = await Repository.GetById(id);
			if (entry == null)
				throw new HttpNotFoundException();

			return entry;
		}

		[HttpGet("edit/{id}")]
		public virtual async Task<RuinSiteWithObeliskData> GetForEditor(int id)
		{
			var result = await Repository.GetForSiteEditor(id);

			if (result == null)
				throw new HttpNotFoundException();

			return result;
		}

		[HttpPost("edit/{id}")]
		[HttpPut("edit/{id}")]
		[Authorize(Policy = "add:ruinsitedata")]
		[Authorize(Policy = "edit:ruinsitedata")]
		public virtual async Task<RuinSiteWithObeliskData> SaveFromEditor([FromBody] RuinSiteWithObeliskData data)
		{
			return await Repository.SaveFromEditor(data);
		}

		[HttpPost()]
		[Authorize(Policy = "add:ruinsitedata")]
		public virtual async Task<RuinSite> Create([FromBody] RuinSite data)
		{
			return await Repository.Create(data);
		}

		[HttpPut("{id}")]
		[Authorize(Policy = "add:ruinsitedata")]
		[Authorize(Policy = "edit:ruinsitedata")]
		public virtual async Task<RuinSite> CreateOrUpdate([FromBody] RuinSite data, int id)
		{
			try
			{
				return await Repository.CreateOrUpdateById(id, data);
			}
			catch (Exception e)
			{
				throw new HttpException(HttpStatusCode.Conflict, "Conflict", e);
			}
		}

		[HttpPatch("{id}")]
		[Authorize(Policy = "edit:ruinsitedata")]
		public virtual async Task<RuinSite> Update([FromBody] RuinSite data, int id)
		{
			try
			{
				return await Repository.Update(id, data);
			}
			catch (Exception e)
			{
				throw new HttpException(HttpStatusCode.Conflict, "Conflict", e);
			}
		}

		[HttpDelete("{id}")]
		[Authorize(Policy = "delete:ruinsitedata")]
		public virtual async Task<ActionResult> Delete(int id)
		{
			return (await Repository.DeleteById(id))
				? (ActionResult)Ok()
				: NotFound();
		}

		////////////////////////////////////////////////////////////////////////////////////


		[HttpGet("{id}/obeliskgroups")]
		public async Task<List<ObeliskGroupWithActiveState>> GetObeliskGroups(int id)
		{
			return await Repository.LoadActiveObeliskGroupsForSite(id);
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

		[HttpGet("searchdata/{categoryName}/{entryNumber}")]
		public async Task<List<RuinSite>> SearchForDataEntries(string categoryName, int entryNumber)
		{
			return await Repository.SearchSitesForData(categoryName, entryNumber);
		}
	}
}
