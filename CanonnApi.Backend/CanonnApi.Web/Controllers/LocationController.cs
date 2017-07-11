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
	/// Provides methods for CRUD operations on <see cref="Location"/> objects
	/// </summary>
	[Route("v1/locations")]
	public class LocationController : BaseDataController<Location, LocationDto>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="LocationController"/> class
		/// </summary>
		/// <param name="logger">An instance of an <see cref="ILogger"/> to log.</param>
		/// <param name="repository">An instance of an <see cref="ILocationRepository"/> to handle database access.</param>
		/// <param name="mapper">An instance of an <see cref="IMapper"/> to automatically map from and to DTOs.</param>
		public LocationController(ILogger<LocationController> logger, ILocationRepository repository, IMapper mapper)
			: base(logger, repository, mapper)
		{
		}

		/// <summary>
		/// Gets an array of location instances from the system
		/// </summary>
		/// <returns>A list of all available locations</returns>
		[HttpGet()]
		[SwaggerResponse(200,  Type = typeof(LocationDto))] // TODO: When SwaggerUI is updated to 3.x make this return the array again.
		[SwaggerResponse(404, Description = "Not found")]
		public async Task<IEnumerable<LocationDto>> Get()
		{
			return await GetAll();
		}

		/// <summary>
		/// Gets a single location based on its id
		/// </summary>
		/// <param name="id">The id of the location to select</param>
		/// <returns>A representation of the selected location</returns>
		[HttpGet("{id}")]
		[SwaggerResponse(200, Type = typeof(LocationDto))]
		[SwaggerResponse(404, Description = "Not found")]
		public async Task<LocationDto> Get(int id)
		{
			return await GetById(id);
		}

		/// <summary>
		/// Creates a new entry for a location
		/// </summary>
		/// <param name="data">A representation of the location to store</param>
		/// <returns>A representation of the freshly created location</returns>
		[HttpPost()]
		[Authorize(Policy = "add:systemdata")]
		[SwaggerResponse(200, Type = typeof(LocationDto))]
		public async Task<LocationDto> Create([FromBody] LocationDto data)
		{
			return await CreateNew(data);
		}

		/// <summary>
		/// Creates or updates a location with a given id
		/// </summary>
		/// <param name="data">A representation of the location to create or update</param>
		/// <param name="id">The id of the location to update or 0 to create a new one</param>
		/// <returns>A representation of the created or updated location</returns>
		[HttpPut("{id}")]
		[Authorize(Policy = "add:systemdata")]
		[Authorize(Policy = "edit:systemdata")]
		[SwaggerResponse(200, Type = typeof(LocationDto))]
		[SwaggerResponse(409, Description = "Conflict")]
		public async Task<LocationDto> CreateOrUpdate([FromBody] LocationDto data, int id)
		{
			return await CreateNewOrUpdateExisting(data, id);
		}

		/// <summary>
		/// Edits an existing location for a given id
		/// </summary>
		/// <param name="data">A representation of the location to update</param>
		/// <param name="id">The id of the location to update</param>
		/// <returns>A representation of the created or updated location</returns>
		[HttpPatch("{id}")]
		[Authorize(Policy = "edit:systemdata")]
		[SwaggerResponse(200, Type = typeof(LocationDto))]
		[SwaggerResponse(404, Description = "Not found")]
		[SwaggerResponse(409, Description = "Conflict")]
		public async Task<LocationDto> Update([FromBody] LocationDto data, int id)
		{
			return await UpdateExisting(data, id);
		}

		/// <summary>
		/// Deletes an existing location based on its id
		/// </summary>
		/// <param name="id">The id of the location to delete</param>
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
