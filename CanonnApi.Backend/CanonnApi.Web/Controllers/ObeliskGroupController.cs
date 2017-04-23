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
	/// Provides methods for CRUD operations on <see cref="ObeliskGroup"/> objects
	/// </summary>
	[Route("v1/obelisks/groups")]
	public class ObeliskGroupController : BaseDataController<ObeliskGroup, ObeliskGroupDto>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ObeliskGroupController"/> class
		/// </summary>
		/// <param name="logger">An instance of an <see cref="ILogger"/> to log.</param>
		/// <param name="repository">An instance of an <see cref="IObeliskGroupRepository"/> to handle database access.</param>
		/// <param name="mapper">An instance of an <see cref="IMapper"/> to automatically map from and to DTOs.</param>
		public ObeliskGroupController(ILogger<ObeliskGroupController> logger, IObeliskGroupRepository repository,
			IMapper mapper)
			: base(logger, repository, mapper)
		{
		}

		/// <summary>
		/// Gets an array of obelisk group instances from the system
		/// </summary>
		/// <returns>A list of all available oeblisk groups</returns>
		[HttpGet()]
		[SwaggerResponse(200, Type = typeof(ObeliskGroupDto))] // TODO: When SwaggerUI is updated to 3.x make this return the array again.
		[SwaggerResponse(404, Description = "Not found")]
		public async Task<IEnumerable<ObeliskGroupDto>> Get()
		{
			return await GetAll();
		}

		/// <summary>
		/// Gets a single obelisk group based on its id
		/// </summary>
		/// <param name="id">The id of the obelisk group to select</param>
		/// <returns>A representation of the selected obelisk group</returns>
		[HttpGet("{id}")]
		[SwaggerResponse(200, Type = typeof(ObeliskGroupDto))]
		[SwaggerResponse(404, Description = "Not found")]
		public async Task<ObeliskGroupDto> Get(int id)
		{
			return await GetById(id);
		}

		/// <summary>
		/// Creates a new entry for a obelisk group
		/// </summary>
		/// <param name="data">A representation of the obelisk group to store</param>
		/// <returns>A representation of the freshly created obelisk group</returns>
		[HttpPost()]
		[Authorize(Policy = "add:ruinbasedata")]
		[SwaggerResponse(200, Type = typeof(ObeliskGroupDto))]
		public async Task<ObeliskGroupDto> Create([FromBody] ObeliskGroupDto data)
		{
			return await CreateNew(data);
		}

		/// <summary>
		/// Creates or updates a obelisk group with a given id
		/// </summary>
		/// <param name="data">A representation of the obelisk group to create or update</param>
		/// <param name="id">The id of the obelisk group to update or 0 to create a new one</param>
		/// <returns>A representation of the created or updated obelisk group</returns>
		[HttpPut("{id}")]
		[Authorize(Policy = "add:ruinbasedata")]
		[Authorize(Policy = "edit:ruinbasedata")]
		[SwaggerResponse(200, Type = typeof(ObeliskGroupDto))]
		[SwaggerResponse(409, Description = "Conflict")]
		public async Task<ObeliskGroupDto> CreateOrUpdate([FromBody] ObeliskGroupDto data, int id)
		{
			return await CreateNewOrUpdateExisting(data, id);
		}

		/// <summary>
		/// Edits an existing obelisk group entry for a given id
		/// </summary>
		/// <param name="data">A representation of the obelisk group to update</param>
		/// <param name="id">The id of the obelisk group to update</param>
		/// <returns>A representation of the created or updated obelisk group</returns>
		[HttpPatch("{id}")]
		[Authorize(Policy = "edit:ruinbasedata")]
		[SwaggerResponse(200, Type = typeof(ObeliskGroupDto))]
		[SwaggerResponse(404, Description = "Not found")]
		[SwaggerResponse(409, Description = "Conflict")]
		public async Task<ObeliskGroupDto> Update([FromBody] ObeliskGroupDto data, int id)
		{
			return await UpdateExisting(data, id);
		}

		/// <summary>
		/// Deletes an existing obelisk group based on its id
		/// </summary>
		/// <param name="id">The id of the obelisk group to delete</param>
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
