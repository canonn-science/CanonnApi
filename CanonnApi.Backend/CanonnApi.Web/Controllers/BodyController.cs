using System;
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
using CanonnApi.Web.Services.RemoteApis;

namespace CanonnApi.Web.Controllers
{
	/// <summary>
	/// Provides methods for CRUD operations on <see cref="Body"/> objects
	/// </summary>
	[Route("v1/stellar/bodies")]
	public class BodyController : BaseDataController<Body, BodyDto>
	{
		private readonly IEdsmService _edsmService;

		/// <summary>
		/// Gets the <see cref="IBodyRepository"/>
		/// </summary>
		protected new IBodyRepository Repository => (IBodyRepository)base.Repository;

		/// <summary>
		/// Initializes a new instance of the <see cref="BodyController"/> class
		/// </summary>
		/// <param name="logger">An instance of an <see cref="ILogger"/> to log.</param>
		/// <param name="repository">An instance of an <see cref="IBodyRepository"/> to handle database access.</param>
		/// <param name="mapper">An instance of an <see cref="IMapper"/> to automatically map from and to DTOs.</param>
		/// <param name="edsmService">An instance of an <see cref="IEdsmService"/> to retrieve external id's and distances from</param>
		public BodyController(ILogger<BodyController> logger, IBodyRepository repository, IMapper mapper, IEdsmService edsmService)
			: base(logger, repository, mapper)
		{
			_edsmService = edsmService ?? throw new ArgumentNullException(nameof(edsmService));
		}

		/// <summary>
		/// Gets an array of body instances from the system
		/// </summary>
		/// <returns>A list of all available bodies</returns>
		[HttpGet()]
		[SwaggerResponse(200, Type = typeof(BodyDto))] // TODO: When SwaggerUI is updated to 3.x make this return the array again.
		[SwaggerResponse(404, Description = "Not found")]
		public async Task<IEnumerable<BodyDto>> Get()
		{
			return await GetAll();
		}

		/// <summary>
		/// Gets a single body based on its id
		/// </summary>
		/// <param name="id">The id of the body to select</param>
		/// <returns>A representation of the selected body</returns>
		[HttpGet("{id}")]
		[SwaggerResponse(200, Type = typeof(BodyDto))]
		[SwaggerResponse(404, Description = "Not found")]
		public async Task<BodyDto> Get(int id)
		{
			return await GetById(id);
		}

		/// <summary>
		/// Creates a new entry for an body
		/// </summary>
		/// <param name="data">A representation of the body to store</param>
		/// <returns>A representation of the freshly created body</returns>
		[HttpPost()]
		[Authorize(Policy = "add:systemdata")]
		[SwaggerResponse(200, Type = typeof(BodyDto))]
		public async Task<BodyDto> Create([FromBody] BodyDto data)
		{
			return await CreateNew(data);
		}

		/// <summary>
		/// Creates or updates a body with a given id
		/// </summary>
		/// <param name="data">A representation of the body create or update</param>
		/// <param name="id">The id of the body to update or 0 to create a new one</param>
		/// <returns>A representation of the created or updated body</returns>
		[HttpPut("{id}")]
		[Authorize(Policy = "add:systemdata")]
		[Authorize(Policy = "edit:systemdata")]
		[SwaggerResponse(200, Type = typeof(BodyDto))]
		[SwaggerResponse(409, Description = "Conflict")]
		public async Task<BodyDto> CreateOrUpdate([FromBody] BodyDto data, int id)
		{
			return await CreateNewOrUpdateExisting(data, id);
		}

		/// <summary>
		/// Edits an existing body entry for a given id
		/// </summary>
		/// <param name="data">A representation of the body to update</param>
		/// <param name="id">The id of the body to update</param>
		/// <returns>A representation of the created or updated body</returns>
		[HttpPatch("{id}")]
		[Authorize(Policy = "edit:systemdata")]
		[SwaggerResponse(200, Type = typeof(BodyDto))]
		[SwaggerResponse(404, Description = "Not found")]
		[SwaggerResponse(409, Description = "Conflict")]
		public async Task<BodyDto> Update([FromBody] BodyDto data, int id)
		{
			return await UpdateExisting(data, id);
		}

		/// <summary>
		/// Deletes an existing body based on its id
		/// </summary>
		/// <param name="id">The id of the body to delete</param>
		/// <returns>200 if deletion was successful; 404 otherwise</returns>
		[HttpDelete("{id}")]
		[Authorize(Policy = "delete:systemdata")]
		[SwaggerResponse(404, Description = "Not found")]
		public async Task<ActionResult> Delete(int id)
		{
			return await DeleteExisting(id);
		}

		/// <summary>
		/// Fetches external body ids and distance to arrival from EDSM
		/// </summary>
		/// <returns>A list of all the updated bodies</returns>
		[HttpGet("updateEdsmIds")]
		[Authorize(Policy = "edit:systemdata")]
		[SwaggerResponse(200, Type = typeof(EdsmUpdatedBody))] // TODO: When SwaggerUI is updated to 3.x make this return the array again.
		public async Task<List<EdsmUpdatedBody>> UpdateEdsmIds()
		{
			var bodies = await Repository.GetAllWithSystems();

			var updatedBodies = await _edsmService.FetchBodyIds(bodies.Where(body => body.EdsmExtId == null || body.Distance == null));
			await Repository.SaveChanges();

			var result = new List<EdsmUpdatedBody>();

			foreach (var updatedBody in updatedBodies)
			{
				var edsmUpdatedBody = Mapper.Map<Body, EdsmUpdatedBody>(updatedBody.Body);
				edsmUpdatedBody.IsUpdated = updatedBody.Updated;
				edsmUpdatedBody.ErrorMessage = updatedBody.ErrorMessage;

				result.Add(edsmUpdatedBody);
			}

			return result;
		}
	}
}
