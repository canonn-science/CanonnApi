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
	/// Provides methods for CRUD operations on <see cref="RuinType"/> objects
	/// </summary>
	[Route("v1/ruins/types")]
	public class RuinTypeController : BaseDataController<RuinType, RuinTypeDto>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="RuinTypeController"/> class
		/// </summary>
		/// <param name="logger">An instance of an <see cref="ILogger"/> to log.</param>
		/// <param name="repository">An instance of an <see cref="IRuinTypeRepository"/> to handle database access.</param>
		/// <param name="mapper">An instance of an <see cref="IMapper"/> to automatically map from and to DTOs.</param>
		public RuinTypeController(ILogger<ArtifactsController> logger, IRuinTypeRepository repository, IMapper mapper)
			: base(logger, repository, mapper)
		{
		}

		/// <summary>
		/// Gets an array of ruin type instances from the system
		/// </summary>
		/// <returns>A list of all available ruin types</returns>
		[HttpGet()]
		[SwaggerResponse(200, Type = typeof(RuinTypeDto))] // TODO: When SwaggerUI is updated to 3.x make this return the array again.
		[SwaggerResponse(404, Description = "Not found")]
		public async Task<IEnumerable<RuinTypeDto>> Get()
		{
			return await GetAll();
		}

		/// <summary>
		/// Gets a single ruin type based on its id
		/// </summary>
		/// <param name="id">The id of the ruin type to select</param>
		/// <returns>A representation of the selected ruin type</returns>
		[HttpGet("{id}")]
		[SwaggerResponse(200, Type = typeof(RuinTypeDto))]
		[SwaggerResponse(404, Description = "Not found")]
		public async Task<RuinTypeDto> Get(int id)
		{
			return await GetById(id);
		}

		/// <summary>
		/// Creates a new entry for a ruin type
		/// </summary>
		/// <param name="data">A representation of the ruin type to store</param>
		/// <returns>A representation of the freshly created ruin type</returns>
		[HttpPost()]
		[Authorize(Policy = "add:ruinbasedata")]
		[SwaggerResponse(200, Type = typeof(RuinTypeDto))]
		public async Task<RuinTypeDto> Create([FromBody] RuinTypeDto data)
		{
			return await CreateNew(data);
		}

		/// <summary>
		/// Creates or updates a ruin type with a given id
		/// </summary>
		/// <param name="data">A representation of the ruin type to create or update</param>
		/// <param name="id">The id of the ruin type to update or 0 to create a new one</param>
		/// <returns>A representation of the created or updated ruin type</returns>
		[HttpPut("{id}")]
		[Authorize(Policy = "add:ruinbasedata")]
		[Authorize(Policy = "edit:ruinbasedata")]
		[SwaggerResponse(200, Type = typeof(RuinTypeDto))]
		[SwaggerResponse(409, Description = "Conflict")]
		public async Task<RuinTypeDto> CreateOrUpdate([FromBody] RuinTypeDto data, int id)
		{
			return await CreateNewOrUpdateExisting(data, id);
		}

		/// <summary>
		/// Edits an existing ruin type for a given id
		/// </summary>
		/// <param name="data">A representation of the ruin type to update</param>
		/// <param name="id">The id of the ruin type to update</param>
		/// <returns>A representation of the created or updated ruin type</returns>
		[HttpPatch("{id}")]
		[Authorize(Policy = "edit:ruinbasedata")]
		[SwaggerResponse(200, Type = typeof(RuinTypeDto))]
		[SwaggerResponse(404, Description = "Not found")]
		[SwaggerResponse(409, Description = "Conflict")]
		public async Task<RuinTypeDto> Update([FromBody] RuinTypeDto data, int id)
		{
			return await UpdateExisting(data, id);
		}

		/// <summary>
		/// Deletes an existing ruin type based on its id
		/// </summary>
		/// <param name="id">The id of the ruin type to delete</param>
		/// <returns>200 if deletion was successful; 404 otherwise</returns>
		[HttpDelete("{id}")]
		[Authorize(Policy = "delete:ruinbasedata")]
		[SwaggerResponse(404, Description = "Not found")]
		public async Task<ActionResult> Delete(int id)
		{
			return await DeleteExisting(id);
		}
	}
}
