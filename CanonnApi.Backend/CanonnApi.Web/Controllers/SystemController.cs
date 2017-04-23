using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AutoMapper;
using CanonnApi.Web.Controllers.Models;
using CanonnApi.Web.Services.DataAccess;
using CanonnApi.Web.Services.RemoteApis;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CanonnApi.Web.Controllers
{
	/// <summary>
	/// Provides methods for CRUD operations on <see cref="DatabaseModels.System"/> objects
	/// </summary>
	[Route("v1/stellar/systems")]
	public class SystemController : BaseDataController<DatabaseModels.System, SystemDto>
	{
		private readonly IEdsmService _edsmService;

		/// <summary>
		/// Gets the <see cref="ISystemRepository"/>
		/// </summary>
		protected new ISystemRepository Repository => (ISystemRepository)base.Repository;

		/// <summary>
		/// Initializes a new instance of the <see cref="ObeliskController"/> class
		/// </summary>
		/// <param name="logger">An instance of an <see cref="ILogger"/> to log.</param>
		/// <param name="repository">An instance of an <see cref="IObeliskRepository"/> to handle database access.</param>
		/// <param name="mapper">An instance of an <see cref="IMapper"/> to automatically map from and to DTOs.</param>
		/// /// <param name="edsmService">An instance of an <see cref="IEdsmService"/> to retrieve external id's and distances from</param>
		public SystemController(ILogger<SystemController> logger, ISystemRepository repository, IMapper mapper, IEdsmService edsmService)
			: base(logger, repository, mapper)
		{
			_edsmService = edsmService ?? throw new ArgumentNullException(nameof(edsmService));
		}

		/// <summary>
		/// Gets an array of system instances from the system
		/// </summary>
		/// <returns>A list of all available systems</returns>
		[HttpGet()]
		[SwaggerResponse(200, Type = typeof(SystemDto))] // TODO: When SwaggerUI is updated to 3.x make this return the array again.
		[SwaggerResponse(404, Description = "Not found")]
		public async Task<IEnumerable<SystemDto>> Get()
		{
			return await GetAll();
		}

		/// <summary>
		/// Gets a single system based on its id
		/// </summary>
		/// <param name="id">The id of the system to select</param>
		/// <returns>A representation of the selected system</returns>
		[HttpGet("{id}")]
		[SwaggerResponse(200, Type = typeof(SystemDto))]
		[SwaggerResponse(404, Description = "Not found")]
		public async Task<SystemDto> Get(int id)
		{
			return await GetById(id);
		}

		/// <summary>
		/// Creates a new entry for a system
		/// </summary>
		/// <param name="data">A representation of the system to store</param>
		/// <returns>A representation of the freshly created system</returns>
		[HttpPost()]
		[Authorize(Policy = "add:systemdata")]
		[SwaggerResponse(200, Type = typeof(SystemDto))]
		public async Task<SystemDto> Create([FromBody] SystemDto data)
		{
			return await CreateNew(data);
		}

		/// <summary>
		/// Creates or updates a system with a given id
		/// </summary>
		/// <param name="data">A representation of the system create or update</param>
		/// <param name="id">The id of the system to update or 0 to create a new one</param>
		/// <returns>A representation of the created or updated system</returns>
		[HttpPut("{id}")]
		[Authorize(Policy = "add:systemdata")]
		[Authorize(Policy = "edit:systemdata")]
		[SwaggerResponse(200, Type = typeof(SystemDto))]
		[SwaggerResponse(409, Description = "Conflict")]
		public async Task<SystemDto> CreateOrUpdate([FromBody] SystemDto data, int id)
		{
			return await CreateNewOrUpdateExisting(data, id);
		}

		/// <summary>
		/// Edits an existing system entry for a given id
		/// </summary>
		/// <param name="data">A representation of the system to update</param>
		/// <param name="id">The id of the system to update</param>
		/// <returns>A representation of the created or updated system</returns>
		[HttpPatch("{id}")]
		[Authorize(Policy = "edit:systemdata")]
		[SwaggerResponse(200, Type = typeof(SystemDto))]
		[SwaggerResponse(404, Description = "Not found")]
		[SwaggerResponse(409, Description = "Conflict")]
		public async Task<SystemDto> Update([FromBody] SystemDto data, int id)
		{
			return await UpdateExisting(data, id);
		}

		/// <summary>
		/// Deletes an existing system based on its id
		/// </summary>
		/// <param name="id">The id of the system to delete</param>
		/// <returns>200 if deletion was successful; 404 otherwise</returns>
		[HttpDelete("{id}")]
		[Authorize(Policy = "delete:systemdata")]
		[SwaggerResponse(404, Description = "Not found")]
		public async Task<ActionResult> Delete(int id)
		{
			return await DeleteExisting(id);
		}

		/// <summary>
		/// Fetches external system ids and galactic coordinates from EDSM
		/// </summary>
		/// <returns>A list of all the updated systems</returns>
		[HttpGet("updateEdsmIds")]
		[Authorize(Policy = "edit:systemdata")]
		[SwaggerResponse(200, Type = typeof(EdsmUpdatedSystem))] // TODO: When SwaggerUI is updated to 3.x make this return the array again.
		public async Task<List<EdsmUpdatedSystem>> UpdateEdsmIds()
		{
			var systems = await Repository.GetAll();

			DateTime maxCoordAge = DateTime.UtcNow - TimeSpan.FromDays(3);
			var updatedSystems = await _edsmService.FetchSystemIds(systems.Where(sys => sys.EdsmExtId == null || sys.EdsmCoordUpdated == null || sys.EdsmCoordUpdated <= maxCoordAge));
			await Repository.SaveChanges();

			var result = new List<EdsmUpdatedSystem>();

			foreach (var updatedSystem in updatedSystems)
			{
				var edsmUpdatedSystem = Mapper.Map<DatabaseModels.System, EdsmUpdatedSystem>(updatedSystem.System);
				edsmUpdatedSystem.IsUpdated = updatedSystem.Updated;
				edsmUpdatedSystem.ErrorMessage = updatedSystem.ErrorMessage;

				result.Add(edsmUpdatedSystem);
			}

			return result;
		}
	}
}
