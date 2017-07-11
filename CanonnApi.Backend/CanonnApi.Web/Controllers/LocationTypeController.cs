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
	/// Provides methods for CRUD operations on <see cref="LocationType"/> objects
	/// </summary>
	[Route("v1/locations/types")]
	public class LocationTypeController : BaseDataController<LocationType, LocationTypeDto>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="LocationTypeController"/> class
		/// </summary>
		/// <param name="logger">An instance of an <see cref="ILogger"/> to log.</param>
		/// <param name="repository">An instance of an <see cref="ILocationTypeRepository"/> to handle database access.</param>
		/// <param name="mapper">An instance of an <see cref="IMapper"/> to automatically map from and to DTOs.</param>
		public LocationTypeController(ILogger<LocationTypeController> logger, ILocationTypeRepository repository, IMapper mapper)
			: base(logger, repository, mapper)
		{
		}

		/// <summary>
		/// Gets an array of location type instances from the system
		/// </summary>
		/// <returns>A list of all available location types</returns>
		[HttpGet()]
		[SwaggerResponse(200, Type = typeof(LocationTypeDto))] // TODO: When SwaggerUI is updated to 3.x make this return the array again.
		[SwaggerResponse(404, Description = "Not found")]
		public async Task<IEnumerable<LocationTypeDto>> Get()
		{
			return await GetAll();
		}

		/// <summary>
		/// Gets a single location type based on its id
		/// </summary>
		/// <param name="id">The id of the location type to select</param>
		/// <returns>A representation of the selected location type</returns>
		[HttpGet("{id}")]
		[SwaggerResponse(200, Type = typeof(LocationTypeDto))]
		[SwaggerResponse(404, Description = "Not found")]
		public async Task<LocationTypeDto> Get(int id)
		{
			return await GetById(id);
		}

		/// <summary>
		/// Creates a new entry for a location type
		/// </summary>
		/// <param name="data">A representation of the location type to store</param>
		/// <returns>A representation of the freshly created location type</returns>
		[HttpPost()]
		[Authorize(Policy = "add:systemdata")]
		[SwaggerResponse(200, Type = typeof(LocationTypeDto))]
		public async Task<LocationTypeDto> Create([FromBody] LocationTypeDto data)
		{
			return await CreateNew(data);
		}

		/// <summary>
		/// Creates or updates a location type with a given id
		/// </summary>
		/// <param name="data">A representation of the location type to create or update</param>
		/// <param name="id">The id of the location type to update or 0 to create a new one</param>
		/// <returns>A representation of the created or updated location type</returns>
		[HttpPut("{id}")]
		[Authorize(Policy = "add:systemdata")]
		[Authorize(Policy = "edit:systemdata")]
		[SwaggerResponse(200, Type = typeof(LocationTypeDto))]
		[SwaggerResponse(409, Description = "Conflict")]
		public async Task<LocationTypeDto> CreateOrUpdate([FromBody] LocationTypeDto data, int id)
		{
			return await CreateNewOrUpdateExisting(data, id);
		}

		/// <summary>
		/// Edits an existing location type for a given id
		/// </summary>
		/// <param name="data">A representation of the location type to update</param>
		/// <param name="id">The id of the location type to update</param>
		/// <returns>A representation of the created or updated location type</returns>
		[HttpPatch("{id}")]
		[Authorize(Policy = "edit:systemdata")]
		[SwaggerResponse(200, Type = typeof(LocationTypeDto))]
		[SwaggerResponse(404, Description = "Not found")]
		[SwaggerResponse(409, Description = "Conflict")]
		public async Task<LocationTypeDto> Update([FromBody] LocationTypeDto data, int id)
		{
			return await UpdateExisting(data, id);
		}

		/// <summary>
		/// Deletes an existing location type based on its id
		/// </summary>
		/// <param name="id">The id of the location type to delete</param>
		/// <returns>200 if deletion was successful; 404 otherwise</returns>
		[HttpDelete("{id}")]
		[Authorize(Policy = "delete:systemdata")]
		[SwaggerResponse(404, Description = "Not found")]
		public async Task<ActionResult> Delete(int id)
		{
			return await DeleteExisting(id);
		}
	}
}
