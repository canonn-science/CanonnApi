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
	/// Provides methods for CRUD operations on <see cref="CodexCategory"/> objects
	/// </summary>
	[Route("v1/codex/categories")]
	public class CodexCategoryController : BaseDataController<CodexCategory, CodexCategoryDto>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="CodexCategoryController"/> class
		/// </summary>
		/// <param name="logger">An instance of an <see cref="ILogger"/> to log.</param>
		/// <param name="repository">An instance of an <see cref="ICodexCategoryRepository"/> to handle database access.</param>
		/// <param name="mapper">An instance of an <see cref="IMapper"/> to automatically map from and to DTOs.</param>
		public CodexCategoryController(ILogger<CodexCategoryController> logger, ICodexCategoryRepository repository, IMapper mapper)
			: base(logger, repository, mapper)
		{
		}

		/// <summary>
		/// Gets an array of codex category instances from the system
		/// </summary>
		/// <returns>A list of all available codex categories</returns>
		[HttpGet()]
		[SwaggerResponse(200, Type = typeof(CodexCategoryDto))] // TODO: When SwaggerUI is updated to 3.x make this return the array again.
		[SwaggerResponse(404, Description = "Not found")]
		public async Task<IEnumerable<CodexCategoryDto>> Get()
		{
			return await GetAll();
		}

		/// <summary>
		/// Gets a single codex category based on its id
		/// </summary>
		/// <param name="id">The id of the codex category to select</param>
		/// <returns>A representation of the selected codex category</returns>
		[HttpGet("{id}")]
		[SwaggerResponse(200, Type = typeof(CodexCategoryDto))]
		[SwaggerResponse(404, Description = "Not found")]
		public async Task<CodexCategoryDto> Get(int id)
		{
			return await GetById(id);
		}

		/// <summary>
		/// Creates a new entry for a codex category
		/// </summary>
		/// <param name="data">A representation of the codex category to store</param>
		/// <returns>A representation of the freshly created codex category</returns>
		[HttpPost()]
		[Authorize(Policy = "add:codexdata")]
		[SwaggerResponse(200, Type = typeof(CodexCategoryDto))]
		public async Task<CodexCategoryDto> Create([FromBody] CodexCategoryDto data)
		{
			return await CreateNew(data);
		}

		/// <summary>
		/// Creates or updates a codex category with a given id
		/// </summary>
		/// <param name="data">A representation of the codex category to create or update</param>
		/// <param name="id">The id of the codex category to update or 0 to create a new one</param>
		/// <returns>A representation of the created or updated codex category</returns>
		[HttpPut("{id}")]
		[Authorize(Policy = "add:codexdata")]
		[Authorize(Policy = "edit:codexdata")]
		[SwaggerResponse(200, Type = typeof(CodexCategoryDto))]
		[SwaggerResponse(409, Description = "Conflict")]
		public async Task<CodexCategoryDto> CreateOrUpdate([FromBody] CodexCategoryDto data, int id)
		{
			return await CreateNewOrUpdateExisting(data, id);
		}

		/// <summary>
		/// Edits an existing codex category entry for a given id
		/// </summary>
		/// <param name="data">A representation of the codex category to update</param>
		/// <param name="id">The id of the codex category to update</param>
		/// <returns>A representation of the created or updated codex category</returns>
		[HttpPatch("{id}")]
		[Authorize(Policy = "edit:codexdata")]
		[SwaggerResponse(200, Type = typeof(CodexCategoryDto))]
		[SwaggerResponse(404, Description = "Not found")]
		[SwaggerResponse(409, Description = "Conflict")]
		public async Task<CodexCategoryDto> Update([FromBody] CodexCategoryDto data, int id)
		{
			return await UpdateExisting(data, id);
		}

		/// <summary>
		/// Deletes an existing codex category based on its id
		/// </summary>
		/// <param name="id">The id of the codex category to delete</param>
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
