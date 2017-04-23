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
	/// Provides methods for CRUD operations on <see cref="CodexData"/> objects
	/// </summary>
	[Route("v1/codex/data")]
	public class CodexDataController : BaseDataController<CodexData, CodexDataDto>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="CodexDataController"/> class
		/// </summary>
		/// <param name="logger">An instance of an <see cref="ILogger"/> to log.</param>
		/// <param name="repository">An instance of an <see cref="IArtifactRepository"/> to handle database access.</param>
		/// <param name="mapper">An instance of an <see cref="IMapper"/> to automatically map from and to DTOs.</param>
		public CodexDataController(ILogger<CodexDataController> logger, ICodexDataRepository repository, IMapper mapper)
			: base(logger, repository, mapper)
		{
		}

		/// <summary>
		/// Gets an array of codex data instances from the system
		/// </summary>
		/// <returns>A list of all available codex data entries</returns>
		[HttpGet()]
		[SwaggerResponse(200, Type = typeof(CodexDataDto))] // TODO: When SwaggerUI is updated to 3.x make this return the array again.
		[SwaggerResponse(404, Description = "Not found")]
		public async Task<IEnumerable<CodexDataDto>> Get()
		{
			return await GetAll();
		}

		/// <summary>
		/// Gets a single codex data entry based on its id
		/// </summary>
		/// <param name="id">The id of the codex data entry to select</param>
		/// <returns>A representation of the selected codex data entry</returns>
		[HttpGet("{id}")]
		[SwaggerResponse(200, Type = typeof(CodexDataDto))]
		[SwaggerResponse(404, Description = "Not found")]
		public async Task<CodexDataDto> Get(int id)
		{
			return await GetById(id);
		}

		/// <summary>
		/// Creates a new entry for an codex data entry
		/// </summary>
		/// <param name="data">A representation of the codex data entry to store</param>
		/// <returns>A representation of the freshly created codex data</returns>
		[HttpPost()]
		[Authorize(Policy = "add:codexdata")]
		[SwaggerResponse(200, Type = typeof(CodexDataDto))]
		public async Task<CodexDataDto> Create([FromBody] CodexDataDto data)
		{
			return await CreateNew(data);
		}

		/// <summary>
		/// Creates or updates an codex data entry with a given id
		/// </summary>
		/// <param name="data">A representation of the codex data entry to create or update</param>
		/// <param name="id">The id of the codex data entry to update or 0 to create a new one</param>
		/// <returns>A representation of the created or updated codex data</returns>
		[HttpPut("{id}")]
		[Authorize(Policy = "add:codexdata")]
		[Authorize(Policy = "edit:codexdata")]
		[SwaggerResponse(200, Type = typeof(CodexDataDto))]
		[SwaggerResponse(409, Description = "Conflict")]
		public async Task<CodexDataDto> CreateOrUpdate([FromBody] CodexDataDto data, int id)
		{
			return await CreateNewOrUpdateExisting(data, id);
		}

		/// <summary>
		/// Edits an existing codex data entry entry for a given id
		/// </summary>
		/// <param name="data">A representation of the codex data entry to update</param>
		/// <param name="id">The id of the codex data entry to update</param>
		/// <returns>A representation of the created or updated codex data</returns>
		[HttpPatch("{id}")]
		[Authorize(Policy = "edit:codexdata")]
		[SwaggerResponse(200, Type = typeof(CodexDataDto))]
		[SwaggerResponse(404, Description = "Not found")]
		[SwaggerResponse(409, Description = "Conflict")]
		public async Task<CodexDataDto> Update([FromBody] CodexDataDto data, int id)
		{
			return await UpdateExisting(data, id);
		}

		/// <summary>
		/// Deletes an existing codex data entry based on its id
		/// </summary>
		/// <param name="id">The id of the codex data entry to delete</param>
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
