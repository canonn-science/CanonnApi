using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RuinsApi.DatabaseModels;
using RuinsApi.Services.DataAccess;

namespace RuinsApi.Controllers
{
	[Route("v1/artifacts")]
	public class ArtifactsController : BaseDataController<Artifact>
	{
		public ArtifactsController(ILogger<ArtifactsController> logger, IArtifactRepository repository)
			:base(logger, repository)
		{
		}

		[HttpPut("{id}")]
		[Authorize(Policy = "add:codexdata")]
		[Authorize(Policy = "edit:codexdata")]
		public override async Task<Artifact> CreateOrUpdate([FromBody] Artifact data, int id)
		{
			return await base.CreateOrUpdate(data, id);
		}

		[HttpPatch("{id}")]
		[Authorize(Policy = "edit:codexdata")]
		public override async Task<Artifact> Update([FromBody] Artifact data, int id)
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
