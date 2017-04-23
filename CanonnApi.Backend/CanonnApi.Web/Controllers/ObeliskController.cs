using System.Collections.Generic;
using System.Linq;
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
	/// Provides methods for CRUD operations on <see cref="Obelisk"/> objects
	/// </summary>
	[Route("v1/obelisks")]
	public class ObeliskController : BaseDataController<Obelisk, ObeliskDto>
	{
		/// <summary>
		/// Gets the <see cref="IObeliskRepository"/>
		/// </summary>
		protected new IObeliskRepository Repository => (IObeliskRepository)base.Repository;

		/// <summary>
		/// Initializes a new instance of the <see cref="ObeliskController"/> class
		/// </summary>
		/// <param name="logger">An instance of an <see cref="ILogger"/> to log.</param>
		/// <param name="repository">An instance of an <see cref="IObeliskRepository"/> to handle database access.</param>
		/// <param name="mapper">An instance of an <see cref="IMapper"/> to automatically map from and to DTOs.</param>
		public ObeliskController(ILogger<ObeliskController> logger, IObeliskRepository repository, IMapper mapper)
			: base(logger, repository, mapper)
		{
		}

		/// <summary>
		/// Gets an array of obelisk instances from the system
		/// </summary>
		/// <returns>A list of all available obelisks</returns>
		[HttpGet()]
		[SwaggerResponse(200, Type = typeof(ObeliskDto))] // TODO: When SwaggerUI is updated to 3.x make this return the array again.
		[SwaggerResponse(404, Description = "Not found")]
		public async Task<IEnumerable<ObeliskDto>> Get()
		{
			return await GetAll();
		}

		/// <summary>
		/// Gets a single obelisk based on its id
		/// </summary>
		/// <param name="id">The id of the obelisk to select</param>
		/// <returns>A representation of the selected obelisk</returns>
		[HttpGet("{id}")]
		[SwaggerResponse(200, Type = typeof(ObeliskDto))]
		[SwaggerResponse(404, Description = "Not found")]
		public async Task<ObeliskDto> Get(int id)
		{
			return await GetById(id);
		}

		/// <summary>
		/// Creates a new entry for a obelisk
		/// </summary>
		/// <param name="data">A representation of the obelisk to store</param>
		/// <returns>A representation of the freshly created obelisk</returns>
		[HttpPost()]
		[Authorize(Policy = "add:ruinbasedata")]
		[SwaggerResponse(200, Type = typeof(ObeliskDto))]
		public async Task<ObeliskDto> Create([FromBody] ObeliskDto data)
		{
			return await CreateNew(data);
		}

		/// <summary>
		/// Creates or updates a obelisk with a given id
		/// </summary>
		/// <param name="data">A representation of the obelisk to create or update</param>
		/// <param name="id">The id of the obelisk to update or 0 to create a new one</param>
		/// <returns>A representation of the created or updated obelisk</returns>
		[HttpPut("{id}")]
		[Authorize(Policy = "add:ruinbasedata")]
		[Authorize(Policy = "edit:ruinbasedata")]
		[SwaggerResponse(200, Type = typeof(ObeliskDto))]
		[SwaggerResponse(409, Description = "Conflict")]
		public async Task<ObeliskDto> CreateOrUpdate([FromBody] ObeliskDto data, int id)
		{
			return await CreateNewOrUpdateExisting(data, id);
		}

		/// <summary>
		/// Edits an existing obelisk entry for a given id
		/// </summary>
		/// <param name="data">A representation of the obelisk to update</param>
		/// <param name="id">The id of the obelisk to update</param>
		/// <returns>A representation of the created or updated obelisk</returns>
		[HttpPatch("{id}")]
		[Authorize(Policy = "edit:ruinbasedata")]
		[SwaggerResponse(200, Type = typeof(ObeliskDto))]
		[SwaggerResponse(404, Description = "Not found")]
		[SwaggerResponse(409, Description = "Conflict")]
		public async Task<ObeliskDto> Update([FromBody] ObeliskDto data, int id)
		{
			return await UpdateExisting(data, id);
		}

		/// <summary>
		/// Deletes an existing obelisk based on its id
		/// </summary>
		/// <param name="id">The id of the obelisk to delete</param>
		/// <returns>200 if deletion was successful; 404 otherwise</returns>
		[HttpDelete("{id}")]
		[Authorize(Policy = "delete:ruinbasedata")]
		[SwaggerResponse(404, Description = "Not found")]
		public async Task<ActionResult> Delete(int id)
		{
			return await DeleteExisting(id);
		}

		/// <summary>
		/// Allows to search for obelisks within a given ruin type and obelisk group, both by id
		/// </summary>
		/// <param name="ruintypeId">The id of the ruin type to list obelisks for</param>
		/// <param name="obeliskgroupId">The id of the obelisk group to list obelisks for</param>
		/// <returns>A list of obelisks that match the filter criteria</returns>
		[HttpGet("search")]
		[SwaggerResponse(200, Type = typeof(ObeliskDto))] // TODO: When SwaggerUI is updated to 3.x make this return the array again.
		[SwaggerResponse(404, Description = "No obelisk found")]
		public async Task<List<ObeliskDto>> Search(int ruintypeId = 0, int obeliskgroupId = 0)
		{
			var result = await Repository.Search(ruintypeId, obeliskgroupId);

			return Mapper.Map<List<Obelisk>, List<ObeliskDto>>(result);
		}
	}
}
