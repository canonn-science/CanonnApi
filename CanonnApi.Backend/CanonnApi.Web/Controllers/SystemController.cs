using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CanonnApi.Web.Services.DataAccess;
using CanonnApi.Web.Services.RemoteApis;

namespace CanonnApi.Web.Controllers
{
	[Route("v1/stellar/systems")]
	public class SystemController : BaseDataController<DatabaseModels.System>
	{
		private readonly IEdsmService _edsmService;
		protected new ISystemRepository Repository => (ISystemRepository)base.Repository;

		public SystemController(ILogger<SystemController> logger, ISystemRepository repository, IEdsmService edsmService)
			:base(logger, repository)
		{
			_edsmService = edsmService ?? throw new ArgumentNullException(nameof(edsmService));
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

		[HttpGet("updateEdsmIds")]
		[Authorize(Policy = "edit:systemdata")]
		public async Task<object> UpdateEdsmIds()
		{
			var systems = await Repository.GetAll();

			DateTime maxCoordAge = DateTime.UtcNow - TimeSpan.FromDays(3);
			var updatedSystems = await _edsmService.FetchSystemIds(systems.Where(sys => sys.EdsmExtId == null || sys.EdsmCoordUpdated == null || sys.EdsmCoordUpdated <= maxCoordAge));
			await Repository.SaveChanges();

			return updatedSystems;
		}
	}
}
