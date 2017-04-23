using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Swashbuckle.AspNetCore.SwaggerGen;
using CanonnApi.Web.Controllers.Models;
using CanonnApi.Web.DatabaseModels;
using CanonnApi.Web.Services.DataAccess;

namespace CanonnApi.Web.Controllers
{
	/// <summary>
	/// Provides methods for CRUD operations on <see cref="Artifact"/> objects
	/// </summary>
	[Route("v1/artifacts")]
	public class ArtifactsController : BaseDataController<Artifact, ArtifactDto>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ArtifactsController"/> class
		/// </summary>
		/// <param name="logger">An instance of an <see cref="ILogger"/> to log.</param>
		/// <param name="repository">An instance of an <see cref="IArtifactRepository"/> to handle database access.</param>
		/// <param name="mapper">An instance of an <see cref="IMapper"/> to automatically map from and to DTOs.</param>
		public ArtifactsController(ILogger<ArtifactsController> logger, IArtifactRepository repository, IMapper mapper)
			: base(logger, repository, mapper)
		{
		}

		/// <summary>
		/// Gets an array of artifact instances from the system
		/// </summary>
		/// <returns>A list of all available artifacts</returns>
		[HttpGet()]
		[SwaggerResponse(200, Type = typeof(ArtifactDto))] // TODO: When SwaggerUI is updated to 3.x make this return the array again.
		[SwaggerResponse(404, Description = "Not found")]
		public async Task<IEnumerable<ArtifactDto>> Get()
		{
			return await GetAll();
		}

		/// <summary>
		/// Gets a single artifact based on its id
		/// </summary>
		/// <param name="id">The id of the artifact to select</param>
		/// <returns>A representation of the selected artifact</returns>
		[HttpGet("{id}")]
		[SwaggerResponse(200, Type = typeof(ArtifactDto))]
		[SwaggerResponse(404, Description = "Not found")]
		public async Task<ArtifactDto> Get(int id)
		{
			return await GetById(id);
		}

		/// <summary>
		/// Creates a new entry for an artifact
		/// </summary>
		/// <param name="data">A representation of the artifact to store</param>
		/// <returns>A representation of the freshly created artifact</returns>
		[HttpPost()]
		[Authorize(Policy = "add:codexdata")]
		[SwaggerResponse(200, Type = typeof(ArtifactDto))]
		public async Task<ArtifactDto> Create([FromBody] ArtifactDto data)
		{
			return await CreateNew(data);
		}

		/// <summary>
		/// Creates or updates an artifact with a given id
		/// </summary>
		/// <param name="data">A representation of the artifact to create or update</param>
		/// <param name="id">The id of the artifact to update or 0 to create a new one</param>
		/// <returns>A representation of the created or updated artifact</returns>
		[HttpPut("{id}")]
		[Authorize(Policy = "add:codexdata")]
		[Authorize(Policy = "edit:codexdata")]
		[SwaggerResponse(200, Type = typeof(ArtifactDto))]
		[SwaggerResponse(409, Description = "Conflict")]
		public async Task<ArtifactDto> CreateOrUpdate([FromBody] ArtifactDto data, int id)
		{
			return await CreateNewOrUpdateExisting(data, id);
		}

		/// <summary>
		/// Edits an existing artifact entry for a given id
		/// </summary>
		/// <param name="data">A representation of the artifact to update</param>
		/// <param name="id">The id of the artifact to update</param>
		/// <returns>A representation of the created or updated artifact</returns>
		[HttpPatch("{id}")]
		[Authorize(Policy = "edit:codexdata")]
		[SwaggerResponse(200, Type = typeof(ArtifactDto))]
		[SwaggerResponse(404, Description = "Not found")]
		[SwaggerResponse(409, Description = "Conflict")]
		public async Task<ArtifactDto> Update([FromBody] ArtifactDto data, int id)
		{
			return await UpdateExisting(data, id);
		}

		/// <summary>
		/// Deletes an existing artifact based on its id
		/// </summary>
		/// <param name="id">The id of the artifact to delete</param>
		/// <returns>200 if deletion was successful; 404 otherwise</returns>
		[HttpDelete("{id}")]
		[Authorize(Policy = "delete:codexdata")]
		[SwaggerResponse(404, Description = "Not found")]
		public async Task<ActionResult> Delete(int id)
		{
			return await DeleteExisting(id);
		}
	}
}
